using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStoreClient.WebServiceAccess.Base;
using COmpStore.Models.ViewModels.Customer;
using COmpStoreClient.Extension;
using COmpStoreClient.Filters;
using COmpStoreClient.WebServiceAccess;

namespace COmpStoreClient.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;
        public AdminController(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }

        [ValidateAdmin]
        public IActionResult Index() => View();

        public IActionResult Error() => View();

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(CustomerLogin model)
        {
            if (ModelState.IsValid)
            {
                var token = await _webApiCalls.VerifyAccount(model);
                if (token != null)
                {
                    HttpContext.Session.SetAuthSession(token);
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View(model);
        }
    }
}