using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using ShopVerseECommercePlatform.Application.Abstraction.IServices;
using ShopVerseECommercePlatform.Application.RRModels.Cart;
using ShopVerseECommercePlatform.Domain.Entities;
using Microsoft.AspNetCore.Http;
using ShopVerseECommercePlatform.Application.Abstraction.IContextService;
using ShopVerseECommercePlatform.Application.Abstraction.IRepository;
using ShopVerseECommercePlatform.Application.Abstraction.IUnitOfWork;
using ShopVerseECommercePlatform.Application.Utils.Result;


namespace ShopVerseECommercePlatform.Application.Services
{
    public class CartService(ICartRepository cartRepository, ICartItemsRepository cartItemsRepository,
                             IContextService contextService, IUnitOfWork unitOfWork, IProductDetailsRepository productDetailsRepository) : ICartService
    {
        public async Task<Result<CartResponse>> AddToCart(CartRequest model)
        {
            Guid userId = contextService.GetId();

            var cart = await cartRepository
            .FirstOrDefaultAsync(x => x.UserId == userId);

            if (cart is null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    TotalAmount = 0,
                };
                await cartRepository.AddAsync(cart);
                await unitOfWork.SaveChangesAsync();
            }

            var product = await productDetailsRepository.GetByIdAsync(model.ProductDetailId);
            if (product is null)
                return Result<CartResponse>.Failure("Product Not Found");

            var cartItem = await cartItemsRepository.FirstOrDefaultAsync(x =>
               x.CartId == cart.Id &&
               x.ProductDetailId == model.ProductDetailId);

            decimal finalPrice = (product.Price - (product.Price * product.Discount / 100)) * model.Quantity;

            if (cartItem is null)
            {
                cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductDetailId = model.ProductDetailId,
                    Price = product.Price,
                    Discount = product.Discount,
                    FinalPrice = finalPrice,
                    Quantity = model.Quantity,
                };
                await cartItemsRepository.AddAsync(cartItem);
            }
            else
            {
                cartItem.Quantity += model.Quantity;
                cartItem.FinalPrice = (cartItem.Price - (cartItem.Price * cartItem.Discount / 100)) * cartItem.Quantity;

                await cartItemsRepository.UpdateAsync(cartItem);
            }

            await unitOfWork.SaveChangesAsync();
            var cartItems = await cartItemsRepository.GetCartItems(cart.Id);

            cart.TotalAmount = cartItems.Sum(x => x.FinalPrice);

            await cartRepository.UpdateAsync(cart);

            int returnVal = await unitOfWork.SaveChangesAsync();

            CartResponse cartResponse = new()
            {
                CartId = cart.Id,
                TotalAmount = cart.TotalAmount,
                UserId = userId,
            };
            if (returnVal > 0)
            {
                return Result<CartResponse>.Success(cartResponse, "Successfully Added To Cart", StatusCodes.Status200OK);
            }
            return Result<CartResponse>.Failure("There is some issue, Please try agan later!", StatusCodes.Status500InternalServerError);
        }

        public Task<Result<string>> DeleteCartItem(Guid cartId)
        {
            throw new NotImplementedException();
        }


        public async Task<Result<IEnumerable<CartResponse>>> GetCartWithItems()
        {
            Guid userId = contextService.GetId();

            var cartItems= await cartRepository.GetCartWithItems(userId);
            if (cartItems is null)
            {
                return Result<IEnumerable<CartResponse>>.Failure("Cart is empty", StatusCodes.Status404NotFound);

            }
            return Result<IEnumerable<CartResponse>>.Success(cartItems);

        }

        //public async Task<Result<IEnumerable<CartItemResponse>>> GetCartItems()
        //{
        //    var cartItems = await cartItemsRepository.GetCartItemsByCartId(cartId);
        //    return cartItems;
        //}

        public Task<Result<CartResponse>> UpdateCartItem(CartUpdateRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
