using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Notezilla.Models.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;

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

        public override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            var user = UserManager.FindByName(userName);
            if (user != null && !user.IsEnabled)
            {
                return Task.FromResult(SignInStatus.LockedOut);
            }
            return base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
        }
    }
}