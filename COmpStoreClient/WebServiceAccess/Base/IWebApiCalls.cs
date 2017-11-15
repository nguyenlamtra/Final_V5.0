using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COmpStore.Models.Entities;
using COmpStore.Models.ViewModels;
using COmpStore.Models.ViewModels.Base;
using COmpStore.Models.Entities.ViewModels.Base;

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
    }
}
