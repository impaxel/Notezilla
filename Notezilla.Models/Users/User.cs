using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NHibernate.AspNet.Identity;
using Notezilla.Models.Notes;

namespace Notezilla.Models.Users
{
    public class User : IdentityUser
    {
        public virtual DateTime RegistrationDate { get; set; }

        public virtual ICollection<Note> Notes { get; set; }

        public User() : base()
        {
            Notes = new List<Note>();
        }

        public User(string userName) : base(userName)
        {
        }

        public virtual async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, string> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
