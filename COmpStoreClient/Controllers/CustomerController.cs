using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.Models.ViewModels.Customer;
using COmpStoreClient.WebServiceAccess.Base;
using COmpStoreClient.Extension;
using COmpStore.Models.Enum;

namespace COmpStoreClient.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;
        public CustomerController(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }

        public IActionResult LoginCustomer()
        {
            return View("LoginCustomer", new CustomerModel());
        }

        [HttpPost]
        public async Task<IActionResult> LoginCustomer(CustomerLogin model)
        {
            if (!ModelState.IsValid)
                return View("LoginCustomer", new CustomerModel() { CustomerLogin = model });
            else
            {
                var token = await _webApiCalls.VerifyAccount(model);
                if (token != null && token.Role == EnumRole.Customer.ToString())
                {
                    HttpContext.Session.SetAuthSession(token);
                    return RedirectToAction("Index", "Products");
                }
                else
                    ModelState.AddModelError("", "Email or Password wrong!");
            }
            return View("LoginCustomer", new CustomerModel { CustomerLogin = model });
        }

        [HttpPost]
        public async Task<IActionResult> Register(CustomerCreate model)
        {
            if (!ModelState.IsValid)
                return View("LoginCustomer", new CustomerModel() { CustomerCreate = model });
            var result = await _webApiCalls.CreateAccountForUser(model);
            if (result.Equals("1"))
            {
                var token = await _webApiCalls.VerifyAccount(new CustomerLogin
                {
                    EmailAddress = model.EmailAddress,
                    Password = model.Password
                });
                HttpContext.Session.SetAuthSession(token);
                return RedirectToAction("Index", "Products");
            }
            else
                ModelState.AddModelError("", "Email already exist");
            return View(model);
        }
    }
}