using System;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Enum;


namespace Takeaway.Admin.Wpf.ViewModels
{


    public interface IUpdatePaymentViewModel : IBaseCompleteOrderViewModel
    {
     
    }


    [OfflineViewModel]
    public class UpdatePaymentViewModel : BaseCompleteOrderViewModel, IUpdatePaymentViewModel
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger("CompletePosOrderViewModel");


        #region Constructor

        public UpdatePaymentViewModel(IEventAggregator eventAggregator, IOfflineShopService service, IWindowManager windowManager, IAppScope appScope)
        {
            _eventAggregator = eventAggregator;
            _service = service;
            _windowManager = windowManager;
            _appScope = appScope;
            InitView();
        }

        #endregion

        #region Methods



        private void InitView()
        {
            PrintReceipt = true;
            PrintReceiptText = ResourcePrintReceipt;
            WithDot = false;
            CardPaymentVisible = false;
            CardPaymentVisible = false;
            VoucherPaymentVisible = false;
            ServiceChargeVisible = false;

        }


        public override void DoComplete()
        {
            Execute(() =>
            {


                Logger.DebugFormat("[DoComplete] Collect Order Information");
                if (SelectedPaymentType != null)
                    MyOrder.PaymentType = SelectedPaymentType.Name;
                MyOrder.Updated = DateTime.Now;
                MyOrder.AgentId = _appScope.CurrentAgent.Id;


                Logger.DebugFormat("[DoComplete] Save Order to Local DB");

                _service.SaveOrder(MyOrder);

                IsCompleted = true;

                Logger.DebugFormat("[DoComplete] Print Final Receipt");
                if (PrintReceipt)
                    DoReprint();


                TryClose();
            });
        }


        #endregion
        
    }
}