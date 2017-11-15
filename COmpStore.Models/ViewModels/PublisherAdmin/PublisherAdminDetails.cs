using COmpStore.Models.ViewModels.ProductAdmin;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Models.ViewModels.PublisherAdmin
{
    public class PublisherAdminDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProductRelate> Products { get; set; }
    }
}
