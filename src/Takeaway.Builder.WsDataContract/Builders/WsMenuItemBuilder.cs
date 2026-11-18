using System.Collections.Generic;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeaway.Builder.WsDataContract.Builders
{
    public class WsMenuItemBuilder
    {
        private readonly WsMenuItem _entity;

        public WsMenuItemBuilder()
        {
            _entity = new WsMenuItem { Isactive = true };

        }


        public WsMenuItemBuilder WithName(string custom = null)
        {
            _entity.Name = custom ?? "Snack Box Meal";

            return this;
        }


        public WsMenuItemBuilder WithTranslatedName(string custom = null)
        {
            _entity.TranslatedName = custom ?? "Snack Box Meal";

            return this;
        }


        public WsMenuItemBuilder WithDescription(string custom = null)
        {
            _entity.Description = custom ?? "California rolls & a can of soft drink";

            return this;
        }

        public WsMenuItemBuilder WithPrice(decimal? custom = null)
        {
            _entity.Price = custom ?? 15;

            return this;
        }

        public WsMenuItemBuilder WithHasSubOrder(bool? custom = null)
        {
            _entity.HasSubOrder = custom ?? false;

            return this;
        }

        public WsMenuItemBuilder WithIshot(bool? custom = null)
        {
            _entity.Ishot = custom ?? false;

            return this;
        }


        public WsMenuItemBuilder WithSortOrder(int? custom = null)
        {
            _entity.SortOrder = custom ?? 0;

            return this;
        }


        public WsMenuItemBuilder WithSubItemSelections(List<WsMenuSubItem> custom = null)
        {
            if (custom != null)
                _entity.SubItemSelections = custom;

            return this;
        }

        public WsMenuItem Build(int? custom = null)
        {
            if (custom != null)
                _entity.Id = custom.Value;

            return _entity;

        }

    }
}