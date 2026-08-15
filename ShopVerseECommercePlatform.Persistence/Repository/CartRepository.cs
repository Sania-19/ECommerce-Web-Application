using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using ShopVerseECommercePlatform.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using ShopVerseECommercePlatform.Persistence.Repository;
using ShopVerseECommercePlatform.Domain.Entities;
using ShopVerseECommercePlatform.Application.RRModels.Cart;

namespace ShopVerseECommercePlatform.Persistence.Repository
{
    public class CartRepository(ShopVerseDbContext context) : BaseRepository<Cart>(context), ICartRepository
    {
        public async Task<IEnumerable<CartResponse>> GetCartWithItems(Guid userId)
        {
            var cartItems = await context.Database.SqlQuery<CartResponse>($@"
                SELECT
                C.Id AS CartId,
                C.UserId,
                C.TotalAmount,
          (
            SELECT
            CI.Id AS CartItemId,
            CI.ProductDetailId,
            PD.ProductId,
            P.Title,
            P.Brand,
            PD.FilePath,
            CI.Price,
            CI.Discount,
            CI.FinalPrice,
            CI.Quantity
        FROM CartItems CI
        INNER JOIN ProductDetails PD
            ON PD.Id = CI.ProductDetailId
        INNER JOIN Products P
            ON P.Id = PD.ProductId
        WHERE CI.CartId = C.Id
        FOR JSON PATH
    ) AS CartItemResponse

FROM Carts C
WHERE C.UserId = {userId}").ToListAsync();

            return cartItems;
        }
    }
}
