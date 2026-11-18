using System.Collections.Generic;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    public interface IDeliveryOptionRepository : IDataRepository<DeliveryOption, int>
    {
        List<DeliveryOption> GetExternalDeliveryOption(int shopId);
        List<DeliveryOption> GetInternalDeliveryOption(int shopId);
    }
}