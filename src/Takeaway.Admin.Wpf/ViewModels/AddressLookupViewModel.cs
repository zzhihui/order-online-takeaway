using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Shop;
using ILog = log4net.ILog;
using LogManager = log4net.LogManager;

namespace Takeaway.Admin.Wpf.ViewModels
{
    public interface IAddressLookupViewModel : IScreen
    {
        string CustomerAddress { get; set; }
        string CustomerEmail { get; set; }
        string CustomerPhone { get; set; }
        string CustomerName { get; set; }

        decimal? DeliveryCostAmount { get; set; }

        bool SelectionComplete { get; set; }


    }

    [OfflineViewModel]
    public class AddressLookupViewModel : BaseViewModel, IAddressLookupViewModel
    {
        private static readonly ILog Logger = LogManager.GetLogger("AddressLookupViewModel");

        #region Properties

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
                    NotifyOfPropertyChange(() => CustomerPhone);
                }
            }
        }
        
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
                    NotifyOfPropertyChange(() => CustomerName);
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
                    NotifyOfPropertyChange(() => CustomerAddress);
                }
            }
        }

        private decimal? _deliveryCostAmount;
        public decimal? DeliveryCostAmount
        {
            get
            {
                return this._deliveryCostAmount;
            }
            set
            {
                if (value != this._deliveryCostAmount)
                {
                    this._deliveryCostAmount = value;
                    NotifyOfPropertyChange(() => DeliveryCostAmount);
                }
            }
        }

        private string _selectedAddress;
        public string SelectedAddress
        {
            get
            {
                return this._selectedAddress;
            }
            set
            {
                if (value != this._selectedAddress)
                {
                    this._selectedAddress = value;
                    CustomerAddress = _selectedAddress;
                    NotifyOfPropertyChange(() => SelectedAddress);
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

        private string _webAddress;
        public string WebAddress
        {
            get
            {
                return this._webAddress;
            }
            set
            {
                if (value != this._webAddress)
                {
                    this._webAddress = value;
                    NotifyOfPropertyChange(() => WebAddress);
                }
            }
        }


        private string _distanceText;
        public string DistanceText
        {
            get
            {
                return this._distanceText;
            }
            set
            {
                if (value != this._distanceText)
                {
                    this._distanceText = value;
                    NotifyOfPropertyChange(() => DistanceText);
                }
            }
        }

        private List<WsShopCustomer> _customerList;
        public List<WsShopCustomer> CustomerList
        {
            get
            {
                return this._customerList;
            }
            set
            {
                if (value != this._customerList)
                {
                    this._customerList = value;
                    NotifyOfPropertyChange(() => CustomerList);
                }
            }
        }


        private WsShopCustomer _selectedCustomer;
        public WsShopCustomer SelectedCustomer
        {
            get
            {
                return this._selectedCustomer;
            }
            set
            {
                if (value != this._selectedCustomer)
                {
                    this._selectedCustomer = value;
                    NotifyOfPropertyChange(() => SelectedCustomer);
                }
            }
        }



        #endregion

        #region Constructor

        private readonly IAppScope _appScope;
        private readonly IOfflineShopService _offlineShopService;
        public AddressLookupViewModel(IAppScope appScope, IOfflineShopService offlineShopService)
        {
            _appScope = appScope;
            _offlineShopService = offlineShopService;
            SelectionComplete = false;
        }

        #endregion

        #region Methods



        public void OpeSelectedCustomer()
        {
            if (SelectedCustomer == null)
                return;
            
            CustomerPhone = SelectedCustomer.PhoneNumber;
            CustomerName = SelectedCustomer.ContactName;
            CustomerEmail = SelectedCustomer.Email;
            CustomerAddress = SelectedCustomer.Address;
        }


        public void DoCancel()
        {
            this.TryClose();
        }

        public void DoClearSearch()
        {
            CustomerEmail = string.Empty;
            CustomerPhone = string.Empty;
            CustomerName = string.Empty;
        }


        public void DoCustomerSearch()
        {
            if (string.IsNullOrEmpty(CustomerPhone)
                && string.IsNullOrEmpty(CustomerName)
                && string.IsNullOrEmpty(CustomerEmail)
                && string.IsNullOrEmpty(CustomerAddress)
                )
                return;

            CustomerList = _offlineShopService.SearchCustomer(CustomerPhone, CustomerName, CustomerEmail,
                CustomerAddress);
        }


        private void DoMapNavigation()
        {

            var customerAddress = string.IsNullOrEmpty(CustomerAddress)
                ? FormatAddress(_appScope.ShopInfo.ShopAddress1)
                : FormatAddress(CustomerAddress);

            WebAddress = string.Format("http://maps.google.com/?f=d&hl=en&saddr={0}&daddr={1}&z=100", _appScope.ShopInfo.ShopAddress1, customerAddress);
            CalculateDistance(customerAddress);

        }

        public void DoCalculateDeliveryCharge()
        {
            var customerAddress = string.IsNullOrEmpty(CustomerAddress)
               ? FormatAddress(_appScope.ShopInfo.ShopAddress1)
               : FormatAddress(CustomerAddress);

            CalculateDistance(customerAddress);
        }

        private void CalculateDistance(string customerAddress)
        {
            var distanceText = "";
            int distanceValue = 0;
            if (GoogleAddress.GetDirection(_appScope.ShopInfo.ShopAddress1, customerAddress, out distanceText,
                out distanceValue))
            {
                DeliveryCostAmount = CalculateDistanceCost(distanceValue);
            }
            DistanceText = distanceText;
        }

        private decimal? CalculateDistanceCost(int distanceValue)
        {
            var item = _appScope.FullDeliveryCost.FirstOrDefault(r => r.FromDistance <= distanceValue && r.ToDistance >= distanceValue);
            return item != null ? item.DeliveryCostAmount : null;
        }

        public void DoShowMap()
        {
            DoMapNavigation();
        }

        private string FormatAddress(string address)
        {
            var builder = new StringBuilder();
            builder.Append(address);
            return builder.ToString().Replace(" ", "+");
        }

        public void DoAddressSuggestion()
        {
            if (string.IsNullOrEmpty(CustomerAddress) || CustomerAddress.Length <= 6 || CustomerAddress == SelectedAddress)
                return;

            Async(() =>
            {

                AddressList = GoogleAddress.SuggestAddress(CustomerAddress);
            });

        }

        public void DoSelectAddress()
        {
            this.SelectionComplete = true;
            TryClose();
        }

        #endregion
    }
}