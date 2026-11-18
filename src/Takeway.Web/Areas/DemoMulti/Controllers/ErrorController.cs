using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoMulti.Controllers
{
    public class ErrorController : BaseErrorController, IErrorController
    {
        public ErrorController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemoMulti);
        }
      
    }
}