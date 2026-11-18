using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Enum;
using ILog = log4net.ILog;
using LogManager = log4net.LogManager;

namespace Takeaway.Admin.Wpf.ViewModels
{

    public interface ISplashViewModel : IScreen
    {

    }

    [OfflineViewModel]
    public class SplashViewModel : BaseViewModel, ISplashViewModel
    {
        private static readonly ILog Logger = LogManager.GetLogger("SplashViewModel");


        private string _progressText;
        public string ProgressText
        {
            get { return _progressText; }
            set
            {
                _progressText = value;
                NotifyOfPropertyChange(() => ProgressText);

            }
        }

        private int _progressValue;
        public int ProgressValue
        {
            get { return _progressValue; }
            set
            {
                _progressValue = value;
                NotifyOfPropertyChange(() => ProgressValue);

            }
        }

        private readonly IEventAggregator _eventAggregator;
        private readonly IAppScope _appScope;
        private readonly IOfflineShopService _offlineShopService;
        private readonly IOnlineService _onlineService;

        public SplashViewModel(IEventAggregator eventAggregator,
                               IAppScope appScope,
                               IOfflineShopService offlineShopService,
                               IOnlineService onlineService)
        {
            _eventAggregator = eventAggregator;
            _appScope = appScope;
            _offlineShopService = offlineShopService;
            _onlineService = onlineService;
        }


        public void InitApp()
        {
            var canOpen = true;
            var internetAvailable = true;

            Async(() =>
            {


                if (!CheckinternetConnection())
                {
                    Logger.WarnFormat("Internet Connection Failed");
                    internetAvailable = false;
                }
                else
                {
                    //Check Licences 
                    if (!CheckLicence())
                    {
                        Logger.WarnFormat("Failed Licence Check");
                        canOpen = false;
                        return;
                    }
                }
                //Loading Lookup Lists
                LoadShopInfo(20);
                LoadUser(30);
                LoadLocalSettings(40);
               
                

                //Finalising
            }
            , () =>
            {
                if (!canOpen)
                    App.Current.Shutdown(0);
                _eventAggregator.Publish(IoC.Get<ILoginViewModel>(), action => Task.Factory.StartNew(action));

            });
        }

      

        private bool CheckinternetConnection()
        {
            ProgressText = "Check internet Connection...";
            ProgressValue = 10;
            Thread.Sleep(1 * 100);

            try
            {
                using (var client = new WebClient())
                {
                    var website = ConfigurationManager.AppSettings["ShopUrl"];

                    using (var stream = client.OpenRead(website))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }


        }

        private bool CheckLicence()
        {
            var shopInfo = _onlineService.GetShopInfo();
            return shopInfo != null && shopInfo.Isactive.HasValue && shopInfo.Isactive.Value;
        }


        private void LoadUser(int progressValue)
        {
            ProgressText = "Loading Shop Info...";
            ProgressValue = progressValue;

            _appScope.AllUsers = _offlineShopService.GetAllUsers();
            Thread.Sleep(1 * 100);
        }


        private void LoadShopInfo(int progressValue)
        {
            ProgressText = "Loading Shop Info...";
            ProgressValue = progressValue;

            _appScope.ShopInfo = _offlineShopService.GetShopInfo();
            Thread.Sleep(1 * 100);
        }


        private void LoadLocalSettings(int progressValue)
        {
            ProgressText = "Loading All Settings...";
            ProgressValue = progressValue;
            _appScope.Settings = _offlineShopService.GetSystemSettings();
            Thread.Sleep(1 * 100);
        }


    }
}