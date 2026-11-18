namespace Takeaway.Data.Entities.Shops
{
    public partial class MenuItemSubItem:BaseDataEntity
    {
        
        public int? MenuItemId { get; set; }
        public int? SubmenuItemId { get; set; }
        public int? SortOrder { get; set; }
        public int? GroupId { get; set; }
        public int? OptionItemGroupId { get; set; }


        public virtual MenuItem MenuItem { get; set; }
        public virtual MenuSubItem MenuSubItem { get; set; }
    }
}
