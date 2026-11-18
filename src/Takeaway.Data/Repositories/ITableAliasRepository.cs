using System.Collections.Generic;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    public interface ITableAliasRepository : IDataRepository<TableAlias, int>
    {
        List<TableAlias> GetAllActive(int shopId);
    }
}