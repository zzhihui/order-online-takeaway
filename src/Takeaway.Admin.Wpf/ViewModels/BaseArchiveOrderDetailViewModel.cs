using System.Collections.Generic;
using System.Collections.ObjectModel;
using Caliburn.Micro;
using Core.Common.Resources;
using Core.Common.Utility;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;
using ILog = log4net.ILog;
using LogManager = log4net.LogManager;

namespace Takeaway.Admin.Wpf.ViewModels
{
    public interface IBaseArchiveOrderDetailViewModel : IScreen
    {
        WsOrder SelectedOrder { get; set; }
        IBaseArchiveOrderViewModel ParentPage { get; set; }
        ObservableCollection<WsDriver> Drivers { get; set; }
        ObservableCollection<WsPaymentType> PaymentTypes { get; set; }
        ObservableCollection<WsOrder> OpenOrders { get; set; }

    }


    public abstract class BaseArchiveOrderDetailViewModel : BaseViewModel, IBaseArchiveOrderDetailViewModel
    {
        private static readonly ILog Logger = LogManager.GetLogger("BaseArchiveOrderDetailViewModel");

        protected IOnlineService _onlineService;
        protected IOfflineShopService _shopService;
        protected  IAppScope _appScope;
        protected  IWindowManager _windowManager;

        #region Properties

        /// <summary>
        /// Open Orders
        /// </summary>
        private ObservableCollection<WsOrder> _openOrders;
        public ObservableCollection<WsOrder> OpenOrders
        {
            get
            {
                return this._openOrders;
            }
            set
            {
                if (value == this._openOrders) return;
                this._openOrders = value;
                NotifyOfPropertyChange(() => OpenOrders);
            }
        }

        /// <summary>
        /// List of drivers
        /// </summary>
        private ObservableCollection<WsDriver> _drivers;
        public ObservableCollection<WsDriver> Drivers
        {
            get
            {
                return this._drivers;
            }
            set
            {
                if (value != this._drivers)
                {
                    this._drivers = value;
                    NotifyOfPropertyChange(() => Drivers);
                }
            }
        }

        /// <summary>
        /// Selected Order
        /// </summary>
        private WsOrder _selectedOrder;
        public WsOrder SelectedOrder
        {
            get
            {
                return this._selectedOrder;
            }
            set
            {
                if (value == this._selectedOrder) return;
                this._selectedOrder = value;
                SetupOtherDetail();
                NotifyOfPropertyChange(() => SelectedOrder);
            }
        }

        /// <summary>
        /// Parent Page
        /// </summary>
        private IBaseArchiveOrderViewModel _parentPage;
        public IBaseArchiveOrderViewModel ParentPage
        {
            get
            {
                return this._parentPage;
            }
            set
            {
                if (value == this._parentPage) return;
                this._parentPage = value;
                NotifyOfPropertyChange(() => ParentPage);
            }
        }

        /// <summary>
        /// List of Payment Types
        /// </summary>
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


        #endregion

        #region Methods

        private void SetupOtherDetail()
        {
            if (SelectedOrder == null)
                return;

            Translate(SelectedOrder.OrderItems, GetTranslatedFlag(_appScope.Settings));

        }

        

        public virtual void InitApp()
        {

        }

        public virtual void DoUpdateDriver()
        {
            var orderDetail = IoC.Get<ISelectDriverViewModel>();
            orderDetail.Drivers = Drivers;
            orderDetail.SelectedOrder = SelectedOrder;
            OpenWindow(_windowManager, orderDetail, "Select Driver");
            if (orderDetail.IsCompleted)
                this.TryClose();

        }

        public virtual void DoUpdatePayment()
        {

            var viewModel = IoC.Get<IUpdatePaymentViewModel>();
            viewModel.MyOrder = SelectedOrder;
            viewModel.PaymentTypes = PaymentTypes;
            viewModel.OrderTotal = SelectedOrder.OrderTotal;
            viewModel.TableId = SelectedOrder.TableNumber ?? 0;
            OpenWindow(_windowManager, viewModel, StringResource.Complete_Payment, 700, 500);
            if (viewModel.IsCompleted)
                this.TryClose();
        }
        public virtual void DoMergeOrder()
        {
            
        }


        public virtual void DoPrintOrder()
        {
            PrinterQueue.PrintOrder(SelectedOrder, GetPrinterSettings(_appScope.Settings), _appScope.ShopInfo, "Takeaway Receipt", WsPrintModeEnum.CloudRePrint, 0, 1);
        }


        public virtual void DoCancelOrder()
        {

            if (!_appScope.IsManager)
            {
                if (!VerifyMoreThanUserRights(_windowManager))
                    return;
            }
            _shopService.CancelOrderById(SelectedOrder);
            TryClose();
        }


        #endregion
    }
}