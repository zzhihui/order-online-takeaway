using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Utility;
using Core.Common.Validation;
using FluentValidation;
using FluentValidation.Results;
using log4net;
using Takeaway.BusinessEngine.BusinessEngine;
using Takeaway.Data.Entities.Authentication;
using Takeaway.Data.Entities.Enum;
using Takeaway.Data.Entities.Payment;
using Takeaway.Data.Entities.Settings;
using Takeaway.Data.Entities.Shops;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Email;
using Takeaway.Service.Contract.Entities.Health;
using Takeaway.Service.Contract.Entities.Licence;
using Takeaway.Service.Contract.Entities.Payment;
using Takeaway.Service.Contract.Entities.Reports;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;
using Takeaway.Service.Exceptions;
using Takeaway.Service.Transformers;
using Takeaway.Service.Validations;

namespace Takeaway.Service.Services
{



    [ServiceHandler]
    public class ShopService : ServiceBase, IShopService
    {
        private readonly IShopBusinessEngine _shopBusinessEngine;
        private readonly ITransformer _transformer;
        protected readonly ILog Logger;


        public ShopService(IValidatorFactory validatorFactory,
                           IShopBusinessEngine shopBusinessEngine,
                           IUnitOfWork unitOfWork, ITransformer transformer)
            : base(validatorFactory, unitOfWork)
        {
            _shopBusinessEngine = shopBusinessEngine;
            _transformer = transformer;
            Logger = LogManager.GetLogger(this.GetType());
        }

        public WsShop GetShopInfo(int shopId)
        {
            return Query(() =>
            {
                Guard.GreaterThanZero(shopId);
                //Perform
                var shopInfo = _shopBusinessEngine.GetShopInfo(shopId);
                if (shopInfo == null)
                    return null;

                //Transform Response
                return _transformer.Transform<ShopInfo, WsShop>(shopInfo);

            });
        }

        public WsShop UpdateShopInfo(WsShop shopInfo, int option, int shopId)
        {
            return Execute(() =>
            {
                Guard.GreaterThanZero(shopId);
                Guard.NotNull(shopInfo);
                Guard.GreaterThanZero(shopInfo.Id);
                Guard.AreEqual(shopId, shopInfo.Id);

                var shopInfoTransformed = _transformer.Transform<WsShop, ShopInfo>(shopInfo);

                _shopBusinessEngine.UpdateShopInfo(shopInfoTransformed, shopId);

                return shopInfo;
            });
        }

        public WsShop GetShopStatus(int shopId)
        {
            return Query(() =>
            {
                Guard.GreaterThanZero(shopId);
                //Perform
                var shopInfo = _shopBusinessEngine.GetShopStatus(shopId);
                if (shopInfo == null)
                    return null;

                //Transform Response
                return _transformer.Transform<ShopInfo, WsShop>(shopInfo);

            });
        }

        public List<WsShop> GetAllShopsNeedTestOrder(int shopId)
        {
            return Query(() =>
            {
                Guard.GreaterThanZero(shopId);
                //Perform
                var shopInfo = _shopBusinessEngine.GetAllShopsNeedTestOrder(shopId);
                if (shopInfo == null)
                    return null;

                //Transform Response
                return _transformer.Transform<List<ShopInfo>, List<WsShop>>(shopInfo);

            });
        }


        public List<WsMenuItemCategory> GetMenuItemCategoryByGroupId(int groupId, int shopId)
        {
            return Query(() =>
            {
                //Perform
                var shopMenuCategories = _shopBusinessEngine.GetMenuItemCategoryByGroupId(groupId, shopId);
                if (shopMenuCategories == null)
                    return null;

                //Transform Response
                return _transformer.Transform<List<MenuItemCategory>, List<WsMenuItemCategory>>(shopMenuCategories);

            });
        }

        public List<WsMenuItemCategory> GetAdminMenuItemCategoryByGroupId(int groupId, int shopId)
        {
            return Query(() =>
            {
                //Perform
                var shopMenuCategories = _shopBusinessEngine.GetAdminMenuItemCategoryByGroupId(groupId, shopId);
                if (shopMenuCategories == null)
                    return null;

                //Transform Response
                return _transformer.Transform<List<MenuItemCategory>, List<WsMenuItemCategory>>(shopMenuCategories);

            });
        }
        



        public WsMenuItemCategory GetMenuItemCategoryById(int id, int shopId)
        {
            return Query(() =>
            {
                //Perform
                var shopMenuCategories = _shopBusinessEngine.GetMenuItemCategoryById(id, shopId);
                if (shopMenuCategories == null)
                    return null;

                //Transform Response
                return _transformer.Transform<MenuItemCategory, WsMenuItemCategory>(shopMenuCategories);

            });
        }

        public WsMenuItemCategory SaveMenuCategory(WsMenuItemCategory item, int shopId)
        {
            return Execute(() =>
            {

                var transformItem = _transformer.Transform<WsMenuItemCategory, MenuItemCategory>(item);

                //Perform
                var outputItem = _shopBusinessEngine.SaveMenuCategory(transformItem, shopId);
                if (outputItem == null)
                    return null;

                item.Id = outputItem.Id;
                return item;

            });
        }

        public WsMenuItemCategory RemoveMenuItemCategoryById(int id, int shopId)
        {
            return Execute(() =>
            {

                Guard.GreaterThanZero(id);
                var shopMenuItems = _shopBusinessEngine.RemoveMenuItemCategoryById(id, shopId);
                return shopMenuItems == null ? null : _transformer.Transform<MenuItemCategory, WsMenuItemCategory>(shopMenuItems);
            });
        }

        public List<WsMenuItem> GetMenuItemByCategoryGroupId(int groupId, int shopId)
        {
            return Query(() =>
            {

                var shopMenuItems = _shopBusinessEngine.GetMenuItemByCategoryGroupId(groupId, shopId);
                return shopMenuItems == null ? null : _transformer.Transform<List<MenuItem>, List<WsMenuItem>>(shopMenuItems);

            });
        }


        public List<WsMenuItem> GetMenuItemsByCategory(int categoryId, int shopId)
        {

            return Query(() =>
            {

                var shopMenuItems = _shopBusinessEngine.GetMenuItemsByCategory(categoryId, shopId);
                return shopMenuItems == null ? null : _transformer.Transform<List<MenuItem>, List<WsMenuItem>>(shopMenuItems);
            });

        }

        public List<WsMenuItem> GetMenuItemsList(int shopId)
        {
            return Query(() =>
            {
                var shopMenuItems = _shopBusinessEngine.GetMenuItemsList(shopId);
                return shopMenuItems == null ? null : _transformer.Transform<List<MenuItem>, List<WsMenuItem>>(shopMenuItems);
            });
        }

        public WsMenuItem SaveMenuItemWithSubItem(int extraId, int memuItemId, int shopId)
        {
            return Execute(() =>
            {
                var shopMenuItems = _shopBusinessEngine.SaveMenuItemWithSubItem(extraId, memuItemId, shopId);
                return shopMenuItems == null ? null : _transformer.Transform<MenuItem, WsMenuItem>(shopMenuItems);
            });
        }

        public WsMenuItem RemoveMenuItemWithSubItem(int extraId, int memuItemId, int shopId)
        {
            return Execute(() =>
            {
                var shopMenuItems = _shopBusinessEngine.RemoveMenuItemWithSubItem(extraId, memuItemId, shopId);
                return shopMenuItems == null ? null : _transformer.Transform<MenuItem, WsMenuItem>(shopMenuItems);
            });
        }

        public WsMenuItem RemoveMenuItemById(int id, int shopId)
        {
            return Execute(() =>
            {

                Guard.GreaterThanZero(id);
                var shopMenuItems = _shopBusinessEngine.RemoveMenuItemById(id, shopId);
                return shopMenuItems == null ? null : _transformer.Transform<MenuItem, WsMenuItem>(shopMenuItems);
            });
        }


        public WsOrder SubmitCustomerOrder(WsOrder orderItem, int shopId)
        {
            return Execute(() =>
            {

                //Verify Shop ID:
                //Add in to re-calculate shop id.
                var menuItemIds = orderItem.OrderItems.Select(item => item.Id).ToList();
                if (menuItemIds == null || menuItemIds.Count <= 0)
                    throw new ServiceException(new Exception("Invalid Order without Items"));
                var recalculatedShopId = _shopBusinessEngine.ReValidateShopIdByOrderItem(menuItemIds, shopId);

                Logger.DebugFormat("Validation as new Order");
                Validate(orderItem, WsOrderValidatorRule.NewOrder.ToString());

                Logger.DebugFormat("Get System Settings");
                var systemSetting = _shopBusinessEngine.GetSystemSettings(recalculatedShopId);

                Logger.DebugFormat("Get Shop Information");
                var shopInfo = _shopBusinessEngine.GetShopInfo(recalculatedShopId);

                Logger.DebugFormat("Transform Order");
                var orderTransformed = _transformer.Transform<WsOrder, Order>(orderItem);


                var rewardPoints = _shopBusinessEngine.UpdateCustomerRewardPoints(recalculatedShopId, orderTransformed);

                //Action
                if (!orderItem.StatusId.HasValue)
                    orderTransformed.StatusId = (int)OrderStatusIdEnum.Open;

                var savedOrder = _shopBusinessEngine.SaveOrder(orderTransformed, recalculatedShopId);
                //Save
                UnitOfWork.Commit();

                var emailSettings = _shopBusinessEngine.GetEmailSettings(systemSetting, recalculatedShopId);


                //Email Client
                if (_shopBusinessEngine.GetEmailSettingsAllowEmailClient(systemSetting, recalculatedShopId) && IsValidateEmail(orderTransformed.Email))
                    _shopBusinessEngine.EmailClientOrderConfirmation(orderTransformed, shopInfo, emailSettings, recalculatedShopId);

                //Email Shop
                if (_shopBusinessEngine.GetEmailSettingsAllowEmailShop(systemSetting, recalculatedShopId))
                    _shopBusinessEngine.EmailBossConfirmation(orderTransformed, shopInfo, shopInfo.Email, emailSettings, recalculatedShopId);

                if (savedOrder != null)
                {
                    orderItem.Id = savedOrder.Id;
                    orderItem.RewardPoints = rewardPoints;
                }


                return orderItem;

            });
        }

        private bool IsValidateEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public List<WsOrder> GetOpenOrders(int shopId)
        {
            return Query(() =>
            {

                var openOrders = _shopBusinessEngine.GetOpenOrders(shopId);
                return openOrders == null ? null : _transformer.Transform<List<Order>, List<WsOrder>>(openOrders);
            });
        }

        public WsOrder GetOrderById(int orderId, int shopId)
        {
            return Query(() =>
            {
                Guard.GreaterThanZero(orderId);

                var order = _shopBusinessEngine.GetOrder(orderId, shopId);
                return order == null ? null : _transformer.Transform<Order, WsOrder>(order);
            });
        }

        public WsOrder CloseOrderById(WsOrder orderItem, int shopId)
        {
            return Execute(() =>
            {
                Validate(orderItem, WsOrderValidatorRule.ExistingOrder.ToString());

                var order = _shopBusinessEngine.CloseOrder(orderItem.Id, shopId);
                return order == null ? null : _transformer.Transform<Order, WsOrder>(order);
            });
        }

        public WsOrder CancelOrderById(WsOrder orderItem, int shopId)
        {
            return Execute(() =>
            {
                Validate(orderItem, WsOrderValidatorRule.ExistingOrder.ToString());

                var order = _shopBusinessEngine.CancelOrder(orderItem.Id, shopId);
                return order == null ? null : _transformer.Transform<Order, WsOrder>(order);
            });
        }

        public WsOrder KillOrderById(WsOrder orderItem, int shopId)
        {
            return Execute(() =>
            {
                Validate(orderItem, WsOrderValidatorRule.ExistingOrder.ToString());

                var order = _shopBusinessEngine.RemoveOrder(orderItem.Id, shopId);
                return order == null ? null : _transformer.Transform<Order, WsOrder>(order);
            });
        }

        public List<WsOrder> GetCompleteOrders(WsOrderSearch search, int shopId)
        {
            return Execute(() =>
            {

                var openOrders = _shopBusinessEngine.GetCompleteOrders(search.SearchDate, shopId);
                return openOrders == null ? null : _transformer.Transform<List<Order>, List<WsOrder>>(openOrders);
            });
        }

        public List<WsShopSpecialOffer> GetLatestOffer(int lastOffer, int userId, int shopId)
        {
            return new List<WsShopSpecialOffer>(); //TODO: Impliement Logic

        }

        public WsShopCustomer RegisterCustomer(WsShopCustomer customer, int shopId)
        {
            return Execute(() =>
            {

                Validate(customer);
                var shopCustomerPocoIn = _transformer.Transform<WsShopCustomer, Customer>(customer);

                var shopCustomer = _shopBusinessEngine.RegisterCustomer(shopCustomerPocoIn, shopId);
                UnitOfWork.Commit();

                var shopCustomerTransformed = _transformer.Transform<Customer, WsShopCustomer>(shopCustomer);
                return shopCustomerTransformed;

            });
        }

        public WsShopCustomer UpdateCustomerInfo(WsShopCustomer customer, int shopId)
        {
            return Execute(() =>
            {
                Guard.NotNull(customer);
                Guard.GreaterThanZero(customer.Id);
                Guard.NotNull(customer.Email);

                var shopCustomerPocoIn = _transformer.Transform<WsShopCustomer, Customer>(customer);

                var shopCustomer = _shopBusinessEngine.UpdateCustomerInfo(shopCustomerPocoIn, shopId);
                UnitOfWork.Commit();

                var shopCustomerTransformed = _transformer.Transform<Customer, WsShopCustomer>(shopCustomer);
                return shopCustomerTransformed;

            });
        }

        public WsShopCustomer ResetCustomerPassword(WsShopCustomer customer, int shopId)
        {
            return Execute(() =>
            {

                Guard.NotNull(customer);
                Guard.GreaterThanZero(customer.Id);
                Guard.NotNull(customer.Email);
                var shopCustomerPocoIn = _transformer.Transform<WsShopCustomer, Customer>(customer);

                var shopCustomer = _shopBusinessEngine.ResetCustomerPassword(shopCustomerPocoIn, shopId);
                UnitOfWork.Commit();

                var shopCustomerTransformed = _transformer.Transform<Customer, WsShopCustomer>(shopCustomer);
                return shopCustomerTransformed;

            });
        }



        public WsShopCustomer VerifySms(WsShopCustomer customer, int shopId)
        {
            return Execute(() =>
            {

                Guard.NotNull(customer.Email);
                var shopCustomerPocoIn = _transformer.Transform<WsShopCustomer, Customer>(customer);

                var shopCustomer = _shopBusinessEngine.VerifySms(shopCustomerPocoIn, shopId);

                var shopCustomerTransformed = _transformer.Transform<Customer, WsShopCustomer>(shopCustomer);
                return shopCustomerTransformed;

            });
        }

        public WsShopCustomer GetCustomerById(int id, int shopId)
        {
            return Query(() =>
            {
                //Perform
                var customer = _shopBusinessEngine.GetCustomerById(id, shopId);
                if (customer == null)
                    return null;

                //Transform Response
                return _transformer.Transform<Customer, WsShopCustomer>(customer);

            });
        }

        public WsShopCustomer ActivateBySms(WsShopCustomer registeredCustomer, int shopId)
        {
            return Execute(() =>
            {
                //Perform
                var shopCustomerPocoIn = _transformer.Transform<WsShopCustomer, Customer>(registeredCustomer);
                var customer = _shopBusinessEngine.ActivateBySms(shopCustomerPocoIn, shopId);
                if (customer == null)
                    return null;

                //Transform Response
                return _transformer.Transform<Customer, WsShopCustomer>(customer);

            });
        }

        public WsShopCustomer Forgetpassword(string email, int shopId)
        {
            return Execute(() =>
            {
                //Perform
                var customer = _shopBusinessEngine.Forgetpassword(email, shopId);
                if (customer == null)
                    return null;

                //Transform Response
                return _transformer.Transform<Customer, WsShopCustomer>(customer);

            });
        }

        public WsShopCustomer LoginCustomer(string email, string password, int shopId)
        {
            return Execute(() =>
            {
                //Perform
                var customer = _shopBusinessEngine.LoginCustomer(email, password, shopId);
                if (customer == null)
                    return null;

                //Transform Response
                return _transformer.Transform<Customer, WsShopCustomer>(customer);

            });
        }



        public List<WsSetting> GetSystemSettings(int shopId)
        {
            return Query(() =>
            {
                //Perform
                var systemSettings = _shopBusinessEngine.GetSystemSettings(shopId);
                if (systemSettings == null)
                    return null;

                //Transform Response
                return _transformer.Transform<List<ServerSetting>, List<WsSetting>>(systemSettings);

            });
        }

        public List<WsSetting> GetClientSystemSettings(int shopId)
        {
            return Query(() =>
            {
                //Perform
                var systemSettings = _shopBusinessEngine.GetClientSystemSettings(shopId);
                if (systemSettings == null)
                    return null;

                //Transform Response
                return _transformer.Transform<List<ServerSetting>, List<WsSetting>>(systemSettings);

            });
        }

        public List<WsDeliveryOption> GetDeliveryOptions(int shopId)
        {
            return Query(() =>
            {
                //Perform
                var deliveryOptions = _shopBusinessEngine.GetDeliveryOptions(shopId);
                if (deliveryOptions == null)
                    return null;

                //Transform Response
                return _transformer.Transform<List<DeliveryOption>, List<WsDeliveryOption>>(deliveryOptions);

            });
        }

        public List<WsDeliveryOption> GetDeliveryOptionsByTime(int shopId)
        {
            return Query(() =>
            {
                //Perform
                var deliveryOptions = _shopBusinessEngine.GetDeliveryOptionsByTime(shopId);
                if (deliveryOptions == null)
                    return null;

                //Transform Response
                return _transformer.Transform<List<DeliveryOption>, List<WsDeliveryOption>>(deliveryOptions);


            });
        }

        public List<WsDeliveryOption> GetDeliveryOptionsInternal(int shopId)
        {
            return Query(() =>
            {
                //Perform
                var deliveryOptions = _shopBusinessEngine.GetDeliveryOptionsInternal(shopId);
                if (deliveryOptions == null)
                    return null;

                //Transform Response
                return _transformer.Transform<List<DeliveryOption>, List<WsDeliveryOption>>(deliveryOptions);

            });
        }

        public WsOrder DiscountCustomerOrder(WsOrder orderItem, int shopId)
        {

            return Query<WsOrder>(() =>
            {
                Validate(orderItem, WsOrderValidatorRule.NewOrder.ToString());

                var orderTransformed = _transformer.Transform<WsOrder, Order>(orderItem);

                if (DiscountShouldApply(orderTransformed.OrderOriginalTotal, shopId))
                {
                    var discountedOrder = _shopBusinessEngine.DiscountOrder(orderTransformed, shopId);

                    orderItem.DiscountRules = discountedOrder.DiscountRef;
                    orderItem.OrderTotal = orderItem.OrderOriginalTotal - discountedOrder.Amount;
                }
                DiscountDeliveryCharge(orderItem, shopId);

                return orderItem;

            });

        }

        private bool DiscountShouldApply(decimal orderOriginalTotal, int shopId)
        {
            switch (shopId)
            {
                case (int)WsShopIdEnum.AOKI:
                    return orderOriginalTotal >= 20;
                default:
                    return true;

            }
        }

        private void DiscountDeliveryCharge(WsOrder orderItem, int shopId)
        {
            switch (shopId)
            {
                case (int)WsShopIdEnum.AOKI:
                    if (orderItem.OrderTotal > 30)
                        orderItem.DeliveryCharge = (decimal)0.001;
                    else
                        orderItem.DeliveryCharge = 0;
                    break;
                default:
                    return;

            }

        }

        public WsShopUser LoginShopAgent(string pin, bool online, int shopId)
        {


            return Query<WsShopUser>(() =>
            {
                Guard.NotNull(pin);

                var user = _shopBusinessEngine.LoginShopUserByPin(pin, shopId);
                if (user == null)
                    return null;
                var userTransformed = _transformer.Transform<ShopUser, WsShopUser>(user);

                return userTransformed;



            });

        }

        public List<WsMenuItem> GetHotMenuItems(int shopId)
        {
            return Query(() =>
            {
                var shopMenuItems = _shopBusinessEngine.GetHotMenuItems(shopId);
                return shopMenuItems == null ? null : _transformer.Transform<List<MenuItem>, List<WsMenuItem>>(shopMenuItems);
            });
        }

        public List<WsMenuItem> GetHotMenuItemsInternal(int shopId)
        {
            return Query(() =>
            {
                var shopMenuItems = _shopBusinessEngine.GetHotMenuItemsInternal(shopId);
                return shopMenuItems == null ? null : _transformer.Transform<List<MenuItem>, List<WsMenuItem>>(shopMenuItems);
            });
        }

        public List<WsPaymentMonth> LoadPaymentMonths(int shopId)
        {
            var paymentMonths = new List<WsPaymentMonth>();

            for (var i = 1; i <= 12; i++)
            {
                paymentMonths.Add(new WsPaymentMonth() { Id = i, Name = DateTimeFormatInfo.CurrentInfo.GetMonthName(i) });
            }

            return paymentMonths;
        }

        public List<WsPaymentYear> LoadPaymentYears(int shopId)
        {
            var paymentYearList = new List<WsPaymentYear>();
            for (var i = 0; i <= 10; i++)
            {
                paymentYearList.Add(new WsPaymentYear() { Id = DateTimeUtil.GetCurrentTime().AddYears(i).Year, Name = DateTimeUtil.GetCurrentTime().AddYears(i).Year.ToString() });
            }

            return paymentYearList;
        }

        public WsPaymentInfo TakePayment(WsPayment payment, int shopId)
        {

            return Query(() =>
            {

                Validate(payment);

                Logger.DebugFormat("Get System Settings");
                var systemSetting = _shopBusinessEngine.GetSystemSettings(shopId);
                var apiKey = _shopBusinessEngine.GetPaymentApiKey(systemSetting);

                var paymentTransformed = _transformer.Transform<WsPayment, Payment>(payment);

                var paymentResults = _shopBusinessEngine.TakePayment(paymentTransformed, apiKey, shopId);

                var wsPaymentResult = _transformer.Transform<PaymentInfo, WsPaymentInfo>(paymentResults);
                return wsPaymentResult;
            });
        }


        public WsPaymentInfo TakeLoyaltyPayment(WsPayment payment, int customerId, int shopId)
        {

            return Execute(() =>
            {

                Guard.NotNull(payment);
                if (payment.Amount <= 0)
                {
                    throw new ValidationException(new[] { new ValidationFailure("Amount", "Invalid Payment Amount") });
                }
                Guard.GreaterThanZero(customerId);

                var paymentTransformed = _transformer.Transform<WsPayment, Payment>(payment);

                var paymentResults = _shopBusinessEngine.TakeLoyaltyPayment(paymentTransformed, customerId, shopId);

                var wsPaymentResult = _transformer.Transform<PaymentInfo, WsPaymentInfo>(paymentResults);

                return wsPaymentResult;
            });
        }

        public WsSyncStatus SyncStatus(int shopId)
        {
            return Execute(() =>
            {

                var result = _shopBusinessEngine.SyncStatus(shopId);
                return new WsSyncStatus() { Result = result };

            });
        }


        public WsEmailResponse EmailException(string message, int shopId)
        {
            return Query(() =>
            {
                var response = new WsEmailResponse();
                Guard.NotNull(message);

                var shopInfo = _shopBusinessEngine.GetShopInfo(shopId);
                var systemSetting = _shopBusinessEngine.GetSystemSettings(shopId);
                var emailSettings = _shopBusinessEngine.GetEmailSettings(systemSetting, shopId);
                var indaSoftEmail =
                    systemSetting.FirstOrDefault(r => r.SettingKey == WsOfflineSettingEnum.IndaSoftEmail.ToString());

                response.IsSuccess = _shopBusinessEngine.EmailException(message, shopInfo.Email, indaSoftEmail.SettingValue, emailSettings, shopId);

                return response;

            });
        }

        public WsShopCustomer GetCustomerByPhoneNumber(string customerPhone, int shopId)
        {
            return Query(() =>
            {

                Guard.NotNull(customerPhone);

                var shopCustomer = _shopBusinessEngine.GetCustomerByPhoneNumber(customerPhone, shopId);
                if (shopCustomer == null)
                    return new WsShopCustomer();

                var shopCustomerTransformed = _transformer.Transform<Customer, WsShopCustomer>(shopCustomer);
                return shopCustomerTransformed;

            });
        }

        public List<WsOrder> GetCustomerOrders(WsShopCustomer customer, int shopId)
        {
            return Query(() =>
            {

                Guard.NotNull(customer);
                Guard.NotNull(customer.PhoneNumber);

                var customerOrders = _shopBusinessEngine.GetCustomerOrders(customer.PhoneNumber, shopId);

                var list = customerOrders.AsEnumerable().Select(dr => new WsOrder
                {
                    Id = dr.Field<int>("ID"),
                    OrderTotal = dr.Field<decimal>("OrderTotal"),
                    Created = dr.Field<DateTime>("Created"),
                }).OrderByDescending(s => s.Created).ToList();

                return list;

            });
        }

        public List<WsShopCustomer> GetAllCustomer(int shopId)
        {
            return Query(() =>
            {
                var shopCustomer = _shopBusinessEngine.GetAllCustomer(shopId);
                var shopCustomerTransformed = _transformer.Transform<List<Customer>, List<WsShopCustomer>>(shopCustomer);
                return shopCustomerTransformed;

            });
        }

        public WsLicenceInfo CheckLicence(string hwid, int shopId)
        {
            return Query(() =>
            {

                Guard.NotNull(hwid);

                var licResponse = new WsLicenceInfo();

                var shopLic = _shopBusinessEngine.CheckLicence(hwid, shopId);
                licResponse.IsValidLicence = shopLic;

                return licResponse;

            });
        }

        public WsReport GetReport(WsReport report, int shopId)
        {
            return Query(() =>
            {

                Guard.GreaterThanZero(report.ReportTypeId);

                switch (report.ReportTypeId)
                {
                    case (int)ReportType.Summary:

                        report.ReportResults = _shopBusinessEngine.GetReport_SummaryReport(report.FromDate, shopId);
                        break;

                    case (int)ReportType.TransactionReport:

                        report.ReportResults = _shopBusinessEngine.GetReport_TransactionReport(report.FromDate, shopId);
                        break;


                    case (int)ReportType.TransactionDrinkReport:

                        report.ReportResults = _shopBusinessEngine.GetReport_TransactionDrinkReport(report.FromDate, shopId);
                        break;

                    case (int)ReportType.OrderFoodReport:

                        report.ReportResults = _shopBusinessEngine.GetReport_OrdeFoodReport(report.FromDate, shopId);
                        break;


                    case (int)ReportType.DriverReport:

                        report.ReportResults = _shopBusinessEngine.GetReport_DriverReport(report.FromDate, shopId);
                        break;


                    case (int)ReportType.CustomerReport:

                        report.ReportResults = _shopBusinessEngine.GetReport_CustomerReport(shopId);
                        break;

                    case (int)ReportType.SummaryMulti:

                        report.ReportResults = _shopBusinessEngine.GetReport_SummaryReport(report.FromDate, report.ToDate, shopId);
                        break;

                    case (int)ReportType.TransactionReportMulti:

                        report.ReportResults = _shopBusinessEngine.GetReport_TransactionReport(report.FromDate, report.ToDate, shopId);
                        break;

                    case (int)ReportType.BusyHourReport:

                        report.ReportResults = _shopBusinessEngine.GetReport_BusyHourReport(report.FromDate, shopId);
                        break;
                    case (int)ReportType.CustomerOrderReport:

                        report.ReportResults = _shopBusinessEngine.GetReport_CustomerOrderReport(shopId);
                        break;
                    case (int)ReportType.SummaryOnline:

                        report.ReportResults = _shopBusinessEngine.GetReport_SummaryOnline(report.FromDate, shopId);
                        break;
                    case (int)ReportType.TransactionReportOnline:

                        report.ReportResults = _shopBusinessEngine.GetReport_TransactionReportOnline(report.FromDate, shopId);
                        break;

                    case (int)ReportType.TransactionDrinkReportMulti:

                        report.ReportResults = _shopBusinessEngine.GetReport_TransactionDrinkReportMulti(report.FromDate, report.ToDate, shopId);
                        break;

                    case (int)ReportType.AdminReportSummaryOnlineReport:
                        report.ReportResults = _shopBusinessEngine.GetReport_AdminReportSummaryOnlineReport(report.FromDate, report.ToDate, shopId);
                        break;

                    case (int)ReportType.AdminReportTransactionOnlineReport:
                        report.ReportResults = _shopBusinessEngine.GetReport_AdminReportTransactionOnlineReport(report.FromDate, report.ToDate, shopId);
                        break;
                    case (int)ReportType.AdminReportStatusReportReport:
                        report.ReportResults = _shopBusinessEngine.GetAdminReport_Status();
                        break;

                    case (int)ReportType.ReportInvoiceOnline:
                        report.ReportResults = _shopBusinessEngine.GetReportInvoiceOnline(report.FromDate, report.ToDate, shopId);
                        break;


                    case (int)ReportType.AdminReportInvoice:
                        report.ReportResults = _shopBusinessEngine.GetAdminReportInvoice();
                        break;
                    default:
                        break;


                }


                return report;

            });
        }

        public WsReport GetAdminReport(WsReport report, int shopId)
        {
            return Query(() =>
            {

                if (report.ReportTypeId != 1)
                    throw new ValidationException(new[] { new ValidationFailure("Integer", "Invalid Admin Id") });


                switch (report.ReportTypeId)
                {
                    case 1:
                        report.ReportResults = _shopBusinessEngine.GetAdminReport_Status();
                        break;
                    case 2:
                        report.ReportResults = _shopBusinessEngine.GetCustomerPayOutReport();
                        break;
                    default:
                        break;


                }


                return report;

            });
        }

        public WsOptionItem SaveOptionItem(WsOptionItem item, int shopId)
        {
            return Execute(() =>
            {

                var transformItem = _transformer.Transform<WsOptionItem, OptionItem>(item);

                //Perform
                var outputItem = _shopBusinessEngine.SaveOptionItem(transformItem, shopId);
                if (outputItem == null)
                    return null;

                item.Id = outputItem.Id;
                return item;

            });
        }

        public WsOptionItem GetOptionItemById(int id, int shopId)
        {
            return Query(() =>
            {
                //Perform
                var outputItem = _shopBusinessEngine.GetOptionItemById(id, shopId);

                if (outputItem == null)
                    return null;

                var transformItem = _transformer.Transform<OptionItem, WsOptionItem>(outputItem);
                return transformItem;

            });
        }

        public WsOptionItem RemoveOptionItemById(int id, int shopId)
        {
            return Execute(() =>
            {

                //Perform
                var outputItem = _shopBusinessEngine.RemoveOptionItemById(id, shopId);
                if (outputItem == null)
                    return null;
                var transformItem = _transformer.Transform<OptionItem, WsOptionItem>(outputItem);
                return transformItem;

            });
        }

        public WsSetting GetSystemSettingById(int id, int shopId)
        {
            return Query(() =>
            {

                //Perform
                var outputItem = _shopBusinessEngine.GetSystemSettingById(id, shopId);
                if (outputItem == null)
                    return null;
                var transformItem = _transformer.Transform<ServerSetting, WsSetting>(outputItem);
                return transformItem;

            });
        }

        public WsSetting SaveSystemSetting(WsSetting selectedItem, int shopId)
        {
            return Execute(() =>
            {

                Guard.GreaterThanZero(selectedItem.Id);

                var transformItem = _transformer.Transform<WsSetting, ServerSetting>(selectedItem);

                //Perform
                var outputItem = _shopBusinessEngine.SaveSystemSetting(transformItem, shopId);
                if (outputItem == null)
                    return null;
                return selectedItem;

            });
        }

        public WsDeliveryCost GetDeliveryCostsById(int id, int shopId)
        {
            return Execute(() =>
            {

                //Perform
                var outputItem = _shopBusinessEngine.GetDeliveryCostsById(id, shopId);
                if (outputItem == null)
                    return null;
                var transformItem = _transformer.Transform<DeliveryCost, WsDeliveryCost>(outputItem);
                return transformItem;

            });
        }

        public WsDeliveryCost SaveDeliveryCost(WsDeliveryCost selectedItem, int shopId)
        {
            return Execute(() =>
            {

                Guard.GreaterThanZero(selectedItem.Id);

                var transformItem = _transformer.Transform<WsDeliveryCost, DeliveryCost>(selectedItem);

                //Perform
                var outputItem = _shopBusinessEngine.SaveDeliveryCost(transformItem, shopId);
                if (outputItem == null)
                    return null;
                return selectedItem;

            });
        }

        public List<WsShopNews> GetShopNews(int shopId)
        {
            return Query(() =>
            {
                //Perform
                var shopNews = _shopBusinessEngine.GetShopNews(shopId);
                if (shopNews == null)
                    return null;

                //Transform Response
                return _transformer.Transform<List<ShopNews>, List<WsShopNews>>(shopNews);

            });
        }

        public WsShopNews SaveShopNews(WsShopNews selectedItem, int shopId)
        {
            return Execute(() =>
            {
                var transformItem = _transformer.Transform<WsShopNews, ShopNews>(selectedItem);

                //Perform
                var outputItem = _shopBusinessEngine.SaveShopNews(transformItem, shopId);
                if (outputItem == null)
                    return null;
                return selectedItem;

            });
        }

        public WsShopNews GetShopNewsById(int id, int shopId)
        {
            return Query(() =>
            {

                //Perform
                var outputItem = _shopBusinessEngine.GetShopNewsById(id, shopId);
                if (outputItem == null)
                    return null;

                var transformItem = _transformer.Transform<ShopNews, WsShopNews>(outputItem);
                return transformItem;

            });
        }

        public WsShopNews RemoveShopNewsById(int id, int shopId)
        {
            return Execute(() =>
            {

                //Perform
                var outputItem = _shopBusinessEngine.RemoveShopNewsById(id, shopId);
                if (outputItem == null)
                    return null;

                var transformItem = _transformer.Transform<ShopNews, WsShopNews>(outputItem);
                return transformItem;

            });
        }

        public List<WsMenuSubItem> GetAllMenuSubItems(int shopId)
        {
            return Execute(() =>
            {
                //Perform
                var outputItem = _shopBusinessEngine.GetAllMenuSubItems(shopId);
                if (outputItem == null)
                    return null;
                var transformItem = _transformer.Transform<List<MenuSubItem>, List<WsMenuSubItem>>(outputItem);
                return transformItem;

            });
        }

        public WsMenuSubItem RemoveMenuSubItemById(int id, int shopId)
        {
            return Execute(() =>
            {

                //Perform
                var outputItem = _shopBusinessEngine.RemoveMenuSubItemById(id, shopId);
                if (outputItem == null)
                    return null;

                var transformItem = _transformer.Transform<MenuSubItem, WsMenuSubItem>(outputItem);
                return transformItem;

            });
        }

        public WsMenuSubItem SaveMenuSubItem(WsMenuSubItem selectedMenuSubItem, int shopId)
        {
            return Execute(() =>
            {

                var transformInItem = _transformer.Transform<WsMenuSubItem, MenuSubItem>(selectedMenuSubItem);

                //Perform
                var outputItem = _shopBusinessEngine.SaveMenuSubItem(transformInItem, shopId);
                if (outputItem == null)
                    return null;

                var transformItem = _transformer.Transform<MenuSubItem, WsMenuSubItem>(outputItem);
                return transformItem;

            });
        }

        public WsMenuSubItem GetMenuSubItemsById(int id, int shopId)
        {
            return Execute(() =>
            {
                //Perform
                var outputItem = _shopBusinessEngine.GetMenuSubItemsById(id, shopId);
                if (outputItem == null)
                    return null;

                var transformItem = _transformer.Transform<MenuSubItem, WsMenuSubItem>(outputItem);
                return transformItem;

            });
        }

        public WsMenuSubItem RemoveMenuSubItemLink(int id, int menuItemId, int shopId)
        {
            return Execute(() =>
            {

                //Perform
                var outputItem = _shopBusinessEngine.RemoveMenuSubItemLink(id, menuItemId, shopId);
                if (outputItem == null)
                    return null;

                var transformItem = _transformer.Transform<MenuSubItem, WsMenuSubItem>(outputItem);
                return transformItem;

            });
        }

        public WsMenuSubItem SaveMenuSubItemWithItem(int id, int selectedMenuItemId, decimal cost, int shopId)
        {
            return Execute(() =>
            {
                //Perform
                var outputItem = _shopBusinessEngine.SaveMenuSubItemWithItem(id, selectedMenuItemId, cost, shopId);
                if (outputItem == null)
                    return null;

                var transformItem = _transformer.Transform<MenuSubItem, WsMenuSubItem>(outputItem);
                return transformItem;

            });
        }

        public string GetEncryptedPassword(string password, int shopId)
        {
            return Execute(() =>
            {

                Guard.GreaterThanZero(shopId);

                //Perform
                return _shopBusinessEngine.GetEncryptedPassword(password, shopId);


            });
        }

        public List<WsOrderHolder> GetHoldOrders(int shopId)
        {
            throw new NotImplementedException();
        }

        public List<WsOrder> GetLocalCompleteOrders(WsOrderSearch searchFilter, int shopId)
        {
            throw new NotImplementedException();
        }

        public void CancelLocalOrderById(WsOrder selectedOrder, int shopId)
        {
            throw new NotImplementedException();
        }

        public WsShopCustomer RemoveCustomer(string customerPhone, int shopId)
        {
            return Execute(() =>
           {
               //Perform
               var customer = _shopBusinessEngine.RemoveCustomerByPhone(customerPhone, shopId);
               var shopCustomerTransformed = _transformer.Transform<Customer, WsShopCustomer>(customer);
               return shopCustomerTransformed;

           });
        }

        public void SyncWithServer(int shopId)
        {
            //This is not a Server Action
        }

        public void InitSyncWithServer(int shopId)
        {
            //This is not a Server Action
        }

        public List<WsOfflineSetting> GetOfflineSettings(int shopId)
        {
            throw new NotImplementedException();
        }

        public void SaveOfflineSettings(string appMaxOrder, string toString, int shopId)
        {
            throw new NotImplementedException();
        }

        public List<WsOptionItem> GetOptionItems(int shopId)
        {
            return Query(() =>
            {
                //Perform
                var optionItems = _shopBusinessEngine.GetOptionItems(shopId);
                if (optionItems == null)
                    return null;

                //Transform Response
                return _transformer.Transform<List<OptionItem>, List<WsOptionItem>>(optionItems);

            });
        }

        public List<WsMenuItemCategoryGroup> GetMenuItemCategoryGroup(int shopId)
        {
            return Query(() =>
            {
                //Perform
                var items = _shopBusinessEngine.GetMenuItemCategoryGroup(shopId);
                if (items == null)
                    return null;

                //Transform Response
                return _transformer.Transform<List<MenuItemCategoryGroup>, List<WsMenuItemCategoryGroup>>(items);

            });
        }

        public WsMenuItem GetMenuItemById(int id, int shopId)
        {
            return Query(() =>
            {
                //Perform
                var item = _shopBusinessEngine.GetMenuItemById(id, shopId);
                if (item == null)
                    return null;

                //Transform Response
                return _transformer.Transform<MenuItem, WsMenuItem>(item);

            });
        }

        public WsMenuItem SaveMenuItem(WsMenuItem menuItem, int shopId)
        {
            return Execute(() =>
            {

                var item = _transformer.Transform<WsMenuItem, MenuItem>(menuItem);

                //Perform
                var outputItem = _shopBusinessEngine.SaveMenuItem(item, shopId);
                if (outputItem == null)
                    return null;

                return menuItem;

            });
        }

        public List<WsTables> GetExistingTables(int tableSize, int shopId)
        {
            throw new NotImplementedException();
        }

        public void SaveTableSettings(WsTables table, int shopId)
        {
            throw new NotImplementedException();
        }

        public WsTables GetTableByTableId(int tableId, int shopId)
        {
            throw new NotImplementedException();
        }

        public List<WsShopUser> GetAllShopUsers(int shopId)
        {
            return Query<List<WsShopUser>>(() =>
            {

                var users = _shopBusinessEngine.GetAllShopUsers(shopId);
                if (users == null)
                    return null;
                var userTransformed = _transformer.Transform<List<ShopUser>, List<WsShopUser>>(users);

                return userTransformed;



            });
        }

        public List<WsTableCommand> GetTableCommand(int shopId)
        {
            return Query(() =>
            {
                //Perform
                var optionItems = _shopBusinessEngine.GetTableCommand(shopId);
                if (optionItems == null)
                    return null;

                //Transform Response
                return _transformer.Transform<List<TableCommand>, List<WsTableCommand>>(optionItems);

            });
        }

        public List<WsDriver> GetAllDrivers(int shopId)
        {
            return Query(() =>
            {
                //Perform
                var optionItems = _shopBusinessEngine.GetAllDrivers(shopId);
                if (optionItems == null)
                    return null;

                //Transform Response
                return _transformer.Transform<List<Driver>, List<WsDriver>>(optionItems);

            });
        }

        public WsOrder UpdateOrder(WsOrder selectedOrder, int updateDriver, int shopId)
        {
            return Execute(() =>
            {
                Validate(selectedOrder, WsOrderValidatorRule.ExistingOrder.ToString());

                Order order = null;

                switch (updateDriver)
                {
                    case (int)UpdateOrderOption.UpdateDriver:
                        order = _shopBusinessEngine.GetOrder(selectedOrder.Id, shopId);
                        order.DriverName = selectedOrder.DriverName;
                        break;

                    case (int)UpdateOrderOption.UpdatePaymentType:
                        order = _shopBusinessEngine.GetOrder(selectedOrder.Id, shopId);
                        order.PaymentType = selectedOrder.PaymentType;
                        break;

                    case (int)UpdateOrderOption.PrintOnlineOrder:

                        var orderTransformed = _transformer.Transform<WsOrder, Order>(selectedOrder);

                        order = _shopBusinessEngine.GetOrder(selectedOrder.Id, shopId);
                        order.StatusId = (int)OrderStatusIdEnum.SystemRemoved;

                        orderTransformed.StatusId = (int)OrderStatusIdEnum.Open;

                        var savedOrder = _shopBusinessEngine.SaveOrder(orderTransformed, shopId);
                        UnitOfWork.Commit();

                        selectedOrder.Id = savedOrder.Id;
                        break;
                }

                return order == null ? null : _transformer.Transform<Order, WsOrder>(order);
            });
        }

        public List<WsPaymentType> GetAllPaymentTypes(int shopId)
        {
            return Query(() =>
            {
                //Perform
                var optionItems = _shopBusinessEngine.GetAllPaymentTypes(shopId);
                if (optionItems == null)
                    return null;

                //Transform Response
                return _transformer.Transform<List<PaymentType>, List<WsPaymentType>>(optionItems);

            });
        }

        public WsMergeOrder MergeOrder(WsMergeOrder mergeOrder, int shopId)
        {
            return Execute(() =>
            {
                Validate(mergeOrder.SelectedOrder, WsOrderValidatorRule.ExistingOrder.ToString());
                Validate(mergeOrder.CurrentOrder, WsOrderValidatorRule.ExistingOrder.ToString());

                var currentOrder = _shopBusinessEngine.GetOrder(mergeOrder.CurrentOrder.Id, shopId);
                var selectedOrder = _shopBusinessEngine.GetOrder(mergeOrder.SelectedOrder.Id, shopId);

                foreach (var orderItem in selectedOrder.OrderItems)
                {
                    orderItem.OrderId = currentOrder.Id;
                }

                currentOrder.OrderOriginalTotal += selectedOrder.OrderOriginalTotal;
                currentOrder.OrderTotal += selectedOrder.OrderTotal;

                selectedOrder.StatusId = (int)OrderStatusIdEnum.Cancelled;

                return mergeOrder;
            });
        }

        public WsOrderHolder HotTempOrder(WsOrderHolder holdOrder, int shopId)
        {
            throw new NotImplementedException();
        }

        public List<WsTableAlias> GetTableAlias(int shopId)
        {
            return Query(() =>
            {
                //Perform
                var optionItems = _shopBusinessEngine.GetTableAlias(shopId);
                if (optionItems == null)
                    return null;

                //Transform Response
                return _transformer.Transform<List<TableAlias>, List<WsTableAlias>>(optionItems);

            });
        }

        public string GetNextOrderReference(WsDeliveryOption selectedDeliveryOption, bool isWalkIn, int shopId)
        {
            throw new NotImplementedException();
        }

        public WsOrder UpdateCloudOrder(WsOrder myOrder, int shopId)
        {
            return Execute(() =>
            {
                Validate(myOrder, WsOrderValidatorRule.ExistingOrder.ToString());

                //Transfom
                var orderTransformed = _transformer.Transform<WsOrder, Order>(myOrder);


                var currentOrder = _shopBusinessEngine.GetOrderByGuid(myOrder.Guid, shopId);
                if (currentOrder != null)
                {
                    currentOrder.StatusId = (int)OrderStatusIdEnum.SystemRemoved;
                    currentOrder.Updated = DateTimeUtil.GetCurrentTime();
                }


                if (!myOrder.StatusId.HasValue)
                    orderTransformed.StatusId = (int)OrderStatusIdEnum.Open;

                //Action
                var savedOrder = _shopBusinessEngine.SaveOrder(orderTransformed, shopId);
                //Save
                UnitOfWork.Commit();

                myOrder.Id = savedOrder.Id;

                return myOrder;
            });
        }

        public List<WsDeliveryArea> GetDeliveryArea(int deliveryArea, int shopId)
        {
            return Execute(() =>
            {
                var deliverAreas = _shopBusinessEngine.GetDeliveryArea(deliveryArea, shopId);
                var wsdeliverAreasTransform = _transformer.Transform<List<DeliveryArea>, List<WsDeliveryArea>>(deliverAreas);
                return wsdeliverAreasTransform;
            });
        }

        public List<WsDeliveryCost> GetDeliveryCosts(int shopId)
        {
            return Execute(() =>
            {
                var deliverCosts = _shopBusinessEngine.GetDeliveryCosts(shopId);
                var wsdeliverAreasTransform = _transformer.Transform<List<DeliveryCost>, List<WsDeliveryCost>>(deliverCosts);
                return wsdeliverAreasTransform;
            });
        }

        public WsShopCustomer SaveCustomerLocal(string customerPhone, string customerAddress, string customerName, string customerEmail,
            decimal? customerDeliveryCharge, int shopId)
        {
            throw new NotImplementedException();
        }

        public WsShopCustomer SaveCustomerCloud(string customerPhone, string customerAddress, string customerName, string customerEmail,
            decimal? customerDeliveryCharge, int shopId)
        {
            return Execute(() =>
            {
                var customer = new WsShopCustomer()
                {
                    PhoneNumber = customerPhone,
                    Address = customerAddress,
                    Email = customerEmail,
                    ContactName = customerEmail,
                    DeliveryCharge = customerDeliveryCharge
                };

                _shopBusinessEngine.SaveCustomerInfo(customerPhone, customerAddress, customerName, customerEmail, customerDeliveryCharge, shopId);


                return customer;

            });
        }
        public WsShopUser SaveShopUser(WsShopUser newShopUser, int shopId)
        {
            return Execute(() =>
            {

                //Validate Requestupdate create user
                Validate(newShopUser);

                //Transform Request
                var shopUserTransformed = _transformer.Transform<WsShopUser, ShopUser>(newShopUser);


                //Check pin already in user
                if (_shopBusinessEngine.CheckUserPinExsits(shopUserTransformed, shopId))
                {
                    newShopUser.ErrorMessage = "There is another user with the same pin";
                    return newShopUser;
                }
                //Check user name is in user
                if (_shopBusinessEngine.CheckShopUserNameUnique(shopUserTransformed, shopId))
                {
                    newShopUser.ErrorMessage = "User name exists please pick another user name";
                    return newShopUser;
                }


                shopUserTransformed.Isactive = true;
                shopUserTransformed.Created = DateTimeUtil.GetCurrentTime();
                shopUserTransformed.Updated = DateTimeUtil.GetCurrentTime();
                shopUserTransformed.ShopId = shopId;

                //Process
                var shopUserResponse = _shopBusinessEngine.SaveShopUser(shopUserTransformed, shopId);
                if (shopUserResponse == null)
                    throw new ServiceException(new Exception("Cannot find user"));

                //Transform Response
                return _transformer.Transform<ShopUser, WsShopUser>(shopUserResponse);

            });
        }

        public WsShopUser GetShopUserById(int userId, int shopId)
        {
            return Query(() =>
            {
                Guard.GreaterThanZero(userId);
                var user = _shopBusinessEngine.GetShopUserById(userId, shopId);
                if (user == null)
                    return null;

                var userTransformed = _transformer.Transform<ShopUser, WsShopUser>(user);

                return userTransformed;

            });
        }

        public WsShopUser RemoveShopUseById(int userId, int shopId)
        {
            return Execute(() =>
            {
                Guard.GreaterThanZero(userId);
                var user = _shopBusinessEngine.RemoveShopUserById(userId, shopId);
                if (user == null)
                    return null;

                var userTransformed = _transformer.Transform<ShopUser, WsShopUser>(user);

                return userTransformed;

            });
        }
    }
}
