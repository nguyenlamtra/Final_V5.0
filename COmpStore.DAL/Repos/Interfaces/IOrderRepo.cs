using COmpStore.DAL.Repos.Base;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels;
using COmpStore.Models.ViewModels.Cart;
using COmpStore.Models.ViewModels.OrderAdmin;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.DAL.Repos.Interfaces
{
    public interface IOrderRepo : IRepo<Order>
    {
        //================================================================
        int AddCart(OrderModel model);

        //int DeleteOrder(int id, bool persist = true);
        IEnumerable<OrderAdminIndex> GetOrderAdminIndex();
        OrderAdminDetails GetOrderAdminDetails(int id);
        int ChangeStatusOrder(OrderAdminChangeStatus model);
        //================================================================
    }
}
