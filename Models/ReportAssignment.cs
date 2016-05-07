using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberLoginSSRS.Models
{
    public class ReportAssignment
    {
        public int ReportAssignmentID { get; set; }
        public int TEReportID { get; set; }
        public int RoleID { get; set; }

        public virtual TEReport TEReport { get; set; }
        public virtual Role Role { get; set; }

    }
}