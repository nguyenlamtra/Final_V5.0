using COmpStore.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Models.ViewModels.Customer
{
    public class CustomerAdminUpdate
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public EnumRole Role { get; set; }
    }
}
