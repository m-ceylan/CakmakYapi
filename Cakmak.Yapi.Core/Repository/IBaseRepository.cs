using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cakmak.Yapi.Core.Repository
{
   public interface IBaseRepository<T>
    {
        IMongoQueryable<T> GetAll();
        IMongoQueryable<T> GetBy(System.Linq.Expressions.Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(string id);
        Task<T> FirstOrDefaultByAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity, bool forceDates = false);
        Task<List<T>> AddAsync(List<T> entities);
        Task UpdateAsync(T entity, bool forceDates = false);
        Task UpdateAsync(string id, T entity);
        Task DeleteAsync(T entity);
        Task DeleteAsync(string id);
        Task<DeleteResult> HardDeleteAsync(string id);
        Task<DeleteResult> HardDeleteManyAsync(FilterDefinition<T> filter);
        Task<UpdateResult> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, bool updateDate = true);
        Task<UpdateResult> DeleteManyAsync(FilterDefinition<T> filter);

    }
}
