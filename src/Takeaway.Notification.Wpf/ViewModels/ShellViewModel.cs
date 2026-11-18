using System;
using System.Diagnostics;
using System.IO;
using System.Security.RightsManagement;
using System.Windows;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Resources;
using Takeaway.Admin.Wpf.Utility;
using Takeway.Mobile.Core.Utilities;
using Action = System.Action;
using ILog = log4net.ILog;
using LogManager = log4net.LogManager;

namespace Takeaway.Admin.Wpf.ViewModels
{
    public interface IShell
    {
      
    }


    [OfflineViewModel]
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<IScreen>, IShell
    {
        private static readonly ILog Logger = LogManager.GetLogger("ShellViewModel");


        private readonly IEventAggregator _eventAggregator;
        private readonly IAppScope _appScope;
        private readonly IAppSettings _appSettings;


        public ShellViewModel(IEventAggregator eventAggregator,
                              IAppScope appScope,
                              IAppSettings appSettings)
        {
            _eventAggregator = eventAggregator;

            _appScope = appScope;
            _appSettings = appSettings;

            InitApp();

            Logger.InfoFormat("[ShellViewModel]  ActivateItem(ISplashViewModel);");
            ActivateItem(IoC.Get<ISplashViewModel>());

        }

        private void InitApp()
        {
            _eventAggregator.Subscribe(this);

            //Display UI
            InitUiControls();

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

        }

        public void Handle(IScreen message)
        {
            ActivateItem(message);
            Application.Current.MainWindow.Topmost = true;
            Application.Current.MainWindow.Activate();
            Application.Current.MainWindow.Focus();

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



        public void ShowVirtualKeyBoard()
        {
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.System) + Path.DirectorySeparatorChar + "osk.exe");
        }


        public void MenuAbout()
        {
            ActivateItem(IoC.Get<IAboutUsViewModel>());
        }


        public void MenuHome()
        {
            if (_appScope.CurrentAgent != null)
                ActivateItem(IoC.Get<IMainViewModel>());
        }


        public void MenuLogout()
        {
            _appScope.CurrentAgent = null;
            var viewModel = IoC.Get<ILoginViewModel>();
            viewModel.UserPin = string.Empty;
            ActivateItem(viewModel);


        }



    }
}