﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStoreClient.WebServiceAccess.Base;
using COmpStore.Models.ViewModels.ProductAdmin;
using COmpStoreClient.Filters;
using COmpStore.Models.ViewModels.Paging;

namespace COmpStoreClient.Controllers
{
    [ValidateAdmin]
    public class AdminProductController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;
        public AdminProductController(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            if (pageNumber > 0)
            {
                PageOutput<ProductAdminIndex> pageOutput;
                pageOutput = await _webApiCalls.GetAdminProductIndex(pageNumber);
                return View(pageOutput);
            }
            return View();
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.SubCategories = await _webApiCalls.GetSubCategoryForCombobox();
            ViewBag.Publishers = await _webApiCalls.GetPublisherForCombobox();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductAdminCreate model)
        {
            ViewBag.SubCategories = await _webApiCalls.GetSubCategoryForCombobox();
            ViewBag.Publishers = await _webApiCalls.GetPublisherForCombobox();
            if (ModelState.IsValid)
            {
                var result = await _webApiCalls.CreateProduct(model);
                if (result.Equals("1"))
                    ViewBag.IsSuccess = true;
                return View();
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _webApiCalls.GetSingleProduct(id);

            if (product != null)
            {
                ViewBag.SubCategories = await _webApiCalls.GetSubCategoryForCombobox();
                ViewBag.Publishers = await _webApiCalls.GetPublisherForCombobox();
                ViewBag.SubCategoryId = product.SubCategoryId;
                ViewBag.PublisherId = product.PublisherId;

                return View(product);
            }
            else
                return RedirectToAction("Index","AdminProduct");
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductAdminUpdate model)
        {
            ViewBag.SubCategories = await _webApiCalls.GetSubCategoryForCombobox();
            ViewBag.Publishers = await _webApiCalls.GetPublisherForCombobox();
            ViewBag.SubCategoryId = model.SubCategoryId;
            ViewBag.PublisherId = model.PublisherId;
            if (ModelState.IsValid)
            {
                var result = await _webApiCalls.UpdateProduct(model);
                if (result.Equals("1"))
                    ViewBag.IsSuccess = true;
                var product = await _webApiCalls.GetSingleProduct(model.Id);
                if(product!=null)
                    return View(product);
                else
                    return RedirectToAction("Index", "AdminProduct");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int[] ids)
        {
            foreach (var id in ids)
            {
                var result = await _webApiCalls.DeleteProduct(id);
                if (result.Equals("0"))
                    return Json(false);
            }
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeIsFeature(int id)
        {
            var result = await _webApiCalls.UpdateIsFeature(id);
            return result.Equals("1") ? Json(true) : Json(false);    
        }
    }
}