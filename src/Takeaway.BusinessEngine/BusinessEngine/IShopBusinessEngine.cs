using System;
using System.Collections.Generic;
using System.Data;
using Takeaway.Data.Entities.Authentication;
using Takeaway.Data.Entities.Payment;
using Takeaway.Data.Entities.Settings;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.BusinessEngine.BusinessEngine
{
    public interface IShopBusinessEngine : IBusinessEngine<IShopBusinessEngine>
    {
        ShopInfo GetShopInfo(int shopId);
        ShopInfo GetShopStatus(int shopId);


        /* Menu Item */
        List<MenuItem> GetMenuItemsByCategory(int categoryId, int shopId);
        List<MenuItem> GetMenuItemByCategoryGroupId(int groupId, int shopId);
        List<MenuItemCategory> GetAdminMenuItemCategoryByGroupId(int groupId, int shopId);
        List<MenuItem> GetHotMenuItems(int shopId);
        List<MenuItem> GetHotMenuItemsInternal(int shopId);
        List<MenuItemCategory> GetMenuItemCategoryByGroupId(int groupId, int shopId);
        List<MenuItemCategoryGroup> GetMenuItemCategoryGroup(int shopId);
        List<MenuItem> GetMenuItemsList(int shopId);
        MenuItem GetMenuItemById(int id, int shopId);
        MenuItem SaveMenuItem(MenuItem item, int shopId);
        MenuItem RemoveMenuItemById(int id, int shopId);


        /* Orders */
        List<Order> GetOpenOrders(int shopId);
        Order GetOrder(int orderId, int shopId);
        Order CloseOrder(int orderId, int shopId);
        Order CancelOrder(int orderId, int shopId);
        Order SaveOrder(Order orderItem, int shopId);
        Order RemoveOrder(int id, int shopId);
        List<Order> GetCompleteOrders(DateTime? searchDate, int shopId);
        DiscountOutput DiscountOrder(Order orderTransformed, int shopId);

        List<ServerSetting> GetSystemSettings(int shopId);
        List<DeliveryOption> GetDeliveryOptions(int shopId);
        List<DeliveryOption> GetDeliveryOptionsByTime(int shopId);
        List<DeliveryOption> GetDeliveryOptionsInternal(int shopId);

        List<OptionItem> GetOptionItems(int shopId);
        List<TableCommand> GetTableCommand(int shopId);
        List<Driver> GetAllDrivers(int shopId);
        List<TableAlias> GetTableAlias(int shopId);
        List<ServerSetting> GetClientSystemSettings(int shopId);
        List<DeliveryArea> GetDeliveryArea(int deliveryAreaGroup, int shopId);
        List<DeliveryCost> GetDeliveryCosts(int shopId);

        /* Utilities */
        void EmailClientOrderConfirmation(Order orderTransformed, ShopInfo shopInfo, EmailSettingsType settings, int shopId);
        void EmailBossConfirmation(Order orderTransformed, ShopInfo shopInfo, string toEmail, EmailSettingsType settings, int shopId);
        EmailSettingsType GetEmailSettings(List<ServerSetting> systemSetting, int shopId);
        bool GetEmailSettingsAllowEmailClient(List<ServerSetting> systemSetting, int shopId);
        bool GetEmailSettingsAllowEmailShop(List<ServerSetting> systemSetting, int shopId);
        bool SyncStatus(int shopId);
        bool EmailException(string message, string fromEmail, string toEmail, EmailSettingsType settings, int shopId);

        bool CheckLicence(string hwid, int shopId);

        /* Payments */
        PaymentInfo TakePayment(Payment paymentTransformed, string apiKey, int shopId);
        List<PaymentType> GetAllPaymentTypes(int shopId);


        /* Customer */
        Customer GetCustomerByPhoneNumber(string customerPhone, int shopId);
        List<Customer> GetAllCustomer(int shopId);
        void SaveCustomerInfo(string customerPhone, string customerAddress, string customerName, string customerEmail, decimal? customerDeliveryCharge, int shopId);
        Customer RemoveCustomerByPhone(string customerPhone, int shopId);

        /* Shop User */
        ShopUser LoginShopUserByPin(string pin, int shopId);
        List<ShopUser> GetAllShopUsers(int shopId);
        ShopUser GetShopUserById(int userId, int shopId);
        bool CheckUserPinExsits(ShopUser shopUserTransformed, int shopId);
        bool CheckShopUserNameUnique(ShopUser shopUserTransformed, int shopId);
        ShopUser SaveShopUser(ShopUser shopUser, int shopId);
        ShopUser RemoveShopUserById(int userId, int shopId);

        /* Reports */
        DataTable GetReport_SummaryReport(DateTime? fromDate, int shopId);
        DataTable GetReport_TransactionReport(DateTime? fromDate, int shopId);
        DataTable GetReport_TransactionReportOnline(DateTime? fromDate, int shopId);
        DataTable GetReport_SummaryOnline(DateTime? fromDate, int shopId);

        DataTable GetReport_TransactionDrinkReport(DateTime? fromDate, int shopId);
        DataTable GetReport_OrdeFoodReport(DateTime? fromDate, int shopId);
        DataTable GetReport_DriverReport(DateTime? fromDate, int shopId);
        DataTable GetReport_CustomerReport(int shopId);
        
        DataTable GetCustomerOrders(string phoneNumber, int shopId);
        DataTable GetReport_SummaryReport(DateTime? fromDate, DateTime? toDate, int shopId);
        DataTable GetReport_TransactionReport(DateTime? fromDate, DateTime? toDate, int shopId);
        DataTable GetReport_TransactionDrinkReportMulti(DateTime? fromDate, DateTime? toDate, int shopId);

        DataTable GetReport_BusyHourReport(DateTime? searchDate, int shopId);
        DataTable GetReport_CustomerOrderReport(int shopId);
        DataTable GetAdminReport_Status();
        DataTable GetCustomerPayOutReport();
        Order GetOrderByGuid(string guid, int shopId);

        ShopInfo UpdateShopInfo(ShopInfo shopInfoTransformed, int shopId);

        string GetPaymentApiKey(List<ServerSetting> systemSetting);
        OptionItem SaveOptionItem(OptionItem transformItem, int shopId);
        OptionItem GetOptionItemById(int id, int shopId);
        OptionItem RemoveOptionItemById(int id, int shopId);
        ServerSetting GetSystemSettingById(int id, int shopId);
        ServerSetting SaveSystemSetting(ServerSetting transformItem, int shopId);
        DeliveryCost GetDeliveryCostsById(int id, int shopId);
        DeliveryCost SaveDeliveryCost(DeliveryCost transformItem, int shopId);
        List<ShopNews> GetShopNews(int shopId);
        ShopNews SaveShopNews(ShopNews transformItem, int shopId);
        ShopNews GetShopNewsById(int id, int shopId);
        ShopNews RemoveShopNewsById(int id, int shopId);
        List<MenuSubItem> GetAllMenuSubItems(int shopId);
        MenuSubItem RemoveMenuSubItemById(int id, int shopId);
        MenuSubItem SaveMenuSubItem(MenuSubItem transformInItem, int shopId);
        MenuSubItem GetMenuSubItemsById(int id, int shopId);
        MenuSubItem RemoveMenuSubItemLink(int id, int menuItemId, int shopId);
        MenuSubItem SaveMenuSubItemWithItem(int id, int selectedMenuItemId, decimal cost, int shopId);
        MenuItemCategory SaveMenuCategory(MenuItemCategory transformItem, int shopId);
        MenuItemCategory RemoveMenuItemCategoryById(int id, int shopId);
        MenuItemCategory GetMenuItemCategoryById(int id, int shopId);
        MenuItem SaveMenuItemWithSubItem(int extraId, int memuItemId, int shopId);
        MenuItem RemoveMenuItemWithSubItem(int extraId, int memuItemId, int shopId);
        Customer RegisterCustomer(Customer shopCustomerPocoIn, int shopId);

        Customer VerifySms(Customer shopCustomerPocoIn, int shopId);
        Customer GetCustomerById(int id, int shopId);
        Customer ActivateBySms(Customer registeredCustomer, int shopId);
        Customer Forgetpassword(string email, int shopId);
        Customer LoginCustomer(string email, string password, int shopId);

        Customer UpdateCustomerInfo(Customer shopCustomerPocoIn, int shopId);
        Customer ResetCustomerPassword(Customer shopCustomerPocoIn, int shopId);

        int ReValidateShopIdByOrderItem(List<int> menuItemIds, int shopId);

        string GetEncryptedPassword(string password, int shopId);
        DataTable GetReport_AdminReportSummaryOnlineReport(DateTime? fromDate, DateTime? toDate, int shopId);
        DataTable GetReport_AdminReportTransactionOnlineReport(DateTime? fromDate, DateTime? toDate, int shopId);
        DataTable GetReportInvoiceOnline(DateTime? fromDate, DateTime? toDate, int shopId);
        DataTable GetAdminReportInvoice();
        PaymentInfo TakeLoyaltyPayment(Payment paymentTransformed, int customerId, int shopId);

        int UpdateCustomerRewardPoints(int recalculatedShopId, Order orderTransformed);

        List<ShopInfo> GetAllShopsNeedTestOrder(int shopId);
    }
}