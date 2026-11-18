using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Core.Common.Utility.Extension;
using Takeaway.Service.Contract.Entities.Print;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Admin.Wpf.Utility
{
    public class OrderPrintBuilder
    {
        public static List<WsPrintRawData> SetupOrdePrintItems(List<WsMenuItem> orderItems, int batchId = 0)
        {
            List<WsMenuItem> groupItems;

            if (batchId == 0)
            {
                groupItems =
                    orderItems.OrderBy(s => s.FoodCategoryId)
                        .ToList();
            }
            else
            {
                groupItems =
                    orderItems.Where(r => r.BatchId == batchId)
                        .OrderBy(s => s.FoodCategoryId)
                        .ToList();
            }

            var orderPrintItems = new List<WsPrintRawData>(groupItems.GroupBy(g => g.Id)
                .Select(group =>
                    new WsPrintRawData
                    {
                        TextPart = string.Format("{0} * {1}", group.Count(),
                        BuildOrderStringWithComment(group)),
                        ValuePart = group.Sum(i => i.Price).ToPriceStringIe(),
                        SortOrder = group.Select(i => i.FoodCategoryId).FirstOrDefault()

                    }
                    )
                .ToList());

            return orderPrintItems.OrderBy(s => s.SortOrder).ToList();

        }

        private static string BuildOrderStringWithComment(IGrouping<int, WsMenuItem> groupResults)
        {
            var builder = new StringBuilder();

            var firstItem = groupResults.FirstOrDefault();
            if (firstItem != null)
                builder.AppendLine(firstItem.DisplayName);

            foreach (var item in groupResults)
            {

                if ((item.SubItemSelections == null||!HasSelection(item.SubItemSelections)) && string.IsNullOrEmpty(item.Comment) )
                    continue;

                if (item.SubItemSelections != null)
                {
                    
                    foreach (var subOrder in item.SubItemSelections)
                    {
                        if (subOrder.Selected != null)
                        {
                            if(!string.IsNullOrEmpty(subOrder.Selected.Comment))
                            builder.AppendLine(string.Format("    {0}", subOrder.Selected.Comment));
                        }
                            
                    }
                }

                builder.AppendLine(item.Comment);
            }

            return builder.ToString();

        }

        private static bool HasSelection(List<WsMenuSubItem> subItemSelections)
        {
            return subItemSelections.Any(subOrder => subOrder.Selected != null);
        }

        //private static string BuildOrderString(WsMenuItem item)
        //{
        //    var builder = new StringBuilder();

        //    builder.AppendLine(item.Name);
        //    if (item.SubItemSelections != null)
        //    {
        //        foreach (var subOrder in item.SubItemSelections)
        //        {
        //            if (subOrder.Selected != null)
        //                builder.AppendLine(string.Format("+ {0}", subOrder.Selected.Name));
        //        }
        //    }

        //    builder.AppendLine(item.Comment);
        //    return builder.ToString();
        //}
    }
}