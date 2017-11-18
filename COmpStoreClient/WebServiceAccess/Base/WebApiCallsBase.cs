using COmpStoreClient.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace COmpStoreClient.WebServiceAccess.Base
{
    //public abstract class WebApiCallsBase
    //{
    //    protected readonly string ServiceAddress;
    //    protected readonly string CartBaseUri;
    //    protected readonly string CategoryBaseUri;
    //    protected readonly string SubCategoryBaseUri;
    //    protected readonly string CustomerBaseUri;
    //    protected readonly string PublisherBaseUri;
    //    protected readonly string ProductBaseUri;
    //    protected readonly string OrdersBaseUri;

    //    protected WebApiCallsBase(IWebServiceLocator settings)
    //    {
    //        ServiceAddress = settings.ServiceAddress;
    //        CartBaseUri = $"{ServiceAddress}api/ShoppingCart/";
    //        CategoryBaseUri = $"{ServiceAddress}api/category/";
    //        SubCategoryBaseUri = $"{ServiceAddress}api/subcategory/";
    //        PublisherBaseUri = $"{ServiceAddress}api/publisher/";
    //        CustomerBaseUri = $"{ServiceAddress}api/customer/";
    //        ProductBaseUri = $"{ServiceAddress}api/product/";
    //        OrdersBaseUri = $"{ServiceAddress}api/orders/";
    //    }

    //    internal async Task<string> GetJsonFromGetResponseAsync(string uri)
    //    {
    //        try
    //        {
    //            using (var client = new HttpClient())
    //            {
    //                var response = await client.GetAsync(uri);
    //                if (!response.IsSuccessStatusCode)
    //                {
    //                    throw new Exception($"The Call to {uri} failed.  Status code: {response.StatusCode}");
    //                }
    //                return await response.Content.ReadAsStringAsync();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //Do something intelligent here
    //            Console.WriteLine(ex);
    //            throw;
    //        }

    //    }
    //    internal async Task<T> GetItemAsync<T>(string uri)
    //        where T : class, new()
    //    {
    //        try
    //        {
    //            var json = await GetJsonFromGetResponseAsync(uri);
    //            return JsonConvert.DeserializeObject<T>(json);
    //        }
    //        catch (Exception ex)
    //        {
    //            //Do something intelligent here
    //            Console.WriteLine(ex);
    //            throw;
    //        }
    //    }
    //    internal async Task<IList<T>> GetItemListAsync<T>(string uri)
    //        where T : class, new()
    //    {
    //        try
    //        {
    //            return JsonConvert.DeserializeObject<IList<T>>(await GetJsonFromGetResponseAsync(uri));
    //        }
    //        catch (Exception ex)
    //        {
    //            //Do something intelligent here
    //            Console.WriteLine(ex);
    //            throw;
    //        }
    //    }

    //    protected static async Task<string> ExecuteRequestAndProcessResponse(
    //        string uri, Task<HttpResponseMessage> task)
    //    {
    //        try
    //        {
    //            var response = await task;
    //            if (!response.IsSuccessStatusCode)
    //            {
    //                throw new Exception($"The Call to {uri} failed.  Status code: {response.StatusCode}");
    //            }
    //            //return response.Headers.Location.AbsoluteUri;
    //            return await response.Content.ReadAsStringAsync();
    //        }
    //        catch (Exception ex)
    //        {
    //            //Do something intelligent here
    //            Console.WriteLine(ex);
    //            throw;
    //        }
    //    }

    //    protected StringContent CreateStringContent(string json)
    //    {
    //        return new StringContent(json, Encoding.UTF8, "application/json");
    //    }

    //    protected async Task<string> SubmitPostRequestAsync(string uri, string json)
    //    {
    //        using (var client = new HttpClient())
    //        {
    //            var task = client.PostAsync(uri, CreateStringContent(json));
    //            return await ExecuteRequestAndProcessResponse(uri, task);
    //        }
    //    }

    //    protected async Task<string> SubmitPutRequestAsync(string uri, string json)
    //    {
    //        using (var client = new HttpClient())
    //        {
    //            //var requestMessage = new HttpRequestMessage(HttpMethod.Put,uri);
    //            //requestMessage.Content = CreateStringContent(json);
    //            //var response = await client.SendAsync(requestMessage);
    //            Task<HttpResponseMessage> task = client.PutAsync(uri, CreateStringContent(json));
    //            return await ExecuteRequestAndProcessResponse(uri, task);
    //        }
    //    }
    //    protected async Task SubmitDeleteRequestAsync(string uri)
    //    {
    //        try
    //        {
    //            using (var client = new HttpClient())
    //            {
    //                Task<HttpResponseMessage> deleteAsync = client.DeleteAsync(uri);
    //                var response = await deleteAsync;
    //                if (!response.IsSuccessStatusCode)
    //                {
    //                    throw new Exception(response.StatusCode.ToString());
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //Do something intelligent here
    //            Console.WriteLine(ex);
    //            throw;
    //        }
    //    }
    //}

    public abstract class WebApiCallsBase
    {
        protected readonly string ServiceAddress;
        protected readonly string CartBaseUri;
        protected readonly string CategoryBaseUri;
        protected readonly string SubCategoryBaseUri;
        protected readonly string CustomerBaseUri;
        protected readonly string PublisherBaseUri;
        protected readonly string ProductBaseUri;
        protected readonly string OrdersBaseUri;
        protected string Token;

        protected WebApiCallsBase(IWebServiceLocator settings)
        {
            ServiceAddress = settings.ServiceAddress;
            CartBaseUri = $"{ServiceAddress}api/ShoppingCart/";
            CategoryBaseUri = $"{ServiceAddress}api/category/";
            SubCategoryBaseUri = $"{ServiceAddress}api/subcategory/";
            PublisherBaseUri = $"{ServiceAddress}api/publisher/";
            CustomerBaseUri = $"{ServiceAddress}api/customer/";
            ProductBaseUri = $"{ServiceAddress}api/product/";
            OrdersBaseUri = $"{ServiceAddress}api/orders/";
        }



        internal async Task<string> GetJsonFromGetResponseAsync(string uri)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    SetTokenHeader(client);

                    var response = await client.GetAsync(uri);

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new WebException();
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"The Call to {uri} failed.  Status code: {response.StatusCode}");
                    }
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                //Do something intelligent here
                Console.WriteLine(ex);
                throw;
            }

        }
        internal async Task<T> GetItemAsync<T>(string uri)
            where T : class, new()
        {
            try
            {
                var json = await GetJsonFromGetResponseAsync(uri);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                //Do something intelligent here
                Console.WriteLine(ex);
                throw;
            }
        }
        internal async Task<IList<T>> GetItemListAsync<T>(string uri)
            where T : class, new()
        {
            try
            {
                return JsonConvert.DeserializeObject<IList<T>>(await GetJsonFromGetResponseAsync(uri));
            }
            catch (Exception ex)
            {
                //Do something intelligent here
                Console.WriteLine(ex);
                throw;
            }
        }

        protected static async Task<string> ExecuteRequestAndProcessResponse(
            string uri, Task<HttpResponseMessage> task)
        {
            try
            {
                var response = await task;

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new WebException();
                }

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"The Call to {uri} failed.  Status code: {response.StatusCode}");
                }
                //return response.Headers.Location.AbsoluteUri;
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                //Do something intelligent here
                Console.WriteLine(ex);
                throw;
            }
        }

        protected StringContent CreateStringContent(string json)
        {
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        protected async Task<string> SubmitPostRequestAsync(string uri, string json)
        {
            using (var client = new HttpClient())
            {
                SetTokenHeader(client);
                var task = client.PostAsync(uri, CreateStringContent(json));
                return await ExecuteRequestAndProcessResponse(uri, task);
            }
        }

        protected async Task<string> SubmitPutRequestAsync(string uri, string json)
        {
            using (var client = new HttpClient())
            {
                SetTokenHeader(client);
                //var requestMessage = new HttpRequestMessage(HttpMethod.Put,uri);
                //requestMessage.Content = CreateStringContent(json);
                //var response = await client.SendAsync(requestMessage);
                Task<HttpResponseMessage> task = client.PutAsync(uri, CreateStringContent(json));
                return await ExecuteRequestAndProcessResponse(uri, task);
            }
        }

        protected async Task<string> SubmitDeleteRequestAsync(string uri)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    SetTokenHeader(client);
                    Task<HttpResponseMessage> deleteAsync = client.DeleteAsync(uri);
                    var response = await deleteAsync;
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(response.StatusCode.ToString());
                    }
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {
                //Do something intelligent here
                Console.WriteLine(ex);
                throw;
            }
        }

        internal HttpClient SetTokenHeader(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            return client;
        }
    }
}
