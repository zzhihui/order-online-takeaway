using System;
using System.Globalization;
using System.Text;

namespace Takeaway.Admin.Service.Utilities
{
    public static class StringExtensions
    {
        public static void AppendLine(this StringBuilder builder, string format, params object[] args)
        {
            builder.AppendFormat(format, args);
            builder.AppendLine();
        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool AsBoolean(this string value)
        {
            bool result;

            if (bool.TryParse(value, out result))
                return result;

            return false;
        }

        public static bool? AsNullableBoolean(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            return value.AsBoolean();
        }

        public static decimal AsDecimal(this string value)
        {
            decimal result;

            if (decimal.TryParse(value, out result))
                return result;

            return 0M;
        }

        // DateTime convert to string
        public static string AsFormattedString(this DateTime? value)
        {
            var result = string.Empty;
            if (value.HasValue)
            {
                result = value.Value.ToString("dd/MM/yyyy");
            }
            return result;
        }

        // DateTime convert from string
        public static DateTime? AsDateTime(this string value)
        {
            const string yearsOld = "YEARS OLD";
            const string yesterday = "YESTERDAY";
            const string today = "TODAY";
            const string tomorrow = "TOMORROW";

            //PMV Free Keycare
            const string startMonthPlus1 = "START_MONTH_PLUS_1";
            const string endMonthPlus1 = "END_MONTH_PLUS_1";
            const string startMonthPlus2 = "START_MONTH_PLUS_2";
            const string endMonthPlus2 = "END_MONTH_PLUS_2";
            const string startMonthPlus3 = "START_MONTH_PLUS_3";
            const string endMonthPlus3 = "END_MONTH_PLUS_3";
            const string startMonthPlus4 = "START_MONTH_PLUS_4";
            const string endMonthPlus4 = "END_MONTH_PLUS_4";
            const string startMonthPlus5 = "START_MONTH_PLUS_5";
            const string endMonthPlus5 = "END_MONTH_PLUS_5";

            if (string.IsNullOrWhiteSpace(value)) return null;

            var textValue = value.Trim().ToUpper();

            if (textValue.Contains(yearsOld))
            {
                var yearsValue = textValue.Replace(yearsOld, "").Trim();

                var years = 0;
                Int32.TryParse(yearsValue, out years);

                return DateTime.Today.AddYears(years * -1).AddDays(-1);
            }

            if (textValue == yesterday) return DateTime.Today.AddDays(-1);

            if (textValue == today) return DateTime.Today;

            if (textValue == tomorrow) return DateTime.Today.AddDays(1);

            if (textValue == startMonthPlus1)
                return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1);

            if (textValue == endMonthPlus1)
                return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(2).AddSeconds(-1);

            if (textValue == startMonthPlus2)
                return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(2);

            if (textValue == endMonthPlus2)
                return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(3).AddSeconds(-1);

            if (textValue == startMonthPlus3)
                return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(3);

            if (textValue == endMonthPlus3)
                return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(4).AddSeconds(-1);

            if (textValue == startMonthPlus4)
                return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(4);

            if (textValue == endMonthPlus4)
                return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(5).AddSeconds(-1);

            if (textValue == startMonthPlus5)
                return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(5);

            if (textValue == endMonthPlus5)
                return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(6).AddSeconds(-1);

            DateTime result;

            var dateFormats = new string[] { "d/M/yyyy", "dd/MM/yyyy", "d/MMM/yyyy" };

            if (!DateTime.TryParseExact(value, dateFormats, new CultureInfo("en-IE"), DateTimeStyles.None, out result))
            {
                return null;
            }
            return result;
        }

        public static int AsInt(this string value)
        {
            int result;

            if (int.TryParse(value, out result))
                return result;

            return 0;
        }

        public static bool FromYN(this string value)
        {
            return value.InternalEquals("Y");
        }

        public static bool FromYesNo(this string value)
        {
            return value.InternalEquals("Yes");
        }

        public static bool IgnoreCaseEquals(this string value, string pattern)
        {
            return value.InternalEquals(pattern);
        }

      

        private static bool InternalEquals(this string value, string pattern)
        {
            return !value.IsEmpty() && value.Equals(pattern, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}