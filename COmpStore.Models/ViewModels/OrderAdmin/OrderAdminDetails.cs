using COmpStore.Models.Enum;
using COmpStore.Models.ViewModels.OrderDetailsAdmin;
using COmpStore.Models.ViewModels.ProductAdmin;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Models.ViewModels.OrderAdmin
{
    public class OrderAdminDetails
    {
        public int Id { get; set; }
        public decimal? OrderTotal { get; set; }
        public DateTime OrderDate { get; set; }
        public string FullName { get; set; }
        public EnumOrderStatus Status { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public IEnumerable<OrderDetailsRelate> OrderDetails { get; set; }
    }
}
