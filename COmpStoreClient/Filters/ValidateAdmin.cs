using COmpStoreClient.Extension;
using COmpStoreClient.WebServiceAccess.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace COmpStoreClient.Filters
{
    public class ValidateAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var _webApiCalls = filterContext.HttpContext.RequestServices.GetService<IWebApiCalls>();
            var authSession = filterContext.HttpContext.Session.GetAuthSession();
            if (authSession == null || authSession.Role != "Admin")
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "Admin" },
                        { "action", "Error" }
                    });
            }
            else
            {
                _webApiCalls.SetToken(filterContext.HttpContext.Session.GetAuthSession().Token);
            }
        }
    }
}
