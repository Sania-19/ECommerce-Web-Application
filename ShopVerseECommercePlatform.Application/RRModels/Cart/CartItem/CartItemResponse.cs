using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.RRModels.Cart.CartItem
{
   public class CartItemResponse
{
    public Guid CartItemId { get; set; }

    public Guid ProductDetailId { get; set; }

    public Guid ProductId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public decimal Discount { get; set; }

    public decimal FinalPrice { get; set; }

    public int Quantity { get; set; }

    public decimal TotalAmount { get; set; }
}
}
