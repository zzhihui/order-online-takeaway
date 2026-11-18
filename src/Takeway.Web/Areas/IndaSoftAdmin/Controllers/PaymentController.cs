using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Common.Utility.Extension;
using Takeaway.Service.Contract.Entities.Payment;
using Takeaway.Service.Validations;
using Takeway.Web.Application.Extension;
using Takeway.Web.Application.Filter;
using Takeway.Web.Application.Utility;
using Takeway.Web.Controllers.Templates;
using Takeway.Web.ViewModels.Order;

namespace Takeway.Web.Areas.IndaSoftAdmin.Controllers
{
    [AreaAuthorization]
    public class PaymentController : BaseContoller
    {
        public PaymentController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemoMulti);
        }


        public ActionResult Index()
        {
            return RedirectToAction("GenericPayment");
        }

        public virtual ActionResult GenericPayment()
        {
            var viewModel = new PaymentViewModel
            {
                Payment = new WsPayment(),
                PaymentMonths = ShopService.LoadPaymentMonths(ShopId),
                PaymentYears = ShopService.LoadPaymentYears(ShopId)
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult GenericPayment(PaymentViewModel model)
        {

            model.Payment.CardExpirationMonth = model.SelectedMonth.ToString();
            model.Payment.CardExpirationYear = model.SelectedYear.ToString();

            var validator = new WsPaymentValidator();
            var result = validator.Validate(model.Payment);

            if (!result.IsValid)
            {
                ModelState.AddModelError("", result.ToErrorMessage());
                return View(model);
            }


            var paymentResult = ShopService.TakePayment(model.Payment, ShopId);
            if (!paymentResult.IsSuccess)
            {
                TempData[GetSessionKey(SessionKey.PaymentTempErrorMessage)] = paymentResult.ErrorMessage;
                return RedirectToAction("GenericPaymentResult");
            }

            TempData[GetSessionKey(SessionKey.PaymentTempConfirmationMessage)] = string.Format("Payment Complete and Reference:{0}.", paymentResult.PaymentRef);
            return RedirectToAction("GenericPaymentResult");
        }

        public virtual ActionResult GenericPaymentResult()
        {
            var viewModel = new PaymentResultViewModel()
            {
                ErrorMessage = (string)(TempData[GetSessionKey(SessionKey.PaymentTempErrorMessage)] ?? string.Empty),
                ConfirmationMessage = (string)(TempData[GetSessionKey(SessionKey.PaymentTempConfirmationMessage)] ?? string.Empty)
            };

            return View(viewModel);
        }


    }
}