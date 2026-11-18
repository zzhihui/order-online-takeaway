using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Resources;
using Core.Common.Utility;
using FluentDateTime;
using Takeaway.BusinessEngine.Exception;
using Takeaway.BusinessEngine.Utility;
using Takeaway.Data.Entities.Authentication;
using Takeaway.Data.Entities.Enum;
using Takeaway.Data.Entities.Health;
using Takeaway.Data.Entities.Payment;
using Takeaway.Data.Entities.Settings;
using Takeaway.Data.Entities.Shops;
using Takeaway.Data.Entities.SMS;
using Takeaway.Data.Repositories;

namespace Takeaway.BusinessEngine.BusinessEngine
{




    [BusinessEngine]
    public partial class ShopBusinessEngine : BusinessEngineBase, IShopBusinessEngine
    {
        private readonly IMenuItemCategoryRepository _menuItemCategoryRepository;
        private readonly IShopInfoRepository _shopInfoRepository;
        private readonly IShopNewsRepository _shopNewsRepository;
        private readonly IShopGeneralHourRepository _shopGeneralHourRepository;
        private readonly IShopSpecialHourRepository _shopSpecialHourRepository;
        private readonly IServerSettingRepository _serverSettingRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryOptionRepository _deliveryOptionRepository;
        private readonly ISyncStatusRepository _syncStatusRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDiscountOfferRepository _discountOfferRepository;
        private readonly ILicenceInfoRepository _licenceInfoRepository;
        private readonly IStoredProcedureRepository _storedProcedureRepository;
        private readonly IOptionItemRepository _optionItemRepository;
        private readonly IShopUserRepository _shopUserRepository;
        private readonly IMenuItemCategoryGroupRepository _menuItemCategoryGroupRepository;
        private readonly ITableCommandRepository _tableCommandRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        private readonly ITableAliasRepository _tableAliasRepository;
        private readonly IDeliveryAreaRepository _deliveryAreaRepository;
        private readonly IDeliveryCostRepository _deliveryCostRepository;
        private readonly IMenuSubItemRepository _menuSubItemRepository;
        private readonly IMenuSubItemItemsRepository _menuSubItemItemsRepository;
        private readonly IMenuItemSubItemRepository _menuItemSubItemRepository;

        private readonly IEncryptionHelper _encryptionHelper;
        private readonly IAuthSmsCodeUtil _authSmsCodeUtil;
        private readonly ISmsUtil _smsUtil;
        public ShopBusinessEngine(IMenuItemCategoryRepository menuItemCategoryRepository,
                                  IShopInfoRepository shopInfoRepository,
                                  IShopNewsRepository shopNewsRepository,
                                  IShopGeneralHourRepository shopGeneralHourRepository,
                                  IShopSpecialHourRepository shopSpecialHourRepository,
                                  IServerSettingRepository serverSettingRepository,
                                  IMenuItemRepository menuItemRepository,
                                  IOrderRepository orderRepository,
                                  IDeliveryOptionRepository deliveryOptionRepository,
                                  ISyncStatusRepository syncStatusRepository,
                                  ICustomerRepository customerRepository,
                                  IDiscountOfferRepository discountOfferRepository,
                                  ILicenceInfoRepository licenceInfoRepository,
                                  IStoredProcedureRepository storedProcedureRepository,
                                  IOptionItemRepository optionItemRepository,
                                  IShopUserRepository shopUserRepository,
                                  IMenuItemCategoryGroupRepository menuItemCategoryGroupRepository,
                                  ITableCommandRepository tableCommandRepository,
                                  ITableAliasRepository tableAliasRepository,
                                  IDriverRepository driverRepository,
                                  IPaymentTypeRepository paymentTypeRepository,
                                  IDeliveryAreaRepository deliveryAreaRepository,
                                  IEncryptionHelper encryptionHelper,
                                  IDeliveryCostRepository deliveryCostRepository,
                                  IMenuSubItemRepository menuSubItemRepository,
                                  IMenuSubItemItemsRepository menuSubItemItemsRepository,
                                  IMenuItemSubItemRepository menuItemSubItemRepository,
                                  IAuthSmsCodeUtil authSmsCodeUtil, ISmsUtil smsUtil)
        {
            _menuItemCategoryRepository = menuItemCategoryRepository;
            _shopInfoRepository = shopInfoRepository;
            _shopNewsRepository = shopNewsRepository;
            _shopGeneralHourRepository = shopGeneralHourRepository;
            _shopSpecialHourRepository = shopSpecialHourRepository;
            _serverSettingRepository = serverSettingRepository;
            _menuItemRepository = menuItemRepository;
            _orderRepository = orderRepository;
            _deliveryOptionRepository = deliveryOptionRepository;
            _syncStatusRepository = syncStatusRepository;
            _customerRepository = customerRepository;
            _discountOfferRepository = discountOfferRepository;
            _licenceInfoRepository = licenceInfoRepository;
            _storedProcedureRepository = storedProcedureRepository;
            _optionItemRepository = optionItemRepository;
            _shopUserRepository = shopUserRepository;
            _menuItemCategoryGroupRepository = menuItemCategoryGroupRepository;
            _tableCommandRepository = tableCommandRepository;
            _driverRepository = driverRepository;
            _paymentTypeRepository = paymentTypeRepository;
            _tableAliasRepository = tableAliasRepository;
            _deliveryAreaRepository = deliveryAreaRepository;
            _deliveryCostRepository = deliveryCostRepository;
            _menuSubItemRepository = menuSubItemRepository;
            _menuSubItemItemsRepository = menuSubItemItemsRepository;
            _menuItemSubItemRepository = menuItemSubItemRepository;
            _authSmsCodeUtil = authSmsCodeUtil;
            _smsUtil = smsUtil;
            _encryptionHelper = encryptionHelper;
        }

        public ShopInfo GetShopInfo(int shopId)
        {

            return Execute(() =>
            {


                var shopInfo = _shopInfoRepository.GetShopInfo(shopId);

                if (shopInfo == null)
                    throw new BusinessEngineException(new System.Exception("Cannot load Shop Info"));

                //Shop News
                var shopNews = _shopNewsRepository.GetLast5News(shopId);
                shopInfo.ShopNews = shopNews;

                //Shop Hour
                var shopGeneralHours = _shopGeneralHourRepository.GetShopGeneralHours(shopId);
                shopInfo.ShopHours = shopGeneralHours;

                //Shop hour special 
                var shopHourSpecial = _shopSpecialHourRepository.GetValidSpecials(shopId);
                shopInfo.ShopSpecialHours = shopHourSpecial;

                //Build IS Open
                shopInfo.IsOpen = CalculateIsOpen(shopGeneralHours, shopHourSpecial, DateTimeUtil.GetCurrentTime()) && (shopInfo.IsOpenByOwner.HasValue && shopInfo.IsOpenByOwner.Value);
                shopInfo.CollectionOptions = GetCollectionOptions(shopGeneralHours);
                return shopInfo;

            });
        }

        public ShopInfo GetShopStatus(int shopId)
        {
            return Execute(() =>
            {

                var shopInfo = _shopInfoRepository.GetShopInfo(shopId);

                if (shopInfo == null)
                    throw new BusinessEngineException(new System.Exception("Cannot load Shop Info"));

                //Shop Hour
                var shopGeneralHours = _shopGeneralHourRepository.GetShopGeneralHours(shopId);
                shopInfo.ShopHours = shopGeneralHours;

                //Shop hour special 
                var shopHourSpecial = _shopSpecialHourRepository.GetValidSpecials(shopId);
                shopInfo.ShopSpecialHours = shopHourSpecial;

                //Build IS Open
                shopInfo.IsOpen = CalculateIsOpen(shopGeneralHours, shopHourSpecial, DateTimeUtil.GetCurrentTime()) && (shopInfo.IsOpenByOwner.HasValue && shopInfo.IsOpenByOwner.Value);
                shopInfo.CollectionOptions = GetCollectionOptions(shopGeneralHours);

                return shopInfo;

            });
        }

        public List<ServerSetting> GetSystemSettings(int shopId)
        {
            return Execute(() => _serverSettingRepository.GetAllSystemSetting(shopId));
        }

        public List<MenuItem> GetMenuItemsByCategory(int categoryId, int shopId)
        {
            return Execute(() =>
            {
                var menuItems = _menuItemRepository.GetActiveMenuItemsByCategoryId(categoryId, shopId);

                //Set HasSubOrder 
                foreach (var menuItem in menuItems.Where(menuItem => menuItem.MenuItemSubItems.Count > 0))
                {
                    menuItem.HasSubOrder = true;
                }
                return menuItems;
            });
        }

        public List<MenuItem> GetMenuItemByCategoryGroupId(int groupId, int shopId)
        {
            return Execute(() =>
            {

                var categories = _menuItemCategoryRepository.GetCategoriesByGroupId(groupId, shopId);

                var menuItems = new List<MenuItem>();

                foreach (var category in categories)
                {
                    menuItems.AddRange(_menuItemRepository.GetActiveMenuItemsByCategoryId(category.Id, shopId));
                }

                //Set HasSubOrder 
                foreach (var menuItem in menuItems.Where(menuItem => menuItem.MenuItemSubItems.Count > 0))
                {
                    menuItem.HasSubOrder = true;
                }
                return menuItems;
            });
        }
        public List<MenuItemCategory> GetAdminMenuItemCategoryByGroupId(int groupId, int shopId)
        {
            return Execute(() =>
            {

                var categories = _menuItemCategoryRepository.GetCategoriesByGroupIdIncNonActive(groupId, shopId);

            
                return categories;
            });
        }

        

        public List<Order> GetOpenOrders(int shopId)
        {
            return Execute(() => _orderRepository.GetOpenOrders(shopId));
        }

        public Order GetOrder(int orderId, int shopId)
        {
            return Execute(() =>
            {

                var order = _orderRepository.GetById(orderId, shopId);
                return order;
            });
        }

        public Order CloseOrder(int orderId, int shopId)
        {
            return Execute(() =>
            {

                var order = _orderRepository.GetById(orderId, shopId);
                if (order != null)
                {
                    order.StatusId = (int)OrderStatusIdEnum.Completed;
                    order.Updated = DateTimeUtil.GetCurrentTime();
                }

                return order;

            });
        }

        public Order CancelOrder(int orderId, int shopId)
        {
            return Execute(() =>
            {

                var order = _orderRepository.GetById(orderId, shopId);
                if (order != null)
                {
                    order.StatusId = (int)OrderStatusIdEnum.Cancelled;
                    order.Updated = DateTimeUtil.GetCurrentTime();
                }

                return order;

            });
        }

        public Order SaveOrder(Order requestOrder, int shopId)
        {

            return Execute(() =>
            {

                if (!requestOrder.Created.HasValue)
                    requestOrder.Created = DateTimeUtil.GetCurrentTime();

                requestOrder.Updated = DateTimeUtil.GetCurrentTime();
                requestOrder.ShopId = shopId;

                foreach (var orderItem in requestOrder.OrderItems)
                {
                    orderItem.Created = DateTimeUtil.GetCurrentTime();
                    orderItem.ShopId = shopId;

                    foreach (var selection in orderItem.OrderItemsSelections)
                    {
                        selection.Created = DateTimeUtil.GetCurrentTime();
                        selection.ShopId = shopId;
                    }

                }

                _orderRepository.Add(requestOrder);

                //Add or update customer 
                CreateOrUpdateCustomer(requestOrder, shopId);

                return requestOrder;

            });


        }

        private void CreateOrUpdateCustomer(Order requestOrder, int shopId)
        {
            var customer = _customerRepository.GetCustomerByPhone(requestOrder.PhoneNumber, shopId);
            if (customer == null)
            {
                var newCustomer = new Customer()
                {
                    FullName = requestOrder.ContactName,
                    Phone1 = requestOrder.PhoneNumber,
                    Email = requestOrder.Email,
                    Address1 = requestOrder.Address,
                    Created = requestOrder.Created,
                    MarketingPermission = requestOrder.MarketingPermission,
                    IsFirstPurchase = 0,
                    ShopId = shopId
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
                customer.MarketingPermission = requestOrder.MarketingPermission;
                if (requestOrder.OrderOriginalTotal >= 15)
                    customer.IsFirstPurchase = 0;

                if (requestOrder.DeliveryCharge > 0)
                    customer.DeliveryCharge = requestOrder.DeliveryCharge;
            }
        }

        public List<DeliveryOption> GetDeliveryOptions(int shopId)
        {
            return Execute(() => _deliveryOptionRepository.GetExternalDeliveryOption(shopId));

        }

        public List<DeliveryOption> GetDeliveryOptionsByTime(int shopId)
        {
            return Execute(() =>
            {
                var deliveryOptions = _deliveryOptionRepository.GetExternalDeliveryOption(shopId);
                var shopGeneralHours = _shopGeneralHourRepository.GetShopGeneralHours(shopId);


                //Collection Only
                if (deliveryOptions != null && !IsDeliveryOpen(shopGeneralHours, DateTimeUtil.GetCurrentTime()))
                {
                    var delivery = deliveryOptions.FirstOrDefault(r => r.Name == DeliveryOptiponEnum.ForDelivery);
                    deliveryOptions.Remove(delivery);
                }
                if (shopId == 38)
                {
                    var collection = deliveryOptions.FirstOrDefault(r => r.Name == DeliveryOptiponEnum.ForCollection);
                    deliveryOptions.Remove(collection);
                }

                return deliveryOptions;


            });
        }

        public DiscountOutput DiscountOrder(Order orderTransformed, int shopId)
        {
            return Execute(() =>
            {

                var shopInfo = _shopInfoRepository.GetShopInfo(shopId);
                Customer customer = null;
                if (orderTransformed.CustomerId.HasValue && orderTransformed.CustomerId.Value > 0)
                {
                    customer = _customerRepository.GetById(orderTransformed.CustomerId.Value, shopId);
                }


                var discount = new DiscountOutput();
                var discountOptions = _discountOfferRepository.GetAll(shopId);

                //1. 10 percent discount rule
                var discountRule1Key = DiscountRuleEnum.Rule10Percent.ToString();
                var discountRule1 = discountOptions.FirstOrDefault(r => r.Name == discountRule1Key);
                if (discountRule1 != null && discountRule1.Isactive == true)
                {
                    var discountValue = orderTransformed.OrderOriginalTotal * discountRule1.DiscountValue;
                    discount.Amount += discountValue.Value;
                    discount.DiscountRef += discountRule1Key;
                }

                //2. 5 percent
                var discountRule2Key = DiscountRuleEnum.Rule5Percent.ToString();
                var discountRule2 = discountOptions.FirstOrDefault(r => r.Name == discountRule2Key);
                if (discountRule2 != null && discountRule2.Isactive == true)
                {
                    var discountValue = orderTransformed.OrderOriginalTotal * discountRule2.DiscountValue;
                    discount.Amount += discountValue.Value;
                    discount.DiscountRef += discountRule1Key;
                }


                if (shopInfo.RegisterDiscount.HasValue && shopInfo.RegisterDiscount.Value >= 1 && customer?.IsFirstPurchase != null && customer.IsFirstPurchase.Value == 1
                && orderTransformed.OrderOriginalTotal >= 15)
                {
                    discount.Amount += shopInfo.RegisterDiscount.Value;
                    discount.DiscountRef += "Registration Discount";
                }

                if (shopInfo.RegisterDiscount.HasValue && shopInfo.RegisterDiscount.Value < 1 && customer?.IsFirstPurchase != null && customer.IsFirstPurchase.Value == 1
               && orderTransformed.OrderOriginalTotal >= 10)
                {
                    var discountValue = orderTransformed.OrderOriginalTotal * shopInfo.RegisterDiscount;
                    discount.Amount += discountValue.Value;
                    discount.DiscountRef += "Registration Discount";
                }

                return discount;
            });
        }

        public void EmailClientOrderConfirmation(Order orderTransformed, ShopInfo shopInfo, EmailSettingsType settings, int shopId)
        {
            Execute(() =>
            {
                var bodyHtml = BuildClientEmailContent(orderTransformed, shopInfo);
                EmailSender.SendEmail(settings, shopInfo.Email, orderTransformed.Email, ContentResource.OrderClientConfirmation_Subject, "", bodyHtml);

            });
        }

        public string GetSettingByKey(List<ServerSetting> systemSetting, string systemKey)
        {

            return Execute(() =>
            {

                var setting = systemSetting.FirstOrDefault(r => r.SettingKey == systemKey);
                if (setting == null)
                    throw new BusinessEngineException(new System.Exception("Setting does not exists"));

                return setting.SettingValue;
            });

        }

        public void EmailBossConfirmation(Order orderTransformed, ShopInfo shopInfo, string toEmail, EmailSettingsType settings, int shopId)
        {
            Execute(() =>
            {

                var bodyHtml = BuildAdminEmailContent(orderTransformed, shopInfo);

                EmailSender.SendEmail(settings, "noreply@kingfood.ie", toEmail, ContentResource.OrderConfirmation_Subject, "",
                    bodyHtml);
            });
        }

        public EmailSettingsType GetEmailSettings(List<ServerSetting> systemSetting, int shopId)
        {

            return Execute(() =>
            {

                var emailSettingObj = new EmailSettingsType
                {
                    SmtpEnabled = GetEmailSettingsSmtpEnabled(systemSetting),
                    SmtpUserName = GetEmailSettingsSmtpUserName(systemSetting),
                    SmtpPassword = GetEmailSettingsSmtpPassword(systemSetting),
                    SmtpServer = GetEmailSettingsSmtpServer(systemSetting)
                };

                return emailSettingObj;

            });


        }





        public bool GetEmailSettingsAllowEmailClient(List<ServerSetting> systemSetting, int shopId)
        {

            return Execute(() =>
            {
                var settingValue = GetSettingByKey(systemSetting, ServerSettingEnum.AppAllowEmailClient);
                return BooleanStringUtil.ConvertStringValue(settingValue);
            });
        }

        public bool GetEmailSettingsAllowEmailShop(List<ServerSetting> systemSetting, int shopId)
        {
            return Execute(() =>
            {
                var settingValue = GetSettingByKey(systemSetting, ServerSettingEnum.AppAllowEmailShop);
                return BooleanStringUtil.ConvertStringValue(settingValue);
            });
        }

        public PaymentInfo TakePayment(Payment payment, string apiKey, int shopId)
        {
            return Execute(() =>
            {
                var paymentUtil = new PaymentUtil();
                payment.Currency = "eur";
                payment.ApiKey = apiKey;
                payment.Description = string.Format("KingFoodShopId:{0}", shopId);

                var result = paymentUtil.TakePayment(payment);
                return result;

            });
        }


        public PaymentInfo TakeLoyaltyPayment(Payment paymentTransformed, int customerId, int shopId)
        {
            /* 1. check customer exists
             * 2. check credit is enough
             * 3. remove credit from customer
             * 4. return success
             */


            return Execute(() =>
            {
                var customer = _customerRepository.GetById(customerId, shopId);
                if (customer == null)
                    throw new BusinessEngineException(new System.Exception("Cannot find customer"));

                var costcredit = (int)(paymentTransformed.Amount * 1000);


                if (!customer.RewardPoints.HasValue || customer.RewardPoints.Value < costcredit)
                {
                    throw new BusinessEngineException(new System.Exception("Cannot complete payment"));
                }

                customer.RewardPoints = customer.RewardPoints - costcredit;

                var paymentResult = new PaymentInfo
                {
                    IsSuccess = true,
                    PaymentRef = "Reward Points",
                    CostCredit = costcredit
                };

                return paymentResult;

            });
        }

        public int UpdateCustomerRewardPoints(int recalculatedShopId, Order orderTransformed)
        {
            if (string.IsNullOrEmpty(orderTransformed.PaymentRef) ||
                orderTransformed.PaymentRef.ToUpper() == "Reward Points".ToUpper())
                return 0;

            var customerId = orderTransformed.CustomerId;
            if (!customerId.HasValue || customerId.Value <= 0)
                return 0;
            var customer = _customerRepository.GetById(customerId.Value, recalculatedShopId);
            if (customer == null)
                return 0;

            var allmenuItems = _menuItemRepository.GetAllActive(recalculatedShopId);

            var rewardPoints = orderTransformed.OrderItems.Select(orderItem => allmenuItems.FirstOrDefault(r => r.Id == orderItem.MenuItemId)).Where(item => item != null && item.RewardPoints.HasValue).Sum(item => item.RewardPoints.Value);

            customer.RewardPoints = customer.RewardPoints + rewardPoints;

            return rewardPoints;

        }

        public List<ShopInfo> GetAllShopsNeedTestOrder(int shopId)
        {
            var shopInfo = _shopInfoRepository.GetAllShopsNeedTestOrder(shopId);
            return shopInfo;
        }


        public bool SyncStatus(int shopId)
        {
            return Execute(() =>
            {

                var currentStatus = _syncStatusRepository.GetFirtRecord(shopId) ?? new SyncStatus();
                currentStatus.CheckTime = DateTimeUtil.GetCurrentTime();
                if (currentStatus.Id == 0)
                {
                    currentStatus.Created = DateTimeUtil.GetCurrentTime();
                    currentStatus.ShopId = shopId;
                    _syncStatusRepository.Add(currentStatus);
                }

                return true;

            });
        }

        public List<MenuItem> GetHotMenuItems(int shopId)
        {
            return Execute(() =>
            {
                var menuItems = _menuItemRepository.GetHotMenuItems(shopId);

                //Set HasSubOrder 
                foreach (var menuItem in menuItems.Where(menuItem => menuItem.MenuItemSubItems.Count > 0))
                {
                    menuItem.HasSubOrder = true;
                }
                return menuItems;
            });
        }

        public List<DeliveryOption> GetDeliveryOptionsInternal(int shopId)
        {
            return Execute(() => _deliveryOptionRepository.GetInternalDeliveryOption(shopId));
        }


        public List<MenuItem> GetHotMenuItemsInternal(int shopId)
        {
            return Execute(() =>
            {
                var menuItems = _menuItemRepository.GetHotMenuItemsInternal(shopId);

                //Set HasSubOrder 
                foreach (var menuItem in menuItems.Where(menuItem => menuItem.MenuItemSubItems.Count > 0))
                {
                    menuItem.HasSubOrder = true;
                }
                return menuItems;
            });
        }

        public bool EmailException(string message, string fromEmail, string toEmail, EmailSettingsType settings, int shopId)
        {
            return Execute(() => EmailSender.SendEmail(settings, fromEmail, toEmail, "PrinterPollingException", message, ""));
        }

        public Customer GetCustomerByPhoneNumber(string customerPhone, int shopId)
        {
            return Execute(() => _customerRepository.GetCustomerByPhone(customerPhone, shopId));
        }

        public List<MenuItemCategory> GetMenuItemCategoryByGroupId(int groupId, int shopId)
        {
            return Execute(() => _menuItemCategoryRepository.GetCategoriesByGroupId(groupId, shopId));
        }

        public Order RemoveOrder(int orderId, int shopId)
        {
            return Execute(() =>
            {

                var order = _orderRepository.GetById(orderId, shopId);
                if (order != null)
                {
                    order.StatusId = (int)OrderStatusIdEnum.Removed;
                    order.Updated = DateTimeUtil.GetCurrentTime();
                }

                return order;

            });
        }

        public List<Order> GetCompleteOrders(DateTime? searchDate, int shopId)
        {
            return Execute(() =>
            {
                if (!searchDate.HasValue)
                    searchDate = DateTime.Today;

                var fromDate = searchDate.Value.Date.AddHours(5);
                var toDate = fromDate.AddDays(1).AddHours(-1);

                var orders = _orderRepository.GetActiveOrdersByDate(fromDate, toDate, shopId);

                return orders;

            });
        }

        public bool CheckLicence(string hwid, int shopId)
        {
            return Execute(() =>
            {
                var lic = _licenceInfoRepository.GetLicence(hwid, shopId);

                return lic != null && lic.Count != 0;
            });


        }

        public DataTable GetReport_SummaryReport(DateTime? searchDate, int shopId)
        {
            return Execute(() =>
            {

                if (!searchDate.HasValue)
                    searchDate = DateTime.Today;

                var fromDate = searchDate.Value.Date.AddHours(5);
                var toDate = fromDate.AddDays(1).AddHours(-1);

                return _storedProcedureRepository.SummaryReport(fromDate, toDate, shopId);
            });



        }

        public DataTable GetReport_TransactionReport(DateTime? searchDate, int shopId)
        {
            return Execute(() =>
            {

                if (!searchDate.HasValue)
                    searchDate = DateTime.Today;

                var fromDate = searchDate.Value.Date.AddHours(5);
                var toDate = fromDate.AddDays(1).AddHours(-1);

                return _storedProcedureRepository.TransactionReport(fromDate, toDate, shopId);
            });

        }

        public DataTable GetReport_TransactionReportOnline(DateTime? searchDate, int shopId)
        {
            return Execute(() =>
            {

                if (!searchDate.HasValue)
                    searchDate = DateTime.Today;

                var fromDate = searchDate.Value.Date.AddHours(5);
                var toDate = fromDate.AddDays(1).AddHours(-1);

                return _storedProcedureRepository.TransactionReportOnline(fromDate, toDate, shopId);
            });
        }

        public DataTable GetReport_SummaryOnline(DateTime? searchDate, int shopId)
        {
            return Execute(() =>
            {

                if (!searchDate.HasValue)
                    searchDate = DateTime.Today;

                var fromDate = searchDate.Value.Date.AddHours(5);
                var toDate = fromDate.AddDays(1).AddHours(-1);

                return _storedProcedureRepository.SummaryOnline(fromDate, toDate, shopId);
            });
        }

        public List<OptionItem> GetOptionItems(int shopId)
        {
            return Execute(() => _optionItemRepository.GetAllActive(shopId));
        }

        public ShopUser LoginShopUserByPin(string pin, int shopId)
        {
            return Execute(() => _shopUserRepository.GetUserByPin(pin, shopId));
        }

        public List<MenuItemCategoryGroup> GetMenuItemCategoryGroup(int shopId)
        {
            return Execute(() => _menuItemCategoryGroupRepository.GetMenuItemCategoryGroup(shopId));

        }

        public List<ShopUser> GetAllShopUsers(int shopId)
        {
            return Execute(() => _shopUserRepository.GetAllActive(shopId));
        }

        public DataTable GetReport_TransactionDrinkReport(DateTime? searchDate, int shopId)
        {
            return Execute(() =>
            {

                if (!searchDate.HasValue)
                    searchDate = DateTime.Today;

                var fromDate = searchDate.Value.Date.AddHours(5);
                var toDate = fromDate.AddDays(1).AddHours(-1);

                return _storedProcedureRepository.TransactionDrinkReport(fromDate, toDate, shopId);
            });
        }

        public List<Customer> GetAllCustomer(int shopId)
        {
            return Execute(() => _customerRepository.GetAll(shopId));
        }

        public List<TableCommand> GetTableCommand(int shopId)
        {
            return Execute(() => _tableCommandRepository.GetAllActive(shopId));
        }

        public List<Driver> GetAllDrivers(int shopId)
        {

            return Execute(() => _driverRepository.GetAll(shopId));

        }

        public List<PaymentType> GetAllPaymentTypes(int shopId)
        {
            return Execute(() => _paymentTypeRepository.GetAll(shopId));
        }

        public DataTable GetReport_OrdeFoodReport(DateTime? searchDate, int shopId)
        {
            return Execute(() =>
            {

                if (!searchDate.HasValue)
                    searchDate = DateTime.Today;

                var fromDate = searchDate.Value.Date.AddHours(5);
                var toDate = fromDate.AddDays(1).AddHours(-1);

                return _storedProcedureRepository.OrdeFoodReport(fromDate, toDate, shopId);
            });
        }

        public DataTable GetReport_DriverReport(DateTime? searchDate, int shopId)
        {
            return Execute(() =>
            {

                if (!searchDate.HasValue)
                    searchDate = DateTime.Today;

                var fromDate = searchDate.Value.Date.AddHours(5);
                var toDate = fromDate.AddDays(1).AddHours(-1);

                return _storedProcedureRepository.DriverReport(fromDate, toDate, shopId);
            });
        }

        public List<TableAlias> GetTableAlias(int shopId)
        {
            return Execute(() => _tableAliasRepository.GetAllActive(shopId));
        }

        public DataTable GetReport_CustomerReport(int shopId)
        {
            return Execute(() => _storedProcedureRepository.CustomerReport(shopId));
        }

        public List<ServerSetting> GetClientSystemSettings(int shopId)
        {
            return Execute(() =>
            {
                var settings = _serverSettingRepository.GetAllNonServer(shopId);

                return settings;


            });
        }

        public List<MenuItem> GetMenuItemsList(int shopId)
        {
            return Execute(() =>
            {
                var menuItems = _menuItemRepository.GetAllActive(shopId);

                //Set HasSubOrder 
                foreach (var menuItem in menuItems.Where(menuItem => menuItem.MenuItemSubItems.Count > 0))
                {
                    menuItem.HasSubOrder = true;
                }
                return menuItems;
            });
        }

        public List<DeliveryArea> GetDeliveryArea(int deliveryAreaGroup, int shopId)
        {
            return Execute(() => _deliveryAreaRepository.GetDeliveryAreaByGroup(deliveryAreaGroup, shopId));

        }

        public DataTable GetCustomerOrders(string phoneNumber, int shopId)
        {
            return Execute(() => _storedProcedureRepository.GetCustomerOrders(phoneNumber, shopId));
        }

        public DataTable GetReport_SummaryReport(DateTime? fromDate, DateTime? toDate, int shopId)
        {
            return Execute(() =>
            {

                var fromDateValue = fromDate.Value.Date.AddHours(5);
                var toDateValue = toDate.Value.AddDays(1).AddHours(-1);

                return _storedProcedureRepository.SummaryReport(fromDateValue, toDateValue, shopId);
            });
        }

        public DataTable GetReport_TransactionReport(DateTime? fromDate, DateTime? toDate, int shopId)
        {
            return Execute(() =>
            {

                var fromDateValue = fromDate.Value.Date.AddHours(5);
                var toDateValue = toDate.Value.AddDays(1).AddHours(-1);

                return _storedProcedureRepository.TransactionReport(fromDateValue, toDateValue, shopId);
            });
        }

        public DataTable GetReport_TransactionDrinkReportMulti(DateTime? fromDate, DateTime? toDate, int shopId)
        {
            return Execute(() =>
            {

                var fromDateValue = fromDate.Value.Date.AddHours(5);
                var toDateValue = toDate.Value.AddDays(1).AddHours(-1);

                return _storedProcedureRepository.TransactionDrinkReport(fromDateValue, toDateValue, shopId);
            });
        }


        public DataTable GetReport_AdminReportSummaryOnlineReport(DateTime? fromDate, DateTime? toDate, int shopId)
        {
            return Execute(() =>
            {

                var fromDateValue = fromDate.Value.Date.AddHours(5);
                var toDateValue = toDate.Value.AddDays(1).AddHours(-1);

                return _storedProcedureRepository.AdminReportSummaryOnlineReport(fromDateValue, toDateValue, shopId);
            });
        }

        public DataTable GetReport_AdminReportTransactionOnlineReport(DateTime? fromDate, DateTime? toDate, int shopId)
        {
            return Execute(() =>
            {

                var fromDateValue = fromDate.Value.Date.AddHours(5);
                var toDateValue = toDate.Value.AddDays(1).AddHours(-1);

                return _storedProcedureRepository.AdminReportTransactionOnlineReport(fromDateValue, toDateValue, shopId);
            });
        }

        public DataTable GetReportInvoiceOnline(DateTime? fromDate, DateTime? toDate, int shopId)
        {
            return Execute(() =>
            {

                var fromDateValue = fromDate.Value.Date.AddHours(5);
                var toDateValue = toDate.Value.AddDays(1).AddHours(-1);

                return _storedProcedureRepository.GetReportInvoiceOnlineReport(fromDateValue, toDateValue, shopId);
            });
        }

        public DataTable GetAdminReportInvoice()
        {

            var toSaturday = DateTime.Now.Previous(DayOfWeek.Saturday);
            var fromSaturday = toSaturday.Previous(DayOfWeek.Saturday);

            var fromDateValue = new DateTime(fromSaturday.Year, fromSaturday.Month, fromSaturday.Day, 5, 0, 0);
            var toDateValue = new DateTime(toSaturday.Year, toSaturday.Month, toSaturday.Day, 5, 0, 0);

            return _storedProcedureRepository.GetAdminReportInvoiceReport(fromDateValue, toDateValue);
        }




        public List<DeliveryCost> GetDeliveryCosts(int shopId)
        {
            return Execute(() => _deliveryCostRepository.GetAll(shopId));
        }

        public void SaveCustomerInfo(string customerPhone, string customerAddress, string customerName, string customerEmail,
            decimal? customerDeliveryCharge, int shopId)
        {
            //Add or update customer 
            var customer = _customerRepository.GetCustomerByPhone(customerPhone, shopId);
            if (customer == null)
            {
                var newCustomer = new Customer()
                {
                    FullName = customerName,
                    Phone1 = customerPhone,
                    Email = customerEmail,
                    Address1 = customerAddress,
                    Created = DateTimeUtil.GetCurrentTime(),

                };

                if (customerDeliveryCharge.HasValue && customerDeliveryCharge.Value > 0)
                    newCustomer.DeliveryCharge = customerDeliveryCharge.Value;

                _customerRepository.Add(newCustomer);

            }
            else
            {
                customer.Email = customerEmail;
                customer.Address1 = customerAddress;
                customer.FullName = customerName;
                if (customerDeliveryCharge.HasValue && customerDeliveryCharge.Value > 0)
                    customer.DeliveryCharge = customerDeliveryCharge.Value;
            }

        }
        public DataTable GetReport_BusyHourReport(DateTime? searchDate, int shopId)
        {
            return Execute(() =>
            {

                if (!searchDate.HasValue)
                    searchDate = DateTime.Today;

                var fromDate = searchDate.Value.Date.AddHours(5);
                var toDate = fromDate.AddDays(1).AddHours(-1);

                return _storedProcedureRepository.BusyHourReport(fromDate, toDate, shopId);
            });
        }

        public DataTable GetReport_CustomerOrderReport(int shopId)
        {
            return Execute(() => _storedProcedureRepository.CustomerOrderReport(shopId));
        }

        public DataTable GetAdminReport_Status()
        {
            return Execute(() => _storedProcedureRepository.AdminStatus());
        }

        public DataTable GetCustomerPayOutReport()
        {
            return Execute(() => _storedProcedureRepository.CustomerPayOutReport());
        }

        public Order GetOrderByGuid(string guid, int shopId)
        {
            return Execute(() => _orderRepository.GetByGuid(guid, shopId));
        }

        public ShopInfo UpdateShopInfo(ShopInfo shopInfoTransformed, int shopId)
        {
            return Execute(() =>
            {
                _shopInfoRepository.Update(shopInfoTransformed);
                return shopInfoTransformed;
            });

        }

        public string GetPaymentApiKey(List<ServerSetting> systemSetting)
        {
            return Execute(() => GetSettingByKey(systemSetting, ServerSettingEnum.AppStripKey));
        }

        public OptionItem SaveOptionItem(OptionItem transformItem, int shopId)
        {
            return Execute(() =>
            {



                if (transformItem.Id > 0)
                {
                    var existingItem = _optionItemRepository.GetOne(r => r.ShopId == shopId & r.Id == transformItem.Id);
                    if (existingItem == null)
                        return null;

                    existingItem.ItemName = transformItem.ItemName;
                    existingItem.ItemDescription = transformItem.ItemDescription;
                    existingItem.TranslateName = transformItem.TranslateName;
                    existingItem.UnitPrice = transformItem.UnitPrice;
                    existingItem.GroupId = transformItem.GroupId;
                    existingItem.Updated = DateTimeUtil.GetCurrentTime();
                    existingItem.ShopId = shopId;

                }
                else
                {

                    transformItem.Isactive = true;
                    transformItem.Created = DateTimeUtil.GetCurrentTime();
                    transformItem.Updated = DateTimeUtil.GetCurrentTime();
                    transformItem.ShopId = shopId;
                    _optionItemRepository.Add(transformItem);
                }



                return transformItem;
            });
        }

        public OptionItem GetOptionItemById(int id, int shopId)
        {
            return Execute(() =>
            {
                return _optionItemRepository.GetOne(r => r.Id == id && r.ShopId == shopId);
            });
        }

        public OptionItem RemoveOptionItemById(int id, int shopId)
        {
            return Execute(() =>
            {

                var optionItem = _optionItemRepository.GetOne(r => r.Id == id && r.ShopId == shopId);

                if (optionItem != null)
                    _optionItemRepository.Remove(optionItem);

                return optionItem;
            });
        }

        public ServerSetting GetSystemSettingById(int id, int shopId)
        {

            return Execute(() =>
            {
                return _serverSettingRepository.GetOne(r => r.Id == id && r.ShopId == shopId);
            });
        }

        public ServerSetting SaveSystemSetting(ServerSetting transformItem, int shopId)
        {
            return Execute(() =>
            {
                if (transformItem.Id == 0)
                    return transformItem;


                var existingItem = _serverSettingRepository.GetOne(r => r.ShopId == shopId & r.Id == transformItem.Id);
                if (existingItem == null)
                    return null;

                existingItem.SettingValue = transformItem.SettingValue;
                existingItem.SettingValue = transformItem.SettingValue;
                existingItem.IsServer = transformItem.IsServer;
                existingItem.Updated = DateTimeUtil.GetCurrentTime();
                existingItem.ShopId = shopId;

                return transformItem;
            });
        }

        public DeliveryCost GetDeliveryCostsById(int id, int shopId)
        {
            return Execute(() =>
            {
                return _deliveryCostRepository.GetOne(r => r.Id == id && r.ShopId == shopId);
            });
        }

        public DeliveryCost SaveDeliveryCost(DeliveryCost transformItem, int shopId)
        {
            return Execute(() =>
            {
                if (transformItem.Id == 0)
                    return transformItem;


                var existingItem = _deliveryCostRepository.GetOne(r => r.ShopId == shopId & r.Id == transformItem.Id);
                if (existingItem == null)
                    return null;

                existingItem.DeliveryCostAmount = transformItem.DeliveryCostAmount;
                existingItem.Updated = DateTimeUtil.GetCurrentTime();
                existingItem.ShopId = shopId;

                return transformItem;
            });
        }

        public List<ShopNews> GetShopNews(int shopId)
        {
            return Execute(() =>
            {
                var items = _shopNewsRepository.GetLast20News(shopId);
                return items;
            });
        }

        public ShopNews SaveShopNews(ShopNews transformItem, int shopId)
        {
            return Execute(() =>
            {

                if (transformItem.Id > 0)
                {
                    var existingItem = _shopNewsRepository.GetOne(r => r.ShopId == shopId & r.Id == transformItem.Id);
                    if (existingItem == null)
                        return null;

                    existingItem.News = transformItem.News;
                    existingItem.Updated = DateTimeUtil.GetCurrentTime();
                    existingItem.ShopId = shopId;
                }
                else
                {

                    transformItem.News = transformItem.News;
                    transformItem.Created = DateTimeUtil.GetCurrentTime();
                    transformItem.Updated = DateTimeUtil.GetCurrentTime();
                    transformItem.ShopId = shopId;
                    _shopNewsRepository.Add(transformItem);
                }
                return transformItem;
            });
        }

        public ShopNews GetShopNewsById(int id, int shopId)
        {
            return Execute(() =>
            {
                return _shopNewsRepository.GetOne(r => r.Id == id && r.ShopId == shopId);

            });
        }

        public ShopNews RemoveShopNewsById(int id, int shopId)
        {
            return Execute(() =>
            {

                var optionItem = _shopNewsRepository.GetOne(r => r.Id == id && r.ShopId == shopId);

                if (optionItem != null)
                    _shopNewsRepository.Remove(optionItem);

                return optionItem;
            });
        }

        public List<MenuSubItem> GetAllMenuSubItems(int shopId)
        {
            return Execute(() =>
            {
                var items = _menuSubItemRepository.GetMany(r => r.ShopId == shopId);
                return items;
            });
        }

        public MenuSubItem RemoveMenuSubItemById(int id, int shopId)
        {
            return Execute(() =>
            {
                var subItem = _menuSubItemRepository.GetOne(r => r.Id == id && r.ShopId == shopId);

                if (subItem != null)
                {
                    //Remove SubItemItems
                    var subItemItems = _menuSubItemItemsRepository.GetMany(r => r.SubItemId == subItem.Id);
                    foreach (var subItemItem in subItemItems)
                    {
                        _menuSubItemItemsRepository.Remove(subItemItem);
                    }

                    //Remove MenuItem=>SubItem
                    var menuItemSubItems = _menuItemSubItemRepository.GetMany(r => r.SubmenuItemId == subItem.Id);
                    foreach (var menuItemSubItem in menuItemSubItems)
                    {
                        _menuItemSubItemRepository.Remove(menuItemSubItem);
                    }

                    _menuSubItemRepository.Remove(subItem);
                }


                return subItem;
            });
        }

        public MenuSubItem SaveMenuSubItem(MenuSubItem transformItem, int shopId)
        {
            return Execute(() =>
            {

                if (transformItem.Id > 0)
                {
                    var existingItem = _menuSubItemRepository.GetOne(r => r.ShopId == shopId & r.Id == transformItem.Id);
                    if (existingItem == null)
                        return null;

                    existingItem.Name = transformItem.Name;
                    existingItem.Description = transformItem.Description;
                    existingItem.Updated = DateTimeUtil.GetCurrentTime();
                    existingItem.ShopId = shopId;
                }
                else
                {
                    transformItem.Created = DateTimeUtil.GetCurrentTime();
                    transformItem.Updated = DateTimeUtil.GetCurrentTime();
                    transformItem.ShopId = shopId;
                    _menuSubItemRepository.Add(transformItem);
                }
                return transformItem;
            });
        }

        public MenuSubItem GetMenuSubItemsById(int id, int shopId)
        {
            return Execute(() =>
            {
                var items = _menuSubItemRepository.GetSubItemById(id, shopId);

                return items;
            });
        }

        public MenuSubItem RemoveMenuSubItemLink(int id, int menuItemId, int shopId)
        {
            return Execute(() =>
            {

                var subItem = _menuSubItemRepository.GetOne(r => r.Id == id && r.ShopId == shopId);
                if (subItem == null)
                    throw new BusinessEngineException(new System.Exception("Cannot find Sub Item"));

                var selection = _menuSubItemItemsRepository.GetOne(r => r.SubItemId == id && r.MenuItemId == menuItemId && r.ShopId == shopId);
                if (selection == null)
                    throw new BusinessEngineException(new System.Exception("Cannot find Sub Item Link Entry"));

                _menuSubItemItemsRepository.Remove(selection);
                return subItem;
            });
        }

        public MenuSubItem SaveMenuSubItemWithItem(int id, int menuItemId, decimal cost, int shopId)
        {
            return Execute(() =>
            {

                var subItem = _menuSubItemRepository.GetOne(r => r.Id == id && r.ShopId == shopId);
                if (subItem == null)
                    throw new BusinessEngineException(new System.Exception("Cannot find Sub Item"));

                var selection = _menuSubItemItemsRepository.GetOne(r => r.SubItemId == id && r.MenuItemId == menuItemId && r.ShopId == shopId);
                if (selection != null)
                {
                    selection.Updated = DateTimeUtil.GetCurrentTime();
                    selection.Cost = cost;
                }
                else
                {
                    var newItem = new MenuSubItemItem()
                    {
                        SubItemId = id,
                        MenuItemId = menuItemId,
                        ShopId = shopId,
                        Cost = cost,
                        Created = DateTimeUtil.GetCurrentTime(),
                        Updated = DateTimeUtil.GetCurrentTime()
                    };


                    _menuSubItemItemsRepository.Add(newItem);
                }



                return subItem;
            });
        }

        public MenuItemCategory SaveMenuCategory(MenuItemCategory item, int shopId)
        {
            return Execute(() =>
            {

                var category = _menuItemCategoryRepository.GetOne(r => r.Id == item.Id && r.ShopId == shopId);
                if (category != null)
                {
                    category.Name = item.Name;
                    category.Description = item.Description;
                    category.TranslateName = item.TranslateName;
                    category.SortOrder = item.SortOrder;
                    category.ImageUrl = item.ImageUrl;
                    category.Updated = DateTimeUtil.GetCurrentTime();
                    item.CategoryGroupId = 0;
                    item.ShopId = shopId;

                }
                else
                {
                    item.Isactive = true;
                    item.ShopId = shopId;
                    item.CategoryGroupId = 0;
                    item.Created = DateTimeUtil.GetCurrentTime();
                    item.Updated = DateTimeUtil.GetCurrentTime();
                    _menuItemCategoryRepository.Add(item);
                }



                return item;
            });
        }

        public MenuItemCategory RemoveMenuItemCategoryById(int id, int shopId)
        {
            return Execute(() =>
            {

                var category = _menuItemCategoryRepository.GetOne(r => r.Id == id && r.ShopId == shopId);

                if (category == null)
                    throw new BusinessEngineException(new System.Exception("Category do not exists"));

                _menuItemCategoryRepository.Remove(category);

                var menuItems = _menuItemRepository.GetActiveMenuItemsByCategoryId(id, shopId);
                foreach (var menuItem in menuItems)
                {
                    menuItem.MenuItemCategoryId = 0;
                    menuItem.Updated = DateTimeUtil.GetCurrentTime();
                }

                return category;

            });
        }

        public MenuItemCategory GetMenuItemCategoryById(int id, int shopId)
        {
            return Execute(() =>
            {

                var category = _menuItemCategoryRepository.GetOne(r => r.Id == id && r.ShopId == shopId);

                if (category == null)
                    throw new BusinessEngineException(new System.Exception("Category do not exists"));

                return category;

            });
        }

        public MenuItem SaveMenuItemWithSubItem(int extraId, int memuItemId, int shopId)
        {
            return Execute(() =>
            {

                var menuItem = _menuItemRepository.GetOne(r => r.Id == memuItemId && r.ShopId == shopId);
                if (menuItem == null)
                    throw new BusinessEngineException(new System.Exception("menuItem do not exists"));

                var subItem = _menuSubItemRepository.GetOne(r => r.Id == extraId && r.ShopId == shopId);
                if (subItem == null)
                    throw new BusinessEngineException(new System.Exception("subItem do not exists"));

                var newMenuItem = new MenuItemSubItem()
                {
                    MenuItemId = memuItemId,
                    SubmenuItemId = extraId,
                    ShopId = shopId,
                    Created = DateTimeUtil.GetCurrentTime(),
                    Updated = DateTimeUtil.GetCurrentTime()
                };

                _menuItemSubItemRepository.Add(newMenuItem);

                return menuItem;

            });
        }

        public MenuItem RemoveMenuItemWithSubItem(int extraId, int memuItemId, int shopId)
        {
            return Execute(() =>
            {

                var menuItem = _menuItemRepository.GetOne(r => r.Id == memuItemId && r.ShopId == shopId);
                if (menuItem == null)
                    throw new BusinessEngineException(new System.Exception("menuItem do not exists"));

                var subItem = _menuSubItemRepository.GetOne(r => r.Id == extraId && r.ShopId == shopId);
                if (subItem == null)
                    throw new BusinessEngineException(new System.Exception("subItem do not exists"));
                var linkItem = _menuItemSubItemRepository.GetOne(r => r.MenuItemId == memuItemId && r.SubmenuItemId == extraId && r.ShopId == shopId);
                if (linkItem == null)
                    throw new BusinessEngineException(new System.Exception("linkItem do not exists"));

                _menuItemSubItemRepository.Remove(linkItem);

                return menuItem;

            });
        }

        public Customer UpdateCustomerInfo(Customer shopCustomerPocoIn, int shopId)
        {
            return Execute(() =>
            {

                //Check if email is in use
                var existingCustomer =
                    _customerRepository.GetOne(r => r.Email == shopCustomerPocoIn.Email && r.ShopId == shopId);

                if (existingCustomer == null)
                {
                    throw new BusinessEngineException(new System.Exception("User doesn't exists"));
                }


                existingCustomer.FullName = shopCustomerPocoIn.FullName;
                existingCustomer.Phone1 = shopCustomerPocoIn.Phone1;
                existingCustomer.Updated = DateTime.Now;
                existingCustomer.Address1 = shopCustomerPocoIn.Address1;


                return existingCustomer;

            });
        }

        public Customer ResetCustomerPassword(Customer shopCustomerPocoIn, int shopId)
        {
            return Execute(() =>
            {

                //Check if email is in use
                var existingCustomer =
                    _customerRepository.GetOne(r => r.Email == shopCustomerPocoIn.Email && r.ShopId == shopId);

                if (existingCustomer == null)
                {
                    throw new BusinessEngineException(new System.Exception("User doesn't exists"));
                }

                existingCustomer.Password = _encryptionHelper.EncryptXml(shopCustomerPocoIn.Password);
                existingCustomer.Updated = DateTime.Now;

                return existingCustomer;

            });
        }

        public Customer RegisterCustomer(Customer shopCustomerPocoIn, int shopId)
        {
            return Execute(() =>
            {

                //Check if email is in use
                var existingCustomer =
                    _customerRepository.GetRegisterUserByEmail(shopCustomerPocoIn.Email, shopId);

                if (existingCustomer != null)
                {
                    throw new BusinessEngineException(new System.Exception("User already exists"));
                }

                //Add new customer
                var newCustomer = new Customer
                {
                    Email = shopCustomerPocoIn.Email,
                    FullName = shopCustomerPocoIn.FullName,
                    ShopId = shopId,
                    Phone1 = shopCustomerPocoIn.Phone1,
                    Password = _encryptionHelper.EncryptXml(shopCustomerPocoIn.Password),
                    AuthenticationCode = _authSmsCodeUtil.GenerateAuthCode(),
                    DeliveryCharge = shopCustomerPocoIn.DeliveryCharge,
                    Address1 = shopCustomerPocoIn.Address1,
                    ValidationPending = shopCustomerPocoIn.ValidationPending,
                    MarketingPermission = shopCustomerPocoIn.MarketingPermission,
                    Created = DateTime.Now,
                    IsFirstPurchase = 1,
                    RewardPoints = 0

                };

                _customerRepository.Add(newCustomer);

                return newCustomer;

            });
        }

        public Customer VerifySms(Customer shopCustomerPocoIn, int shopId)
        {
            return Execute(() =>
            {

                //Check if email is in use
                var existingCustomer =
                    _customerRepository.GetOne(r => r.Email == shopCustomerPocoIn.Email && r.ShopId == shopId);

                if (existingCustomer == null)
                {
                    throw new System.Exception("Invalid Input");
                }

                var shopInfo = _shopInfoRepository.GetShopInfo(shopId);
                if (!shopInfo.SmsBalance.HasValue || shopInfo.SmsBalance.Value <= 0)
                {
                    throw new System.Exception("Not Enough SMS Credits");
                }

                existingCustomer.Phone1 = shopCustomerPocoIn.Phone1;

                var settings = _serverSettingRepository.GetAllSystemSetting(shopId);
                var smsaccount = settings.FirstOrDefault(r => r.SettingKey == ServerSettingEnum.AppSmsAccount).SettingValue;
                var smsToken = settings.FirstOrDefault(r => r.SettingKey == ServerSettingEnum.AppSmsToken).SettingValue;
                var smsFromNumber = settings.FirstOrDefault(r => r.SettingKey == ServerSettingEnum.AppSmsFromNumber).SettingValue;
                var smsRequest = new SmsRequest();
                smsRequest.AccountId = smsaccount;
                smsRequest.AccountToken = smsToken;
                smsRequest.FromNumber = smsFromNumber;
                smsRequest.ToNumber = existingCustomer.Phone1;
                smsRequest.Body = string.Format("your noodle box registration verification code is:{0}", existingCustomer.AuthenticationCode);

                var response = _smsUtil.SendSMS(smsRequest);

                //Reduce Credits
                if (response.IsSuccess.HasValue && response.IsSuccess.Value)
                {
                    shopInfo.SmsBalance = shopInfo.SmsBalance - 1;
                }
                else
                {
                    throw new System.Exception(response.ErrorMessage);
                }

                return existingCustomer;

            });
        }

        public Customer GetCustomerById(int id, int shopId)
        {
            return Execute(() => _customerRepository.GetById(id, shopId));
        }

        public Customer ActivateBySms(Customer registeredCustomer, int shopId)
        {
            return Execute(() =>
            {

                //Check if email is in use
                var existingCustomer =
                    _customerRepository.GetOne(r => r.Id == registeredCustomer.Id && r.ShopId == shopId);

                if (existingCustomer == null)
                {
                    throw new System.Exception("Invalid Input");
                }


                if (existingCustomer.AuthenticationCode == registeredCustomer.AuthenticationCode)
                {
                    existingCustomer.ValidationPending = 2;
                }
                else
                {
                    throw new System.Exception("Invalid Input");
                }
                return existingCustomer;

            });
        }

        public Customer Forgetpassword(string email, int shopId)
        {
            return Execute(() =>
            {

                //Check if email is in use
                var existingCustomer =
                    _customerRepository.GetOne(r => r.Email == email && r.ShopId == shopId);

                if (existingCustomer == null)
                {
                    throw new System.Exception("Invalid Input");
                }
                if (existingCustomer.ValidationPending == 1)
                {
                    throw new System.Exception("Still in Validation Pending");
                }

                var shopInfo = GetShopInfo(shopId);
                var systemSetting = GetSystemSettings(shopId);
                var emailSettings = GetEmailSettings(systemSetting, shopId);
                var decodePassword = _encryptionHelper.DecriptEncryptXml(existingCustomer.Password);
                var body = string.Format("Your password is: {0}", decodePassword);
                EmailSender.SendEmail(emailSettings, shopInfo.Email, email, "Retrieve Password", "", body);

                return existingCustomer;

            });
        }

        public Customer LoginCustomer(string email, string password, int shopId)
        {
            return Execute(() =>
            {
                var encodePassword = _encryptionHelper.EncryptXml(password);
                //Check if email is in use
                var existingCustomer =
                    _customerRepository.GetOne(r => r.Email == email && r.Password == encodePassword && r.ShopId == shopId);

                return existingCustomer;

            });
        }



        public int ReValidateShopIdByOrderItem(List<int> menuItemIds, int shopId)
        {
            return Execute(() =>
            {
                //Get Shop Ids from MenuItems
                var menuItems = _menuItemRepository.GetMany(r => menuItemIds.Contains(r.Id));
                if (menuItems == null || menuItems.Count == 0)
                    throw new BusinessEngineException(new System.Exception("menuItems do not exists"));

                var ids = menuItems.Select(r => r.ShopId).Distinct();
                if (ids.Count() > 1)
                    throw new BusinessEngineException(new System.Exception("menuItems cannot cross shop"));

                var menucalcShopId = ids.FirstOrDefault();

                if (menucalcShopId != shopId)
                    return menucalcShopId;
                else
                    return shopId;

            });
        }

        public string GetEncryptedPassword(string password, int shopId)
        {
            return Execute(() => _encryptionHelper.EncryptXml(password));
        }



        public ShopUser GetShopUserById(int userId, int shopId)
        {
            return Execute(() => _shopUserRepository.GetById(userId, shopId));
        }

        public bool CheckUserPinExsits(ShopUser shopUserTransformed, int shopId)
        {
            return Execute(() => _shopUserRepository.CheckUserPinExists(shopUserTransformed.Pin, shopUserTransformed.Id, shopId));
        }

        public bool CheckShopUserNameUnique(ShopUser shopUserTransformed, int shopId)
        {
            return Execute(() => _shopUserRepository.CheckShopUserNameUnique(shopUserTransformed.Pin, shopUserTransformed.Id, shopId));
        }

        public ShopUser SaveShopUser(ShopUser shopUser, int shopId)
        {
            return Execute(() =>
            {


                if (shopUser.Id > 0)
                {//Existing user
                    var user = _shopUserRepository.GetById(shopUser.Id, shopId);
                    user.Pin = shopUser.Pin;
                    user.UserName = shopUser.UserName;
                    user.UserGroupId = shopUser.UserGroupId;
                    if (user.Password != shopUser.Password)
                        user.Password = _encryptionHelper.EncryptXml(shopUser.Password);

                    return user;
                }
                else
                {

                    shopUser.Password = _encryptionHelper.EncryptXml(shopUser.Password);
                    _shopUserRepository.Add(shopUser);
                    return shopUser;
                }

            });

        }

        public ShopUser RemoveShopUserById(int userId, int shopId)
        {
            return Execute(() =>
            {

                var user = _shopUserRepository.GetById(userId, shopId);

                if (user != null)
                    _shopUserRepository.Remove(user);

                return user;

            });
        }

        public MenuItem GetMenuItemById(int id, int shopId)
        {
            return Execute(() =>
            {
                var menuItem = _menuItemRepository.GetById(id, shopId);

                if (menuItem.MenuItemSubItems.Count > 0)
                    menuItem.HasSubOrder = true;

                return menuItem;
            });
        }

        public MenuItem SaveMenuItem(MenuItem item, int shopId)
        {
            return Execute(() =>
            {
                var menuItem = _menuItemRepository.GetById(item.Id, shopId);
                if (menuItem == null)
                {

                    item.Isactive = true;
                    item.Updated = DateTimeUtil.GetCurrentTime();
                    item.Created = DateTimeUtil.GetCurrentTime();
                    item.ShopId = shopId;
                    _menuItemRepository.Add(item);

                }
                else
                {
                    menuItem.UnitPrice = item.UnitPrice;
                    menuItem.ItemName = item.ItemName;
                    menuItem.TranslateName = item.TranslateName;
                    menuItem.ItemPic = item.ItemPic;
                    menuItem.ItemDescription = item.ItemDescription;
                    menuItem.ExtraComment = item.ExtraComment;
                    menuItem.MenuItemCategoryId = item.MenuItemCategoryId;
                    menuItem.Ishot = item.Ishot;
                    menuItem.SortOrder = item.SortOrder;
                    menuItem.NoSelection = item.NoSelection;
                    menuItem.FoodCategoryId = item.FoodCategoryId;
                    menuItem.ExcludeOnline = item.ExcludeOnline;
                    menuItem.RewardPoints = item.RewardPoints;
                    menuItem.Updated = DateTimeUtil.GetCurrentTime();
                    menuItem.ShopId = shopId;
                }
                return item;

            });
        }

        public MenuItem RemoveMenuItemById(int id, int shopId)
        {
            return Execute(() =>
            {
                var menuItem = _menuItemRepository.GetById(id, shopId);
                if (menuItem == null)
                    throw new BusinessEngineException(new System.Exception("menuItem do not exists"));

                menuItem.Isactive = false;
                menuItem.Updated = DateTimeUtil.GetCurrentTime();

                return menuItem;

            });
        }

        public Customer RemoveCustomerByPhone(string customerPhone, int shopId)
        {
            return Execute(() =>
            {

                var customer = _customerRepository.GetCustomerByPhone(customerPhone, shopId);
                if (customer == null)
                    return null;

                _customerRepository.Remove(customer);

                return customer;

            });
        }
    }
}
