using System.Collections.Generic;
using System.Collections.ObjectModel;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Admin.Wpf.ViewModels
{

    public interface ITableCommandViewModel : IScreen
    {
        ObservableCollection<WsTableCommand> TableCommandList { get; set; }
        ObservableCollection<WsTableCommand> SelectedTableCommandsList { get; set; }

        bool SelectionComplete { get; set; }

    }

    [OfflineViewModel]
    public class TableCommandViewModel : BaseViewModel, ITableCommandViewModel
    {

        #region Methods

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

        private WsTableCommand _selectedTableCommand;
        public WsTableCommand SelectedTableCommand
        {
            get
            {
                return this._selectedTableCommand;
            }
            set
            {
                if (value != this._selectedTableCommand)
                {
                    this._selectedTableCommand = value;
                    NotifyOfPropertyChange(() => SelectedTableCommand);
                }
            }
        }

        private bool _selectionComplete;
        public bool SelectionComplete
        {
            get
            {
                return this._selectionComplete;
            }
            set
            {
                if (value != this._selectionComplete)
                {
                    this._selectionComplete = value;
                    NotifyOfPropertyChange(() => SelectionComplete);
                }
            }
        }

        #endregion

        #region Constructor

        public TableCommandViewModel()
        {
            SelectionComplete = false;
            SelectedTableCommandsList = new ObservableCollection<WsTableCommand>();
            TableCommandList = new ObservableCollection<WsTableCommand>();
        }

        #endregion

        #region Methods

        public void DoRemove()
        {
            if (SelectedTableCommand == null)
                return;
            SelectedTableCommandsList.Remove(SelectedTableCommand);
        }

        public void DoCancel()
        {
            this.TryClose();
        }

        public void DoAddTableCommand()
        {
            SelectionComplete = true;
            if (SelectedTableCommandsList == null)
                SelectedTableCommandsList = new ObservableCollection<WsTableCommand>();

            foreach (var command in TableCommandList)
            {
                if (!command.IsSelected)
                    continue;

                SelectedTableCommandsList.Add(command);
            }

            this.TryClose();

        }

        #endregion


    }
}