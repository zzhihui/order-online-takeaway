using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;
using Takeway.Mobile.Core.Resources;

namespace Takeway.Mobile.Core.Converters
{
    public class DecimalStringValueConverter : MvxValueConverter<decimal?, string>
    {

        protected override string Convert(decimal? value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!value.HasValue)
                value = 0;
            
            var userSelectedCulture = new CultureInfo("en-IE");
            return string.Format("{0}", value.Value.ToString("C2", userSelectedCulture));
        }
         
    }
}