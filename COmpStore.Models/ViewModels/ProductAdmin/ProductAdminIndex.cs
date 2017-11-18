using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Models.ViewModels.ProductAdmin
{
    public class ProductAdminIndex
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductImage { get; set; }
        public int UnitsInStock { get; set; }
        public bool IsFeature { get; set; }
    }
}
