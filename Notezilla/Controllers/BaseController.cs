using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Notezilla.Auth;
using Notezilla.Models.Repositories;

namespace Notezilla.Controllers
{
    public class BaseController : Controller
    {
        protected UserRepository userRepository;

        public BaseController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public SignInManager SignInManager
            => HttpContext.GetOwinContext().Get<SignInManager>();

        public UserManager UserManager
            => HttpContext.GetOwinContext().GetUserManager<UserManager>();
    }
}