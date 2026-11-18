using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Utility.Extension;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;

namespace Takeaway.Admin.Wpf.ViewModels
{

    public interface IOptionExtraViewModel : IScreen
    {
        WsMenuItem SelectedItem { get; set; }
        bool ExtraVisible { get; set; }

        WsOrder SelectedOrder { get; set; }

        string Comment { get; set; }

        ObservableCollection<WsOptionItem> OptionItems { get; set; }
        ObservableCollection<WsOptionItem> OptionItems1 { get; set; }
        ObservableCollection<WsOptionItem> OptionItems2 { get; set; }
        ObservableCollection<WsOptionItem> OptionItems3 { get; set; }
        ObservableCollection<WsOptionItem> OptionItems4 { get; set; }
    }

    [OfflineViewModel]
    public class OptionExtraViewModel : BaseViewModel, IOptionExtraViewModel
    {

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

        private ObservableCollection<SubItemModel> _subItemSelections;
        public ObservableCollection<SubItemModel> SubItemSelections
        {
            get { return _subItemSelections; }
            set
            {
                if (value != this._subItemSelections)
                {
                    this._subItemSelections = value;

                    NotifyOfPropertyChange(() => SubItemSelections);
                }
            }
        }

        private bool _extraVisible;
        public bool ExtraVisible
        {
            get { return _extraVisible; }
            set
            {
                if (value != this._extraVisible)
                {
                    this._extraVisible = value;

                    NotifyOfPropertyChange(() => ExtraVisible);
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
                    CreateSubItemModels();
                    NotifyOfPropertyChange(() => SelectedItem);
                }
            }
        }

        private ObservableCollection<WsOptionItem> _optionItems;
        public ObservableCollection<WsOptionItem> OptionItems
        {
            get { return _optionItems; }
            set
            {
                if (value != this._optionItems)
                {
                    this._optionItems = value;

                    NotifyOfPropertyChange(() => OptionItems);
                }
            }
        }


        private ObservableCollection<WsOptionItem> _optionItems1;
        public ObservableCollection<WsOptionItem> OptionItems1
        {
            get { return _optionItems1; }
            set
            {
                if (value != this._optionItems1)
                {
                    this._optionItems1 = value;

                    NotifyOfPropertyChange(() => OptionItems1);
                }
            }
        }


        private ObservableCollection<WsOptionItem> _optionItems2;
        public ObservableCollection<WsOptionItem> OptionItems2
        {
            get { return _optionItems2; }
            set
            {
                if (value != this._optionItems2)
                {
                    this._optionItems2 = value;

                    NotifyOfPropertyChange(() => OptionItems2);
                }
            }
        }


        private ObservableCollection<WsOptionItem> _optionItems3;
        public ObservableCollection<WsOptionItem> OptionItems3
        {
            get { return _optionItems3; }
            set
            {
                if (value != this._optionItems3)
                {
                    this._optionItems3 = value;

                    NotifyOfPropertyChange(() => OptionItems3);
                }
            }
        }

        private ObservableCollection<WsOptionItem> _optionItems4;
        public ObservableCollection<WsOptionItem> OptionItems4
        {
            get { return _optionItems4; }
            set
            {
                if (value != this._optionItems4)
                {
                    this._optionItems4 = value;

                    NotifyOfPropertyChange(() => OptionItems4);
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

        private int _extraCommand;
        public int ExtraCommand
        {
            get { return _extraCommand; }
            set
            {
                if (value != this._extraCommand)
                {
                    this._extraCommand = value;
                    NotifyOfPropertyChange(() => ExtraCommand);
                }
            }
        }

        private ObservableCollection<WsOptionItemSelection> _optionItemSelection;
        public ObservableCollection<WsOptionItemSelection> OptionItemSelection
        {
            get { return _optionItemSelection; }
            set
            {
                if (value != this._optionItemSelection)
                {
                    this._optionItemSelection = value;
                    NotifyOfPropertyChange(() => OptionItemSelection);
                }
            }
        }

        private WsOptionItemSelection _selectedOptionItemSelection;
        public WsOptionItemSelection SelectedOptionItemSelection
        {
            get { return _selectedOptionItemSelection; }
            set
            {
                if (value != this._selectedOptionItemSelection)
                {
                    this._selectedOptionItemSelection = value;
                    NotifyOfPropertyChange(() => SelectedOptionItemSelection);
                }
            }
        }



        #endregion

        #region Constructor

        private readonly IAppScope _appScope;
        private readonly IWindowManager _windowManager;

        public OptionExtraViewModel(IAppScope appScope, IWindowManager windowManager)
        {
            _appScope = appScope;
            _windowManager = windowManager;

            SubItemSelections = new ObservableCollection<SubItemModel>();
            OptionItemSelection = new ObservableCollection<WsOptionItemSelection>();
            ExtraCharge = 0;

        }

        #endregion

        #region Methods

        public void DoAdd()
        {
            ExtraCommand = (int)WsOptionItemCommandEnum.Add;
        }

        public void DoChange()
        {
            ExtraCommand = (int)WsOptionItemCommandEnum.Change;
        }

        public void DoRemove()
        {
            if (SelectedOptionItemSelection == null)
                return;

            OptionItemSelection.Remove(SelectedOptionItemSelection);
        }

        public void DoExtra()
        {
            ExtraCommand = (int)WsOptionItemCommandEnum.Extra;

        }

        public void DoNo()
        {
            ExtraCommand = (int)WsOptionItemCommandEnum.No;
        }

        public void DoLess()
        {
            ExtraCommand = (int)WsOptionItemCommandEnum.Less;
        }

        public void DoJust()
        {
            ExtraCommand = (int)WsOptionItemCommandEnum.Only;
        }

        public void DoSpecial()
        {
            ExtraCommand = (int)WsOptionItemCommandEnum.Special;
        }

        public void AddOptionItem(int optionItemId)
        {
            var selectedOptionItem = OptionItems.FirstOrDefault(r => r.Id == optionItemId);

            decimal costOfSelection;
            var commentValue = ExtraCommentOfSelection(selectedOptionItem, ExtraCommand, out costOfSelection);


            OptionItemSelection.Add(new WsOptionItemSelection()
            {
                AddCommand = ExtraCommand,
                OptionItem = selectedOptionItem,
                Cost = costOfSelection,
                Comment = commentValue
            });

            ExtraCommand = (int)WsOptionItemCommandEnum.None;
        }

        private string ExtraCommentOfSelection(WsOptionItem selectedOptionItem, int extraCommand, out decimal costOfSelection)
        {
            costOfSelection = 0;

            if (selectedOptionItem == null)
                return string.Empty;

            var builder = new StringBuilder();

            var translateFlag = GetTranslatedFlag(_appScope.Settings);
            //var translateFlagBoolean = (translateFlag == (int)TranslateType.Both) || (translateFlag == (int)TranslateType.Chinese);

            switch (extraCommand)
            {

                case (int)WsOptionItemCommandEnum.Add:

                    costOfSelection = selectedOptionItem.UnitPrice ?? 0;

                    builder.AppendFormat("{0} ", GetDisplayOptionAdd(translateFlag));
                        
                    builder.AppendFormat("{0} ", selectedOptionItem.DisplayName);
                    if (selectedOptionItem.UnitPrice.HasValue && selectedOptionItem.UnitPrice.Value > 0)
                        builder.AppendFormat("{0}", selectedOptionItem.UnitPrice.ToPriceStringIe());
                    break;

                case (int)WsOptionItemCommandEnum.Extra:

                    costOfSelection = selectedOptionItem.UnitPrice ?? 0;

                    builder.AppendFormat("{0} ", GetDisplayOptionExtra(translateFlag));
                    builder.AppendFormat("{0} ", selectedOptionItem.DisplayName);
                    break;


                case (int)WsOptionItemCommandEnum.No:
                    builder.AppendFormat("{0} ", GetDisplayOptionNo(translateFlag));
                    builder.AppendFormat("{0} ", selectedOptionItem.DisplayName);
                    break;


                case (int)WsOptionItemCommandEnum.Less:
                    builder.AppendFormat("{0} ", GetDisplayOptionLess(translateFlag));
                    builder.AppendFormat("{0} ", selectedOptionItem.DisplayName);
                    break;

                case (int)WsOptionItemCommandEnum.Change:
                    builder.AppendFormat("{0} ", GetDisplayOptioChange(translateFlag));
                    builder.AppendFormat("{0} ", selectedOptionItem.DisplayName);
                    break;
                case (int)WsOptionItemCommandEnum.Only:
                    builder.AppendFormat("{0} ", GetDisplayOptionOnly(translateFlag));
                    builder.AppendFormat("{0} ", selectedOptionItem.DisplayName);
                    break;

                case (int)WsOptionItemCommandEnum.Special:
                    var viewModel = IoC.Get<ISpecialExtraViewModel>();
                    viewModel.SelectedItem = selectedOptionItem;
                    OpenWindow(_windowManager, viewModel, "Special", 350, 350);
                    if (!viewModel.SelectionComplete)
                        break;
                    builder.AppendFormat("{0} ", WsOptionItemCommandEnum.Special.ToString());
                    costOfSelection = viewModel.ExtraCharge;
                    builder.AppendFormat("{0} ", selectedOptionItem.DisplayName);
                    builder.AppendFormat("{0}", costOfSelection.ToPriceStringIe());
                    break;

                default:
                    builder.AppendFormat("{0} ", selectedOptionItem.DisplayName);
                    break;

            }




            return builder.ToString();

        }

        private string GetDisplayOptionAdd(int translateFlag)
        {
            switch (translateFlag)
            {
                case (int)TranslateType.Both:
                    return string.Format("{0}({1})", WsOptionItemCommandEnum.Add.ToString(), "加");
                case (int)TranslateType.Chinese:
                    return "加";
                default:
                    return WsOptionItemCommandEnum.Add.ToString();

            }
        }
        private string GetDisplayOptionExtra(int translateFlag)
        {
            switch (translateFlag)
            {
                case (int)TranslateType.Both:
                    return string.Format("{0}({1})", WsOptionItemCommandEnum.Extra.ToString(), "多");
                case (int)TranslateType.Chinese:
                    return "多";
                default:
                    return WsOptionItemCommandEnum.Extra.ToString();

            }
        }

        private string GetDisplayOptionNo(int translateFlag)
        {
            switch (translateFlag)
            {
                case (int)TranslateType.Both:
                    return string.Format("{0}({1})", WsOptionItemCommandEnum.No.ToString(), "免");
                case (int)TranslateType.Chinese:
                    return "免";
                default:
                    return WsOptionItemCommandEnum.No.ToString();

            }
        }

        private string GetDisplayOptionLess(int translateFlag)
        {
            switch (translateFlag)
            {
                case (int)TranslateType.Both:
                    return string.Format("{0}({1})", WsOptionItemCommandEnum.Less.ToString(), "少");
                case (int)TranslateType.Chinese:
                    return "少";
                default:
                    return WsOptionItemCommandEnum.Less.ToString();

            }
        }

        private string GetDisplayOptionOnly(int translateFlag)
        {
            switch (translateFlag)
            {
                case (int)TranslateType.Both:
                    return string.Format("{0}({1})", WsOptionItemCommandEnum.Only.ToString(), "只");
                case (int)TranslateType.Chinese:
                    return "只";
                default:
                    return WsOptionItemCommandEnum.Only.ToString();

            }
        }

        private string GetDisplayOptioChange(int translateFlag)
        {
            switch (translateFlag)
            {
                case (int)TranslateType.Both:
                    return string.Format("{0}({1})", WsOptionItemCommandEnum.Change.ToString(), "换");
                case (int)TranslateType.Chinese:
                    return "换";
                default:
                    return WsOptionItemCommandEnum.Change.ToString();

            }
        }


        private decimal ExtraCostOfSelection(WsOptionItem selectedOptionItem, int extraCommand)
        {
            if (selectedOptionItem == null)
                return 0;

            switch (extraCommand)
            {
                case (int)WsOptionItemCommandEnum.Extra:
                    return selectedOptionItem.UnitPrice ?? 0;

                default:
                    return 0;
            }
        }


        private void CreateSubItemModels()
        {
            if (SelectedItem == null)
                return;


            var transFlag = GetTranslatedFlag(_appScope.Settings);

            foreach (var subItem in SelectedItem.SubItemSelections)
            {
                subItem.Guid = Guid.NewGuid().ToString();
                subItem.Selections = Translate(subItem.Selections.OrderBy(r=>r.Id).ToList(), transFlag);
                SubItemSelections.Add(SubItemModel.CreateSubItem(subItem, ExtraVisible));
            }
        }

        public void Cancel()
        {
            ClearSelection();

            this.TryClose();
        }

        public void DoPopupOptionItem(int? optionGroupId, string subItemGuid)
        {
            if (!optionGroupId.HasValue)
                return;

            var item = SubItemSelections.FirstOrDefault(r => r.Guid == subItemGuid);

            if (item == null)
                return;

            var optionExtraViewModel = IoC.Get<IOptionExtraMultiViewModel>();
            optionExtraViewModel.OptionItems = OptionItems;
            optionExtraViewModel.OptionItems1 = OptionItems1;
            optionExtraViewModel.OptionItems2 = OptionItems2;
            optionExtraViewModel.OptionItems3 = OptionItems3;
            optionExtraViewModel.OptionItems4 = OptionItems4;


            OpenWindow(_windowManager, optionExtraViewModel, "Selection", 1000, 550);

            if (!optionExtraViewModel.SelectionComplete) return;
            SelectedItem.Price += optionExtraViewModel.AddtionalCharge;
            item.Comment = optionExtraViewModel.Comment;
        }

        public void AddToBasket()
        {

            foreach (var vmSubItem in SubItemSelections)
            {
                var item = SelectedItem.SubItemSelections.FirstOrDefault(r => r.Guid == vmSubItem.Guid);
                item.Selected = vmSubItem.SelectedOptionExtraItem;
                if (item.Selected == null)
                    continue;
                item.Selected.Comment = vmSubItem.Comment;
                SelectedItem.Price += vmSubItem.SelectedOptionExtraItem.Price;

            }


            var finalComment = string.Format("{0} {1}", GetOptionItemComment(), Comment);
            SelectedItem.Price += ExtraCharge;
          
            SelectedItem.Comment += finalComment;

            ClearSelection();

            this.TryClose();

        }

        private string GetOptionItemComment()
        {
            var builder = new StringBuilder();
            foreach (var item in OptionItemSelection)
            {

                builder.AppendFormat("{0}, ", item.Comment);
                ExtraCharge += item.Cost;
            }

            return builder.ToString();
        }

        private void ClearSelection()
        {
            SubItemSelections = new ObservableCollection<SubItemModel>();
            OptionItemSelection = new ObservableCollection<WsOptionItemSelection>();
            ErrorMessage = "";
            Comment = "";
        }

        protected override void OnDeactivate(bool close)
        {
            ClearSelection();
        }

        #endregion
    }
}