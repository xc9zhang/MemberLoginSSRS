using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberLoginSSRS.Models
{
    public class TEReport
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SsrsServerUrl { get; set; }
        public string Path { get; set; }

        public virtual ICollection<ReportAssignment> ReportAssignments { get; set; }
        public virtual ICollection<CategoryAssignment> CategoryAssignments { get; set; }
    }
}