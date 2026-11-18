using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Core.Common.Utility;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Print;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;
using ILog = log4net.ILog;
using LogManager = log4net.LogManager;

namespace Takeaway.Admin.Wpf.ViewModels
{
    public interface IBaseCompleteOrderViewModel : IScreen
    {
        WsOrder MyOrder { get; set; }
        decimal OrderTotal { get; set; }
        bool IsCompleted { get; set; }
        int TableId { get; set; }
        ObservableCollection<WsPaymentType> PaymentTypes { get; set; }
    }


    public abstract class BaseCompleteOrderViewModel : BaseViewModel, IBaseCompleteOrderViewModel
    {
        protected const string ResourcePrintReceipt = "Print Receipt";
        protected const string ResourceNoReceipt = "No Receipt";

        private static readonly ILog Logger = LogManager.GetLogger("BaseCompleteOrderViewModel");

        #region Attribs

        private int _inputSelection;
        public int InputSelection
        {
            get
            {
                return this._inputSelection;
            }
            set
            {
                if (value != this._inputSelection)
                {
                    this._inputSelection = value;
                    NotifyOfPropertyChange(() => InputSelection);
                }
            }
        }

        private WsOrder _myOrder;
        public WsOrder MyOrder
        {
            get { return _myOrder; }
            set
            {
                if (value != this._myOrder)
                {
                    this._myOrder = value;
                    NotifyOfPropertyChange(() => MyOrder);
                }
            }
        }

        private int _tableId;
        public int TableId
        {
            get
            {
                return this._tableId;
            }
            set
            {
                if (value != this._tableId)
                {
                    this._tableId = value;
                    NotifyOfPropertyChange(() => TableId);
                }
            }
        }

        private decimal _orderTotal;
        public decimal OrderTotal
        {
            get
            {
                return this._orderTotal;
            }
            set
            {
                if (value != this._orderTotal)
                {
                    this._orderTotal = value;
                    NotifyOfPropertyChange(() => OrderTotal);
                }
            }
        }


        private decimal _cashPayment;
        public decimal CashPayment
        {
            get
            {
                return this._cashPayment;
            }
            set
            {
                if (value != this._cashPayment)
                {
                    this._cashPayment = value;
                    CalculateChange();
                    NotifyOfPropertyChange(() => CashPayment);
                }
            }
        }

        private bool _cashPaymentVisible;
        public bool CashPaymentVisible
        {
            get
            {
                return this._cashPaymentVisible;
            }
            set
            {
                if (value != this._cashPaymentVisible)
                {
                    this._cashPaymentVisible = value;
                    if (!_cashPaymentVisible)
                        CashPayment = 0;
                    NotifyOfPropertyChange(() => CashPaymentVisible);
                }
            }
        }


        private decimal _cardPayment;
        public decimal CardPayment
        {
            get
            {
                return this._cardPayment;
            }
            set
            {
                if (value != this._cardPayment)
                {
                    this._cardPayment = value;
                    CalculateChange();
                    NotifyOfPropertyChange(() => CardPayment);
                }
            }
        }

        private bool _cardPaymentVisible;
        public bool CardPaymentVisible
        {
            get
            {
                return this._cardPaymentVisible;
            }
            set
            {
                if (value != this._cardPaymentVisible)
                {
                    this._cardPaymentVisible = value;
                    if (!_cardPaymentVisible)
                        CardPayment = 0;
                    NotifyOfPropertyChange(() => CardPaymentVisible);
                }
            }
        }


        private decimal _voucherPayment;
        public decimal VoucherPayment
        {
            get
            {
                return this._voucherPayment;
            }
            set
            {
                if (value != this._voucherPayment)
                {
                    this._voucherPayment = value;
                    CalculateChange();
                    NotifyOfPropertyChange(() => VoucherPayment);
                }
            }
        }


        private bool _voucherPaymentVisible;
        public bool VoucherPaymentVisible
        {
            get
            {
                return this._voucherPaymentVisible;
            }
            set
            {
                if (value != this._voucherPaymentVisible)
                {
                    this._voucherPaymentVisible = value;
                    if (!_voucherPaymentVisible)
                        VoucherPayment = 0;
                    NotifyOfPropertyChange(() => VoucherPaymentVisible);
                }
            }
        }

        private decimal _serviceCharge;
        public decimal ServiceCharge
        {
            get
            {
                return this._serviceCharge;
            }
            set
            {
                if (value != this._serviceCharge)
                {
                    this._serviceCharge = value;
                    CalculateChange();
                    NotifyOfPropertyChange(() => ServiceCharge);
                }
            }
        }

        private bool _serviceChargeVisible;
        public bool ServiceChargeVisible
        {
            get
            {
                return this._serviceChargeVisible;
            }
            set
            {
                if (value != this._serviceChargeVisible)
                {
                    this._serviceChargeVisible = value;
                    if (!_serviceChargeVisible)
                        ServiceCharge = 0;
                    NotifyOfPropertyChange(() => ServiceChargeVisible);
                }
            }
        }


        private decimal _change;
        public decimal Change
        {
            get
            {
                return this._change;
            }
            set
            {
                if (value != this._change)
                {
                    this._change = value;
                    NotifyOfPropertyChange(() => Change);
                }
            }
        }

        private ObservableCollection<WsPaymentType> _paymentTypes;
        public ObservableCollection<WsPaymentType> PaymentTypes
        {
            get
            {
                return this._paymentTypes;
            }
            set
            {
                if (value != this._paymentTypes)
                {
                    this._paymentTypes = value;
                    NotifyOfPropertyChange(() => PaymentTypes);
                }
            }
        }


        private WsPaymentType _selectedPaymentType;
        public WsPaymentType SelectedPaymentType
        {
            get
            {
                return this._selectedPaymentType;
            }
            set
            {
                if (value != this._selectedPaymentType)
                {
                    this._selectedPaymentType = value;
                    CheckPaymentVisible();
                    NotifyOfPropertyChange(() => SelectedPaymentType);
                }
            }
        }



        private bool _printReceipt;
        public bool PrintReceipt
        {
            get
            {
                return this._printReceipt;
            }
            set
            {
                if (value != this._printReceipt)
                {
                    this._printReceipt = value;
                    NotifyOfPropertyChange(() => PrintReceipt);
                    ChangePrintReceipt();
                }
            }
        }



        private string _printReceiptText;
        public string PrintReceiptText
        {
            get
            {
                return this._printReceiptText;
            }
            set
            {
                if (value != this._printReceiptText)
                {
                    this._printReceiptText = value;
                    NotifyOfPropertyChange(() => PrintReceiptText);
                }
            }
        }

        private string _finalComment;
        public string FinalComment
        {
            get
            {
                return this._finalComment;
            }
            set
            {
                if (value == this._finalComment) return;
                this._finalComment = value;
                NotifyOfPropertyChange(() => FinalComment);
            }
        }

        private bool _withDot;
        public bool WithDot
        {
            get
            {
                return this._withDot;
            }
            set
            {
                if (value != this._withDot)
                {
                    this._withDot = value;
                    NotifyOfPropertyChange(() => WithDot);
                }
            }
        }


        private bool _isCompleted;
        public bool IsCompleted
        {
            get
            {
                return this._isCompleted;
            }
            set
            {
                if (value != this._isCompleted)
                {
                    this._isCompleted = value;
                    NotifyOfPropertyChange(() => IsCompleted);
                }
            }
        }

        #endregion

        #region Constructor

        protected IEventAggregator _eventAggregator;
        protected IOfflineShopService _service;
        protected IWindowManager _windowManager;
        protected IAppScope _appScope;
        protected IAppSettings _appSettings;

        #endregion

        #region Methods

        public abstract void DoComplete();


        protected void InitView()
        {
            PrintReceipt = true;
            PrintReceiptText = ResourcePrintReceipt;
            WithDot = false;
            CardPaymentVisible = false;
            CardPaymentVisible = false;
            VoucherPaymentVisible = false;
            ServiceChargeVisible = false;
        }

        protected void ResetTable()
        {
            if (TableId <= 0) return;

            Logger.ErrorFormat("[DoComplete] Save Table ID: {0}", TableId);
           
            _service.ClearTable(TableId);
        }

        protected void CreateUniqueOrderRef()
        {
            if (string.IsNullOrEmpty(MyOrder.Guid))
                MyOrder.Guid = Guid.NewGuid().ToString();

            if (!string.IsNullOrEmpty(MyOrder.OrderRef))
                return;
            var uniqueRefFlag = _appSettings.GetSetting(AppSettingEnum.UniqueReference.ToString());
            if (uniqueRefFlag != "true")
            {
                Logger.DebugFormat("[DoComplete] Get Order Reference With Delivery Type");
                MyOrder.OrderRef = _service.GetNextOrderReference(MyOrder.DeliveryType, MyOrder.IsWalkIn ?? false);
                if(string.IsNullOrEmpty(MyOrder.OrderRef))
                    MyOrder.OrderRef = GuidHashUtil.GetUniqueOrderKey();
            }
            else
            {
                Logger.DebugFormat("[DoComplete] Get Order Reference With Delivery Type");
                if (string.IsNullOrEmpty(MyOrder.OrderRef))
                    MyOrder.OrderRef = GuidHashUtil.GetUniqueOrderKey();
            }
        }




        private void ChangePrintReceipt()
        {
            PrintReceiptText = PrintReceipt ? ResourcePrintReceipt : ResourceNoReceipt;
        }

        private void CalculateChange()
        {
            var paymentTotal = CashPayment + CardPayment + VoucherPayment + ServiceCharge;

            if (paymentTotal >= OrderTotal)
            {
                Change = paymentTotal - OrderTotal;
            }
            else
            {
                Change = 0;
            }
        }

        public void FieldSelection(int selection)
        {
            InputSelection = selection;
        }


        public void EnterValue(string enterValue)
        {
            switch (enterValue)
            {
                case "10":
                case "20":
                case "50":
                    SetInputValue(enterValue);
                    break;
                case ".":
                    WithDot = true;
                    break;
                default:

                    SetInputAppendValue(enterValue);


                    break;
            }
        }

        private void SetInputAppendValue(string enterValue)
        {
            switch (InputSelection)
            {
                case 1:

                    CashPayment = ParseInputValue(CashPayment, enterValue);
                    break;

                case 2:
                    CardPayment = ParseInputValue(CardPayment, enterValue);
                    break;
                case 3:
                    VoucherPayment = ParseInputValue(VoucherPayment, enterValue);
                    break;
                case 4:
                    ServiceCharge = ParseInputValue(ServiceCharge, enterValue);
                    break;
            }

        }

        private decimal ParseInputValue(decimal existingValue, string enterValue)
        {
            var input = string.Format(WithDot ? "{0}.{1}" : "{0}{1}", existingValue, enterValue);
            WithDot = false;
            decimal newValue = 0;
            return decimal.TryParse(input, out newValue) ? newValue : 0;
        }

        private void SetInputValue(string enterValue)
        {
            switch (InputSelection)
            {
                case 1:
                    CashPayment = decimal.Parse(enterValue);
                    break;

                case 2:
                    CardPayment = decimal.Parse(enterValue);
                    break;
                case 3:
                    VoucherPayment = decimal.Parse(enterValue);
                    break;
                case 4:
                    ServiceCharge = decimal.Parse(enterValue);
                    break;
            }

        }

        private void CheckPaymentVisible()
        {
            if (SelectedPaymentType == null)
                return;

            if (SelectedPaymentType.Name.ToUpper() == WsPaymentTypeEnum.Cash.ToString().ToUpper())
            {
                CashPaymentVisible = true;
                CardPaymentVisible = false;
                VoucherPaymentVisible = false;
                ServiceChargeVisible = false;

            }
            else if (SelectedPaymentType.Name.ToUpper() == WsPaymentTypeEnum.Card.ToString().ToUpper())
            {
                CashPaymentVisible = false;
                CardPaymentVisible = true;
                VoucherPaymentVisible = false;
                ServiceChargeVisible = false;
            }
            else if (SelectedPaymentType.Name.ToUpper() == WsPaymentTypeEnum.Cheque.ToString().ToUpper())
            {
                CashPaymentVisible = false;
                CardPaymentVisible = false;
                VoucherPaymentVisible = true;
                ServiceChargeVisible = false;
            }
            else if (SelectedPaymentType.Name.ToUpper() == WsPaymentTypeEnum.Mix.ToString().ToUpper())
            {
                CashPaymentVisible = true;
                CardPaymentVisible = true;
                VoucherPaymentVisible = true;
                ServiceChargeVisible = false;
            }


        }


        public void DoOpenCashDrawer()
        {
            var printSetting = GetFirstPrinterName(_appScope.Settings);
            var printerName = printSetting.PrinterName;
            if (string.IsNullOrEmpty(printerName))
                return;
            var printCashDrawerCommand = GetSettingByKey(WsOfflineSettingEnum.AppPrinterCashTillCommand, _appScope.Settings).SettingValue;
            if (string.IsNullOrEmpty(printCashDrawerCommand))
                return;

            var commandByte = printCashDrawerCommand.Split(',');
            var sendinByte = new byte[commandByte.Length];

            for (int i = 0; i < commandByte.Length; i++)
            {
                var result = IntegerStringUtil.ConvertStringValue(commandByte[i]);
                sendinByte[i] = (byte)result;
            }

            var commandString = ASCIIEncoding.ASCII.GetString(sendinByte);

            RawPrinterHelper.SendStringToPrinter(printerName, commandString);
        }

       

        protected bool SelectedCashType()
        {
            if (SelectedPaymentType == null)
                return false;
            return SelectedPaymentType.Name.ToUpper() == WsPaymentTypeEnum.Cash.ToString().ToUpper()
                   || SelectedPaymentType.Name.ToUpper() == WsPaymentTypeEnum.Mix.ToString().ToUpper();
        }

        public virtual void DoReprint()
        {
            PrinterQueue.PrintOrder(MyOrder, GetPrinterSettings(_appScope.Settings), _appScope.ShopInfo, "Takeaway Receipt", WsPrintModeEnum.CompleteOrder);
        }

       

        #endregion

    }
}