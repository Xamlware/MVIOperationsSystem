using MVIOperationsSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly OfflineContext _db;

        public Repository(OfflineContext db)
        {
            _db = db;
        }

        public TEntity Get(int id)
        {
            // Here we are working with a DbContext, not PlutoContext. So we don't have DbSets 
            // such as Courses or Authors, and we need to use the generic Set() method to access them.
            return _db.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            // Note that here I've repeated _db.Set<TEntity>() in every method and this is causing
            // too much noise. I could get a reference to the DbSet returned from this method in the 
            // constructor and store it in a private field like _entities. This way, the implementation
            // of our methods would be cleaner:
            // 
            // _entities.ToList();
            // _entities.Where();
            // _entities.SingleOrDefault();
            // 
            // I didn't change it because I wanted the code to look like the videos. But feel free to change
            // this on your own.
            return _db.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _db.Set<TEntity>().Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _db.Set<TEntity>().SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            _db.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _db.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _db.Set<TEntity>().RemoveRange(entities);
        }
    }
}