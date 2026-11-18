using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Core.Test.Utilities.Utilities;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Builder.Data.Builders
{
    public class MenuItemCategoryBuilder
    {

        private readonly MenuItemCategory _entity = new MenuItemCategory();

        public MenuItemCategoryBuilder()
        {
            _entity.Name = "MenuItemCategory Name";
            _entity.Description = "MenuItemCategory Description";
            _entity.ImageUrl = "sample.gif";
            _entity.Isactive = true;

        }


        public MenuItemCategoryBuilder WithMenuItems(IEnumerable<MenuItem> custom = null)
        {

            if (custom == null)
            {
                _entity.MenuItems = new Collection<MenuItem>()
                {
                    ObjectMother.CreateAndFill<MenuItem>(),
                    ObjectMother.CreateAndFill<MenuItem>(),
                    ObjectMother.CreateAndFill<MenuItem>()
                };
            }
            else
            {
                _entity.MenuItems = custom.ToList();

            }

            return this;

        }

        public MenuItemCategory Build()
        {
            return _entity;
        }
    }
}