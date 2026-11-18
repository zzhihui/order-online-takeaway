using System.Collections.Generic;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Email;
using Takeaway.Service.Contract.Entities.Health;
using Takeaway.Service.Contract.Entities.Payment;
using Takeaway.Service.Contract.Entities.Reports;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Service.Services
{
    public interface IShopService
    {

        #region Shop info
        WsShop GetShopInfo(int shopId);
        WsShop UpdateShopInfo(WsShop shopInfo, int shopId, int updateOption);

        WsShop GetShopStatus(int shopId);


        List<WsShop> GetAllShopsNeedTestOrder(int shopId);
        #endregion

        #region MenuItems
        /*Menu Items */
        List<WsMenuItemCategory> GetMenuItemCategoryByGroupId(int groupId, int shopId);
        List<WsMenuItemCategory> GetAdminMenuItemCategoryByGroupId(int groupId, int shopId);
        WsMenuItemCategory GetMenuItemCategoryById(int id, int shopId);
        WsMenuItemCategory SaveMenuCategory(WsMenuItemCategory selectedCategory, int shopId);
        WsMenuItemCategory RemoveMenuItemCategoryById(int id, int shopId);
        List<WsMenuItem> GetMenuItemByCategoryGroupId(int groupId, int shopId);
        List<WsMenuItem> GetMenuItemsByCategory(int categoryId, int shopId);
        WsMenuItem GetMenuItemById(int id, int shopId);
        WsMenuItem SaveMenuItem(WsMenuItem menuItem, int shopId);
        WsMenuItem RemoveMenuItemById(int id, int shopId);
        List<WsMenuItem> GetMenuItemsList(int shopId);

        WsMenuItem SaveMenuItemWithSubItem(int extraId, int memuItemId, int shopId);
        WsMenuItem RemoveMenuItemWithSubItem(int extraId, int memuItemId, int shopId);


        #endregion

        #region Orders


        /*Orders */
        WsOrder SubmitCustomerOrder(WsOrder orderItem, int shopId);
        List<WsOrder> GetOpenOrders(int shopId);
        WsOrder GetOrderById(int orderId, int shopId);
        WsOrder CancelOrderById(WsOrder order, int shopId);
        WsOrder KillOrderById(WsOrder order, int shopId);
        WsOrder DiscountCustomerOrder(WsOrder orderItem, int shopId);

        //Report
        List<WsOrder> GetCompleteOrders(WsOrderSearch search, int shopId);

        WsOrder UpdateOrder(WsOrder selectedOrder, int updateMode, int shopId);
        WsOrder UpdateCloudOrder(WsOrder myOrder, int shopId);

        List<WsDeliveryArea> GetDeliveryArea(int takeaway, int shopId);
        List<WsDeliveryCost> GetDeliveryCosts(int shopId);
        #endregion

        #region Customer

        /* Customer */
        List<WsShopCustomer> GetAllCustomer(int shopId);

        WsShopCustomer RegisterCustomer(WsShopCustomer selectedCustomer, int shopId);
        
        WsShopCustomer VerifySms(WsShopCustomer registeredCustomer, int shopId);

        WsShopCustomer GetCustomerById(int id, int shopId);

        WsShopCustomer ActivateBySms(WsShopCustomer registeredCustomer, int shopId);

        WsShopCustomer Forgetpassword(string email, int shopId);

        WsShopCustomer LoginCustomer(string email, string password, int shopId);

        WsShopCustomer UpdateCustomerInfo(WsShopCustomer registeredCustomer, int shopId);

        WsShopCustomer ResetCustomerPassword(WsShopCustomer registeredCustomer, int shopId);

        #endregion

        #region lookups
        /* Settings lookups */
        List<WsSetting> GetSystemSettings(int shopId);
        List<WsSetting> GetClientSystemSettings(int shopId);

        List<WsTableCommand> GetTableCommand(int shopId);
        List<WsDriver> GetAllDrivers(int shopId);
        List<WsTableAlias> GetTableAlias(int shopId);
        List<WsDeliveryOption> GetDeliveryOptions(int shopId);
        List<WsDeliveryOption> GetDeliveryOptionsByTime(int shopId);
        List<WsDeliveryOption> GetDeliveryOptionsInternal(int shopId);

        List<WsOptionItem> GetOptionItems(int shopId);

        #endregion

        #region payment 

        /* Payment */
        List<WsPaymentMonth> LoadPaymentMonths(int shopId);
        List<WsPaymentYear> LoadPaymentYears(int shopId);
        WsPaymentInfo TakePayment(WsPayment payment, int shopId);
        WsPaymentInfo TakeLoyaltyPayment(WsPayment payment, int customerId,int shopId);

        List<WsPaymentType> GetAllPaymentTypes(int shopId);
        #endregion

        #region Utilities

        /* Utilities */

        WsSyncStatus SyncStatus(int shopId);
        WsEmailResponse EmailException(string format, int shopId);
        #endregion

        #region ShopUser


        /* Users */
        WsShopUser LoginShopAgent(string pin, bool online, int shopId);
        List<WsShopUser> GetAllShopUsers(int shopId);
        WsShopUser SaveShopUser(WsShopUser newShopUser, int shopId);
        WsShopUser GetShopUserById(int userId, int shopId);
        WsShopUser RemoveShopUseById(int userId, int shopId);
        #endregion

        #region Reports


        /* Reports */
        WsReport GetReport(WsReport report, int shopId);

        WsReport GetAdminReport(WsReport report, int shopId);

        #endregion

        #region Option Items

        WsOptionItem SaveOptionItem(WsOptionItem item, int shopId);
        WsOptionItem GetOptionItemById(int id, int shopId);
        WsOptionItem RemoveOptionItemById(int id, int shopId);

        #endregion

        #region System Setting

        WsSetting GetSystemSettingById(int id, int shopId);
        WsSetting SaveSystemSetting(WsSetting selectedItem, int shopId);

        #endregion

        #region Delivery Cost

        WsDeliveryCost GetDeliveryCostsById(int id, int shopId);
        WsDeliveryCost SaveDeliveryCost(WsDeliveryCost selectedItem, int shopId);

        #endregion

        #region Shop News

        List<WsShopNews> GetShopNews(int shopId);
        WsShopNews SaveShopNews(WsShopNews selectedShopNews, int shopId);
        WsShopNews GetShopNewsById(int id, int shopId);
        WsShopNews RemoveShopNewsById(int id, int shopId);

        #endregion


        #region Menu Sub Items

        List<WsMenuSubItem> GetAllMenuSubItems(int shopId);
        WsMenuSubItem RemoveMenuSubItemById(int id, int shopId);
        WsMenuSubItem SaveMenuSubItem(WsMenuSubItem selectedMenuSubItem, int shopId);
        WsMenuSubItem GetMenuSubItemsById(int id, int shopId);
        WsMenuSubItem RemoveMenuSubItemLink(int id, int menuItemId, int shopId);
        WsMenuSubItem SaveMenuSubItemWithItem(int id, int selectedMenuItemId, decimal cost, int shopId);
        #endregion

        string GetEncryptedPassword(string password, int shopId);


        
    }
}