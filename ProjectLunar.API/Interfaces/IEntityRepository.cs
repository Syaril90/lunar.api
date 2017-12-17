using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectLunar.API.Interfaces
{
    public interface IEntityRepository<T>
        where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        T InsertOnCommit(T entity);
        T UpdateOnCommit(T entity);
        void DeleteOnCommit(T entity);
        void DeletePermanentOnCommit(T entity);
        void CommitChanges();



    }
}
