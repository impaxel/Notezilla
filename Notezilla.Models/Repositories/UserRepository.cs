using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Notezilla.Models.Users;

namespace Notezilla.Models.Repositories
{
    [Repository]
    public class UserRepository : Repository<User>
    {
        public UserRepository(ISession session) : base(session)
        {
        }
    }
}
