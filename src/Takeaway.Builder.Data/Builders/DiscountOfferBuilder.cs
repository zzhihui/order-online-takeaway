using Takeaway.Data.Entities.Shops;

namespace Takeaway.Builder.Data.Builders
{
    public class DiscountOfferBuilder
    {

        private readonly DiscountOffer _entity = new DiscountOffer();


        public DiscountOfferBuilder()
        {
            _entity.Name ="DiscountOfferName";
            _entity.Description = "DiscountOfferDescription";

        }

        public DiscountOffer Build()
        {
            return _entity;

        }
    }
}