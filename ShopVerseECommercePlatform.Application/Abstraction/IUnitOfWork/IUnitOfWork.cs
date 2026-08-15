using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ShopVerseECommercePlatform.Application.Abstraction.IUnitOfWork
{
    public interface IUnitOfWork
    {
        IDbTransaction BeginTransaction();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
