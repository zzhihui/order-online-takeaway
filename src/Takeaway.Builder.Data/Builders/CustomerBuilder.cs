using Takeaway.Data.Entities.Shops;

namespace Takeaway.Builder.Data.Builders
{
    public class CustomerBuilder
    {
        private readonly Customer _entity = new Customer();

        public CustomerBuilder()
        {
            _entity.FullName = Faker.Name.First();
            _entity.Username = Faker.Internet.UserName();
            _entity.Address1 = Faker.Address.StreetName();
            _entity.Address2 = Faker.Address.StreetAddress();
            _entity.Email = Faker.Internet.Email();
            _entity.Phone1 = Faker.Phone.Number();
            _entity.Phone2 = Faker.Phone.Number();
        }


        public Customer Build()
        {
            return _entity;
        }
    }
}