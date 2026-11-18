using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Health;
using Takeaway.Service.Contract.Entities.Reports;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;
using Takeway.Mobile.Core.Services.RestServices;
using Takeway.Mobile.Core.Utilities;

namespace Takeaway.Admin.Util.Services
{
    public interface IOnlineService
    {
        WsShop GetShopInfo(int? shopid = Constants.ShopId);
        List<WsShop> GetAllShopsNeedTestOrder(int? shopid = Constants.ShopId);
        List<WsMenuItemCategory> GetMenuItemCategory(int? shopid = Constants.ShopId);
        List<WsMenuItem> GetMenuItemByCategoryGroupId(int external, int shopid);
        List<WsMenuItem> GetMenuItemsByCategory(int categoryId, int? shopid = Constants.ShopId);
        List<WsDeliveryCost> GetDeliveryCosts(int? shopid = Constants.ShopId);
        List<WsDeliveryOption> GetDeliveryOptions(int? shopid = Constants.ShopId);
        List<WsDriver> GetDrivers(int? shopid = Constants.ShopId);
        List<WsSetting> GetSettings(int? shopid = Constants.ShopId);
        List<WsOptionItem> GetOptionItems(int? shopid = Constants.ShopId);
        List<WsPaymentType> GetPaymentTypes(int? shopid = Constants.ShopId);
        List<WsShopUser> GetShopUsers(int? shopid = Constants.ShopId);
        List<WsTableAlias> GetTableAlias(int? shopid = Constants.ShopId);
        List<WsTableCommand> GetTableCommand(int? shopid = Constants.ShopId);
        List<WsShopCustomer> GetCustomers(int? shopid = Constants.ShopId);
        List<WsOrder> GetOpenOrders(int? shopid = Constants.ShopId);

        WsOrder UpdateCloudOrder(WsOrder wsOrder, int? shopid = Constants.ShopId);

        WsOrder SubmitCustomerOrder(WsOrder order, int shopId);
        WsSyncStatus SyncStatus(int? shopid = Constants.ShopId);
        WsReport GetReport(WsReport report, int? shopid = Constants.ShopId);


        
    }


    public class OnlineService: RestServiceBase,IOnlineService
    {
        public WsShop GetShopInfo(int? shopid = Constants.ShopId)
        {

            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetShopInfo?shopId={1}", Constants.SiteUrl, shopid),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsShop>(responseJson);

            });
        }

       
        public List<WsShop> GetAllShopsNeedTestOrder(int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetAllShopsNeedTestOrder?shopId={1}", Constants.SiteUrl, shopid),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsShop>>(responseJson);

            });
        }

        public List<WsMenuItemCategory> GetMenuItemCategory(int? shopid = Constants.ShopId)
        {

            var collection = new List<WsMenuItemCategory>();
            collection.AddRange(GetMenuItemCategoryByGroupId((int)MenuItemCategoryGroupEnum.External, shopid.Value));
            collection.AddRange(GetMenuItemCategoryByGroupId((int)MenuItemCategoryGroupEnum.ExternalOthers, shopid.Value));
            collection.AddRange(GetMenuItemCategoryByGroupId((int)MenuItemCategoryGroupEnum.InternalStandard, shopid.Value));
            collection.AddRange(GetMenuItemCategoryByGroupId((int)MenuItemCategoryGroupEnum.InternalLunch, shopid.Value));

            return collection;
        }

        public List<WsMenuItem> GetMenuItemByCategoryGroupId(int groupId, int shopid)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/GetMenuItemByCategoryGroupId?groupId={1}&shopId={2}", Constants.SiteUrl,
                            groupId, shopid), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsMenuItem>>(responseJson);
            });
        }

        public List<WsMenuItem> GetMenuItemsByCategory(int categoryId, int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(
                        string.Format("{0}/api/ShopApi/GetMenuItemsByCategory?categoryId={1}&shopId={2}", Constants.SiteUrl,
                            categoryId, shopid), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsMenuItem>>(responseJson);
            });
        }

        public List<WsDeliveryCost> GetDeliveryCosts(int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {
                var response =
                    GetService(String.Format("{0}/api/ShopApi/GetDeliveryCosts?shopId={1}", Constants.SiteUrl, shopid),
                        Constants.ClientTokenKey);
                var resultContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<WsDeliveryCost>>(resultContent);
            });
        }

        public List<WsDeliveryOption> GetDeliveryOptions(int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetDeliveryOptionsInternal?shopId={1}", Constants.SiteUrl, shopid),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsDeliveryOption>>(responseJson);
            });
        }

        public List<WsDriver> GetDrivers(int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetAllDrivers?shopId={1}", Constants.SiteUrl, shopid),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsDriver>>(responseJson);
            });
        }

        public List<WsSetting> GetSettings(int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetClientSystemSettings?shopId={1}", Constants.SiteUrl, shopid),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsSetting>>(responseJson);
            });
        }

        public List<WsOptionItem> GetOptionItems(int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetOptionItems?shopId={1}", Constants.SiteUrl, shopid),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsOptionItem>>(responseJson);
            });
        }

        public List<WsPaymentType> GetPaymentTypes(int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetAllPaymentTypes?shopId={1}", Constants.SiteUrl, shopid),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsPaymentType>>(responseJson);
            });
        }

        public List<WsShopUser> GetShopUsers(int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetAllShopUsers?shopId={1}", Constants.SiteUrl, shopid),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsShopUser>>(responseJson);
            });
        }

        public List<WsTableAlias> GetTableAlias(int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetTableAlias?shopId={1}", Constants.SiteUrl, shopid),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsTableAlias>>(responseJson);
            });
        }

        public List<WsTableCommand> GetTableCommand(int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {
                var response = GetService(string.Format("{0}/api/ShopApi/GetTableCommand?shopId={1}", Constants.SiteUrl, shopid),
                    Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsTableCommand>>(responseJson);
            });
        }

        public List<WsShopCustomer> GetCustomers(int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {

                var response = GetService(string.Format("{0}/api/ShopApi/GetAllCustomer?shopId={1}", Constants.SiteUrl, shopid), Constants.ClientTokenKey);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<WsShopCustomer>>(responseJson);

            });
        }

        public List<WsOrder> GetOpenOrders(int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {
                var response = GetService(String.Format("{0}/api/ShopApi/GetOpenOrders?shopId={1}", Constants.SiteUrl, shopid),
                    Constants.ClientTokenKey);
                var resultContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<WsOrder>>(resultContent);
            });
        }

        public WsOrder UpdateCloudOrder(WsOrder wsOrder, int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(wsOrder);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/UpdateCloudOrder?shopId={1}", Constants.SiteUrl, shopid),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<WsOrder>(responseJson);
            });
        }

        public WsOrder SubmitCustomerOrder(WsOrder order, int shopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(order);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/SubmitCustomerOrder?shopId={1}", Constants.SiteUrl, shopId),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsOrder>(responseJson);
            });
        }

        public WsSyncStatus SyncStatus(int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {
                var response = GetService(String.Format("{0}/api/ShopApi/SyncStatus?shopId={1}", Constants.SiteUrl, shopid),
                    Constants.ClientTokenKey);
                var resultContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<WsSyncStatus>(resultContent);
            });
        }

        public WsReport GetReport(WsReport report, int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {
                var jsonString = JsonConvert.SerializeObject(report);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = PostService(string.Format("{0}/api/ShopApi/GetReport?shopId={1}", Constants.SiteUrl, shopid),
                    Constants.ClientTokenKey, content);

                var responseJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<WsReport>(responseJson);
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
    }
}