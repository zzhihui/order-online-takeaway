using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Shops;

namespace Takeaway.Admin.Util.Repositories
{

    public interface ITablesRepository : IDataRepository<Tables, int>
    {

    }


    [Repository]
    public class TablesRepository : DataRepository<TakeawayContext, Tables, int>, ITablesRepository
    {
        public TablesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}