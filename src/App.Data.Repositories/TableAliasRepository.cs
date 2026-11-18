using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    public class TableCommandRepository : DataRepository<TakeawayContext, TableCommand, int>, ITableCommandRepository
    {
        public TableCommandRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<TableCommand> GetAllActive(int shopId)
        {
            return GetMany(r => r.Isactive.HasValue && r.Isactive.Value && r.ShopId== shopId).OrderByDescending(s => s.SortOrder).ToList();
        }
    }
}