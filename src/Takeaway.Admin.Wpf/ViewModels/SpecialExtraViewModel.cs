using System;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Admin.Wpf.ViewModels
{
    public interface ISpecialExtraViewModel : IScreen
    {
        WsOptionItem SelectedItem { get; set; }
        decimal ExtraCharge { get; set; }

        bool SelectionComplete { get; set; }

    }

    [OfflineViewModel]
    public class SpecialExtraViewModel : BaseViewModel, ISpecialExtraViewModel
    {

        #region Properties

        private WsOptionItem _selectedItem;
        public WsOptionItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (value != this._selectedItem)
                {
                    this._selectedItem = value;
                    NotifyOfPropertyChange(() => SelectedItem);
                }
            }
        }

        private decimal _extraCharge;
        public decimal ExtraCharge
        {
            get { return _extraCharge; }
            set
            {
                if (value != this._extraCharge)
                {
                    this._extraCharge = value;
                    NotifyOfPropertyChange(() => ExtraCharge);
                }
            }
        }

        #endregion

        #region Constructors


        public SpecialExtraViewModel()
        {
            SelectionComplete = false;
        }

        #endregion

        #region Methods

        public void DoApplySpecial()
        {
            SelectionComplete = true;
            this.TryClose();
        }

        public void DoCancel()
        {
            this.TryClose();
        }

        #endregion


    }
}