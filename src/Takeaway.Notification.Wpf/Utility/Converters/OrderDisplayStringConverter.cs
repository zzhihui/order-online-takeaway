using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Core.Common.Utility.Extension;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Admin.Wpf.Utility.Converters
{
    public class OrderDisplayStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (!(value is WsOrder)) return "";

                var order = ((WsOrder) value);

            return string.Format("Order:[{0}]-[{1}]-[Total:{2}]", order.OrderRef, order.Address,
                order.OrderTotal.ToPriceStringIe());

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}