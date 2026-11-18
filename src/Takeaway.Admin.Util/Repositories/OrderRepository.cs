using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Enum;
using Takeaway.Admin.Util.Entities.Shops;

namespace Takeaway.Admin.Util.Repositories
{
    public interface IOrderRepository : IDataRepository<Order, int>
    {
        List<Order> GetOpenOrders(int shopId);
        List<Order> GetActiveOrdersByDate(DateTime fromDate, DateTime toDate, int shopId);
        Order GetByGuid(string guid, int shopId);
        Order GetById(int orderId, int shopId);
        void Reload(Order orderExists);
    }


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

        public void Reload(Order orderExists)
        {
            ((DbContext)this.DataContext).Entry(orderExists).Reload();
        }
    }
}