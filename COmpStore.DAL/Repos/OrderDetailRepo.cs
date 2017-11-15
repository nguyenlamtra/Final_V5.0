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

namespace COmpStore.DAL.Repos
{
    public class OrderDetailRepo : RepoBase<OrderDetail>, IOrderDetailRepo
    {
        public OrderDetailRepo(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        public OrderDetailRepo()
        {
        }

        public override IEnumerable<OrderDetail> GetAll()
            => Table.OrderBy(x => x.Id);

        public override IEnumerable<OrderDetail> GetRange(int skip, int take)
            => GetRange(Table.OrderBy(x => x.Id), skip, take);

        internal IEnumerable<OrderDetailWithProductInfo> GetRecords(IQueryable<OrderDetail> query)
            => query
                .Include(x => x.Product)
                .ThenInclude(p => p.SubCategory)
                .Select(x => new OrderDetailWithProductInfo
                {
                    OrderId = x.OrderId,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitCost = x.UnitCost,
                    LineItemTotal = x.LineItemTotal,
                    Description = x.Product.Description,
                    ProductName = x.Product.ProductName,
                    ProductImage = x.Product.ProductImage,
                    Number = x.Product.Number,
                    SubCategoryName = x.Product.SubCategory.SubCategoryName
                })
                .OrderBy(x => x.ProductName);

        public IEnumerable<OrderDetailWithProductInfo> GetCustomersOrdersWithDetails(int customerId)
            => GetRecords(Table.Where(x => x.Order.CustomerId == customerId));

        public IEnumerable<OrderDetailWithProductInfo> GetSingleOrderWithDetails(int orderId)
            => GetRecords(Table.Where(x => x.Order.Id == orderId));

    }
}
