using COmpStore.Models.Entities;
using COmpStore.Models.Entities.ViewModels.Base;
using COmpStore.Models.ViewModels;
using COmpStore.Models.ViewModels.Base;
using COmpStore.Models.ViewModels.CategoryAdmin;
using COmpStore.Models.ViewModels.Customer;
using COmpStore.Models.ViewModels.Paging;
using COmpStore.Models.ViewModels.ProductAdmin;
using COmpStore.Models.ViewModels.PublisherAdmin;
using COmpStore.Models.ViewModels.SubCategoryAdmin;
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

        //================================================================================================

        public async Task<IList<CategoryAdminIndex>> GetAdminCategoryIndex()
        {
            //Get All Category: 

            var uri = $"{ServiceAddress}api/category/admin";
            return await GetItemListAsync<CategoryAdminIndex>(uri);
        }

        public async Task<string> CreateCategory(CategoryAdminCreate model)
        {
            // Create category http://localhost:40001/api/category/admin
            var uri = $"{ServiceAddress}api/category/admin";

            return await SubmitPostRequestAsync(uri, JsonConvert.SerializeObject(model));
        }

        public async Task<string> UpdateCategory(CategoryAdminUpdate model)
        {
            // Update category http://localhost:40001/api/category/admin
            var uri = $"{ServiceAddress}api/category/admin";
            return await SubmitPutRequestAsync(uri, JsonConvert.SerializeObject(model));
        }

        public async Task<string> DeleteCategory(int id)
        {
            // Delete category http://localhost:40001/api/category/admin/{id}
            var uri = $"{ServiceAddress}api/category/admin/{id}";
            return await SubmitDeleteRequestAsync(uri);
        }

        public async Task<CategoryAdminDetails> DetailsCategory(int id)
        {
            // details category http://localhost:40001/api/category/admin/{id}
            var uri = $"{ServiceAddress}api/category/admin/{id}";
            return await GetItemAsync<CategoryAdminDetails>(uri);
        }

        public async Task<CategoryAdminUpdate> GetSingleCategory(int id)
        {
            // get single category http://localhost:40001/api/category/admin/update/{id}
            var uri = $"{ServiceAddress}api/category/admin/{id}";
            return await GetItemAsync<CategoryAdminUpdate>(uri);
        }

        public async Task<IList<PublisherAdminIndex>> GetAdminPublisherIndex()
        {
            //Get All publisher: 

            var uri = $"{ServiceAddress}api/publisher/admin";
            return await GetItemListAsync<PublisherAdminIndex>(uri);
        }

        public async Task<string> CreatePublisher(PublisherAdminCreate model)
        {
            // Create publisher http://localhost:40001/api/publisher/admin
            var uri = $"{ServiceAddress}api/publisher/admin";

            return await SubmitPostRequestAsync(uri, JsonConvert.SerializeObject(model));
        }

        public async Task<string> UpdatePublisher(PublisherAdminUpdate model)
        {
            // Update publisher http://localhost:40001/api/publisher/admin
            var uri = $"{ServiceAddress}api/publisher/admin";
            return await SubmitPutRequestAsync(uri, JsonConvert.SerializeObject(model));
        }

        public async Task<string> DeletePublisher(int id)
        {
            // Delete publisher http://localhost:40001/api/publisher/admin/{id}
            var uri = $"{ServiceAddress}api/publisher/admin/{id}";
            return await SubmitDeleteRequestAsync(uri);
        }

        public async Task<PublisherAdminDetails> DetailsPublisher(int id)
        {
            // details publisher http://localhost:40001/api/publisher/admin/{id}
            var uri = $"{ServiceAddress}api/publisher/admin/{id}";
            return await GetItemAsync<PublisherAdminDetails>(uri);
        }

        public async Task<PublisherAdminUpdate> GetSinglePublisher(int id)
        {
            // get single publisher http://localhost:40001/api/publisher/admin/update/{id}
            var uri = $"{ServiceAddress}api/publisher/admin/{id}";
            return await GetItemAsync<PublisherAdminUpdate>(uri);
        }

        public async Task<IList<SubCategoryAdminIndex>> GetAdminSubCategoryIndex()
        {
            //Get All subcategory: 

            var uri = $"{ServiceAddress}api/subcategory/admin";
            return await GetItemListAsync<SubCategoryAdminIndex>(uri);
        }

        public async Task<string> CreateSubCategory(SubCategoryAdminCreate model)
        {
            // Create subcategory http://localhost:40001/api/subcategory/admin
            var uri = $"{ServiceAddress}api/subcategory/admin";

            return await SubmitPostRequestAsync(uri, JsonConvert.SerializeObject(model));
        }

        public async Task<string> UpdateSubCategory(SubCategoryAdminUpdate model)
        {
            // Update subcategory http://localhost:40001/api/subcategory/admin
            var uri = $"{ServiceAddress}api/subcategory/admin";
            return await SubmitPutRequestAsync(uri, JsonConvert.SerializeObject(model));
        }

        public async Task<string> DeleteSubCategory(int id)
        {
            // Delete subcategory http://localhost:40001/api/subcategory/admin/{id}
            var uri = $"{ServiceAddress}api/subcategory/admin/{id}";
            return await SubmitDeleteRequestAsync(uri);
        }

        public async Task<SubCategoryAdminDetails> DetailsSubCategory(int id)
        {
            // details subcategory http://localhost:40001/api/subcategory/admin/{id}
            var uri = $"{ServiceAddress}api/subcategory/admin/{id}";
            return await GetItemAsync<SubCategoryAdminDetails>(uri);
        }

        public async Task<SubCategoryAdminUpdate> GetSingleSubCategory(int id)
        {
            // get single subcategory http://localhost:40001/api/subcategory/admin/update/{id}
            var uri = $"{ServiceAddress}api/subcategory/admin/{id}";
            return await GetItemAsync<SubCategoryAdminUpdate>(uri);
        }

        //public async Task<IList<ProductAdminIndex>> GetAdminProductIndex()
        //{
        //    //Get All product: 

        //    var uri = $"{ServiceAddress}api/product/admin";
        //    return await GetItemListAsync<ProductAdminIndex>(uri);
        //}

        public async Task<PageOutput<ProductAdminIndex>> GetAdminProductIndex(int pageNumber = 1)
        {
            //Get All product: 

            var uri = $"{ServiceAddress}api/product/admin?pageNumber={pageNumber}";
            return await GetItemAsync<PageOutput<ProductAdminIndex>>(uri);
        }

        public async Task<string> CreateProduct(ProductAdminCreate model)
        {
            // Create product http://localhost:40001/api/product/admin
            var uri = $"{ServiceAddress}api/product/admin";

            return await SubmitPostRequestAsync(uri, JsonConvert.SerializeObject(model));
        }

        public async Task<string> UpdateProduct(ProductAdminUpdate model)
        {
            // Update product http://localhost:40001/api/product/admin
            var uri = $"{ServiceAddress}api/product/admin";
            return await SubmitPutRequestAsync(uri, JsonConvert.SerializeObject(model));
        }

        public async Task<string> DeleteProduct(int id)
        {
            // Delete product http://localhost:40001/api/product/admin/{id}
            var uri = $"{ServiceAddress}api/product/admin/{id}";
            return await SubmitDeleteRequestAsync(uri);
        }

        public async Task<ProductAdminUpdate> GetSingleProduct(int id)
        {
            // get single product http://localhost:40001/api/product/admin/update/{id}
            var uri = $"{ServiceAddress}api/product/admin/update/{id}";
            return await GetItemAsync<ProductAdminUpdate>(uri);
        }

        public async Task<IList<PublisherCombobox>> GetPublisherForCombobox()
        {
            // get list publisher http://localhost:40001/api/publisher/admin/combobox
            var uri = $"{ServiceAddress}api/publisher/admin/combobox";
            return await GetItemListAsync<PublisherCombobox>(uri);
        }

        public async Task<IList<SubCategoryCombobox>> GetSubCategoryForCombobox()
        {
            // get list subcategory http://localhost:40001/api/subcategory/admin/combobox
            var uri = $"{ServiceAddress}api/subcategory/admin/combobox";
            return await GetItemListAsync<SubCategoryCombobox>(uri);
        }

        public async Task<SessionAuth> VerifyAccount(CustomerLogin model)
        {
            var uri = $"{ServiceAddress}api/auth/login";
            var json = await SubmitPostRequestAsync(uri, JsonConvert.SerializeObject(model));
            return JsonConvert.DeserializeObject<SessionAuth>(json);
        }

        public async Task<string> CheckPermission(string token)
        {
            var uri = $"{ServiceAddress}api/auth/checkpermission";
            return await SubmitPostRequestAsync(uri, null);
        }

        public void SetToken(string token)
        {
            this.Token = token;
        }

        //===========================================================================================================
    }
}
