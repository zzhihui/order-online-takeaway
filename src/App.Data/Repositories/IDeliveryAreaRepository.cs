using System.Collections.Generic;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    public interface IDeliveryAreaRepository : IDataRepository<DeliveryArea, int>
    {
        List<DeliveryArea> GetDeliveryAreaByGroup(int deliveryAreaGroup, int shopId);
    }
}