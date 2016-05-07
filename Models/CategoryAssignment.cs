using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberLoginSSRS.Models
{
    public class CategoryAssignment
    {
        public int CategoryAssignmentID { get; set; }
        public int TEReportID { get; set; }
        public int CategoryID { get; set; }

        public virtual TEReport TEReport { get; set; }
        public virtual Category Category { get; set; }

    }
}