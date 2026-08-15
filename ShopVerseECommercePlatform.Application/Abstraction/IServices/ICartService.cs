using ShopVerseECommercePlatform.Application.RRModels.Cart;
using ShopVerseECommercePlatform.Application.Utils.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.Abstraction.IServices
{
    public interface ICartService
    {
        Task<Result<CartResponse>> AddToCart(CartRequest model);
        Task<Result<CartResponse>> UpdateCartItem(CartUpdateRequest model);
        Task<Result<string>> DeleteCartItem(Guid cartId);

        //Task<Result<IEnumerable<CartItemResponse>>> GetCartItemsByCartId();
        Task<Result<IEnumerable<CartResponse>>> GetCartWithItems();
    }
}
