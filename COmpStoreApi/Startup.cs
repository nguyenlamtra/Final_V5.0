using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using COmpStoreApi.Filters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

using COmpStore.DAL.Repos;
using COmpStore.DAL.EF;
using COmpStore.DAL.Repos.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace COmpStoreApi
{
    public class Startup
    {
        private IHostingEnvironment _env;
        public Startup(IHostingEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            _env = env;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // Add framework services.
            //services.AddMvcCore();
            services.AddMvcCore(config =>
                config.Filters.Add(new StoreCompExceptionFilter(_env.IsDevelopment())))
                .AddJsonFormatters(j =>
                {
                    j.ContractResolver = new DefaultContractResolver();
                    j.Formatting = Formatting.Indented;
                });

            // http://docs.asp.net/en/latest/security/cors.html
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials();
                });
            });
            //NOTE: Did not disable mixed mode running here
            services.AddDbContext<StoreContext>(options => options.UseSqlServer(
                                                    Configuration.GetConnectionString("StoreComp")));

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is the secret keys of Lam And Tra")),

                    ValidateIssuer = true,
                    ValidIssuer = "my app",

                    ValidateAudience = true,
                    ValidAudience = "the clients",

                    ValidateLifetime = true, //validate the expiration and not before values in the token

                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                                  policy.RequireClaim(ClaimTypes.Role, "Admin"));
                options.AddPolicy("User", policy =>
                                  policy.RequireClaim(ClaimTypes.Role, "User"));
            });

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Tea & Lam API", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "COmpStoreApi.xml");
                c.IncludeXmlComments(xmlPath);
            });

            services.AddScoped<ICategoryRepo,CategoryRepo>();
            services.AddScoped<ISubCategoryRepo,SubCategoryRepo>();
            services.AddScoped<IPublisherRepo,PublisherRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<ICustomerRepo, CustomerRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IOrderDetailRepo, OrderDetailRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                loggerFactory.AddDebug();
            }

            //if (env.IsDevelopment())
            //{
            //    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //    {
            //        StoreDataInitializer.InitializeData(app.ApplicationServices);
            //    }
            //}

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseAuthentication();
            app.UseCors("AllowAll");  // has to go before UseMvc
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
