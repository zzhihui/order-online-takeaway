using System;
using Cirrious.MvvmCross.Plugins.Sqlite;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeway.Mobile.Core.Services.DataServices
{
    public class DbOrderItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public decimal OrderValue { get; set; }

        public DateTime? Created { get; set; }


        public static DbOrderItem CreateDbOrderItem(WsOrder order)
        {


            return new DbOrderItem()
            {
                Created = DateTime.Now,
                OrderId = order.Id,
                OrderValue = order.OrderTotal
            };

        }
    }
}
