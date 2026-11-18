using System;
using Core.Test.Utilities.Utilities;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Builder.Data.Builders
{
    public class ShopNewsBuilder
    {

        private readonly ShopNews _entity = new ShopNews();

        public ShopNewsBuilder()
        {
            _entity.News = string.Format("This is a news {0}",Guid.NewGuid().ToString());
            _entity.Created = DateTime.Now;
        }

        public ShopNews Build()
        {
            return _entity;
        }
    }
}