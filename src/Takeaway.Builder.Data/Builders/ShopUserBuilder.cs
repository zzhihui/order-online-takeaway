using System;
using Core.Common.Utility;
using Takeaway.Data.Entities.Authentication;

namespace Takeaway.Builder.Data.Builders
{
    public class ShopUserBuilder
    {
        private readonly ShopUser _entity = new ShopUser();
      
        public ShopUserBuilder()
        {
            _entity.UserName = "TestUserName";
            _entity.Password = new EncryptionHelper().EncryptXml("SomePassword");
            _entity.Isactive=true;
            _entity.Created = DateTime.Now;

        }


        public ShopUser Build()
        {
            return _entity;
        }
    }
}