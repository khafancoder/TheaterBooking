using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using NHibernate;

namespace TheaterBooking.Infrastructure
{
    public class NHRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {

        ISession _session;

        public NHRepository()
        {
            _session = NHSessionFactory.GetFactory().GetCurrentSession();
        }

        public NHRepository(ISession session)
        {
            _session = session;
        }

        public void Dispose()
        {
            _session.Dispose();
        }


        public IList<TEntity> List()
        {
            return _session
                .CreateCriteria<TEntity>()
                .SetResultTransformer(new NHibernate.Transform.DistinctRootEntityResultTransformer())
                .List<TEntity>();
        }

        public TEntity Get(int id)
        {
            return _session.Get<TEntity>(id);
        }

        public void Delete(int id)
        {
            Delete(_session.Get<TEntity>(id));
        }

        public void Delete(TEntity item)
        {
            ITransaction trans = _session.BeginTransaction();
            try
            {
                _session.Delete(item);
                _session.Flush();

                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }

        public TEntity Save(TEntity item)
        {
            ITransaction trans = _session.BeginTransaction();
            try
            {
                _session.SaveOrUpdate(item);
                _session.Flush();

                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;
            }

            return item;
        }

        public IList<TEntity> Find(Expression<Func<TEntity, bool>> whereExpression)
        {
            return _session.QueryOver<TEntity>()
                .Where(whereExpression)
                .TransformUsing(new NHibernate.Transform.DistinctRootEntityResultTransformer())
                .List();
        }

        public TEntity FindOne(Expression<Func<TEntity, bool>> whereExpression)
        {
            return _session.QueryOver<TEntity>()
                .Where(whereExpression)
                .TransformUsing(new NHibernate.Transform.DistinctRootEntityResultTransformer())
                .SingleOrDefault();
        }

        public int Count(Expression<Func<TEntity, bool>> whereExpression)
        {
            return _session.QueryOver<TEntity>()
                 .Where(whereExpression)
                 .TransformUsing(new NHibernate.Transform.DistinctRootEntityResultTransformer())
                 .RowCount();
        }


    }
}
