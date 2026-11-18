using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Takeaway.Admin.Wpf.Utility.Converters
{
    public class ColorIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (!(value is bool)) return "#DA542D";

            return (bool)value ? "#016B0F" : "#DA542D";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}