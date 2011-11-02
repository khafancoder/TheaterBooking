using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using TheaterBooking.Web.ViewModels;

namespace TheaterBooking.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signin(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    var user = Membership.GetUser(model.UserName);
                    if (user != null && user.IsLockedOut)
                        ModelState.AddModelError("", "نام کاربری شما به دلیل ورود اشباه کلمه عبور غیر فعال گشته، لطفا به مدیریت سایت مراجعه فرمایید");
                    else
                        ModelState.AddModelError("", "نام کاربری یا کلمه عبور صحیح نمی باشد");
                }
            }

            return View(model);
        }

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.PasswordLength = Membership.MinRequiredPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Membership.GetUser(User.Identity.Name);

                if (user.ChangePassword(model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "خطا در تغییر کلمه عبور");
                }
            }

            ViewBag.PasswordLength = Membership.MinRequiredPasswordLength;

            return View(model);
        }



        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Manage()
        {
            IList<MembershipUser> result = new List<MembershipUser>();
            var usersList = Membership.GetAllUsers();

            foreach (MembershipUser u in usersList)
            {
                result.Add(u);
            }

            return View(result);
        }



        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(string id)
        {
            MembershipUser user = Membership.GetUser(id);
            if (user != null)
            {
                //delete the user
                Membership.DeleteUser(user.UserName, true);

                return RedirectToAction("Manage");
            }
            else
                return HttpNotFound();

        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.PasswordLength = Membership.MinRequiredPasswordLength;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus creationStatus = MembershipCreateStatus.ProviderError;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, out creationStatus);
                if (creationStatus == MembershipCreateStatus.Success)
                {
                    if (model.IsAdmin)
                        Roles.AddUserToRole(model.UserName, "admin");
                }
                else
                {
                    ModelState.AddModelError("", " خطا در ایجاد حساب کاربری : " + creationStatus.ToString());
                    return View();
                }
            }

            return RedirectToAction("Manage");
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(string id)
        {
            var user = Membership.GetUser(id);
            if (user != null)
            {
                CreateUserModel usermodel = new CreateUserModel()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    IsAdmin = Roles.IsUserInRole(user.UserName, "admin")
                };

                return View(usermodel);
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(CreateUserModel model)
        {
            MembershipUser user = Membership.GetUser(model.UserName);
            if (user != null)
            {
                //update user admin role
                if (model.IsAdmin)
                {
                    if (!Roles.IsUserInRole(user.UserName, "admin"))
                        Roles.AddUserToRole(user.UserName, "admin");
                }
                else
                {
                    if (Roles.IsUserInRole(user.UserName, "admin"))
                        Roles.RemoveUserFromRole(user.UserName, "admin");
                }

                //update user email
                user.Email = model.Email;
                Membership.UpdateUser(user);


                return RedirectToAction("Manage");
            }
            else
                return HttpNotFound();
        }


    }
}
