using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using MemberLoginSSRS.Helpers;
using MemberLoginSSRS.ViewModels;

namespace MemberLoginSSRS.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        public ActionResult Index()
        {
            return View(UserHelper.GetAllUsers());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var report = UserHelper.GetUser(id.Value);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new UserEditVM
            {
                RoleSelectList = new SelectList(UserHelper.GetAllRoles(), "Id", "Name"),
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,SelectedRoles")] UserEditVM user)
        {
            if (ModelState.IsValid)
            {
                UserHelper.SaveUser(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var report = UserHelper.GetUser(id.Value);
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
            UserHelper.DeleteUser(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var report = UserHelper.GetUser(id.Value);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(new UserEditVM
            {
                RoleSelectList = new SelectList(UserHelper.GetAllRoles(), "Id", "Name"),
                ID = report.ID,
                Name = report.Name,
                SelectedRoles = report.RoleIds,
            });
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,SelectedRoles,")] UserEditVM user)
        {
            if (ModelState.IsValid)
            {
                UserHelper.SaveUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}
