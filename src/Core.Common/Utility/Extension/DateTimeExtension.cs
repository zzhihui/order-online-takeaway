using System;

namespace Core.Common.Utility.Extension
{
    public static class DateTimeExtension
    {
        public static string ToDateUtcFormat(this DateTime? source)
        {
            return source.HasValue ? source.Value.ToString("yyyy-MM-dd") : string.Empty;
        }

        public static string ToDateTimeUtcFormat(this DateTime? source)
        {
            return source.HasValue ? source.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
        }
    }
}