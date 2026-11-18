using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.IndaSoftAdmin.Controllers
{
    public class AccountController : BaseAccountController, IAccountController
    {
        public AccountController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemoMulti);

        }
    }
}