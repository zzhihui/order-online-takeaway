using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.Demo2.Controllers
{
    public class ErrorController : BaseErrorController, IErrorController
    {
        public ErrorController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }
      
    }
}