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
    public class AccountController : BaseController
    {
        public AccountController(UserRepository userRepository) : base(userRepository)
        {
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = SignInManager.PasswordSignIn(model.Login, model.Password, false, false);
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

        public ActionResult CreateAccount(string login, string password)
        {
            var user = new User
            {
                UserName = login,
                Password = password
            };
            var result = UserManager.CreateAsync(user, password);
            if (result.Result.Succeeded)
            {
                SignInManager.SignIn(user, false, false);
            }
            else
            {
                foreach (var error in result.Result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return RedirectToAction("Login", "Account");
        }

        //public ActionResult CreateAccount(string login, string password)
        //{
        //    var user = new User
        //    {
        //        UserName = login,
        //        Password = password,
        //        Name = "Вадим"
        //    };
        //    userRepository.Save(user);
        //    return View("Index");
        //}

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}