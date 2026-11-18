using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Core.Common.Core;
using Core.Common.Utility;
using Core.Common.Validation;
using log4net;
using Newtonsoft.Json;
using Takeaway.Admin.Util.CacheRepository;
using Takeaway.Admin.Util.Entities.Authentication;
using Takeaway.Admin.Util.Entities.Enum;
using Takeaway.Admin.Util.Entities.Settings;
using Takeaway.Admin.Util.Entities.Shops;
using Takeaway.Admin.Util.Repositories;
using Takeaway.Admin.Util.Transformers.In;
using Takeaway.Admin.Util.Transformers.Out;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Reports;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;
using Takeway.Mobile.Core.Utilities;

namespace Takeaway.Admin.Util.Services
{

    public interface IOfflineShopService
    {
        /*Shop Info*/
        WsShop GetShopInfo(int? shopid = Constants.ShopId);
        WsShop SyncShopInfo(WsShop shopInfo, int? shopid = Constants.ShopId);

        /*Menu Items */
        List<WsMenuItemCategory> GetAllMenuCategory(int? shopid = Constants.ShopId);
        List<WsMenuItem> GetAllMenuItems(int? shopid = Constants.ShopId);

        /*Orders */
        WsOrder SaveOrder(WsOrder orderItem, int? shopid = Constants.ShopId);
        WsOrder CancelOrderById(WsOrder order, int? shopid = Constants.ShopId);
        List<WsOrder> GetCompleteOrders(WsOrderSearch search, int? shopid = Constants.ShopId);

        WsOrderHolder HoldTempOrder(WsOrderHolder holdOrder, int? shopid = Constants.ShopId);
        List<WsOrderHolder> GetHoldOrders(int? shopid = Constants.ShopId);

        /* Settings lookups */
        List<WsSetting> GetSystemSettings(int? shopid = Constants.ShopId);
        List<WsTableCommand> GetTableCommand(int? shopid = Constants.ShopId);
        List<WsDriver> GetAllDrivers(int? shopid = Constants.ShopId);
        List<WsDeliveryOption> GetDeliveryOptions(int? shopid = Constants.ShopId);
        List<WsOptionItem> GetOptionItems(int? shopid = Constants.ShopId);
        List<WsDeliveryCost> GetDeliveryCosts(int? shopid = Constants.ShopId);
        List<WsPhoneRecord> GetLastphoneIn5(int? shopid = Constants.ShopId);
        string GetNextOrderReference(WsDeliveryOption selectedDeliveryOption, bool isWalkIn, int? shopid = Constants.ShopId);

        /* Payment */
        List<WsPaymentType> GetAllPaymentTypes(int? shopid = Constants.ShopId);

        /* Users */
        WsShopCustomer GetCustomerByPhoneNumber(string customerPhone, int? shopid = Constants.ShopId);
        List<WsShopUser> GetAllUsers(int? shopid = Constants.ShopId);

        /* Reports */
        WsReport GetReport(WsReport report, int? shopid = Constants.ShopId);

        /* Tables */
        List<WsTables> GetExistingTables(int tableSize, int? shopid = Constants.ShopId);
        WsTables ClearTable(int tableId, int? shopid = Constants.ShopId);
        void SaveTableSettings(WsTables table, int? shopid = Constants.ShopId);

        /* Sync  */
        List<WsMenuItemCategory> SyncMenuItemCategory(List<WsMenuItemCategory> menuItemCategories, int? shopid = Constants.ShopId);
        List<WsMenuItem> SyncMenuItems(List<WsMenuItem> menuItems, int? shopid = Constants.ShopId);
        List<WsDeliveryCost> SyncDeliveryCost(List<WsDeliveryCost> deliveryCost, int? shopid = Constants.ShopId);
        List<WsDeliveryOption> SyncDeliveryOptions(List<WsDeliveryOption> lists, int? shopid = Constants.ShopId);
        List<WsDriver> SyncDrivers(List<WsDriver> lists, int? shopid = Constants.ShopId);
        List<WsSetting> SyncSettings(List<WsSetting> lists, int? shopid = Constants.ShopId);
        List<WsOptionItem> SyncOptionItems(List<WsOptionItem> lists, int? shopid = Constants.ShopId);
        List<WsPaymentType> SyncPaymentTypes(List<WsPaymentType> lists, int? shopid = Constants.ShopId);
        List<WsShopUser> SyncShopUsers(List<WsShopUser> lists, int? shopid = Constants.ShopId);
        List<WsTableAlias> SyncTableAlias(List<WsTableAlias> lists, int? shopid = Constants.ShopId);
        List<WsTableCommand> SyncTableCommand(List<WsTableCommand> lists, int? shopid = Constants.ShopId);
        List<WsShopCustomer> SyncCustomers(List<WsShopCustomer> lists, int? shopid = Constants.ShopId);
        void SaveSettingByKey(string settingKey, string settingValue, int? shopid = Constants.ShopId);
        List<WsOfflineSetting> GetOfflineSettings(int? shopid = Constants.ShopId);
        List<Order> GetPendingOrders(int? shopid = Constants.ShopId);
        void UpdateUploadedOrder(Order order, int? shopid = Constants.ShopId);

        void CleanOrderMoreThan7Days(int? shopid = Constants.ShopId);
        void RemoveAllPhoneRecords(int? shopid = Constants.ShopId);
        void SaveCallerId(WsPhoneRecord wsPhoneRecord, int? shopid = Constants.ShopId);
        List<WsShopCustomer> SearchCustomer(string customerPhone, string customerName, string customerEmail, string customerAddress, int? shopid = Constants.ShopId);
        List<WsShopCustomer> GetAllCustomers(int? shopid = Constants.ShopId);

        WsPhoneRecord GetLast30SecondsCallerIdByChannel(string phoneNumber, string channel, int? shopid = Constants.ShopId);
        void CleanCallerIdsMoreThan7Days(int? shopid = Constants.ShopId);

        void SaveCustomer(string customerPhone, string customerAddress, string customerName, string customerEmail, decimal? customerDeliveryCharge, int? shopid = Constants.ShopId);
        void RemoveCustomer(string customerPhone, string customerAddress, int? shopid = Constants.ShopId);
    }


    public class OfflineShopService : IOfflineShopService
    {

        #region Constructor

        protected readonly IUnitOfWork UnitOfWork;
        protected readonly ILog Logger;
        private readonly IShopInfoRepository _shopInfoRepository;
        private readonly IShopUserRepository _shopUserRepository;
        private readonly IDeliveryOptionRepository _deliveryOptionRepository;
        private readonly IMenuItemCategoryRepository _menuItemCategoryRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IOptionItemRepository _optionItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryCostRepository _deliveryCostRepository;
        private readonly IServerSettingRepository _serverSettingRepository;
        private readonly ITableCommandRepository _tableCommandRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly ITableAliasRepository _tableAliasRepository;
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        private readonly IStoredProcedureRepository _storedProcedureRepository;
        private readonly IOrderTransformer _orderTransformer;
        private readonly ICustomerTransformer _customerTransformer;
        private readonly IOfflineServerSettingRepository _offlineServerSettingRepository;
        private readonly IWsOfflineSettingTransformer _wsOfflineSettingTransformer;
        private readonly IOrderSequenceRepository _orderSequenceRepository;
        private readonly IOrderHolderRepository _orderHolderRepository;
        private readonly ICallerIdRepository _callerIdRepository;
        private readonly IWsPhoneRecordTransformer _wsPhoneRecordTransformer;
        private readonly IWsShopCustomerTransformer _wsShopCustomerTransformer;
        private readonly ITablesRepository _tablesRepository;
        private readonly IWsShopInfoRepository _wsShopInfoRepository;
        private readonly IWsSettingRepository _wsSettingRepository;
        private readonly IWsOfflineSettingRepository _wsOfflineSettingRepository;
        private readonly IPhoneRecordTransformer _phoneRecordTransformer;
        private readonly IWsUserRepository _wsUserRepository;



        public OfflineShopService(IUnitOfWork unitOfWork,
                                  IShopInfoRepository shopInfoRepository,
                                  IDeliveryOptionRepository deliveryOptionRepository,
                                  IShopUserRepository shopUserRepository,
                                  IMenuItemRepository menuItemRepository,
                                  IOptionItemRepository optionItemRepository,
                                  IOrderRepository orderRepository,
                                  IOrderTransformer orderTransformer,
                                  ICustomerRepository customerRepository,
                                  IDeliveryCostRepository deliveryCostRepository,
                                  IServerSettingRepository serverSettingRepository,
                                  ITableCommandRepository tableCommandRepository,
                                  IDriverRepository driverRepository,
                                  ITableAliasRepository tableAliasRepository,
                                  IPaymentTypeRepository paymentTypeRepository,
                                  IStoredProcedureRepository storedProcedureRepository,
                                  IMenuItemCategoryRepository menuItemCategoryRepository,
                                  ICustomerTransformer customerTransformer,
                                  IOfflineServerSettingRepository offlineServerSettingRepository,
                                  IWsOfflineSettingTransformer wsOfflineSettingTransformer,
                                  IOrderSequenceRepository orderSequenceRepository,
                                  IOrderHolderRepository orderHolderRepository,
                                  ICallerIdRepository callerIdRepository,
                                  IWsPhoneRecordTransformer wsPhoneRecordTransformer,
                                  IWsShopCustomerTransformer wsShopCustomerTransformer,
                                  ITablesRepository tablesRepository, IWsShopInfoRepository wsShopInfoRepository, IWsSettingRepository wsSettingRepository, IWsOfflineSettingRepository wsOfflineSettingRepository, IPhoneRecordTransformer phoneRecordTransformer, IWsUserRepository wsUserRepository)
        {

            Logger = LogManager.GetLogger(this.GetType());
            UnitOfWork = unitOfWork;
            _shopInfoRepository = shopInfoRepository;
            _deliveryOptionRepository = deliveryOptionRepository;
            _shopUserRepository = shopUserRepository;
            _menuItemRepository = menuItemRepository;
            _optionItemRepository = optionItemRepository;
            _orderRepository = orderRepository;
            _orderTransformer = orderTransformer;
            _customerRepository = customerRepository;
            _deliveryCostRepository = deliveryCostRepository;
            _serverSettingRepository = serverSettingRepository;
            _tableCommandRepository = tableCommandRepository;
            _driverRepository = driverRepository;
            _tableAliasRepository = tableAliasRepository;
            _paymentTypeRepository = paymentTypeRepository;
            _storedProcedureRepository = storedProcedureRepository;
            _menuItemCategoryRepository = menuItemCategoryRepository;
            _customerTransformer = customerTransformer;
            _offlineServerSettingRepository = offlineServerSettingRepository;
            _wsOfflineSettingTransformer = wsOfflineSettingTransformer;
            _orderSequenceRepository = orderSequenceRepository;
            _orderHolderRepository = orderHolderRepository;
            _callerIdRepository = callerIdRepository;
            _wsPhoneRecordTransformer = wsPhoneRecordTransformer;
            _wsShopCustomerTransformer = wsShopCustomerTransformer;
            _tablesRepository = tablesRepository;
            _wsShopInfoRepository = wsShopInfoRepository;
            _wsSettingRepository = wsSettingRepository;
            _wsOfflineSettingRepository = wsOfflineSettingRepository;
            _phoneRecordTransformer = phoneRecordTransformer;
            _wsUserRepository = wsUserRepository;
        }
        #endregion

        #region Support Methods

        public TResponse Execute<TResponse>(Func<TResponse> action)
        {
            try
            {
                var result = action();
                UnitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("OfflineService:[Exception] {0}-{1}", ex.Message, ex.StackTrace);
                throw;
            }

        }

        public TResponse Query<TResponse>(Func<TResponse> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("OfflineService:[Exception] {0}-{1}", ex.Message, ex.StackTrace);
                throw;
            }

        }
        #endregion

        #region Methods

        public WsShop GetShopInfo(int? shopid = Constants.ShopId)
        {

            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
            {
                return GetShopInfoRedis();
            }


            return Query(() =>
            {
                var shopInfoDb = _shopInfoRepository.GetOne(r => r.ShopId == shopid.Value);
                var shopInfo = JsonConvert.DeserializeObject<WsShop>(shopInfoDb.Json);
                return shopInfo;
            });

        }

        private WsShop GetShopInfoRedis()
        {
            return _wsShopInfoRepository.GetShopInfo();
        }

        public List<WsDeliveryOption> GetDeliveryOptions(int? shopid = Constants.ShopId)
        {
            return Query(() =>
            {
                var deliveryOptions = _deliveryOptionRepository.GetAll();

                return deliveryOptions.Select(item => JsonConvert.DeserializeObject<WsDeliveryOption>(item.Json)).ToList();
            });
        }

        public List<WsMenuItemCategory> GetAllMenuCategory(int? shopid = Constants.ShopId)
        {
            return Query(() =>
            {
                var menuItemCategories = _menuItemCategoryRepository.GetMany(r => r.ShopId == shopid.Value);

                return menuItemCategories.Select(item => JsonConvert.DeserializeObject<WsMenuItemCategory>(item.Json)).ToList();
            });
        }

        public List<WsMenuItem> GetAllMenuItems(int? shopid = Constants.ShopId)
        {
            return Query(() =>
            {
                var deliveryOptions = _menuItemRepository.GetMany(r => r.ShopId == shopid.Value);

                return deliveryOptions.Select(item => JsonConvert.DeserializeObject<WsMenuItem>(item.Json)).ToList();
            });
        }

        public List<WsOptionItem> GetOptionItems(int? shopid = Constants.ShopId)
        {
            return Query(() =>
            {
                var optionItems = _optionItemRepository.GetMany(r => r.ShopId == shopid.Value);
                return optionItems.Select(item => JsonConvert.DeserializeObject<WsOptionItem>(item.Json)).ToList();
            });
        }

        public List<WsDeliveryCost> GetDeliveryCosts(int? shopid = Constants.ShopId)
        {
            return Query(() =>
            {
                var deliveryCosts = _deliveryCostRepository.GetMany(r => r.ShopId == shopid.Value);
                return deliveryCosts.Select(item => JsonConvert.DeserializeObject<WsDeliveryCost>(item.Json)).ToList();
            });
        }

        public List<WsTableCommand> GetTableCommand(int? shopid = Constants.ShopId)
        {
            return Query(() =>
            {
                //Perform
                var optionItems = _tableCommandRepository.GetMany(r => r.ShopId == shopid.Value);

                return optionItems.Select(item => JsonConvert.DeserializeObject<WsTableCommand>(item.Json)).ToList();

            });
        }

        public List<WsDriver> GetAllDrivers(int? shopid = Constants.ShopId)
        {
            return Query(() =>
            {
                //Perform
                var optionItems = _driverRepository.GetMany(r => r.ShopId == shopid.Value);
                return optionItems.Select(item => JsonConvert.DeserializeObject<WsDriver>(item.Json)).ToList();

            });
        }

        public List<WsPaymentType> GetAllPaymentTypes(int? shopid = Constants.ShopId)
        {
            return Query(() =>
            {
                //Perform
                var optionItems = _paymentTypeRepository.GetAll();
                return optionItems.Select(item => JsonConvert.DeserializeObject<WsPaymentType>(item.Json)).ToList();

            });
        }

        public List<WsShopUser> GetAllUsers(int? shopid = Constants.ShopId)
        {
            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
                return GetAllUsersRedis();

            return Query(() =>
            {
                var optionItems = _shopUserRepository.GetMany(r => r.ShopId == shopid.Value);
                return optionItems.Select(item => JsonConvert.DeserializeObject<WsShopUser>(item.Json)).ToList();
            });
        }

        private List<WsShopUser> GetAllUsersRedis()
        {
            return _wsUserRepository.GetAllUsers();
        }

        public List<WsSetting> GetSystemSettings(int? shopid = Constants.ShopId)
        {
            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
                return GetSystemSettingsRedis();


                return Query(() =>
            {

                var settings = _serverSettingRepository.GetMany(r => r.ShopId == shopid.Value);
                return settings.Select(item => JsonConvert.DeserializeObject<WsSetting>(item.Json)).ToList();

            });
        }

        private List<WsSetting> GetSystemSettingsRedis()
        {
            return _wsSettingRepository.GetAllSettings();
        }

        public List<WsTableAlias> GetTableAlias(int? shopid = Constants.ShopId)
        {
            return Query(() =>
            {
                //Perform
                var optionItems = _tableAliasRepository.GetMany(r => r.ShopId == shopid.Value);
                return optionItems.Select(item => JsonConvert.DeserializeObject<WsTableAlias>(item.Json)).ToList();

            });
        }

        public WsOrderHolder HoldTempOrder(WsOrderHolder holdOrder, int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {

                var order = new OrderHolder
                {
                    Created = DateTime.Now,
                    Json = JsonConvert.SerializeObject(holdOrder),
                    ShopId = shopid.Value
                };


                _orderHolderRepository.Add(order);

                return holdOrder;


            });
        }

        public List<WsOrderHolder> GetHoldOrders(int? shopid = Constants.ShopId)
        {
            return Query(() =>
            {
                //Perform
                var optionItems = _orderHolderRepository.GetMany(r => r.ShopId == shopid.Value).OrderByDescending(s => s.Id).Take(10);
                return optionItems.Select(item => JsonConvert.DeserializeObject<WsOrderHolder>(item.Json)).ToList();

            });
        }

        public string GetNextOrderReference(WsDeliveryOption selectedDeliveryOption, bool isWalkIn, int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {
                if (selectedDeliveryOption == null)
                    return string.Empty;

                switch (selectedDeliveryOption.Name)
                {
                    case DeliveryOptions.ForDelivery:
                        return ReturnDeliveryMethod("D", (int)WsDeliverySequenceEnum.Delivery);
                    case DeliveryOptions.ForCollection:
                        return isWalkIn ? ReturnDeliveryMethod("W", (int)WsDeliverySequenceEnum.Walkin) : ReturnDeliveryMethod("C", (int)WsDeliverySequenceEnum.Collection);
                    case DeliveryOptions.ForEatIn:
                        return ReturnDeliveryMethod("E", (int)WsDeliverySequenceEnum.EatIn);
                    default:
                        return string.Empty;
                }
            });
        }

        private string ReturnDeliveryMethod(string code, int deliverySequenceId, int? shopid = Constants.ShopId)
        {

            var currentDate = DateTime.Now;
            var yesterday = DateTime.Now.AddDays(-1);

            var searchDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);

            if (DateTime.Now.Hour <= 7)
                searchDate = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day);


            var deliverySequence = _orderSequenceRepository.GetOne(r => r.OrderType == deliverySequenceId && r.ShopId == shopid.Value && r.Created == searchDate);
            var deliverySeq = 1;
            if (deliverySequence != null)
            {
                deliverySeq = deliverySequence.OrderSequence + 1;
                deliverySequence.OrderSequence = deliverySeq;
            }
            else
            {
                var newEntry = new OrderReference()
                {
                    Created=searchDate,
                    OrderSequence = deliverySeq,
                    OrderType = deliverySequenceId,
                    ShopId = shopid.Value
                };
                _orderSequenceRepository.Add(newEntry);

            }

            UnitOfWork.Commit();

            return string.Format("{1}{0}", deliverySeq.ToString().PadLeft(4, '0'), code);
        }

        public List<WsOfflineSetting> GetOfflineSettings(int? shopid = Constants.ShopId)
        {
            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
                return GetOfflineSettingsRedis();

            return Query(() =>
            {
                var dbList = _offlineServerSettingRepository.GetMany(r => r.ShopId == shopid.Value);
                return _wsOfflineSettingTransformer.Transform<List<OfflineSetting>, List<WsOfflineSetting>>(dbList);

            });
        }

        private List<WsOfflineSetting> GetOfflineSettingsRedis()
        {
            return _wsOfflineSettingRepository.GetAllSettings();
        }

        public void SaveSettingByKey(string settingKey, string settingValue, int? shopid = Constants.ShopId)
        {
            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
            {
                SaveSettingByKeyRedis(settingKey, settingValue);
                return;
            }
                

                Execute(() =>
            {
                var settingItem = _offlineServerSettingRepository.GetOne(r => r.ShopId == shopid.Value && r.Name == settingKey);

                if (settingItem == null)
                {
                    var newSetting = new OfflineSetting()
                    {
                        Name = settingKey,
                        SettingValue = settingValue,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        ShopId = shopid.Value

                    };
                    _offlineServerSettingRepository.Add(newSetting);
                    return newSetting;

                }
                else
                {
                    settingItem.SettingValue = settingValue;
                    return settingItem;
                }

            });
        }

        private void SaveSettingByKeyRedis(string settingKey, string settingValue)
        {
            _wsOfflineSettingRepository.SaveSetting(settingKey, settingValue);
        }

        public List<WsPhoneRecord> GetLastphoneIn5(int? shopid = Constants.ShopId)
        {
            return Query(() =>
            {
                var dbList = _callerIdRepository.GetMany(r => r.ShopId == shopid.Value).OrderByDescending(s => s.Id).Take(10).ToList();
                return _wsPhoneRecordTransformer.Transform<List<CallerId>, List<WsPhoneRecord>>(dbList);

            });
        }

        public WsShopCustomer GetCustomerByPhoneNumber(string customerPhone, int? shopid = Constants.ShopId)
        {

            return Query(() =>
            {

                Guard.NotNull(customerPhone);

                var shopCustomer = _customerRepository.GetCustomerByPhone(customerPhone, shopid.Value);
                if (shopCustomer == null)
                    return new WsShopCustomer();

                var shopCustomerTransformed = _wsShopCustomerTransformer.Transform<Customer, WsShopCustomer>(shopCustomer);
                return shopCustomerTransformed;

            });
        }

        public List<WsTables> GetExistingTables(int tableSize, int? shopid = Constants.ShopId)
        {
            return Query(() =>
            {

                var dblist = _tablesRepository.GetMany(r => r.ShopId == shopid.Value && r.TableId <= tableSize);

                for (var i = 1; i <= tableSize; i++)
                {
                    if (dblist.All(r => r.TableId != i))
                    {
                        dblist.Add(new Tables() {ShopId =shopid.Value, TableId = i,Json = JsonConvert.SerializeObject(new WsTables() { Id=i, Alias = string.Format("Table {0}",i),ShopId = shopid.Value}) });
                    }
                }
                
               return dblist.Select(item => JsonConvert.DeserializeObject<WsTables>(item.Json)).ToList();

            });
        }

        public WsTables ClearTable(int tableId, int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {

                var table = _tablesRepository.GetOne(r => r.ShopId == shopid.Value && r.TableId == tableId);
                if(table!=null)
                    _tablesRepository.Remove(table);

                return new WsTables();
            });
        }

        public void SaveTableSettings(WsTables table, int? shopid = Constants.ShopId)
        {
            Execute(() =>
            {
                var tableEntry = _tablesRepository.GetOne(r => r.ShopId == shopid.Value && r.TableId == table.Id);

                if (tableEntry != null)
                {
                    tableEntry.Json = JsonConvert.SerializeObject(table);
                }
                else
                {
                    tableEntry = new Tables
                    {
                        Json = JsonConvert.SerializeObject(table),
                        Created = DateTime.Now,
                        ShopId = shopid.Value,
                        TableId = table.Id
                    };
                    _tablesRepository.Add(tableEntry);
                }

                return table;

            });
        }

        public WsReport GetReport(WsReport report, int? shopid = Constants.ShopId)
        {
            return Query(() =>
            {
                Guard.GreaterThanZero(report.ReportTypeId);


                if (!report.FromDate.HasValue)
                    report.FromDate = DateTime.Today;

                var fromDate = report.FromDate.Value.Date.AddHours(5);
                var toDate = fromDate.AddDays(1).AddHours(-1);




                switch (report.ReportTypeId)
                {
                    case (int)ReportType.Summary:

                        report.ReportResults = _storedProcedureRepository.SummaryReport(fromDate, toDate, shopid.Value);
                        break;

                    case (int)ReportType.TransactionReport:

                        report.ReportResults = _storedProcedureRepository.TransactionReport(fromDate, toDate, shopid.Value);
                        break;


                    case (int)ReportType.TransactionDrinkReport:

                        report.ReportResults = _storedProcedureRepository.TransactionDrinkReport(fromDate, toDate, shopid.Value);
                        break;

                    case (int)ReportType.OrderFoodReport:

                        report.ReportResults = _storedProcedureRepository.OrdeFoodReport(fromDate, toDate, shopid.Value);
                        break;


                    case (int)ReportType.DriverReport:

                        report.ReportResults = _storedProcedureRepository.DriverReport(fromDate, toDate, shopid.Value);
                        break;

                    case (int)ReportType.CustomerReport:

                        report.ReportResults = _storedProcedureRepository.CustomerReport(shopid.Value);
                        break;

                    case (int)ReportType.SummaryMulti:

                        toDate = report.ToDate.Value.AddDays(1).AddHours(-1);
                        report.ReportResults = _storedProcedureRepository.DriverReport(fromDate, toDate, shopid.Value);
                        break;

                    case (int)ReportType.TransactionReportMulti:
                        toDate = report.ToDate.Value.AddDays(1).AddHours(-1);
                        report.ReportResults = _storedProcedureRepository.SummaryReport(fromDate, toDate, shopid.Value);
                        break;

                    case (int)ReportType.BusyHourReport:

                        report.ReportResults = _storedProcedureRepository.BusyHourReport(fromDate, toDate, shopid.Value);
                        break;
                    case (int)ReportType.CustomerOrderReport:
                        report.ReportResults = _storedProcedureRepository.CustomerOrderReport(shopid.Value);
                        break;

                    default:
                        break;


                }


                return report;

            });
        }

        public List<WsOrder> GetCompleteOrders(WsOrderSearch search, int? shopid = Constants.ShopId)
        {

            return Query(() =>
            {
                if (!search.SearchDate.HasValue)
                    search.SearchDate = DateTime.Today;


                var fromDate = search.SearchDate.Value.Date.AddHours(5);
                var toDate = fromDate.AddDays(1).AddHours(-1);

                var orders = _orderRepository.GetActiveOrdersByDate(fromDate, toDate, shopid.Value);

                if (orders == null)
                    return new List<WsOrder>();

                return orders.Select(item => JsonConvert.DeserializeObject<WsOrder>(item.Json)).ToList();


            });
        }

        public void CleanCallerIdsMoreThan7Days(int? shopid = Constants.ShopId)
        {
            Execute(() =>
            {
                var searchDate = DateTime.Today.AddDays(-3);

                var ordersToRemove = _callerIdRepository.GetMany(r => r.ShopId == shopid.Value && r.Created < searchDate);

                foreach (var order in ordersToRemove)
                    _callerIdRepository.Remove(order);


                return ordersToRemove;

            });
        }

       


        public void CleanOrderMoreThan7Days(int? shopid = Constants.ShopId)
        {
            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
            {
                CleanOrderMoreThan7DaysRedis();
            }


            Execute(() =>
             {
                 var searchDate = DateTime.Today.AddDays(-10);

                 var ordersToRemove = _orderRepository.GetMany(r => r.ShopId == shopid.Value && r.Created < searchDate && r.IsUploaded.HasValue && r.IsUploaded.Value);

                 foreach (var order in ordersToRemove)
                     _orderRepository.Remove(order);


                 return ordersToRemove;

             });

        }

        private void CleanOrderMoreThan7DaysRedis()
        {
            throw new NotImplementedException();
        }

        public void RemoveAllPhoneRecords(int? shopid = Constants.ShopId)
        {
            Execute(() =>
            {
                var records = _callerIdRepository.GetMany(r => r.ShopId == shopid.Value);
                foreach(var record in records)
                    _callerIdRepository.Remove(record);

                return records;

            });
        }

        public void SaveCallerId(WsPhoneRecord wsPhoneRecord, int? shopid = Constants.ShopId)
        {
            Execute(() =>
            {
                Guard.NotNull(wsPhoneRecord);

                var callerIdEntry = _phoneRecordTransformer.Transform<WsPhoneRecord, CallerId>(wsPhoneRecord);
                callerIdEntry.ShopId = shopid.Value;
                _callerIdRepository.Add(callerIdEntry);
                return wsPhoneRecord;
  
            });
        }


       

        public WsPhoneRecord GetLast30SecondsCallerIdByChannel(string phoneNumber, string channel, int? shopid = Constants.ShopId)
        {
           return  Query(() =>
           {

               var timeToCompare = DateTime.Now.AddSeconds(-30);

                var entry =
                    _callerIdRepository.GetOne(
                        r =>
                            r.ShopId == shopid.Value && 
                            r.LineId == channel && 
                            r.Number == phoneNumber &&
                            r.Created >= timeToCompare);

                if (entry == null)
                    return null;

                var wsCallerIdEntry = _wsPhoneRecordTransformer.Transform<CallerId,WsPhoneRecord>(entry);
                return wsCallerIdEntry;

            });
        }




        public List<WsShopCustomer> SearchCustomer(string customerPhone, string customerName, string customerEmail, string customerAddress, int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {
                
                List<Customer> customers=new List<Customer>();
                if (!string.IsNullOrEmpty(customerPhone))
                    customers = _customerRepository.GetCustomersByPhone(customerPhone, shopid.Value);
                else if (!string.IsNullOrEmpty(customerName))
                    customers = _customerRepository.GetCustomersByName(customerName, shopid.Value);
                else if (!string.IsNullOrEmpty(customerEmail))
                    customers = _customerRepository.GetCustomersByEmail(customerEmail, shopid.Value);
                else if (!string.IsNullOrEmpty(customerAddress))
                    customers = _customerRepository.GetCustomersByAddress(customerAddress, shopid.Value);

              return _wsShopCustomerTransformer.Transform<List<Customer>, List<WsShopCustomer>>(customers);

            });
        }


        public List<WsShopCustomer> GetAllCustomers(int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {
                var customers= _customerRepository.GetAll(shopid.Value);
                return _wsShopCustomerTransformer.Transform<List<Customer>, List<WsShopCustomer>>(customers);

            });
        }


        public WsOrder SaveOrder(WsOrder orderItem, int? shopid = Constants.ShopId)
        {

            /*
             Assume following information is set by client 
             OrderItem.Guid(unique key)
             order.Created = DateTime.Now
             order.source
             
            */

            return Execute(() =>
            {

                Guard.NotNull(orderItem.Guid);

                var order = _orderRepository.GetByGuid(orderItem.Guid, shopid.Value);


                if (order == null)
                {
                    order = _orderTransformer.Transform<WsOrder, Order>(orderItem);
                    order.Json = JsonConvert.SerializeObject(orderItem);
                    order.IsModified = false;
                    order.IsUploaded = false;
                    order.ShopId = shopid.Value;
                    _orderRepository.Add(order);

                }
                else
                {
                    _orderRepository.Reload(order);
                    var newOrder = _orderTransformer.Transform<WsOrder, Order>(orderItem);
                    ObjectUtil.CopyPropertyValues(newOrder,order);
                    order.Json = JsonConvert.SerializeObject(orderItem);
                    order.IsModified = true;
                    order.IsUploaded = false;
                    order.Updated = DateTime.Now;
                    order.ShopId = shopid.Value;
                }
                
                UnitOfWork.Commit();

                orderItem.Id = order.Id;
                CreateOrUpdateCustomer(order, shopid.Value);
                return orderItem;

            });
        }

        private void CreateOrUpdateCustomer(Order requestOrder, int shopId)
        {
            if (string.IsNullOrEmpty(requestOrder.PhoneNumber))
                return;

            var customer = _customerRepository.CheckExistingCustomer(requestOrder.PhoneNumber, requestOrder.Address, shopId);
            if (customer == null)
            {
                var newCustomer = new Customer()
                {
                    FullName = requestOrder.ContactName,
                    Phone1 = requestOrder.PhoneNumber,
                    Email = requestOrder.Email,
                    Address1 = requestOrder.Address,
                    ShopId = shopId,
                    Created = requestOrder.Created
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
            }
        }


        public void SaveCustomer(string customerPhone, string customerAddress, string customerName, string customerEmail,
           decimal? customerDeliveryCharge, int? shopid = Constants.ShopId)
        {
            Execute(() =>
            {

                var customer = _customerRepository.CheckExistingCustomer(customerPhone, customerAddress, shopid.Value);
                if (customer == null)
                {
                    var newCustomer = new Customer()
                    {
                        FullName = customerName,
                        Phone1 = customerPhone,
                        Email = customerEmail,
                        Address1 = customerAddress,
                        ShopId = shopid.Value,
                        Created = DateTime.Now
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

                return customer;
            });
       }

        public void RemoveCustomer(string customerPhone, string customerAddress, int? shopid = Constants.ShopId)
        {
            Execute(() =>
            {

                var customer = _customerRepository.CheckExistingCustomer(customerPhone, customerAddress, shopid.Value);
                if (customer != null)
                {
                    _customerRepository.Remove(customer);
                }
             
                return customer;
            });
        }



        public WsOrder CancelOrderById(WsOrder order, int? shopid = Constants.ShopId)
        {
            return Execute(() =>
            {

                Guard.NotNull(order.Guid);
                var orderExists = _orderRepository.GetByGuid(order.Guid, shopid.Value);
                _orderRepository.Reload(orderExists);


                if (orderExists != null)
                {
                    orderExists.StatusId = (int)OrderStatusIdEnum.Cancelled;
                    orderExists.Updated = DateTime.Now;
                    orderExists.IsModified = true;
                    orderExists.IsUploaded = false;

                    order.StatusId = (int)OrderStatusIdEnum.Cancelled;
                    orderExists.Json = JsonConvert.SerializeObject(order);
                }

                return order;

            });
        }


        public List<Order> GetPendingOrders(int? shopid = Constants.ShopId)
        {

            return Query(() =>
            {
                var uploadTime = DateTime.Now.AddMinutes(-5);
                return _orderRepository.GetMany(r => r.ShopId == shopid.Value && (!r.IsUploaded.HasValue || !r.IsUploaded.Value)
                && r.Updated< uploadTime).Take(10).ToList();

            });
        }

        public void UpdateUploadedOrder(Order order, int? shopid = Constants.ShopId)
        {
            Execute(() =>
            {
                order.Updated = DateTime.Now;
                return order;
            });


        }


        #region Sync Items


        public WsShop SyncShopInfo(WsShop shopInfo, int? shopid = Constants.ShopId)
        {

            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
                return SyncShopInfoRedis(shopInfo);


            return Execute(() =>
            {

                var shopInfoExisting = _shopInfoRepository.GetOne(r => r.ShopId == shopid.Value);

                if (shopInfoExisting != null)
                    _shopInfoRepository.Remove(shopInfoExisting);

                var shopInfoNew = new ShopInfo
                {
                    ShopId = shopid.Value,
                    Json = JsonConvert.SerializeObject(shopInfo),
                    Created = DateTime.Now,
                    Updated = DateTime.Now
                };

                _shopInfoRepository.Add(shopInfoNew);

                return shopInfo;

            });
        }

        private WsShop SyncShopInfoRedis(WsShop shopInfo)
        {

            _wsShopInfoRepository.RemoveShopInfo();
            _wsShopInfoRepository.SaveShopInfo(shopInfo);
            return shopInfo;

        }

        public List<WsMenuItemCategory> SyncMenuItemCategory(List<WsMenuItemCategory> menuItemCategories, int? shopid = Constants.ShopId)
        {
            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
                return new List<WsMenuItemCategory>();

            return Execute(() =>
            {
                var categories = _menuItemCategoryRepository.GetMany(r => r.ShopId == shopid.Value);
                foreach (var cat in categories)
                {
                    _menuItemCategoryRepository.Remove(cat);
                }

                foreach (var cateNew in menuItemCategories)
                {
                    var newCategory = new MenuItemCategory()
                    {
                        ShopId = shopid.Value,
                        Json = JsonConvert.SerializeObject(cateNew),
                        Created = DateTime.Now,
                        Updated = DateTime.Now

                    };
                    _menuItemCategoryRepository.Add(newCategory);
                }

                return menuItemCategories;

            });
        }

        

        public List<WsDeliveryCost> SyncDeliveryCost(List<WsDeliveryCost> deliveryCost, int? shopid = Constants.ShopId)
        {

            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
                return new List<WsDeliveryCost>();

            return Execute(() =>
        {
            var dbList = _deliveryCostRepository.GetMany(r => r.ShopId == shopid.Value);

            foreach (var dbItem in dbList)
            {
                _deliveryCostRepository.Remove(dbItem);
            }

            foreach (var item in deliveryCost)
            {
                var newItem = new DeliveryCost()
                {
                    ShopId = shopid.Value,
                    Json = JsonConvert.SerializeObject(item),
                    Created = DateTime.Now,
                    Updated = DateTime.Now

                };
                _deliveryCostRepository.Add(newItem);
            }

            return deliveryCost;
        });
        }


        public List<WsDeliveryOption> SyncDeliveryOptions(List<WsDeliveryOption> lists, int? shopid = Constants.ShopId)
        {
            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
                return new List<WsDeliveryOption>();

            return Execute(() =>
        {
            var dbList = _deliveryOptionRepository.GetAll();

            foreach (var dbItem in dbList)
            {
                _deliveryOptionRepository.Remove(dbItem);
            }

            foreach (var item in lists)
            {
                var newItem = new DeliveryOption()
                {
                    Json = JsonConvert.SerializeObject(item),
                    Created = DateTime.Now,
                    Updated = DateTime.Now

                };
                _deliveryOptionRepository.Add(newItem);
            }

            return lists;
        });
        }

        public List<WsDriver> SyncDrivers(List<WsDriver> lists, int? shopid = Constants.ShopId)
        {
            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
                return new List<WsDriver>();


            return Execute(() =>
        {
            var dbList = _driverRepository.GetMany(r => r.ShopId == shopid.Value);

            foreach (var dbItem in dbList)
            {
                _driverRepository.Remove(dbItem);
            }

            foreach (var item in lists)
            {
                var newItem = new Driver()
                {
                    ShopId = shopid.Value,
                    Json = JsonConvert.SerializeObject(item),
                    Created = DateTime.Now,
                    Updated = DateTime.Now

                };
                _driverRepository.Add(newItem);
            }

            return lists;
        });
        }

        public List<WsSetting> SyncSettings(List<WsSetting> lists, int? shopid = Constants.ShopId)
        {
            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
                return SyncSettingsRedis(lists);


            return Execute(() =>
        {
            var dbList = _serverSettingRepository.GetMany(r => r.ShopId == shopid.Value);

            foreach (var dbItem in dbList)
            {
                _serverSettingRepository.Remove(dbItem);
            }

            foreach (var item in lists)
            {
                var newItem = new ServerSetting
                {
                    ShopId = shopid.Value,
                    Json = JsonConvert.SerializeObject(item),
                    Created = DateTime.Now,
                    Updated = DateTime.Now

                };
                _serverSettingRepository.Add(newItem);
            }

            return lists;
        });
        }

        private List<WsSetting> SyncSettingsRedis(List<WsSetting> lists)
        {
            _wsSettingRepository.RemoveAllSettings();
            _wsSettingRepository.SaveAllSettings(lists);
            return lists;
        }

        public List<WsOptionItem> SyncOptionItems(List<WsOptionItem> lists, int? shopid = Constants.ShopId)
        {

            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
                return new List<WsOptionItem>();

            return Execute(() =>
        {
            var dbList = _optionItemRepository.GetMany(r => r.ShopId == shopid.Value);

            foreach (var dbItem in dbList)
            {
                _optionItemRepository.Remove(dbItem);
            }

            foreach (var item in lists)
            {
                var newItem = new OptionItem()
                {
                    ShopId = shopid.Value,
                    Json = JsonConvert.SerializeObject(item),
                    Created = DateTime.Now,
                    Updated = DateTime.Now

                };
                _optionItemRepository.Add(newItem);
            }

            return lists;
        });
        }

        public List<WsPaymentType> SyncPaymentTypes(List<WsPaymentType> lists, int? shopid = Constants.ShopId)
        {
            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
                return new List<WsPaymentType>();

            return Execute(() =>
            {
                var dbList = _paymentTypeRepository.GetAll();

            foreach (var dbItem in dbList)
            {
                _paymentTypeRepository.Remove(dbItem);
            }

            foreach (var item in lists)
            {
                var newItem = new PaymentType()
                {
                    Json = JsonConvert.SerializeObject(item),
                    Created = DateTime.Now,
                    Updated = DateTime.Now

                };
                _paymentTypeRepository.Add(newItem);
            }

            return lists;
        });
        }

        public List<WsShopUser> SyncShopUsers(List<WsShopUser> lists, int? shopid = Constants.ShopId)
        {

            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
                return SyncShopUsersRedis(lists);


            return Execute(() =>
        {
            var dbList = _shopUserRepository.GetMany(r => r.ShopId == shopid.Value);

            foreach (var dbItem in dbList)
            {
                _shopUserRepository.Remove(dbItem);
            }

            foreach (var item in lists)
            {
                var newItem = new ShopUser()
                {
                    ShopId = shopid.Value,
                    Json = JsonConvert.SerializeObject(item),
                    Created = DateTime.Now,
                    Updated = DateTime.Now

                };
                _shopUserRepository.Add(newItem);
            }

            return lists;
        });
        }

        private List<WsShopUser> SyncShopUsersRedis(List<WsShopUser> lists)
        {
            _wsUserRepository.RemoveAll();
            _wsUserRepository.SaveAll(lists);
            return lists;
        }

        public List<WsTableAlias> SyncTableAlias(List<WsTableAlias> lists, int? shopid = Constants.ShopId)
        {

            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
                return new List<WsTableAlias>();


            return Execute(() =>
        {
            var dbList = _tableAliasRepository.GetMany(r => r.ShopId == shopid.Value);

            foreach (var dbItem in dbList)
            {
                _tableAliasRepository.Remove(dbItem);
            }

            foreach (var item in lists)
            {
                var newItem = new TableAlias()
                {
                    ShopId = shopid.Value,
                    Json = JsonConvert.SerializeObject(item),
                    Created = DateTime.Now,
                    Updated = DateTime.Now

                };
                _tableAliasRepository.Add(newItem);
            }

            return lists;
        });
        }

        public List<WsTableCommand> SyncTableCommand(List<WsTableCommand> lists, int? shopid = Constants.ShopId)
        {

            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
                return new List<WsTableCommand>();


            return Execute(() =>
        {
            var dbList = _tableCommandRepository.GetMany(r => r.ShopId == shopid.Value);

            foreach (var dbItem in dbList)
            {
                _tableCommandRepository.Remove(dbItem);
            }

            foreach (var item in lists)
            {
                var newItem = new TableCommand()
                {
                    ShopId = shopid.Value,
                    Json = JsonConvert.SerializeObject(item),
                    Created = DateTime.Now,
                    Updated = DateTime.Now

                };
                _tableCommandRepository.Add(newItem);
            }

            return lists;
        });
        }

        public List<WsShopCustomer> SyncCustomers(List<WsShopCustomer> lists, int? shopid = Constants.ShopId)
        {//TODO: handel Redis
            return Execute(() =>
            {
                var dbList = _customerRepository.GetMany(r => r.ShopId == shopid.Value);

                foreach (var dbItem in dbList)
                {
                    _customerRepository.Remove(dbItem);
                }

                UnitOfWork.Commit();
                foreach (var item in lists)
                {
                    var customer = _customerTransformer.Transform<WsShopCustomer, Customer>(item);
                    _customerRepository.Add(customer);
                }

                return lists;
            });
        }



        public List<WsMenuItem> SyncMenuItems(List<WsMenuItem> menuItems, int? shopid = Constants.ShopId)
        {
            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
                return new List<WsMenuItem>();



            return Execute(() =>
            {
                var menuItemDb = _menuItemRepository.GetMany(r => r.ShopId == shopid.Value);

                foreach (var menuItem in menuItemDb)
                {
                    _menuItemRepository.Remove(menuItem);
                }

                foreach (var menuItemNew in menuItems)
                {
                    var newMenuItem = new MenuItem()
                    {
                        ShopId = shopid.Value,
                        Json = JsonConvert.SerializeObject(menuItemNew),
                        Created = DateTime.Now,
                        Updated = DateTime.Now

                    };
                    _menuItemRepository.Add(newMenuItem);
                }

                return menuItems;
            });
        }



        #endregion











        #endregion
    }
}