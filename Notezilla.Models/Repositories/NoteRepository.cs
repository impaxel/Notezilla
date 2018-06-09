using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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

        public IList<Note> GetAllByUser(User user, FetchOptions options = null)
        {
            var crit = session.CreateCriteria<Note>().Add(Restrictions.Eq("Author", user));
            if (options != null)
            {
                SetFetchOptions(crit, options);
            }
            return crit.List<Note>();
        }
    }
}
