using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using COmpStore.Models.ViewModels.Customer;
using COmpStore.Models.Entities;
using COmpStore.DAL.Repos.Interfaces;
using COmpStoreApi.Filters;
using COmpStoreApi.Helper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using COmpStore.Models.ViewModels;

namespace COmpStoreApi.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private IConfiguration _configuration;
        private ILogger<AuthController> _logger;
        private ICustomerRepo _customerRepo { get; set; }

        public AuthController(IConfiguration configuration, ILogger<AuthController> logger, ICustomerRepo customerRepo)
        {
            _logger = logger;
            _configuration = configuration;
            _customerRepo = customerRepo;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("checkpermission")]
        [Authorize(Policy ="Admin")]
        public int Check()
        {
            return 1;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        [ValidateForm]
        public int Register([FromBody] CustomerRegister model)
        {
            var customer = new Customer()
            {
                EmailAddress = model.EmailAddress,
                FullName = model.FullName,
                Password = StringHelper.EncryptPassword(model.Password),
                Role = model.Role
            };

            return _customerRepo.Add(customer);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public SessionAuth GetTokenAsync([FromBody] CustomerLogin model)
        {
            try
            {
                var customer = _customerRepo.GetCustomerByEmail(model.EmailAddress);
                if (customer == null)
                {
                    return null;
                }
                var pass = StringHelper.EncryptPassword(model.Password);
                if (customer.Password == pass)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a secret that needs to be at least 16 characters long"));
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, customer.EmailAddress));
                    claims.Add(new Claim(JwtRegisteredClaimNames.Email, customer.EmailAddress));
                    claims.Add(new Claim(ClaimTypes.Role, customer.Role));

                    var token = new JwtSecurityToken(
                        issuer: "your app",
                        audience: "the client of your app",
                        claims: claims,
                        notBefore: DateTime.Now,
                        expires: DateTime.Now.AddHours(24),
                        signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
                    );

                    string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return new SessionAuth
                    {
                        Role=customer.Role,
                        Token=jwtToken,
                        EmailAddress = customer.EmailAddress
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error while creating token: {ex}");
                return null;
            }
        }
    }
}