using System.Web.Mvc;
using Takeway.Web.Controllers.Templates;
using Takeway.Web.ViewModels.Admin;

namespace Takeway.Web.Areas.IndaSoftAdmin.Controllers
{
    public class AdminController : BaseAdminController, IAdminController
    {
        public AdminController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemoMulti);
        }

        public virtual ActionResult GeneratePassword()
        {
            var viewModel = new GeneratePasswordViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult GeneratePassword(GeneratePasswordViewModel viewModel)
        {

            viewModel.PasswordString = ShopService.GetEncryptedPassword(viewModel.Password, ShopId);
            return View(viewModel);
        }


    }
}