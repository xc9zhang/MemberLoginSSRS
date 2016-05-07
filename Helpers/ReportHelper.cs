using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MemberLoginSSRS.DAL;
using MemberLoginSSRS.DTO;
using MemberLoginSSRS.Migrations;
using MemberLoginSSRS.Models;
using Microsoft.Ajax.Utilities;
using Category = MemberLoginSSRS.Models.Category;

namespace MemberLoginSSRS.Helpers
{
    [Authorize]
    public static class ReportHelper
    {
        private static readonly MemberLoginSSRSContext Db = new MemberLoginSSRSContext();

        public static IEnumerable<TEReportDto> GetAllReports()
        {
            var reports = Db.TEReports.ToList();
            return reports.Select(GetReport).ToList();
        }

        internal static TEReportDto GetReport(int id)
        {
            var report = Db.TEReports.Include(c=>c.ReportAssignments.Select(b=>b.Role)).FirstOrDefault(p=>p.ID==id);
            return GetReport(report);
        }

        internal static TEReportDto GetReport(TEReport report)
        {
            return new TEReportDto
            {
                ID = report.ID,
                Name = report.Name,
                SsrsServerUrl = report.SsrsServerUrl,
                Path = report.Path,
                Roles = report.ReportAssignments==null?null: string.Join(",", report.ReportAssignments.Select(p => p.Role.Name).OrderBy(p => p)),
                RoleIds = report.ReportAssignments == null ? null : report.ReportAssignments.Select(p => p.RoleID),
                CategoryIds = report.CategoryAssignments == null ? null : report.CategoryAssignments.Select(p => p.CategoryID),
                Categories = report.CategoryAssignments == null ? null : string.Join(",", report.CategoryAssignments.Select(p => p.Category.Name).OrderBy(p => p)),
            };
        }

        internal static IEnumerable<TEReportDto> GetReports(string userName)
        {
            var userRoles = UserHelper.GetRoles(userName);
            var roleNames = userRoles.Select(p => p.Name);
            var reports = Db.TEReports.Where(p => p.ReportAssignments.Any(x => roleNames.Contains(x.Role.Name))).ToList();
            return reports.Select(GetReport).ToList();

        }

        internal static void SaveReport(ViewModels.ReportEditVM report)
        {
            int id = report.ID;
            var reportUpdate = new TEReport
            {
                ID=report.ID,
                Name = report.Name,
                SsrsServerUrl = report.SsrsServerUrl,
                Path = report.Path
            };
            if (id == 0)
            {
                Db.TEReports.Add(reportUpdate);
                Db.SaveChanges();
                id = reportUpdate.ID;
            }
            else
            {
                reportUpdate = Db.TEReports.FirstOrDefault(p => p.ID == id);
                reportUpdate.Name = report.Name;
                reportUpdate.SsrsServerUrl = report.SsrsServerUrl;
                reportUpdate.Path = report.Path;
                foreach (var assignment in Db.ReportAssignments.Where(p=>p.TEReportID == id))
                {
                    reportUpdate.ReportAssignments.Remove(assignment);
                    Db.ReportAssignments.Remove(assignment);
                }
                foreach (var categoryassignment in Db.CategoryAssignments.Where(p => p.TEReportID == id))
                {
                    reportUpdate.CategoryAssignments.Remove(categoryassignment);
                    Db.CategoryAssignments.Remove(categoryassignment);
                }

                Db.TEReports.AddOrUpdate(reportUpdate);
                Db.SaveChanges();
            }
            if (report.SelectedRoles != null)
            {
                foreach (var roleid in report.SelectedRoles)
                {
                    Db.ReportAssignments.Add(new ReportAssignment
                    {
                        TEReportID = id,
                        RoleID = roleid
                    });
                }
            }
            if (report.SelectedCategories != null)
            {
                foreach (var categoryid in report.SelectedCategories)
                {
                    Db.CategoryAssignments.Add(new CategoryAssignment
                    {
                        TEReportID = id,
                        CategoryID = categoryid
                    });
                }
            }

            Db.SaveChanges();
        }

        internal static void DeleteReport(int id)
        {
            var report = Db.TEReports.FirstOrDefault(p => p.ID == id);
            Db.TEReports.Remove(report);
            Db.SaveChanges();
        }

        internal static IEnumerable<Category> GetAllCategories()
        {
            return Db.Categories.ToList();
        }

        public static IEnumerable<ReportCategoryDto> GetReportList(string userName)
        {
            var userRoles = UserHelper.GetRoles(userName);
            var roleNames = userRoles.Select(p => p.Name);
            var reports = Db.TEReports.Where(p => p.ReportAssignments.Any(x => roleNames.Contains(x.Role.Name))).ToList();

            var categories=reports.SelectMany(p => p.CategoryAssignments).DistinctBy(p=>p.CategoryID).ToList();

            return categories.Select(p => new ReportCategoryDto
            {
                CategoryID =p.CategoryID,
                Name = p.Category.Name,
                TEReports = reports.Where(k=>k.CategoryAssignments.Any(q=>q.CategoryID ==p.CategoryID)).ToList()
            }).ToList();
        }



        internal static bool SaveLayout(string layout, string username)
        {
            var user = Db.Users.FirstOrDefault(p => p.Name == username);
            if (user != null)
            {
                user.Layout = layout;
                Db.SaveChanges();
                return true;
            }
            return false;
        }

        internal static string GetLayout(string username)
        {
            var user = Db.Users.FirstOrDefault(p => p.Name == username);
            return user != null ? user.Layout : null;
        }
    }
}