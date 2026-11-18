using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Core.Common.Resources;
using Core.Common.Utility;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Features;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Print;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;
using ILog = log4net.ILog;
using LogManager = log4net.LogManager;

namespace Takeaway.Admin.Wpf.ViewModels
{

    public abstract class BasePosViewModel : BaseViewModel
    {
        protected static readonly ILog Logger = LogManager.GetLogger("PosViewModel");

        #region Properties

        #region IndaSoft Info


        private bool _notSearch;
        public bool NotSearch
        {
            get { return _notSearch; }
            set
            {
                _notSearch = value;
                NotifyOfPropertyChange(() => Email);

            }
        }



        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);

            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                NotifyOfPropertyChange(() => PhoneNumber);

            }
        }

        #endregion

        #region Customer Infor

        private string _customerName;
        public string CustomerName
        {
            get
            {
                return this._customerName;
            }
            set
            {
                if (value != this._customerName)
                {
                    this._customerName = value;
                    MyOrder.ContactName = _customerName;
                    NotifyOfPropertyChange(() => CustomerName);
                }
            }
        }

        private string _customerEmail;
        public string CustomerEmail
        {
            get
            {
                return this._customerEmail;
            }
            set
            {
                if (value != this._customerEmail)
                {
                    this._customerEmail = value;
                    MyOrder.Email = _customerEmail;
                    NotifyOfPropertyChange(() => CustomerEmail);
                }
            }
        }

        private string _customerPhone;
        public string CustomerPhone
        {
            get
            {
                return this._customerPhone;
            }
            set
            {
                if (value != this._customerPhone)
                {
                    this._customerPhone = value;
                    if (_customerPhone != MyOrder.PhoneNumber)
                    {
                        MyOrder.PhoneNumber = _customerPhone;
                        if (!string.IsNullOrEmpty(_customerPhone) && _customerPhone.Length > 6)
                            DoSearchByPhone();
                    }
                    NotifyOfPropertyChange(() => CustomerPhone);
                }
            }
        }

        private string _customerAddress;
        public string CustomerAddress
        {
            get
            {
                return this._customerAddress;
            }
            set
            {
                if (value != this._customerAddress)
                {
                    this._customerAddress = value;
                    MyOrder.Address = _customerAddress;
                    NotifyOfPropertyChange(() => CustomerAddress);
                }
            }
        }

        private decimal? _customerDeliveryCharge;
        public decimal? CustomerDeliveryCharge
        {
            get
            {
                return this._customerDeliveryCharge;
            }
            set
            {
                if (value != this._customerDeliveryCharge)
                {
                    this._customerDeliveryCharge = value;
                    NotifyOfPropertyChange(() => CustomerDeliveryCharge);
                }
            }
        }

        private int _numberOfPeople;
        public int NumberOfPeople
        {
            get
            {
                return this._numberOfPeople;
            }
            set
            {
                if (value != this._numberOfPeople)
                {
                    this._numberOfPeople = value;
                    NotifyOfPropertyChange(() => NumberOfPeople);
                }
            }
        }


        #endregion

        #region Lookups

        private ObservableCollection<WsTableCommand> _tableCommandList;
        public ObservableCollection<WsTableCommand> TableCommandList
        {
            get
            {
                return this._tableCommandList;
            }
            set
            {
                if (value != this._tableCommandList)
                {
                    this._tableCommandList = value;
                    NotifyOfPropertyChange(() => TableCommandList);
                }
            }
        }

        private ObservableCollection<WsPaymentType> _paymentTypes;
        public ObservableCollection<WsPaymentType> PaymentTypes
        {
            get
            {
                return this._paymentTypes;
            }
            set
            {
                if (value != this._paymentTypes)
                {
                    this._paymentTypes = value;
                    NotifyOfPropertyChange(() => PaymentTypes);
                }
            }
        }

        #endregion



        // OrderModes.EatIn.ToString(); setup a mode to difficiate eat in and takeaway

        private int _batchId;
        public int BatchId
        {
            get
            {
                return this._batchId;
            }
            set
            {
                if (value != this._batchId)
                {
                    this._batchId = value;
                    NotifyOfPropertyChange(() => BatchId);
                }
            }
        }

        private int _tableId;
        public int TableId
        {
            get
            {
                return this._tableId;
            }
            set
            {
                if (value != this._tableId)
                {
                    this._tableId = value;
                    NotifyOfPropertyChange(() => TableId);
                }
            }
        }

        private bool? _controlFlag;
        public bool? ControlFlag
        {
            get
            {
                return this._controlFlag;
            }
            set
            {
                if (value != this._controlFlag)
                {
                    this._controlFlag = value;
                    NotifyOfPropertyChange(() => ControlFlag);
                }
            }
        }

        private WsOrder _myOrder;
        public WsOrder MyOrder
        {
            get { return _myOrder; }
            set
            {
                if (value == this._myOrder) return;
                this._myOrder = value;
                NotifyOfPropertyChange(() => MyOrder);
            }
        }

        private string _selectedDeliveryOptionDisplay;
        public string SelectedDeliveryOptionDisplay
        {
            get { return _selectedDeliveryOptionDisplay; }
            set
            {
                if (value == this._selectedDeliveryOptionDisplay)
                    return;
                _selectedDeliveryOptionDisplay = value;
                CalculateTotal();
                NotifyOfPropertyChange(() => SelectedDeliveryOptionDisplay);

            }
        }


        private bool _eatInVisible;
        public bool EatinVisible
        {
            get
            {
                return this._eatInVisible;
            }
            set
            {
                if (value != this._eatInVisible)
                {
                    this._eatInVisible = value;
                    NotifyOfPropertyChange(() => EatinVisible);
                }
            }
        }

        private bool _takeawayVisible;
        public bool TakeawayVisible
        {
            get
            {
                return this._takeawayVisible;
            }
            set
            {
                if (value != this._takeawayVisible)
                {
                    this._takeawayVisible = value;
                    NotifyOfPropertyChange(() => TakeawayVisible);
                }
            }
        }



        private decimal _orderTotal;
        public decimal OrderTotal
        {
            get
            {
                return this._orderTotal;
            }
            set
            {
                if (value != this._orderTotal)
                {
                    this._orderTotal = value;
                    NotifyOfPropertyChange(() => OrderTotal);
                }
            }
        }

        private decimal _deliveryCharge;
        public decimal DeliveryCharge
        {
            get
            {
                return this._deliveryCharge;
            }
            set
            {
                if (value != this._deliveryCharge)
                {
                    this._deliveryCharge = value;
                    CalculateTotal();
                    NotifyOfPropertyChange(() => DeliveryCharge);

                }
            }
        }

        private decimal _discountAmount;
        public decimal DiscountAmount
        {
            get
            {
                return this._discountAmount;
            }
            set
            {
                if (value != this._discountAmount)
                {
                    this._discountAmount = value;
                    NotifyOfPropertyChange(() => DiscountAmount);
                }
            }
        }


        #region Support Lists

        private ObservableCollection<WsTableCommand> _selectedTableCommandsList;
        public ObservableCollection<WsTableCommand> SelectedTableCommandsList
        {
            get
            {
                return this._selectedTableCommandsList;
            }
            set
            {
                if (value != this._selectedTableCommandsList)
                {
                    this._selectedTableCommandsList = value;
                    NotifyOfPropertyChange(() => SelectedTableCommandsList);
                }
            }
        }

        private List<string> _addressList;
        public List<string> AddressList
        {
            get
            {
                return this._addressList;
            }
            set
            {
                if (value != this._addressList)
                {
                    this._addressList = value;
                    NotifyOfPropertyChange(() => AddressList);
                }
            }
        }

        private ObservableCollection<WsMenuItem> _orderItems;
        public ObservableCollection<WsMenuItem> OrderItems
        {
            get
            {
                return this._orderItems;
            }
            set
            {
                if (value != this._orderItems)
                {
                    this._orderItems = value;
                    MyOrder.OrderItems = _orderItems.ToList();
                    SetupOrdePrintItems();
                    NotifyOfPropertyChange(() => OrderItems);
                }
            }
        }

        private List<string> _deliveryOptionDisplay;
        public List<string> DeliveryOptionDisplay
        {
            get { return _deliveryOptionDisplay; }
            set
            {
                _deliveryOptionDisplay = value;
                NotifyOfPropertyChange(() => DeliveryOptionDisplay);

            }
        }

        private ObservableCollection<WsMenuItemCategory> _categories;
        public ObservableCollection<WsMenuItemCategory> Categories
        {
            get
            {
                return this._categories;
            }
            set
            {
                if (value != this._categories)
                {
                    this._categories = value;
                    NotifyOfPropertyChange(() => Categories);
                }
            }
        }

        private ObservableCollection<WsMenuItemCategory> _categoriesB;
        public ObservableCollection<WsMenuItemCategory> CategoriesB
        {
            get
            {
                return this._categoriesB;
            }
            set
            {
                if (value != this._categoriesB)
                {
                    this._categoriesB = value;
                    NotifyOfPropertyChange(() => CategoriesB);
                }
            }
        }

        private ObservableCollection<WsMenuItem> _categoryItems;
        public ObservableCollection<WsMenuItem> CategoryItems
        {
            get
            {
                return this._categoryItems;
            }
            set
            {
                if (value != this._categoryItems)
                {
                    this._categoryItems = value;
                    NotifyOfPropertyChange(() => CategoryItems);
                }
            }
        }

        private ObservableCollection<WsMenuItem> _hotItems;
        public ObservableCollection<WsMenuItem> HotItems
        {
            get
            {
                return this._hotItems;
            }
            set
            {
                if (value != this._hotItems)
                {
                    this._hotItems = value;
                    NotifyOfPropertyChange(() => HotItems);
                }
            }
        }

        private WsMenuItem _selectedOrderItem;
        public WsMenuItem SelectedOrderItem
        {
            get
            {
                return this._selectedOrderItem;
            }
            set
            {
                if (value != this._selectedOrderItem)
                {
                    this._selectedOrderItem = value;
                    NotifyOfPropertyChange(() => SelectedOrderItem);
                }
            }
        }

        private ObservableCollection<WsOptionItem> _optionItems;
        public ObservableCollection<WsOptionItem> OptionItems
        {
            get { return _optionItems; }
            set
            {
                if (value != this._optionItems)
                {
                    this._optionItems = value;

                    NotifyOfPropertyChange(() => OptionItems);
                }
            }
        }

        private ObservableCollection<WsOptionItem> _optionItems1;
        public ObservableCollection<WsOptionItem> OptionItems1
        {
            get { return _optionItems1; }
            set
            {
                if (value != this._optionItems1)
                {
                    this._optionItems1 = value;

                    NotifyOfPropertyChange(() => OptionItems1);
                }
            }
        }

        private ObservableCollection<WsOptionItem> _optionItems2;
        public ObservableCollection<WsOptionItem> OptionItems2
        {
            get { return _optionItems2; }
            set
            {
                if (value != this._optionItems2)
                {
                    this._optionItems2 = value;

                    NotifyOfPropertyChange(() => OptionItems2);
                }
            }
        }

        private ObservableCollection<WsOptionItem> _optionItems3;
        public ObservableCollection<WsOptionItem> OptionItems3
        {
            get { return _optionItems3; }
            set
            {
                if (value != this._optionItems3)
                {
                    this._optionItems3 = value;

                    NotifyOfPropertyChange(() => OptionItems3);
                }
            }
        }

        private ObservableCollection<WsOptionItem> _optionItems4;
        public ObservableCollection<WsOptionItem> OptionItems4
        {
            get { return _optionItems4; }
            set
            {
                if (value != this._optionItems4)
                {
                    this._optionItems4 = value;

                    NotifyOfPropertyChange(() => OptionItems4);
                }
            }
        }

        private ObservableCollection<WsPrintRawData> _orderPrintItems;
        public ObservableCollection<WsPrintRawData> OrderPrintItems
        {
            get
            {
                return this._orderPrintItems;
            }
            set
            {
                if (value != this._orderPrintItems)
                {
                    this._orderPrintItems = value;
                    NotifyOfPropertyChange(() => OrderPrintItems);
                }
            }
        }

        private ObservableCollection<WsDeliveryOption> _deliveryOptions;
        public ObservableCollection<WsDeliveryOption> DeliveryOptions
        {
            get
            {
                return this._deliveryOptions;
            }
            set
            {
                if (value != this._deliveryOptions)
                {
                    this._deliveryOptions = value;
                    NotifyOfPropertyChange(() => DeliveryOptions);
                }
            }
        }

        #endregion

        #endregion

        #region Contructors

        protected IOfflineShopService _service;
        protected IAppScope _appScope;
        protected IWindowManager _windowManager;
        protected IEventAggregator _eventAggregator;
        protected IAddressLookupAutoCompleteFeatureToggle _addressLookupAutoCompleteFeatureToggle;


        #endregion

        #region Methods
        public abstract void DoCompleteOrder();

        protected string GetDeliveryOptionDisplay()
        {

            if (MyOrder.DeliveryType == null)
                return Service.Contract.Enum.DeliveryOptions.DisplayWalkIn;

            if (MyOrder.DeliveryType.Name == Service.Contract.Enum.DeliveryOptions.ForEatIn)
            {
                return Service.Contract.Enum.DeliveryOptions.ForEatIn;
            }

            if (MyOrder.DeliveryType.Name == Service.Contract.Enum.DeliveryOptions.ForDelivery)
                return Service.Contract.Enum.DeliveryOptions.DisplayDelivery;
            if (MyOrder.IsWalkIn.HasValue && MyOrder.IsWalkIn.Value)
                return Service.Contract.Enum.DeliveryOptions.DisplayWalkIn;

            return Service.Contract.Enum.DeliveryOptions.DisplayCollection;

        }

        public void CollectOrderInformation()
        {
            CalculateTotal();
            MyOrder.IsWalkIn = IsDeliveryWalkIn();
            MyOrder.DeliveryType = CalculateDeliveryType();

            if (TableId <= 0) return;
            MyOrder.NumberOfPeople = NumberOfPeople;
            MyOrder.TableNumber = TableId;
        }




        private void SetupOrdePrintItems()
        {

            OrderPrintItems = new ObservableCollection<WsPrintRawData>(OrderPrintBuilder.SetupOrdePrintItems(OrderItems.ToList()));
        }

        protected void InitCategory()
        {

            Categories = new ObservableCollection<WsMenuItemCategory>();
            var items = new List<WsMenuItemCategory>();
            if (TableId > 0)
            {
                items =
                    _appScope.FullCategories.Where(
                        r => r.CategoryGroupId == (int)MenuItemCategoryGroupEnum.InternalStandard).ToList();
            }
            else
            {
                items = _appScope.FullCategories.Where(
                        r => r.CategoryGroupId == (int)MenuItemCategoryGroupEnum.External).ToList();


            }

            Categories = new ObservableCollection<WsMenuItemCategory>(Translate(items, GetTranslatedFlag(_appScope.Settings)));



            CategoriesB = new ObservableCollection<WsMenuItemCategory>();
            var itemsB = new List<WsMenuItemCategory>();
            if (TableId > 0)
            {
                itemsB = _appScope.FullCategories.Where(
                        r => r.CategoryGroupId == (int)MenuItemCategoryGroupEnum.InternalLunch).ToList();
            }
            else
            {
                itemsB = _appScope.FullCategories.Where(
                        r => r.CategoryGroupId == (int)MenuItemCategoryGroupEnum.ExternalOthers).ToList();

            }


            CategoriesB = new ObservableCollection<WsMenuItemCategory>(Translate(itemsB, GetTranslatedFlag(_appScope.Settings)));

            HotItems = new ObservableCollection<WsMenuItem>();
            var hItems = TableId > 0 ?
                _appScope.FullHotItemsInternal :
                _appScope.FullHotItemsExternal;
            HotItems = new ObservableCollection<WsMenuItem>(Translate(hItems, GetTranslatedFlag(_appScope.Settings)));
        }

        protected void InitDeliveryOptions()
        {

            DeliveryOptionDisplay = new List<string>()
            {
                Service.Contract.Enum.DeliveryOptions.DisplayDelivery,
                Service.Contract.Enum.DeliveryOptions.DisplayCollection,
                Service.Contract.Enum.DeliveryOptions.DisplayWalkIn,
            };


            if (TableId > 0)
            {
                DeliveryOptions = new ObservableCollection<WsDeliveryOption>(_appScope.FullDeliveryOptions.Where(r => r.IsInternal.HasValue && r.IsInternal.Value));
                SelectedDeliveryOptionDisplay = Service.Contract.Enum.DeliveryOptions.ForEatIn;


                EatinVisible = true;
                TakeawayVisible = false;

                //If not delivery then 0 
                DeliveryCharge = 0;

            }
            else
            {
                DeliveryOptions = new ObservableCollection<WsDeliveryOption>(_appScope.FullDeliveryOptions.Where(r => !r.IsInternal.HasValue));
                SelectedDeliveryOptionDisplay = Service.Contract.Enum.DeliveryOptions.DisplayWalkIn;

                EatinVisible = false;
                TakeawayVisible = true;
                DeliveryCharge = _appScope.ShopInfo.DeliveryCharge.Value;
            }
        }


        protected void InitOptionItems()
        {
            Translate(_appScope.FulllOptionItems, GetTranslatedFlag(_appScope.Settings));
            OptionItems = new ObservableCollection<WsOptionItem>(_appScope.FulllOptionItems);
            OptionItems1 = new ObservableCollection<WsOptionItem>(OptionItems.Where(r => r.GroupId == 1).ToList());
            OptionItems2 = new ObservableCollection<WsOptionItem>(OptionItems.Where(r => r.GroupId == 2).ToList());
            OptionItems3 = new ObservableCollection<WsOptionItem>(OptionItems.Where(r => r.GroupId == 3).ToList());
            OptionItems4 = new ObservableCollection<WsOptionItem>(OptionItems.Where(r => r.GroupId == 4).ToList());
        }

        protected void LoadTableCommandList()
        {

            Translate(_appScope.TableCommandList, GetTranslatedFlag(_appScope.Settings));
            TableCommandList = new ObservableCollection<WsTableCommand>(_appScope.TableCommandList);
        }

        protected void GetPaymentTypes()
        {
            PaymentTypes = new ObservableCollection<WsPaymentType>(_appScope.PaymentTypes);
        }


        public void SelectCategory(int categoryId)
        {

            CategoryItems = new ObservableCollection<WsMenuItem>(Translate(
                _appScope.FullCategoryItems.Where(r => r.MenuItemCategoryId == categoryId).ToList()
              , GetTranslatedFlag(_appScope.Settings)));

        }

        public void SelectCategoryItem(int itemId)
        {
            var selectItem = CategoryItems.FirstOrDefault(r => r.Id == itemId);
            AddToBasketOrOpenOptionExtra(selectItem);
        }

        public void DoTableCommand()
        {
            var viewModel = IoC.Get<ITableCommandViewModel>();
            viewModel.TableCommandList = SerializeHelper.CloneJson(TableCommandList);
            viewModel.SelectedTableCommandsList = SerializeHelper.CloneJson(SelectedTableCommandsList);

            OpenWindow(_windowManager, viewModel, "Table Command", 700, 500);

            if (viewModel.SelectionComplete)
                SelectedTableCommandsList = viewModel.SelectedTableCommandsList;


        }

        public void AddToBasketOrOpenOptionExtra(WsMenuItem selectItem)
        {
            var obj = SerializeHelper.CloneJson(selectItem);
            obj.Guid = Guid.NewGuid().ToString();
            obj.BatchId = BatchId;
            MyOrder.OrderItems.Add(obj);
            var selection = MyOrder.OrderItems.FirstOrDefault(r => r.Guid == obj.Guid);
            SelectedOrderItem = selection;

            CalculateTotal();

            if (GetAutoPopupExtra(_appScope.Settings))
            {
                //Only if there are option extra and NoSelectionIsNotOn Then Auto Do Comment
                if ((obj.HasSubOrder.HasValue && obj.HasSubOrder.Value) && (obj.NoSelection.HasValue && obj.NoSelection.Value || !obj.NoSelection.HasValue))
                    DoComment();
            }


        }




        public void DoComment()
        {
            if (SelectedOrderItem == null)
                return;

            var optionExtraViewModel = IoC.Get<IOptionExtraViewModel>();

            //Show Extra box on the bottom
            optionExtraViewModel.ExtraVisible = true;
            if (SelectedOrderItem.SubItemSelections.Exists(r => r.OptionItemGroupId.HasValue))
                optionExtraViewModel.ExtraVisible = false;
            
            optionExtraViewModel.SelectedItem = SerializeHelper.CloneJson(SelectedOrderItem);
            optionExtraViewModel.OptionItems = OptionItems;
            optionExtraViewModel.OptionItems1 = OptionItems1;
            optionExtraViewModel.OptionItems2 = OptionItems2;
            optionExtraViewModel.OptionItems3 = OptionItems3;
            optionExtraViewModel.OptionItems4 = OptionItems4;

            if (!string.IsNullOrEmpty(SelectedOrderItem.Comment))
                optionExtraViewModel.Comment = SelectedOrderItem.Comment;


            OpenWindow(_windowManager, optionExtraViewModel, StringResource.OptionExtra_Title, 1000, 700);

            MyOrder.OrderItems.Remove(SelectedOrderItem);
            MyOrder.OrderItems.Add(optionExtraViewModel.SelectedItem);
            SelectedOrderItem = optionExtraViewModel.SelectedItem;
            CalculateTotal();


        }

        public void DoAddressSuggestion()
        {
            if (!_addressLookupAutoCompleteFeatureToggle.FeatureEnabled)
                return;

            if (string.IsNullOrEmpty(CustomerAddress) || CustomerAddress.Length <= 6)
                return;

            AddressList = GoogleAddress.SuggestAddress(CustomerAddress);

        }

        public void CalculateDeliveryCharge()
        {
            if (string.IsNullOrEmpty(SelectedDeliveryOptionDisplay))
                return;

            if (SelectedDeliveryOptionDisplay == Service.Contract.Enum.DeliveryOptions.DisplayCollection
                || SelectedDeliveryOptionDisplay == Service.Contract.Enum.DeliveryOptions.DisplayWalkIn
                || SelectedDeliveryOptionDisplay == Service.Contract.Enum.DeliveryOptions.ForEatIn)
                DeliveryCharge = 0;
            else if (SelectedDeliveryOptionDisplay == Service.Contract.Enum.DeliveryOptions.DisplayDelivery && DeliveryCharge == 0)
            {
                if (CustomerDeliveryCharge.HasValue && CustomerDeliveryCharge.Value > 0)
                    DeliveryCharge = CustomerDeliveryCharge.Value;
                else
                    DeliveryCharge = _appScope.ShopInfo.DeliveryCharge.Value;
            }
        }

        public void RemoveSelectedOrderItem()
        {
            if (SelectedOrderItem == null)
                return;

            MyOrder.OrderItems.Remove(SelectedOrderItem);
            OrderItems = new ObservableCollection<WsMenuItem>(MyOrder.OrderItems);

            CalculateTotal();

        }

        public void DoRemoveSelected()
        {
            RemoveSelectedOrderItem();
        }

        public virtual void DoSplitTable()
        {
            if (TableId == 0)
                return;

            CalculateTotal();


            var table = _appScope.AvailableTables.FirstOrDefault(r => r.Id == TableId);
            table.NumberOfPeople = NumberOfPeople;
            table.Order = MyOrder;
            table.Started = DateTime.Now;

            //SaveOrder
            _service.SaveTableSettings(table);

            var viewModel = IoC.Get<ISplitTableViewModel>();
            viewModel.CurrentTable = table;
            OpenWindow(_windowManager, viewModel, "Split Table", 700, 500);

        }

        public void DoSearchByPhone()
        {

            if (NotSearch)
            {
                NotSearch = false;
                return;
            }
                
            Async(() =>
            {

                var customer = _service.GetCustomerByPhoneNumber(CustomerPhone);
                if (customer != null)
                {
                    CustomerName = customer.ContactName;
                    CustomerEmail = customer.Email;
                    CustomerAddress = customer.Address;
                    if (customer.DeliveryCharge > 0)
                    {
                        CustomerDeliveryCharge = customer.DeliveryCharge.Value;
                        DeliveryCharge = customer.DeliveryCharge.Value;
                    }

                }
            });

        }


        public virtual void DoMergeTable()
        {
            if (TableId == 0)
                return;

            CalculateTotal();


            var table = _appScope.AvailableTables.FirstOrDefault(r => r.Id == TableId);

            table.NumberOfPeople = NumberOfPeople;
            table.Order = MyOrder;
            table.Started = DateTime.Now;

            //SaveOrder
            _service.SaveTableSettings(table);

            var viewModel = IoC.Get<IMergeTableViewModel>();
            viewModel.CurrentTable = table;
            OpenWindow(_windowManager, viewModel, "Merge Table", 700, 500);
            if (viewModel.IsCompleted)
                ResetOrder();


        }

        protected void ResetOrder()
        {
            if (TableId > 0)
            {
                var table = _appScope.AvailableTables.FirstOrDefault(r => r.Id == TableId);
                table.Order = null;
                table.Started = null;
                _eventAggregator.Publish(IoC.Get<IEatInTableViewModel>(), action => Task.Factory.StartNew(action));
            }
            else
            {
                StartNewPos();
            }
        }

        public void DoOpenCashDrawer()
        {
            //if (!VerifyMoreThanUserRights(_windowManager))
            //    return;

            var printSetting = GetPrinterSettings(_appScope.Settings);
            var printerSettingFirst = printSetting.FirstOrDefault();
            var printerName = printerSettingFirst.PrinterName;
            var printCashDrawerCommand = GetSettingByKey(WsOfflineSettingEnum.AppPrinterCashTillCommand, _appScope.Settings).SettingValue; ;

            if (string.IsNullOrEmpty(printCashDrawerCommand))
                return;

            var commandByte = printCashDrawerCommand.Split(',');
            var sendinByte = new byte[commandByte.Length];
            for (int i = 0; i < commandByte.Length; i++)
            {
                var result = IntegerStringUtil.ConvertStringValue(commandByte[i]);
                sendinByte[i] = (byte)result;
            }

            var commandString = ASCIIEncoding.ASCII.GetString(sendinByte);

            RawPrinterHelper.SendStringToPrinter(printerName, commandString);

        }

        public void DoSpecial()
        {
            if (SelectedOrderItem == null)
                return;

            var optionExtraViewModel = IoC.Get<ISpecialViewModel>();

            optionExtraViewModel.SelectedItem = SerializeHelper.CloneJson(SelectedOrderItem);
            if (SelectedOrderItem.Price.HasValue)
            {
                optionExtraViewModel.ExtraCharge = SelectedOrderItem.Price.Value;
                optionExtraViewModel.OrgPrice = SelectedOrderItem.Price.Value;
            }


            OpenWindow(_windowManager, optionExtraViewModel, "Special Modification", 500, 400);

            MyOrder.OrderItems.Remove(SelectedOrderItem);
            MyOrder.OrderItems.Add(optionExtraViewModel.SelectedItem);
            SelectedOrderItem = optionExtraViewModel.SelectedItem;
            CalculateTotal();
        }


        public void DoDeliveryAdd()
        {
            DeliveryCharge += (decimal)0.5;
        }

        public void DoDeliveryReduce()
        {
            if (DeliveryCharge - (decimal)0.5 >= 0)
                DeliveryCharge -= (decimal)0.5;
        }


        public virtual void DoEatIn()
        {
            CollectOrderInformation();

            if (TableId > 0)
            {
                var table = _appScope.AvailableTables.FirstOrDefault(r => r.Id == TableId);
                table.NumberOfPeople = NumberOfPeople;
                table.Order = MyOrder;
                table.Started = DateTime.Now;

                //SaveOrder
                _service.SaveTableSettings(table);
                _eventAggregator.Publish(IoC.Get<IEatInTableViewModel>(), action => Task.Factory.StartNew(action));

            }
            else
            {
                var orderHolder = new WsOrderHolder
                {
                    Order = MyOrder,
                    HoldTime = DateTime.Now
                };

                _service.HoldTempOrder(orderHolder);
                StartNewPos();
            }



        }

        private void StartNewPos()
        {
            var viewModel = IoC.Get<IPosViewModel>();
            viewModel.InitialViewModel();
            viewModel.InitOrder();
            _eventAggregator.Publish(viewModel, action => Task.Factory.StartNew(action));
        }


        public void DoAddressLookup()
        {
            var viewModel = IoC.Get<IAddressLookupViewModel>();
            viewModel.CustomerEmail = CustomerEmail;
            viewModel.CustomerName = CustomerName;
            viewModel.CustomerPhone = CustomerPhone;
            viewModel.CustomerAddress = CustomerAddress;
            OpenWindow(_windowManager, viewModel, "Address Lookup", 950, 720);
            if (viewModel.SelectionComplete)
            {
                NotSearch = true;
                CustomerAddress = viewModel.CustomerAddress;
                CustomerName = viewModel.CustomerName;
                CustomerEmail = viewModel.CustomerEmail;
                CustomerPhone = viewModel.CustomerPhone;

                CustomerDeliveryCharge = viewModel.DeliveryCostAmount ?? 0;
                if (viewModel.DeliveryCostAmount.HasValue && SelectedDeliveryOptionDisplay == Service.Contract.Enum.DeliveryOptions.DisplayDelivery)
                {
                    DeliveryCharge = viewModel.DeliveryCostAmount.Value;
                }

            }


        }


        protected WsDeliveryOption CalculateDeliveryType()
        {
            switch (SelectedDeliveryOptionDisplay)
            {
                case Service.Contract.Enum.DeliveryOptions.DisplayWalkIn:
                case Service.Contract.Enum.DeliveryOptions.DisplayCollection:
                    return
                        DeliveryOptions.FirstOrDefault(
                            r => r.Name == Service.Contract.Enum.DeliveryOptions.ForCollection);
                case Service.Contract.Enum.DeliveryOptions.ForEatIn:
                    return
                        DeliveryOptions.FirstOrDefault(
                            r => r.Name == Service.Contract.Enum.DeliveryOptions.ForEatIn);
                default:
                    return
                        DeliveryOptions.FirstOrDefault(
                            r => r.Name == Service.Contract.Enum.DeliveryOptions.ForDelivery);
            }
        }


        public void SelectHotItem(int itemId)
        {

            var selectItem = HotItems.FirstOrDefault(r => r.Id == itemId);
            AddToBasketOrOpenOptionExtra(selectItem);
        }

        public virtual void DoNewOrder()
        {

            //Show New Caller ID Window 
            var viewModel = IoC.Get<INewOrderSelectionViewModel>();
            OpenWindow(_windowManager, viewModel, "Caller ID");
            if (viewModel.SelectionComplete)
                CustomerPhone = viewModel.PhoneIn;

        }


        public virtual void DoReprint()
        {

            CalculateTotal();
            var maxBatchId = MyOrder.OrderItems.Max(r => r.BatchId);
            if (maxBatchId.HasValue && GetPrintInBatch(_appScope.Settings) && BatchId == maxBatchId)
                PrinterQueue.PrintOrder(MyOrder, GetPrinterSettings(_appScope.Settings), _appScope.ShopInfo, "Takeaway Receipt", WsPrintModeEnum.RePrint.ToString(), maxBatchId.Value);

            else if (BatchId > maxBatchId)
                PrinterQueue.PrintOrder(MyOrder, GetPrinterSettings(_appScope.Settings), _appScope.ShopInfo, "Takeaway Receipt", WsPrintModeEnum.RePrint.ToString(), 0, 1);

            else
                PrinterQueue.PrintOrder(MyOrder, GetPrinterSettings(_appScope.Settings), _appScope.ShopInfo, "Takeaway Receipt", WsPrintModeEnum.RePrint.ToString());

        }



        public virtual void DoTakePayment()
        {
        }


        public void DoDiscount()
        {
            if (MyOrder == null || MyOrder.OrderOriginalTotal == 0)
                return;

            var viewModel = IoC.Get<IApplyDiscountViewModel>();
            viewModel.SelectedOrder = MyOrder;
            viewModel.InitDiscountAllowed();

            //Set Discount initially
            viewModel.DiscountAmount = DiscountAmount;

            OpenWindow(_windowManager, viewModel, StringResource.ApplyDiscount_applyDiscount, 500, 400);

            //Update Discount 
            DiscountAmount = viewModel.DiscountAmount;

            CalculateTotal();

        }

        public void DoClone()
        {
            if (SelectedOrderItem == null)
                return;
            var obj = SerializeHelper.CloneJson(SelectedOrderItem);
            obj.Guid = Guid.NewGuid().ToString();
            obj.BatchId = BatchId;
            MyOrder.OrderItems.Add(obj);
            var selection = MyOrder.OrderItems.FirstOrDefault(r => r.Guid == obj.Guid);
            SelectedOrderItem = selection;

            CalculateTotal();
        }


        protected bool IsDeliveryWalkIn()
        {
            return SelectedDeliveryOptionDisplay == Service.Contract.Enum.DeliveryOptions.DisplayWalkIn;
        }


        protected void CalculateTotal()
        {
            if (ControlFlag.HasValue && ControlFlag.Value)
                return;

            CalculateDeliveryCharge();

            if (MyOrder == null || MyOrder.OrderItems == null)
                return;

            //Calculate Total
            MyOrder.OrderOriginalTotal = MyOrder.OrderItems.Where(item => item.Price.HasValue).Sum(item => item.Price.Value);
            MyOrder.OrderTotal = MyOrder.OrderOriginalTotal - DiscountAmount;
            //Set Delivery Charge
            MyOrder.DeliveryCharge = DeliveryCharge;

            //Set Order Total
            OrderTotal = MyOrder.OrderTotal + MyOrder.DeliveryCharge;
            OrderItems = new ObservableCollection<WsMenuItem>(MyOrder.OrderItems);
            MyOrder.TableCommand = SelectedTableCommandsList == null ? new List<WsTableCommand>() : SelectedTableCommandsList.ToList();

        }

        public virtual void DoCancelTable()
        {
            if (TableId == 0) return;
            Logger.ErrorFormat("[DoCancelTable] Cancel ID: {0}", TableId);
            var table = new WsTables
            {
                Id = TableId,
                Name = string.Format("Table {0}", TableId),
            };
            _service.SaveTableSettings(table);
            ResetOrder();
        }

        #endregion

    }
}