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

        #region CREATE
        public async Task<int> AddAsync(T entity)
        {
            await context.AddAsync(entity);
            var returnVal = await context.SaveChangesAsync();
            return returnVal;
        }

        public async Task<int> AddRangeAsync(IEnumerable<T> entities)
        {
            await context.AddRangeAsync(entities);
            return await context.SaveChangesAsync();
        }
        #endregion

        #region READ
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return  await context.Set<T>().FindAsync(id);
        }

        public async Task<T> FirstOrDefaultAsync(Expression <Func<T,bool>> expression)
        {
            return await context.Set<T>().FirstOrDefaultAsync(expression);
        }
        public async Task<T> LastOrDefaultAsync(Expression <Func<T,bool>> expression)
        {
            return await context.Set<T>().LastOrDefaultAsync(expression);
        }
        public async Task<IEnumerable<T>> FindByAsync(Expression<Func<T,bool>> expression)
        {
            return await context.Set<T>().Where(expression).ToListAsync();
        }
        public async Task<int> CountAsync(Expression<Func<T,bool>> expression)
        {
            return await context.Set<T>().CountAsync(expression);
        }
        public async Task<bool> IsExistAsync(Expression<Func<T,bool>> expression)
        {
            return await context.Set<T>().AnyAsync(expression);
        }
        #endregion

        #region UPDATE
        public async Task<int> UpdateAsync(T entity)
        {
            context.Update(entity);
            var returnVal = await context.SaveChangesAsync();
            return returnVal;
        }

        public async Task<int> UpdateRangeAsync(IEnumerable<T> entities)
        {
            context.UpdateRange(entities);
            return await context.SaveChangesAsync();
        }
        #endregion

        #region DELETE
        //Delete using Entity
        public async Task<int> DeleteAsync(T entity)
        {
            await Task.Run(() => context.Remove(entity));
            var returnVal = await context.SaveChangesAsync();
            return returnVal;
        }

        //Delete using Primary Key (id)
        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = new T() { Id = id };
            context.Remove(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteRangeAsync(IEnumerable<T> entities)
        {
            context.RemoveRange(entities);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteRangeAsync(IEnumerable<Guid> ids)
        {
            List<T> entities = new List<T>();
            foreach (var id in ids)
            {
                var entity = new T { Id = id };
                entities.Add(entity);
            }

            context.RemoveRange(entities);
            return await context.SaveChangesAsync();
        }
        #endregion


    }
}
