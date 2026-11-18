using System;
using System.Globalization;
using System.Windows.Data;

namespace Takeaway.Admin.Wpf.Utility.Converters
{
    public class OrderIdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            return value.ToString().PadLeft(8, '0');
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}