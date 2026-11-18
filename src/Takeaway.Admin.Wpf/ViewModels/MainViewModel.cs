using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Features;
using Takeaway.Admin.Wpf.Utility;
using Takeway.Mobile.Core.Utilities;

namespace Takeaway.Admin.Wpf.ViewModels
{
    public interface IMainViewModel : IScreen
    {

    }

    [OfflineViewModel]
    public class MainViewModel : BaseViewModel, IMainViewModel
    {

        #region Constructor

        private readonly IEventAggregator _eventAggregator;
        private readonly IAppScope _appScope;
        private readonly IPosFeatureToggle _posFeatureToggle;
        private readonly IOnlineFeatureToggle _onlineFeatureToggle;
        private readonly IWindowManager _windowManager;


        public MainViewModel(IEventAggregator eventAggregator,
                             IAppScope appScope,
                             IPosFeatureToggle posFeatureToggle,
                             IOnlineFeatureToggle onlineFeatureToggle,
                             IWindowManager windowManager)
        {
            _eventAggregator = eventAggregator;
            _appScope = appScope;
            _posFeatureToggle = posFeatureToggle;
            _onlineFeatureToggle = onlineFeatureToggle;
            _windowManager = windowManager;

            OnlineFeature = _onlineFeatureToggle.FeatureEnabled;
            PosFeature = _posFeatureToggle.FeatureEnabled;


        }


        #endregion

        #region Methods

        public void TilePos()
        {

            var viewModel = IoC.Get<IPosViewModel>();
            viewModel.InitialViewModel();
            viewModel.InitOrder();
            _eventAggregator.Publish(viewModel, action => Task.Factory.StartNew(action));
        }

        public void OrderOnline()
        {
            _eventAggregator.Publish(IoC.Get<IOnlineOrderViewModel>(), action => Task.Factory.StartNew(action));
        }

        public void AboutUs()
        {
            _eventAggregator.Publish(IoC.Get<IAboutUsViewModel>(), action => Task.Factory.StartNew(action));
        }

        public void Help()
        {
            _eventAggregator.Publish(IoC.Get<IHelpViewModel>(), action => Task.Factory.StartNew(action));
        }

        public void ReportMenu()
        {
            //if (!_appScope.IsManager)
            //{
            //    if (!VerifyMoreThanUserRights(_windowManager))
            //        return;
            //}
            _eventAggregator.Publish(IoC.Get<IReportInternalViewModel>(), action => Task.Factory.StartNew(action));
        }

        public void Setting()
        {

        }

        public void EatIn()
        {
            _eventAggregator.Publish(IoC.Get<IEatInTableViewModel>(), action => Task.Factory.StartNew(action));
        }

        public void MenuAdmin()
        {

        }

        public void MenuReport()
        {
            Process.Start(string.Format("{0}/report", Constants.SiteWebUrl));
        }

        #endregion
    }
}