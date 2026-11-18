using System;
using System.Globalization;

namespace Core.Common.Utility.Extension
{
    public static class DecimalExtension
    {
        public static string ToPriceStringIe(this decimal? source)
        {
            return source.HasValue ? source.Value.ToString("c2", new CultureInfo("en-IE")) : string.Empty;
        }

        public static string ToPriceStringIe(this decimal source)
        {
            return source.ToString("c2", new CultureInfo("en-IE"));
        }

        public static string ToPercentStringId(this decimal source)
        {
            return source.ToString("p1", new CultureInfo("en-IE"));
        }

        public static string ToWholeNumber(this decimal? source)
        {
            return source.HasValue ? Math.Truncate(source.Value).ToString() : string.Empty;
        }
    }
}