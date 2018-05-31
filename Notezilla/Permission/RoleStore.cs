using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using NHibernate;
using Notezilla.Models.Users;

namespace Notezilla.Permission
{
    public class RoleStore : IRoleStore<Role, long>
    {
        private readonly ISession session;

        public RoleStore(ISession session)
        {
            this.session = session;
        }

        public Task CreateAsync(Role role)
        {
            session.Save(role);
            session.Flush();
            return Task.FromResult(0);
        }

        public Task DeleteAsync(Role role)
        {
            session.Delete(role);
            session.Flush();
            return Task.FromResult(0);
        }

        public Task<Role> FindByIdAsync(long roleId)
        {
            return Task.FromResult(session.Get<Role>(roleId));
        }

        public Task<Role> FindByNameAsync(string roleName)
        {
            return Task.FromResult(session.QueryOver<Role>()
                    .Where(r => r.Name == roleName)
                    .SingleOrDefault());
        }

        public Task UpdateAsync(Role role)
        {
            session.Update(role);
            session.Flush();
            return Task.FromResult(0);
        }

        public void Dispose()
        {

        }
    }
}