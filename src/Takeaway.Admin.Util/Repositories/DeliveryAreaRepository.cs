using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Shops;

namespace Takeaway.Admin.Util.Repositories
{
    public interface IDeliveryAreaRepository : IDataRepository<DeliveryArea, int>
    {
    }


    [Repository]
    public class DeliveryAreaRepository : DataRepository<TakeawayContext, DeliveryArea, int>, IDeliveryAreaRepository
    {
         public DeliveryAreaRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
        
    }

}