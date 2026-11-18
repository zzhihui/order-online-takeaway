using System;

namespace Takeaway.Data.Entities.Reports
{
    public partial class ReportParameter : BaseDataEntity
    {
        
        public int? ReportId { get; set; }
        public string ParameterName { get; set; }
        public int? ParameterTypeId { get; set; }
        public virtual Report Report { get; set; }
        public virtual ReportParameterType ReportParameterType { get; set; }
    }
}
