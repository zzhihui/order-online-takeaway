using System.Collections.Generic;
using System.Linq;
using Core.Test.Utilities.Utilities;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Builder.Data.Builders
{
    public class MenuItemBuilder
    {

        private readonly MenuItem _entity = new MenuItem();

        public MenuItemBuilder()
        {
            
            
            _entity.ItemName = "SampleItemName";
            _entity.ItemDescription = "Sample Item Description";
            _entity.UnitPrice=decimal.Parse("13.56");
            _entity.ItemPic="Sample.Png";
            _entity.TranslateName="Some Translated Name";
            _entity.HasSubOrder=false;
            _entity.Isactive=true;

        }



        public MenuItemBuilder WithMenuItemSubItems(IEnumerable<MenuItemSubItem> custom = null)
        {
            if (custom == null)
            {
                _entity.MenuItemSubItems = new List<MenuItemSubItem>()
                {
                    ObjectMother.CreateAndFill<MenuItemSubItem>(),
                    ObjectMother.CreateAndFill<MenuItemSubItem>(),
                    ObjectMother.CreateAndFill<MenuItemSubItem>(),
                    ObjectMother.CreateAndFill<MenuItemSubItem>()

                };
            }
            else
            {
                _entity.MenuItemSubItems = custom.ToList();
            }
            return this;
        }


        public MenuItem Build()
        {
            return _entity;
        }
    }
}