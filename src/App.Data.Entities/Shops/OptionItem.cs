using System;
using System.Collections.Generic;

namespace Takeaway.Data.Entities.Shops
{
    public class OptionItem : BaseDataEntity
    {
        
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public decimal? UnitPrice { get; set; }
        
        
        public string TranslateName { get; set; }

        public bool? Isactive { get; set; }
        
        public int? SortOrder { get; set; }
        

        public int? GroupId { get; set; }




    }
}