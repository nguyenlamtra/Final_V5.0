﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStoreClient.WebServiceAccess.Base;
using COmpStore.Models.ViewModels.Base;
using COmpStore.Models.ViewModels.Cart;
using COmpStoreClient.Extension;
using COmpStore.Models.ViewModels.Paging;

namespace COmpStoreClient.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;
        public ProductsController(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }


        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction(nameof(Featured));
        }

        public async Task<ActionResult> Details(int id)
        {
            var prod = await _webApiCalls.GetOneProductAsync(id);
            ViewData["publisher"] = await _webApiCalls.GetProductsForPublisherAsync(prod.PublisherId);
            return View(prod);
        }

        internal async Task<IActionResult> GetListOfProducts(int pageNumber = 1,
            int id = -1, bool featured = false, string searchString = "")
        {
            IList<ProductAndSubCategoryBase> prods;

            if (featured)
            {
                prods = await _webApiCalls.GetFeaturedProductsAsync(pageNumber);
            }
            else if (!string.IsNullOrEmpty(searchString))
            {
                prods = await _webApiCalls.SearchAsync(searchString);
            }
            else
            {
                prods = await _webApiCalls.GetProductsForSubCategoryAsync(id);
            }

            if (prods == null)
            {
                return NotFound();
            }
            return View("ProductList", prods);
        }
        internal async Task<IActionResult> GetListOfProducts2(int id = -1)
        {
            IList<ProductAndPublisherBase> prods;
            prods = await _webApiCalls.GetProductsForPublisherAsync(id);
            if (prods == null)
            {
                return NotFound();
            }
            return View("ProductList", prods);
        }
        // Get all Product list (Featured = true)
        [HttpGet]
        public async Task<IActionResult> Featured(int pageNumber)
        {
            ViewBag.Title = "Featured Products";
            ViewBag.Header = "Featured Products";
            ViewBag.ShowSubCategory = true;
            ViewBag.Featured = true;
            return await GetListOfProducts(pageNumber, featured: true);
        }
        // Get Subcategory by id
        [HttpGet]
        public async Task<IActionResult> ProductList(int pageNumber, int id)
        {
            var subcat = await _webApiCalls.GetSubCategoryAsync(id);
            ViewBag.Title = subcat?.SubCategoryName;
            ViewBag.Header = subcat?.SubCategoryName;
            ViewBag.ShowSubCategory = false;
            ViewBag.Featured = false;
            return await GetListOfProducts(pageNumber, id);
        }
        public async Task<IActionResult> PublisherList(int id)
        {
            var pubcat = await _webApiCalls.GetPublisherAsync(id);
            ViewBag.Title = pubcat?.PublisherName;
            ViewBag.Header = pubcat?.PublisherName;
            ViewBag.ShowPublisher = false;
            ViewBag.Featured = false;
            return await GetListOfProducts2(id);
        }

        // Search url = products/search?searchString = 
        [Route("[controller]/[action]")]
        [HttpPost("{searchString}")]
        public async Task<IActionResult> Search(int pageNumber, string searchString)
        {
            ViewBag.Title = searchString;
            ViewBag.Header = searchString;
            ViewBag.SubCategory = true;
            ViewBag.Featured = false;
            return await GetListOfProducts(pageNumber, searchString: searchString);
        }

        //private readonly IWebApiCalls _webApiCalls;
        //public ProductsController(IWebApiCalls webApiCalls)
        //{
        //    _webApiCalls = webApiCalls;
        //}


        //[HttpGet]
        //public ActionResult Error()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult Index()
        //{
        //    return RedirectToAction(nameof(Featured));
        //}

        //public async Task<ActionResult> Details(int id)
        //{
        //    var prod = await _webApiCalls.GetOneProductAsync(id);
        //    ViewData["publisher"] = await _webApiCalls.GetProductsForPublisherAsync(prod.PublisherId);
        //    return View(prod);
        //}

        //internal async Task<IActionResult> GetListOfProducts(int pageNumber = 1,
        //    int id = -1, bool featured = false, string searchString = "")
        //{
        //    PageOutput<ProductAndSubCategoryBase> pageOutput;

        //    if (featured)
        //    {
        //        pageOutput = await _webApiCalls.GetFeaturedProductsAsync(pageNumber);
        //    }
        //    else if (!string.IsNullOrEmpty(searchString))
        //    {
        //        pageOutput = await _webApiCalls.SearchAsync(searchString, pageNumber);
        //        ViewBag.SearchString = searchString;
        //    }
        //    else
        //    {
        //        pageOutput = await _webApiCalls.GetProductsForSubCategoryAsync(id, pageNumber);
        //        ViewBag.SubCategoryId = id;
        //    }

        //    if (pageOutput == null)
        //    {
        //        return NotFound();
        //    }
        //    return View("ProductList", pageOutput);

        //    //IList<ProductAndSubCategoryBase> prods;

        //    //if (featured)
        //    //{
        //    //    prods = await _webApiCalls.GetFeaturedProductsAsync(pageNumber);
        //    //}
        //    //else if (!string.IsNullOrEmpty(searchString))
        //    //{
        //    //    prods = await _webApiCalls.SearchAsync(searchString);
        //    //}
        //    //else
        //    //{
        //    //    prods = await _webApiCalls.GetProductsForSubCategoryAsync(id);
        //    //}

        //    //if (prods == null)
        //    //{
        //    //    return NotFound();
        //    //}
        //    //return View("ProductList", prods);
        //}
        //internal async Task<IActionResult> GetListOfProducts2(int id = -1)
        //{
        //    //IList<ProductAndPublisherBase> prods;
        //    //prods = await _webApiCalls.GetProductsForPublisherAsync(id);

        //    PageOutput<ProductAndSubCategoryBase> pageOutput;
        //    pageOutput = await _webApiCalls.GetProductsForPublisherAsync(id);

        //    if (pageOutput == null)
        //    {
        //        return NotFound();
        //    }
        //    return View("ProductList", pageOutput);
        //}
        //// Get all Product list (Featured = true)
        //[HttpGet]
        //public async Task<IActionResult> Featured(int pageNumber = 1)
        //{
        //    ViewBag.Title = "Featured Products";
        //    ViewBag.Header = "Featured Products";
        //    ViewBag.ShowSubCategory = true;
        //    ViewBag.Featured = true;
        //    return await GetListOfProducts(pageNumber, featured: true);
        //}
        //// Get Subcategory by id
        //[HttpGet]
        //public async Task<IActionResult> ProductList(int id, int pageNumber = 1)
        //{
        //    var subcat = await _webApiCalls.GetSubCategoryAsync(id);
        //    ViewBag.Title = subcat?.SubCategoryName;
        //    ViewBag.Header = subcat?.SubCategoryName;
        //    ViewBag.ShowSubCategory = false;
        //    ViewBag.Featured = false;
        //    return await GetListOfProducts(pageNumber, id);
        //}
        //public async Task<IActionResult> PublisherList(int id)
        //{
        //    var pubcat = await _webApiCalls.GetPublisherAsync(id);
        //    ViewBag.Title = pubcat?.PublisherName;
        //    ViewBag.Header = pubcat?.PublisherName;
        //    ViewBag.ShowPublisher = false;
        //    ViewBag.Featured = false;
        //    return await GetListOfProducts2(id);
        //}

        //// Search url = products/search?searchString = 
        //[Route("[controller]/[action]")]
        //[HttpPost("{searchString}")]
        //public async Task<IActionResult> Search(string searchString, int pageNumber = 1)
        //{
        //    ViewBag.Title = searchString;
        //    ViewBag.Header = searchString;
        //    ViewBag.SubCategory = true;
        //    ViewBag.Featured = false;
        //    return await GetListOfProducts(pageNumber, searchString: searchString);
        //}

    }
}





































//private readonly IWebApiCalls _webApiCalls;
//public ProductsController(IWebApiCalls webApiCalls)
//{
//    _webApiCalls = webApiCalls;
//}


//[HttpGet]
//public ActionResult Error()
//{
//    return View();
//}

//[HttpGet]
//public ActionResult Index()
//{
//    return RedirectToAction(nameof(Featured));
//}

//internal async Task<IActionResult> GetListOfProducts(
//    int id = -1, bool featured = false, string searchString = "")
//{
//    IList<ProductAndSubCategoryBase> prods;
//    if (featured)
//    {
//        prods = await _webApiCalls.GetFeaturedProductsAsync();
//    }
//    else if (!string.IsNullOrEmpty(searchString))
//    {
//        prods = await _webApiCalls.SearchAsync(searchString);
//    }
//    else
//    {
//        prods = await _webApiCalls.GetProductsForSubCategoryAsync(id);
//    }

//    if (prods == null)
//    {
//        return NotFound();
//    }
//    return View("ProductList", prods);
//}
//internal async Task<IActionResult> GetListOfProducts2(int id = -1)
//{
//    IList<ProductAndPublisherBase> prods;
//    prods = await _webApiCalls.GetProductsForPublisherAsync(id);
//    if (prods == null)
//    {
//        return NotFound();
//    }
//    return View("ProductList", prods);
//}
//// Get all Product list (Featured = true)
//[HttpGet]
//public async Task<IActionResult> Featured()
//{
//    ViewBag.Title = "Featured Products";
//    ViewBag.Header = "Featured Products";
//    ViewBag.ShowSubCategory = true;
//    ViewBag.Featured = true;
//    return await GetListOfProducts(featured: true);
//}
//// Get Subcategory by id
//[HttpGet]
//public async Task<IActionResult> ProductList(int id)
//{
//    var subcat = await _webApiCalls.GetSubCategoryAsync(id);
//    ViewBag.Title = subcat?.SubCategoryName;
//    ViewBag.Header = subcat?.SubCategoryName;
//    ViewBag.ShowSubCategory = false;
//    ViewBag.Featured = false;
//    return await GetListOfProducts(id);
//}
//public async Task<IActionResult> PublisherList(int id)
//{
//    var pubcat = await _webApiCalls.GetPublisherAsync(id);
//    ViewBag.Title = pubcat?.PublisherName;
//    ViewBag.Header = pubcat?.PublisherName;
//    ViewBag.ShowPublisher = false;
//    ViewBag.Featured = false;
//    return await GetListOfProducts2(id);
//}

//// Search url = products/search?searchString = 
//[Route("[controller]/[action]")]
//[HttpPost("{searchString}")]
//public async Task<IActionResult> Search(string searchString)
//{
//    ViewBag.Title = searchString;
//    ViewBag.Header = searchString;
//    ViewBag.SubCategory = true;
//    ViewBag.Featured = false;
//    return await GetListOfProducts(searchString: searchString);
//}
