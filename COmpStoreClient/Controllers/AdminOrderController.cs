using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStoreClient.Filters;
using COmpStoreClient.WebServiceAccess.Base;
using COmpStore.Models.ViewModels.OrderAdmin;
using COmpStore.Models.Enum;

namespace COmpStoreClient.Controllers
{
    [ValidateAdmin]
    public class AdminOrderController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;
        public AdminOrderController(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }

        public async Task<IActionResult> Index()
            => View(await _webApiCalls.GetOrderAdminIndex());

        public async Task<IActionResult> Details(int id) => View(await _webApiCalls.GetOrderAdminDetails(id));

        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            var model = new OrderAdminChangeStatus { OrderId = orderId, Status = EnumOrderStatus.Completed };
            await _webApiCalls.ChangeStatusOrder(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DenyOrder(int orderId)
        {
            var model = new OrderAdminChangeStatus { OrderId = orderId, Status = EnumOrderStatus.Denied };
            await _webApiCalls.ChangeStatusOrder(model);
            return RedirectToAction("Index");
        }
    }
}