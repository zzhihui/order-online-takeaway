using System;

namespace Takeaway.Data.Entities.Reports
{
    public partial class ReportLabelReport : BaseDataEntity
    {
        
        public int? ReportLabelId { get; set; }
        public int? ReportId { get; set; }
        
        public virtual Report Report { get; set; }
        public virtual ReportLabel ReportLabel { get; set; }
    }
}
