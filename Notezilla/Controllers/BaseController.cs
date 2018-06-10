using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NHibernate.AspNet.Identity;
using Notezilla.Models.Repositories;
using Notezilla.Models.Users;

namespace Notezilla.Controllers
{
    public class BaseController : Controller
    {
        protected UserRepository userRepository;

        public BaseController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public SignInManager<User, string> SignInManager
            => HttpContext.GetOwinContext().Get<SignInManager<User, string>>();

        public UserManager<User> UserManager
            => HttpContext.GetOwinContext().GetUserManager<UserManager<User>>();

        public RoleManager<IdentityRole> RoleManager
            => HttpContext.GetOwinContext().Get<RoleManager<IdentityRole>>();

        public IAuthenticationManager AuthenticationManager
            => HttpContext.GetOwinContext().Authentication;
    }
}