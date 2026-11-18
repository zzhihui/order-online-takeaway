using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.Services;
using Takeaway.Service.Contract.Entities.Shop;


namespace Takeaway.Admin.Wpf.ViewModels
{
    public interface IOrderHolderViewModel : IScreen
    {
        ObservableCollection<WsOrderHolder> OpenOrders { get; set; }

    }


    [OfflineViewModel]
    public class OrderHolderViewModel : BaseViewModel, IOrderHolderViewModel
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger("OrderHolderViewModel");

        #region Properties

        /// <summary>
        /// Open Orders
        /// </summary>
        private ObservableCollection<WsOrderHolder> _openOrders;
        public ObservableCollection<WsOrderHolder> OpenOrders
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
        /// Selected Order
        /// </summary>
        private WsOrderHolder _selectedOrder;
        public WsOrderHolder SelectedOrder
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

        #endregion

        #region Constructor

        private readonly IOfflineShopService _shopService;
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;

        public OrderHolderViewModel(IOfflineShopService shopService, IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            _shopService = shopService;
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            InitViewModel();
        }



        #endregion

        #region Methods

        private void InitViewModel()
        {
            Async(GetAllHoldingOrders);
        }

        private void GetAllHoldingOrders()
        {
            OpenOrders = new ObservableCollection<WsOrderHolder>(_shopService.GetHoldOrders());
        }

        public void OpenSelectedOrder()
        {
            if (SelectedOrder == null)
                return;

            var viewModel = IoC.Get<IPosViewModel>();
            viewModel.InitialViewModel();
            viewModel.MyOrder = SelectedOrder.Order;
            viewModel.InitOrder();
            _eventAggregator.Publish(viewModel, action => Task.Factory.StartNew(action));

        }



        #endregion

    }
}