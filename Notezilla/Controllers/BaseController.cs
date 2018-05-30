using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    }
}