using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common.Net4.Utility.Attribute;
using Core.Common.Utility;
using Takeaway.BusinessEngine.Exception;
using Takeaway.Data.Entities.Authentication;
using Takeaway.Data.Entities.Enum;
using Takeaway.Data.Entities.Settings;
using Takeaway.Data.Entities.Shops;
using Takeaway.Data.Net4.Repositories;

namespace Takeaway.BusinessEngine.BusinessEngine
{
    public interface IShopOfflineBusinessEngine : IBusinessEngine<IShopOfflineBusinessEngine>
    {
        List<OfflineSetting> GetOfflineSettings();
        void SaveSetting(string settingKey, string valueString);
        ShopInfo GetShopInfo();
        List<MenuItem> GetMenuItemsByCategory(int categoryId);
        List<DeliveryOption> GetDeliveryOptions();
        List<DeliveryOption> GetDeliveryOptionsInternal();
        List<MenuItem> GetHotMenuItems();
        List<MenuItem> GetHotMenuItemsInternal();
        Customer GetCustomerByPhoneNumber(string customerPhone);
        Order SaveOrder(Order orderTransformed);
        List<MenuItemCategory> GetMenuItemCategoryByGroupId(int groupId);
        void RemoveAllCategories();
        void SaveCategoryInLocalDb(List<MenuItemCategory> categories);
        void RemoveShopInfo();
        void SaveShopInfo(ShopInfo shopInfoTransformed);
        void RemoveAllItems();
        void SaveMenuItems(List<MenuItem> menuItemtransformed);
        bool IsInitialRun(string settingKey);


        void RemoveDeliveryOptions();
        void SaveDeliveryOptions(List<DeliveryOption> deliveryOptionsTransformed);
        List<Order> GetPendingOrder();
        void RemovePendingOrder(Order order);
        List<OptionItem> GetOptionItems();
        void RemoveAllOptionItems();
        void SaveOptionItems(List<OptionItem> listOfItemsTransformed);

        List<MenuItemCategoryGroup> GetMenuItemCategoryGroup();
        Tables GetTableById(int tableId);
        void UpdateTableInfo(Tables saveTable);

        bool VerifyUserRights(string userPin);
        ShopUser LoginShopUserByPin(string userPin);
        ShopUser GetUserByPin(string pin);
        void RemoveAllShopUser();
        void SaveShopUsers(List<ShopUser> itemsTransformed);
        List<Tables> GetAllTables();
    }


    [OfflineBusinessEngine]
    public class ShopOfflineBusinessEngine : BusinessEngineBase, IShopOfflineBusinessEngine
    {

        private readonly IOfflineSettingRepository _offlineSettingRepository;
        private readonly IShopInfoRepository _shopInfoRepository;
        private readonly IMenuItemCategoryRepository _menuItemCategoryRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryOptionRepository _deliveryOptionRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOptionItemRepository _optionItemRepository;
        private readonly ITablesRespository _tablesRespository;
        private readonly IShopUserRepository _shopUserRepository;


        public ShopOfflineBusinessEngine(IOfflineSettingRepository offlineSettingRepository,
                                         IShopInfoRepository shopInfoRepository,
                                         IMenuItemCategoryRepository menuItemCategoryRepository,
                                         IMenuItemRepository menuItemRepository,
                                         IOrderRepository orderRepository,
                                         IDeliveryOptionRepository deliveryOptionRepository,
                                         ICustomerRepository customerRepository,
                                         IOptionItemRepository optionItemRepository, 
                                         ITablesRespository tablesRespository, 
                                         IShopUserRepository shopUserRepository)
        {
            _offlineSettingRepository = offlineSettingRepository;
            _shopInfoRepository = shopInfoRepository;
            _menuItemCategoryRepository = menuItemCategoryRepository;
            _menuItemRepository = menuItemRepository;
            _orderRepository = orderRepository;
            _deliveryOptionRepository = deliveryOptionRepository;
            _customerRepository = customerRepository;
            _optionItemRepository = optionItemRepository;
            _tablesRespository = tablesRespository;
            _shopUserRepository = shopUserRepository;
        }

        public List<OfflineSetting> GetOfflineSettings()
        {
            return _offlineSettingRepository.GetAllSettings();
        }

        public void SaveSetting(string settingKey, string valueString)
        {
            _offlineSettingRepository.SaveSetting(settingKey, valueString);
        }

        public ShopInfo GetShopInfo()
        {
            return Execute(() =>
            {
                var shopInfo = _shopInfoRepository.GetShopInfo();

                if (shopInfo == null)
                    throw new BusinessEngineException(new System.Exception("Cannot load Shop Info"));
                shopInfo.IsOpen = false;

                return shopInfo;

            });
        }

        public List<MenuItemCategory> GetMenuItemCategoryByGroupId(int groupId)
        {
            return Execute(() => _menuItemCategoryRepository.GetMenuItemCategoryByGroupId(groupId));
        }

        public void RemoveAllCategories()
        {
            Execute(() => _menuItemCategoryRepository.RemoveAllCategories());
        }

        public void SaveCategoryInLocalDb(List<MenuItemCategory> categories)
        {
            Execute(() => _menuItemCategoryRepository.SaveCategories(categories));
        }

        public void RemoveShopInfo()
        {
            Execute(() => _shopInfoRepository.RemoveShopInfo());
        }

        public void SaveShopInfo(ShopInfo shopInfoTransformed)
        {
            Execute(() => _shopInfoRepository.SaveShopInfo(shopInfoTransformed));
        }

        public void RemoveAllItems()
        {
            Execute(() => _menuItemRepository.RemoveAllItems());
        }

        public void SaveMenuItems(List<MenuItem> menuItemtransformed)
        {
            Execute(() => _menuItemRepository.SaveMenuItems(menuItemtransformed));
        }

        public bool IsInitialRun(string settingKey)
        {
            return Execute(() =>
            {
                var setting = _offlineSettingRepository.GetSettingByKey(settingKey);
                return setting == null || BooleanStringUtil.ConvertStringValue(setting.SettingValue);
            });
        }



        public void RemoveDeliveryOptions()
        {
            Execute(() =>
            {
                _deliveryOptionRepository.RemoveDeliveryOptions();

            });
        }

        public void SaveDeliveryOptions(List<DeliveryOption> deliveryOptionsTransformed)
        {
            Execute(() =>
            {
                _deliveryOptionRepository.SaveDeliveryOptions(deliveryOptionsTransformed);

            });
        }

        public List<Order> GetPendingOrder()
        {
            return Execute(() => _orderRepository.GetPendingOrders());
        }

        public void RemovePendingOrder(Order order)
        {
            Execute(() => _orderRepository.RemoveOrder(order));
        }

        public List<OptionItem> GetOptionItems()
        {
            return Execute(() => _optionItemRepository.GetAll());

        }

        public void RemoveAllOptionItems()
        {
            Execute(() =>
            {
                _optionItemRepository.RemoveAll();

            });
        }

        public void SaveOptionItems(List<OptionItem> listOfItemsTransformed)
        {
            Execute(() =>
            {
                _optionItemRepository.SaveOptionItems(listOfItemsTransformed);

            });
        }

        public List<MenuItemCategoryGroup> GetMenuItemCategoryGroup()
        {

            throw new NotImplementedException();

        }

        public Tables GetTableById(int tableId)
        {
            return Execute(() => _tablesRespository.GetTableById(tableId));
        }

        public void UpdateTableInfo(Tables saveTable)
        {
            _tablesRespository.SaveTableById(saveTable);
        }

        public bool VerifyUserRights(string userPin)
        {
            var user = _shopUserRepository.GetUserByPin(userPin);
            if(user==null)
                return false;

            return user.UserGroupId > 0;
        }

        public ShopUser LoginShopUserByPin(string userPin)
        {
            return _shopUserRepository.GetUserByPin(userPin);
        }

        public ShopUser GetUserByPin(string pin)
        {
            return _shopUserRepository.GetUserByPin(pin);
        }

        public void RemoveAllShopUser()
        {
            _shopUserRepository.RemoveAllShopUser();
        }

        public void SaveShopUsers(List<ShopUser> itemsTransformed)
        {
            Execute(() =>
            {
                _shopUserRepository.SaveShopUsers(itemsTransformed);

            });
        }

        public List<Tables> GetAllTables()
        {
            return Execute(() => _tablesRespository.GetAllTables());
        }


        public List<MenuItem> GetMenuItemsByCategory(int categoryId)
        {
            return Execute(() =>
            {
                var menuItems = _menuItemRepository.GetActiveMenuItemsByCategoryId(categoryId);

                //Set HasSubOrder 
                foreach (var menuItem in menuItems.Where(menuItem => menuItem.MenuItemSubItems.Count > 0))
                {
                    menuItem.HasSubOrder = true;
                }
                return menuItems;
            });
        }

        public List<DeliveryOption> GetDeliveryOptions()
        {
            return Execute(() => _deliveryOptionRepository.GetExternalDeliveryOption());
        }

        public List<DeliveryOption> GetDeliveryOptionsInternal()
        {
            return Execute(() => _deliveryOptionRepository.GetInternalDeliveryOption());
        }

        public List<MenuItem> GetHotMenuItems()
        {
            return Execute(() =>
            {
                var menuItems = _menuItemRepository.GetHotMenuItems();

                //Set HasSubOrder 
                foreach (var menuItem in menuItems.Where(menuItem => menuItem.MenuItemSubItems.Count > 0))
                {
                    menuItem.HasSubOrder = true;
                }
                return menuItems;
            });

        }

        public List<MenuItem> GetHotMenuItemsInternal()
        {
            return Execute(() =>
            {
                var menuItems = _menuItemRepository.GetHotMenuItemsInternal();

                //Set HasSubOrder 
                foreach (var menuItem in menuItems.Where(menuItem => menuItem.MenuItemSubItems.Count > 0))
                {
                    menuItem.HasSubOrder = true;
                }
                return menuItems;
            });
        }

        public Customer GetCustomerByPhoneNumber(string customerPhone)
        {
            return Execute(() => _customerRepository.GetCustomerByPhone(customerPhone));
        }

        public Order SaveOrder(Order requestOrder)
        {
            return Execute(() =>
            {
                requestOrder.Created = DateTime.Now;
                requestOrder.StatusId = (int)OrderStatusIdEnum.Open;

                foreach (var orderItem in requestOrder.OrderItems)
                {
                    orderItem.Created = DateTime.Now;

                    foreach (var selection in orderItem.OrderItemsSelections)
                        selection.Created = DateTime.Now;

                }

                _orderRepository.Add(requestOrder);

                if (string.IsNullOrEmpty(requestOrder.PhoneNumber))
                    return requestOrder;


                var customer = _customerRepository.GetCustomerByPhone(requestOrder.PhoneNumber);
                if (customer == null)
                {

                    var newCustomer = new Customer()
                    {
                        FullName = requestOrder.ContactName,
                        Phone1 = requestOrder.PhoneNumber,
                        Email = requestOrder.Email,
                        Address1 = requestOrder.Address,
                        Created = requestOrder.Created,

                    };

                    if (requestOrder.DeliveryCharge > 0)
                        newCustomer.DeliveryCharge = requestOrder.DeliveryCharge;

                    _customerRepository.Add(newCustomer);



                }
                else
                {
                    customer.Email = requestOrder.Email;
                    customer.Address1 = requestOrder.Address;
                    customer.FullName = requestOrder.ContactName;
                    if (requestOrder.DeliveryCharge > 0)
                        customer.DeliveryCharge = requestOrder.DeliveryCharge;

                    _customerRepository.UpdateCustomer(customer);
                }





                return requestOrder;

            });
        }
    }
}