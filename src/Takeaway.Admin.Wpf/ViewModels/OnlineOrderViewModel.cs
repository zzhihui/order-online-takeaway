using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Resources;
using Core.Common.Utility;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Features;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Shop;


namespace Takeaway.Admin.Wpf.ViewModels
{
    public interface IOnlineOrderViewModel : IBaseArchiveOrderViewModel
    {

    }

    [OfflineViewModel]
    public class OnlineOrderViewModel : BaseArchiveOrderViewModel, IOnlineOrderViewModel
    {
       

        #region Constructors


        public OnlineOrderViewModel(IOfflineShopService service, IWindowManager windowManager, IEventAggregator eventAggregator, IAppScope appScope, IOnlineService onlineService)

        {
            _onlineService = onlineService;
            _shopService = service;
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            _appScope = appScope;
            CalculatedOrders = new ObservableCollection<WsOrder>();
            Async(InitialViewModel);

        }


        #endregion

        #region Methods
        public override void GetOpenOrder()
        {
            var orders = _onlineService.GetOpenOrders();
            OpenOrders= new ObservableCollection<WsOrder>(orders);
            OrgOpenOrder = new ObservableCollection<WsOrder>(SerializeHelper.CloneJson(OpenOrders));
        }

        public override void OpenSelectedOrder()
        {
            if (SelectedOrder == null)
                return;

            var orderDetail = IoC.Get<IOnlineOrderDetailViewModel>();
            orderDetail.SelectedOrder = SelectedOrder;
            orderDetail.ParentPage = this;
            OpenWindow(_windowManager, orderDetail, StringResource.Order_Title);

            SelectedOrder = null;
            Async(GetOpenOrder);
        }

        public override void DoEditOrder(string guid)
        {
            //DO Nothing
        }


        public override void DoPayAllOrder()
        {
            //DO Nothing
        }



        #endregion


    }
}