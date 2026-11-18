using System;
using Core.Test.Utilities.Utilities;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Builder.Data.Builders
{
    public class MemuItemOfferBuilder
    {
        private readonly MemuItemOffer _entity = new MemuItemOffer();

        public MemuItemOfferBuilder()
        {
            _entity.FromDate = DateTime.Now.AddDays(-5);
            _entity.EndDate = DateTime.Now.AddDays(10);

        }

        public MemuItemOfferBuilder WithMenuItem(MenuItem custom=null)
        {
            _entity.MenuItem = custom ?? ObjectMother.CreateAndFill<MenuItem>();
            return this;
        }


        public MemuItemOfferBuilder WithDiscountOffer(DiscountOffer custom=null)
        {
            _entity.DiscountOffer = custom ?? ObjectMother.CreateAndFill<DiscountOffer>();
            return this;
        }

        public MemuItemOffer Build()
        {
            return _entity;
        }
    }
}