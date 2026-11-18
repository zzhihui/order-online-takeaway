using System.Collections.Generic;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    public interface ITableCommandRepository : IDataRepository<TableCommand, int>
    {
        List<TableCommand> GetAllActive(int shopId);
    }
}