using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;

namespace Takeaway.Admin.Wpf.ViewModels
{

    public interface ISplashViewModel : IScreen
    {

    }

    [OfflineViewModel]
    public class SplashViewModel : BaseViewModel, ISplashViewModel
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger("SplashViewModel");


        #region Properties

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
        #endregion

        #region Constructor

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

        #endregion

        #region Methods
       
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
                LoadTableCommands(50);
                LoadPaymentTypes(50);
                LoadDrivers(50);
                LoadCategories(60);
                LoadMenuforCategory(60);
                LoadHotItems(70);
                LoadOptionItems(70);
                LoadDeliveryOptions(70);
                LoadDeliveryCost(70);


                //Delete orders that is more than 7 days old
                CleanOldLocalOrders(90);
                CleanCallerIds(90);

                //Finalising
            }
            , () =>
            {
                if (!canOpen)
                    App.Current.Shutdown(0);
                _eventAggregator.Publish(IoC.Get<ILoginViewModel>(), action => Task.Factory.StartNew(action));

            });
        }

        private void CleanCallerIds(int progressValue)
        {
            Execute(() => {

                ProgressText = "Clean Local Data";
                ProgressValue = progressValue;


                _offlineShopService.CleanCallerIdsMoreThan7Days();
                Thread.Sleep(1 * 100);
            });
        }

        private void CleanOldLocalOrders(int progressValue)
        {
            Execute(() => { 

            ProgressText = "Clean Local Data";
            ProgressValue = progressValue;

            
            _offlineShopService.CleanOrderMoreThan7Days();
            Thread.Sleep(1 * 100);
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

        private void LoadDeliveryCost(int progressValue)
        {
            ProgressText = "Loading Delivery Cost...";
            ProgressValue = progressValue;

            _appScope.FullDeliveryCost = _offlineShopService.GetDeliveryCosts();
            Thread.Sleep(1 * 100);
        }

        private void LoadDeliveryOptions(int progressValue)
        {
            ProgressText = "Loading Delivery options...";
            ProgressValue = progressValue;

            _appScope.FullDeliveryOptions = _offlineShopService.GetDeliveryOptions();

            Thread.Sleep(1 * 100);
        }

        private void LoadOptionItems(int progressValue)
        {
            ProgressText = "Loading Option Items...";
            ProgressValue = progressValue;

            _appScope.FulllOptionItems = _offlineShopService.GetOptionItems();

            Thread.Sleep(1 * 100);
        }

        private void LoadHotItems(int progressValue)
        {
            ProgressText = "Loading Hot Items...";
            ProgressValue = progressValue;

            var externalIds = _appScope.FullCategories.Where(r => r.CategoryGroupId == (int)MenuItemCategoryGroupEnum.External).Select(r => r.Id).ToList();
            var internalIds = _appScope.FullCategories.Where(r => r.CategoryGroupId == (int)MenuItemCategoryGroupEnum.InternalStandard).Select(r => r.Id).ToList();

            _appScope.FullHotItemsExternal = _appScope.FullCategoryItems.Where(r => r.Ishot.HasValue && r.Ishot.Value && externalIds.Contains(r.MenuItemCategoryId.Value)).ToList();
            _appScope.FullHotItemsInternal = _appScope.FullCategoryItems.Where(r => r.Ishot.HasValue && r.Ishot.Value && internalIds.Contains(r.MenuItemCategoryId.Value)).ToList();

            Thread.Sleep(1 * 100);
        }

        private void LoadMenuforCategory(int progressValue)
        {
            ProgressText = "Loading Menus for Categories...";
            ProgressValue = progressValue;
            _appScope.FullCategoryItems = _offlineShopService.GetAllMenuItems();

            Thread.Sleep(1 * 100);
        }

        private void LoadCategories(int progressValue)
        {
            ProgressText = "Loading Categories...";
            ProgressValue = progressValue;
            _appScope.FullCategories = _offlineShopService.GetAllMenuCategory();
            Thread.Sleep(1 * 100);
        }

        private void LoadDrivers(int progressValue)
        {
            ProgressText = "Loading All Drivers...";
            ProgressValue = progressValue;
            _appScope.AllDrivers = _offlineShopService.GetAllDrivers();
            Thread.Sleep(1 * 100);
        }

        private void LoadPaymentTypes(int progressValue)
        {
            ProgressText = "Loading Payment Types...";
            ProgressValue = progressValue;
            _appScope.PaymentTypes = _offlineShopService.GetAllPaymentTypes();
            Thread.Sleep(1 * 100);
        }

        private void LoadTableCommands(int progressValue)
        {
            ProgressText = "Loading Table Commands...";
            ProgressValue = progressValue;
            _appScope.TableCommandList = _offlineShopService.GetTableCommand();
            Thread.Sleep(1 * 100);
        }

        private void LoadLocalSettings(int progressValue)
        {
            ProgressText = "Loading All Settings...";
            ProgressValue = progressValue;
            _appScope.Settings = _offlineShopService.GetSystemSettings();
            Thread.Sleep(1 * 100);
        }

        #endregion
    }
}