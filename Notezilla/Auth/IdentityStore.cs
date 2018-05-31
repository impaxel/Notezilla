using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using NHibernate;
using Notezilla.Models.Users;

namespace Notezilla.Auth
{
    public class IdentityStore : IUserStore<User, long>,
        IUserPasswordStore<User, long>,
        IUserLockoutStore<User, long>,
        IUserTwoFactorStore<User, long>,
        IUserRoleStore<User, long>
    {
        private ISession session;

        public IdentityStore(ISession session)
        {
            this.session = session;
        }

        #region IUserStore<User, int>
        public Task CreateAsync(User user)
        {
            session.Save(user);
            session.Flush();
            return Task.FromResult(0);
        }

        public Task DeleteAsync(User user)
        {
            session.Delete(user);
            session.Flush();
            return Task.FromResult(0);
        }

        public Task<User> FindByIdAsync(long userId)
        {
            return Task.FromResult(session.Get<User>(userId));
        }

        public Task<User> FindByNameAsync(string username)
        {
            return Task.FromResult(session.QueryOver<User>()
                    .Where(u => u.UserName == username)
                    .SingleOrDefault());
        }

        public Task UpdateAsync(User user)
        {
            session.Update(user);
            session.Flush();
            return Task.FromResult(0);
        }
        #endregion

        #region IUserPasswordStore<User, int>
        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.Password = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(true);
        }
        #endregion

        #region IUserLockoutStore<User, int>
        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
        {
            return Task.FromResult(DateTimeOffset.MaxValue);
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            return Task.CompletedTask;
        }

        public Task<int> IncrementAccessFailedCountAsync(User user)
        {
            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(User user)
        {
            return Task.CompletedTask;
        }

        public Task<int> GetAccessFailedCountAsync(User user)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetLockoutEnabledAsync(User user)
        {
            return Task.FromResult(false);
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            return Task.CompletedTask;
        }
        #endregion

        #region IUserTwoFactorStore<User, int>
        public Task SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            return Task.CompletedTask;
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user)
        {
            return Task.FromResult(false);
        }
        #endregion

        public void Dispose()
        {
            //do nothing
        }

        public Task AddToRoleAsync(User user, string roleName)
        {
            var role = session.QueryOver<Role>()
                    .Where(r => r.Name == roleName)
                    .SingleOrDefault();
            if (!user.Roles.Contains(role))
            {
                user.Roles.Add(role);
            }
            return Task.FromResult(0);
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            var role = session.QueryOver<Role>()
                    .Where(r => r.Name == roleName)
                    .SingleOrDefault();
            user.Roles.Remove(role);
            return Task.FromResult(0);
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            return Task.FromResult((IList<string>)user.Roles.Select(r => r.Name).ToList());
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            return Task.FromResult(user.Roles.Any(r => r.Name == roleName));
        }
    }
}