using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Admin.Wpf.Utility.Converters
{
    public class SubItemStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var builder = new StringBuilder();

            var list = value as List<WsMenuSubItem>;
            if (list != null)
            {
                foreach (var subOrder in list)
                {
                    if (subOrder.Selected!=null)
                        builder.AppendLine(string.Format("+ {0}", subOrder.Selected.DisplayName, subOrder.Selected.Comment));
                }
            }
            return builder.ToString();
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        } 
         
    }
}