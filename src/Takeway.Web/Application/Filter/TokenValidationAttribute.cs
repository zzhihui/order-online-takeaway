using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Core.Common.Utility;

namespace Takeway.Web.Infrastructure.Filter
{
    public class TokenValidationAttribute : ActionFilterAttribute
    {

        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger("TokenValidationRequest");

        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            string token;

            try
            {
                token = actionContext.Request.Headers.GetValues("IndaSoftAuthToken").First();
                Logger.Debug(actionContext.Request);
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("TokenValidationAttribute:[Exception]Missing Authorization Token. {0}-{1}", ex.Message, ex.StackTrace));
                Logger.ErrorFormat("TokenValidationAttribute:[Exception]Missing Authorization Token. {0}-{1}", ex.Message, ex.StackTrace);
                actionContext.Response =
                  new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                  {
                      Content = new StringContent("Missing Authorization Token")
                  };
                return;
            }

            try
            {
                var decryptedToken = TokenEncryptor.Decrypt(token);
                var decryptedTokensplit = decryptedToken.Split(',');

                if (decryptedTokensplit.Length < 2)
                    throw new Exception("Unauthorized User");

                if (decryptedTokensplit[0].Trim() != ConfigurationManager.AppSettings["ShopAuthKey"])
                    throw new Exception("Unauthorized User");

                //Do not evaluate shop id
                //if (decryptedTokensplit[1].Trim() != ConfigurationManager.AppSettings["ShopId"])
                //    throw new Exception("Unauthorized User");

                base.OnActionExecuting(actionContext);
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("TokenValidationAttribute:[Exception]Unauthorized User. {0}-{1}", ex.Message, ex.StackTrace));
                Logger.ErrorFormat("TokenValidationAttribute:[Exception]Unauthorized User. {0}-{1}", ex.Message, ex.StackTrace);
                actionContext.Response =
                  new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                  {
                      Content = new StringContent("Unauthorized User")
                  };
                return;
            }
        }
    }
}