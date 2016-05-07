using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberLoginSSRS.Helpers
{
    public static class GlobalConstants
    {
        public static string TeReportConnection = System.Configuration.ConfigurationManager.ConnectionStrings["TEReporting"].ConnectionString;
        public static int RefreshRate = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RefreshRate"]);
    }
}