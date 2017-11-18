using COmpStore.Models.ViewModels.SubCategoryAdmin;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Models.ViewModels.CategoryAdmin
{
    public class CategoryAdminDetails
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<SubCategoryAdminIndex> SubCategories { get; set; }
    }
}
