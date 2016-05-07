using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using MemberLoginSSRS.DAL;
using MemberLoginSSRS.Helpers;
using Microsoft.Reporting.WebForms;

namespace MemberLoginSSRS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Creates a ReportViewer control and stores it on the ViewBag
        /// </summary>
        /// <returns></returns>
        public ActionResult View(int id, int categoryId)
        {
            var report = ReportHelper.GetReport(id);
            report.SelectedCategoryId = categoryId;
            var reportViewer = new ReportViewer
            {
                ProcessingMode = ProcessingMode.Remote,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100)

           
             
            };

            IReportServerCredentials irsc = new CustomReportCredentials("RemoteUser", "Remote2User", "TOUREASTGROUP");
            reportViewer.ServerReport.ReportServerCredentials = irsc;

            //IReportServerCredentials irsc = new CustomReportCredentials("tony.song", "nvtgf7TH", "TOUREASTGROUP");
            //reportViewer.ServerReport.ReportServerCredentials = irsc;

            reportViewer.ServerReport.ReportPath = report.Path;
            reportViewer.ServerReport.ReportServerUrl = new Uri(report.SsrsServerUrl);
            reportViewer.AsyncRendering = false;


         

          
         

            ViewBag.ReportViewer = reportViewer;

            return View(report);
        }

    }
}