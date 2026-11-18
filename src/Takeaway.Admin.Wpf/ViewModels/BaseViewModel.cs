using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Core.Common.Utility;
using Takeaway.Service.Contract.Entities.Print;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;
using Takeway.Mobile.Core.Utilities;
using Action = System.Action;

namespace Takeaway.Admin.Wpf.ViewModels
{

    public class BaseViewModel : Screen
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger("BaseViewModel");

        #region Properties

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

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return this._errorMessage;
            }
            set
            {
                if (value == this._errorMessage) return;
                this._errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }

        private bool _onlineFeature;
        public bool OnlineFeature
        {
            get
            {
                return this._onlineFeature;
            }
            set
            {
                if (value == this._onlineFeature) return;
                this._onlineFeature = value;
                NotifyOfPropertyChange(() => OnlineFeature);
            }
        }

        private bool _posFeature;
        public bool PosFeature
        {
            get
            {
                return this._posFeature;
            }
            set
            {
                if (value == this._posFeature) return;
                this._posFeature = value;
                NotifyOfPropertyChange(() => PosFeature);
            }
        }
     
       

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return this._isBusy;
            }
            set
            {
                if (value == this._isBusy) return;
                this._isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }


        private bool _isAdmin;
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                NotifyOfPropertyChange(() => IsAdmin);

            }
        }

        #endregion

        #region Methods

        public bool VerifyMoreThanUserRights(IWindowManager windowManager)
        {
            var viewModel = IoC.Get<IPinViewModel>();
            OpenWindow(windowManager, viewModel, "Pin Required", 300, 450);

            return viewModel.HaveRights;
        }

        public List<WsMenuItem> Translate(List<WsMenuItem> items, int translate)
        {

            foreach (var item in items)
            {
                switch (translate)
                {
                    case (int)TranslateType.Chinese:
                        item.DisplayName = item.TranslatedName;
                        break;
                    case (int)TranslateType.Both:
                        item.DisplayName = string.Format("{1}. {0}", item.Name, item.TranslatedName);
                        break;
                    default:
                        item.DisplayName = item.Name;
                        break;
                }


                Translate(item, translate);
            }

            return items;
        }

        public WsMenuItem TranslateItem(WsMenuItem item, int translate)
        {


            switch (translate)
            {
                case (int)TranslateType.Chinese:
                    item.DisplayName = item.TranslatedName;
                    break;
                case (int)TranslateType.Both:
                    item.DisplayName = string.Format("{1}. {0}", item.Name, item.TranslatedName);
                    break;
                default:
                    item.DisplayName = item.Name;
                    break;
            }

            return item;
        }

        public List<WsMenuItemCategory> Translate(List<WsMenuItemCategory> items, int translate)
        {

            foreach (var item in items)
            {
                switch (translate)
                {
                    case (int)TranslateType.Chinese:
                        item.DisplayName = item.TranslateName;
                        break;
                    case (int)TranslateType.Both:
                        item.DisplayName = string.Format("{1}. {0}", item.Name, item.TranslateName);
                        break;
                    default:
                        item.DisplayName = item.Name;
                        break;
                }
            }

            return items;
        }

        private void Translate(WsMenuItem meniItem, int translate)
        {
            if (meniItem.SubItemSelections == null)
                return;

            foreach (var item in meniItem.SubItemSelections)
            {
                foreach (var selection in item.Selections)
                {
                    switch (translate)
                    {
                        case (int)TranslateType.Chinese:
                            selection.DisplayName = selection.TranslatedName;
                            break;
                        case (int)TranslateType.Both:
                            selection.DisplayName = string.Format("{1}. {0}", selection.Name, selection.TranslatedName);
                            break;
                        default:
                            selection.DisplayName = item.Name;
                            break;
                    }
                }
            }
        }

        public List<WsOptionItem> Translate(List<WsOptionItem> items, int translate)
        {

            foreach (var item in items)
            {

                switch (translate)
                {
                    case (int)TranslateType.Chinese:
                        item.DisplayName = item.TranslateName;
                        break;
                    case (int)TranslateType.Both:
                        item.DisplayName = string.Format("{1}. {0}", item.ItemName, item.TranslateName);
                        break;
                    default:
                        item.DisplayName = item.ItemName;
                        break;
                }

            }
            return items;
        }

        public List<WsTableCommand> Translate(List<WsTableCommand> items, int translate)
        {

            foreach (var item in items)
            {

                switch (translate)
                {
                    case (int)TranslateType.Chinese:
                        item.DisplayName = item.TranslateName;
                        break;
                    case (int)TranslateType.Both:
                        item.DisplayName = string.Format("{1}. {0}", item.ItemName, item.TranslateName);
                        break;
                    default:
                        item.DisplayName = item.ItemName;
                        break;
                }
            }

            return items;
        }


        public WsSetting GetSettingByKey(string settingKey, List<WsSetting> systemSettings)
        {
            return systemSettings.FirstOrDefault(r => r.Name == settingKey);
        }


        protected WsPrintSetting GetFirstPrinterName(List<WsSetting> settings)
        {
            return LoadIndividualPrinterSettings(settings, 1);
        }

        private WsPrintSetting LoadIndividualPrinterSettings(List<WsSetting> OfflineSettings, int index)
        {
            var printerSetting = new WsPrintSetting();

            var printerReceiptWidthKey = string.Format("{0}{1}", WsOfflineSettingEnum.AppPrinterReceiptWidth, index);
            var printerXflowKey = string.Format("{0}{1}", WsOfflineSettingEnum.AppPrinterXflow, index);
            var printerYflowKey = string.Format("{0}{1}", WsOfflineSettingEnum.AppPrinterYflow, index);
            var printerFontNameKey = string.Format("{0}{1}", WsOfflineSettingEnum.AppPrinterFontName, index);
            var printerLargeFontSizeKey = string.Format("{0}{1}", WsOfflineSettingEnum.AppPrinterLargeFontSize, index);
            var printerMediumFontSizeKey = string.Format("{0}{1}", WsOfflineSettingEnum.AppPrinterMediumFontSize, index);
            var printerSmallFontSizeKey = string.Format("{0}{1}", WsOfflineSettingEnum.AppPrinterSmallFontSize, index);
            var printNameKey = string.Format("{0}{1}", WsOfflineSettingEnum.AppPrintName, index);
            var printWithGroupKey = string.Format("{0}{1}", WsOfflineSettingEnum.AppPrinterPrintWithGroup, index);
            var printLanguageKey = string.Format("{0}{1}", WsOfflineSettingEnum.AppPrinterPrintLanguage, index);



            var printerReceiptWidth = OfflineSettings.FirstOrDefault(r => r.Name == printerReceiptWidthKey);
            var printerReceiptWidthValue = float.Parse(printerReceiptWidth.SettingValue, CultureInfo.InvariantCulture.NumberFormat);

            var printerXflow = OfflineSettings.FirstOrDefault(r => r.Name == printerXflowKey);
            var printerXflowValue = float.Parse(printerXflow.SettingValue, CultureInfo.InvariantCulture.NumberFormat);

            var printerYflow = OfflineSettings.FirstOrDefault(r => r.Name == printerYflowKey);
            var printerYflowValue = float.Parse(printerYflow.SettingValue, CultureInfo.InvariantCulture.NumberFormat);

            var printerFontName = OfflineSettings.FirstOrDefault(r => r.Name == printerFontNameKey);

            var printerLargeFontSize = OfflineSettings.FirstOrDefault(r => r.Name == printerLargeFontSizeKey);
            var printerLargeFontSizeValue = IntegerStringUtil.ConvertStringValue(printerLargeFontSize.SettingValue);

            var printerMediumFontSize = OfflineSettings.FirstOrDefault(r => r.Name == printerMediumFontSizeKey);
            var printerMediumFontSizeValue = IntegerStringUtil.ConvertStringValue(printerMediumFontSize.SettingValue);

            var printerSmallFontSize = OfflineSettings.FirstOrDefault(r => r.Name == printerSmallFontSizeKey);
            var printerSmallFontValue = IntegerStringUtil.ConvertStringValue(printerSmallFontSize.SettingValue);

            var printerName = OfflineSettings.FirstOrDefault(r => r.Name == printNameKey);
            var printWithGroup = BooleanStringUtil.ConvertStringValue(OfflineSettings.FirstOrDefault(r => r.Name == printWithGroupKey).SettingValue);
            var printLanguage = IntegerStringUtil.ConvertStringValue(OfflineSettings.FirstOrDefault(r => r.Name == printLanguageKey).SettingValue);


            printerSetting.PrinterReceiptWidth = printerReceiptWidthValue;
            printerSetting.PrinterXflow = printerXflowValue;
            printerSetting.PrinterYflow = printerYflowValue;
            printerSetting.PrinterFontName = printerFontName.SettingValue;
            printerSetting.PrinterLargeFontSize = printerLargeFontSizeValue;
            printerSetting.PrinterMediumFontSize = printerMediumFontSizeValue;
            printerSetting.PrinterSmallFontSize = printerSmallFontValue;
            printerSetting.PrinterName = printerName.SettingValue;
            printerSetting.PrintWithGroup = printWithGroup;
            printerSetting.PrintLanguage = printLanguage;

            return printerSetting;
        }


        protected List<WsPrintSetting> GetPrinterSettings(List<WsSetting> settings)
        {
            var printerSettings = new List<WsPrintSetting>();

            for (var i = 1; i <= Constants.SupportPrinterNumber; i++)
            {
                printerSettings.Add(LoadIndividualPrinterSettings(settings,i));
            }

            return printerSettings;
        }

        protected int GetTranslatedFlag(List<WsSetting> settings)
        {
            var setting = GetSettingByKey(WsOfflineSettingEnum.AppUserTranslateInternal, settings);
            return setting == null ? 0 : IntegerStringUtil.ConvertStringValue(setting.SettingValue);
        }


        protected int GetCustomerTableNumbers(List<WsSetting> settings)
        {
            var setting = GetSettingByKey(WsOfflineSettingEnum.AppTableNumber, settings);
            return setting == null ? 10 : IntegerStringUtil.ConvertStringValue(setting.SettingValue);
        }


        protected bool GetAutoPopupExtra(List<WsSetting> settings)
        {
            var setting = settings.FirstOrDefault(r => r.Name == WsOfflineSettingEnum.AppAutoPopupExtra);
            return setting == null || BooleanStringUtil.ConvertStringValue(setting.SettingValue);
        }


        public bool GetPrintInBatch(List<WsSetting> settings)
        {
            var setting = settings.FirstOrDefault(r => r.Name == WsOfflineSettingEnum.AppPrintInBatch);
            return setting == null || BooleanStringUtil.ConvertStringValue(setting.SettingValue);
        }

        #region Support Methods

        public void OpenWindow(IWindowManager windowManager, IScreen optionExtraViewModel, string title, int? width = null, int? height = null)
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.Manual;

            settings.Width = width ?? 640;

            settings.Height = height ?? 480;
            settings.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            optionExtraViewModel.DisplayName = title;
            windowManager.ShowDialog(optionExtraViewModel, null, settings);
        }


        public void Async(Action action, Action final=null)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += (o, ea) =>
            {
                action();
            };
            worker.RunWorkerCompleted += (o, ea) =>
            {
                IsBusy = false;
                if (final != null)
                    final();
            };
            IsBusy = true;
            worker.RunWorkerAsync();
        }


        public void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("WPFViewModel:[Execute] {0}-{1}", ex.Message, ex.StackTrace);
            }
        }

        #endregion

        #endregion
    }
}