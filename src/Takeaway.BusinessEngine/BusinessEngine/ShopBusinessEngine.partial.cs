using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common.Resources;
using Core.Common.Utility;
using Core.Common.Utility.Extension;
using Takeaway.Data.Entities.Enum;
using Takeaway.Data.Entities.Settings;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.BusinessEngine.BusinessEngine
{
    public partial class ShopBusinessEngine
    {

        public bool CalculateIsOpen(IEnumerable<ShopGeneralHour> shopGeneralHours, List<ShopSpecialHour> shopHourSpecial, DateTime currentTime)
        {

            var isOpen = false;

            //Date of the week to look
            var pad = 0;
            var todayDayOfWeekString = CalculateDateOfWeekString(currentTime, out pad);

            var currentDateTimeValue = CalculateCurrentTick(todayDayOfWeekString, pad, currentTime);

            var shopHoursforToday = shopGeneralHours.FirstOrDefault(r => r.DateOfWeek == todayDayOfWeekString);

            if (currentDateTimeValue >= shopHoursforToday.StartingTime.Value.Ticks
                && currentDateTimeValue <= shopHoursforToday.EndingTime.Value.Ticks)
            {
                isOpen = true;
            }

            if (!isOpen)
                return false;


            foreach (var specialDayForToday in shopHourSpecial)
            {
                if (IsInclusive(specialDayForToday.StartingTime.Value, specialDayForToday.EndingTime.Value, currentTime))
                    isOpen = false;
            }

            return isOpen;

        }

        public List<string> GetCollectionOptions(IEnumerable<ShopGeneralHour> shopGeneralHours)
        {
            var list = new List<string>();
            var currentTime = DateTimeUtil.GetCurrentTime();
            var pad = 0;
            var todayDayOfWeekString = CalculateDateOfWeekString(currentTime, out pad);
            var currentDateTimeValue = CalculateCurrentDay(todayDayOfWeekString, pad, currentTime);
            var shopHoursforToday = shopGeneralHours.FirstOrDefault(r => r.DateOfWeek == todayDayOfWeekString);

            currentDateTimeValue = currentDateTimeValue.AddMinutes(40);

            do
            {
                list.Add(currentDateTimeValue.ToString("hh:mm tt").ToUpper());
                currentDateTimeValue = currentDateTimeValue.AddMinutes(15);
            } while (currentDateTimeValue < shopHoursforToday.EndingTime.Value);
            return list;

        }


        private bool IsDeliveryOpen(ICollection<ShopGeneralHour> shopGeneralHours, DateTime currentTime)
        {
            var isOpen = false;

            //Date of the week to look
            var pad = 0;
            var todayDayOfWeekString = CalculateDateOfWeekString(currentTime, out pad);

            var currentDateTimeValue = CalculateCurrentTick(todayDayOfWeekString, pad, currentTime);

            var shopHoursforToday = shopGeneralHours.FirstOrDefault(r => r.DateOfWeek == todayDayOfWeekString);

            if (currentDateTimeValue >= shopHoursforToday.DeliveryStartingTime.Value.Ticks
                && currentDateTimeValue <= shopHoursforToday.EndingTime.Value.Ticks)
            {
                isOpen = true;
            }

            if (!isOpen)
                return false;

            return isOpen;
        }


        private DateTime CalculateCurrentDay(string todayDayOfWeekString, int pad, DateTime currentTime)
        {
            var compareDate = DateTime.MinValue;

            if (todayDayOfWeekString.Equals(DayOfWeek.Monday.ToString()))
            {

                compareDate = new DateTime(2000, 1, 1, currentTime.Hour, currentTime.Minute, currentTime.Second).AddDays(pad);
            }
            else if (todayDayOfWeekString.Equals(DayOfWeek.Tuesday.ToString()))
            {
                compareDate = new DateTime(2000, 1, 2, currentTime.Hour, currentTime.Minute, currentTime.Second).AddDays(pad);
            }
            else if (todayDayOfWeekString.Equals(DayOfWeek.Wednesday.ToString()))
            {
                compareDate = new DateTime(2000, 1, 3, currentTime.Hour, currentTime.Minute, currentTime.Second).AddDays(pad);
            }
            else if (todayDayOfWeekString.Equals(DayOfWeek.Thursday.ToString()))
            {
                compareDate = new DateTime(2000, 1, 4, currentTime.Hour, currentTime.Minute, currentTime.Second).AddDays(pad);
            }
            else if (todayDayOfWeekString.Equals(DayOfWeek.Friday.ToString()))
            {
                compareDate = new DateTime(2000, 1, 5, currentTime.Hour, currentTime.Minute, currentTime.Second).AddDays(pad);
            }
            else if (todayDayOfWeekString.Equals(DayOfWeek.Saturday.ToString()))
            {
                compareDate = new DateTime(2000, 1, 6, currentTime.Hour, currentTime.Minute, currentTime.Second).AddDays(pad);
            }
            else if (todayDayOfWeekString.Equals(DayOfWeek.Sunday.ToString()))
            {
                compareDate = new DateTime(2000, 1, 7, currentTime.Hour, currentTime.Minute, currentTime.Second).AddDays(pad);
            }
            return compareDate;
        }



        private long CalculateCurrentTick(string todayDayOfWeekString, int pad, DateTime currentTime)
        {
            var compareDate = DateTime.MinValue;

            if (todayDayOfWeekString.Equals(DayOfWeek.Monday.ToString()))
            {

                compareDate = new DateTime(2000, 1, 1, currentTime.Hour, currentTime.Minute, currentTime.Second).AddDays(pad);
            }
            else if (todayDayOfWeekString.Equals(DayOfWeek.Tuesday.ToString()))
            {
                compareDate = new DateTime(2000, 1, 2, currentTime.Hour, currentTime.Minute, currentTime.Second).AddDays(pad);
            }
            else if (todayDayOfWeekString.Equals(DayOfWeek.Wednesday.ToString()))
            {
                compareDate = new DateTime(2000, 1, 3, currentTime.Hour, currentTime.Minute, currentTime.Second).AddDays(pad);
            }
            else if (todayDayOfWeekString.Equals(DayOfWeek.Thursday.ToString()))
            {
                compareDate = new DateTime(2000, 1, 4, currentTime.Hour, currentTime.Minute, currentTime.Second).AddDays(pad);
            }
            else if (todayDayOfWeekString.Equals(DayOfWeek.Friday.ToString()))
            {
                compareDate = new DateTime(2000, 1, 5, currentTime.Hour, currentTime.Minute, currentTime.Second).AddDays(pad);
            }
            else if (todayDayOfWeekString.Equals(DayOfWeek.Saturday.ToString()))
            {
                compareDate = new DateTime(2000, 1, 6, currentTime.Hour, currentTime.Minute, currentTime.Second).AddDays(pad);
            }
            else if (todayDayOfWeekString.Equals(DayOfWeek.Sunday.ToString()))
            {
                compareDate = new DateTime(2000, 1, 7, currentTime.Hour, currentTime.Minute, currentTime.Second).AddDays(pad);
            }
            return compareDate.Ticks;
        }

        private string CalculateDateOfWeekString(DateTime currentDate, out int pad)
        {

            if (currentDate.Hour < 7)
            {
                pad = 1;
                return currentDate.AddDays(-1).DayOfWeek.ToString();
            }

            pad = 0;
            return currentDate.DayOfWeek.ToString();
        }


        private bool IsInclusive(DateTime startingTime, DateTime endingTime, DateTime compareDate)
        {
            return compareDate >= startingTime && compareDate <= endingTime;
        }


        private bool GetEmailSettingsSmtpEnabled(List<ServerSetting> systemSetting)
        {
            var settingValue = GetSettingByKey(systemSetting, ServerSettingEnum.AppSmtpenabled);
            return BooleanStringUtil.ConvertStringValue(settingValue);
        }

        private string GetEmailSettingsSmtpUserName(List<ServerSetting> systemSetting)
        {
            return GetSettingByKey(systemSetting, ServerSettingEnum.AppSmtpUsername);
        }

        private string GetEmailSettingsSmtpPassword(List<ServerSetting> systemSetting)
        {
            return GetSettingByKey(systemSetting, ServerSettingEnum.AppSmtppassword);
        }

        private string GetEmailSettingsSmtpServer(List<ServerSetting> systemSetting)
        {
            return GetSettingByKey(systemSetting, ServerSettingEnum.AppSmtpServer);
        }


        private string BuildClientEmailContent(Order orderTransformed, ShopInfo shopInfo)
        {
            var bodyHtml = ContentResource.OrderClientConfirmation_HtmlBody;
            bodyHtml = bodyHtml.Replace("{{0}}", shopInfo.Name);
            bodyHtml = bodyHtml.Replace("{{1}}", orderTransformed.OrderRef);
            bodyHtml = bodyHtml.Replace("{{2}}", orderTransformed.ContactName);
            bodyHtml = bodyHtml.Replace("{{3}}", orderTransformed.PhoneNumber);
            bodyHtml = bodyHtml.Replace("{{4}}", orderTransformed.Address);
            bodyHtml = bodyHtml.Replace("{{5}}", orderTransformed.Created.ToDateTimeUtcFormat());
            bodyHtml = bodyHtml.Replace("{{6}}", BuildOrderItem(orderTransformed));
            bodyHtml = bodyHtml.Replace("{{7}}", BuildDeliveryOrCollection(orderTransformed));
            bodyHtml = bodyHtml.Replace("{{8}}", BuildPaidOrLater(orderTransformed));
            bodyHtml = bodyHtml.Replace("{{9}}", orderTransformed.OrderTotal.ToPriceStringIe());
            bodyHtml = bodyHtml.Replace("{{10}}", orderTransformed.DeliveryCharge.ToPriceStringIe());
            bodyHtml = bodyHtml.Replace("{{11}}", BuildTotalCost(orderTransformed));
            bodyHtml = bodyHtml.Replace("{{12}}", shopInfo.ShopNumber);
            bodyHtml = bodyHtml.Replace("{{13}}", shopInfo.Address1);
            bodyHtml = bodyHtml.Replace("{{14}}", BuildDiscountHeader(orderTransformed));
            bodyHtml = bodyHtml.Replace("{{15}}", BuildDiscountValue(orderTransformed));


            return bodyHtml;
        }

        private string BuildDiscountValue(Order orderTransformed)
        {
            return orderTransformed.OrderOriginalTotal > orderTransformed.OrderTotal ? (orderTransformed.OrderOriginalTotal - orderTransformed.OrderTotal).ToPriceStringIe() : string.Empty;
        }

        private string BuildDiscountHeader(Order orderTransformed)
        {
            return orderTransformed.OrderOriginalTotal > orderTransformed.OrderTotal ? "Discount" : string.Empty;
        }

        private string BuildTotalCost(Order orderTransformed)
        {
            return (orderTransformed.OrderTotal + orderTransformed.DeliveryCharge).ToPriceStringIe();
        }

        private string BuildPaidOrLater(Order orderTransformed)
        {

            if (string.IsNullOrEmpty(orderTransformed.PaymentRef))
            {
                return "Pay Later";

            }
            else if (orderTransformed.PaymentRef.ToUpper() == ("Pay Later").ToUpper())
            {
                return "Paid Online with Loyalty";
            }
            else
            {
                return "Paid Online";
            }
        }


        private string BuildDeliveryOrCollection(Order orderTransformed)
        {
            return orderTransformed.DeliveryOptionId == 1 ? "For Delivery" : "For Collection";
        }

        private string BuildOrderItem(Order orderTransformed)
        {

            var orderItemBuilder = new StringBuilder();

            foreach (var orderitem in orderTransformed.OrderItems)
            {
                orderItemBuilder.AppendLine("<tr><td class=\"item-col item\"> ");
                orderItemBuilder.AppendFormat("<b>{0}</b><br>", orderitem.MenuItem.ItemName);
                orderItemBuilder.AppendLine(BuildSubItemSelection(orderitem));
                orderItemBuilder.AppendFormat(" {0}</td >", orderitem.Comment);


                orderItemBuilder.AppendLine("<td class=\"item-col quantity\">1</td>");
                orderItemBuilder.AppendFormat("<td class=\"item-col\">{0}</td>{1}", orderitem.MenuItem.UnitPrice.ToPriceStringIe(), Environment.NewLine);
                orderItemBuilder.AppendLine("</tr>");


            }

            return orderItemBuilder.ToString();
        }

        private string BuildSubItemSelection(OrderItem orderitem)
        {
            var builder = new System.Text.StringBuilder();
            //foreach (var subOrder in orderitem.OrderItemsSelections)
            //{
            //    builder.AppendLine(string.Format("+ {0}<br>", subOrder.OrderItemSelectedItem.ItemName));
            //}

            return builder.ToString();
        }

        private string BuildAdminEmailContent(Order orderTransformed, ShopInfo shopInfo)
        {
            var bodyHtml = ContentResource.OrderConfirmation_HtmlBody;
            bodyHtml = bodyHtml.Replace("{{0}}", shopInfo.Name);
            bodyHtml = bodyHtml.Replace("{{1}}", orderTransformed.OrderRef);
            bodyHtml = bodyHtml.Replace("{{2}}", orderTransformed.ContactName);
            bodyHtml = bodyHtml.Replace("{{3}}", orderTransformed.PhoneNumber);
            bodyHtml = bodyHtml.Replace("{{4}}", orderTransformed.Address);
            bodyHtml = bodyHtml.Replace("{{5}}", orderTransformed.Created.ToDateTimeUtcFormat());
            bodyHtml = bodyHtml.Replace("{{6}}", BuildOrderItem(orderTransformed));
            bodyHtml = bodyHtml.Replace("{{7}}", BuildDeliveryOrCollection(orderTransformed));
            bodyHtml = bodyHtml.Replace("{{8}}", BuildPaidOrLater(orderTransformed));
            bodyHtml = bodyHtml.Replace("{{9}}", orderTransformed.OrderTotal.ToPriceStringIe());
            bodyHtml = bodyHtml.Replace("{{10}}", orderTransformed.DeliveryCharge.ToPriceStringIe());
            bodyHtml = bodyHtml.Replace("{{11}}", BuildTotalCost(orderTransformed));
            bodyHtml = bodyHtml.Replace("{{12}}", shopInfo.ShopNumber);
            bodyHtml = bodyHtml.Replace("{{13}}", shopInfo.Address1);
            bodyHtml = bodyHtml.Replace("{{14}}", BuildDiscountHeader(orderTransformed));
            bodyHtml = bodyHtml.Replace("{{15}}", BuildDiscountValue(orderTransformed));

            return bodyHtml;


        }
    }
}

