using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStoreClient.WebServiceAccess.Base;
using AutoMapper;
using Newtonsoft.Json;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels;
using COmpStore.Models.Entities.ViewModels.Base;
using COmpStore.Models.ViewModels.Cart;
using COmpStoreClient.Extension;
using COmpStoreClient.Exceptions;
using COmpStoreClient.Filters;

namespace COmpStoreClient.Controllers
{
    public class CartController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;
        public CartController(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }

        //============================================================
        [HttpPost]
        public IActionResult ChangeQuantity([FromBody]SelectedProduct selectedProduct)
        {
            var selectedProducts = HttpContext.Session.GetSelectedProducts();
            if (selectedProduct.Quantity > 0)
            {
                var temp = selectedProducts.SingleOrDefault(x => x.ProductId == selectedProduct.ProductId);
                temp.Quantity = selectedProduct.Quantity;
                HttpContext.Session.SetSelectedProducts(selectedProducts);
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        [ValidateCustomer]
        public async Task<IActionResult> ShoppingCart()
        {
            var selectedProducts = HttpContext.Session.GetSelectedProducts();
            var viewModel = await _webApiCalls.GetCartView(selectedProducts.Select(x => x.ProductId).ToArray());
            selectedProducts.OrderBy(x => x.ProductId);
            viewModel.OrderBy(x => x.ProductId);
            ViewBag.Products = viewModel;

            return View(new OrderModel { SelectedProducts = selectedProducts });
        }

        [HttpPost]
        [ValidateCustomer]
        public async Task<IActionResult> ShoppingCart(OrderModel orderModel)
        {
            if (ModelState.IsValid)
            {
                var auth = HttpContext.Session.GetAuthSession();
                if (auth == null) throw new AuthCustomerException();
                orderModel.EmailAddress = auth.EmailAddress;
                var result = await _webApiCalls.SaveOrder(orderModel);
                if (result.Equals("1"))
                {
                    TempData["OrderSuccess"] = "true";
                    HttpContext.Session.ClearSelectedProducts();
                    return RedirectToAction("Index", "Products");
                }
            }
            var viewModel = await _webApiCalls.GetCartView(orderModel.SelectedProducts.Select(x => x.ProductId).ToArray());
            orderModel.SelectedProducts.OrderBy(x => x.ProductId);
            viewModel.OrderBy(x => x.ProductId);
            ViewBag.Products = viewModel;

            return View(orderModel);
        }

        public IActionResult RemoveProductFromCart(int productId)
        {
            var selectedProducts = HttpContext.Session.GetSelectedProducts();
            var product = selectedProducts.SingleOrDefault(x => x.ProductId == productId);
            if (product != null)
            {
                selectedProducts.Remove(product);
                HttpContext.Session.SetSelectedProducts(selectedProducts);
                return Json(true);
            }
            else return Json(false);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var product = await _webApiCalls.GetSingleProduct(productId);
            if (product != null && product.UnitsInStock > 0)
            {
                var selectedProducts = HttpContext.Session.GetSelectedProducts();
                var selectedProduct = selectedProducts.SingleOrDefault(x => x.ProductId == productId);

                if (selectedProduct != null)
                    if (selectedProduct.Quantity + 1 > product.UnitsInStock)
                        return Json(false);
                    else
                        selectedProduct.Quantity++;
                else
                    selectedProducts.Add(new SelectedProduct { ProductId = productId, Quantity = 1 });
                HttpContext.Session.SetSelectedProducts(selectedProducts);
                return Json(true);
            }
            else
                return Json(false);



        }
        //============================================================
    }
}