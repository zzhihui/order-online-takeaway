using System.Collections.Generic;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    public interface ICustomerRepository : IDataRepository<Customer, int>
    {
        Customer GetCustomerByPhone(string phoneNumber, int shopId);
        List<Customer> GetAll(int shopId);
        Customer GetById(int id, int shopId);
        Customer GetRegisterUserByEmail(string email, int shopId);
    }

}