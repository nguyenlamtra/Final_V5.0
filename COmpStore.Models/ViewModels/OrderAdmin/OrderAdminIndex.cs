using COmpStore.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Models.ViewModels.OrderAdmin
{
    public class OrderAdminIndex
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }
        public EnumOrderStatus Status { get; set; }
    }
}
