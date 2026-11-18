using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.Demo.Controllers
{
    public class ErrorController : BaseErrorController, IErrorController
    {
        public ErrorController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }
      
    }
}