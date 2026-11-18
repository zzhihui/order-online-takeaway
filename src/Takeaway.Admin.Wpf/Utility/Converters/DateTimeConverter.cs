using System;
using System.Globalization;
using System.Windows.Data;

namespace Takeaway.Admin.Wpf.Utility.Converters
{
    public class DateTimeConverter:  IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
                return ((DateTime) value).ToString("yyyy-MM-dd HH:mm:ss");

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}