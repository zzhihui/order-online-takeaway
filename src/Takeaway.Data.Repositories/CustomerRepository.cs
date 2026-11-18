using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.Entities.Shops;
using TakeawayContext = Takeaway.Data.DbContexts.TakeawayContext;

namespace Takeaway.Data.Repositories
{

    [Repository]
    public class CustomerRepository : DataRepository<TakeawayContext, Customer, int>, ICustomerRepository
    {
        public CustomerRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Customer GetCustomerByPhone(string phoneNumber, int shopId)
        {
            return GetOne(r => r.Phone1 == phoneNumber && r.ShopId==shopId);
        }

        public List<Customer> GetAll(int shopId)
        {
            return GetMany(r => r.ShopId == shopId);
        }

        public Customer GetById(int id, int shopId)
        {
            return GetOne(r => r.Id == id && r.ShopId == shopId);
        }

        public Customer GetRegisterUserByEmail(string email, int shopId)
        {
            return GetOne(r => r.Email == email && r.ShopId == shopId && r.Password!=null && r.Password!="");
        }
    }
}