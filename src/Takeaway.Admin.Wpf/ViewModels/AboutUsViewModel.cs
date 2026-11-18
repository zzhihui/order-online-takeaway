using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Net4.Utility;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;
using ILog = log4net.ILog;
using LogManager = log4net.LogManager;

namespace Takeaway.Admin.Wpf.ViewModels
{
    public interface IAboutUsViewModel : IScreen
    {

    }

    [OfflineViewModel]
    public class AboutUsViewModel : BaseViewModel, IAboutUsViewModel
    {
        private static readonly ILog Logger = LogManager.GetLogger("AboutUsViewModel");

        #region Properties

        /// <summary>
        /// Version number
        /// </summary>
        private string _versionNumber;
        public string VersionNumber
        {
            get { return _versionNumber; }
            set
            {
                _versionNumber = value;
                NotifyOfPropertyChange(() => VersionNumber);

            }
        }


        /// <summary>
        /// HWID
        /// </summary>
        private string _hwid;
        public string Hwid
        {
            get { return _hwid; }
            set
            {
                _hwid = value;
                NotifyOfPropertyChange(() => Hwid);

            }
        }


        /// <summary>
        /// Email
        /// </summary>
        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);

            }
        }


        /// <summary>
        /// Contact Phone number
        /// </summary>
        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                NotifyOfPropertyChange(() => PhoneNumber);

            }
        }


        #endregion

        #region Constructors

        private readonly IAppScope _appScope;
        private readonly IOnlineService _onlineService;
        private readonly IOfflineShopService _offlineShopService;
        public AboutUsViewModel(IAppScope appScope, IOnlineService onlineService, IOfflineShopService offlineShopService)
        {
            _appScope = appScope;
            _onlineService = onlineService;
            _offlineShopService = offlineShopService;

            InitView();
        }

        private void InitView()
        {
            Logger.DebugFormat("[InitView] GetSystemSettings from Cloud");

            Async(() =>
            {

                var email = GetSettingByKey(WsOfflineSettingEnum.IndaSoftEmail, _appScope.Settings);
                var phoneNumber = GetSettingByKey(WsOfflineSettingEnum.IndaSoftPhone, _appScope.Settings);
                VersionNumber = typeof(ShellViewModel).Assembly.GetName().Version.ToString();

                Hwid = ProductLib.Generate();
                Email = email != null ? email.SettingValue : "info:indasoft.ie";
                PhoneNumber = phoneNumber != null ? phoneNumber.SettingValue : "0874414099";

            });
        }

        #endregion


        public void DoUpdate()
        {
            Async(() =>
            {
                SyncShopInfo();
                var categories = SyncMenuItemCategory();
                SyncMenuItems(categories);
                SyncSetting();
                SyncDeliveryCost();
                SyncDeliveryOption();
                SyncDriver();
                SyncOptionItems();
                SyncPaymentType();
                SyncShopUser();
                SyncTableAlias();
                SyncTableCommand();

            }, () => { Application.Current.Shutdown(0);});
        }


        private void SyncTableCommand()
        {
            var lists = _onlineService.GetTableCommand();
            if (lists == null || lists.Count == 0)
            {
                Logger.ErrorFormat("Fail to SyncTableCommand");
                return;
            }

            _offlineShopService.SyncTableCommand(lists);

        }

        private void SyncTableAlias()
        {
            var lists = _onlineService.GetTableAlias();
            if (lists == null || lists.Count == 0)
            {
                Logger.ErrorFormat("Fail to SyncTableAlias");
                return;
            }
            _offlineShopService.SyncTableAlias(lists);
        }

        private void SyncShopUser()
        {

            var lists = _onlineService.GetShopUsers();
            if (lists == null || lists.Count == 0)
            {
                Logger.ErrorFormat("Fail to SyncTableAlias");
                return;
            }
            _offlineShopService.SyncShopUsers(lists);
        }

        private void SyncPaymentType()
        {

            var lists = _onlineService.GetPaymentTypes();
            if (lists == null || lists.Count == 0)
            {
                Logger.ErrorFormat("Fail to SyncPaymentType");
                return;
            }
            _offlineShopService.SyncPaymentTypes(lists);
        }

        private void SyncOptionItems()
        {

            var lists = _onlineService.GetOptionItems();
            if (lists == null || lists.Count == 0)
            {
                Logger.ErrorFormat("Fail to SyncOptionItems");
                return;
            }

            _offlineShopService.SyncOptionItems(lists);
        }

        private void SyncSetting()
        {

            var lists = _onlineService.GetSettings();
            if (lists == null || lists.Count == 0)
            {
                Logger.ErrorFormat("Fail to SyncSetting");
                return;
            }
            _offlineShopService.SyncSettings(lists);
        }

        private void SyncDriver()
        {

            var lists = _onlineService.GetDrivers();
            if (lists == null || lists.Count == 0)
            {
                Logger.ErrorFormat("Fail to SyncDriver");
                return;
            }
            _offlineShopService.SyncDrivers(lists);
        }

        private void SyncDeliveryOption()
        {
            var lists = _onlineService.GetDeliveryOptions();
            if (lists == null || lists.Count == 0)
            {
                Logger.ErrorFormat("Fail to SyncDeliveryOption");
                return;
            }
            _offlineShopService.SyncDeliveryOptions(lists);

        }

        private void SyncDeliveryCost()
        {

            var deliveryCost = _onlineService.GetDeliveryCosts();
            if (deliveryCost == null || deliveryCost.Count == 0)
            {
                Logger.ErrorFormat("Fail to SyncDeliveryCost");
                return;
            }
            _offlineShopService.SyncDeliveryCost(deliveryCost);


        }

        private void SyncMenuItems(List<int> lists)
        {

            var menuItems = new List<WsMenuItem>();
            foreach (var item in lists)
            {
                menuItems.AddRange(_onlineService.GetMenuItemsByCategory(item));
            }

            if (menuItems == null || menuItems.Count == 0)
            {
                Logger.ErrorFormat("Fail to load Menu Item");
                return;
            }
            _offlineShopService.SyncMenuItems(menuItems);
        }

        private List<int> SyncMenuItemCategory()
        {
            var list = new List<int>();

            var menuItemCategories = _onlineService.GetMenuItemCategory();
            if (menuItemCategories == null || menuItemCategories.Count == 0)
            {
                Logger.ErrorFormat("Fail to load Menu Item Category");
                return list;
            }
            var items = _offlineShopService.SyncMenuItemCategory(menuItemCategories);

            list.AddRange(items.Select(cate => cate.Id));

            return list.Distinct().ToList();
        }

        private void SyncShopInfo()
        {

            var shopInfo = _onlineService.GetShopInfo();
            if (shopInfo == null)
            {
                Logger.ErrorFormat("Faild to download Shop Info");
                return;
            }
            _offlineShopService.SyncShopInfo(shopInfo);
        }


    }
}