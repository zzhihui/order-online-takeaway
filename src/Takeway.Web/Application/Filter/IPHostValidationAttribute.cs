using System;
using System.Diagnostics;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Takeway.Web.Infrastructure.Filter
{
    public class IpHostValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            var context = actionContext.Request.Properties["MS_HttpContext"] as System.Web.HttpContextBase;
            string userIp = context.Request.UserHostAddress;

            //var businessEngin = ApplicationScope.ResolveStatic<IBusinessEngineFactory>();


            try
            {
                //AuthorizedIPRepository.GetAuthorizedIPs().First(x => x == userIP);
                //Use a table to block IP address


            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("IpHostValidationAttribute:[Exception] Unauthorized IP Address {0}-{1}", ex.Message, ex.StackTrace));
                actionContext.Response =
                   new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                   {
                       Content = new StringContent("Unauthorized IP Address")
                   };
                return;
            }
        }
    }
}
