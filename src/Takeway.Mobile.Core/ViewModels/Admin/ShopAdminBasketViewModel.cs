using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Core.Common.Utility;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Services;
using Takeway.Mobile.Core.Services.RestServices;

namespace Takeway.Mobile.Core.ViewModels.Admin
{
    public class ShopAdminBasketViewModel : BaseViewModel
    {


        #region Properties
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

        private bool _canSubmit = false;
        public bool CanSubmit
        {
            get { return _canSubmit; }
            set { _canSubmit = value; RaisePropertyChanged(() => CanSubmit); }
        }

        private List<WsDeliveryOption> _deliveryOptions;
        public List<WsDeliveryOption> DeliveryOptions
        {
            get { return _deliveryOptions; }
            set { _deliveryOptions = value; RaisePropertyChanged(() => DeliveryOptions); }
        }


        #region Submit Information

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(() => Name); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; RaisePropertyChanged(() => Email); }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; RaisePropertyChanged(() => Comment); }
        }
        #endregion


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

        #endregion

        #region Constructor

        //Attribs
        private readonly IShopRestServiceWrapper _takewayClientService;

        public void Init()
        {
            GetDeliveryOptions();
            GetPendingOrder();
        }



        public ShopAdminBasketViewModel(IShopRestServiceWrapper takewayClientService)
        {
            _takewayClientService = takewayClientService;
        }

        #endregion

        #region Methods

        private void GetDeliveryOptions()
        {
            DeliveryOptions = _takewayClientService.GetDeliveryOptionsInternal();
        }

        private void OnSubmitOrderCommand()
        {
   
            Async(() =>
            {

                //Gathering Information
                FinaliseOrder();

                //Submit Information
                var orderSubmitted = _takewayClientService.SubmitCustomerOrder(MyOrder);

                //Clear Order
                ResetOrder();

            }, () =>
            {

                ShowViewModel<ShopAdminHomePadViewModel>();
            });


        }

        private void ResetOrder()
        {
            var globalViewModel = Mvx.Resolve<IGlobalModel>();
            globalViewModel.ResetOrder();

        }

        private void FinaliseOrder()
        {
            MyOrder.ContactName = Name;
            MyOrder.Email = Email;
            MyOrder.Comment = Comment;
            MyOrder.OrderRef = GuidHashUtil.GetUniqueOrderKey();
            MyOrder.IsInternalOrder = true;
            if (DeliveryOptions != null && DeliveryOptions.Count > 0)
                MyOrder.DeliveryType = DeliveryOptions.FirstOrDefault(r => r.Name == Takeaway.Service.Contract.Enum.DeliveryOptions.ForEatIn);

        }

        private void DoBasketItemsRemoveCommand(WsMenuItem item)
        {

            var globalViewModel = Mvx.Resolve<IGlobalModel>();
            globalViewModel.RemoveOrderItemBuyGuid(item);

            GetPendingOrder();

            ShowViewModel<ShopAdminBasketViewModel>();

        }

        private void OnAddMoreCommand()
        {
            ShowViewModel<ShopAdminHomePadViewModel>();
        }
        private void GetPendingOrder()
        {

            var globalViewModel = Mvx.Resolve<IGlobalModel>();

            CanSubmit = !globalViewModel.NoOrderItemYet();

            MyOrder = globalViewModel.MyOrder;

            MyOrderItems = MyOrder.OrderItems;
            OrderTotal = MyOrder.OrderTotal.ToString("C2", new CultureInfo("en-IE"));

        }

        #endregion

    }
}