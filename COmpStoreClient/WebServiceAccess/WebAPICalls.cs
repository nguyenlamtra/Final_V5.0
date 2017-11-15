using COmpStore.Models.Entities;
using COmpStore.Models.Entities.ViewModels.Base;
using COmpStore.Models.ViewModels;
using COmpStore.Models.ViewModels.Base;
using COmpStoreClient.Configuration;
using COmpStoreClient.WebServiceAccess.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStoreClient.WebServiceAccess
{
    public class WebApiCalls : WebApiCallsBase, IWebApiCalls
    {

        public WebApiCalls(IWebServiceLocator settings) : base(settings)
        {
        }

        //Cart 
        public async Task<IList<CartRecordWithProductInfo>> GetCartAsync(int customerId)
        {
            // http://localhost:40001/api/ShoppingCart/0
            return await GetItemListAsync<CartRecordWithProductInfo>($"{CartBaseUri}{customerId}");
        }
        public async Task<CartRecordWithProductInfo> GetCartRecordAsync(int customerId, int productId)
        {
            // http://localhost:40001/api/ShoppingCart/0/0
            return await GetItemAsync<CartRecordWithProductInfo>($"{CartBaseUri}{customerId}/{productId}");
        }
        public async Task<string> AddToCartAsync(int customerId, int productId, int quantity)
        {
            //http://localhost:40001/api/shoppingcart/{customerId} HTTPPost
            //Note: ProductId and Quantity in the body
            //http://localhost:40001/api/shoppingcart/0 {"ProductId":22,"Quantity":2}
            //		Content-Type:application/json
            string uri = $"{CartBaseUri}{customerId}";
            string json = $"{{\"ProductId\":{productId},\"Quantity\":{quantity}}}";
            return await SubmitPostRequestAsync(uri, json);
        }
        public async Task<int> PurchaseCartAsync(Customer customer)
        {
            //Purchase: http://localhost:40001/api/shoppingcart/{customerId}/buy HTTPPost
            //Note: Customer in the body
            //{ "Id":1,"FullName":"Super Spy","EmailAddress":"spy@secrets.com"}
            //  http://localhost:40001/api/shoppingcart/0/buy 
            var json = JsonConvert.SerializeObject(customer);
            var uri = $"{CartBaseUri}{customer.Id}/buy";
            return int.Parse(await SubmitPostRequestAsync(uri, json));
        }

        public async Task<string> UpdateCartItemAsync(ShoppingCartRecord item)
        {
            // Change Cart Item(Quantity): http://localhost:40001/api/shoppingcart/{customerId}/{id} HTTPPut
            //   Note: Id, CustomerId, ProductId, TimeStamp, DateCreated, and Quantity in the body
            //{"Id":0,"CustomerId":0,"ProductId":32,"Quantity":2, "TimeStamp":"AAAAAAAA86s=","DateCreated":"1/20/2016"}
            //http://localhost:40001/api/shoppingcart/0/AAAAAAAA86s=
            string uri = $"{CartBaseUri}{item.CustomerId}/{item.Id}";
            var json = JsonConvert.SerializeObject(item);
            return await SubmitPutRequestAsync(uri, json);
        }
        public async Task RemoveCartItemAsync(int customerId, int shoppingCartRecordId, byte[] timeStamp)
        {
            //Remove Cart Item: http://localhost:40001/api/shoppingcart/{customerId}/{id}/{TimeStamp} HTTPDelete
            //    http://localhost:40001/api/shoppingcart/0/0/AAAAAAAA1Uc=
            var timeStampString = JsonConvert.SerializeObject(timeStamp);
            var uri = $"{CartBaseUri}{customerId}/{shoppingCartRecordId}/{timeStampString}";
            await SubmitDeleteRequestAsync(uri);
        }

        //Categories
        public async Task<IList<Category>> GetCategoriesAsync()
        {
            //http://localhost:40001/api/category
            return await GetItemListAsync<Category>(CategoryBaseUri);
        }
        public async Task<IList<Product>> GetAllProductWithSubCategoryNamAsync()
        {
            //http://localhost:40001/api/product
            return await GetItemListAsync<Product>(ProductBaseUri);
        }
        public async Task<Category> GetCategoryAsync(int id)
        {
            //http://localhost:40001/api/category/{id}
            return await GetItemAsync<Category>($"{CategoryBaseUri}{id}");
        }

        public async Task<IList<SubCategory>> GetSubCategoriesAsync()
        {
            //http://localhost:40001/api/subcategory
            return await GetItemListAsync<SubCategory>(SubCategoryBaseUri);
        }
        public async Task<SubCategory> GetSubCategoryAsync(int id)
        {
            //http://localhost:40001/api/subcategory/{id}
            return await GetItemAsync<SubCategory>($"{SubCategoryBaseUri}{id}");
        }

        public async Task<IList<Publisher>> GetPublishersAsync()
        {
            //http://localhost:40001/api/publisher
            return await GetItemListAsync<Publisher>(PublisherBaseUri);
        }
        public async Task<Publisher> GetPublisherAsync(int id)
        {
            //http://localhost:40001/api/Publisher/{id}
            return await GetItemAsync<Publisher>($"{PublisherBaseUri}{id}");
        }


        public async Task<IList<ProductAndSubCategoryBase>> GetProductsForSubCategoryAsync(int subcategoryId)
        {
            // http://localhost:40001/api/subcategory/{subcategoryId}/products
            var uri = $"{SubCategoryBaseUri}{subcategoryId}/products";
            return await GetItemListAsync<ProductAndSubCategoryBase>(uri);
        }

        public async Task<IList<ProductAndPublisherBase>> GetProductsForPublisherAsync(int publisherId)
        {
            // http://localhost:40001/api/publisher/{publisherId}/products
            var uri = $"{PublisherBaseUri}{publisherId}/products";
            return await GetItemListAsync<ProductAndPublisherBase>(uri);
        }
        
        //Customers
        public async Task<IList<Customer>> GetCustomersAsync()
        {
            //Get All Customers: http://localhost:40001/api/customer
            return await GetItemListAsync<Customer>($"{CustomerBaseUri}");
        }
        public async Task<Customer> GetCustomerAsync(int id)
        {
            //Get One customer: http://localhost:40001/api/customer/{id}
            //http://localhost:40001/api/customer/1
            return await GetItemAsync<Customer>($"{CustomerBaseUri}{id}");
        }
        //Products
        public async Task<IList<ProductAndSubCategoryBase>> GetFeaturedProductsAsync()
        {
            // http://localhost:40001/api/product/featured
            return await GetItemListAsync<ProductAndSubCategoryBase>($"{ProductBaseUri}featured");
        }
        public async Task<ProductAndSubCategoryBase> GetOneProductAsync(int productId)
        {
            // http://localhost:40001/api/product/{id}
            return await GetItemAsync<ProductAndSubCategoryBase>($"{ProductBaseUri}{productId}");
        }
        //Orders
        public async Task<IList<Order>> GetOrdersAsync(int customerId)
        {
            //Get Order History: http://localhost:40001/api/orders/{customerId}
            return await GetItemListAsync<Order>($"{OrdersBaseUri}{customerId}");
        }
        public async Task<OrderWithDetailsAndProductInfo> GetOrderDetailsAsync(
  int customerId, int orderId)
        {
            //Get Order Details: http://localhost:40001/api/orders/{customerId}/{orderId}
            var url = $"{OrdersBaseUri}{customerId}/{orderId}";
            return await GetItemAsync<OrderWithDetailsAndProductInfo>(url);
        }
        //Search
        public async Task<IList<ProductAndSubCategoryBase>> SearchAsync(string searchTerm)
        {
            var uri = $"{ServiceAddress}api/search/{searchTerm}";
            return await GetItemListAsync<ProductAndSubCategoryBase>(uri);
        }

    }
}
