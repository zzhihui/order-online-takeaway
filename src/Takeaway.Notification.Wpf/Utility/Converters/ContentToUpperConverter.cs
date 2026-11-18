using System;
using System.Globalization;
using System.Windows.Data;

namespace Takeaway.Admin.Wpf.Utility.Converters
{
    public class ContentToUpperConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value ==null)
                return string.Empty;

            if (string.IsNullOrEmpty(value.ToString()))
            {
                return string.Empty;
            }
            return value.ToString().ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        } 
    }
}