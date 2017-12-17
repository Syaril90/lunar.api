using Microsoft.EntityFrameworkCore;
using ProjectLunar.API.Interfaces;
using ProjectLunar.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectLunar.API.Repositories
{
    public class EntityRepository<T> : IEntityRepository<T>
       where T : class
    {
        protected LunarContext _context;
        public EntityRepository(LunarContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual T InsertOnCommit(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public virtual T UpdateOnCommit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual void DeleteOnCommit(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public virtual void DeletePermanentOnCommit(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public virtual void CommitChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.ValidationResult.ErrorMessage);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
