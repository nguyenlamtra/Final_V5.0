using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStoreClient.WebServiceAccess.Base;
using COmpStore.Models.ViewModels.CategoryAdmin;
using COmpStoreClient.Filters;
using COmpStoreClient.Extension;

namespace COmpStoreClient.Controllers
{
    [ValidateAdmin]
    public class AdminCategoryController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;
        public AdminCategoryController(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IList<CategoryAdminIndex> categorys;
            
            categorys = await _webApiCalls.GetAdminCategoryIndex();
            return View(categorys);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryAdminCreate model)
        {
            if (ModelState.IsValid)
            {
                var result = await _webApiCalls.CreateCategory(model);
                if (result.Equals("1"))
                    ViewBag.IsSuccess = true;
                return View();
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var category = await _webApiCalls.GetSingleCategory(id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryAdminUpdate model)
        {
            if (ModelState.IsValid)
            {
                var result = await _webApiCalls.UpdateCategory(model);
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
                var result = await _webApiCalls.DeleteCategory(id);
                if (result.Equals("0"))
                    return Json(false);
            }
            return Json(true);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _webApiCalls.DetailsCategory(id);
            if (model != null)
                return View(model);
            else
                return RedirectToAction("Index");
        }
    }
}