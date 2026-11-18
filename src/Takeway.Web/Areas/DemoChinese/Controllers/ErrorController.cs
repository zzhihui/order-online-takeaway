using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoChinese.Controllers
{
    public class ErrorController : BaseErrorController, IErrorController
    {
        public ErrorController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }
    }
}