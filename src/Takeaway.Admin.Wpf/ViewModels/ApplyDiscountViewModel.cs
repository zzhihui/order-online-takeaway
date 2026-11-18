using System;
using System.Linq;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Resources;
using Core.Common.Utility.Extension;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;
using ILog = log4net.ILog;
using LogManager = log4net.LogManager;

namespace Takeaway.Admin.Wpf.ViewModels
{

    public interface IApplyDiscountViewModel : IScreen
    {
        decimal DiscountAmount { get; set; }

        WsOrder SelectedOrder { get; set; }

        void InitDiscountAllowed();
    }

    [OfflineViewModel]
    public class ApplyDiscountViewModel : BaseViewModel, IApplyDiscountViewModel
    {
        private static readonly ILog Logger = LogManager.GetLogger("ApplyDiscountViewModel");

        #region Properties

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


        private bool _isDiscountExcludeDrink;
        public bool IsDiscountExcludeDrink
        {
            get
            {
                return this._isDiscountExcludeDrink;
            }
            set
            {
                if (value != this._isDiscountExcludeDrink)
                {
                    this._isDiscountExcludeDrink = value;
                    NotifyOfPropertyChange(() => IsDiscountExcludeDrink);
                }
            }
        }

        /// <summary>
        /// Display Max Discount
        /// </summary>
        private string _maxDiscountAllowedText;
        public string MaxDiscountAllowedText
        {
            get
            {
                return this._maxDiscountAllowedText;
            }
            set
            {
                if (value != this._maxDiscountAllowedText)
                {
                    this._maxDiscountAllowedText = value;
                    NotifyOfPropertyChange(() => MaxDiscountAllowedText);
                }
            }
        }

        #endregion

        #region Constructor
      
        public ApplyDiscountViewModel()
        {
            
        }


        #endregion

        #region Methods

        public void InitDiscountAllowed()
        {
            if (SelectedOrder == null)
            {
                DiscountAmount = 0;
                MaxDiscountAllowedText = StringResource.ApplyDiscount_NotAllow;
            }
            else
            {
                DiscountAmount = 0;
                MaxDiscountAllowedText = string.Format(StringResource.ApplyDiscount_discountMax, SelectedOrder.OrderOriginalTotal.ToPriceStringIe());
            }
        }

        public void DoApplyPercent()
        {
            if(SelectedOrder == null)
                return;

            var discountMaxAmount = CalculateDiscountMaxAmount();
            DiscountAmount = Math.Round(discountMaxAmount * (ApplyDiscountPercent * (decimal)0.01), 2);
            
        }

        public void DoPercent(string enterValue)
        {
            if (SelectedOrder == null)
                return;

            var discountMaxAmount = CalculateDiscountMaxAmount();

            decimal applyDiscountPercentage = 0;

            if(decimal.TryParse(enterValue, out applyDiscountPercentage))
            {
                DiscountAmount = Math.Round(discountMaxAmount * (applyDiscountPercentage * (decimal)0.01), 2);
            }

        }

        private decimal CalculateDiscountMaxAmount()
        {
            var discountMaxAmount = SelectedOrder.OrderOriginalTotal;
            const int foodcateogryDrinkId = (int) WsFoodCategoryEnum.Drinks;
            //TODO: Drinks now have Drink and SoftDrink

            if (IsDiscountExcludeDrink)
                discountMaxAmount =
                    SelectedOrder.OrderItems.Where(r => r.FoodCategoryId != foodcateogryDrinkId).Sum(r => r.Price) ??
                    0;
            return discountMaxAmount;
        }

        public void DoApplyDiscountAmount()
        {
            //Validation
            if (DiscountAmount > SelectedOrder.OrderOriginalTotal)
                return;

            TryClose();

        }

        public void DoCancel()
        {
            TryClose();
        }

        #endregion
    }
}