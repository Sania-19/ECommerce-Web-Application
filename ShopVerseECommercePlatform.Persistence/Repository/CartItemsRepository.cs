using Microsoft.EntityFrameworkCore;
using ShopVerseECommercePlatform.Domain.Entities;
using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using ShopVerseECommercePlatform.Persistence.Data;
using ShopVerseECommercePlatform.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Persistence.Repository
{
    public class CartItemsRepository(ShopVerseDbContext context) : BaseRepository<CartItem>(context), ICartItemsRepository
    {
        

        public async Task<List<CartItem>> GetCartItems(Guid cartId)
        {
            return await context.CartItems
                .Where(x => x.CartId == cartId)
                .ToListAsync();
        }

        //public async Task<Result<IEnumerable<CartItemResponse>>> GetCartItems()
        //{



        //    var cartItems = await context.Database.SqlQuery<CartItemResponse>($@"SELECT C.TotalAmount,CI.Id,CI.ProductDetailId,CI.Quantity,CI.FinalPrice,Price,Discount,Quantity 
        //                                                                        From Carts C
        //                                                                        INNER JOIN CartItems CI
        //                                                                        ON C.Id=CI.CartId
        //                                                                        ").ToListAsync();


        //    return cartItems;
        //}
    }
}
