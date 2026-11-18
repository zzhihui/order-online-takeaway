namespace Takeaway.Data.Entities.Shops
{
    public partial class MenuSubItemItem : BaseDataEntity
    {

        public int? SubItemId { get; set; }
        public int? MenuItemId { get; set; }

        public decimal? Cost { get; set; }


        public virtual MenuItem MenuItem { get; set; }
        public virtual MenuSubItem MenuSubItem { get; set; }
    }
}
