using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Threading;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Admin.Wpf.ViewModels
{

    public interface INewOrderSelectionViewModel : IScreen
    {
        string PhoneIn { get; set; }

        bool SelectionComplete { get; set; }
    }


    [OfflineViewModel]
    public class NewOrderSelectionViewModel : BaseViewModel, INewOrderSelectionViewModel
    {

        private const string Channel1Code = "1";
        private const string Channel2Code = "2";

        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger("NewOrderSelectionViewModel");

        #region Properties

        private string _phoneIn1;
        public string PhoneIn1
        {
            get
            {
                return this._phoneIn1;
            }
            set
            {
                if (value != this._phoneIn1)
                {
                    this._phoneIn1 = value;
                    NotifyOfPropertyChange(() => PhoneIn1);
                }
            }
        }

        private string _phoneIn2;
        public string PhoneIn2
        {
            get
            {
                return this._phoneIn2;
            }
            set
            {
                if (value != this._phoneIn2)
                {
                    this._phoneIn2 = value;
                    NotifyOfPropertyChange(() => PhoneIn2);
                }
            }
        }

        private string _phoneIn3;
        public string PhoneIn3
        {
            get
            {
                return this._phoneIn3;
            }
            set
            {
                if (value != this._phoneIn3)
                {
                    this._phoneIn3 = value;
                    NotifyOfPropertyChange(() => PhoneIn3);
                }
            }
        }

        private string _phoneIn4;
        public string PhoneIn4
        {
            get
            {
                return this._phoneIn4;
            }
            set
            {
                if (value != this._phoneIn4)
                {
                    this._phoneIn4 = value;
                    NotifyOfPropertyChange(() => PhoneIn4);
                }
            }
        }
        private string _phoneIn5;
        public string PhoneIn5
        {
            get
            {
                return this._phoneIn5;
            }
            set
            {
                if (value != this._phoneIn5)
                {
                    this._phoneIn5 = value;
                    NotifyOfPropertyChange(() => PhoneIn5);
                }
            }
        }


        private string _phoneSelected1;
        public string PhoneSelected1
        {
            get
            {
                return this._phoneSelected1;
            }
            set
            {
                if (value != this._phoneSelected1)
                {
                    this._phoneSelected1 = value;
                    NotifyOfPropertyChange(() => PhoneSelected1);
                }
            }
        }

        private string _phoneSelected2;
        public string PhoneSelected2
        {
            get
            {
                return this._phoneSelected2;
            }
            set
            {
                if (value != this._phoneSelected2)
                {
                    this._phoneSelected2 = value;
                    NotifyOfPropertyChange(() => PhoneSelected2);
                }
            }
        }

        private string _addressSelected1;
        public string AddressSelected1
        {
            get
            {
                return this._addressSelected1;
            }
            set
            {
                if (value != this._addressSelected1)
                {
                    this._addressSelected1 = value;
                    NotifyOfPropertyChange(() => AddressSelected1);
                }
            }
        }


        private string _addressSelected2;
        public string AddressSelected2
        {
            get
            {
                return this._addressSelected2;
            }
            set
            {
                if (value != this._addressSelected2)
                {
                    this._addressSelected2 = value;
                    NotifyOfPropertyChange(() => AddressSelected2);
                }
            }
        }



        private string _phoneIn;
        public string PhoneIn
        {
            get
            {
                return this._phoneIn;
            }
            set
            {
                if (value != this._phoneIn)
                {
                    this._phoneIn = value;
                    NotifyOfPropertyChange(() => PhoneIn);
                }
            }
        }




        private ObservableCollection<WsPhoneRecord> _phoneRecords;
        public ObservableCollection<WsPhoneRecord> PhoneRecords
        {
            get
            {
                return this._phoneRecords;
            }
            set
            {
                if (value != this._phoneRecords)
                {
                    this._phoneRecords = value;
                    NotifyOfPropertyChange(() => PhoneRecords);
                }
            }
        }


        #endregion

        #region Constructor

        private readonly IOfflineShopService _shopService;
        private DispatcherTimer _dispatcherTimer;
        private readonly IAppSettings _appSettings;


        public NewOrderSelectionViewModel(IAppSettings appSettings, IOfflineShopService shopService)
        {
            _appSettings = appSettings;
            _shopService = shopService;
            SelectionComplete = false;
            CheckCallerIdRunning();
            InitRead();
            InitialTimer();
        }

        #endregion

        #region Methods

        private void CheckCallerIdRunning()
        {
            Execute(() =>
            {
                var pname = Process.GetProcessesByName("callerId");
                if (pname.Length == 0)
                    StartCallerIdProcess();
            });
        }

        private void StartCallerIdProcess()
        {

            var path = _appSettings.GetSetting(AppSettingEnum.CallerIdExePath.ToString());
            Process.Start(path);
        }

        private void InitialTimer()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += DispatcherServerCheckTimerTick;
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(2);
            _dispatcherTimer.Start();
        }

        private void DispatcherServerCheckTimerTick(object sender, EventArgs e)
        {
            InitRead();
        }

        public override void CanClose(Action<bool> callback)
        {
            if (_dispatcherTimer != null)
                _dispatcherTimer.Stop();
            callback(true); // will cancel close
        }

        private void InitRead()
        {

            var callerIds = new List<WsPhoneRecord>();

            Execute(() => callerIds = _shopService.GetLastphoneIn5());

            PhoneRecords = new ObservableCollection<WsPhoneRecord>(callerIds ?? new List<WsPhoneRecord>());

            if (PhoneRecords.Count > 0)
                PhoneIn1 = PhoneRecords[0].Phone.TrimEnd('\0');
            if (PhoneRecords.Count > 1)
                PhoneIn2 = PhoneRecords[1].Phone.TrimEnd('\0');
            if (PhoneRecords.Count > 2)
                PhoneIn3 = PhoneRecords[2].Phone.TrimEnd('\0');
            if (PhoneRecords.Count > 3)
                PhoneIn4 = PhoneRecords[3].Phone.TrimEnd('\0');
            if (PhoneRecords.Count > 4)
                PhoneIn5 = PhoneRecords[4].Phone.TrimEnd('\0');


            var phoneSelected1Obj = PhoneRecords.FirstOrDefault(r => r.Channel == Channel1Code && r.Created >= DateTime.Now.AddSeconds(-30));
            if (phoneSelected1Obj != null)
            {
                PhoneSelected1 = phoneSelected1Obj.Phone.TrimEnd('\0');
                var customer = _shopService.GetCustomerByPhoneNumber(PhoneSelected1);
                AddressSelected1 = customer != null ? customer.Address : "";
            }
            else
            {
                PhoneSelected1 = "";
                AddressSelected1 = "";
            }


            var phoneSelected2Obj = PhoneRecords.FirstOrDefault(r => r.Channel == Channel2Code && r.Created >= DateTime.Now.AddSeconds(-30));
            if (phoneSelected2Obj != null)
            {
                PhoneSelected2 = phoneSelected2Obj.Phone.TrimEnd('\0');
                var customer = _shopService.GetCustomerByPhoneNumber(PhoneSelected2);
                AddressSelected2 = customer != null ? customer.Address : "";
            }
            else
            {
                PhoneSelected2 = "";
                AddressSelected2 = "";
            }
        }

        public void SelectPhone1()
        {
            PhoneIn = PhoneSelected1;
            SelectionComplete = true;
            TryClose();
        }


        public void SelectPhone2()
        {
            PhoneIn = PhoneSelected2;
            SelectionComplete = true;
            TryClose();
        }

        public void Phone1()
        {
            PhoneIn = PhoneIn1;
            SelectionComplete = true;
            TryClose();
        }

        public void Phone2()
        {
            PhoneIn = PhoneIn2;
            SelectionComplete = true;
            TryClose();
        }
        public void Phone3()
        {
            PhoneIn = PhoneIn3;
            SelectionComplete = true;
            TryClose();
        }

        public void Phone4()
        {
            PhoneIn = PhoneIn4;
            SelectionComplete = true;
            TryClose();
        }
        public void Phone5()
        {
            PhoneIn = PhoneIn5;
            SelectionComplete = true;
            TryClose();
        }

        #endregion

    }
}