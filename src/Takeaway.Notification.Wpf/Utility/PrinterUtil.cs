using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows;
using System.Windows.Resources;
using Core.Common.Utility.Extension;
using Takeaway.Admin.Util.Utilities;
using Takeaway.Service.Contract.Entities.Print;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;

namespace Takeaway.Admin.Wpf.Utility
{

    public interface IPrinterUtil
    {
        WsPrintRequest PrintRequest { get; set; }
        WsPrintResponse Print();

    }


    public class PrinterUtil : PrintUtilBase, IPrinterUtil
    {

        public PrinterUtil()
        {
            PrintRequest = new WsPrintRequest();
            PrintSetting = new WsPrintSetting();

        }




        public override void DoPrintAction(object sender, PrintPageEventArgs e)
        {
            InitPrintingSettings();

            DrawOrderHeader(e);
            DrawOrderInfo(e);

            DrawLine(e, DrawLinePen1);

            DrawOrderDetail(e);

            DrawOrderFooter(e);

        }

        private void DrawOrderInfo(PrintPageEventArgs e)
        {

            //Print Mode
            if (!string.IsNullOrEmpty(PrintRequest.PrintMode))
            {
                DrawString(e, string.Format("**{0}***", PrintRequest.PrintMode), DrawFontArialMediumBold, DrawFormatLeft);
            }

            //Order Ref
            DrawString(e, string.Format("Ref: {0}", PrintRequest.Order.OrderRef), DrawFontArialMediumBold, DrawFormatLeft, true);

        }






        private void DrawOrderFooter(PrintPageEventArgs e)
        {

            //TimeStamp
            DrawString(e, DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"), DrawFontArialMediumRegular, DrawFormatLeft, true);
            DrawLine(e, DrawLinePen2);

            //Line Space
            DrawString(e, " ", DrawFontArialMediumRegular, DrawFormatLeft);

            //Shop Website
            DrawString(e, ConfigurationManager.AppSettings["ShopSiteUrl"], DrawFontArialMediumRegular, DrawFormatCenter);

            //Term and condition
            DrawString(e, string.Format("Additonal Terms and condition, please see our website. "), DrawFontArialSmallRegular, DrawFormatLeft);

            //Line Space
            DrawString(e, " ", DrawFontArialMediumRegular, DrawFormatLeft);

        }


        private void DrawOrderDetail(PrintPageEventArgs e)
        {
            var orderItems = PrintRequest.BatchId == 0 ? PrintRequest.Order.OrderItems : PrintRequest.Order.OrderItems.Where(r => r.BatchId == PrintRequest.BatchId).ToList();

            if (PrintSetting.PrintWithGroup)
            {
                DrawOrdeItemsWithGrouping(e, orderItems);
            }
            else
            {
                DrawOrdeItemsWithOutGrouping(e, orderItems);
            }


            DrawOrderSummary(e);

            //Line Space
            DrawString(e, " ", DrawFontArialSmallRegular, DrawFormatCenter);

            //Table Number
            DrawTableNumber(e);

            //Table Command
            DrawTableCommand(e);

            DrawOrderContact(e);
        }

        private void DrawOrdeItemsWithOutGrouping(PrintPageEventArgs e, List<WsMenuItem> orderItems)
        {

            //Order Information
            foreach (var orderItem in orderItems)
            {

                switch (PrintSetting.PrintLanguage)
                {
                    case (int)TranslateType.Both:
                        DrawString(e, orderItem.Name, DrawFontArialLargeRegular, DrawFormatLeft, false, true);
                        break;
                    case (int)TranslateType.Chinese:
                        DrawString(e, orderItem.TranslatedName, DrawFontArialSuperLargeRegular, DrawFormatLeft, false, true);
                        break;

                    default:
                        DrawString(e, orderItem.Name, DrawFontArialLargeRegular, DrawFormatLeft, false, true);
                        break;
                }


                DrawString(e, " ", DrawFontArialSmallRegular, DrawFormatCenter);

                //SubItem Selections
                foreach (var orderSubItem in orderItem.SubItemSelections)
                {
                    if (orderSubItem.Selected == null)
                        continue;

                    var subItemText = string.Format("-{0}", Translate(orderSubItem.Selected));
                    DrawString(e, subItemText, DrawFontArialMediumRegular, DrawFormatLeft, false, false, false, 1);


                    if (!string.IsNullOrEmpty(orderSubItem.Selected.Comment))
                        DrawString(e, string.Format("  {0}", orderSubItem.Selected.Comment), DrawFontArialMediumRegular, DrawFormatLeft, false, false, false, 1);
                }

                if (string.IsNullOrEmpty(orderItem.Comment))
                {
                    //Line Space
                    DrawLine(e, DrawLinePen1);
                    continue;
                }


                //Draw Line
                DrawLine(e, DrawLinePen1);

            }

        }

        private string Translate(WsMenuItem item)
        {
            switch (PrintSetting.PrintLanguage)
            {
                case (int)TranslateType.Both:
                    return string.Format("{1}. {0}", item.Name, item.TranslatedName);
                    break;
                case (int)TranslateType.Chinese:
                    return item.TranslatedName;
                    break;

                default:
                    return item.Name;
                    break;
            }

        }

        private void DrawOrdeItemsWithGrouping(PrintPageEventArgs e, List<WsMenuItem> orderItems)
        {

            var nonGroupItems = orderItems.Where(r => (r.HasSubOrder.HasValue && r.HasSubOrder.Value) || !string.IsNullOrEmpty(r.Comment)).ToList();
            var groupItems = orderItems.Where(r => (!r.HasSubOrder.HasValue || !r.HasSubOrder.Value) && string.IsNullOrEmpty(r.Comment)).ToList();

            var ordergroupItems = (groupItems.GroupBy(g => g.Id)
                .Select(group =>

                    new WsPrintGroupItem
                    {
                        Count = group.Count(),
                        Name = group.FirstOrDefault().Name,
                        TranslatedName = group.FirstOrDefault().TranslatedName,

                        MenuItems = group.ToList(),
                        ValuePart = group.Sum(i => i.Price).ToPriceStringIe(),
                        SortOrder = group.Select(i => i.FoodCategoryId).FirstOrDefault()
                    }
                    )
                .ToList());

            var nonGroupordergroupItems = nonGroupItems.Select(item => new WsPrintGroupItem()
            {
                Count = 1,
                Name = item.Name,
                TranslatedName = item.TranslatedName,
                ValuePart = item.Price.ToPriceStringIe(),
                SortOrder = item.FoodCategoryId,
                MenuItems = new List<WsMenuItem> { item }
            }).ToList();


            var orderPrintItems = ordergroupItems;
            orderPrintItems.AddRange(nonGroupordergroupItems);
            orderPrintItems = orderPrintItems.OrderBy(r => r.SortOrder).OrderBy(r => r.Name).ToList();

            DrawLine(e, DrawLinePen2);
            DrawOrderItemGroup(e, orderPrintItems);

            //var orderPrintItemsStarter = orderPrintItems.Where(r => r.SortOrder == 1).ToList();
            //if (orderPrintItemsStarter.Any())
            //{
            //    DrawLine(e, DrawLinePen2);
            //    DrawString(e, "====STARTER====", DrawFontArialSuperLargeRegular, DrawFormatCenter, false, true);
            //    DrawLine(e, DrawLinePen2);
            //    DrawOrderItemGroup(e, orderPrintItemsStarter);
            //}


            //var orderPrintItemsMain = orderPrintItems.Where(r => r.SortOrder == 2).ToList();
            //if (orderPrintItemsMain.Any())
            //{
            //    DrawLine(e, DrawLinePen2);
            //    DrawString(e, "====MAIN====", DrawFontArialSuperLargeRegular, DrawFormatCenter, false, true);
            //    DrawLine(e, DrawLinePen2);
            //    DrawOrderItemGroup(e, orderPrintItemsMain);
            //}

            //var orderPrintItemsDesert = orderPrintItems.Where(r => r.SortOrder == 3).ToList();
            //if (orderPrintItemsDesert.Any())
            //{
            //    DrawLine(e, DrawLinePen2);
            //    DrawString(e, "====DESERT====", DrawFontArialSuperLargeRegular, DrawFormatCenter, false, true);
            //    DrawLine(e, DrawLinePen2);
            //    DrawOrderItemGroup(e, orderPrintItemsDesert);
            //}

            //var orderPrintItemsDrinks = orderPrintItems.Where(r => r.SortOrder == 4).ToList();
            //if (orderPrintItemsDrinks.Any())
            //{
            //    DrawLine(e, DrawLinePen2);
            //    DrawString(e, "====DRINKS====", DrawFontArialSuperLargeRegular, DrawFormatCenter, false, true);
            //    DrawLine(e, DrawLinePen2);
            //    DrawOrderItemGroup(e, orderPrintItemsDrinks);
            //}
            //var orderPrintItemOther = orderPrintItems.Where(r => r.SortOrder != 1 && r.SortOrder != 2 && r.SortOrder != 3 && r.SortOrder != 4).ToList();
            //if (orderPrintItemOther.Any())
            //{


            //    DrawLine(e, DrawLinePen2);
            //    DrawString(e, "====OTHERS====", DrawFontArialSuperLargeRegular, DrawFormatCenter, false, true);
            //    DrawLine(e, DrawLinePen2);
            //    DrawOrderItemGroup(e, orderPrintItemOther);
            //}

        }

        private void DrawOrderItemGroup(PrintPageEventArgs e, List<WsPrintGroupItem> orderPrintItems)
        {
            foreach (var items in orderPrintItems)
            {
                DrawString(e, items.ValuePart, DrawFontArialMediumRegular, DrawFormatRight, false, false, true);
                switch (PrintSetting.PrintLanguage)
                {
                    case (int)TranslateType.Both:
                        DrawString(e, string.Format("{1} X {0}", items.TranslatedName, items.Count), DrawFontArialSuperLargeRegular, DrawFormatLeft, false, true);
                        DrawString(e, items.Name, DrawFontArialLargeRegular, DrawFormatLeft, false, true);
                        break;
                    case (int)TranslateType.Chinese:
                        DrawString(e, string.Format("{1} X {0}", items.TranslatedName, items.Count), DrawFontArialSuperLargeRegular, DrawFormatLeft, false, true);
                        break;

                    default:
                        DrawString(e, string.Format("{1} X {0}", items.Name, items.Count), DrawFontArialMediumRegular, DrawFormatLeft, false, true);
                        break;
                }


                foreach (var idv in items.MenuItems)
                {

                    if ((idv.SubItemSelections == null || !HasSelection(idv.SubItemSelections)) && string.IsNullOrEmpty(idv.Comment))
                        continue;

                    DrawLine(e, DrawLinePen3);

                    if (idv.SubItemSelections != null)
                    {

                        foreach (var subOrder in idv.SubItemSelections)
                        {
                            if (subOrder.Selected != null)
                            {

                                DrawString(e, string.Format("-{0}", Translate(subOrder.Selected)), DrawFontArialMediumRegular, DrawFormatLeft, false, false, false, 1);
                                if (!string.IsNullOrEmpty(subOrder.Selected.Comment))
                                    DrawString(e, string.Format("  {0}", subOrder.Selected.Comment), DrawFontArialMediumRegular, DrawFormatLeft, false, false, false, 1);
                            }

                        }
                    }

                    if (!string.IsNullOrEmpty(idv.Comment) && !string.IsNullOrEmpty(idv.Comment.Trim()))
                        DrawString(e, string.Format("*{0}", idv.Comment), DrawFontArialMediumRegular, DrawFormatLeft, true, false, false, 1);
                }

                //DrawLine(e, DrawLinePen1);
            }
        }


        private bool HasSelection(List<WsMenuSubItem> subItemSelections)
        {
            return subItemSelections.Any(subOrder => subOrder.Selected != null);
        }

        private void DrawOrderSummary(PrintPageEventArgs e)
        {
            var sri = GetSteamResource();
            var newImage = Image.FromStream(sri.Stream);
            e.Graphics.DrawImage(newImage, new RectangleF(X, Y, 50, 50));



            if (!OrderIsEatIn())
            {
                //Order Total
                DrawString(e, string.Format("Order: {0}", PrintRequest.Order.OrderTotal.ToPriceStringIe()), DrawFontArialMediumBold, DrawFormatRight);

                //Delivery Type
                var deliveryTypetext = PrintRequest.Order.DeliveryType == null ? "" : PrintRequest.Order.DeliveryType.Name;
                DrawString(e, deliveryTypetext, DrawFontArialMediumRegular, DrawFormatRight);

                //Delivery Charge
                var deliveryChargetext = string.Format("Delivery Charge: {0}", PrintRequest.Order.DeliveryCharge.ToPriceStringIe());
                DrawString(e, deliveryChargetext, DrawFontArialMediumRegular, DrawFormatRight);
            }
            else
            {

                DrawString(e, " ", DrawFontArialMediumRegular, DrawFormatRight);
                DrawString(e, " ", DrawFontArialMediumRegular, DrawFormatRight);
                DrawString(e, " ", DrawFontArialMediumRegular, DrawFormatRight);

            }
            //Line Space
            DrawString(e, " ", DrawFontArialMediumRegular, DrawFormatRight);

            //Order Total
            var orderTotaltext = string.Format("Total: {0}", (PrintRequest.Order.OrderTotal + PrintRequest.Order.DeliveryCharge).ToPriceStringIe());
            DrawString(e, orderTotaltext, DrawFontArialSuperLargeBold, DrawFormatRight);

            //Paid not Paid
            if (!OrderIsEatIn() && string.IsNullOrEmpty(PrintRequest.Order.PaymentType))
            {

                var paymentStatusReftext = string.IsNullOrEmpty(PrintRequest.Order.PaymentRef) ? "Pay Later" : "Paid Online";
                DrawString(e, paymentStatusReftext, DrawFontArialMediumBold, DrawFormatRight);
                if (!string.IsNullOrEmpty(PrintRequest.Order.PaymentRef))
                {
                    var paymentReftext = string.Format("Payment Ref: {0}", PrintRequest.Order.PaymentRef);
                    DrawString(e, paymentReftext, DrawFontArialSmallRegular, DrawFormatRight);
                }
            }
            else if (!string.IsNullOrEmpty(PrintRequest.Order.PaymentType))
            {
                var paidtext = string.Format("Paid:{0}", PrintRequest.Order.PaymentType);
                DrawString(e, paidtext, DrawFontArialMediumBold, DrawFormatRight);
            }

        }

        private void DrawTableNumber(PrintPageEventArgs e)
        {
            if (!OrderIsEatIn()) return;

            var tableNumber = PrintRequest.Order.TableNumber ?? 0;
            var text = WsTableAliasUtil.GetTableNameById(tableNumber);
            if (PrintRequest.BatchId > 0)
            {
                text = string.Format("{0} ({1})", text, PrintRequest.BatchId);
            }

            DrawString(e, text, DrawFontArialLargeBold, DrawFormatLeft);
        }

        private void DrawTableCommand(PrintPageEventArgs e)
        {
            if (!string.IsNullOrEmpty(PrintRequest.Order.Comment))
                DrawString(e, string.Format("*{0} ", PrintRequest.Order.Comment), DrawFontArialMediumBold, DrawFormatLeft);

            foreach (var command in PrintRequest.Order.TableCommand)
            {
                DrawString(e, string.Format("*** {0} ***", command.ItemName), DrawFontArialLargeBold, DrawFormatLeft);
            }
        }


        private void DrawOrderContact(PrintPageEventArgs e)
        {

            //Name
            DrawString(e, PrintRequest.Order.ContactName, DrawFontArialMediumRegular, DrawFormatLeft);

            //Phone
            DrawString(e, PrintRequest.Order.PhoneNumber, DrawFontArialMediumRegular, DrawFormatLeft);

            //Address
            DrawString(e, string.Format("{0}", PrintRequest.Order.Address), DrawFontArialLargeRegular, DrawFormatLeft);

        }

        private StreamResourceInfo GetSteamResource()
        {
            if (PrintRequest.Order.DeliveryType == null)
            {
                return Application.GetResourceStream(new Uri("/Takeaway.Admin.Util;Component/Images/IndaSoftIconYellow.jpg", UriKind.Relative));
            }

            if (PrintRequest.Order.DeliveryType.Name == DeliveryOptions.ForDelivery)
            {
                return
                    Application.GetResourceStream(new Uri("/Takeaway.Admin.Util;Component/Images/CarIco.jpg",
                        UriKind.Relative));
            }
            else if (PrintRequest.Order.DeliveryType.Name == DeliveryOptions.ForCollection && PrintRequest.Order.IsWalkIn.HasValue && PrintRequest.Order.IsWalkIn.Value)
            {
                return
                    Application.GetResourceStream(new Uri("/Takeaway.Admin.Util;Component/Images/WalkinIco.jpg",
                        UriKind.Relative));
            }
            else if (PrintRequest.Order.DeliveryType.Name == DeliveryOptions.ForCollection)
            {
                return
                   Application.GetResourceStream(new Uri("/Takeaway.Admin.Util;Component/Images/CollectionIco.jpg",
                       UriKind.Relative));
            }
            else if (PrintRequest.Order.DeliveryType.Name == DeliveryOptions.ForEatIn)
            {
                return
                    Application.GetResourceStream(new Uri("/Takeaway.Admin.Util;Component/Images/EatinIco.jpg",
                        UriKind.Relative));
            }
            else
            {
                return
                    Application.GetResourceStream(new Uri(
                        "/Takeaway.Admin.Wpf;Component/Images/IndaSoftIconYellow.png", UriKind.Relative));
            }
        }



        private bool OrderIsEatIn()
        {
            return PrintRequest.Order.DeliveryType != null &&
                   PrintRequest.Order.DeliveryType.Name == DeliveryOptions.ForEatIn;
        }
    }
}