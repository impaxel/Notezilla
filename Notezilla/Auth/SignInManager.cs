using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Notezilla.Models.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Notezilla.Auth
{
    public class SignInManager : SignInManager<User, long>
    {
        public SignInManager(UserManager<User, long> userManager,
            IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public void SignOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}