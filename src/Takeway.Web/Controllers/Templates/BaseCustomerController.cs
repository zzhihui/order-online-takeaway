using System.Web.Mvc;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.Application.Utility;
using Takeway.Web.Models.Customer;

namespace Takeway.Web.Controllers.Templates
{

    public class BaseCustomerController : BaseContoller
    {
        protected static bool SmsValidationRequired = false;


        public virtual ActionResult Register()
        {
            var viewModel = new CustomerViewModel();
            viewModel.RegisteredCustomer = new WsShopCustomer();
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
            viewModel.RegisteredCustomer = new WsShopCustomer()
            {
                Email = viewModel.Email,
                Password = viewModel.Password,
                PhoneNumber = viewModel.MobileNumber,
                ContactName = viewModel.ContactName,
                Address = viewModel.Address,
            };

            //Set marketing permission
            if (viewModel.MarkingPermission)
                viewModel.RegisteredCustomer.MarketingPermission = 1;

            if (SmsValidationRequired)
                viewModel.RegisteredCustomer.ValidationPending = 1;

            var customer = ShopService.RegisterCustomer(viewModel.RegisteredCustomer, ShopId);

            if (customer == null)
            {
                viewModel.ErrorMessage = "Issue register new customer, user may already exists";
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
                TempData[GetSessionKey(SessionKey.RegisterCustomer)] = customer;
                return RedirectToAction("SmsVerification");
            }
            else
            {
                //Otherwise 
                return RedirectToAction("Login");
            }

        }
        public virtual ActionResult SmsVerification()
        {
            var viewModel = new VerificationViewModel();
            viewModel.RegisteredCustomer = (WsShopCustomer)TempData[GetSessionKey(SessionKey.RegisterCustomer)];
            return View(viewModel);
        }
        public virtual ActionResult SmsValidation(int id)
        {
            var viewModel = new VerificationViewModel();
            viewModel.RegisteredCustomer = ShopService.GetCustomerById(id, ShopId);
            viewModel.MobileNumber = viewModel.RegisteredCustomer.PhoneNumber;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult SmsValidation(VerificationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ErrorMessage = "Please enter valid information";
                return View(viewModel);
            }
            viewModel.RegisteredCustomer.PhoneNumber = viewModel.MobileNumber;
            var customer = ShopService.VerifySms(viewModel.RegisteredCustomer, ShopId);
            if (customer == null)
            {
                viewModel.ErrorMessage = "Issue validating customer";
                return View(viewModel);
            }

            TempData[GetSessionKey(SessionKey.RegisterCustomer)] = customer;
            return RedirectToAction("ActiveAccountStart");
        }
        public virtual ActionResult ActiveAccountStart()
        {
            var viewModel = new ActiveAccountViewModel();
            viewModel.RegisteredCustomer = (WsShopCustomer)TempData[GetSessionKey(SessionKey.RegisterCustomer)];
            return View(viewModel);
        }
        public virtual ActionResult ActiveAccount(int id)
        {
            var viewModel = new ActiveAccountViewModel();
            viewModel.UserId = id;
            viewModel.RegisteredCustomer = ShopService.GetCustomerById(id, ShopId);
            viewModel.RegisteredCustomer.AuthenticationCode = "";
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ActiveAccount(ActiveAccountViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ErrorMessage = "Please enter valid information";
                return View(viewModel);
            }

            var customer = ShopService.ActivateBySms(viewModel.RegisteredCustomer, ShopId);

            if (customer == null)
            {
                viewModel.ErrorMessage = "Failed to Active Your Account";
                return View(viewModel);
            }

            return RedirectToAction("Login");
        }
        public virtual ActionResult Login()
        {
            var viewModel = new LoginViewModel();
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ErrorMessage = "Please enter valid information";
                ModelState.AddModelError("", viewModel.ErrorMessage);
                return View(viewModel);
            }

            var customer = ShopService.LoginCustomer(viewModel.Email, viewModel.Password, ShopId);

            if (customer == null)
            {
                viewModel.ErrorMessage = "Failed to Login, please verify your credential";
                ModelState.AddModelError("", viewModel.ErrorMessage);
                return View(viewModel);
            }
            if (customer.ValidationPending == 1 && SmsValidationRequired)
            {
                TempData[GetSessionKey(SessionKey.RegisterCustomer)] = customer;
                return RedirectToAction("SmsVerification");
            }

            viewModel.ShopCustomer = customer;

            return RedirectToAction("CategoryList", "Order");
        }
        public virtual ActionResult Logout()
        {
            var viewModel = new LoginViewModel();
            viewModel.ShopCustomer = null;
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
        public virtual ActionResult Forgetpassword()
        {
            var viewModel = new ForgetPasswordViewModel();
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Forgetpassword(ForgetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ErrorMessage = "Please enter valid information";
                return View(viewModel);
            }

            var customer = ShopService.Forgetpassword(viewModel.Email, ShopId);
            if (customer == null)
                return RedirectToAction("ForgetpasswordFailed");
            return RedirectToAction("ForgetpasswordComplete");
        }
        public virtual ActionResult ForgetpasswordComplete()
        {
            return View();
        }
        public virtual ActionResult ForgetpasswordFailed()
        {
            return View();
        }



        public virtual ActionResult ViewCustomer()
        {
            var viewModel = new CustomerViewModel();
            return View(viewModel);
        }


        public virtual ActionResult EditMyInfo()
        {
            var viewModel = new CustomerViewModel();
            viewModel.Email = viewModel.ShopCustomer.Email;
            viewModel.Address = viewModel.ShopCustomer.Address;
            viewModel.ContactName = viewModel.ShopCustomer.ContactName;
            viewModel.MobileNumber = viewModel.ShopCustomer.PhoneNumber;
            viewModel.CustomerId = viewModel.ShopCustomer.Id;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult EditMyInfo(CustomerViewModel viewModel)
        {
            viewModel.RegisteredCustomer = new WsShopCustomer()
            {
                Email = viewModel.Email,
                PhoneNumber = viewModel.MobileNumber,
                ContactName = viewModel.ContactName,
                Address = viewModel.Address,
                Id = viewModel.CustomerId ?? 0,
            };

            var customer = ShopService.UpdateCustomerInfo(viewModel.RegisteredCustomer, ShopId);

            if (customer == null)
            {
                viewModel.ErrorMessage = "Issue update customer information, please try again";
                ModelState.AddModelError("", viewModel.ErrorMessage);
                return View(viewModel);
            }
            if (!string.IsNullOrEmpty(customer.ErrorMessage))
            {
                ModelState.AddModelError("", customer.ErrorMessage);
                return View(viewModel);
            }

            viewModel.ShopCustomer = customer;
            return RedirectToAction("ViewCustomer");

        }

        public virtual ActionResult ResetPassword()
        {
            var viewModel = new CustomerViewModel();
            viewModel.Email = viewModel.ShopCustomer.Email;
            viewModel.CustomerId = viewModel.ShopCustomer.Id;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ResetPassword(CustomerViewModel viewModel)
        {
            viewModel.RegisteredCustomer = new WsShopCustomer()
            {
                Email = viewModel.Email,
                Password = viewModel.Password,
                Id = viewModel.CustomerId ?? 0,
            };

            var customer = ShopService.ResetCustomerPassword(viewModel.RegisteredCustomer, ShopId);

            if (customer == null)
            {
                viewModel.ErrorMessage = "Issue reset your password, please try again";
                ModelState.AddModelError("", viewModel.ErrorMessage);
                return View(viewModel);
            }
            if (!string.IsNullOrEmpty(customer.ErrorMessage))
            {
                ModelState.AddModelError("", customer.ErrorMessage);
                return View(viewModel);
            }

            viewModel.ShopCustomer = customer;
            return RedirectToAction("ViewCustomer");
        }
    }
}