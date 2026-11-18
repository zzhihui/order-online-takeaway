using System.Collections.ObjectModel;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Resources;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Features;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Admin.Wpf.ViewModels
{

    public interface IPosViewModel : IScreen
    {
        int TableId { get; set; }
        int BatchId { get; set; }
        WsOrder MyOrder { get; set; }
        ObservableCollection<WsTableCommand> SelectedTableCommandsList { get; set; }
        void InitialViewModel();
        void InitOrder();
        void CollectOrderInformation();

    }

    [OfflineViewModel]
    public class PosViewModel : BasePosViewModel, IPosViewModel
    {
        #region Constructor

        public PosViewModel(IEventAggregator eventAggregator,
                            IOfflineShopService service,
                            IWindowManager windowManager,
                            IAppScope appScope,
                            IAddressLookupAutoCompleteFeatureToggle addressLookupAutoCompleteFeatureToggle)
        {
            _eventAggregator = eventAggregator;
            _service = service;
            _windowManager = windowManager;
            _appScope = appScope;
            _addressLookupAutoCompleteFeatureToggle = addressLookupAutoCompleteFeatureToggle;
           
        }

        #endregion

        #region Methods

        public void InitialViewModel()
        {
            MyOrder = new WsOrder();
            InitCategory();
            InitDeliveryOptions();
            LoadTableCommandList();
            InitOptionItems();
            GetPaymentTypes();
        }

        public void InitOrder()
        {
            ControlFlag = true;
            if (MyOrder.NumberOfPeople.HasValue)
                NumberOfPeople = MyOrder.NumberOfPeople.Value;

            CustomerName = MyOrder.ContactName;
            CustomerEmail = MyOrder.Email;
            CustomerPhone = MyOrder.PhoneNumber;
            CustomerAddress = MyOrder.Address;

            if(TableId==0)
            SelectedDeliveryOptionDisplay = GetDeliveryOptionDisplay();

            if (MyOrder.DeliveryCharge > 0)
                CustomerDeliveryCharge = MyOrder.DeliveryCharge;
            DeliveryCharge = MyOrder.DeliveryCharge;

            DiscountAmount = MyOrder.OrderOriginalTotal - MyOrder.OrderTotal;

            OrderItems = new ObservableCollection<WsMenuItem>(MyOrder.OrderItems);

            ControlFlag = false;

            CalculateTotal();
        }

       
        public override void DoCompleteOrder()
        {

            CollectOrderInformation();

             //Show Check out Form
            var viewModel = IoC.Get<ICompleteOrderViewModel>();
            viewModel.MyOrder = MyOrder;
            viewModel.PaymentTypes = PaymentTypes;
            viewModel.OrderTotal = OrderTotal;
            viewModel.TableId = TableId;
            OpenWindow(_windowManager, viewModel, StringResource.Complete_Payment, 700, 500);
            if (viewModel.IsCompleted)
                ResetOrder();

        }

        #endregion

    }
}