using System;
using System.Collections.Generic;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    public interface IOrderRepository : IDataRepository<Order, int>
    {
        List<Order> GetOpenOrders(int shopId);
        List<Order> GetActiveOrdersByDate(DateTime fromDate, DateTime toDate, int shopId);
        Order GetByGuid(string guid, int shopId);
        Order GetById(int orderId, int shopId);
    }
}