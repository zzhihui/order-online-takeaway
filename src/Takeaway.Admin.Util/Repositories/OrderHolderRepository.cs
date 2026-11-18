using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Shops;

namespace Takeaway.Admin.Util.Repositories
{

    public interface IOrderHolderRepository : IDataRepository<OrderHolder, int>
    {

    }


    [Repository]
    public class OrderHolderRepository : DataRepository<TakeawayContext, OrderHolder, int>, IOrderHolderRepository
    {
        public OrderHolderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}