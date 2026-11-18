using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Core.Common.Utility;
using Core.Common.Utility.Extension;
using Takeaway.Service.Contract.Entities.Payment;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;
using Takeaway.Service.Validations;
using Takeway.Web.Application.Extension;
using Takeway.Web.Application.Utility;
using Takeway.Web.ViewModels.Order;

namespace Takeway.Web.Controllers.Templates
{
    public interface IOrderController
    {
        ActionResult Index();
        ActionResult Help();
        ActionResult DeliveryArea();
        ActionResult Category();
        ActionResult CategoryList();
        ActionResult CategoryItem(int? id);
        ActionResult AddItemToBasket(int? id);
        ActionResult RemoveItemFromBasket(string id);
        ActionResult AddItemToBasket(AddItemToBasketViewModel viewModel);
        ActionResult CompleteOrder();
        ActionResult Basket(BasketViewModel viewModel);
        ActionResult BasketSummary();
        ActionResult TakePayment();
        ActionResult TakePayment(PaymentViewModel viewModel);
        ActionResult PayLater(PaymentViewModel viewModel);
        ActionResult Basket();
        ActionResult GenericPayment();
        ActionResult GenericPayment(PaymentViewModel model);
        ActionResult GenericPaymentResult();

    }


    public class BaseOrderController : BaseContoller
    {
        public CompleteOrderViewModel TempOrderId
        {
            get
            {
                return (CompleteOrderViewModel)TempData[GetSessionKey(SessionKey.OrderTempOrderId)];
            }
            set
            {
                TempData[GetSessionKey(SessionKey.OrderTempOrderId)] = value;
            }
        }

        protected void CalculateOrderTotal()
        {
            var viewModel = new AddItemToBasketViewModel();

            viewModel.MyOrder.OrderTotal =
                viewModel.MyOrder.OrderItems.Where(item => item.Price.HasValue).Sum(item => item.Price.Value);
            viewModel.MyOrder.OrderOriginalTotal = viewModel.MyOrder.OrderTotal;
        }

        protected void CalculateDeliveryCharge(BasketViewModel viewModel, WsShop shopInfo)
        {
            var deliveryCost = viewModel.DeliveryAreas.FirstOrDefault(r => r.Id == viewModel.SelectedDeliveryAreaId);


            if (viewModel.MyOrder.DeliveryType.Name == DeliveryOptions.ForCollection)
            {
                viewModel.MyOrder.DeliveryCharge = 0;
            }
            else if (viewModel.MyOrder.DeliveryCharge == 0)
            {
                viewModel.MyOrder.DeliveryCharge = deliveryCost.DeliveryCost.Value;
            }

        }

        protected bool IsDelivery(string deliveryTypeName)
        {
            return deliveryTypeName != DeliveryOptions.ForCollection;
        }

        protected void InitOrderIfNull()
        {

            var viewModel = new AddItemToBasketViewModel();
            if (viewModel.MyOrder == null)
            {
                viewModel.MyOrder = new WsOrder { Guid = Guid.NewGuid().ToString() };
            }
        }

        public string CreateAnchor(int id)
        {
            return string.Format("#arc{0}", id);
        }

        public virtual ActionResult Index()
        {


            var serverSettings = ShopService.GetSystemSettings(ShopId);
            var allowOnlineOrderSetting =
                serverSettings.FirstOrDefault(r => r.Name == WsServerSettingEnum.OnlineAllowonlineorder.ToString());
            var appAvailable = serverSettings.FirstOrDefault(r => r.Name == WsServerSettingEnum.OnlineAppavailable.ToString());
            var viewModel = new OrderViewModel
            {
                ShopInfo = ShopService.GetShopInfo(ShopId),
                AllowOnlineOrder = allowOnlineOrderSetting,
                AppAvailable = appAvailable
            };

            if (viewModel.ShopInfo == null)
                return Redirect("http://www.kingfood.ie");

            return View(viewModel);
        }

        public virtual ActionResult Help()
        {
            return View();
        }

        public virtual ActionResult DeliveryArea()
        {
            var viewModel = new DeliveryAreaViewModel { DeliveryAreas = ShopService.GetDeliveryArea((int)DeliveryAreaEnum.Takeaway, ShopId), };
            return View(viewModel);
        }

        public virtual ActionResult Category()
        {

            var categories =
                ShopService.GetMenuItemCategoryByGroupId((int)MenuItemCategoryGroupEnum.External, ShopId)
                    .Where(r => !r.IsInternal.HasValue || !r.IsInternal.Value)
                    .ToList();

            var viewModel = new CategoryViewModel { CategoryCategories = categories };
            return View(viewModel);
        }

        public virtual ActionResult CategoryList()
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


        public virtual ActionResult CategoryItem(int? id)
        {
            var serverSettings = ShopService.GetSystemSettings(ShopId);
            var allowOnlineOrderSetting =
                serverSettings.FirstOrDefault(r => r.Name == WsServerSettingEnum.OnlineAllowonlineorder.ToString());

            var viewModel = new CategoryItemViewModel { AllowOnlineOrder = allowOnlineOrderSetting };

            if (!id.HasValue || id.Value <= 0) return View(viewModel);

            viewModel.CategoryId = id.Value;
            viewModel.CategoryItems = ShopService.GetMenuItemsByCategory(viewModel.CategoryId, ShopId)
                                                 .Where(r => !r.ExcludeOnline.HasValue || !r.ExcludeOnline.Value).ToList();
            viewModel.Categories =
                ShopService.GetMenuItemCategoryByGroupId((int)MenuItemCategoryGroupEnum.External, ShopId)
                    .Where(r => !r.IsInternal.HasValue || !r.IsInternal.Value)
                    .ToList();
            return View(viewModel);

        }

        public virtual ActionResult AddItemToBasket(int? id)
        {
            var subItemsSelections = new List<WsMenuItem>();
            var viewModel = new AddItemToBasketViewModel();

            if (!id.HasValue || id.Value <= 0) return View(viewModel);

            if (viewModel.CategoryItems != null)
            {
                viewModel.SelectedItem = viewModel.CategoryItems.FirstOrDefault(r => r.Id == id.Value);
                foreach (var subitem in viewModel.SelectedItem.SubItemSelections)
                {
                    subItemsSelections.AddRange(subitem.Selections);
                }

            }

            viewModel.SubOrderSelection = subItemsSelections.Distinct().ToList();
            return View(viewModel);
        }

        public ActionResult RemoveItemFromBasket(string id)
        {

            InitOrderIfNull();

            var viewModel = new AddItemToBasketViewModel();

            var selection = viewModel.MyOrder.OrderItems.FirstOrDefault(r => r.Guid == id);
            viewModel.MyOrder.OrderItems.Remove(selection);

            CalculateOrderTotal();

            return RedirectToAction("Basket");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult AddItemToBasket(AddItemToBasketViewModel viewModel)
        {
            if (!ModelState.IsValid) return View();

            InitOrderIfNull();


            for (int i = 0; i < viewModel.Quantity; i++)
            {
                var selectedItem = (WsMenuItem)viewModel.SelectedItem.Clone();

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

            return RedirectToAction("Basket");
        }


        [ChildActionOnly]
        public virtual ActionResult BasketSummary()
        {

            var viewmodel = new BasketSummaryViewModel();
            viewmodel.BasketItemCount = viewmodel.MyOrder == null ? 0 : viewmodel.MyOrder.OrderItems.Count;

            return PartialView(viewmodel);
        }

        public virtual ActionResult CompleteOrder()
        {
            var viewModel = TempOrderId;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Basket(BasketViewModel viewModel)
        {

            if (!ModelState.IsValid) return View(viewModel);

            var shopInfo = ShopService.GetShopInfo(ShopId);
            viewModel.MyOrder.ContactName = viewModel.Name;
            viewModel.MyOrder.PhoneNumber = viewModel.PhoneNumber;
            viewModel.MyOrder.Email = viewModel.Email;
            viewModel.MyOrder.Address = viewModel.Address;
            viewModel.MyOrder.Comment = viewModel.Comment;
            if (viewModel.MarkingPermission)
                viewModel.MyOrder.MarketingPermission = 1;

            viewModel.MyOrder.DeliveryType =
                viewModel.DeliveryOptions.FirstOrDefault(r => r.Id == viewModel.SelectedDeliveryOptionId);

            CalculateDeliveryCharge(viewModel, shopInfo);
            if (!string.IsNullOrEmpty(viewModel.CollectionTimeSelected))
            {
                viewModel.MyOrder.CollectionTime = viewModel.CollectionTimeSelected;
            }
            
            


            viewModel.MyOrder.OrderRef = GuidHashUtil.GetUniqueOrderKey();

            if (shopInfo.TakePayment.HasValue && shopInfo.TakePayment.Value)
                return RedirectToAction("TakePayment");

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

        public virtual ActionResult TakePayment()
        {

            var viewModel = new PaymentViewModel
            {
                Payment = new WsPayment(),
                PaymentMonths = ShopService.LoadPaymentMonths(ShopId),
                PaymentYears = ShopService.LoadPaymentYears(ShopId),

            };

            viewModel.Payment.Amount = viewModel.MyOrder.OrderTotal + viewModel.MyOrder.DeliveryCharge;
            viewModel.OrderTotal = viewModel.MyOrder.OrderTotal;
            viewModel.DeliveryCharge = viewModel.MyOrder.DeliveryCharge;

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleButton(Name = "action", Argument = "TakePayment")]
        public virtual ActionResult TakePayment(PaymentViewModel viewModel)
        {

            if (!ModelState.IsValid) return View(viewModel);

            viewModel.Payment.CardExpirationMonth = viewModel.SelectedMonth.ToString();
            viewModel.Payment.CardExpirationYear = viewModel.SelectedYear.ToString();

            var validator = new WsPaymentValidator();
            var result = validator.Validate(viewModel.Payment);

            if (!result.IsValid)
            {
                ModelState.AddModelError("", result.ToErrorMessage());
                return View(viewModel);
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleButton(Name = "action", Argument = "PayLater")]
        public virtual ActionResult PayLater(PaymentViewModel viewModel)
        {
            if (!ModelState.IsValid) return RedirectToAction("TakePayment");
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


        public virtual ActionResult Basket()
        {
            var viewModel = new BasketViewModel
            {
                ShopInfo = ShopService.GetShopInfo(ShopId),
                DeliveryOptions = ShopService.GetDeliveryOptionsByTime(ShopId),
                DeliveryAreas = ShopService.GetDeliveryArea((int)DeliveryAreaEnum.Takeaway, ShopId)

            };


            viewModel.CollectionTimeOptions = viewModel.ShopInfo.CollectionOptions;
            InitOrderIfNull();

            viewModel.MinOrder = viewModel.ShopInfo.MinOrder ?? 0;

            if (viewModel.ShopCustomer != null)
            {
                viewModel.Name = viewModel.ShopCustomer.ContactName;
                viewModel.PhoneNumber = viewModel.ShopCustomer.PhoneNumber;
                viewModel.Address = viewModel.ShopCustomer.Address;
                viewModel.Email = viewModel.ShopCustomer.Email;
                viewModel.MarkingPermission = viewModel.ShopCustomer.MarketingPermission.HasValue &&
                                              viewModel.ShopCustomer.MarketingPermission.Value == 1;
                viewModel.MyOrder.CustomerId = viewModel.ShopCustomer.Id;
            }




            if (viewModel.MyOrder.OrderItems.Count > 0)
                viewModel.MyOrder = ShopService.DiscountCustomerOrder(viewModel.MyOrder, ShopId);

            return View(viewModel);
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
        [MultipleButton(Name = "action", Argument = "GenericPayment")]
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

            TempData["Payment.ConfirmationMessage"] = string.Format("Payment Complete and Reference:{0}.", paymentResult.PaymentRef);
            return RedirectToAction("GenericPaymentResult");
        }

        public virtual ActionResult TakePaymentResult()
        {

            var viewModel = new PaymentResultViewModel()
            {
                ErrorMessage = (string)(TempData[GetSessionKey(SessionKey.PaymentTempErrorMessage)] ?? string.Empty),
                ConfirmationMessage = (string)(TempData[GetSessionKey(SessionKey.PaymentTempConfirmationMessage)] ?? string.Empty)
            };

            return View(viewModel);
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

        [ChildActionOnly]
        public virtual ActionResult ProfileSummary()
        {

            var viewmodel = new BasketSummaryViewModel();

            return PartialView(viewmodel);
        }


        public virtual ActionResult ListTakePayment()
        {
            var viewModel = new ListPaymentViewModel();

            viewModel.TotalAmount = viewModel.MyOrder.OrderTotal + viewModel.MyOrder.DeliveryCharge;
            viewModel.OrderTotal = viewModel.MyOrder.OrderTotal;
            viewModel.DeliveryCharge = viewModel.MyOrder.DeliveryCharge;
            viewModel.CostCredits = viewModel.TotalAmount * 1000;

            if (viewModel.ShopCustomer != null && viewModel.ShopCustomer.RewardPoints.HasValue
                && viewModel.ShopCustomer.RewardPoints.Value >= viewModel.TotalAmount * 1000)
            {
                viewModel.IsEnoughPoint = true;
            }

            return View(viewModel);
        }

        public virtual ActionResult TakeCashPayment()
        {

            var viewModel = new PaymentViewModel();

            viewModel.MyOrder.OrderSource = (int)WsOrderSourceEnum.Web;
            var order = ShopService.SubmitCustomerOrder(viewModel.MyOrder, ShopId);
            if (order.RewardPoints.HasValue)
                viewModel.ShopCustomer.RewardPoints = viewModel.ShopCustomer.RewardPoints + order.RewardPoints.Value;
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
        public virtual ActionResult TakeLoyaltyPayment()
        {

            /* 1. Check Points
             * 2. Remove points
             * 3. Submit order
             */

            var viewModel = new PaymentViewModel
            {
                Payment = new WsPayment(),
            };

            viewModel.Payment.Amount = viewModel.MyOrder.OrderTotal + viewModel.MyOrder.DeliveryCharge;
            viewModel.OrderTotal = viewModel.MyOrder.OrderTotal;
            viewModel.DeliveryCharge = viewModel.MyOrder.DeliveryCharge;
            var totalcostCredit = viewModel.Payment.Amount * 1000;

            if (viewModel.ShopCustomer == null || !viewModel.ShopCustomer.RewardPoints.HasValue
                || viewModel.ShopCustomer.RewardPoints.Value < totalcostCredit)
            {
                return RedirectToAction("Basket");
            }

            var paymentResult = ShopService.TakeLoyaltyPayment(viewModel.Payment, viewModel.ShopCustomer.Id, ShopId);
            if (!paymentResult.IsSuccess)
            {
                TempData[GetSessionKey(SessionKey.PaymentTempErrorMessage)] = "Fail to process Payment, please verify if you have enough credit";
                Trace.TraceInformation(string.Format("TakeLoyaltyPayment: Fail to Process Payment: {0}", paymentResult.ErrorMessage));
                return RedirectToAction("TakeLoyaltyPaymentResult");
            }

            //set payment Ref
            viewModel.MyOrder.PaymentRef = paymentResult.PaymentRef;
            viewModel.ShopCustomer.RewardPoints = viewModel.ShopCustomer.RewardPoints - paymentResult.CostCredit;

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

        public virtual ActionResult TakeLoyaltyPaymentResult()
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