using System;
using System.Collections.Generic;

namespace Takeaway.Admin.Wpf.Utility
{
    public class SystemLocaleHelper
    {
        public static bool SupportLocale(string defaultSystemLocal)
        {
            var validLocal = new List<string>()
            {
                "zh-CN"
            };

            return validLocal.Contains(defaultSystemLocal);

        }
    }
}