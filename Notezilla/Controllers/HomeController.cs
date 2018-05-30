using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notezilla.Models.Repositories;

namespace Notezilla.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(UserRepository userRepository) : base(userRepository)
        {
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}