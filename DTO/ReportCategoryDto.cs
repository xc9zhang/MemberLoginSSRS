using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MemberLoginSSRS.Models;

namespace MemberLoginSSRS.DTO
{
    public class ReportCategoryDto
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }

        public IEnumerable<TEReport> TEReports { get; set; }

    }
}