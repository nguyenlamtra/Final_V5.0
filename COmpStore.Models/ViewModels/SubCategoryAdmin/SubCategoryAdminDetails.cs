using COmpStore.Models.ViewModels.ProductAdmin;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Models.ViewModels.SubCategoryAdmin
{
    public class SubCategoryAdminDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<ProductRelate> Products { get; set; }
    }
}
