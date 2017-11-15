using COmpStore.Models.Entities;
using COmpStoreClient.WebServiceAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStoreClient.Authentication
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IWebApiCalls _webApiCalls;

        public AuthHelper(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }
        public Customer GetCustomerInfo()
        {
            return _webApiCalls.GetCustomersAsync().Result.FirstOrDefault();
        }
    }
}
