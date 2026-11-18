using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Shops;

namespace Takeaway.Admin.Util.Repositories
{
    

    public interface ITableCommandRepository : IDataRepository<TableCommand, int>
    {

    }

    public class TableCommandRepository : DataRepository<TakeawayContext, TableCommand, int>, ITableCommandRepository
    {
        public TableCommandRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

      
    }
}