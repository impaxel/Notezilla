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

        public List<Note> GetAll(User user, string tag = null)
        {
            var crit = session.CreateCriteria<Note>().Add(Restrictions.Eq("Author", user));
            List<Note> notes;
            if (tag != null)
            {
                notes = crit.List<Note>().Where(x => x.Tags.Contains(tag)).ToList();
            }
            else
            {
                notes = crit.List<Note>().ToList();
            }
            return notes;
        }
    }
}
