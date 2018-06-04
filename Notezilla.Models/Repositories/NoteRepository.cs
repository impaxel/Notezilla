using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using Notezilla.Models.Notes;
using Notezilla.Models.Users;

namespace Notezilla.Models.Repositories
{
    [Repository]
    public class NoteRepository : Repository<Note>
    {
        public NoteRepository(ISession session) : base(session)
        {
        }
    }
}
