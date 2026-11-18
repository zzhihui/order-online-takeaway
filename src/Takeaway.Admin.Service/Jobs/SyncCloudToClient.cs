using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using log4net;
using Quartz;
using Takeaway.Admin.Service.Core;
using Takeaway.Admin.Util.Services;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Admin.Service.Jobs
{
    [ProcessJob]
    [DisallowConcurrentExecution]
    public class SyncCloudToClient : IInterruptableJob, IBaseJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SyncCloudToClient).Name);
        volatile bool _stop;

        private readonly IOnlineService _onlineService;
        private readonly IOfflineShopService _offlineShopService;

        public SyncCloudToClient(IOnlineService onlineService, IOfflineShopService offlineShopService)
        {
            _onlineService = onlineService;
            _offlineShopService = offlineShopService;
        }


        public void Execute(IJobExecutionContext context)
        {

            Logger.Info("Start SyncCloudToClient Job ");

            _stop = false;
            if (ConfigurationManager.AppSettings["FeatureSyncCloudToClientOn"] != "true")
            {
                Logger.Info("[SyncCloudToClient]Job is turned off  ");
            }
            else
            {
                //var shopInfo=SyncShopInfo();
                //if (shopInfo != null && shopInfo.ActiveSync.HasValue && shopInfo.ActiveSync.Value)
                //{
                //    var categories = SyncMenuItemCategory();
                //    SyncMenuItems(categories);
                //    SyncSetting();
                //    SyncDeliveryCost();
                //    SyncDeliveryOption();
                //    SyncDriver();
                //    SyncOptionItems();
                //    SyncPaymentType();
                //    SyncShopUser();
                //    SyncTableAlias();
                //    SyncTableCommand();
                //}
            }

            Logger.Info("Finish SyncCloudToClient Job ");

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

        private WsShop SyncShopInfo()
        {
            var shopInfo = _onlineService.GetShopInfo();
            if (shopInfo == null)
            {
                Logger.ErrorFormat("Faild to download Shop Info");
                return null;
            }
            _offlineShopService.SyncShopInfo(shopInfo);
            return shopInfo;
        }


        public void Interrupt() { _stop = true; }
    }
}