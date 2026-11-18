using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Builder.WsDataContract.Builders
{
    public class WsMenuItemCategoryBuilder
    {
        private readonly WsMenuItemCategory _entity;

        public WsMenuItemCategoryBuilder()
        {
            _entity = new WsMenuItemCategory { Isactive = true, WebImageUrl = "" };
        }


        public WsMenuItemCategoryBuilder WithName(string custom = null)
        {
            _entity.Name = custom ?? "Special Meal Deal";

            return this;
        }


        public WsMenuItemCategoryBuilder WithDescription(string custom = null)
        {
            _entity.Description = custom ?? "What is on offer";

            return this;
        }


        public WsMenuItemCategory Build(int? custom = null)
        {
            if (custom != null)
                _entity.Id = custom.Value;

            return _entity;

        }

    }
}