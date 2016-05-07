using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using System.Collections.Generic;

using MemberLoginSSRS.Helpers;
using MemberLoginSSRS.ViewModels;

namespace MemberLoginSSRS.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {

        public ActionResult Index()
        {


         
                                             
            return View(ReportHelper.GetAllReports());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var report = ReportHelper.GetReport(id.Value);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ReportEditVM
            {
                RoleSelectList = new SelectList(UserHelper.GetAllRoles(), "Id", "Name"),
                CategorySelectList = new SelectList(ReportHelper.GetAllCategories(), "Id", "Name")

            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,SsrsServerUrl,Path,SelectedRoles,SelectedCategories")] ReportEditVM report)
        {
            if (ModelState.IsValid)
            {
                ReportHelper.SaveReport(report);
                return RedirectToAction("Index");
            }

            return View(report);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var report = ReportHelper.GetReport(id.Value);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReportHelper.DeleteReport(id);
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var report = ReportHelper.GetReport(id.Value);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(new ReportEditVM
            {
                RoleSelectList = new SelectList(UserHelper.GetAllRoles(), "Id", "Name"),
                CategorySelectList = new SelectList(ReportHelper.GetAllCategories(), "Id", "Name"),
                ID = report.ID,
                Name = report.Name,
                SsrsServerUrl =report.SsrsServerUrl,
                Path=report.Path,
                SelectedRoles = report.RoleIds,
                SelectedCategories = report.CategoryIds
            });
        }

        // POST: /Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,SsrsServerUrl,Path,SelectedRoles,SelectedCategories")] ReportEditVM report)
        {
            if (ModelState.IsValid)
            {
                ReportHelper.SaveReport(report);
                return RedirectToAction("Index");
            }
            return View(report);
        }


    }
}
