using System;
using System.Collections.Generic;

namespace Takeaway.Data.Entities.Shops
{
    public class MenuItemCategory : BaseDataEntity
    {
        public MenuItemCategory()
        {
            this.MenuItems = new List<MenuItem>();
        }

        
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public bool? Isactive { get; set; }

        public bool? IsInternal { get; set; }

        public int? CategoryGroupId { get; set; }

        public string TranslateName { get; set; }


        public int? SortOrder { get; set; }
        
        
        public virtual ICollection<MenuItem> MenuItems { get; set; }
         
    }
}