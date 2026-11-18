using System.Collections.Generic;
using System.Windows.Input;
using Chance.MvvmCross.Plugins.UserInteraction;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Takeaway.Service.Contract.Entities.Payment;
using Takeaway.Service.Services;
using Takeway.Mobile.Core.Services.RestServices;

namespace Takeway.Mobile.Core.ViewModels.Admin
{
    public class ShopAdminPaymentViewModel : BaseViewModel
    {

        #region Properties

        private WsPayment _payment;
        public WsPayment Payment
        {
            get { return _payment; }
            set { _payment = value; RaisePropertyChanged(() => Payment); }
        }

        private bool _amountReadOnly;
        public bool AmountReadyOnly
        {
            get { return _amountReadOnly; }
            set { _amountReadOnly = value; RaisePropertyChanged(() => AmountReadyOnly); }
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

        public ShopAdminPaymentViewModel(IShopRestServiceWrapper service)
        {
            _service = service;
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
                CompleteOrder();
            else
                ErrorMessage = paymentResult.ErrorMessage;

        }


        private void CompleteOrder()
        {


            Mvx.Resolve<IUserInteraction>().Alert("Payment Taken Successfully",
                    async () =>
                    {
                        ShowViewModel<ShopAdminPaymentViewModel>();

                    }, "Payment Taken");
        }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(Payment.CardNumber))
            {
                ErrorMessage = "* Please enter cardnumber";
                return false;
            }


            if (string.IsNullOrEmpty(Payment.CardCvc))
            {
                ErrorMessage = "* Please enter CVC code";
                return false;
            }

            return true;
        }


        private void SetupPaymentLookup()
        {
            Payment = new WsPayment();
            PaymentMonths = _service.LoadPaymentMonths();
            PaymentYear = _service.LoadPaymentYears();

        }




    }
}