using COmpStore.DAL.Repos.Base;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.DAL.Repos.Interfaces
{
    public interface IOrderRepo : IRepo<Order>
    {
        IEnumerable<Order> GetOrderHistory(int customerId);
        OrderWithDetailsAndProductInfo GetOneWithDetails(int customerId, int orderId);
    }
}
