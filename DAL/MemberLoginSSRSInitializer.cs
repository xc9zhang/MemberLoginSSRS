using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MemberLoginSSRS.Models;

namespace MemberLoginSSRS.DAL
{
    public class MemberLoginSSRSInitializer : System.Data.Entity.CreateDatabaseIfNotExists<MemberLoginSSRSContext>
    {
        protected override void Seed(MemberLoginSSRSContext context)
        {
            var roles = new List<Role>
            {
                new Role
                {
                    ID=1,
                    Name = "Admin"
                },
                new Role
                {
                    ID=2,
                    Name = "Accounting"
                },
                new Role
                {
                    ID=3,
                    Name = "Sales"
                }
             
            };
            roles.ForEach(s => context.Roles.Add(s));
            context.SaveChanges();


            var categories = new List<Category>
            {
                new Category
                {
                    ID=1,
                    Name = "MIS"
                },
                new Category
                {
                    ID=2,
                    Name = "Sales"
                },

                  new Category
                {
                    ID=3,
                    Name = "Accounting"
                },


                  new Category
                {
                    ID=4,
                    Name = "IT"
                }
            };
            categories.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();


            var reports = new List<TEReport>
            {
                new TEReport
                {
                    Name = "DailyB2BTickets-TrendLine",
                    Path="/TEReporting/Report_DailyB2BTicketsMAA",
                    SsrsServerUrl = "http://192.168.168.140/ReportServer/"
                },
                new TEReport
                {
                    Name = "Monthly B2B China Tickets",
                    Path="/TEReporting/Report_MonthlyB2BChinaTickets",
                    SsrsServerUrl = "http://192.168.168.140/ReportServer/"
                },
                new TEReport
                {
                    Name = "Tickets Admin report",
                    Path="/TEReporting/Report_Tickets",
                    SsrsServerUrl = "http://192.168.168.140/ReportServer/"
                },


            };
            reports.ForEach(s => context.TEReports.Add(s));
            context.SaveChanges();

            var reportAssignment = new List<ReportAssignment>
            {
                new ReportAssignment
                {
                    TEReportID = 1,RoleID = 1
                },
                new ReportAssignment
                {
                    TEReportID = 2,RoleID = 2
                },
                new ReportAssignment
                {
                    TEReportID = 1,RoleID = 2
                },
                new ReportAssignment
                {
                    TEReportID = 2,RoleID = 1
                },
                new ReportAssignment
                {
                    TEReportID = 3,RoleID = 1
                },
            };
            reportAssignment.ForEach(s => context.ReportAssignments.Add(s));
            context.SaveChanges();

            var categoryAssignment = new List<CategoryAssignment>
            {
                new CategoryAssignment
                {
                    TEReportID = 1,CategoryID = 1
                },
                new CategoryAssignment
                {
                    TEReportID = 2,CategoryID = 2
                },
                new CategoryAssignment
                {
                    TEReportID = 1,CategoryID = 2
                }
            };
            reportAssignment.ForEach(s => context.ReportAssignments.Add(s));
            context.SaveChanges();


        }
    }
}