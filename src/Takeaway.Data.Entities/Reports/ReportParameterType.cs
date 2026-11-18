using System;
using System.Collections.Generic;

namespace Takeaway.Data.Entities.Reports
{
    public partial class ReportParameterType : BaseDataEntity
    {
        public ReportParameterType()
        {
            this.ReportParameters = new List<ReportParameter>();
        }


        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ReportParameter> ReportParameters { get; set; }
    }
}
