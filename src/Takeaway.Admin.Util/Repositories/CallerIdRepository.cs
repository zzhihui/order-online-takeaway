using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Shops;

namespace Takeaway.Admin.Util.Repositories
{

    public interface ICallerIdRepository : IDataRepository<CallerId, int>
    {

    }


    [Repository]
    public class CallerIdRepository: DataRepository<TakeawayContext, CallerId, int>, ICallerIdRepository
    {
        public CallerIdRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}