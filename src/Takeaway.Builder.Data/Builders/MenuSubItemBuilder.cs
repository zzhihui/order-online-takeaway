using System.Collections.Generic;
using System.Linq;
using Core.Test.Utilities.Utilities;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Builder.Data.Builders
{
    public class MenuSubItemBuilder
    {
        private readonly MenuSubItem _entity = new MenuSubItem();

        public MenuSubItemBuilder()
        {
            _entity.Name = "Sample SubItem";
            _entity.Description = "Sample SubItem Description";
        }

        public MenuSubItemBuilder WithMenuSubItemItems(IEnumerable<MenuSubItemItem> custom=null)
        {
            if (custom == null)
            {

                _entity.MenuSubItemItems = new List<MenuSubItemItem>()
                {
                    ObjectMother.CreateAndFill<MenuSubItemItem>(),
                    ObjectMother.CreateAndFill<MenuSubItemItem>(),
                    ObjectMother.CreateAndFill<MenuSubItemItem>(),
                };
            }
            else
            {
                _entity.MenuSubItemItems = custom.ToList();
            }

            return this;
            
        }



        public MenuSubItem Build()
        {

            return _entity;

            //public string Name { get; set; }
            //public string Description { get; set; }

            //public virtual ICollection<MenuItemSubItem> MenuItemSubItems { get; set; }
            //public virtual ICollection<MenuSubItemItem> MenuSubItemItems { get; set; }
        }
    }
}