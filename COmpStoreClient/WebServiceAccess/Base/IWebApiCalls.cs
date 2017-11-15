using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels;
using COmpStore.Models.ViewModels.Base;
using COmpStore.Models.Entities.ViewModels.Base;
using COmpStore.Models.ViewModels.CategoryAdmin;
using COmpStore.Models.ViewModels.Customer;
using COmpStore.Models.ViewModels.ProductAdmin;
using COmpStore.Models.ViewModels.Paging;
using COmpStore.Models.ViewModels.SubCategoryAdmin;
using COmpStore.Models.ViewModels.PublisherAdmin;

namespace COmpStoreClient.WebServiceAccess.Base
{
    public interface IWebApiCalls
    {
        //CategoryController
        Task<IList<Category>> GetCategoriesAsync();
        Task<IList<Product>> GetAllProductWithSubCategoryNamAsync();
        Task<Category> GetCategoryAsync(int id);
        Task<IList<SubCategory>> GetSubCategoriesAsync();
        Task<SubCategory> GetSubCategoryAsync(int id);
        Task<IList<Publisher>> GetPublishersAsync();
        Task<Publisher> GetPublisherAsync(int id);
        Task<IList<ProductAndSubCategoryBase>> GetProductsForSubCategoryAsync(int subcategoryId);
        Task<IList<ProductAndPublisherBase>> GetProductsForPublisherAsync(int publisherId);
        //Customer Controller
        Task<IList<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerAsync(int id);
        //Orders Controller
        Task<IList<Order>> GetOrdersAsync(int customerId);
        Task<OrderWithDetailsAndProductInfo> GetOrderDetailsAsync(int customerId, int orderId);
        //Product Controller
        Task<ProductAndSubCategoryBase> GetOneProductAsync(int productId);
        Task<IList<ProductAndSubCategoryBase>> GetFeaturedProductsAsync();
        //SearchAsync Controller
        Task<IList<ProductAndSubCategoryBase>> SearchAsync(string searchTerm);
        //Shopping Cart Controller
        Task<IList<CartRecordWithProductInfo>> GetCartAsync(int customerId);
        Task<CartRecordWithProductInfo> GetCartRecordAsync(int customerId, int productId);
        Task<string> AddToCartAsync(int customerId, int productId, int quantity);
        Task<string> UpdateCartItemAsync(ShoppingCartRecord item);
        Task RemoveCartItemAsync(int customerId, int shoppingCartRecordId, byte[] timeStamp);
        Task<int> PurchaseCartAsync(Customer customer);

        //===============
        //CategoryController
        Task<IList<CategoryAdminIndex>> GetAdminCategoryIndex();
        Task<string> CreateCategory(CategoryAdminCreate model);
        Task<string> UpdateCategory(CategoryAdminUpdate model);
        Task<string> DeleteCategory(int id);
        Task<CategoryAdminDetails> DetailsCategory(int id);
        Task<CategoryAdminUpdate> GetSingleCategory(int id);

        //Publisher Controller

        Task<IList<PublisherAdminIndex>> GetAdminPublisherIndex();
        Task<string> CreatePublisher(PublisherAdminCreate model);
        Task<string> UpdatePublisher(PublisherAdminUpdate model);
        Task<string> DeletePublisher(int id);
        Task<PublisherAdminDetails> DetailsPublisher(int id);
        Task<PublisherAdminUpdate> GetSinglePublisher(int id);
        Task<IList<PublisherCombobox>> GetPublisherForCombobox();

        //SubCategory Controller
        Task<IList<SubCategoryAdminIndex>> GetAdminSubCategoryIndex();
        Task<string> CreateSubCategory(SubCategoryAdminCreate model);
        Task<string> UpdateSubCategory(SubCategoryAdminUpdate model);
        Task<string> DeleteSubCategory(int id);
        Task<SubCategoryAdminDetails> DetailsSubCategory(int id);
        Task<SubCategoryAdminUpdate> GetSingleSubCategory(int id);
        Task<IList<SubCategoryCombobox>> GetSubCategoryForCombobox();

        //Product Controller
        //Task<IList<ProductAdminIndex>> GetAdminProductIndex();
        Task<PageOutput<ProductAdminIndex>> GetAdminProductIndex(int pageNumber);
        Task<string> CreateProduct(ProductAdminCreate model);
        Task<string> UpdateProduct(ProductAdminUpdate model);
        Task<string> DeleteProduct(int id);
        Task<ProductAdminUpdate> GetSingleProduct(int id);

        //Admin Controller
        Task<SessionAuth> VerifyAccount(CustomerLogin model);
        Task<string> CheckPermission(string token);

        //Token
        void SetToken(string token);
        //===============
    }
}
