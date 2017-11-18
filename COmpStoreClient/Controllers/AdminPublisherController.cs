using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStoreClient.WebServiceAccess.Base;
using COmpStore.Models.ViewModels.PublisherAdmin;
using COmpStoreClient.Filters;

namespace COmpStoreClient.Controllers
{
    [ValidateAdmin]
    public class AdminPublisherController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;
        public AdminPublisherController(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(await _webApiCalls.GetAdminPublisherIndex());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(PublisherAdminCreate model)
        {
            if (ModelState.IsValid)
            {
                var result = await _webApiCalls.CreatePublisher(model);
                if (result.Equals("1"))
                    ViewBag.IsSuccess = true;
                return View();
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var publisher = await _webApiCalls.GetSinglePublisher(id);

            if (publisher != null)
                return View(publisher);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(PublisherAdminUpdate model)
        {
            if (ModelState.IsValid)
            {
                var result = await _webApiCalls.UpdatePublisher(model);
                if (result.Equals("1"))
                    ViewBag.IsSuccess = true;
                return View();
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int[] ids)
        {
            foreach (var id in ids)
            {
                var result = await _webApiCalls.DeletePublisher(id);
                if (result.Equals("0"))
                    return Json(false);
            }
            return Json(true);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _webApiCalls.DetailsPublisher(id);
            if (model != null)
                return View(model);
            else
                return RedirectToAction("Index");
        }
    }
}