using System;

namespace Takeaway.Data.Entities.Reports
{
    public partial class ReportCategoryReport : BaseDataEntity
    {
        
        public int? ReportId { get; set; }
        public int? ReportCategoryId { get; set; }
        
        public virtual Report Report { get; set; }
        public virtual ReportCategory ReportCategory { get; set; }
    }
}
