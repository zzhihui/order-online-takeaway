using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using Core.Common.Utility;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;

namespace Takeaway.Admin.Wpf.ViewModels
{

    public interface IBaseArchiveOrderViewModel : IScreen
    {
        
    }


    public abstract class BaseArchiveOrderViewModel : BaseViewModel, IBaseArchiveOrderViewModel
    {
        protected IOnlineService _onlineService;
        protected IOfflineShopService _shopService;
        protected  IWindowManager _windowManager;
        protected  IEventAggregator _eventAggregator;
        protected IAppScope _appScope;

        #region Properties

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

        private ObservableCollection<WsOrder> _calculatedOrders;
        public ObservableCollection<WsOrder> CalculatedOrders
        {
            get
            {
                return this._calculatedOrders;
            }
            set
            {
                if (value == this._calculatedOrders) return;
                this._calculatedOrders = value;
                NotifyOfPropertyChange(() => CalculatedOrders);
            }
        }

        private WsOrder _selectedCalculatedOrders;
        public WsOrder SelectedCalculatedOrders
        {
            get
            {
                return this._selectedCalculatedOrders;
            }
            set
            {
                if (value == this._selectedCalculatedOrders) return;
                this._selectedCalculatedOrders = value;
                NotifyOfPropertyChange(() => SelectedCalculatedOrders);
            }
        }

        private ObservableCollection<WsOrder> _orgOpenOrder;
        public ObservableCollection<WsOrder> OrgOpenOrder
        {
            get
            {
                return this._orgOpenOrder;
            }
            set
            {
                if (value == this._orgOpenOrder) return;
                this._orgOpenOrder = value;
                NotifyOfPropertyChange(() => OrgOpenOrder);
            }
        }

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
                NotifyOfPropertyChange(() => SelectedOrder);
            }
        }

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

        private string _searchOrderRef;
        public string SearchOrderRef
        {
            get
            {
                return this._searchOrderRef;
            }
            set
            {
                if (value == this._searchOrderRef) return;
                this._searchOrderRef = value;
                NotifyOfPropertyChange(() => SearchOrderRef);
            }
        }


        private string _searchOrderRefs;
        public string SearchOrderRefs
        {
            get
            {
                return this._searchOrderRefs;
            }
            set
            {
                if (value == this._searchOrderRefs) return;
                this._searchOrderRefs = value;
                NotifyOfPropertyChange(() => SearchOrderRefs);
            }
        }

        private string _searchOrderPhone;
        public string SearchOrderPhone
        {
            get
            {
                return this._searchOrderPhone;
            }
            set
            {
                if (value == this._searchOrderPhone) return;
                this._searchOrderPhone = value;
                NotifyOfPropertyChange(() => SearchOrderPhone);
            }
        }


        private decimal _calculatedOrderTotal;
        public decimal CalculatedOrderTotal
        {
            get
            {
                return this._calculatedOrderTotal;
            }
            set
            {
                if (value == this._calculatedOrderTotal) return;
                this._calculatedOrderTotal = value;
                NotifyOfPropertyChange(() => CalculatedOrderTotal);
            }
        }

        #endregion

        #region Methods

        public abstract void GetOpenOrder();
        public abstract void OpenSelectedOrder();
        public abstract void DoEditOrder(string guid);
       

        protected void InitialViewModel()
        {
            GetOpenOrder();
            GetAllDrivers();
            GetPaymentTypes();
        }

        private void GetPaymentTypes()
        {
            PaymentTypes = new ObservableCollection<WsPaymentType>(_appScope.PaymentTypes);
        }

        private void GetAllDrivers()
        {
            Drivers = new ObservableCollection<WsDriver>(_appScope.AllDrivers);
        }


        public void DoSearchOrder()
        {
            if (!string.IsNullOrEmpty(SearchOrderRef))
                OpenOrders = new ObservableCollection<WsOrder>(OrgOpenOrder.Where(r => r.OrderRef.ToUpper().Contains(SearchOrderRef.ToUpper())).ToList());
            else if (!string.IsNullOrEmpty(SearchOrderPhone))
                OpenOrders = new ObservableCollection<WsOrder>(OrgOpenOrder.Where(r => r.PhoneNumber == SearchOrderPhone).ToList());
            else
                GetOpenOrder();
        }


        public void DoUnPaidOrder()
        {
            OpenOrders = new ObservableCollection<WsOrder>(OrgOpenOrder.Where(r => string.IsNullOrEmpty(r.PaymentType)).ToList());
        }

        public void DoClearSearch()
        {
            SearchOrderRef = string.Empty;
            SearchOrderPhone = string.Empty;
            Async(GetOpenOrder);
        }

        public void DoAddToCalc(string guid)
        {
            if (string.IsNullOrEmpty(guid) || OpenOrders == null)
                return;

            var order = OpenOrders.FirstOrDefault(r => r.Guid == guid);
            if (order == null)
                return;

            if (CalculatedOrders.FirstOrDefault(r => r.Guid == guid) == null)
            {
                CalculatedOrders.Add(SerializeHelper.CloneJson(order));
                CalculateSubTotals(CalculatedOrders);
            }


        }

        private void CalculateSubTotals(ObservableCollection<WsOrder> calculatedOrders)
        {
            CalculatedOrderTotal = calculatedOrders.Sum(r => r.OrderTotal);
        }


        public void DoRemoveSelectOrder()
        {
            if (SelectedCalculatedOrders == null)
                return;

            CalculatedOrders.Remove(SelectedCalculatedOrders);
            CalculateSubTotals(CalculatedOrders);
        }

        public void DoClearAllSelectedOrder()
        {
            CalculatedOrders = new ObservableCollection<WsOrder>();
            CalculateSubTotals(CalculatedOrders);

        }


        public virtual void DoPayAllOrder()
        {
            foreach (var order in CalculatedOrders)
            {
                order.PaymentType = WsPaymentTypeEnum.Cash.ToString();
                order.Updated = DateTime.Now;
                _shopService.SaveOrder(order);
            }
            GetOpenOrder();

        }


        public void DoLocalOrder()
        {
            if (string.IsNullOrEmpty(SearchOrderRefs))
                return;

            var orderList=SearchOrderRefs.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            CalculatedOrders= new ObservableCollection<WsOrder>(OrgOpenOrder.Where(r => orderList.Contains(r.OrderRef)).ToList());
            CalculateSubTotals(CalculatedOrders);
        }



        #endregion

    }
}