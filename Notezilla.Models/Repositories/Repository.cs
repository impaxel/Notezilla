using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using NHibernate;
using NHibernate.Criterion;
using Notezilla.Models.Notes;

namespace Notezilla.Models.Repositories
{
    public abstract class Repository<T> where T : class
    {
        protected ISession session;

        public Repository(ISession session)
        {
            this.session = session;
        }

        public virtual T Load(long id)
        {
            return session.Load<T>(id);
        }

        public virtual void Save(T entity)
        {
            using (var tr = session.BeginTransaction())
            {
                session.SaveOrUpdate(entity);
                tr.Commit();
            }
        }

        public virtual void Update(T entity)
        {
            using (var tr = session.BeginTransaction())
            {
                session.Update(entity);
                tr.Commit();
            }
        }

        public virtual IList<T> GetAll(FetchOptions options = null)
        {
            var crit = session.CreateCriteria<T>();
            if (options != null)
            {
                if (!string.IsNullOrEmpty(options.SortExpression))
                {
                    crit.AddOrder(options.SortDirection == SortDirection.Ascending ?
                        Order.Asc(options.SortExpression) :
                        Order.Desc(options.SortExpression));
                }
            }
            return crit.List<T>();
        }
    }
}
