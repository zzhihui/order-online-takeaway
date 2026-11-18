using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Shops;

namespace Takeaway.Admin.Util.Repositories
{
    public interface ITableAliasRepository : IDataRepository<TableAlias, int>
    {
    
    }
    public class TableAliasRepository : DataRepository<TakeawayContext, TableAlias, int>, ITableAliasRepository
    {
        public TableAliasRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

    
    }
}