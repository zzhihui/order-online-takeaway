using System;

namespace Core.Common.Utility
{
    public class DateTimeComparer
    {
        public static int CompareTimeOnly(DateTime source, DateTime destination)
        {

            var currentTime = DateTimeUtil.GetCurrentTime();
            var sourceOnlyTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, source.Hour,
                source.Minute, source.Second);

            var destinationOnlyTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, destination.Hour,
                destination.Minute, destination.Second);


            return DateTime.Compare(sourceOnlyTime, destinationOnlyTime);



        }
    }
}