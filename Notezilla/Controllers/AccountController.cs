using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            return View();
        }

        public ActionResult CreateAccount(string login, string password)
        {
            var user = new User
            {
                UserName = login,
                Password = password,
                Name = "Вадим"
            };
            userRepository.Save(user);
            return View("Index");
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}