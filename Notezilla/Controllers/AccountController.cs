using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Notezilla.Models;
using Notezilla.Models.Repositories;
using Notezilla.Models.Users;

namespace Notezilla.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        public AccountController(UserRepository userRepository) : base(userRepository)
        {
        }

        [AllowAnonymous]
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Signin(SigninViewModel model)
        {
            if (ModelState.IsValid && !User.Identity.IsAuthenticated)
            {
                var result = SignInManager.PasswordSignInAsync(model.Login, model.Password, false, false).Result;
                if (result == SignInStatus.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неверное имя пользователя или пароль");
                }
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Signout()
        {
            SignInManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Signup()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(SignupViewModel model)
        {
            if (ModelState.IsValid && !User.Identity.IsAuthenticated)
            {
                var user = new User(model.Login);
                var result = UserManager.CreateAsync(user, model.Password);
                if (result.Result.Succeeded)
                {
                    UserManager.AddToRoleAsync(user.Id, "User");
                    SignInManager.SignIn(user, false, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}