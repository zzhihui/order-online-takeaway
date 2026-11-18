using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Takeway.Web.Application.Filter
{
    public class AreaAuthorizationAttribute : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var area = filterContext.RouteData.DataTokens["area"];


            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else
            {
                try
                {
                    var userData =
                        ((System.Web.Security.FormsIdentity)filterContext.HttpContext.User.Identity).Ticket.UserData;

                    if (area == null && !string.IsNullOrEmpty(userData))
                        filterContext.Result = new HttpUnauthorizedResult();
                    else if (userData != area.ToString())
                        filterContext.Result = new HttpUnauthorizedResult();
                }
                catch
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }

            }



        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var area = filterContext.RouteData.DataTokens["area"];

            if (filterContext.Result == null)
            {

                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
             new { area = area.ToString(), controller = "Account", action = "Login", returnUrl = filterContext.HttpContext.Request.RawUrl }));
            }
            if (filterContext.Result is HttpUnauthorizedResult && area != null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                    new
                    {
                        area = area.ToString(),
                        controller = "Account",
                        action = "Login",
                        returnUrl = filterContext.HttpContext.Request.RawUrl
                    }));

            }
            else if(filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                       new
                       {
                           controller = "Account",
                           action = "Login",
                           returnUrl = filterContext.HttpContext.Request.RawUrl
                       }));

            }

        }
    }
}