using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Core.Common.Resources;
using log4net.Config;

namespace Takeaway.Admin.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger("App");
        public App()
        {

            CheckSingleInstance();

            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
            XmlConfigurator.Configure();
        }

        private static void CheckSingleInstance()
        {
            var proc = Process.GetCurrentProcess();
            var count = Process.GetProcesses().Count(p => p.ProcessName == proc.ProcessName);
            if (count <= 1) return;
            MessageBox.Show("Already an instance is running, please wait...");
            App.Current.Shutdown();
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = string.Format(StringResource.App_Exception, e.Exception.Message);
            MessageBox.Show(errorMessage, StringResource.App_ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Error);

            Logger.ErrorFormat("{0}:-{1}", e.Exception.Message,e.Exception.StackTrace);

            e.Handled = true;
            Current.Shutdown();
        }
    }
}
