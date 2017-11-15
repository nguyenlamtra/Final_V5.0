using COmpStoreClient.WebServiceAccess.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace COmpStoreClient.ViewComponents
{
    //https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components
    //The runtime searches for the view in the following paths:
    //    Views/<controller_name>/Components/<view_component_name>/<view_name>
    //    Views/Shared/Components/<view_component_name>/<view_name>
    public class Menu : ViewComponent
    {
        private readonly IWebApiCalls _webApiCalls;

        public Menu(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cats = await _webApiCalls.GetCategoriesAsync();
            ViewData["subcates"] = await _webApiCalls.GetSubCategoriesAsync();
            ViewData["publisher"] = await _webApiCalls.GetPublishersAsync();
            ViewData["pros"] = await _webApiCalls.GetAllProductWithSubCategoryNamAsync();
            if (cats == null)
            {
                return new ContentViewComponentResult("There was an error getting the subcategories");
            }
            return View("MenuView", cats);
        }
    }
}
