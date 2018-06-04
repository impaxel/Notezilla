using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notezilla.Models;
using Notezilla.Models.Repositories;

namespace Notezilla.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        public UserController(UserRepository userRepository) : base(userRepository)
        {
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Manage()
        {
            var model = new UserListViewModel
            {
                Users = userRepository.GetAll()
            };
            return View(model);
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}