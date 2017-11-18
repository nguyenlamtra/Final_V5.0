using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using COmpStoreClient.Configuration;
using COmpStoreClient.WebServiceAccess;
using COmpStoreClient.WebServiceAccess.Base;
using COmpStoreClient.Filters;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace COmpStoreClient
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddSingleton(_ => Configuration);
            services.AddSingleton<IWebServiceLocator, WebServiceLocator>();
            services.AddSingleton<IWebApiCalls, WebApiCalls>();
            services.AddMvc();
            //services.AddMvc(config => {
            //    config.Filters.Add(
            //        new AuthActionFilter(services.BuildServiceProvider().GetService<IAuthHelper>()));
            //});
            //services.BuildServiceProvider().GetService<IAuthHelper>()
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                //app.UseExceptionHandler(configure =>
                //{
                //    configure.Run(async context =>
                //    {
                //        var ex = context.Features
                //                        .Get<IExceptionHandlerFeature>()
                //                        .Error;
                //        var a = ex.GetType();
                //        if (ex.GetType() == typeof(WebException))
                //        {
                //            context.Response.Redirect("/Admin/Login");
                //        }
                        
                //    });
                //});
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseSession();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Admin}/{action=Login}/{id?}");
            });
        }
    }
}
