using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Models.ViewModels.Cart
{
    public class CartModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int UnitsInStock { get; set; }
        public decimal UnitCost { get; set; }
        public string ProductImage { get; set; }
    }
}
