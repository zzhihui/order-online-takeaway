using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Print;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;
using Takeway.Mobile.Core.Utilities;
using Core.Common.Utility;

namespace Takeaway.Admin.Wpf.ViewModels
{
    public interface IOrderDetailViewModel : IScreen
    {
        WsOrder SelectedOrder { get; set; }
    }


    [OfflineViewModel]
    public class OrderDetailViewModel: BaseViewModel, IOrderDetailViewModel
    {
        public WsOrder SelectedOrder { get; set; }
        private readonly IAppScope _appScope;

        public OrderDetailViewModel(IAppScope appScope)
        {
            _appScope = appScope;
        }

        public void DoPrintOrder()
        {
            PrinterQueue.PrintOrder(SelectedOrder, GetPrinterSettings(_appScope.Settings), _appScope.ShopInfo, "Takeaway Receipt", WsPrintModeEnum.CloudRePrint, 0, 1);
        }


        public void DoClose()
        {
            this.TryClose();
        }

        protected List<WsPrintSetting> GetPrinterSettings(List<WsSetting> settings)
        {
            var printerSettings = new List<WsPrintSetting>();

            for (var i = 1; i <= Constants.SupportPrinterNumber; i++)
            {
                printerSettings.Add(LoadIndividualPrinterSettings(settings, i));
            }

            return printerSettings;
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
    }
}