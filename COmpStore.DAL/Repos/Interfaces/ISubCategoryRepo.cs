using COmpStore.DAL.Repos.Base;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.DAL.Repos.Interfaces
{
    public interface ISubCategoryRepo
    {
        IEnumerable<SubCategoryAndCategoryBase> GetAllWithCategoryName();
        SubCategoryAndCategoryBase GetOneWithCategoryName(int id);
        IEnumerable<SubCategoryAndCategoryBase> GetSubCategoriesForCategory(int id);
        IEnumerable<SubCategoryAndCategoryBase> Search(string searchString);
    }
}
