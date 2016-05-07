using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MemberLoginSSRS.Helpers;

namespace MemberLoginSSRS.ViewModels
{
    public class ReportEditVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SsrsServerUrl { get; set; }
        public string Path { get; set; }
        public SelectList RoleSelectList { get; set; }
        public IEnumerable<int> SelectedRoles { get; set; }

        public SelectList CategorySelectList { get; set; }
        public IEnumerable<int> SelectedCategories { get; set; }
    }
}