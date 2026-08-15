using ShopVerseECommercePlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ShopVerseECommercePlatform.Application.Abstraction.IRepository
{
    public interface IBaseRepository<T> where T : BaseEntity, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task<IQueryable<T>> FindByAsync(Expression<Func<T, bool>> expression);
        Task<int> CountAsync(Expression<Func<T,bool>> expression);
        Task<bool> IsExistAsync(Expression<Func<T,bool>> expression);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        Task DeleteRangeAsync(IEnumerable<Guid> ids);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        
    }
}
