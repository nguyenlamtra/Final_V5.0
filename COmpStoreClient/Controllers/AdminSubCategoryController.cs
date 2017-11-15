using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStoreClient.WebServiceAccess.Base;
using COmpStore.Models.ViewModels.SubCategoryAdmin;
using COmpStoreClient.Filters;

namespace COmpStoreClient.Controllers
{
    [ValidateAdmin]
    public class AdminSubCategoryController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;
        public AdminSubCategoryController(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }

        public IActionResult Create(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubCategoryAdminCreate model)
        {
            if (ModelState.IsValid)
            {
                var result = await _webApiCalls.CreateSubCategory(model);
                if (result.Equals("1"))
                {
                    ViewBag.IsSuccess = true;
                    ViewBag.CategoryId = model.CategoryId;
                    return View();
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var sub = await _webApiCalls.GetSingleSubCategory(id);
            return View(sub);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SubCategoryAdminUpdate model)
        {
            if (ModelState.IsValid)
            {
                var result = await _webApiCalls.UpdateSubCategory(model);
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
                var result = await _webApiCalls.DeleteSubCategory(id);
                if (result.Equals("0"))
                    return Json(false);
            }
            return Json(true);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _webApiCalls.DetailsSubCategory(id);
            if (model != null)
                return View(model);
            else
                return RedirectToAction("Index");
        }
    }
}