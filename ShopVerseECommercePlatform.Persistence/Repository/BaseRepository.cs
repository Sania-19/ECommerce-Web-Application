using Microsoft.EntityFrameworkCore;
using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using ShopVerseECommercePlatform.Domain.Entities;
using ShopVerseECommercePlatform.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ShopVerseECommercePlatform.Persistence.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        private readonly ShopVerseDbContext context;

        public BaseRepository(ShopVerseDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(T entity)
        {
            await context.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await context.AddRangeAsync(entities);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() => context.Set<T>().Count(expression));
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => context.Remove(entity));

        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = new T()
            {
                Id = id
            };
            await Task.Run(() => context.Remove(entity));
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            await Task.Run(() => context.RemoveRange(entities));
        }

        public async Task DeleteRangeAsync(IEnumerable<Guid> ids)
        {
            var models = new List<T>();
            foreach (var id in ids)
            {
                var model = new T()
                {
                    Id = id
                };
                models.Add(model);
            }
            ;
            await Task.Run(() => context.RemoveRange(models));
        }

        public async Task<IQueryable<T>> FindByAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() => context.Set<T>().Where(expression));
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() => context.Set<T>().FirstOrDefault(expression));
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        {
            return await context.Set<T>().AnyAsync(expression);
        }

        public async Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() => context.Set<T>().LastOrDefault(expression));
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() => context.Update(entity));
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            await Task.Run(() => context.UpdateRange(entities));
        }

    }
}
