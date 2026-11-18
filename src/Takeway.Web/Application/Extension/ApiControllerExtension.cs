using System.Web.Http;
using Takeway.Web.Infrastructure.HttpAction;

namespace Takeway.Web.Infrastructure.Extension
{
    public static class ApiControllerExtension
    {
        public static NotFoundPlainTextActionResult NotFound(ApiController controller, string message)
        {
            return new NotFoundPlainTextActionResult(controller.Request, message);
        }

        public static NotFoundPlainTextActionResult BadRequest(ApiController controller, string message)
        {
            return new NotFoundPlainTextActionResult(controller.Request, message);
        }
    }
}