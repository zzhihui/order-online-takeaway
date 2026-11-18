using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Utility;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;

namespace Takeaway.Admin.Wpf.ViewModels
{
    public interface IMainViewModel : IScreen
    {

    }

    [OfflineViewModel]
    public class MainViewModel : BaseViewModel, IMainViewModel
    {

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


        private Visibility _newOrderVisibility;
        public Visibility NewOrderVisibility
        {
            get { return _newOrderVisibility; }
            set
            {
                _newOrderVisibility = value;
                NotifyOfPropertyChange(() => NewOrderVisibility);

            }
        }



        private readonly IEventAggregator _eventAggregator;
        private readonly IAppScope _appScope;
        private readonly IWindowManager _windowManager;
        private DispatcherTimer _orderDispatcherTimer;
        private readonly IOnlineService _onlineService;
        private readonly IOfflineShopService _offlineShopService;
        private SoundPlayer _sourPlayer;

        public MainViewModel(IEventAggregator eventAggregator,
                             IAppScope appScope,
                             IWindowManager windowManager,
                             IOnlineService onlineService,
                             IOfflineShopService offlineShopService)
        {
            _eventAggregator = eventAggregator;
            _appScope = appScope;
            _windowManager = windowManager;
            _onlineService = onlineService;
            _offlineShopService = offlineShopService;
            _sourPlayer = new SoundPlayer(AppResource.Alarm01);
            NewOrderVisibility = Visibility.Hidden;
            StartOrderCheckTimer();

            LoadNewOrders();


        }

        public override void CanClose(Action<bool> callback)
        {
            if (_orderDispatcherTimer != null)
                _orderDispatcherTimer.Stop();
            callback(true); // will cancel close
        }

        private void StartOrderCheckTimer()
        {
            _orderDispatcherTimer = new DispatcherTimer(DispatcherPriority.Background, Application.Current.Dispatcher);
            _orderDispatcherTimer.Interval = TimeSpan.FromSeconds(2 * 60);
            _orderDispatcherTimer.Tick += CheckNewOrderTick;
            _orderDispatcherTimer.Start();
        }

        private void CheckNewOrderTick(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new System.Action(LoadNewOrders));
        }


        public void DoClearSearch()
        {
            SearchOrderRef = string.Empty;
            SearchOrderPhone = string.Empty;
            LoadNewOrders();
        }


        public void DoSearchOrder()
        {

            if (!string.IsNullOrEmpty(SearchOrderRef))
                OpenOrders = new ObservableCollection<WsOrder>(_appScope.LocalOrders.Where(r => r.OrderRef.ToUpper().Contains(SearchOrderRef.ToUpper())).ToList());
            else if (!string.IsNullOrEmpty(SearchOrderPhone))
                OpenOrders = new ObservableCollection<WsOrder>(_appScope.LocalOrders.Where(r => r.PhoneNumber == SearchOrderPhone).ToList());
            else
                LoadNewOrders();
        }



        public void OpenSelectedOrder()
        {
            if (SelectedOrder == null)
                return;

            NewOrderVisibility = Visibility.Hidden;
            _sourPlayer.Stop();


            var orderDetail = IoC.Get<IOrderDetailViewModel>();
            orderDetail.SelectedOrder = SelectedOrder;
            OpenWindow(_windowManager, orderDetail, "Order Detail");

            SelectedOrder = null;

            Async(LoadNewOrders);


        }

        private void LoadNewOrders()
        {


            //Get Open Order
            var order = _onlineService.GetOpenOrders();
            if (order == null || !order.Any())
                return;

            //set order
            _appScope.LocalOrders = order;
            OpenOrders = new ObservableCollection<WsOrder>(_appScope.LocalOrders);

            //Get Max Order
            var offlineSettings = _offlineShopService.GetOfflineSettings();
            var localMaxOrder = offlineSettings.FirstOrDefault(r => r.Name == WsOfflineSettingEnum.AppMaxOrderDestop);
            var currentMax = 0;
            if (localMaxOrder != null)
                currentMax = IntegerStringUtil.ConvertStringValue(localMaxOrder.SettingValue);

            var maxOrderId = order.Max(r => r.Id);

            //Check if new Order
            if (maxOrderId <= currentMax) return;

            NewOrderVisibility = Visibility.Visible;
            _offlineShopService.SaveSettingByKey(WsOfflineSettingEnum.AppMaxOrderDestop, maxOrderId.ToString());
            _sourPlayer.PlayLooping();


            _eventAggregator.Publish(this, action => Task.Factory.StartNew(action));

        }
    }
}