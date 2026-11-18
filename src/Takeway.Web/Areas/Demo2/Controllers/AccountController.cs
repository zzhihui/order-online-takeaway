using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.Demo2.Controllers
{
    public class AccountController : BaseAccountController, IAccountController
    {
        public AccountController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);

        }
    }
}