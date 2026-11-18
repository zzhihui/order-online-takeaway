using System;
using System.Collections.Generic;

namespace Takeaway.Data.Entities.Reports
{
    public partial class ReportLabel : BaseDataEntity
    {
        public ReportLabel()
        {
            this.ReportLabelReports = new List<ReportLabelReport>();
        }

        
        public string Name { get; set; }
        public string Description { get; set; }
        
        public bool? Isactive { get; set; }
        public virtual ICollection<ReportLabelReport> ReportLabelReports { get; set; }
    }
}
