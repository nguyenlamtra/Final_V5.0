using COmpStore.DAL.Repos.Base;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.DAL.Repos.Interfaces
{
    public interface IShoppingCartRepo : IRepo<ShoppingCartRecord>
    {
        CartRecordWithProductInfo GetShoppingCartRecord(int customerId, int productId);
        IEnumerable<CartRecordWithProductInfo> GetShoppingCartRecords(int customerId);
        int Purchase(int customerId);
        ShoppingCartRecord Find(int customerId, int productId);
        int Update(ShoppingCartRecord entity, int? quantityInStock, bool persist = true);
        int Add(ShoppingCartRecord entity, int? quantityInStock, bool persist = true);
    }
}
