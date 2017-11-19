using COmpStore.DAL.EF;
using COmpStore.DAL.Repos.Base;
using COmpStore.DAL.Repos.Interfaces;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COmpStore.Models.ViewModels.Cart;
using COmpStore.Models.ViewModels.OrderAdmin;
using COmpStore.Models.ViewModels.ProductAdmin;
using COmpStore.Models.ViewModels.OrderDetailsAdmin;
using COmpStore.Models.Enum;
using COmpStore.Models.ViewModels.Paging;

namespace COmpStore.DAL.Repos
{
    public class OrderRepo : RepoBase<Order>, IOrderRepo
    {
        private readonly IOrderDetailRepo _orderDetailRepo;

        public OrderRepo(DbContextOptions<StoreContext> options, IOrderDetailRepo orderDetailRepo) : base(options)
        {
            _orderDetailRepo = _orderDetailRepo;
        }
        public OrderRepo(IOrderDetailRepo orderDetailRepo)
        {
            _orderDetailRepo = orderDetailRepo;
        }

        //==================================================================================
        public int AddCart(OrderModel model)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            var customerId = Db.Customers.Where(x => x.IsDeleted == false).SingleOrDefault(x => x.EmailAddress == model.EmailAddress)?.Id;
            if (customerId != 0)
            {
                foreach (var selectedProduct in model.SelectedProducts)
                {
                    var product = Db.Products.Where(x => x.IsDeleted == false).SingleOrDefault(x => x.Id == selectedProduct.ProductId);
                    if (product != null && (product.UnitsInStock -= selectedProduct.Quantity) >= 0)
                    {
                        orderDetails.Add(new OrderDetail
                        {
                            ProductId = selectedProduct.ProductId,
                            Quantity = selectedProduct.Quantity,
                            UnitCost = product.UnitCost,
                            LineItemTotal = selectedProduct.Quantity * product.UnitCost
                        });
                    }
                    else return 0;
                }
                Db.Orders.Add(new Order
                {
                    CustomerId = customerId.Value,
                    OrderDate = DateTime.Now,
                    OrderDetails = orderDetails,
                    OrderTotal = orderDetails.Sum(x => x.Quantity * x.UnitCost),
                    Status = EnumOrderStatus.NotReadYet.ToString(),
                    Address = model.Address,
                    Phone = model.Phone
                });
                Db.SaveChanges();
                return 1;
            }
            else return 0;
        }

        //public int DeleteOrder(int id, bool persist = true)
        //{
        //    var order = Db.Orders.Include(x => x.OrderDetails).SingleOrDefault(x => x.Id == id);
        //    if (order != null)
        //    {
        //        order.IsDeleted = true;
        //        order.OrderDetails.ForEach(x => x.IsDeleted = true);
        //        Db.SaveChanges();
        //        return 1;
        //    }
        //    else
        //        return 0;
        //}

        public PageOutput<OrderAdminIndex> GetOrderAdminIndex(int pageNumber, int pageSize)
             => new PageOutput<OrderAdminIndex>
             {
                 TotalPage = (Table.Count() % pageSize == 0) ? (Table.Count() / pageSize) : (Table.Count() / pageSize + 1),
                 PageNumber = pageNumber,
                 Items = Table.Where(o => o.IsDeleted == false).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(o => new OrderAdminIndex
                 {
                     FullName = o.Customer.FullName,
                     Id = o.Id,
                     OrderDate = o.OrderDate,
                     OrderTotal = o.OrderTotal ?? 0,
                     Status = (EnumOrderStatus)Enum.Parse(typeof(EnumOrderStatus), o.Status)
                 }).ToList()
             };

        internal IEnumerable<OrderDetailsRelate> GetOrderDetails(IEnumerable<OrderDetail> orderDetails)
            => orderDetails.Select(od => new OrderDetailsRelate
            {
                LineItemTotal = od.LineItemTotal ?? 0,
                Quantity = od.Quantity,
                UnitCost = od.UnitCost,
                ProductName = od.Product.ProductName
            });

        public OrderAdminDetails GetOrderAdminDetails(int id)
        {
            var order = Table.SingleOrDefault(o => o.Id == id);
            if (order.Status == EnumOrderStatus.NotReadYet.ToString())
            {
                Db.Orders.Attach(order);
                order.Status = EnumOrderStatus.Processing.ToString();
                Db.SaveChanges();
            }
            return Table.Where(o => o.Id == id).Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product).Select(o => new OrderAdminDetails
                {
                    FullName = o.Customer.FullName,
                    OrderDate = o.OrderDate,
                    OrderTotal = o.OrderTotal,
                    OrderDetails = GetOrderDetails(o.OrderDetails),
                    Phone = o.Phone,
                    Address = o.Address,
                    Id = o.Id,
                    Status = (EnumOrderStatus)Enum.Parse(typeof(EnumOrderStatus), o.Status)
                }).SingleOrDefault();
        }


        public int ChangeStatusOrder(OrderAdminChangeStatus model)
        {
            try
            {
                var order = Find(model.OrderId);
                if (order != null)
                {
                    order.Status = model.Status.ToString();
                }
                Update(order);
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }

        }

        //=============================================================================
    }
}
