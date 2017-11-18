using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Models.ViewModels.Customer
{
    public class CustomerAdminIndex
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public int SumOrders { get; set; }
    }
}
