using System.Collections.Generic;
using System.Collections.ObjectModel;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Email;
using Takeaway.Service.Contract.Entities.Health;
using Takeaway.Service.Contract.Entities.Licence;
using Takeaway.Service.Contract.Entities.Payment;
using Takeaway.Service.Contract.Entities.Reports;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Services;
using Takeway.Mobile.Core.Utilities;

namespace Takeway.Mobile.Core.Services.RestServices
{
    public interface IShopRestServiceWrapper : IShopService
    {

    }


    public class ShopRestServiceWrapper : IShopRestServiceWrapper
    {
        private readonly IShopService _shopService;
        private readonly ICacheProvider _cacheProvider;

        public ShopRestServiceWrapper(IShopService shopService, ICacheProvider cacheProvider)
        {
            _shopService = shopService;
            _cacheProvider = cacheProvider;
        }


        public WsShop GetShopInfo()
        {
            return _shopService.GetShopInfo();
        }

        public List<WsMenuItemCategory> GetMenuItemCategoryByGroupId(int groupId)
        {
            var cacheKey = string.Format("{0}{1}", CacheKey.MenuItemCategory, groupId);
            var cacheValue = _cacheProvider.Get<string, List<WsMenuItemCategory>>(cacheKey);
            if (cacheValue != null)
                return cacheValue;

            var response = _shopService.GetMenuItemCategoryByGroupId(groupId);

            if (response != null && response.Count > 0)
            _cacheProvider.Add(cacheKey, response);
            return response;

        }

        public List<WsMenuItem> GetMenuItemsByCategory(int categoryId)
        {
            var cacheKey = string.Format("{0}{1}", CacheKey.MenuItemsByCategory, categoryId);
            var cacheValue = _cacheProvider.Get<string, List<WsMenuItem>>(cacheKey);
            if (cacheValue != null)
                return cacheValue;
            var response = _shopService.GetMenuItemsByCategory(categoryId);

            if (response != null && response.Count > 0)
            _cacheProvider.Add(cacheKey, response);
            return response;


        }

        public List<WsMenuItem> GetMenuItemsList()
        {
            throw new System.NotImplementedException();
        }

        public WsMenuItem RemoveMenuItemById(int id)
        {
            throw new System.NotImplementedException();
        }

        public WsOrder SubmitCustomerOrder(WsOrder orderItem)
        {
            return _shopService.SubmitCustomerOrder(orderItem);
        }

        public List<WsOrder> GetOpenOrders()
        {
            return _shopService.GetOpenOrders();
        }

        public WsOrder GetOrderById(int orderId)
        {
            return _shopService.GetOrderById(orderId);
        }

        public WsOrder CloseOrderById(WsOrder order)
        {
            return _shopService.CloseOrderById(order);
        }

        public WsOrder CancelOrderById(WsOrder order)
        {
            return _shopService.CancelOrderById(order);
        }

        public WsOrder KillOrderById(WsOrder order)
        {
            return _shopService.KillOrderById(order);
        }

        public List<WsOrder> GetCompleteOrders(WsOrderSearch search)
        {
            return _shopService.GetCompleteOrders(search);
        }

        public List<WsShopSpecialOffer> GetLatestOffer(int lastOffer, int userId)
        {
            return _shopService.GetLatestOffer(lastOffer, userId);
        }

        public List<WsSetting> GetSystemSettings()
        {
            return _shopService.GetSystemSettings();
        }

        public List<WsSetting> GetClientSystemSettings()
        {
            throw new System.NotImplementedException();
        }

        public List<WsDeliveryOption> GetDeliveryOptions()
        {
            var cacheValue = _cacheProvider.Get<string, List<WsDeliveryOption>>(CacheKey.DeliveryOptions.ToString());
            if (cacheValue != null)
                return cacheValue;

            var response= _shopService.GetDeliveryOptions();

            if (response != null && response.Count > 0)
            _cacheProvider.Add(CacheKey.DeliveryOptions.ToString(), response);
            return response;
        }

        public List<WsDeliveryOption> GetDeliveryOptionsInternal()
        {
            var cacheValue = _cacheProvider.Get<string, List<WsDeliveryOption>>(CacheKey.DeliveryOptionsInternal.ToString());
            if (cacheValue != null)
                return cacheValue;

            var response = _shopService.GetDeliveryOptionsInternal();

            if (response != null && response.Count > 0)
            _cacheProvider.Add(CacheKey.DeliveryOptionsInternal.ToString(), response);
            return response;
        }

        public WsOrder DiscountCustomerOrder(WsOrder orderItem)
        {
            return _shopService.DiscountCustomerOrder(orderItem);
        }

        public WsShopUser LoginShopAgent(string pin, bool online)
        {
            return _shopService.LoginShopAgent(pin, online);
        }

        public List<WsMenuItem> GetHotMenuItems()
        {
            return _shopService.GetHotMenuItems();
        }

        public List<WsMenuItem> GetHotMenuItemsInternal()
        {
            return _shopService.GetHotMenuItemsInternal();
        }

        public List<WsPaymentMonth> LoadPaymentMonths()
        {
            return _shopService.LoadPaymentMonths();
        }

        public List<WsPaymentYear> LoadPaymentYears()
        {
            return _shopService.LoadPaymentYears();
        }

        public WsPaymentInfo TakePayment(WsPayment payment)
        {
            return _shopService.TakePayment(payment);
        }

        public WsSyncStatus SyncStatus()
        {
            return _shopService.SyncStatus();
        }

        public WsShopUser AuthenticateUser(string username, string password, int shopId)
        {
            return _shopService.AuthenticateUser(username, password, shopId);
        }

        public WsEmailResponse EmailException(string format)
        {
            return _shopService.EmailException(format);
        }

        public WsShopCustomer GetCustomerByPhoneNumber(string customerPhone)
        {
            return _shopService.GetCustomerByPhoneNumber(customerPhone);
        }

        public List<WsOrder> GetCustomerOrders(WsShopCustomer customer)
        {
            throw new System.NotImplementedException();
        }

        public List<WsShopCustomer> GetAllCustomer()
        {
            throw new System.NotImplementedException();
        }

        public WsLicenceInfo CheckLicence(string hwid)
        {
            return _shopService.CheckLicence(hwid);
        }

        public WsReport GetReport(WsReport report)
        {
            return _shopService.GetReport(report);
        }

        public List<WsOrderHolder> GetHoldOrders()
        {
            throw new System.NotImplementedException();
        }

        public List<WsOrder> GetLocalCompleteOrders(WsOrderSearch searchFilter)
        {
            throw new System.NotImplementedException();
        }

        public void CancelLocalOrderById(WsOrder selectedOrder)
        {
            throw new System.NotImplementedException();
        }

        public WsShopCustomer RemoveCustomer(string customerPhone)
        {
            throw new System.NotImplementedException();
        }

        public void SyncWithServer()
        {
            throw new System.NotImplementedException();
        }

        public void InitSyncWithServer()
        {
            throw new System.NotImplementedException();
        }

        public List<WsOfflineSetting> GetOfflineSettings()
        {
            throw new System.NotImplementedException();
        }

        public void SaveOfflineSettings(string appMaxOrder, string toString)
        {
            throw new System.NotImplementedException();
        }

        public List<WsOptionItem> GetOptionItems()
        {
            return _shopService.GetOptionItems();
        }

        public List<WsMenuItemCategoryGroup> GetMenuItemCategoryGroup()
        {
            return _shopService.GetMenuItemCategoryGroup();
        }

        public WsMenuItem GetMenuItemById(int id)
        {
            throw new System.NotImplementedException();
        }

        public WsMenuItem SaveMenuItem(WsMenuItem menuItem)
        {
            throw new System.NotImplementedException();
        }

        public List<WsTables> GetExistingTables(int tableSize)
        {
            throw new System.NotImplementedException();
        }

        public void SaveTableSettings(WsTables table)
        {
            throw new System.NotImplementedException();
        }

        public WsTables GetTableByTableId(int tableId)
        {
            throw new System.NotImplementedException();
        }

        public List<WsShopUser> GetAllShopUsers()
        {
            throw new System.NotImplementedException();
        }

        public List<WsTableCommand> GetTableCommand()
        {
            throw new System.NotImplementedException();
        }

        public List<WsDriver> GetAllDrivers()
        {
            throw new System.NotImplementedException();
        }

        public WsOrder UpdateOrder(WsOrder selectedOrder, int updateDriver)
        {
            throw new System.NotImplementedException();
        }

        public List<WsPaymentType> GetAllPaymentTypes()
        {
            throw new System.NotImplementedException();
        }

        public WsMergeOrder MergeOrder(WsMergeOrder mergeOrder)
        {
            throw new System.NotImplementedException();
        }

        public WsOrderHolder HotTempOrder(WsOrderHolder holdOrder)
        {
            throw new System.NotImplementedException();
        }

        public List<WsTableAlias> GetTableAlias()
        {
            throw new System.NotImplementedException();
        }

        public string GetNextOrderReference(WsDeliveryOption selectedDeliveryOption, bool isWalkIn)
        {
            throw new System.NotImplementedException();
        }

        public WsOrder UpdateCloudOrder(WsOrder myOrder)
        {
            throw new System.NotImplementedException();
        }

        public List<WsDeliveryArea> GetDeliveryArea(int takeaway)
        {
            throw new System.NotImplementedException();
        }

        public List<WsDeliveryCost> GetDeliveryCosts()
        {
            throw new System.NotImplementedException();
        }

        public WsShopCustomer SaveCustomerLocal(string customerPhone, string customerAddress, string customerName, string customerEmail,
            decimal? customerDeliveryCharge)
        {
            throw new System.NotImplementedException();
        }

        public WsShopCustomer SaveCustomerCloud(string customerPhone, string customerAddress, string customerName, string customerEmail,
            decimal? customerDeliveryCharge)
        {
            throw new System.NotImplementedException();
        }

 public WsShopUser SaveShopUser(WsShopUser newShopUser)
        {
            throw new System.NotImplementedException();
        }

        public WsShopUser GetShopUserById(int userId)
        {
            throw new System.NotImplementedException();
        }

        public WsShopUser RemoveShopUseById(int userId)
        {
            throw new System.NotImplementedException();
        }


    }
}