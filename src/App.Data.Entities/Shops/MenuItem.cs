using System;
using System.Collections.Generic;

namespace Takeaway.Data.Entities.Shops
{
    public class MenuItem : BaseDataEntity
    {
        public MenuItem()
        {
            this.MemuItemOffers = new List<MemuItemOffer>();
            this.MenuItemSubItems = new List<MenuItemSubItem>();
            this.MenuSubItemItems = new List<MenuSubItemItem>();
            this.OrderItems = new List<OrderItem>();
            this.OrderItemsSelections= new List<OrderItemsSelection>();
        }

        
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public decimal? UnitPrice { get; set; }
        public string ItemPic { get; set; }
        public string VideoLink { get; set; }

        public int? MenuItemCategoryId { get; set; }
        public string TranslateName { get; set; }
        public int? HotGroupId { get; set; }
       
        public bool? HasSubOrder { get; set; }

        public bool? Isactive { get; set; }

        public bool? Ishot { get; set; }
        public int? SortOrder { get; set; }

        public bool? NoSelection { get; set; }

        public int? FoodCategoryId { get; set; }
        
        public virtual ICollection<MemuItemOffer> MemuItemOffers { get; set; }
        public virtual MenuItemCategory MenuItemCategory { get; set; }
        public virtual ICollection<MenuItemSubItem> MenuItemSubItems { get; set; }
        public virtual ICollection<MenuSubItemItem> MenuSubItemItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<OrderItemsSelection> OrderItemsSelections { get; set; }

        





    }
}