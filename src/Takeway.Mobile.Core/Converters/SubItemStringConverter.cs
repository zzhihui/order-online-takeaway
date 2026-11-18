using System;
using System.Collections.Generic;
using Cirrious.CrossCore.Converters;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeway.Mobile.Core.Converters
{
    public class SubItemStringConverter : MvxValueConverter<List<WsMenuSubItem>, string>
    {
        protected override string Convert(List<WsMenuSubItem> value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var builder = new System.Text.StringBuilder();
            foreach (var subOrder in value)
            {
                builder.AppendLine(string.Format("+ {0}", subOrder.Selected.Name));
            }
            return builder.ToString();
        }
    }
}