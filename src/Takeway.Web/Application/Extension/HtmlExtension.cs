using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;


namespace Takeway.Web.Application.Extension
{
    public static class HtmlExtension
    {

        public static string Content(this HtmlHelper html, string imagePath)
        {
            var area = System.Web.HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"];

            if (area == null)
            {
                area = "";
            }
            else
            {
                area = "Areas/" + area + "/";
            }
            return VirtualPathUtility.ToAbsolute("~/" + area + "Content/" + imagePath);
        }


        public static string Shared(this HtmlHelper html, string imagePath)
        {
            var area = System.Web.HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"];

            if (area == null)
            {
                area = "";
            }
            else
            {
                area = "Areas/" + area + "/";
            }
            return VirtualPathUtility.ToAbsolute("~/" + area + "Views/Shared/" + imagePath);
        }


        public static int SetAuthCookie<T>(this HttpResponseBase responseBase, string name, bool rememberMe, T userData)
        {
            var cookie = FormsAuthentication.GetAuthCookie(name, rememberMe);
            var ticket = FormsAuthentication.Decrypt(cookie.Value);

            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration,
                ticket.IsPersistent, JsonConvert.SerializeObject(userData), ticket.CookiePath);
            var encTicket = FormsAuthentication.Encrypt(newTicket);

            
            cookie.Value = encTicket;

            responseBase.Cookies.Add(cookie);

            return encTicket.Length;
        }

    }

   
}