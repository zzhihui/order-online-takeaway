using System;
using Cirrious.CrossCore.Converters;

namespace Takeway.Mobile.Core.Converters
{
    public class OrderIdConverter:MvxValueConverter<int, string>
    {
        protected override string Convert(int value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.Format("Order ID:{0}", value.ToString().PadLeft(8,'0'));
        }
    }
}