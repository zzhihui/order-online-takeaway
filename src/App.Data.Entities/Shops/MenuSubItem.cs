using System;
using System.Collections.Generic;

namespace Takeaway.Data.Entities.Shops
{
    public class MenuSubItem : BaseDataEntity
    {

        public MenuSubItem()
        {
            this.MenuItemSubItems = new List<MenuItemSubItem>();
            this.MenuSubItemItems = new List<MenuSubItemItem>();
        }


        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<MenuItemSubItem> MenuItemSubItems { get; set; }
        public virtual ICollection<MenuSubItemItem> MenuSubItemItems { get; set; }
    }
}