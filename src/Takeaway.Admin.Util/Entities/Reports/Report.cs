using System.Collections.Generic;
using System.Data;

namespace Takeaway.Admin.Util.Entities.Reports
{
    public partial class Report : BaseDataEntity
    {
        public Report()
        {
            this.ReportCategoryReports = new List<ReportCategoryReport>();
            this.ReportLabelReports = new List<ReportLabelReport>();
            this.ReportParameters = new List<ReportParameter>();
        }

        
        public string ReportName { get; set; }
        public string ReportDescription { get; set; }
        public string ReportSql { get; set; }
        public string ReportRole { get; set; }
        public bool? Isactive { get; set; }

        public DataTable ReportResults { get; set; }

        public virtual ICollection<ReportCategoryReport> ReportCategoryReports { get; set; }
        public virtual ICollection<ReportLabelReport> ReportLabelReports { get; set; }
        public virtual ICollection<ReportParameter> ReportParameters { get; set; }
    }
}
