using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Chance.MvvmCross.Plugins.UserInteraction;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Core.Common.Utility;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Services;
using Takeway.Mobile.Core.Resources;
using Takeway.Mobile.Core.Services.DataServices;
using Takeway.Mobile.Core.Services.RestServices;

namespace Takeway.Mobile.Core.ViewModels.Client
{
    public class ShopClientSubmitOrderViewModel : BaseViewModel
    {

        #region Properties

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


        private string _deliveryCharge;
        public string DeliveryCharge
        {
            get { return _deliveryCharge; }
            set { _deliveryCharge = value; RaisePropertyChanged(() => DeliveryCharge); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(() => Name); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; RaisePropertyChanged(() => Address); }
        }


        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; RaisePropertyChanged(() => Email); }
        }

        private List<WsDeliveryOption> _deliveryOptions;
        public List<WsDeliveryOption> DeliveryOptions
        {
            get { return _deliveryOptions; }
            set { _deliveryOptions = value; RaisePropertyChanged(() => DeliveryOptions); }
        }

        private WsDeliveryOption _deliveryOptionsSelected;
        public WsDeliveryOption DeliveryOptionsSelected
        {
            get { return _deliveryOptionsSelected; }
            set { _deliveryOptionsSelected = value; RaisePropertyChanged(() => DeliveryOptionsSelected); }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; RaisePropertyChanged(() => PhoneNumber); }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; RaisePropertyChanged(() => Comment); }
        }

        #endregion

        #region Commands

        public ICommand BackCommand
        {
            get
            {
                return new MvxCommand(OnBackCommand);
            }
        }
        public ICommand FinishCommand
        {
            get
            {
                return new MvxCommand(OnFinishCommand);
            }
        }

        #endregion

        #region Constructors

        private readonly IConfigurationRepository _dataRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IShopRestServiceWrapper _takewayClientService;


        public void Init()
        {
            GetPendingOrderAsync();
            GetDeliveryOptions();
            GetCachedContactDetail();
        }

        public ShopClientSubmitOrderViewModel(IConfigurationRepository dataRepository, IShopRestServiceWrapper takewayClientService, IOrderRepository orderRepository, IGlobalModel globalModel)
        {
            _dataRepository = dataRepository;
            _takewayClientService = takewayClientService;
            _orderRepository = orderRepository;
            GlobalViewModel = globalModel;

        }
        #endregion

        #region Methods

        private void GetPendingOrderAsync()
        {
            MyOrder = GlobalViewModel.MyOrder;
            OrderTotal = MyOrder.OrderTotal.ToString("C2", new CultureInfo("en-IE"));
            DeliveryCharge = GlobalViewModel.WsShopInfo.DeliveryCharge.Value.ToString("C2", new CultureInfo("en-IE"));
        }

        private void GetDeliveryOptions()
        {
            Async(() => { DeliveryOptions = _takewayClientService.GetDeliveryOptions(); });
        }

        private void GetCachedContactDetail()
        {

            Name = _dataRepository.GetOrderName();
            Address = _dataRepository.GetOrderAddress();
            PhoneNumber = _dataRepository.GetPhoneNumber();
            Email = _dataRepository.GetEmail();

        }


        private void OnFinishCommand()
        {
            if (!Validate()) return;
            StoreContactDetailInfoInCache();

            if (GlobalViewModel.TakingPaymentOn())
                CalculateTotalAndTakePayment();
            else
                CompleteOrder();
        }

        private void CalculateTotalAndTakePayment()
        {
            GlobalViewModel.SetUpOrderHeader(Name, Address, PhoneNumber, Comment, Email, DeliveryOptionsSelected);

            ShowViewModel<ShopPaymentViewModel>();
        }

        private bool Validate()
        {
            ErrorMessage = "";

            if (string.IsNullOrEmpty(Name))
            {
                ErrorMessage = StringResource.page_submitorder_validation_name;
                return false;
            }

            if (string.IsNullOrEmpty(PhoneNumber))
            {
                ErrorMessage = StringResource.page_submitorder_validation_phone;
                return false;
            }

            if (string.IsNullOrEmpty(Address))
            {
                ErrorMessage = StringResource.page_submitorder_validation_address;
                return false;
            }

            if (string.IsNullOrEmpty(Email))
            {
                ErrorMessage = "* Please enter email";
                return false;
            }

            if (!IsValidEmail(Email))
            {
                ErrorMessage = "* Please enter valid email";
                return false;
            }


            return true;

        }

        private bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.IsMatch(email);
        }

        private void CompleteOrder()
        {

            GlobalViewModel.SetUpOrderHeader(Name, Address, PhoneNumber, Comment, Email, DeliveryOptionsSelected);
            GlobalViewModel.MyOrder.OrderRef = GuidHashUtil.GetUniqueOrderKey();
            var order = _takewayClientService.SubmitCustomerOrder(GlobalViewModel.MyOrder);

            _orderRepository.Add(DbOrderItem.CreateDbOrderItem(order));

            Mvx.Resolve<IUserInteraction>().Alert(string.Format(StringResource.page_submitorder_confirm_header, GlobalViewModel.MyOrder.OrderRef),
                    async () =>
                    {

                        GlobalViewModel.ResetOrder();
                        ShowViewModel<ShopClientMainViewModel>();

                    }, StringResource.page_submitorder_confirm_title);
        }

        private void StoreContactDetailInfoInCache()
        {

            _dataRepository.CreateOrUpdateKey(DbConfigurationEnum.Name.ToString(), Name);
            _dataRepository.CreateOrUpdateKey(DbConfigurationEnum.Address.ToString(), Address);
            _dataRepository.CreateOrUpdateKey(DbConfigurationEnum.Phone.ToString(), PhoneNumber);
            _dataRepository.CreateOrUpdateKey(DbConfigurationEnum.Email.ToString(), Email);


        }

        private void OnBackCommand()
        {

            ShowViewModel<ShopClientBasketViewModel>();

        }

        #endregion
    }
}