using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Enum;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    [Repository]
    public class OrderRepository : DataRepository<TakeawayContext, Order, int>, IOrderRepository
    {
        public OrderRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<Order> GetOpenOrders(int shopId)
        {
            var last3days = DateTime.Today.AddDays(-3);

            return GetMany(r => 
                        r.StatusId == (int)OrderStatusIdEnum.Open 
                        && (!r.IsInternalOrder.HasValue || !r.IsInternalOrder.Value)
                        && r.Created.HasValue && r.Created >= last3days
                        && r.ShopId==shopId
                        ).OrderByDescending(s=>s.Created).ToList();
        }

        public List<Order> GetActiveOrdersByDate(DateTime fromDate, DateTime toDate, int shopId)
        {
            return GetMany(r =>
                    (r.StatusId == (int)OrderStatusIdEnum.Open 
                    ||r.StatusId==(int)OrderStatusIdEnum.Completed)
                    && r.Created >= fromDate 
                    && r.Created <= toDate
                    && r.ShopId==shopId
                    ).OrderByDescending(s => s.Id).ToList();
                
        }

        public Order GetByGuid(string guid, int shopId)
        {
            return GetOne(r=>r.Guid== guid && r.StatusId != (int)OrderStatusIdEnum.SystemRemoved && r.ShopId==shopId);
        }

        public Order GetById(int orderId, int shopId)
        {
            return GetOne(r => r.ShopId == shopId && r.Id == orderId);
        }
    }
}