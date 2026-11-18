using System;
using System.Drawing;
using System.Drawing.Printing;
using Takeaway.Service.Contract.Entities.Print;
using Takeaway.Service.Contract.Enum;

namespace Takeaway.Admin.Wpf.Utility
{
    public interface IPrintReportUtil
    {

    }


    public class PrintReportUtil : PrintUtilBase,IPrintReportUtil
    {
  
        public PrintReportUtil()
        {
            PrintRequest = new WsPrintRequest();
            PrintSetting = new WsPrintSetting();

        }

        public override void DoPrintAction(object sender, PrintPageEventArgs e)
        {

            InitPrintingSettings();
            DrawOrderHeader(e);

            //Order Information
            foreach (var dataItem in PrintRequest.RawDatas)
            {

                if (dataItem.RawDataFormat == (int)PrintRawDataFormat.ContentOnly || dataItem.RawDataFormat == 0)
                {
                    var text = string.Format("{0} ", dataItem.TextPart);
                    DrawString(e, text, DrawFontArialMediumBold, DrawFormatLeft);

                }
                if (dataItem.RawDataFormat == (int)PrintRawDataFormat.ValueOnly || dataItem.RawDataFormat == 0)
                {
                    var text = string.Format("{0} ", dataItem.ValuePart);
                    DrawString(e, text, DrawFontArialMediumBold, DrawFormatRight);
                }

                DrawString(e, " ", DrawFontArialSmallRegular, DrawFormatCenter);
                DrawString(e, " ", DrawFontArialSmallRegular, DrawFormatCenter);

            }

            //Spacing
            DrawString(e, " ", DrawFontArialSmallRegular, DrawFormatCenter);
            DrawString(e, " ", DrawFontArialSmallRegular, DrawFormatCenter);


            DrawLine(e, DrawLinePen2);
            DrawString(e, "Additional Comment", DrawFontArialSmallRegular, DrawFormatCenter);
           
            for (int i = 0; i < 30; i++)
            {

                DrawString(e, "-", DrawFontArialSmallRegular, DrawFormatLeft);
                DrawString(e, "-", DrawFontArialSmallRegular, DrawFormatRight);

            }

            DrawLine(e, DrawLinePen2);

            

        }
    }
}