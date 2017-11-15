using COmpStore.DAL.Repos.Base;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels.Base;
using COmpStore.Models.ViewModels.SubCategoryAdmin;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.DAL.Repos.Interfaces
{
    public interface ISubCategoryRepo : IRepo<SubCategory>
    {
        IEnumerable<SubCategoryAndCategoryBase> GetAllWithCategoryName();
        SubCategoryAndCategoryBase GetOneWithCategoryName(int id);
        IEnumerable<SubCategoryAndCategoryBase> GetSubCategoriesForCategory(int id);
        IEnumerable<SubCategoryAndCategoryBase> Search(string searchString);
        //=================================================================================================
        IEnumerable<SubCategoryAdminIndex> GetSubCategoryAdminIndex();
        SubCategoryAdminDetails GetSubCategoryAdminDetails(int id);
        IEnumerable<SubCategoryCombobox> GetSubCategoryCombobox();
        //=================================================================================================
    }
}
