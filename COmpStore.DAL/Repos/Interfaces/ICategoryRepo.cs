using COmpStore.DAL.Repos.Base;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels.CategoryAdmin;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.DAL.Repos.Interfaces
{
    public interface ICategoryRepo : IRepo<Category>
    {
        IEnumerable<Category> GetAllWithCategories();
        Category GetOneWithCategory(int? id);
        //=======
        IEnumerable<CategoryAdminIndex> GetAdminCategoryIndex();
        CategoryAdminDetails GetAdminCategoryDetails(int id);
        int DeleteCategory(int id, bool persist = true);
        //=======
    }
}
