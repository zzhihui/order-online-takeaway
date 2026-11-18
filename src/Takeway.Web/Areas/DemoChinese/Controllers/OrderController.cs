using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Core.Common.Utility.Extension;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;
using Takeaway.Service.Validations;
using Takeway.Web.Application.Extension;
using Takeway.Web.Application.Utility;
using Takeway.Web.Controllers.Templates;
using Takeway.Web.ViewModels.Order;

namespace Takeway.Web.Areas.DemoChinese.Controllers
{
    public class OrderController : BaseOrderController, IOrderController
    {
        public OrderController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }


        public override ActionResult Index()
        {
            return RedirectToAction("CategoryList");
        }

        [OutputCache(Duration = 600, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public override ActionResult CategoryList()
        {
            var viewModel = new CategoryItemViewModel
            {
                CategoryCategories =
                   ShopService.GetMenuItemCategoryByGroupId((int)MenuItemCategoryGroupEnum.External, ShopId)
                   .Where(r => !r.IsInternal.HasValue || !r.IsInternal.Value)
                   .ToList()
            };

            if (viewModel.CategoryItems == null || viewModel.CategoryItems.Count == 0)
                viewModel.CategoryItems =
                    ShopService.GetMenuItemByCategoryGroupId((int)MenuItemCategoryGroupEnum.External, ShopId)
                        .Where(r => !r.ExcludeOnline.HasValue || !r.ExcludeOnline.Value)
                        .ToList();

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult AddItemToBasket(AddItemToBasketViewModel viewModel)
        {
            if (!ModelState.IsValid) return View();

            InitOrderIfNull();


            for (var i = 0; i < viewModel.Quantity; i++)
            {
                var selectedItem = (WsMenuItem) viewModel.SelectedItem.Clone();

                selectedItem.Guid = Guid.NewGuid().ToString();

                foreach (var item in selectedItem.SubItemSelections)
                {
                    item.Selected = viewModel.SubOrderSelection.FirstOrDefault(r => r.Id == item.SelectedId);
                    selectedItem.Price += item.Selected.Price;
                }

                viewModel.MyOrder.OrderItems.Add(selectedItem);
            }
            //Calculate Total
            CalculateOrderTotal();

            return RedirectToAction("CategoryList");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleButton(Name = "action", Argument = "TakePayment")]
        public override ActionResult TakePayment(PaymentViewModel viewModel)
        {

            if (!ModelState.IsValid) return View(viewModel);

            viewModel.Payment.CardExpirationMonth = viewModel.SelectedMonth.ToString();
            viewModel.Payment.CardExpirationYear = viewModel.SelectedYear.ToString();

            var validator = new WsPaymentValidator();
            var result = validator.Validate(viewModel.Payment);

            if (!result.IsValid)
            {
                TempData[GetSessionKey(SessionKey.PaymentTempErrorMessage)] = result.ToErrorMessage();
                return RedirectToAction("TakePaymentResult");
            }

            var paymentResult = ShopService.TakePayment(viewModel.Payment, ShopId);
            if (!paymentResult.IsSuccess)
            {
                TempData[GetSessionKey(SessionKey.PaymentTempErrorMessage)] = "Fail to process Payment, please verify your card detail";
                Trace.TraceInformation(string.Format("TakePayment: Fail to Process Payment: {0}", paymentResult.ErrorMessage));
                return RedirectToAction("TakePaymentResult");
            }

            //set payment Ref
            viewModel.MyOrder.PaymentRef = paymentResult.PaymentRef;
            viewModel.MyOrder.OrderSource = (int)WsOrderSourceEnum.Web;
            var order = ShopService.SubmitCustomerOrder(viewModel.MyOrder, ShopId);
            TempOrderId = new CompleteOrderViewModel()
            {
                OrderId = order.OrderRef,
                OrderTotal = viewModel.MyOrder.OrderTotal,
                DeliveryCharge = viewModel.MyOrder.DeliveryCharge,
                SubTotal = viewModel.MyOrder.OrderTotal + viewModel.MyOrder.DeliveryCharge,
                IsDelivery = viewModel.MyOrder.DeliveryType == null || IsDelivery(viewModel.MyOrder.DeliveryType.Name)

            };
            viewModel.MyOrder = null;
            return RedirectToAction("CompleteOrder");
        }
    }
}