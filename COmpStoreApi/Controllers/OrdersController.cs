using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.ViewModels.Cart;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels.OrderAdmin;

namespace COmpStoreApi.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private IOrderRepo Repo { get; set; }
        private ICustomerRepo CustomerRepo { get; set; }
        private IProductRepo ProductRepo { get; set; }
        public OrdersController(IOrderRepo repo, ICustomerRepo customerRepo, IProductRepo productRepo)
        {
            Repo = repo;
            CustomerRepo = customerRepo;
            ProductRepo = productRepo;
        }

        //======================
        [HttpPost]
        public int SaveOrder([FromBody]OrderModel model)
        => Repo.AddCart(model);

        [HttpGet("admin")]
        public IEnumerable<OrderAdminIndex> GetOrderAdminIndex() => Repo.GetOrderAdminIndex();

        [HttpGet("admin/{id}")]
        public OrderAdminDetails GetOrderAdminDetails(int id) => Repo.GetOrderAdminDetails(id);

        [HttpPut("admin/change-status")]
        public int ChangeStatusOrder([FromBody]OrderAdminChangeStatus model)
        {
            var order = Repo.Find(model.OrderId);
            if (order != null)
            {
                order.Status = model.Status.ToString();
                Repo.Update(order);
                return 1;
            }
            else return 0;
        }
        //=====================
    }
}