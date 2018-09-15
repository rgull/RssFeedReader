using DAL.Entities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.BaseRepositories
{
    public abstract class GenericGetRepository<T> where T : BaseEntity
    {
        internal ApplicationDbContext _db;
        protected DbSet<T> _dbSet;

        internal GenericGetRepository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();

        }
        internal GenericGetRepository()
        {
            _db = new ApplicationDbContext();
            _dbSet = _db.Set<T>();

        }


        internal IQueryable<T> Get()
        {
            return _dbSet.AsQueryable<T>().AsNoTracking();
        }

        public T Get(int id)
        {
            return Get().FirstOrDefault(t => t.Id == id);
        }

        public List<T> Get(Expression<Func<T, bool>> predicate)
        {
            return Get().Where(predicate).ToList();
        }


        /// <summary>
        /// override the Get methods to include child entities in a single call 
        /// </summary>
        /// <param name="predicate">to apply search</param>
        /// <param name="includes">to include child entities</param>
        /// <returns></returns>                 
        public List<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Get();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (predicate != null)
                query = query.Where(predicate);

            return query.ToList();
        }

        /// <summary>
        /// override the Get methods to include child entities in a single call 
        /// </summary>
        /// <param name="includes">to include child entities</param>
        /// <returns></returns> 
        public List<T> Get(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Get();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }

        /// <summary>
        /// override the Get methods to include child entities in a single call 
        /// </summary>
        /// <param name="id">to get single record</param>
        /// <param name="includes">to include child entities</param>
        /// <returns></returns> 
        public T Get(int id, params Expression<Func<T, object>>[] includes)
        {
            var query = Get();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.FirstOrDefault(t => t.Id == id);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = Get();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.FirstOrDefault(predicate);
        }

    }
}
