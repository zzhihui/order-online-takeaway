using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.IndaSoftAdmin.Controllers
{
    public class ErrorController : BaseErrorController, IErrorController
    {
        public ErrorController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemoMulti);
        }
    }
}