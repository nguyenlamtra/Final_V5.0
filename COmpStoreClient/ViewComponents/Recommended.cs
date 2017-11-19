using COmpStoreClient.WebServiceAccess.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStoreClient.ViewComponents
{
    public class Recommended : ViewComponent
    {
        private readonly IWebApiCalls _webApiCalls;

        public Recommended(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var publisher = await _webApiCalls.GetProductsForPublisherAsync(1);
            if (publisher == null)
            {
                return new ContentViewComponentResult("There was an error getting the publisher");
            }
            return View("RecommendView", publisher);
        }
    }
}
