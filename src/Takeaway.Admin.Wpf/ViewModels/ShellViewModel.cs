using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Resources;
using Takeaway.Admin.Wpf.Features;
using Takeaway.Admin.Wpf.Utility;
using Takeway.Mobile.Core.Utilities;
using Action = System.Action;
using ILog = log4net.ILog;
using LogManager = log4net.LogManager;

namespace Takeaway.Admin.Wpf.ViewModels
{
    public interface IShell { }


    [OfflineViewModel]
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<IScreen>, IShell
    {
        private static readonly ILog Logger = LogManager.GetLogger("ShellViewModel");

        #region Properties

        private Visibility _toolbarVisibility;
        public Visibility ToolbarVisibility
        {
            get { return _toolbarVisibility; }
            set
            {
                _toolbarVisibility = value;
                NotifyOfPropertyChange(() => ToolbarVisibility);

            }
        }

        private bool _inCommingOrder;
        public bool InCommingOrder
        {
            get { return _inCommingOrder; }
            set
            {
                _inCommingOrder = value;
                NotifyOfPropertyChange(() => InCommingOrder);

            }
        }

        private bool _onlineFeature;
        public bool OnlineFeature
        {
            get
            {
                return this._onlineFeature;
            }
            set
            {
                if (value != this._onlineFeature)
                {
                    this._onlineFeature = value;
                    NotifyOfPropertyChange(() => OnlineFeature);
                }
            }
        }

        private bool _posFeature;
        public bool PosFeature
        {
            get
            {
                return this._posFeature;
            }
            set
            {
                if (value != this._posFeature)
                {
                    this._posFeature = value;
                    NotifyOfPropertyChange(() => PosFeature);
                }
            }
        }


        #endregion

        #region Constructor

        private readonly IEventAggregator _eventAggregator;
        private readonly IAppScope _appScope;
        private readonly IAppSettings _appSettings;
        private readonly IOnlineFeatureToggle _onlineFeatureToggle;
        private readonly IPosFeatureToggle _posFeatureToggle;

        public ShellViewModel(IEventAggregator eventAggregator,
                              IAppScope appScope,
                              IAppSettings appSettings,
                              IOnlineFeatureToggle onlineFeatureToggle,
                              IPosFeatureToggle posFeatureToggle)
        {
            _eventAggregator = eventAggregator;

            _appScope = appScope;
            _appSettings = appSettings;
            _onlineFeatureToggle = onlineFeatureToggle;

            _posFeatureToggle = posFeatureToggle;

            InitApp();

            Logger.InfoFormat("[ShellViewModel]  ActivateItem(ISplashViewModel);");
            ActivateItem(IoC.Get<ISplashViewModel>());

        }

        #endregion

        #region Methods

        private void InitApp()
        {
            _eventAggregator.Subscribe(this);

            //Display UI
            InitUiControls();

            //Setup Feature Toggle
            SetupFeatureToggle();

            Logger.InfoFormat("[InitApp]CheckCallerIdRunning");
            CheckCallerIdRunning();

        }

        public override void CanClose(Action<bool> callback)
        {

            var result = MessageBox.Show(StringResource.Shell_CloseText, StringResource.Shell_CloseTitle, MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes)
                return;

            base.CanClose(callback);
        }

        private void InitUiControls()
        {
            this.DisplayName = StringResource.Main_AppTitle;
            ToolbarVisibility = Visibility.Hidden;

        }
        private void SetupFeatureToggle()
        {
            OnlineFeature = _onlineFeatureToggle.FeatureEnabled;
            PosFeature = _posFeatureToggle.FeatureEnabled;
        }
        public void Handle(IScreen message)
        {
            ActivateItem(message);
            ToolbarVisibility = Visibility.Visible;
        }

        private void CheckCallerIdRunning()
        {
            Execute(() =>
            {
                var pname = Process.GetProcessesByName("callerId");
                if (pname.Length == 0)
                    StartCallerIdProcess();

            });
        }

        private void StartCallerIdProcess()
        {
            var path = _appSettings.GetSetting(AppSettingEnum.CallerIdExePath.ToString());
            Process.Start(path);
        }

        public void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("WPFViewModel:[Execute] {0}-{1}", ex.Message, ex.StackTrace);
            }
        }

        #region Menu

        public void ShowVirtualKeyBoard()
        {
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.System) + Path.DirectorySeparatorChar + "osk.exe");
        }

        public void MenuHome()
        {
            if (_appScope.CurrentAgent != null)
                ActivateItem(IoC.Get<IMainViewModel>());
            else
            {
                var viewModel = IoC.Get<IAboutUsViewModel>();
                ActivateItem(viewModel);
            }

        }


        public void MenuPos()
        {
            if (_appScope.CurrentAgent != null)
            {
                var viewModel = IoC.Get<IPosViewModel>();
                viewModel.InitialViewModel();
                viewModel.InitOrder();
                ActivateItem(viewModel);
            }
            else
            {
                var viewModel = IoC.Get<ILoginViewModel>();
                viewModel.UserPin = string.Empty;
                ActivateItem(viewModel);
            }

        }

        public void MenuEatIn()
        {
            if (_appScope.CurrentAgent != null)
                ActivateItem(IoC.Get<IEatInTableViewModel>());
            else
            {
                var viewModel = IoC.Get<ILoginViewModel>();
                viewModel.UserPin = string.Empty;
                ActivateItem(viewModel);
            }

        }

        public void MenuOrder()
        {
            //Hide new Order Icon
            if (_appScope.CurrentAgent != null)
            {
                InCommingOrder = false;
                ActivateItem(IoC.Get<IArchiveLocalOrderViewModel>());
            }
            else
            {
                var viewModel = IoC.Get<ILoginViewModel>();
                viewModel.UserPin = string.Empty;
                ActivateItem(viewModel);
            }

        }
        public void MenuHoldOrder()
        {
            //Hide new Order Icon
            if (_appScope.CurrentAgent != null)
            {
                InCommingOrder = false;
                ActivateItem(IoC.Get<IOrderHolderViewModel>());
            }
            else
            {
                var viewModel = IoC.Get<ILoginViewModel>();
                viewModel.UserPin = string.Empty;
                ActivateItem(viewModel);
            }

        }

        public void MenuOnline()
        {
            if (_appScope.CurrentAgent != null)
            {
                InCommingOrder = false;
                ActivateItem(IoC.Get<IOnlineOrderViewModel>());
            }
            else
            {
                var viewModel = IoC.Get<ILoginViewModel>();
                viewModel.UserPin = string.Empty;
                ActivateItem(viewModel);
            }

        }

        public void MenuTabletOrder()
        {
            //TODO: Using Tablet
        }

        public void MenuCustomer()
        {
            if (_appScope.CurrentAgent != null)
                ActivateItem(IoC.Get<ICustomerViewModel>());
        }

        public void MenuReport()
        {
            Process.Start(string.Format("{0}/report", Constants.SiteWebUrl));
        }

        public void MenuReportLocal()
        {
            if (_appScope.CurrentAgent != null)
            {
                ActivateItem(IoC.Get<IReportInternalViewModel>());
            }
            else
            {
                var viewModel = IoC.Get<ILoginViewModel>();
                viewModel.UserPin = string.Empty;
                ActivateItem(viewModel);
            }

        }

        public void MenuSetting()
        {

        }

        public void MenuHelp()
        {

            if (_appScope.CurrentAgent != null)
                ActivateItem(IoC.Get<IHelpViewModel>());
            else
            {
                var viewModel = IoC.Get<ILoginViewModel>();
                viewModel.UserPin = string.Empty;
                ActivateItem(viewModel);
            }
        }

        public void MenuAbout()
        {
            if (_appScope.CurrentAgent != null)
                ActivateItem(IoC.Get<IAboutUsViewModel>());
            else
            {
                var viewModel = IoC.Get<ILoginViewModel>();
                viewModel.UserPin = string.Empty;
                ActivateItem(viewModel);
            }
        }



        public void MenuLogout()
        {
            _appScope.CurrentAgent = null;
            var viewModel = IoC.Get<ILoginViewModel>();
            viewModel.UserPin = string.Empty;
            ActivateItem(viewModel);


        }


        public void MenuAdmin()
        {
            //TODO: Menu Admin
            //Either Admin Site or Client


        }

        #endregion

        #endregion

    }
}