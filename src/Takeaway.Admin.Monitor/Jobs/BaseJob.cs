using System;
using Takeaway.Admin.Service.Core;
using Takeaway.Admin.Service.Utilities;

namespace Takeaway.Admin.Service.Jobs
{
    public interface IBaseJob
    { }

    public class BaseJob
    {

        public static DateTime ValidateDate(string dateTimeAsString, string name)
        {
            if (string.IsNullOrWhiteSpace(dateTimeAsString))
                throw new InvalidParameterExcpetion(string.Format("Blank {0} Date", name));

            var dateTime = dateTimeAsString.AsDateTime();

            if (!dateTime.HasValue)
                throw new InvalidParameterExcpetion(string.Format("Invalid {0} Date value '{1}'", name, dateTimeAsString));

            return dateTime.Value;
        }
    }
}