using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using Takeaway.Service.Contract.Entities.Print;

namespace Takeaway.Admin.Wpf.Utility
{
    public abstract class PrintUtilBase
    {
        public WsPrintRequest PrintRequest { get; set; }
        public WsPrintSetting PrintSetting { get; set; }

        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public int Padding { get; set; }

        public Font DrawFontArialSuperLargeRegular { get; set; }
        public Font DrawFontArialSuperLargeBold { get; set; }
        public Font DrawFontArialLargeBold { get; set; }
        public Font DrawFontArialLargeRegular { get; set; }

        public Font DrawFontArialMediumBold { get; set; }
        public Font DrawFontArialMediumRegular { get; set; }
        public Font DrawFontArialSmallRegular { get; set; }

        public SolidBrush DrawBrush { get; set; }

        public Pen DrawLinePen2 { get; set; }
        public Pen DrawLinePen3 { get; set; }

        public Pen DrawLinePen1 { get; set; }

        public StringFormat DrawFormatCenter { get; set; }
        public StringFormat DrawFormatLeft { get; set; }

        public StringFormat DrawFormatRight { get; set; }



        public WsPrintResponse Print()
        {

            var response = new WsPrintResponse();

            try
            {
                var recordDoc = new PrintDocument { DocumentName = PrintRequest.DocumentName };
                recordDoc.PrintPage += DoPrintAction; // function below
                recordDoc.PrintController = new StandardPrintController(); // hides status dialog popup
                var ps = new PrinterSettings { PrinterName = PrintRequest.PrinterName };
                recordDoc.PrinterSettings = ps;
                recordDoc.Print();
                response.PrintSucceed = true;

            }
            catch (Exception ex)
            {
                response.PrintSucceed = true;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public void InitPrintingSettings()
        {
            X = PrintSetting.PrinterXflow;
            Y = PrintSetting.PrinterYflow;
            Width = PrintSetting.PrinterReceiptWidth; // max width I found through trial and error
            Height = 0F;
            Padding = 40;

            DrawFontArialSuperLargeRegular = new Font(PrintSetting.PrinterFontName, PrintSetting.PrinterLargeFontSize + 3, FontStyle.Regular);
            DrawFontArialSuperLargeBold = new Font(PrintSetting.PrinterFontName, PrintSetting.PrinterLargeFontSize + 3, FontStyle.Bold);
            DrawFontArialLargeBold = new Font(PrintSetting.PrinterFontName, PrintSetting.PrinterLargeFontSize, FontStyle.Bold);
            DrawFontArialLargeRegular = new Font(PrintSetting.PrinterFontName, PrintSetting.PrinterLargeFontSize, FontStyle.Regular);
            DrawFontArialMediumRegular = new Font(PrintSetting.PrinterFontName, PrintSetting.PrinterMediumFontSize, FontStyle.Regular);
            DrawFontArialMediumBold = new Font(PrintSetting.PrinterFontName, PrintSetting.PrinterMediumFontSize, FontStyle.Bold);
            DrawFontArialSmallRegular = new Font(PrintSetting.PrinterFontName, PrintSetting.PrinterSmallFontSize, FontStyle.Regular);
            DrawBrush = new SolidBrush(Color.Black);
            DrawLinePen2 = new Pen(Color.Black, 2);
            DrawLinePen1 = new Pen(Color.Black, 1);
            DrawLinePen1.DashStyle = DashStyle.Custom;
            DrawLinePen1.DashPattern = new float[] { 1, 1 };
            DrawFormatCenter = new StringFormat { Alignment = StringAlignment.Center };
            DrawFormatLeft = new StringFormat { Alignment = StringAlignment.Near };
            DrawFormatRight = new StringFormat { Alignment = StringAlignment.Far };
            DrawLinePen3= new Pen(Color.Black, 1);
            DrawLinePen3.DashStyle = DashStyle.Custom;
            DrawLinePen3.DashPattern = new float[] { 3, 3 };

            

        }

        public void DrawString(PrintPageEventArgs e, string text, Font font, StringFormat format, bool doAddNewLine = false, bool doPadding = false, bool noIncreaseY = false, int? subItem=null)
        {

            var padWidth = Width; //Setting padding width to Full Width
            var subX = X;


            if (subItem.HasValue)
            {
                padWidth = Width - subItem.Value*15; //If this is sub item then pad with 20
                subX = X + subItem.Value * 15;
            }
                

            if (doPadding)
                padWidth -= Padding;

            e.Graphics.DrawString(text, font, DrawBrush, new RectangleF(subX, Y, padWidth, Height), format);
            if (!noIncreaseY)
                Y += e.Graphics.MeasureString(text, font).Height;



            for (var i = 1; i <= 5; i++)
            {
                //Add Line If Required
                if (e.Graphics.MeasureString(text, font).Width >= padWidth*i)
                    Y += e.Graphics.MeasureString(text, font).Height;
            }

            if (doAddNewLine)
            {
                Y += e.Graphics.MeasureString(text, font).Height;
            }

        }

        public void DrawLine(PrintPageEventArgs e, Pen pen)
        {
            e.Graphics.DrawLine(pen, X, Y, Width, Y);
        }

        public void DrawOrderHeader(PrintPageEventArgs e)
        {
            //Shop Name
            DrawString(e, PrintRequest.ShopInfo.ShopName, DrawFontArialSuperLargeBold, DrawFormatCenter);

            //Shop Number
            DrawString(e, string.Format("Tel: {0}", PrintRequest.ShopInfo.ShopNumber), DrawFontArialMediumRegular, DrawFormatLeft);

            //Shop Address1
            DrawString(e, string.Format("{0},{1}", PrintRequest.ShopInfo.ShopAddress1, PrintRequest.ShopInfo.ShopAddress2), DrawFontArialSmallRegular, DrawFormatLeft);


            //Separator
            DrawLine(e, DrawLinePen2);
            DrawString(e, " ", DrawFontArialSmallRegular, DrawFormatCenter);

        }



        public abstract void DoPrintAction(object sender, PrintPageEventArgs e);

    }
}