using System;

namespace Core.Common.Utility
{
    public class DateTimeUtil
    {
        public static DateTime GetCurrentTime()
        {

            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
        }
    }
}