using System.Collections.Generic;
using Newtonsoft.Json;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Services;
using Takeway.Mobile.Core.Services.RestServices;

namespace JsonFileGenerator
{
    public class Generator
    {
        private readonly IShopService _takeawayService;


        public Generator()
        {
            _takeawayService=new ShopRestService();
        }

        public void Run()
        {
            //1. Generate Shop Info
            GetShopInfo();

            //2. Get menuItem Category
            var menuItemCategories = GetMenuItemCategoryByGroupId(0);

            foreach (var menuCategory in menuItemCategories)
            {
                //3. Get Category Item
                var categoryItem = GetMenuItemsByCategory(menuCategory.Id);
            }

            //4. Get Delivery OPtions
            GetDeliveryOptions();

            //5. Get System Settings
            GetSystemSettings();

            //6. WsShopUser
            GetShopUser();

            //7. Get Open Orders
            GetOpenOrders();


        }

        private void GetOpenOrders()
        {
            var response = _takeawayService.GetOpenOrders();
            JsonExporter.Export("GetOpenOrders", JsonConvert.SerializeObject(response));
        }

        private void GetShopUser()
        {
            var response = _takeawayService.AuthenticateUser("fusion","password",1);
            JsonExporter.Export("ShopUser", JsonConvert.SerializeObject(response));
        }

        private void GetSystemSettings()
        {
            var response = _takeawayService.GetSystemSettings();
            JsonExporter.Export("SystemSettings", JsonConvert.SerializeObject(response));
        }

        private void GetDeliveryOptions()
        {
            var response = _takeawayService.GetDeliveryOptions();
            JsonExporter.Export("DeliveryOptions", JsonConvert.SerializeObject(response));

            
        }

        private List<WsMenuItem> GetMenuItemsByCategory(int categoryId)
        {
            var response = _takeawayService.GetMenuItemsByCategory(categoryId);
            JsonExporter.Export(string.Format("MenuItems{0}",categoryId), JsonConvert.SerializeObject(response));
            return response;
        }

        private IEnumerable<WsMenuItemCategory> GetMenuItemCategoryByGroupId(int groupId)
        {
            var response = _takeawayService.GetMenuItemCategoryByGroupId(groupId);
            JsonExporter.Export(string.Format("MenuItemCategories"), JsonConvert.SerializeObject(response));
            return response;

        }

        private void GetShopInfo()
        {
            var response = _takeawayService.GetShopInfo();
            JsonExporter.Export(string.Format("ShopInfo"), JsonConvert.SerializeObject(response));

        }
    }
}