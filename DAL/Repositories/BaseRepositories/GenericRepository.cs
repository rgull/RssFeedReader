using DAL.Entities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.BaseRepositories
{
    public abstract class GenericRepository<T> : GenericGetRepository<T> where T : BaseMainEntity
    {

        internal GenericRepository(ApplicationDbContext db, string userId) : base(db)
        {
        }

        internal GenericRepository( ) : base()
        {
        }

        /// <summary>
        /// Inseting a new entity into db. 
        /// </summary>
        /// <param name="entity">BaseMainEntity</param>
        internal void Insert(T entity)
        {
            entity.CreatedDateTime = DateTime.Now;
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Inserting a  List of entities at once into db.
        /// </summary>
        /// <param name="entities">List of BaseMainEntity</param>
        internal void Insert(List<T> entities)
        {
            var date = DateTime.Now;
            foreach (var entity in entities)
            {
                entity.CreatedDateTime = date;
            }

            _dbSet.AddRange(entities);
        }

        /// <summary>
        /// Updating existing entity from db. 
        /// </summary>
        /// <param name="entity">BaseMainEntity</param>
        internal void Update(T entity)
        {
            var local = _db.Set<T>().Local.FirstOrDefault(f => f.Id == entity.Id);
            if (local != null)
                _db.Entry(local).State = EntityState.Detached;
            entity.UpdatedDateTime = DateTime.Now;
            _dbSet.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }
        
        /// <summary>
        /// Deleting existing entity from db. 
        /// </summary>
        /// <param name="entity">BaseMainEntity</param>
        internal virtual void Delete(T entity)
        {
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);

        }

        /// <summary>
        /// Deleting IQueryable of entities at once from db.
        /// </summary>
        /// <param name="entities">IQueryable of BaseMainEntity</param>
        internal void Delete(IQueryable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Save changes ( wrapper of _db.SaveChanges() )
        /// </summary>
        /// <returns>number of objects got changed</returns>
        internal int SaveChanges()
        {
            return _db.SaveChanges();
        }
    }
}
