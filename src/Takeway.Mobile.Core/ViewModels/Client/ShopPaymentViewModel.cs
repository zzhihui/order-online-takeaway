using System;
using System.Collections.Generic;
using System.Windows.Input;
using Chance.MvvmCross.Plugins.UserInteraction;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Core.Common.Utility;
using Core.Common.Utility.Extension;
using Takeaway.Service.Contract.Entities.Payment;
using Takeaway.Service.Services;
using Takeaway.Service.Validations;
using Takeway.Mobile.Core.Resources;
using Takeway.Mobile.Core.Services.DataServices;
using Takeway.Mobile.Core.Services.RestServices;

namespace Takeway.Mobile.Core.ViewModels.Client
{
    public class ShopPaymentViewModel : BaseViewModel
    {

        #region Properties

        private WsPayment _payment;
        public WsPayment Payment
        {
            get { return _payment; }
            set { _payment = value; RaisePropertyChanged(() => Payment); }
        }

        private List<WsPaymentMonth> _paymentMonths;
        public List<WsPaymentMonth> PaymentMonths
        {
            get { return _paymentMonths; }
            set { _paymentMonths = value; RaisePropertyChanged(() => PaymentMonths); }
        }


        private WsPaymentMonth _selectedMonth;
        public WsPaymentMonth SelectedMonth
        {
            get { return _selectedMonth; }
            set { _selectedMonth = value; RaisePropertyChanged(() => SelectedMonth); }
        }


        private WsPaymentYear _selectedYear;
        public WsPaymentYear SelectedYear
        {
            get { return _selectedYear; }
            set { _selectedYear = value; RaisePropertyChanged(() => SelectedYear); }
        }

        private List<WsPaymentYear> _paymentYear;
        public List<WsPaymentYear> PaymentYear
        {
            get { return _paymentYear; }
            set { _paymentYear = value; RaisePropertyChanged(() => PaymentYear); }
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

        private readonly IShopRestServiceWrapper _service;
        private readonly IOrderRepository _orderRepository;

        public ShopPaymentViewModel(IShopRestServiceWrapper service, IGlobalModel globalModel, IOrderRepository orderRepository)
        {
            _service = service;
            _orderRepository = orderRepository;
            GlobalViewModel = globalModel;
        }
        public void Init()
        {
            SetupPaymentLookup();
        }

        #endregion


        private void OnBackCommand()
        {
            CompleteOrder();
        }

        private void OnFinishCommand()
        {

            Payment.CardExpirationMonth = SelectedMonth.Id.ToString();
            Payment.CardExpirationYear = SelectedYear.Id.ToString();

            if (!Validate()) return;

            var paymentResult = _service.TakePayment(Payment);
            if (paymentResult.IsSuccess)
            {
                GlobalViewModel.MyOrder.PaymentRef = paymentResult.PaymentRef;
                CompleteOrder();
            }
            else
                ErrorMessage = "Fail to process Payment, please verify your card detail";

        }


        private void CompleteOrder()
        {

            GlobalViewModel.MyOrder.OrderRef = GuidHashUtil.GetUniqueOrderKey();

            var order = _service.SubmitCustomerOrder(GlobalViewModel.MyOrder);

            _orderRepository.Add(DbOrderItem.CreateDbOrderItem(order));

            Mvx.Resolve<IUserInteraction>().Alert(string.Format(StringResource.page_submitorder_confirm_header, GlobalViewModel.MyOrder.OrderRef),
                    async () =>
                    {

                        GlobalViewModel.ResetOrder();
                        ShowViewModel<ShopClientMainViewModel>();

                    }, StringResource.page_submitorder_confirm_title);
        }

        private bool Validate()
        {

            var validator = new WsPaymentValidator();
            var result = validator.Validate(Payment);

            if (result.IsValid) return true;
            ErrorMessage = result.ToErrorMessage();
            return false;
        }


        private void SetupPaymentLookup()
        {
            Payment = new WsPayment();
            PaymentMonths = _service.LoadPaymentMonths();
            PaymentYear = _service.LoadPaymentYears();

            SetupPaymentTotal();
        }

        private void SetupPaymentTotal()
        {
            Payment.Amount = GlobalViewModel.MyOrder.OrderTotal + GlobalViewModel.MyOrder.DeliveryCharge;
        }

    }
}