using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.Demo.Controllers
{
    public class AccountController : BaseAccountController, IAccountController
    {
        public AccountController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);

        }
    }
}