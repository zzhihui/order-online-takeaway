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
    public interface IOptionExtraMultiViewModel : IScreen
    {
        ObservableCollection<WsOptionItem> OptionItems { get; set; }
        ObservableCollection<WsOptionItem> OptionItems1 { get; set; }
        ObservableCollection<WsOptionItem> OptionItems2 { get; set; }
        ObservableCollection<WsOptionItem> OptionItems3 { get; set; }
        ObservableCollection<WsOptionItem> OptionItems4 { get; set; }

        string Comment { get; set; }
        decimal? AddtionalCharge { get; set; }
        bool SelectionComplete { get; set; }
    }
    [OfflineViewModel]
    public class OptionExtraMultiViewModel : BaseViewModel, IOptionExtraMultiViewModel
    {
        #region Properties

        private decimal? _addtionalCharge;
        public decimal? AddtionalCharge
        {
            get { return _addtionalCharge; }
            set
            {
                if (value != this._addtionalCharge)
                {
                    this._addtionalCharge = value;

                    NotifyOfPropertyChange(() => AddtionalCharge);
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

        private string _commentText;
        public string CommentText
        {
            get { return _commentText; }
            set
            {
                if (value != this._commentText)
                {
                    this._commentText = value;

                    NotifyOfPropertyChange(() => CommentText);
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

        #endregion

        #region Constructor

        private readonly IAppScope _appScope;

        public OptionExtraMultiViewModel(IAppScope appScope)
        {
            _appScope = appScope;
            OptionItemSelection = new BindableCollection<WsOptionItemSelection>();
            SelectionComplete = false;
            AddtionalCharge = 0;

        }

        #endregion

        #region Methods

        public void DoAdd()
        {
            ExtraCommand = (int)WsOptionItemCommandEnum.Add;
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

        public void RemoveOptionItem(int optionItemId)
        {
            var selectedOptionItem = OptionItemSelection.FirstOrDefault(r => r.Id == optionItemId);

            if (selectedOptionItem == null)
                return;

            OptionItemSelection.Remove(selectedOptionItem);

        }

        public void DoChange()
        {
            ExtraCommand = (int)WsOptionItemCommandEnum.Change;
        }

        public void AddOptionItem(int optionItemId)
        {
            var selectedOptionItem = OptionItems.FirstOrDefault(r => r.Id == optionItemId);
            if (selectedOptionItem == null)
                return;


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
            var translateFlagBoolean = translateFlag == (int)TranslateType.Chinese;

            switch (extraCommand)
            {
                case (int)WsOptionItemCommandEnum.Add:

                    costOfSelection = selectedOptionItem.UnitPrice ?? 0;

                    builder.AppendFormat("{0} ", translateFlagBoolean ? "加" : WsOptionItemCommandEnum.Add.ToString());
                    builder.AppendFormat("{0} ", selectedOptionItem.DisplayName);
                    if (selectedOptionItem.UnitPrice.HasValue && selectedOptionItem.UnitPrice.Value > 0)
                        builder.AppendFormat("{0}", selectedOptionItem.UnitPrice.ToPriceStringIe());
                    break;

                case (int)WsOptionItemCommandEnum.Extra:

                    costOfSelection = selectedOptionItem.UnitPrice ?? 0;

                    builder.AppendFormat("{0} ", translateFlagBoolean ? "多" : WsOptionItemCommandEnum.Extra.ToString());
                    builder.AppendFormat("{0} ", selectedOptionItem.DisplayName);
                    break;


                case (int)WsOptionItemCommandEnum.No:
                    builder.AppendFormat("{0} ", translateFlagBoolean ? "免" : WsOptionItemCommandEnum.No.ToString());
                    builder.AppendFormat("{0} ", selectedOptionItem.DisplayName);
                    break;


                case (int)WsOptionItemCommandEnum.Less:
                    builder.AppendFormat("{0} ", translateFlagBoolean ? "少" : WsOptionItemCommandEnum.Less.ToString());
                    builder.AppendFormat("{0} ", selectedOptionItem.DisplayName);
                    break;

                case (int)WsOptionItemCommandEnum.Change:
                    builder.AppendFormat("{0} ", translateFlagBoolean ? "换" : WsOptionItemCommandEnum.Change.ToString());
                    builder.AppendFormat("{0} ", selectedOptionItem.DisplayName);
                    break;
                case (int)WsOptionItemCommandEnum.Only:
                    builder.AppendFormat("{0} ", translateFlagBoolean ? "只" : WsOptionItemCommandEnum.Only.ToString());
                    builder.AppendFormat("{0} ", selectedOptionItem.DisplayName);
                    break;

                default:
                    builder.AppendFormat("{0} ", selectedOptionItem.DisplayName);
                    break;

            }

            return builder.ToString();

        }

        public void AddToBasket()
        {

            var selectionComment = GetOptionItemComment();

            if (!string.IsNullOrEmpty(selectionComment))
                Comment = selectionComment;
            SelectionComplete = true;
            this.TryClose();

        }

        private string GetOptionItemComment()
        {
            var builder = new StringBuilder();
            foreach (var item in OptionItemSelection)
            {

                builder.AppendFormat("{0}, ", item.Comment);
                AddtionalCharge += item.Cost;
            }

            return builder.ToString();
        }

        public void Cancel()
        {

            this.TryClose();
        }

        #endregion


    }
}