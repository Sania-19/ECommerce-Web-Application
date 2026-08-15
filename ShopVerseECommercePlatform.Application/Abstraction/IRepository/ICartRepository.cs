using ShopVerseECommercePlatform.Application.RRModels.Cart;
using ShopVerseECommercePlatform.Domain.Entities;
using ShopVerseECommercePlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.Abstraction.IRepository
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
         Task<IEnumerable<CartResponse>> GetCartWithItems(Guid userId);

    }
}
