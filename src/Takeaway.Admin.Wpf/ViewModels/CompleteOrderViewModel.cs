using System;
using System.Windows;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Resources;
using Newtonsoft.Json;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Enum;

using ILog = log4net.ILog;
using LogManager = log4net.LogManager;

namespace Takeaway.Admin.Wpf.ViewModels
{

    public interface ICompleteOrderViewModel : IBaseCompleteOrderViewModel, IScreen
    {

    }

    [OfflineViewModel]
    public class CompleteOrderViewModel : BaseCompleteOrderViewModel, ICompleteOrderViewModel
    {
        private static readonly ILog Logger = LogManager.GetLogger("CompleteOrderViewModel");

        #region Constructor

        public CompleteOrderViewModel(IEventAggregator eventAggregator, IOfflineShopService service, IWindowManager windowManager, IAppScope appScope, IAppSettings appSettings)
        {
            _eventAggregator = eventAggregator;
            _service = service;
            _windowManager = windowManager;
            _appScope = appScope;
            _appSettings = appSettings;
            InitView();
        }

        #endregion

        #region Methods

        public override void DoComplete()
        {
            Execute(() =>
            {

                Logger.DebugFormat("[DoComplete] Collect Order Information");
                if (SelectedPaymentType != null)
                    MyOrder.PaymentType = SelectedPaymentType.Name;

                //Set Order Date
                if (!MyOrder.Created.HasValue)
                    MyOrder.Created = DateTime.Now;

                    MyOrder.Updated = DateTime.Now;

                //Set Agent and comment
                FinaliseOrderCompleteInfo();

                //Set Order Ref
                CreateUniqueOrderRef();

                Logger.DebugFormat("[DoComplete] Save Order to Local DB");
                var orderSubmitted = _service.SaveOrder(MyOrder);
                if (orderSubmitted == null || orderSubmitted.Id == 0)
                {
                    MessageBox.Show(StringResource.POS_FailSubmit, StringResource.POS_FailSubmitTitle, MessageBoxButton.OK);
                    Logger.ErrorFormat("[DoComplete] Error Submit to Local DB: {0}", JsonConvert.SerializeObject(MyOrder));
                    return;
                }

                //Reset table
                Logger.DebugFormat("[DoComplete] Save Order to Local DB");
                ResetTable();

                IsCompleted = true;

                Logger.DebugFormat("[DoComplete] Print Final Receipt");
                if (PrintReceipt)
                    DoReprint();

                if (SelectedCashType())
                {
                    DoOpenCashDrawer();
                    //MessageBox.Show("Please give customer change", "Taking Cash Payment", MessageBoxButton.OK);
                }


                TryClose();
            });
        }

        private void FinaliseOrderCompleteInfo()
        {
            MyOrder.AgentId = _appScope.CurrentAgent.Id;
            MyOrder.Comment = FinalComment;
            if(!MyOrder.StatusId.HasValue)
                MyOrder.StatusId = (int)WsOrderStatusIdEnum.Open;
            MyOrder.IsInternalOrder = true;
            if(!MyOrder.OrderSource.HasValue)
            MyOrder.OrderSource = (int) WsOrderSourceEnum.Shop;
        }

        #endregion

    }
}