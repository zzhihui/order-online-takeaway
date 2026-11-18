using System.Web.Mvc;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.Controllers.Templates;
using Takeway.Web.Models.Customer;

namespace Takeway.Web.Areas.Demo2.Controllers
{
    public class CustomerController : BaseContoller
    {
        private const bool SmsValidationRequired = false;
        public CustomerController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }

        public virtual ActionResult Register()
        {
            var viewModel = new CustomerViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Register(CustomerViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                viewModel.ErrorMessage = "Please enter valid information";
                return View(viewModel);
            }

            //Saving item
            viewModel.SelectedCustomer = new WsShopCustomer()
            {
                Email = viewModel.Email,
                Password = viewModel.Password
            };

            if (SmsValidationRequired)
                viewModel.SelectedCustomer.ValidationPending = 1;

            var customer = ShopService.RegisterCustomer(viewModel.SelectedCustomer, ShopId);

            if (customer == null)
            {
                viewModel.ErrorMessage = "Issue saving item";
                ModelState.AddModelError("", viewModel.ErrorMessage);
                return View(viewModel);
            }
            if (!string.IsNullOrEmpty(customer.ErrorMessage))
            {
                ModelState.AddModelError("", customer.ErrorMessage);
                return View(viewModel);
            }

            //If Require SMS validation then validation page
            if (SmsValidationRequired)
            {
                viewModel.SelectedCustomer = customer;
                return RedirectToAction("SmsValidation");
            }
            else
            {
                //Otherwise 
                return RedirectToAction("Login");
            }

        }


        public virtual ActionResult SmsValidation()
        {
            var viewModel = new CustomerViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult SmsValidation(CustomerViewModel viewModel)
        {
            return RedirectToAction("ActiveAccount");
        }


        public virtual ActionResult ActiveAccount()
        {
            var viewModel = new CustomerViewModel();
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ActiveAccount(CustomerViewModel viewModel)
        {
            return View(viewModel);
        }


        public virtual ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Login(CustomerViewModel viewModel)
        {
            return View();
        }

        public virtual ActionResult Logout()
        {
            return View();
        }

    }
}