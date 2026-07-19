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
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> expression);
        Task<int> CountAsync(Expression<Func<T,bool>> expression);
        Task<bool> IsExistAsync(Expression<Func<T,bool>> expression);
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteAsync(T entity);
        Task<int> AddRangeAsync(IEnumerable<T> entities);
        Task<int> DeleteRangeAsync(IEnumerable<T> entities);
        Task<int> DeleteRangeAsync(IEnumerable<Guid> ids);
        Task<int> UpdateRangeAsync(IEnumerable<T> entities);
        
    }
}
