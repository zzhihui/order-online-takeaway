using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web.Http;
using Core.Common.Utility;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Payment;
using Takeaway.Service.Contract.Entities.Reports;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Services;

namespace Takeway.Web.Controllers.Api
{
    public class ShopApiController : ApiControllerBase
    {
        #region Constructors

        private readonly IShopService _shopService;
        public ShopApiController(IShopService shopService)
        {
            _shopService = shopService;
        }

        #endregion

        #region Shop Info
        public IHttpActionResult GetShopInfo(int shopId)
        {
            return Execute("GetShopInfo", shopId,() =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetShopInfo(shopId);
            });
        }

        [HttpGet]
        public IHttpActionResult GetAllShopsNeedTestOrder(int shopId)
        {
            return Execute("GetAllShopsNeedTestOrder", shopId, () =>
            {
                return _shopService.GetAllShopsNeedTestOrder(shopId);
            });
        }

        public IHttpActionResult GetShopStatus(int shopId)
        {
            return Execute("GetShopStatus", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetShopStatus(shopId);
            });
        }


        [HttpPost]
        public IHttpActionResult UpdateShopInfo(WsShop shopInfo, int updateOption, int shopId)
        {

            return Execute("UpdateShopInfo", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.UpdateShopInfo(shopInfo, updateOption, shopId);
            });
        }

        #endregion

        #region MenuItem
        public IHttpActionResult GetMenuItemCategoryByGroupId(int groupId, int shopId)
        {
            return Execute("GetMenuItemCategoryByGroupId", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetMenuItemCategoryByGroupId(groupId, shopId);

            });
        }

        public IHttpActionResult GetAdminMenuItemCategoryByGroupId(int groupId, int shopId)
        {
            return Execute("GetAdminMenuItemCategoryByGroupId", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetAdminMenuItemCategoryByGroupId(groupId, shopId);

            });
        }

        [HttpGet]
        public IHttpActionResult GetMenuItemCategoryById(int id, int shopId)
        {
            return Execute("GetMenuItemCategoryById", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetMenuItemCategoryById(id, shopId);

            });
        }

        [HttpPost]
        public IHttpActionResult SaveMenuCategory(WsMenuItemCategory category, int shopId)
        {
            return Execute("SaveMenuCategory", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.SaveMenuCategory(category, shopId);
            });
        }
        

        public IHttpActionResult GetMenuItemByCategoryGroupId(int groupId, int shopId)
        {
            return Execute("GetMenuItemByCategoryGroupId", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetMenuItemByCategoryGroupId(groupId, shopId);
            });
        }
        public IHttpActionResult GetMenuItemsByCategory(int categoryId, int shopId)
        {
            return Execute("GetMenuItemsByCategory", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetMenuItemsByCategory(categoryId, shopId);
            });
        }
        public IHttpActionResult GetMenuItemById(int id, int shopId)
        {
            return Execute("GetMenuItemById", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetMenuItemById(id, shopId);
            });
        }
        [HttpPost]
        public IHttpActionResult SaveMenuItem(WsMenuItem menuItem, int shopId)
        {
            return Execute("SaveMenuItem", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.SaveMenuItem(menuItem, shopId);
            });
        }
        [HttpGet]
        public IHttpActionResult RemoveMenuItemById(int id, int shopId)
        {
            return Execute("RemoveMenuItemById", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.RemoveMenuItemById(id, shopId);
            });
        }

        [HttpGet]
        public IHttpActionResult RemoveMenuItemCategoryById(int id, int shopId)
        {
            return Execute("RemoveMenuItemCategoryById", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.RemoveMenuItemCategoryById(id, shopId);
            });
        }

        [HttpGet]
        public IHttpActionResult GetMenuItemsList(int shopId)
        {
            return Execute("GetMenuItemsList", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetMenuItemsList(shopId);
            });
        }
        [HttpGet]
        public IHttpActionResult SaveMenuItemWithSubItem(int extraId, int memuItemId, int shopId)
        {
            return Execute("SaveMenuItemWithSubItem", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.SaveMenuItemWithSubItem(extraId, memuItemId,shopId);
            });
        }


        [HttpGet]
        public IHttpActionResult RemoveMenuItemWithSubItem(int extraId, int memuItemId, int shopId)
        {
            return Execute("RemoveMenuItemWithSubItem", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.RemoveMenuItemWithSubItem(extraId, memuItemId, shopId);
            });
        }

        #endregion

        #region Orders
        [HttpPost]
        public IHttpActionResult SubmitCustomerOrder(WsOrder orderItem, int shopId)
        {

            return Execute("SubmitCustomerOrder", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.SubmitCustomerOrder(orderItem, shopId);
            });
        }
        [HttpPost]
        public IHttpActionResult DiscountCustomerOrder(WsOrder orderItem, int shopId)
        {

            return Execute("DiscountCustomerOrder", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.DiscountCustomerOrder(orderItem, shopId);
            });
        }
        public IHttpActionResult GetOpenOrders(int shopId)
        {

            return Execute("GetOpenOrders", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetOpenOrders(shopId);
            });
        }
        public IHttpActionResult GetOrderById(int orderId, int shopId)
        {

            return Execute("GetOrderById", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetOrderById(orderId, shopId);
            });

        }
        [HttpPost]
        public IHttpActionResult GetCompleteOrders(WsOrderSearch search, int shopId)
        {
            return Execute("GetCompleteOrders", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetCompleteOrders(search, shopId);
            });
        }
        [HttpPost]
        public IHttpActionResult KillOrderById(WsOrder order, int shopId)
        {
            return Execute("KillOrderById", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.KillOrderById(order, shopId);
            });
        }
        [HttpPost]
        public IHttpActionResult CancelOrderById(WsOrder order, int shopId)
        {

            return Execute("CancelOrderById", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.CancelOrderById(order, shopId);
            });
        }
        [HttpPost]
        public IHttpActionResult UpdateOrder(WsOrder order, int updateOption, int shopId)
        {
            //TODO: DO WE NEED THIS??
            return Execute("UpdateOrder", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.UpdateOrder(order, updateOption, shopId);
            });
        }
        [HttpPost]
        public IHttpActionResult UpdateCloudOrder(WsOrder order, int shopId)
        {
            return Execute("UpdateCloudOrder", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.UpdateCloudOrder(order, shopId);
            });
        }

        #endregion

        #region System Settings
        public IHttpActionResult GetSystemSettings(int shopId)
        {
            return Execute("GetSystemSettings", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetSystemSettings(shopId);
            });
        }
        public IHttpActionResult GetClientSystemSettings(int shopId)
        {
            return Execute("GetClientSystemSettings", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetClientSystemSettings(shopId);
            });
        }
        [HttpPost]
        public IHttpActionResult SaveSystemSetting(WsSetting selectItem, int shopId)
        {
            return Execute("SaveSystemSetting", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.SaveSystemSetting(selectItem, shopId);
            });
        }
        [HttpGet]
        public IHttpActionResult GetSystemSettingById(int id, int shopId)
        {
            return Execute("GetSystemSettingById", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetSystemSettingById(id, shopId);
            });
        }

        #endregion

        #region Delivery Options
        public IHttpActionResult GetDeliveryOptions(int shopId)
        {
            return Execute("GetDeliveryOptions", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetDeliveryOptions(shopId);

            });
        }
        public IHttpActionResult GetDeliveryOptionsByTime(int shopId)
        {
            return Execute("GetDeliveryOptionsByTime", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetDeliveryOptionsByTime(shopId);

            });
        }
        public IHttpActionResult GetDeliveryOptionsInternal(int shopId)
        {
            return Execute("GetDeliveryOptionsInternal", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetDeliveryOptionsInternal(shopId);

            });
        }
        #endregion

        #region Option Items
        public IHttpActionResult GetOptionItems(int shopId)
        {
            return Execute("GetDeliveryOptions", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetOptionItems(shopId);

            });
        }
        [HttpPost]
        public IHttpActionResult SaveOptionItem(WsOptionItem optionItem, int shopId)
        {
            return Execute("SaveOptionItem", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.SaveOptionItem(optionItem, shopId);
            });
        }
        [HttpGet]
        public IHttpActionResult GetOptionItemById(int id, int shopId)
        {
            return Execute("GetOptionItemById", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetOptionItemById(id, shopId);
            });
        }
        [HttpGet]
        public IHttpActionResult RemoveOptionItemById(int userId, int shopId)
        {
            return Execute("RemoveOptionItemById", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.RemoveOptionItemById(userId, shopId);
            });
        }

        #endregion

        #region Login
        [HttpGet]
        public IHttpActionResult LoginShopAgent(string pin, int shopId)
        {
            return Execute("LoginShopAgent", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.LoginShopAgent(pin, true, shopId);
            });
        }

        #endregion

        #region Take payment
        [HttpPost]
        public IHttpActionResult TakePayment(WsPayment payment, int shopId)
        {
            return Execute("TakePayment", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.TakePayment(payment, shopId);
            });
        }

        [HttpPost]
        public IHttpActionResult TakeLoyaltyPayment(WsPayment payment, int customerId, int shopId)
        {
            return Execute("TakeLoyaltyPayment", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.TakeLoyaltyPayment(payment, customerId,shopId);
            });
        }
        public IHttpActionResult GetPaymentMonths(int shopId)
        {
            return Execute("GetPaymentMonths", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.LoadPaymentMonths(shopId);
            });
        }
        public IHttpActionResult GetPaymentYears(int shopId)
        {
            return Execute("GetPaymentYears", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.LoadPaymentYears(shopId);
            });
        }
        public IHttpActionResult GetAllPaymentTypes(int shopId)
        {
            return Execute("GetAllPaymentTypes", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetAllPaymentTypes(shopId);
            });
        }

        #endregion

        #region Delivery Area

        public IHttpActionResult GetDeliveryArea(int deliveryGroupId, int shopId)
        {
            return Execute("GetDeliveryArea", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetDeliveryArea(deliveryGroupId, shopId);
            });
        }

        #endregion

        #region Delivery Costs

        public IHttpActionResult GetDeliveryCosts(int shopId)
        {
            return Execute("GetDeliveryCosts", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetDeliveryCosts(shopId);
            });
        }
        [HttpPost]
        public IHttpActionResult SaveDeliveryCost(WsDeliveryCost selectItem, int shopId)
        {
            return Execute("SaveDeliveryCost", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.SaveDeliveryCost(selectItem, shopId);
            });
        }
        [HttpGet]
        public IHttpActionResult GetDeliveryCostsById(int id, int shopId)
        {
            return Execute("GetDeliveryCostsById", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetDeliveryCostsById(id, shopId);
            });
        }

        #endregion

        #region Shop User

        public IHttpActionResult GetShopUserById(int userId, int shopId)
        {
            return Execute("GetShopUserById", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetShopUserById(userId, shopId);
            });
        }
        public IHttpActionResult GetAllShopUsers(int shopId)
        {
            return Execute("GetAllShopUsers", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetAllShopUsers(shopId);
            });
        }
        [HttpPost]
        public IHttpActionResult SaveShopUser(WsShopUser newShopUser, int shopId)
        {
            return Execute("SaveShopUser", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.SaveShopUser(newShopUser, shopId);
            });
        }
        [HttpGet]
        public IHttpActionResult RemoveShopUseById(int userId, int shopId)
        {
            return Execute("RemoveShopUseById", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.RemoveShopUseById(userId, shopId);
            });
        }


        [HttpGet]
        public IHttpActionResult GetEncryptedPassword(string password, int shopId)
        {
            return Execute("GetEncryptedPassword", shopId, () => _shopService.GetEncryptedPassword(password, shopId));
        }

        #endregion

        #region Table Command

        public IHttpActionResult GetTableCommand(int shopId)
        {
            return Execute("GetTableCommand", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetTableCommand(shopId);
            });
        }

        #endregion

        #region Table Alias
        public IHttpActionResult GetTableAlias(int shopId)
        {
            return Execute("GetTableAlias", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetTableAlias(shopId);
            });
        }

        #endregion

        #region Drivers

        public IHttpActionResult GetAllDrivers(int shopId)
        {
            return Execute("GetAllDrivers", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetAllDrivers(shopId);
            });
        }
        #endregion

        #region Customers

        [HttpGet]
        public IHttpActionResult GetAllCustomer(int shopId)
        {
            return Execute("GetAllCustomer", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetAllCustomer(shopId);
            });
        }

        [HttpPost]
        public IHttpActionResult RegisterCustomer(WsShopCustomer customer, int shopId)
        {
            return Execute("RegisterCustomer", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.RegisterCustomer(customer,shopId);
            });
        }

        [HttpPost]
        public IHttpActionResult UpdateCustomerInfo(WsShopCustomer customer, int shopId)
        {
            return Execute("UpdateCustomerInfo", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.UpdateCustomerInfo(customer, shopId);
            });
        }


        [HttpPost]
        public IHttpActionResult ResetCustomerPassword(WsShopCustomer customer, int shopId)
        {
            return Execute("ResetCustomerPassword", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.ResetCustomerPassword(customer, shopId);
            });
        }



        

        [HttpGet]
        public IHttpActionResult GetCustomerById(int id, int shopId)
        {
            return Execute("GetCustomerById", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetCustomerById(id,shopId);
            });
        }

        [HttpPost]
        public IHttpActionResult VerifySms(WsShopCustomer registeredCustomer, int shopId)
        {
            return Execute("VerifySms", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.VerifySms(registeredCustomer, shopId);
            });
        }


        [HttpPost]
        public IHttpActionResult ActivateBySms(WsShopCustomer registeredCustomer, int shopId)
        {
            return Execute("ActivateBySms", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.ActivateBySms(registeredCustomer, shopId);
            });
        }


        [HttpGet]
        public IHttpActionResult Forgetpassword(string email, int shopId)
        {
            return Execute("Forgetpassword", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.Forgetpassword(email, shopId);
            });
        }


        [HttpGet]
        public IHttpActionResult LoginCustomer(string email, string password, int shopId)
        {
            return Execute("LoginCustomer", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.LoginCustomer(email, password,shopId);
            });
        }


        #endregion

        #region Reports

        [HttpPost]
        public IHttpActionResult GetReport(WsReport search, int shopId)
        {
            return Execute("GetReport", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetReport(search, shopId);
            });
        }

        [HttpPost]
        public IHttpActionResult GetAdminReport(WsReport search, int shopId)
        {
            return Execute("GetAdminReport", shopId, () =>
            {
                return _shopService.GetAdminReport(search, shopId);
            });
        }

        


        #endregion

        #region Server Status

        [HttpGet]
        public IHttpActionResult SyncStatus(int shopId)
        {
            return Execute("SyncStatus", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.SyncStatus(shopId);
            });
        }


        #endregion

        #region Shop News

        [HttpGet]
        public IHttpActionResult GetShopNews(int shopId)
        {
            return Execute("GetShopNews", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetShopNews(shopId);
            });
        }
        [HttpPost]
        public IHttpActionResult SaveShopNews(WsShopNews selectItem, int shopId)
        {
            return Execute("SaveShopNews", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.SaveShopNews(selectItem, shopId);
            });
        }
        [HttpGet]
        public IHttpActionResult GetShopNewsById(int id, int shopId)
        {
            return Execute("GetShopNewsById", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetShopNewsById(id, shopId);
            });
        }
        [HttpGet]
        public IHttpActionResult RemoveShopNewsById(int id, int shopId)
        {
            return Execute("RemoveShopNewsById", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.RemoveShopNewsById(id, shopId);
            });
        }

        #endregion

        #region Menu Sub Items

        [HttpGet]
        public IHttpActionResult GetAllMenuSubItems(int shopId)
        {
            return Execute("GetAllMenuSubItems", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetAllMenuSubItems(shopId);
            });
        }

        [HttpGet]
        public IHttpActionResult RemoveMenuSubItemById(int id, int shopId)
        {
            return Execute("GetAllMenuSubItems", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.RemoveMenuSubItemById(id,shopId);
            });
        }

        [HttpPost]
        public IHttpActionResult SaveMenuSubItem(WsMenuSubItem selectedMenuSubItem, int shopId)
        {
            return Execute("SaveMenuSubItem", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.SaveMenuSubItem(selectedMenuSubItem, shopId);
            });
        }

        [HttpGet]
        public IHttpActionResult GetMenuSubItemsById(int id, int shopId)
        {
            return Execute("GetMenuSubItemsById", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.GetMenuSubItemsById(id,shopId);
            });
        }


        [HttpGet]
        public IHttpActionResult RemoveMenuSubItemLink(int id, int menuItemId, int shopId)
        {
            return Execute("RemoveMenuSubItemLink", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.RemoveMenuSubItemLink(id, menuItemId,shopId);
            });
        }

        [HttpGet]
        public IHttpActionResult SaveMenuSubItemWithItem(int id, int selectedMenuItemId, decimal cost, int shopId)
        {
            return Execute("SaveMenuSubItemWithItem", shopId, () =>
            {
                CheckUserPermission(shopId);
                return _shopService.SaveMenuSubItemWithItem(id, selectedMenuItemId, cost, shopId);
            });
        }

        


        #endregion

        private void CheckUserPermission(int shopId)
        {
            var headers = this.Request.Headers;
            var token = headers.GetValues("IndaSoftAuthToken").FirstOrDefault();
            var decryptedToken = TokenEncryptor.Decrypt(token);
            var decryptedTokensplit = decryptedToken.Split(',');

            if (decryptedTokensplit.Length < 2)
                throw new Exception("Unauthorized User");

            if (decryptedTokensplit[1].Trim() != shopId.ToString()
                && decryptedTokensplit[1].Trim() != "IndaSoft")
                throw new Exception("Unauthorized User");
        }

        

    }
}
