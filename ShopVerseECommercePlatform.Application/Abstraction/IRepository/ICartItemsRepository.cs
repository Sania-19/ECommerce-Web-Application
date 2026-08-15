using ShopVerseECommercePlatform.Domain.Entities;
using ShopVerseECommercePlatform.Application.Abstraction.IContextService;
using ShopVerseECommercePlatform.Application.Utils.Result;
using ShopVerseECommercePlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.Abstraction.IRepository
{
    public interface ICartItemsRepository : IBaseRepository<CartItem>
    {
       Task<List<CartItem>> GetCartItems(Guid cartId);

       //Task<Result<IEnumerable<CartItemResponse>>> GetCartItems();
    }
}
