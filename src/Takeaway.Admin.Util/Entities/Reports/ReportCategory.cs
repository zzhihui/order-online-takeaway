using System.Collections.Generic;

namespace Takeaway.Admin.Util.Entities.Reports
{
    public partial class ReportCategory : BaseDataEntity
    {
        public ReportCategory()
        {
            this.SubReportCategories = new List<ReportCategory>();
            this.ReportCategoryReports = new List<ReportCategoryReport>();
        }

        
        public string Name { get; set; }
        public string Description { get; set; }
        public int? MainReportCategoryId { get; set; }
        public bool? Isactive { get; set; }
        
        public virtual ICollection<ReportCategory> SubReportCategories { get; set; }
        public virtual ReportCategory MainReportCategory { get; set; }
        public virtual ICollection<ReportCategoryReport> ReportCategoryReports { get; set; }
    }
}
