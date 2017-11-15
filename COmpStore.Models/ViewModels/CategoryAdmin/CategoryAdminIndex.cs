using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Models.ViewModels.CategoryAdmin
{
    public class CategoryAdminIndex
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int SumSubCategories { get; set; }
    }
}
