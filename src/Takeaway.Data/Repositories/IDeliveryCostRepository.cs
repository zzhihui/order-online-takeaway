using System.Collections.Generic;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    public interface IDeliveryCostRepository : IDataRepository<DeliveryCost, int>
    {
        List<DeliveryCost> GetAll(int shopId);
    }
}