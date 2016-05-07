using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MemberLoginSSRS.Helpers;
using MemberLoginSSRS.Models;

namespace MemberLoginSSRS.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //loginModel passed in with @Html.BeginForm after the user has pressed "Login".
        public ActionResult Login(LoginModel loginModel, string returnUrl)
        {

          

            if (ModelState.IsValid && Membership.ValidateUser(loginModel.UserName, loginModel.Password))
          //  if (ModelState.IsValid)
            {

             

                var userRoles = UserHelper.GetRoles(loginModel.UserName);
                if (!userRoles.Any())
                {
                    //Adds an error to the error list to be displayed at @Html.ValidationSummary
                    ModelState.AddModelError("",
                        "You're not authorized to use this app please contact Admin.");
                    return View(loginModel);
                }
                //Creates a non-presistant cookie that destroys itself on browser close.
                FormsAuthentication.SetAuthCookie(loginModel.UserName, false);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");

            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }

    }
}