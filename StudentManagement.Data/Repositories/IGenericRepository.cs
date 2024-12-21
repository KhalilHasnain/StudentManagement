using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Data.Repositories
{
    public interface IGenericRepository<T> : IDisposable
    {
        //GetAll
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeEntities = "");
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeEntities = "");

        //GetSingle
        T GetById(object id);
        Task<T> GetByIdAsync(object id);

        //Add
        void Add(T entity);
        Task<T> AddAsyncReturn(T entity);
        Task AddAsyncNotReturn(T entity);

        //Delete
        void DeleteById(object id);
        void Delete(T entity);
        Task DeleteByIdAsync(object id);
        Task<T> DeleteAsyncReturn(T entity);
        Task DeleteAsyncNotReturn(T entity);

        //Update
        void Update(T entity);
        Task<T> UpdateAsyncReturn(T entity);
        Task UpdateAsyncNotReturn(T entity);
    }
}
