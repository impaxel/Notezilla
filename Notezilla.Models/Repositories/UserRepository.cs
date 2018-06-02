using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Notezilla.Models.Users;
using System.Web;

namespace Notezilla.Models.Repositories
{
    [Repository]
    public class UserRepository : Repository<User>
    {
        public UserRepository(ISession session) : base(session)
        {
        }

        public User GetCurrentUser(IPrincipal user = null)
        {
            user = user ?? HttpContext.Current.User;
            if (user == null || user.Identity == null)
            {
                return null;
            }
            var currentUserId = user.Identity.GetUserId();
            if (string.IsNullOrEmpty(currentUserId) || !long.TryParse(currentUserId, out long userId))
            {
                return null;
            }
            return Load(userId);
        }
    }
}
