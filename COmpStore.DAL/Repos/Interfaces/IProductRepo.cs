using COmpStore.DAL.Repos.Base;
using COmpStore.Models.Entities;
using COmpStore.Models.Entities.ViewModels.Base;
using COmpStore.Models.ViewModels.Base;
using COmpStore.Models.ViewModels.Paging;
using COmpStore.Models.ViewModels.ProductAdmin;
using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.DAL.Repos.Interfaces
{
    public interface IProductRepo : IRepo<Product>
    {
        IEnumerable<ProductAndSubCategoryBase> Search(string searchString);
        IEnumerable<ProductAndSubCategoryBase> GetAllWithSubCategoryName();
        IEnumerable<ProductAndPublisherBase> GetProductsForPublisher(int id);
        IEnumerable<ProductAndSubCategoryBase> GetProductsForSubCategory(int id);
        IEnumerable<ProductAndSubCategoryBase> GetFeaturedWithSubCategoryName();
        ProductAndSubCategoryBase GetOneWithSubCategoryName(int id);
        //======================================================================================================
        //IEnumerable<ProductAdminIndex> GetProductAdminIndex();
        string GetImageProduct(int id);
        int UpdateExceptImage(Product model, bool persist = true);
        PageOutput<ProductAdminIndex> GetProductAdminIndex(int pageNumber = 1, int pageSize = 2);

        //======================================================================================================
    }
}
