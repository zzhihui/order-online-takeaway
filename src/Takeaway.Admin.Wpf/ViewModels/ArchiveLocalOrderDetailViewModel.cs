using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Resources;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using ILog = log4net.ILog;
using LogManager = log4net.LogManager;

namespace Takeaway.Admin.Wpf.ViewModels
{

    public interface IArchiveLocalOrderDetailViewModel : IBaseArchiveOrderDetailViewModel
    {


    }
    [OfflineViewModel]
    public class ArchiveLocalOrderDetailViewModel : BaseArchiveOrderDetailViewModel, IArchiveLocalOrderDetailViewModel
    {

        private static readonly ILog Logger = LogManager.GetLogger("ArchiveLocalOrderDetailViewModel");

        #region Constructor


        public ArchiveLocalOrderDetailViewModel(IOfflineShopService shopService,
                                           IAppScope appScope,
                                           IWindowManager windowManager)
        {
            _shopService = shopService;
            _appScope = appScope;
            _windowManager = windowManager;

            InitApp();
        }
        #endregion

        #region Methods
        


        #endregion
    }
}