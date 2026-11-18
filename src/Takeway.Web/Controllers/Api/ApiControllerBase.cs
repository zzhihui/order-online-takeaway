using System;
using System.Diagnostics;
using System.Web.Http;
using Core.Common.Exception;
using log4net;
using Takeaway.Service.Exceptions;
using Takeway.Web.Infrastructure.Extension;

namespace Takeway.Web.Controllers.Api
{
    public interface IApiControllerBase
    {
        IHttpActionResult Execute<T>(string operation, int shopId, Func<T> action);
    }

    public class ApiControllerBase : ApiController, IApiControllerBase
    {
        protected readonly ILog Logger;

        public ApiControllerBase()
        {
            Logger = LogManager.GetLogger(this.GetType());
        }
        
        public IHttpActionResult Execute<T>(string operation, int shopId, Func<T> action)
        {
            var sessionId = Guid.NewGuid().ToString();
            try
            {
                Logger.DebugFormat("[Operation:{0}][SessionId:{1}] [ShopId:{2}] Perform Action", operation, sessionId, shopId);
                Trace.TraceInformation("[Operation:{0}][SessionId:{1}] [ShopId:{2}] Perform Action", operation, sessionId, shopId);
                return Ok(action());

            }
            catch (ServiceException ex)
            {
                return HandleException(operation, sessionId, shopId, ex);
            }
            catch (Exception ex)
            {
                return HandleException(operation, sessionId, shopId, ex);
            }


        }

        private IHttpActionResult HandleException(string operation, string sessionId, int shopId, Exception ex)
        {
            Logger.ErrorFormat("[Operation:{0}][SessionId:{1}][ShopId:{4}] Error:{2}--{3}", operation, sessionId, ex.Message, ex.StackTrace, shopId);
            Trace.TraceError(string.Format("[Operation:{0}][SessionId:{1}][ShopId:{4}] Error:{2}--{3}", operation, sessionId, ex.Message, ex.StackTrace, shopId));

            var message = string.Format("[Operation:{0}][SessionId:{1}][ShopId:{4}] Error:{2}--{3}", operation,
                sessionId, ex.Message, ex.StackTrace, shopId);
            var unHandleException = new CoreException(message);

            return ApiControllerExtension.NotFound(this, unHandleException.Message);
        }

        private IHttpActionResult HandleException(string operation, string sessionId, int shopId, CoreException ex)
        {
            Logger.ErrorFormat("[Operation:{0}][SessionId:{1}][ShopId:{4}] Error:{2}--{3}", operation, sessionId, ex.Message, ex.StackTrace, shopId);
            Trace.TraceError(string.Format("[Operation:{0}][SessionId:{1}][ShopId:{4}] Error:{2}--{3}", operation, sessionId, ex.Message, ex.StackTrace, shopId));
            return ApiControllerExtension.BadRequest(this, ex.Message);
        }


    }
}