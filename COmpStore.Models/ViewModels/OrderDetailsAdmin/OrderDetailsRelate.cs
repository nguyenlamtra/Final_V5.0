using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Models.ViewModels.OrderDetailsAdmin
{
    public class OrderDetailsRelate
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal LineItemTotal { get; set; }
    }
}
