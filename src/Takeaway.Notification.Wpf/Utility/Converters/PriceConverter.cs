using System;
using System.Globalization;
using System.Windows.Data;

namespace Takeaway.Admin.Wpf.Utility.Converters
{
    public class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal)
            {
                return ((decimal)value).ToString("n2");
            }
            else
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal decValue;
            return decimal.TryParse(value.ToString(), out decValue) ? decValue : decimal.Parse("0");
        }
    }
}