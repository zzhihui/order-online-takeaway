using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    public class TableAliasRepository : DataRepository<TakeawayContext, TableAlias, int>, ITableAliasRepository
    {
        public TableAliasRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<TableAlias> GetAllActive(int shopId)
        {
            return GetMany(r => r.Isactive.HasValue && r.Isactive.Value && r.ShopId==shopId).OrderByDescending(s => s.SortOrder).ToList();
        }
    }
}