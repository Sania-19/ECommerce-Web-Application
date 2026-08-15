using ShopVerseECommercePlatform.Application.Abstraction.IUnitOfWork;
using ShopVerseECommercePlatform.Persistence.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ShopVerseECommercePlatform.Persistence.Repository
{
    public class UnitOfWork(ShopVerseDbContext context) : IUnitOfWork
    {
        public IDbTransaction BeginTransaction()
        {
            return context.Database.BeginTransaction().GetDbTransaction();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
