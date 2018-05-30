﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace Notezilla.Models.Repositories
{
    public abstract class Repository<T> where T: class
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
                session.Save(entity);
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
    }
}