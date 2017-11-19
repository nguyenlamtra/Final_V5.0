using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace COmpStore.Models.ViewModels.Customer
{
    public class CustomerModel
    {
        public CustomerLogin CustomerLogin { get; set; }
        public CustomerCreate CustomerCreate { get; set; }

    }
}
