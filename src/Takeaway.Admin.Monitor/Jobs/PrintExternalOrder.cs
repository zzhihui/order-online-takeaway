using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using Core.Common.Utility;
using log4net;
using Quartz;
using Takeaway.Admin.Service.Core;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Print;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;
using Takeway.Mobile.Core.Utilities;


namespace Takeaway.Admin.Service.Jobs
{
    
    [ProcessJob]
    [DisallowConcurrentExecution]
    public class PrintExternalOrder:IInterruptableJob, IBaseJob
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PrintExternalOrder).Name);
        volatile bool _stop;

        private readonly IOnlineService _onlineService;
        private readonly IOfflineShopService _offlineShopService;
        private readonly IAppScope _appScope;

        public PrintExternalOrder(IOnlineService onlineService, IOfflineShopService offlineShopService, IAppScope appScope)
        {
            _onlineService = onlineService;
            _offlineShopService = offlineShopService;
            _appScope = appScope;
        }


        public void Execute(IJobExecutionContext context)
        {

            Log.Info("Start Print Online Order Job ");            
            _stop = false;

            if (ConfigurationManager.AppSettings["FeaturePrintExternalOrderOn"] != "true")
            {
                Log.Info("[PrintExternalOrder]Job is turned off  ");            
            }
            else
            {
                
                CheckOnlineOrders();
                SyncMyStatus();
            }
       
            Log.Info("Finish PrintExternalOrder Job ");            
        }

        private void SyncMyStatus()
        {
            try
            {
                _onlineService.SyncStatus();
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("{0}-{1}", ex.Message, ex.StackTrace);
            }
        }


        private void CheckOnlineOrders()
        {

            try
            {
                Log.DebugFormat("[CheckOnlineOrders] Loading Server Setting");
                _appScope.Settings = _offlineShopService.GetSystemSettings();
                if (_appScope.Settings == null)
                    return;
                Log.DebugFormat("[CheckOnlineOrders] Loading Offline Setting");
                var offlineSettings = _offlineShopService.GetOfflineSettings();



                Log.DebugFormat("[CheckOnlineOrders] Loading Shop Info");
                _appScope.ShopInfo = _onlineService.GetShopInfo();
                if (_appScope.ShopInfo == null)
                    return;


                Log.DebugFormat("[CheckOnlineOrders] Getting Open Order");
                var order = _onlineService.GetOpenOrders();
                if (order == null || !order.Any())
                    return;

                Log.DebugFormat("[CheckOnlineOrders] Last Max Order Stored");
                var localMaxOrder = offlineSettings.FirstOrDefault(r => r.Name == WsOfflineSettingEnum.AppMaxOrder);
                var currentMax = 0;
                if(localMaxOrder!=null)
                currentMax = IntegerStringUtil.ConvertStringValue(localMaxOrder.SettingValue);


                Log.DebugFormat("[CheckOnlineOrders] Previous Max Order Id {0}", currentMax);
                
                var maxOrderId = order.Max(r => r.Id);
                Log.DebugFormat("[CheckOnlineOrders] maxOrderId {0}",maxOrderId);
                if (maxOrderId <= currentMax)
                    return; //No new order exists 
              
             
                //Auto Receipt
                Log.DebugFormat("[CheckOnlineOrders] Check if Auto Receipt");
                var autoReceipt = BooleanStringUtil.ConvertStringValue(_appScope.Settings.FirstOrDefault(r => r.Name == WsOfflineSettingEnum.AppAutoReceipt).SettingValue);

                if (autoReceipt)
                {
                    //Get New Orders
                    var newlyOrders = order.Where(r => r.Id > currentMax);

                    foreach (var newOrder in newlyOrders)
                    {
                        PrintReceipt(newOrder);
                    }
                }

                Log.DebugFormat("[CheckOnlineOrders] Save Max Order");
                _offlineShopService.SaveSettingByKey(WsOfflineSettingEnum.AppMaxOrder, maxOrderId.ToString());
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("{0}-{1}", ex.Message, ex.StackTrace);
            }

        }

        private void PrintReceipt(WsOrder newOrder)
        {
         
            Translate(newOrder.OrderItems);
            var printerSettings = GetPrinterSettings(_appScope.Settings);
            PrinterQueue.PrintOrder(newOrder, printerSettings, _appScope.ShopInfo, "Takeaway Receipt");
        
        }

        private List<WsPrintSetting> GetPrinterSettings(List<WsSetting> settings)
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



        private void Translate(IEnumerable<WsMenuItem> items)
        {
            var translate = GetTranslatedFlag(_appScope.Settings);

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
            }
        }

        protected int GetTranslatedFlag(List<WsSetting> settings)
        {
            var setting = GetSettingByKey(WsOfflineSettingEnum.AppUserTranslateInternal, settings);
            return setting == null ? 0 : IntegerStringUtil.ConvertStringValue(setting.SettingValue);
        }


        public WsSetting GetSettingByKey(string settingKey, List<WsSetting> systemSettings)
        {
            return systemSettings.FirstOrDefault(r => r.Name == settingKey);
        }


        public void Interrupt() { _stop = true; }
    }
}