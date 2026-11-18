using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Shops;

namespace Takeaway.Admin.Util.Repositories
{

    public interface IOrderSequenceRepository : IDataRepository<OrderReference, int>
    {

    }


    [Repository]
    public class OrderSequenceRepository : DataRepository<TakeawayContext, OrderReference, int>, IOrderSequenceRepository
    {
        public OrderSequenceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}