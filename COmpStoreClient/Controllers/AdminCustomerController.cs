using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStoreClient.WebServiceAccess.Base;
using COmpStoreClient.Filters;
using COmpStore.Models.ViewModels.Customer;

namespace COmpStoreClient.Controllers
{
    [ValidateAdmin]
    public class AdminCustomerController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;
        public AdminCustomerController(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }

        public async Task<IActionResult> Index()
            => View(await _webApiCalls.GetAdminCustomerIndex());

        public async Task<IActionResult> Details(int id)
            => View(await _webApiCalls.GetAdminCustomerDetails(id));

        public IActionResult Create() => View();
        public IActionResult Error() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CustomerAdminCreate model)
        {
            if (ModelState.IsValid)
            {
                var result = await _webApiCalls.CreateCustomer(model);
                if (result.Equals("1"))
                {
                    ViewBag.IsSuccess = true;
                    return View();
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var customer = await _webApiCalls.GetSingleCustomer(id);
            if (customer != null)
                return View(customer);
            else
                return RedirectToAction("Index");
        }
            

        [HttpPost]
        public async Task<IActionResult> Update(CustomerAdminUpdate model)
        {
            if (ModelState.IsValid)
            {
                var result = await _webApiCalls.UpdateCustomer(model);
                if (result.Equals("1"))
                {
                    ViewBag.IsSuccess = true;
                    return View();
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int[] ids)
        {
            foreach (var id in ids)
            {
                var result = await _webApiCalls.DeleteCustomer(id);
                if (result.Equals("0"))
                    return Json(false);
            }
            return Json(true);
        }
    }
}