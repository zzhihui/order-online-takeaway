using System.Collections.ObjectModel;
using Caliburn.Micro;
using Takeaway.Admin.Util.Services;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;


namespace Takeaway.Admin.Wpf.ViewModels
{

    public interface IBaseSelectDriverViewModel:IScreen
    {
        ObservableCollection<WsDriver> Drivers { get; set; }
        WsOrder SelectedOrder { get; set; }
        bool IsCompleted { get; set; }
    }


    public abstract class BaseSelectDriverViewModel : BaseViewModel, IBaseSelectDriverViewModel
    {

        #region Properties

        private ObservableCollection<WsDriver> _drivers;
        public ObservableCollection<WsDriver> Drivers
        {
            get
            {
                return this._drivers;
            }
            set
            {
                if (value != this._drivers)
                {
                    this._drivers = value;
                    NotifyOfPropertyChange(() => Drivers);
                }
            }
        }

        private WsDriver _selectedDriver;
        public WsDriver SelectedDriver
        {
            get
            {
                return this._selectedDriver;
            }
            set
            {
                if (value != this._selectedDriver)
                {
                    this._selectedDriver = value;
                    NotifyOfPropertyChange(() => SelectedDriver);
                }
            }
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get
            {
                return this._isCompleted;
            }
            set
            {
                if (value != this._isCompleted)
                {
                    this._isCompleted = value;
                    NotifyOfPropertyChange(() => IsCompleted);
                }
            }
        }

        private WsOrder _selectedOrder;
        public WsOrder SelectedOrder
        {
            get
            {
                return this._selectedOrder;
            }
            set
            {
                if (value == this._selectedOrder) return;
                this._selectedOrder = value;
                NotifyOfPropertyChange(() => SelectedOrder);
            }
        }

        #endregion

        #region Constructors

        protected IOfflineShopService _shopService;


        #endregion

        #region Methods
       
        public virtual void DoSelectDriver()
        {
            if (SelectedDriver == null)
                return;

            SelectedOrder.DriverName = SelectedDriver.Name;

            _shopService.SaveOrder(SelectedOrder);

            IsCompleted = true;
            this.TryClose();
        }

        public virtual void DoCancel()
        {
            this.TryClose();
        }

        #endregion
    }
}