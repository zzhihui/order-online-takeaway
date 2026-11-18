using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;

using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Shop;
using System.Threading.Tasks;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.Services;
using Takeaway.Service.Contract.Entities.Settings;


namespace Takeaway.Admin.Wpf.ViewModels
{
    public interface IEatInTableViewModel : IScreen
    {

    }

    [OfflineViewModel]
    public class EatInTableViewModel : BaseViewModel, IEatInTableViewModel
    {
        #region Properties

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

        #endregion

        #region Constructors

        private readonly IAppScope _appScope;
        private readonly IEventAggregator _eventAggregator;
        private readonly IOfflineShopService _service;
        public EatInTableViewModel(IAppScope appScope, IEventAggregator eventAggregator, IOfflineShopService service)
        {
            _appScope = appScope;
            _eventAggregator = eventAggregator;
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

      

        public void SelectTable(int id)
        {
            var viewModel = IoC.Get<IPosViewModel>();
            viewModel.TableId = id;
            viewModel.InitialViewModel();

            viewModel.BatchId = 1;
            var currentTable = AvailableTables.FirstOrDefault(r => r.Id == id);
            if (currentTable!=null && currentTable.Order != null)
            {
                viewModel.SelectedTableCommandsList = new ObservableCollection<WsTableCommand>(currentTable.Order.TableCommand);
                var currentBatchId = currentTable.Order.OrderItems.Max(r => r.BatchId);
                if (currentBatchId.HasValue)
                    viewModel.BatchId = currentBatchId.Value + 1;
                viewModel.MyOrder = currentTable.Order;

            }
            viewModel.InitOrder();
            _eventAggregator.Publish(viewModel, action => Task.Factory.StartNew(action));
        }

        #endregion
    }
}