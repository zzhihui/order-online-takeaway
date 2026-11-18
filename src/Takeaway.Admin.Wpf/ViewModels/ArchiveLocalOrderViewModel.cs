using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Resources;
using Core.Common.Utility;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Shop;
using ILog = log4net.ILog;
using LogManager = log4net.LogManager;

namespace Takeaway.Admin.Wpf.ViewModels
{
    public interface IArchiveLocalOrderViewModel : IBaseArchiveOrderViewModel
    {
        
    }

    [OfflineViewModel]
    public class ArchiveLocalOrderViewModel : BaseArchiveOrderViewModel, IArchiveLocalOrderViewModel
    {
        private static readonly ILog Logger = LogManager.GetLogger("ArchiveLocalOrderViewModel");

        #region Constructors

        public ArchiveLocalOrderViewModel(IOfflineShopService service, IWindowManager windowManager, IEventAggregator eventAggregator, IAppScope appScope)

        {
            _shopService = service;
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            _appScope = appScope;
            CalculatedOrders = new ObservableCollection<WsOrder>();
            Async(InitialViewModel);

        }

        #endregion

        #region Methods


        #endregion

        public override void GetOpenOrder()
        {
            var searchFilter = new WsOrderSearch();

            var currentDate = DateTime.Now;
            var yesterday = DateTime.Now.AddDays(-1);

            if (DateTime.Now.Hour <= 7)
                searchFilter.SearchDate = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day);
            else
                searchFilter.SearchDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);

            OpenOrders = new ObservableCollection<WsOrder>(_shopService.GetCompleteOrders(searchFilter) ?? new List<WsOrder>());
            OrgOpenOrder = new ObservableCollection<WsOrder>(SerializeHelper.CloneJson(OpenOrders));
        }

        public override void OpenSelectedOrder()
        {
            if (SelectedOrder == null)
                return;

            var orderDetail = IoC.Get<IArchiveLocalOrderDetailViewModel>();
            orderDetail.SelectedOrder = SelectedOrder;
            orderDetail.ParentPage = this;
            orderDetail.Drivers = Drivers;
            orderDetail.OpenOrders = OrgOpenOrder;
            orderDetail.PaymentTypes = PaymentTypes;
            OpenWindow(_windowManager, orderDetail, StringResource.Order_Title);
          

            SelectedOrder = null;
            Async(GetOpenOrder);
        }

        public override void DoEditOrder(string guid)
        {
            if (string.IsNullOrEmpty(guid) || OpenOrders == null)
                return;

            var order = OpenOrders.FirstOrDefault(r => r.Guid == guid);
            if (order == null)
                return;

            var viewModel = IoC.Get<IPosViewModel>();
            if (order.TableNumber.HasValue)
                viewModel.TableId = order.TableNumber.Value;
            viewModel.InitialViewModel();
            viewModel.MyOrder = order;
            viewModel.InitOrder();
            _eventAggregator.Publish(viewModel, action => Task.Factory.StartNew(action));
        }
    }
}