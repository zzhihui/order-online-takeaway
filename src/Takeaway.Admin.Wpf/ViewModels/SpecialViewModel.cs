using System;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Admin.Wpf.ViewModels
{
    public interface ISpecialViewModel : IScreen
    {
        WsMenuItem SelectedItem { get; set; }
        decimal ExtraCharge { get; set; }
        decimal OrgPrice { get; set; }
    }

    [OfflineViewModel]
    public class SpecialViewModel : BaseViewModel, ISpecialViewModel
    {


        #region Properties

        private decimal _applyDiscountPercent;
        public decimal ApplyDiscountPercent
        {
            get
            {
                return this._applyDiscountPercent;
            }
            set
            {
                if (value != this._applyDiscountPercent)
                {
                    this._applyDiscountPercent = value;
                    NotifyOfPropertyChange(() => ApplyDiscountPercent);
                }
            }
        }

        private WsMenuItem _selectedItem;
        public WsMenuItem SelectedItem
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

        private decimal _orgPrice;
        public decimal OrgPrice
        {
            get { return _orgPrice; }
            set
            {
                if (value != this._orgPrice)
                {
                    this._orgPrice = value;
                    NotifyOfPropertyChange(() => OrgPrice);
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (value != this._comment)
                {
                    this._comment = value;
                    NotifyOfPropertyChange(() => Comment);
                }
            }
        }

        #endregion


        #region Contructor

        public SpecialViewModel()
        {
            SetExtraCharge();
        }

        #endregion


        #region Methods

        private void SetExtraCharge()
        {
            if (SelectedItem != null)
            {
                if (SelectedItem.Price.HasValue)
                {
                    ExtraCharge = SelectedItem.Price.Value;
                    OrgPrice = SelectedItem.Price.Value;
                }

            }
        }

        public void DoApplySpecial()
        {

            SelectedItem.Price = ExtraCharge;
            SelectedItem.Comment += Comment;

            this.TryClose();

        }


        public void DoPercent(string enterValue)
        {

            switch (enterValue)
            {
                case "5":
                    ExtraCharge = Math.Round(OrgPrice * (decimal)0.95, 2);
                    Comment = "5% Discount Applied,";
                    break;
                case "10":
                    ExtraCharge = Math.Round(OrgPrice * (decimal)0.9, 2);
                    Comment = "10% Discount Applied,";
                    break;
                case "20":
                    ExtraCharge = Math.Round(OrgPrice * (decimal)0.8, 2);
                    Comment = "20% Discount Applied,";
                    break;

                case "50":
                    ExtraCharge = Math.Round(OrgPrice * (decimal)0.5, 2);
                    Comment = "50% Discount Applied,";
                    break;
            }

        }


        public void DoApplyPercent()
        {
            ExtraCharge = Math.Round(OrgPrice * (1-ApplyDiscountPercent*(decimal) 0.01), 2);
            Comment = string.Format("{0}% Discount Applied,", ApplyDiscountPercent);
        }



        public void DoCancel()
        {
            this.TryClose();
        }

        #endregion
    }
}