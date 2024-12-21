using Microsoft.EntityFrameworkCore;
using StudentManagement.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Data.Repositories
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            //_context = context;
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        //Get
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeEntities = "")
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query= query.Where(filter);
            }
            //_context.User.Include("EntityName1, EntityName2, EntityName3")
            foreach (var includeEntity in includeEntities.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeEntity); // includeEntity = EntityName1, includeEntity = EntityName2, includeEntity = EntityName3
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeEntities = "")
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query=query.Where(filter);
            }
            //_context.User.Include("EntityName1, EntityName2, EntityName3")
            foreach (var includeEntity in includeEntities.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeEntity); // includeEntity = EntityName1, includeEntity = EntityName2, includeEntity = EntityName3
            }
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        //Add
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsyncNotReturn(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<T> AddAsyncReturn(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        //Update

        //public void Update(T entity)
        //{
        //    if (_context.Entry(entity).State == EntityState.Detached)
        //    {
        //        _dbSet.Attach(entity);
        //    }
        //    _dbSet.Update(entity);
        //}

        //public async Task UpdateAsyncNotReturn(T entity)
        //{
        //    if (_context.Entry(entity).State == EntityState.Detached)
        //    {
        //        _dbSet.Attach(entity);
        //    }
        //    _dbSet.Update(entity);
        //}

        //public async Task<T> UpdateAsyncReturn(T entity)
        //{
        //    if (_context.Entry(entity).State == EntityState.Detached)
        //    {
        //        _dbSet.Attach(entity);
        //    }
        //    _dbSet.Update(entity);
        //    return entity;
        //}

        public void Update(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateAsyncNotReturn(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Entry(entity).State = EntityState.Modified;
        }

        public async Task<T> UpdateAsyncReturn(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        //Delete

        //public void Delete(T entity)
        //{
        //    if (_context.Entry(entity).State == EntityState.Detached)
        //    {
        //        _dbSet.Attach(entity);
        //    }
        //    _dbSet.Entry(entity).State = EntityState.Deleted;
        //}

        //public async Task DeleteAsyncNotReturn(T entity)
        //{
        //    if (_context.Entry(entity).State == EntityState.Detached)
        //    {
        //        _dbSet.Attach(entity);
        //    }
        //    _dbSet.Entry(entity).State = EntityState.Deleted;
        //}

        //public async Task<T> DeleteAsyncReturn(T entity)
        //{
        //    if (_context.Entry(entity).State == EntityState.Detached)
        //    {
        //        _dbSet.Attach(entity);
        //    }
        //    _dbSet.Entry(entity).State = EntityState.Deleted;
        //    return entity;
        //}
        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public void DeleteById(object id)
        {
            T entityFromDB = _dbSet.Find(id);
            Delete(entityFromDB);
        }

        public async Task DeleteByIdAsync(object id)
        {
            T entityFromDB = await _dbSet.FindAsync(id);
            await DeleteAsyncNotReturn(entityFromDB);
        }

        public async Task DeleteAsyncNotReturn(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public async Task<T> DeleteAsyncReturn(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            return entity;
        }

        //Dispose
        #region IDisposable Members
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
