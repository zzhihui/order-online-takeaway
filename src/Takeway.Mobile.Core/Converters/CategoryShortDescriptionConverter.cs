using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;

namespace Takeway.Mobile.Core.Converters
{
    public class CategoryShortDescriptionConverter : MvxValueConverter<string, string>
    {
        protected override string Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            if(string.IsNullOrEmpty(value)||value.Length<50)
            {
                return value;
            }
            return string.Format("{0}...", value.Substring(0, 50));
        }
    }
}