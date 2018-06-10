﻿using System;
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
        public ActionResult Manage(FetchOptions options)
        {
            var model = new UserListViewModel
            {
                Users = userRepository.GetAll(options)
            };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult LockUnlock(string userId)
        {
            var user = userRepository.Load(userId);
            UserManager.SetLockoutEnabledAsync(userId, true);
            UserManager.SetLockoutEndDateAsync(userId, DateTimeOffset.MaxValue);
            UserManager.UpdateSecurityStampAsync(userId);   
            userRepository.Update(user);
            return RedirectToAction("Manage");
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}