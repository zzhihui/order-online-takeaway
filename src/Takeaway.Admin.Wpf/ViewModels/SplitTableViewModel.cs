using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Shop;


namespace Takeaway.Admin.Wpf.ViewModels
{

    public interface ISplitTableViewModel : IScreen
    {
        WsTables CurrentTable { get; set; }

    }

    [OfflineViewModel]
    public class SplitTableViewModel : BaseViewModel, ISplitTableViewModel
    {
        #region Properties

        
        private WsTables _currentTable;
        public WsTables CurrentTable
        {
            get
            {
                return this._currentTable;
            }
            set
            {
                if (value != this._currentTable)
                {
                    this._currentTable = value;
                    NotifyOfPropertyChange(() => CurrentTable);
                }
            }
        }

        private ObservableCollection<WsTables> _availableTables;
        public ObservableCollection<WsTables> AvailableTables
        {
            get
            {
                return this._availableTables;
            }
            set
            {
                if (value != this._availableTables)
                {
                    this._availableTables = value;
                    NotifyOfPropertyChange(() => AvailableTables);
                }
            }
        }

        private WsTables _selectedTable;
        public WsTables SelectedTable
        {
            get
            {
                return this._selectedTable;
            }
            set
            {
                if (value != this._selectedTable)
                {
                    this._selectedTable = value;
                    NotifyOfPropertyChange(() => SelectedTable);
                }
            }
        }

        #endregion

        #region Contructor
        
        private readonly IAppScope _appScope;
        private readonly IOfflineShopService _service;
        public SplitTableViewModel(IAppScope appScope, IOfflineShopService service)
        {
            _appScope = appScope;
            _service = service;
            InitialView();
        }

        #endregion

        #region Methods
       
        private void InitialView()
        {
            InitTables();
        }

        private void InitTables()
        {
            var tableSize = GetCustomerTableNumbers(_appScope.Settings);
            AvailableTables = new ObservableCollection<WsTables>(_service.GetExistingTables(tableSize));
            _appScope.AvailableTables = AvailableTables.ToList();
        }


        public void DoSplitTable()
        {
            if (SelectedTable == null || CurrentTable == null)
                return;

            if (SelectedTable.Id == CurrentTable.Id)
            {
                MessageBox.Show("Cannot Select the current Table", "Table Selection", MessageBoxButton.OK);
                return;
            }


            var loadTableInfo = _appScope.AvailableTables.FirstOrDefault(r=>r.Id==SelectedTable.Id);

            if (loadTableInfo.Started.HasValue)
            {
                MessageBox.Show("Table is already occupied", "Table Occupied", MessageBoxButton.OK);
                return;
            }

            loadTableInfo.Order = CurrentTable.Order;
            loadTableInfo.Started = CurrentTable.Started;
            _service.SaveTableSettings(loadTableInfo);
            this.TryClose();
        }

        public void DoCancel()
        {
            this.TryClose();
        }

        #endregion
    }
}