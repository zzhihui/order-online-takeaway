using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using ILog = log4net.ILog;
using LogManager = log4net.LogManager;

namespace Takeaway.Admin.Wpf.ViewModels
{

    public interface IOnlineOrderDetailViewModel : IBaseArchiveOrderDetailViewModel
    {

    }

    [OfflineViewModel]
    public class OnlineOrderDetailViewModel : BaseArchiveOrderDetailViewModel, IOnlineOrderDetailViewModel
    {

        private static readonly ILog Logger = LogManager.GetLogger("OnlineOrderDetailViewModel");


        #region Constructor

        public OnlineOrderDetailViewModel(IOnlineService onlineService,
                                          IOfflineShopService shopService,
                                          IAppScope appScope,
                                          IWindowManager windowManager)
        {
            _onlineService = onlineService;
            _shopService = shopService;
            _appScope = appScope;
            _windowManager = windowManager;

            InitApp();
        }
        #endregion

        #region Methods


        public override void DoCancelOrder()
        {

            if (!_appScope.IsManager)
            {
                if (!VerifyMoreThanUserRights(_windowManager))
                    return;
            }
            // _onlineService.CancelOrderById(SelectedOrder);
            TryClose();
        }


        public override void DoUpdateDriver()
        {
            //DO nothing
        }
        public override void DoUpdatePayment()
        {

            //DO nothing
        }


        #endregion

    }
}