using COmpStore.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Models.ViewModels.OrderAdmin
{
    public class OrderAdminChangeStatus
    {
        public int OrderId { get; set; }
        public EnumOrderStatus Status { get; set; }
    }
}
