using System;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Builder.Data.Builders
{
    public class MenuSubItemItemBuilder
    {
        private readonly MenuSubItemItem _entity = new MenuSubItemItem();

        public MenuSubItemItemBuilder()
        {
            _entity.Created = DateTime.Now;
        }
        public MenuSubItemItem Build()
        {
            return _entity;
        }
    }
}