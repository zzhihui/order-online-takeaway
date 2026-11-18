using System.Collections.Generic;
using System.Collections.ObjectModel;
using Takeaway.Builder.WsDataContract.Builders;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Email;
using Takeaway.Service.Contract.Entities.Health;
using Takeaway.Service.Contract.Entities.Licence;
using Takeaway.Service.Contract.Entities.Payment;
using Takeaway.Service.Contract.Entities.Reports;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Services;

namespace Takeway.Mobile.Core.Services.RestServices
{
    public class ShopRestServiceMock : IShopService
    {
        public WsShop GetShopInfo()
        {
            return new WsShopBuilder().Build();
        }

      
        public List<WsMenuItemCategory> GetMenuItemCategoryByGroupId(int groupId)
        {
            return new List<WsMenuItemCategory>()
            {
                new WsMenuItemCategoryBuilder().WithName("Special Offers").WithDescription("What is on offer").Build(1),
                new WsMenuItemCategoryBuilder().WithName("Meal Deals").WithDescription("Please note: Extras may be offered with these items.").Build(2),
                new WsMenuItemCategoryBuilder().WithName("Appetisers").WithDescription("Please note: Extras may be offered with these items.").Build(3),
                new WsMenuItemCategoryBuilder().WithName("Soups").WithDescription("").Build(4),
                new WsMenuItemCategoryBuilder().WithName("Salads").WithDescription("").Build(5),
                new WsMenuItemCategoryBuilder().WithName("Curries").WithDescription("").Build(6),
                new WsMenuItemCategoryBuilder().WithName("Wok dishes").WithDescription("").Build(7),
                new WsMenuItemCategoryBuilder().WithName("Wok noodles special").WithDescription("").Build(8)
            };
        }

        public List<WsMenuItem> GetMenuItemsByCategory(int categoryId)
        {
            return new List<WsMenuItem>()
            {
                new WsMenuItemBuilder().WithName("Snack Box Meal").WithDescription("California rolls & a can of soft drink").WithHasSubOrder().WithIshot().WithSubItemSelections().WithPrice(10).Build(1),
                new WsMenuItemBuilder().WithName("Meal Deal For One").WithDescription("Thai Massaman curry with beef OR chicken & one boiled rice & a can of soft drink").WithHasSubOrder().WithIshot().WithSubItemSelections().WithPrice(new decimal(7.58)).Build(2),
                new WsMenuItemBuilder().WithName("Meal for Two").WithDescription("Red curry with beef & boiled rice & one California sushi roll & a can of soft drink").WithHasSubOrder().WithIshot().WithSubItemSelections().WithPrice(new decimal(6.58)).Build(3),
                new WsMenuItemBuilder().WithName("Duck Spring Roll").WithDescription("").WithHasSubOrder().WithIshot().WithSubItemSelections().WithPrice(new decimal(5.98)).Build(4),
                new WsMenuItemBuilder().WithName("Spare Ribs").WithDescription("Grilled marinated pork spare ribs with Japanese teriyaki sauce").WithHasSubOrder().WithIshot().WithSubItemSelections().WithPrice(new decimal(8.50)).Build(5),
                new WsMenuItemBuilder().WithName("Yasai Gyoza").WithDescription().WithHasSubOrder().WithIshot().WithSubItemSelections().WithPrice(new decimal(12)).Build(6),
                new WsMenuItemBuilder().WithName("Tonkatsu").WithDescription().WithHasSubOrder().WithIshot().WithSubItemSelections().WithPrice(new decimal(11)).Build(7),

            };
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
            return orderItem;
        }

        public List<WsOrder> GetOpenOrders()
        {
            throw new System.NotImplementedException();
        }

        public WsOrder GetOrderById(int orderId)
        {
            throw new System.NotImplementedException();
        }

        public WsOrder CloseOrderById(WsOrder order)
        {
            return order;
        }

        public WsOrder CancelOrderById(WsOrder order)
        {
            return order;
        }

        public WsOrder KillOrderById(WsOrder order)
        {
            return order;
        }

        public List<WsOrder> GetCompleteOrders(WsOrderSearch search)
        {
            throw new System.NotImplementedException();
        }

        public List<WsShopSpecialOffer> GetLatestOffer(int lastOffer, int userId)
        {
            throw new System.NotImplementedException();
        }

        public List<WsSetting> GetSystemSettings()
        {
            throw new System.NotImplementedException();
        }

        public List<WsSetting> GetClientSystemSettings()
        {
            throw new System.NotImplementedException();
        }

        public List<WsDeliveryOption> GetDeliveryOptions()
        {
            throw new System.NotImplementedException();
        }

        public List<WsDeliveryOption> GetDeliveryOptionsInternal()
        {
            throw new System.NotImplementedException();
        }

        public WsOrder DiscountCustomerOrder(WsOrder orderItem)
        {
            return orderItem;
        }

        public WsShopUser LoginShopAgent(string pin, bool online)
        {
            throw new System.NotImplementedException();
        }

        public List<WsMenuItem> GetHotMenuItems()
        {
            throw new System.NotImplementedException();
        }

        public List<WsMenuItem> GetHotMenuItemsInternal()
        {
            throw new System.NotImplementedException();
        }

        public List<WsPaymentMonth> LoadPaymentMonths()
        {
            throw new System.NotImplementedException();
        }

        public List<WsPaymentYear> LoadPaymentYears()
        {
            throw new System.NotImplementedException();
        }

        public WsPaymentInfo TakePayment(WsPayment payment)
        {
            throw new System.NotImplementedException();
        }

        public WsSyncStatus SyncStatus()
        {
            throw new System.NotImplementedException();
        }

        public WsShopUser AuthenticateUser(string username, string password, int shopId)
        {
            throw new System.NotImplementedException();
        }

        public WsEmailResponse EmailException(string format)
        {
            throw new System.NotImplementedException();
        }

        public WsShopCustomer GetCustomerByPhoneNumber(string customerPhone)
        {
            throw new System.NotImplementedException();
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
            return new WsLicenceInfo(){IsValidLicence =true};
        }

        public WsReport GetReport(WsReport report)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public List<WsMenuItemCategoryGroup> GetMenuItemCategoryGroup()
        {
            throw new System.NotImplementedException();
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