using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Email;
using Takeaway.Service.Contract.Entities.Health;
using Takeaway.Service.Contract.Entities.Payment;
using Takeaway.Service.Contract.Entities.Reports;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Services;
using Takeway.Mobile.Core.Utilities;

namespace Takeway.Mobile.Core.Services.RestServices
{
    public class ShopRestService : RestServiceBase, IShopService
    {
        public WsShop GetShopInfo(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetShopInfo?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsShop>(responseJson);

            });
        }

        public WsShop UpdateShopInfo(WsShop shopInfo, int updateOption, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(shopInfo);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/UpdateShopInfo?updateOption={1}&shopId={2}", Constants.SiteUrl, updateOption, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsShop>(responseJson);
            });
        }

        public WsShop GetShopStatus(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetShopStatus?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsShop>(responseJson);

            });
        }

        public List<WsShop> GetAllShopsNeedTestOrder(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetAllShopsNeedTestOrder?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsShop>>(responseJson);

            });
        }

        public List<WsMenuItemCategory> GetMenuItemCategoryByGroupId(int groupId, int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetMenuItemCategoryByGroupId?groupId={1}&shopId={2}", Constants.SiteUrl, groupId, shopId),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsMenuItemCategory>>(responseJson);
            });
        }


        public List<WsMenuItemCategory> GetAdminMenuItemCategoryByGroupId(int groupId, int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetAdminMenuItemCategoryByGroupId?groupId={1}&shopId={2}", Constants.SiteUrl, groupId, shopId),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsMenuItemCategory>>(responseJson);
            });
        }


        public WsMenuItemCategory GetMenuItemCategoryById(int id, int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetMenuItemCategoryById?id={1}&shopId={2}", Constants.SiteUrl, id, shopId),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsMenuItemCategory>(responseJson);
            });
        }

        public WsMenuItemCategory SaveMenuCategory(WsMenuItemCategory item, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(item);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/SaveMenuCategory?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsMenuItemCategory>(responseJson);
            });
        }

        public WsMenuItemCategory RemoveMenuItemCategoryById(int id, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/RemoveMenuItemCategoryById?id={1}&shopId={2}", Constants.SiteUrl,
                            id, shopId), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsMenuItemCategory>(responseJson);
            });
        }

        public List<WsMenuItem> GetMenuItemByCategoryGroupId(int groupId, int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetMenuItemByCategoryGroupId?groupId={1}&shopId={2}", Constants.SiteUrl, groupId, shopId),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsMenuItem>>(responseJson);
            });
        }

        public List<WsMenuItem> GetMenuItemsByCategory(int categoryId, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/GetMenuItemsByCategory?categoryId={1}&shopId={2}", Constants.SiteUrl,
                            categoryId, shopId), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsMenuItem>>(responseJson);
            });
        }


        public WsMenuItem RemoveMenuItemById(int id, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/RemoveMenuItemById?id={1}&shopId={2}", Constants.SiteUrl,
                            id, shopId), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsMenuItem>(responseJson);
            });
        }

        public List<WsMenuItem> GetMenuItemsList(int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/GetMenuItemsList?shopId={1}", Constants.SiteUrl, shopId), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsMenuItem>>(responseJson);
            });
        }

        public WsMenuItem SaveMenuItemWithSubItem(int extraId, int memuItemId, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/SaveMenuItemWithSubItem?extraId={1}&memuItemId={2}&shopId={3}", Constants.SiteUrl,
                            extraId, memuItemId, shopId), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsMenuItem>(responseJson);
            });
        }

        public WsMenuItem RemoveMenuItemWithSubItem(int extraId, int memuItemId, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/RemoveMenuItemWithSubItem?extraId={1}&memuItemId={2}&shopId={3}", Constants.SiteUrl,
                            extraId, memuItemId, shopId), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsMenuItem>(responseJson);
            });
        }

        public WsOrder SubmitCustomerOrder(WsOrder orderItem, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(orderItem);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/SubmitCustomerOrder?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsOrder>(responseJson);
            });
        }

        public List<WsOrder> GetOpenOrders(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(String.Format("{0}/api/ShopApi/GetOpenOrders?&shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey);
                var resultContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<WsOrder>>(resultContent);
            });
        }

        public WsOrder GetOrderById(int orderId, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(String.Format("{0}/api/ShopApi/GetOrderById?orderId={1}&shopId={2}", Constants.SiteUrl, orderId, shopId),
                        Constants.ClientTokenKey);
                var resultContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<WsOrder>(resultContent);
            });
        }

        public WsOrder CancelOrderById(WsOrder order, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(order);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/CancelOrderById?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsOrder>(responseJson);
            });
        }

        public WsOrder KillOrderById(WsOrder order, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(order);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/KillOrderById?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsOrder>(responseJson);
            });
        }

        public List<WsOrder> GetCompleteOrders(WsOrderSearch search, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(search);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/GetCompleteOrders?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsOrder>>(responseJson);
            });
        }

        public WsShopCustomer RegisterCustomer(WsShopCustomer selectedCustomer, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(selectedCustomer);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/RegisterCustomer?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsShopCustomer>(responseJson);
            });
        }

        public WsShopCustomer VerifySms(WsShopCustomer registeredCustomer, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(registeredCustomer);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/VerifySms?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsShopCustomer>(responseJson);
            });
        }

        public WsShopCustomer GetCustomerById(int id, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(String.Format("{0}/api/ShopApi/GetCustomerById?id={1}&shopId={2}", Constants.SiteUrl, id, shopId),
                        Constants.ClientTokenKey);
                var resultContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<WsShopCustomer>(resultContent);
            });
        }

        public WsShopCustomer ActivateBySms(WsShopCustomer registeredCustomer, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(registeredCustomer);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/ActivateBySms?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsShopCustomer>(responseJson);
            });
        }

        public WsShopCustomer Forgetpassword(string email, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(String.Format("{0}/api/ShopApi/Forgetpassword?email={1}&shopId={2}", Constants.SiteUrl, email, shopId),
                        Constants.ClientTokenKey);
                var resultContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<WsShopCustomer>(resultContent);
            });
        }

        public WsShopCustomer LoginCustomer(string email, string password, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(String.Format("{0}/api/ShopApi/LoginCustomer?email={1}&password={2}&shopId={3}", Constants.SiteUrl, email, password, shopId),
                        Constants.ClientTokenKey);
                var resultContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<WsShopCustomer>(resultContent);
            });
        }

        public WsShopCustomer UpdateCustomerInfo(WsShopCustomer selectedCustomer, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(selectedCustomer);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/UpdateCustomerInfo?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsShopCustomer>(responseJson);
            });
        }

        public WsShopCustomer ResetCustomerPassword(WsShopCustomer selectedCustomer, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(selectedCustomer);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/ResetCustomerPassword?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsShopCustomer>(responseJson);
            });
        }

        public List<WsSetting> GetSystemSettings(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetSystemSettings?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsSetting>>(responseJson);
            });
        }

        public List<WsSetting> GetClientSystemSettings(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetClientSystemSettings?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsSetting>>(responseJson);
            });
        }

        public List<WsDeliveryOption> GetDeliveryOptions(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetDeliveryOptions?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsDeliveryOption>>(responseJson);
            });
        }

        public List<WsDeliveryOption> GetDeliveryOptionsByTime(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetDeliveryOptionsByTime?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsDeliveryOption>>(responseJson);
            });
        }

        public List<WsDeliveryOption> GetDeliveryOptionsInternal(int shopId)
        {
            throw new NotImplementedException();
        }

        public List<WsOptionItem> GetOptionItems(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetOptionItems?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsOptionItem>>(responseJson);
            });
        }

        public WsOrder DiscountCustomerOrder(WsOrder orderItem, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(orderItem);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/DiscountCustomerOrder?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsOrder>(responseJson);
            });
        }

        public WsShopUser LoginShopAgent(string pin, bool online, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/LoginShopAgent?pin={1}&shopId={2}", Constants.SiteUrl,
                            pin, shopId), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsShopUser>(responseJson);
            });
        }


        public List<WsPaymentMonth> LoadPaymentMonths(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetPaymentMonths?shopId={1}", Constants.SiteUrl
                    , shopId),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsPaymentMonth>>(responseJson);
            });
        }

        public List<WsPaymentYear> LoadPaymentYears(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetPaymentYears?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsPaymentYear>>(responseJson);
            });
        }

        public WsPaymentInfo TakePayment(WsPayment payment, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(payment);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/TakePayment?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsPaymentInfo>(responseJson);
            });
        }

        public WsPaymentInfo TakeLoyaltyPayment(WsPayment payment, int customerId, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(payment);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/TakeLoyaltyPayment?customerId={2}&shopId={1}", Constants.SiteUrl, shopId, customerId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsPaymentInfo>(responseJson);
            });
        }

        public WsSyncStatus SyncStatus(int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/SyncStatus?shopId={1}", Constants.SiteUrl, shopId), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<WsSyncStatus>(responseJson);
            });
        }

        public WsEmailResponse EmailException(string message, int shopId)
        {
            return Execute(() =>
           {

               var response = GetService(string.Format("{0}/api/ShopApi/EmailException?message={1}&shopId={2}", Constants.SiteUrl, message, shopId), Constants.ClientTokenKey);

               var responseJson = response.Content.ReadAsStringAsync().Result;

               return JsonConvert.DeserializeObject<WsEmailResponse>(responseJson);

           });

        }

        public List<WsShopCustomer> GetAllCustomer(int shopId)
        {
            return Execute(() =>
            {

                var response = GetService(string.Format("{0}/api/ShopApi/GetAllCustomer?shopId={1}", Constants.SiteUrl, shopId), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsShopCustomer>>(responseJson);

            });
        }

        public WsReport GetReport(WsReport report, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(report);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/GetReport?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsReport>(responseJson);
            });
        }

        public WsReport GetAdminReport(WsReport report, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(report);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/GetAdminReport?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsReport>(responseJson);
            });
        }

        public WsOptionItem SaveOptionItem(WsOptionItem item, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(item);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/SaveOptionItem?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsOptionItem>(responseJson);
            });
        }

        public WsOptionItem GetOptionItemById(int id, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/GetOptionItemById?id={1}&shopId={2}", Constants.SiteUrl, id, shopId), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsOptionItem>(responseJson);
            });
        }

        public WsOptionItem RemoveOptionItemById(int id, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(String.Format("{0}/api/ShopApi/RemoveOptionItemById?userId={1}&shopId={2}", Constants.SiteUrl, id, shopId),
                        Constants.ClientTokenKey);
                var resultContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<WsOptionItem>(resultContent);
            });
        }

        public WsSetting GetSystemSettingById(int id, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/GetSystemSettingById?id={1}&shopId={2}", Constants.SiteUrl, id, shopId), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsSetting>(responseJson);
            });
        }

        public WsSetting SaveSystemSetting(WsSetting selectedItem, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(selectedItem);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/SaveSystemSetting?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsSetting>(responseJson);
            });
        }

        public WsDeliveryCost GetDeliveryCostsById(int id, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/GetDeliveryCostsById?id={1}&shopId={2}", Constants.SiteUrl, id, shopId), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsDeliveryCost>(responseJson);
            });
        }

        public WsDeliveryCost SaveDeliveryCost(WsDeliveryCost selectedItem, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(selectedItem);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/SaveDeliveryCost?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsDeliveryCost>(responseJson);
            });
        }

        public List<WsShopNews> GetShopNews(int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/GetShopNews?shopId={1}", Constants.SiteUrl, shopId), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsShopNews>>(responseJson);
            });
        }

        public WsShopNews SaveShopNews(WsShopNews selectedShopNews, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(selectedShopNews);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/SaveShopNews?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsShopNews>(responseJson);
            });
        }

        public WsShopNews GetShopNewsById(int id, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/GetShopNewsById?id={1}&shopId={2}", Constants.SiteUrl, id, shopId), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsShopNews>(responseJson);
            });
        }

        public WsShopNews RemoveShopNewsById(int id, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(String.Format("{0}/api/ShopApi/RemoveShopNewsById?id={1}&shopId={2}", Constants.SiteUrl, id, shopId),
                        Constants.ClientTokenKey);
                var resultContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<WsShopNews>(resultContent);
            });
        }

        public List<WsMenuSubItem> GetAllMenuSubItems(int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/GetAllMenuSubItems?shopId={1}", Constants.SiteUrl, shopId), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsMenuSubItem>>(responseJson);
            });
        }

        public WsMenuSubItem RemoveMenuSubItemById(int id, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(String.Format("{0}/api/ShopApi/RemoveMenuSubItemById?id={1}&shopId={2}", Constants.SiteUrl, id, shopId),
                        Constants.ClientTokenKey);
                var resultContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<WsMenuSubItem>(resultContent);
            });
        }

        public WsMenuSubItem SaveMenuSubItem(WsMenuSubItem selectedMenuSubItem, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(selectedMenuSubItem);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/SaveMenuSubItem?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsMenuSubItem>(responseJson);
            });
        }

        public WsMenuSubItem GetMenuSubItemsById(int id, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/GetMenuSubItemsById?id={1}&shopId={2}", Constants.SiteUrl, id, shopId), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsMenuSubItem>(responseJson);
            });
        }

        public WsMenuSubItem RemoveMenuSubItemLink(int id, int menuItemId, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(String.Format("{0}/api/ShopApi/RemoveMenuSubItemLink?id={1}&menuItemId={2}&shopId={3}", Constants.SiteUrl, id, menuItemId, shopId),
                        Constants.ClientTokenKey);
                var resultContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<WsMenuSubItem>(resultContent);
            });
        }

        public WsMenuSubItem SaveMenuSubItemWithItem(int id, int selectedMenuItemId, decimal cost, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/SaveMenuSubItemWithItem?id={1}&selectedMenuItemId={2}&cost={3}&shopId={4}", Constants.SiteUrl, id, selectedMenuItemId, cost, shopId), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsMenuSubItem>(responseJson);
            });
        }

        public string GetEncryptedPassword(string password, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/GetEncryptedPassword?password={1}&shopId={2}", Constants.SiteUrl, password, shopId), Constants.ClientTokenKey);
                var responseJson = response.Content.ReadAsStringAsync().Result;
                return responseJson;
            });
        }

        public WsMenuItem GetMenuItemById(int id, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/GetMenuItemById?id={1}&shopId={2}", Constants.SiteUrl, id, shopId), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsMenuItem>(responseJson);
            });
        }

        public WsMenuItem SaveMenuItem(WsMenuItem menuItem, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(menuItem);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/SaveMenuItem?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsMenuItem>(responseJson);
            });
        }

        public List<WsShopUser> GetAllShopUsers(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetAllShopUsers?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsShopUser>>(responseJson);
            });
        }

        public List<WsTableCommand> GetTableCommand(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetTableCommand?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsTableCommand>>(responseJson);
            });
        }

        public List<WsDriver> GetAllDrivers(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetAllDrivers?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsDriver>>(responseJson);
            });
        }

        public WsOrder UpdateOrder(WsOrder selectedOrder, int updateOption, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(selectedOrder);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/UpdateOrder?updateOption={1}&shopId={2}", Constants.SiteUrl, updateOption, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsOrder>(responseJson);
            });
        }

        public List<WsPaymentType> GetAllPaymentTypes(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetAllPaymentTypes?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsPaymentType>>(responseJson);
            });
        }

        public List<WsTableAlias> GetTableAlias(int shopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetTableAlias?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsTableAlias>>(responseJson);
            });
        }

        public WsOrder UpdateCloudOrder(WsOrder myOrder, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(myOrder);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/UpdateCloudOrder?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsOrder>(responseJson);
            });
        }

        public List<WsDeliveryArea> GetDeliveryArea(int deliveryGroupId, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(String.Format("{0}/api/ShopApi/GetDeliveryArea?deliveryGroupId={1}&shopId={2}", Constants.SiteUrl, deliveryGroupId, shopId),
                        Constants.ClientTokenKey);
                var resultContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<WsDeliveryArea>>(resultContent);
            });
        }

        public List<WsDeliveryCost> GetDeliveryCosts(int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(String.Format("{0}/api/ShopApi/GetDeliveryCosts?shopId={1}", Constants.SiteUrl, shopId),
                        Constants.ClientTokenKey);
                var resultContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<WsDeliveryCost>>(resultContent);
            });
        }


        public WsShopUser SaveShopUser(WsShopUser newShopUser, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(newShopUser);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/SaveShopUser?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsShopUser>(responseJson);
            });
        }

        public WsShopUser GetShopUserById(int userId, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(String.Format("{0}/api/ShopApi/GetShopUserById?userId={1}&shopId={2}", Constants.SiteUrl, userId, shopId),
                        Constants.ClientTokenKey);
                var resultContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<WsShopUser>(resultContent);
            });
        }

        public WsShopUser RemoveShopUseById(int userId, int shopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(String.Format("{0}/api/ShopApi/RemoveShopUseById?userId={1}&shopId={2}", Constants.SiteUrl, userId, shopId),
                        Constants.ClientTokenKey);
                var resultContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<WsShopUser>(resultContent);
            });
        }

        
    }
}