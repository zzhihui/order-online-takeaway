using System.Web.Mvc;
using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Controllers
{
    public class AccountController : BaseAccountController, IAccountController
    {
        public AccountController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);

        }

        public override ActionResult Login(string returnUrl)
        {
            ViewBag.Title = "Home Page";
            return RedirectToAction("Blank", "Home");

        }
    }
}