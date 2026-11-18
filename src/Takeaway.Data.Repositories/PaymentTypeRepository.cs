using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    [Repository]
    public class PaymentTypeRepository : DataRepository<TakeawayContext, PaymentType, int>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }


        public List<PaymentType> GetAll(int shopId)
        {
            return GetAllEntities();
        }
    }
}