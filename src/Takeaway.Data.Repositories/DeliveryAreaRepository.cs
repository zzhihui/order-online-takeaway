using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
     [Repository]
    public class DeliveryAreaRepository : DataRepository<TakeawayContext, DeliveryArea, int>, IDeliveryAreaRepository
    {
         public DeliveryAreaRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }


         public List<DeliveryArea> GetDeliveryAreaByGroup(int deliveryAreaGroup, int shopId)
         {
             return GetMany(r => r.DeliveryGroup == deliveryAreaGroup && r.ShopId==shopId);
         }
    }

}