using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;

namespace Takeway.Mobile.Core.Converters
{
    public class DateStringDashConverter:MvxValueConverter<DateTime?, string>
    {

        protected override string Convert(DateTime? value, Type targetType, object parameter, CultureInfo culture)
        {
            return !value.HasValue ? string.Empty : value.Value.ToString("yyyy-MM-dd");
        }

    }
}