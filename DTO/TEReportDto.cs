using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberLoginSSRS.DTO
{
    public class TEReportDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SsrsServerUrl { get; set; }
        public string Path { get; set; }
        public string Roles { get; set; }


        public IEnumerable<int> RoleIds { get; set; }

        public IEnumerable<int> CategoryIds { get; set; }

        public string Categories { get; set; }

        public int SelectedCategoryId { get; set; }
    }
}