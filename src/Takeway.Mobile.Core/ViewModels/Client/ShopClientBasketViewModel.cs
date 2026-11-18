using System.Collections.Generic;
using System.Globalization;
using System.Windows.Input;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Services;
using Takeway.Mobile.Core.Resources;
using Takeway.Mobile.Core.Services.RestServices;

namespace Takeway.Mobile.Core.ViewModels.Client
{
    public class ShopClientBasketViewModel : BaseViewModel
    {
        //Attribs
        private readonly IShopRestServiceWrapper _takewayClientService;

        //Properties
        private WsOrder _myOrder;
        public WsOrder MyOrder
        {
            get { return _myOrder; }
            set
            {
                _myOrder = value;
                RaisePropertyChanged(() => MyOrder);
            }
        }

        private string _orderTotal;
        public string OrderTotal
        {
            get { return _orderTotal; }
            set { _orderTotal = value; RaisePropertyChanged(() => OrderTotal); }
        }

        private bool _minOrderTextViewVisible = false;
        public bool MinOrderTextViewVisible
        {
            get { return _minOrderTextViewVisible; }
            set { _minOrderTextViewVisible = value; RaisePropertyChanged(() => MinOrderTextViewVisible); }
        }

        private bool _canSubmit = false;
        public bool CanSubmit
        {
            get { return _canSubmit; }
            set { _canSubmit = value; RaisePropertyChanged(() => CanSubmit); }
        }

        private string _minOrderText;
        public string MinOrderText
        {
            get { return _minOrderText; }
            set { _minOrderText = value; RaisePropertyChanged(() => MinOrderText); }
        }

        private string _deliverChargeText;
        public string DeliverChargeText
        {
            get { return _deliverChargeText; }
            set { _deliverChargeText = value; RaisePropertyChanged(() => DeliverChargeText); }
        }

        private string _discountText;
        public string DiscountText
        {
            get { return _discountText; }
            set { _discountText = value; RaisePropertyChanged(() => DiscountText); }
        }

        private List<WsMenuItem> _myOrderItems;
        public List<WsMenuItem> MyOrderItems
        {
            get { return _myOrderItems; }
            set { _myOrderItems = value; RaisePropertyChanged(() => MyOrderItems); }
        }


        public ICommand AddMoreCommand
        {
            get
            {
                return new MvxCommand(OnAddMoreCommand);
            }
        }
        public ICommand SubmitOrderCommand
        {
            get
            {
                return new MvxCommand(OnSubmitOrderCommand);
            }
        }


        public ICommand BasketItemsRemoveCommand
        {
            get
            {
                return new MvxCommand<WsMenuItem>(DoBasketItemsRemoveCommand);
            }
        }


        public void Init()
        {
            GetPendingOrder();
        }



        public ShopClientBasketViewModel(IShopRestServiceWrapper takewayClientService)
        {
            _takewayClientService = takewayClientService;
        }


        private void OnSubmitOrderCommand()
        {
            ShowViewModel<ShopClientSubmitOrderViewModel>();
        }
        private void DoBasketItemsRemoveCommand(WsMenuItem item)
        {

            var globalViewModel = Mvx.Resolve<IGlobalModel>();
            globalViewModel.RemoveOrderItem(item);

            GetPendingOrder();

            ShowViewModel<ShopClientBasketViewModel>();


        }
        private void OnAddMoreCommand()
        {
            ShowViewModel<ShopClientMainViewModel>();
        }
        private void GetPendingOrder()
        {

            var globalViewModel = Mvx.Resolve<IGlobalModel>();
            Async(() =>
            {

                globalViewModel.WsShopInfo = _takewayClientService.GetShopInfo();

            },

                () =>
                {

                    //Display Delivery Charge Information
                    var deliveryCharge = globalViewModel.GetDeliveryCharge();
                    DisplayDeliveryCharge(deliveryCharge);

                    //Display Min Order
                    var minPremium = globalViewModel.GetMinPremium();
                    DisplayMinPremiumBar(minPremium);


                    if (globalViewModel.IsShopClosed())//In case Closed
                    {
                        ErrorMessage = StringResource.page_basket_shopclosed;
                        CanSubmit = false;
                    }
                    else if (globalViewModel.NoOrderItemYet()) //In case no Item Selected
                    {
                        CanSubmit = false;
                    }
                    else
                    {

                        CanSubmit = globalViewModel.OrderLessThanMinOrder(); //Check if Order less than Min Premium
                        IsBusy = true;
                        globalViewModel.MyOrder = _takewayClientService.DiscountCustomerOrder(globalViewModel.MyOrder);
                        IsBusy = false;

                    }

                    MyOrder = globalViewModel.MyOrder;

                    DiscountText = MyOrder.OrderOriginalTotal > MyOrder.OrderTotal ? StringResource.page_basket_discount_text : string.Empty;

                    MyOrderItems = MyOrder.OrderItems;
                    OrderTotal = MyOrder.OrderTotal.ToString("C2", new CultureInfo("en-IE"));
                });
        }

        private void DisplayDeliveryCharge(decimal? deliveryCharge)
        {
            if (deliveryCharge.HasValue)
                DeliverChargeText = string.Format(StringResource.page_basket_deliverycharge, deliveryCharge.Value.ToString("C2", new CultureInfo("en-IE")));
        }

        private void DisplayMinPremiumBar(decimal minPremium)
        {
            if (minPremium <= 0) return;

            MinOrderTextViewVisible = true;
            MinOrderText = string.Format(StringResource.page_basket_minorder_text,
                minPremium.ToString("C2", new CultureInfo("en-IE")));
        }
    }
}