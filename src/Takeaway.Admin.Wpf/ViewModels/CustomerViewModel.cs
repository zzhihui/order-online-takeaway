using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Admin.Wpf.ViewModels
{


    public interface ICustomerViewModel:IScreen
    {


    }

    [OfflineViewModel]
    public class CustomerViewModel:BaseViewModel, ICustomerViewModel
    {
        #region Properties

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

        #endregion

        private ObservableCollection<WsDeliveryCost> _deliveryCost;
        public ObservableCollection<WsDeliveryCost> DeliveryCost
        {
            get
            {
                return this._deliveryCost;
            }
            set
            {
                if (value != this._deliveryCost)
                {
                    this._deliveryCost = value;
                    NotifyOfPropertyChange(() => DeliveryCost);
                }
            }
        }


        private ObservableCollection<WsShopCustomer> _customers;
        public ObservableCollection<WsShopCustomer> Customers
        {
            get
            {
                return this._customers;
            }
            set
            {
                if (value != this._customers)
                {
                    this._customers = value;
                    NotifyOfPropertyChange(() => Customers);
                }
            }
        }


        private ObservableCollection<WsShopCustomer> _orgCustomers;
        public ObservableCollection<WsShopCustomer> OrgCustomers
        {
            get
            {
                return this._orgCustomers;
            }
            set
            {
                if (value != this._orgCustomers)
                {
                    this._orgCustomers = value;
                    NotifyOfPropertyChange(() => OrgCustomers);
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
                    PopulateCustomerDetail();
                    NotifyOfPropertyChange(() => SelectedCustomer);
                }
            }
        }

        #endregion

        #region Contructor

        private readonly IEventAggregator _eventAggregator;
        private readonly IOfflineShopService _service;
        private readonly IWindowManager _windowManager;
        private readonly IAppScope _appScope;

        public CustomerViewModel(IEventAggregator eventAggregator, IOfflineShopService service, IWindowManager windowManager, IAppScope appScope)
        {
            _eventAggregator = eventAggregator;
            _service = service;
            _windowManager = windowManager;
            _appScope = appScope;
            InitialViewModel();
        }

        #endregion

        #region Methods

        public void InitialViewModel()
        {

            InitialDeliveryCost();
            LoadAllCustomers();

        }

        private void InitialDeliveryCost()
        {
            DeliveryCost = new BindableCollection<WsDeliveryCost>(_appScope.FullDeliveryCost);
        }

        private void LoadAllCustomers()
        {
            Async(() =>
            {
                OrgCustomers = new BindableCollection<WsShopCustomer>(_service.GetAllCustomers());
            });
        }

        public void DoDeliveryAdd()
        {
            CustomerDeliveryCharge += (decimal)0.5;
        }

        public void DoDeliveryReduce()
        {
            if (CustomerDeliveryCharge - (decimal)0.5 >= 0)
                CustomerDeliveryCharge -= (decimal)0.5;
        }

       

        public void DoAddressLookup()
        {
            var viewModel = IoC.Get<IAddressLookupViewModel>();
            viewModel.CustomerEmail = CustomerEmail;
            viewModel.CustomerName = CustomerName;
            viewModel.CustomerPhone = CustomerPhone;
            viewModel.CustomerAddress = CustomerAddress;

            OpenWindow(_windowManager, viewModel, "Address Lookup", 900, 720);
            if (viewModel.SelectionComplete)
            {
                CustomerAddress = viewModel.CustomerAddress;
                CustomerName = viewModel.CustomerName;
                CustomerEmail = viewModel.CustomerEmail;
                CustomerPhone = viewModel.CustomerPhone;

                CustomerDeliveryCharge = viewModel.DeliveryCostAmount ?? 0;
            }
                
        }

        public void RestCustomer()
        {
            CustomerDeliveryCharge = null;
            CustomerEmail = string.Empty;
            CustomerPhone = string.Empty;
            CustomerName = string.Empty;
            CustomerAddress = string.Empty;
            Customers = new BindableCollection<WsShopCustomer>();

        }

        public void DoSearchCustomer()
        {
            Customers = OrgCustomers;
            if (!string.IsNullOrEmpty(CustomerPhone))
            {
                Customers = new BindableCollection<WsShopCustomer>(Customers.Where(r => r.PhoneNumber == CustomerPhone).ToList());
            }
            if (!string.IsNullOrEmpty(CustomerEmail))
            {
                Customers = new BindableCollection<WsShopCustomer>(Customers.Where(r => r.Email.Contains(CustomerEmail)).ToList());
            }
            if (!string.IsNullOrEmpty(CustomerName))
            {
                Customers = new BindableCollection<WsShopCustomer>(Customers.Where(r => r.ContactName.Contains(CustomerName)).ToList());
            }

        }

        public void DoSaveCustomer()
        {
            if (string.IsNullOrEmpty(CustomerPhone))
                return;

            Execute(() =>
            {
                _service.SaveCustomer(CustomerPhone, CustomerAddress, CustomerName, CustomerEmail, CustomerDeliveryCharge);
                MessageBox.Show("Customer Record Saved Successfully");
            });

            RestCustomer();
            LoadAllCustomers();
        }



        public void DoRemoveCustomer()
        {
            if (string.IsNullOrEmpty(CustomerPhone))
                return;

            Execute(() =>
            {
                _service.RemoveCustomer(CustomerPhone, CustomerAddress);
                MessageBox.Show("Customer Record Removed Successfully");
            });

            RestCustomer();
            LoadAllCustomers();
        }

        private void PopulateCustomerDetail()
        {
            if (SelectedCustomer == null)
                return;

            CustomerDeliveryCharge = SelectedCustomer.DeliveryCharge;
            CustomerEmail = SelectedCustomer.Email;
            CustomerPhone = SelectedCustomer.PhoneNumber;
            CustomerName = SelectedCustomer.ContactName;
            CustomerAddress = SelectedCustomer.Address;

        }

        #endregion
    }
}