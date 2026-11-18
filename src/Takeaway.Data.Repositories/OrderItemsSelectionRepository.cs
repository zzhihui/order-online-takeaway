using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    [Repository]
    public class OrderItemsSelectionRepository : DataRepository<TakeawayContext, OrderItemsSelection, int>, IOrderItemsSelectionRepository
    {
        public OrderItemsSelectionRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }

}