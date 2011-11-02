using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheaterBooking.Infrastructure
{
    public interface IRepository<TEntity> : IDisposable
    {
        IList<TEntity> List();
        TEntity Get(int id);
        void Delete(int id);
        void Delete(TEntity item);
        TEntity Save(TEntity item);
        IList<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression);
        TEntity FindOne(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression);
        int Count(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression);
    }
}
