using System.Collections.Generic;
using Takeaway.Service.Contract.Entities.Print;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Admin.Wpf.Utility
{
    public class PrinterQueue
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="printSettings"></param>
        /// <param name="shopInfo"></param>
        /// <param name="documentName"></param>
        /// <param name="printMode">Mode= Preview etc</param>
        /// <param name="printWhichPrinter">0 means all, 1 means first only, 2 means backs only</param>
        public static void PrintOrder(WsOrder order, List<WsPrintSetting> printSettings, WsShop shopInfo, string documentName, string printMode = null,  int batchId=0,int printWhichPrinter = 0)
        {
            var counter = 0;

            foreach (var printerSetting in printSettings)
            {
                if (string.IsNullOrEmpty(printerSetting.PrinterName))
                    continue;

                counter++;
                if (printWhichPrinter >0  && counter > printWhichPrinter)
                    break;


                var printerUtil = new PrinterUtil
                {
                    PrintRequest = new WsPrintRequest()
                    {
                        PrinterName = printerSetting.PrinterName,
                        Order = order,
                        ShopInfo = shopInfo,
                        DocumentName = documentName,
                        BatchId = batchId,
                        PrintMode = printMode
                    },

                    PrintSetting = printerSetting

                };

                if (counter == 1)
                    printerUtil.PrintRequest.PrintMode = printMode + " For Counter";
                else if (counter == 2)
                    printerUtil.PrintRequest.PrintMode = printMode + " For Kitchen";


                printerUtil.Print();


            }

        }


        public static void PrintReport(List<WsPrintSetting> printSettings, WsShop shopInfo, List<WsPrintRawData> rawData, string documentName)
        {

            if (printSettings.Count == 1)
                return;


            var printerSetting = printSettings[0];

            var printerUtil = new PrintReportUtil()
            {
                PrintRequest = new WsPrintRequest()
                {
                    PrinterName = printerSetting.PrinterName,
                    RawDatas = rawData,
                    ShopInfo = shopInfo,
                    DocumentName = documentName
                },

                PrintSetting = printerSetting

            };

            printerUtil.Print();


        }
    }
}