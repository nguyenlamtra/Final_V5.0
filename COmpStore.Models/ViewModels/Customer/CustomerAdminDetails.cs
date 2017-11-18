using COmpStore.Models.ViewModels.OrderAdmin;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Models.ViewModels.Customer
{
    public class CustomerAdminDetails
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }
        public IEnumerable<OrderRelate> Orders { get; set; }
    }
}
