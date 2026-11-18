using System.Collections.Generic;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    public interface IDriverRepository : IDataRepository<Driver, int>
    {
        List<Driver> GetAll(int shopId);
    }
}