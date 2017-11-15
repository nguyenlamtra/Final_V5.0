using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStoreClient.WebServiceAccess.Base;
using COmpStore.Models.ViewModels;
using COmpStore.Models.Entities;

namespace COmpStoreClient.Controllers
{
    [Route("[controller]/[action]/{customerId}")]
    public class OrdersController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;

        public OrdersController(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int customerId)
        {
            ViewBag.Title = "Order History";
            ViewBag.Header = "Order History";
            IList<Order> orders = await _webApiCalls.GetOrdersAsync(customerId);
            if (orders == null) return NotFound();
            return View(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Details(int customerId, int orderId)
        {
            ViewBag.Title = "Order Details";
            ViewBag.Header = "Order Details";
            OrderWithDetailsAndProductInfo orderDetails = await _webApiCalls.GetOrderDetailsAsync(customerId, orderId);
            if (orderDetails == null) return NotFound();
            return View(orderDetails);
        }

    }
}