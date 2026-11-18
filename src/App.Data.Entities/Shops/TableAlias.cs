namespace Takeaway.Data.Entities.Shops
{
    public class TableAlias : BaseDataEntity
    {
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }

        public string TranslateName { get; set; }

        public bool? Isactive { get; set; }

        public int? SortOrder { get; set; }
    }
}