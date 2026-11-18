using System;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Builder.Data.Builders
{
    public class MenuItemSubItemBuilder
    {

        private readonly MenuItemSubItem _entity = new MenuItemSubItem();

        public MenuItemSubItemBuilder()
        {
            _entity.Created = DateTime.Now;
        }
        public MenuItemSubItem Build()
        {
            return _entity;
        }
    }
}