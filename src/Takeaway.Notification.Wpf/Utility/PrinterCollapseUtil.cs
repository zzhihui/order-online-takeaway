using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Mime;
using System.Windows;
using System.Windows.Resources;
using Core.Common.Utility.Extension;
using Takeaway.Admin.Util.Utilities;
using Takeaway.Service.Contract.Entities.Print;
using Takeaway.Service.Contract.Enum;
using FontStyle = System.Drawing.FontStyle;

namespace Takeaway.Admin.Wpf.Utility
{

  


    public class PrinterCollapseUtil : IPrinterUtil
    {


        public WsPrintRequest PrintRequest { get; set; }
        public WsPrintSetting PrintSetting { get; set; }

        public PrinterCollapseUtil()
        {
            PrintRequest = new WsPrintRequest();
            PrintSetting = new WsPrintSetting();

        }

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

        private void DoPrintAction(object sender, PrintPageEventArgs e)
        {
            var x = PrintSetting.PrinterXflow;
            var y = PrintSetting.PrinterYflow;
            var width = PrintSetting.PrinterReceiptWidth; // max width I found through trial and error
            const float height = 0F;
            const int padding = 30;

            var drawFontArialSuperLargeRegular = new Font(PrintSetting.PrinterFontName, PrintSetting.PrinterLargeFontSize + 3, FontStyle.Regular);
            var drawFontArialSuperLargeBold = new Font(PrintSetting.PrinterFontName, PrintSetting.PrinterLargeFontSize + 3, FontStyle.Bold);
            var drawFontArialLargeBold = new Font(PrintSetting.PrinterFontName, PrintSetting.PrinterLargeFontSize, FontStyle.Bold);
            var drawFontArialLargeRegular = new Font(PrintSetting.PrinterFontName, PrintSetting.PrinterLargeFontSize, FontStyle.Regular);
            var drawFontArialMediumRegular = new Font(PrintSetting.PrinterFontName, PrintSetting.PrinterMediumFontSize, FontStyle.Regular);
            var drawFontArialMediumBold = new Font(PrintSetting.PrinterFontName, PrintSetting.PrinterMediumFontSize, FontStyle.Bold);
            var drawFontArialSmallRegular = new Font(PrintSetting.PrinterFontName, PrintSetting.PrinterSmallFontSize, FontStyle.Regular);
            var drawBrush = new SolidBrush(Color.Black);
            var drawLinePen2 = new Pen(Color.Black, 2);
            var drawLinePen1 = new Pen(Color.Black, 1);
            drawLinePen1.DashStyle = DashStyle.Custom;
            drawLinePen1.DashPattern = new float[] { 1, 1 };

            // Set format of string.
            var drawFormatCenter = new StringFormat { Alignment = StringAlignment.Center };
            var drawFormatLeft = new StringFormat { Alignment = StringAlignment.Near };
            var drawFormatRight = new StringFormat { Alignment = StringAlignment.Far };



            //Shop Name
            var text = PrintRequest.ShopInfo.ShopName;
            e.Graphics.DrawString(text, drawFontArialSuperLargeBold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArialSuperLargeBold).Height;

            //Shop Number
            text = string.Format("Tel: {0}", PrintRequest.ShopInfo.ShopNumber);
            e.Graphics.DrawString(text, drawFontArialMediumRegular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;

            //Shop Address1
            text = string.Format("{0},{1}", PrintRequest.ShopInfo.ShopAddress1, PrintRequest.ShopInfo.ShopAddress2);
            e.Graphics.DrawString(text, drawFontArialSmallRegular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;

            //Separator
            e.Graphics.DrawLine(drawLinePen2, x, y, width, y);
            text = " ";
            e.Graphics.DrawString(text, drawFontArialSmallRegular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;



            if (!string.IsNullOrEmpty(PrintRequest.PrintMode))
            {
                text = string.Format("**{0}**", PrintRequest.PrintMode);
                e.Graphics.DrawString(text, drawFontArialMediumBold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;
            }


            text = string.Format("Ref: {0}", PrintRequest.Order.OrderRef);
            e.Graphics.DrawString(text, drawFontArialMediumBold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;
            //Add Line If Required
            if (e.Graphics.MeasureString(text, drawFontArialMediumBold).Width >= width - padding)
                y += e.Graphics.MeasureString(text, drawFontArialMediumBold).Height;

            y += e.Graphics.MeasureString(text, drawFontArialMediumBold).Height;

            foreach (var dataItem in PrintRequest.RawDatas)
            {

                if (dataItem.RawDataFormat == (int)PrintRawDataFormat.ContentOnly || dataItem.RawDataFormat == 0)
                {
                    text = string.Format("{0} ", dataItem.TextPart);
                    e.Graphics.DrawString(text, drawFontArialLargeRegular, drawBrush, new RectangleF(x, y, width - padding, height), drawFormatLeft);

                }
                if (dataItem.RawDataFormat == (int)PrintRawDataFormat.ValueOnly || dataItem.RawDataFormat == 0)
                {
                    var text1 = string.Format("{0} ", dataItem.ValuePart);
                    e.Graphics.DrawString(text1, drawFontArialLargeRegular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                }

                if (e.Graphics.MeasureString(text, drawFontArialLargeRegular).Width >= width - padding)
                    y += e.Graphics.MeasureString(text, drawFontArialLargeRegular).Height;

                y += e.Graphics.MeasureString(text, drawFontArialLargeRegular).Height;

                //Spacing
                e.Graphics.DrawLine(drawLinePen1, x, y, width, y);
                text = " ";
                e.Graphics.DrawString(text, drawFontArialSmallRegular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                y += e.Graphics.MeasureString(text, drawFontArialSmallRegular).Height;

            }

           
            var sri = GetSteamResource();
            var newImage = Image.FromStream(sri.Stream);
            e.Graphics.DrawImage(newImage, new RectangleF(x, y, 80, 80));




            if (!OrderIsEatIn())
            {
                //Order Total
                text = string.Format("Order: {0}", PrintRequest.Order.OrderTotal.ToPriceStringIe());
                e.Graphics.DrawString(text, drawFontArialMediumBold, drawBrush, new RectangleF(x, y, width, height),
                    drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;

                //Delivery Type
                text = PrintRequest.Order.DeliveryType == null ? "" : PrintRequest.Order.DeliveryType.Name;
                e.Graphics.DrawString(text, drawFontArialMediumRegular, drawBrush, new RectangleF(x, y, width, height),
                    drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;

                text = string.Format("Delivery Charge: {0}", PrintRequest.Order.DeliveryCharge.ToPriceStringIe());
                e.Graphics.DrawString(text, drawFontArialMediumRegular, drawBrush, new RectangleF(x, y, width, height),
                    drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;
            }
            else
            {
                text = " ";
                e.Graphics.DrawString(text, drawFontArialMediumRegular, drawBrush, new RectangleF(x, y, width, height),
                    drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArialMediumRegular, drawBrush, new RectangleF(x, y, width, height),
                    drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArialMediumRegular, drawBrush, new RectangleF(x, y, width, height),
                    drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;
                
            }
            //Line Space
            text = " ";
            y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;

            //Order Total
            text = string.Format("Total: {0}", (PrintRequest.Order.OrderTotal + PrintRequest.Order.DeliveryCharge).ToPriceStringIe());
            e.Graphics.DrawString(text, drawFontArialMediumBold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;

            //Paid not Paid
            if (!OrderIsEatIn() && string.IsNullOrEmpty(PrintRequest.Order.PaymentType))
            {
                text = string.IsNullOrEmpty(PrintRequest.Order.PaymentRef) ? "Pay Later" : "Paid Online";
                e.Graphics.DrawString(text, drawFontArialMediumBold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArialMediumBold).Height;

                if (!string.IsNullOrEmpty(PrintRequest.Order.PaymentRef))
                {
                    text = string.Format("Payment Ref: {0}", PrintRequest.Order.PaymentRef);
                    e.Graphics.DrawString(text, drawFontArialSmallRegular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString(text, drawFontArialSmallRegular).Height;

                }
            }
            else if (!string.IsNullOrEmpty(PrintRequest.Order.PaymentType))
            {
                text = string.Format("Paid:{0}", PrintRequest.Order.PaymentType);
                e.Graphics.DrawString(text, drawFontArialMediumBold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(text, drawFontArialMediumBold).Height;
            }

            //Line Space
            text = " ";
            y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;
           
            //Table Number
            if (OrderIsEatIn())
            {
                var tableNumber = PrintRequest.Order.TableNumber ?? 0;
                text = WsTableAliasUtil.GetTableNameById(tableNumber);
                if (PrintRequest.BatchId > 0)
                {
                    text = string.Format("{0} ({1})", text, PrintRequest.BatchId);
                }
                e.Graphics.DrawString(text, drawFontArialLargeBold, drawBrush, new RectangleF(x, y, width, height),
                    drawFormatLeft);
                y += e.Graphics.MeasureString(text, drawFontArialLargeBold).Height;
            }

            //Table Command
            foreach (var command in PrintRequest.Order.TableCommand)
            {
                text = string.Format("*** {0} ***", command.ItemName);
                e.Graphics.DrawString(text, drawFontArialLargeBold, drawBrush, new RectangleF(x, y, width, height),
                    drawFormatLeft);
                y += e.Graphics.MeasureString(text, drawFontArialLargeBold).Height;
            }



            //Name
            text = PrintRequest.Order.ContactName;
            e.Graphics.DrawString(text, drawFontArialMediumRegular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;

            //Phone
            text = PrintRequest.Order.PhoneNumber;
            e.Graphics.DrawString(text, drawFontArialMediumRegular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;

            //Address
            text = string.Format("{0}", PrintRequest.Order.Address);
            e.Graphics.DrawString(text, drawFontArialLargeRegular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            y += e.Graphics.MeasureString(text, drawFontArialLargeRegular).Height;


            text = " ";
            e.Graphics.DrawString(text, drawFontArialMediumRegular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;

            text = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
            e.Graphics.DrawString(text, drawFontArialMediumRegular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;



            //Line Space
            text = " ";
            y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;

            e.Graphics.DrawLine(drawLinePen2, x, y, width, y);
            text = " ";
            e.Graphics.DrawString(text, drawFontArialSmallRegular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;

            text = ConfigurationManager.AppSettings["ShopSiteUrl"];
            e.Graphics.DrawString(text, drawFontArialMediumRegular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;


            //Term and condition
            text = string.Format("Additonal Terms and condition, please see our website. ");
            e.Graphics.DrawString(text, drawFontArialSmallRegular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            y += e.Graphics.MeasureString(text, drawFontArialMediumRegular).Height;

            

           


        }

        private  StreamResourceInfo GetSteamResource()
        {
            if (PrintRequest.Order.DeliveryType == null)
            {
                return Application.GetResourceStream(new Uri("/Takeaway.Admin.Wpf;Component/Images/IndaSoftIconYellow.jpg", UriKind.Relative));
            }

            if (PrintRequest.Order.DeliveryType.Name == DeliveryOptions.ForDelivery)
            {
                return
                    Application.GetResourceStream(new Uri("/Takeaway.Admin.Wpf;Component/Images/CarIco.jpg",
                        UriKind.Relative));
            }
            else if (PrintRequest.Order.DeliveryType.Name == DeliveryOptions.ForCollection && PrintRequest.Order.IsWalkIn.HasValue && PrintRequest.Order.IsWalkIn.Value)
            {
                return
                    Application.GetResourceStream(new Uri("/Takeaway.Admin.Wpf;Component/Images/WalkinIco.jpg",
                        UriKind.Relative));
            }
            else if (PrintRequest.Order.DeliveryType.Name == DeliveryOptions.ForCollection)
            {
                return
                   Application.GetResourceStream(new Uri("/Takeaway.Admin.Wpf;Component/Images/CollectionIco.jpg",
                       UriKind.Relative));
            }
            else if (PrintRequest.Order.DeliveryType.Name == DeliveryOptions.ForEatIn)
            {
                return
                    Application.GetResourceStream(new Uri("/Takeaway.Admin.Wpf;Component/Images/EatinIco.jpg",
                        UriKind.Relative));
            }
            else
            {
                return
                    Application.GetResourceStream(new Uri(
                        "/Takeaway.Admin.Wpf;Component/Images/IndaSoftIconYellow.png", UriKind.Relative));
            }
            
        }

        private List<string> SplitLanguage(string name)
        {
            var list = name.Split(new string[] { ". " }, StringSplitOptions.RemoveEmptyEntries);
            return list.ToList();
        }

        private bool OrderIsEatIn()
        {
            return PrintRequest.Order.DeliveryType != null &&
                   PrintRequest.Order.DeliveryType.Name == DeliveryOptions.ForEatIn;
        }
    }
}