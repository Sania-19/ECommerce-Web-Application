using ShopVerseECommercePlatform.Application.RRModels.Cart.CartItem;
using ShopVerseECommercePlatform.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json;

namespace ShopVerseECommercePlatform.Application.RRModels.Cart
{
    public class CartResponse
    {
        public Guid CartId { get; set; }

        public Guid UserId { get; set; }

        public decimal TotalAmount { get; set; }

        //public List<CartItemResponse> Items { get; set; } = new();
        public string? CartItemResponse { get; set; }

        [NotMapped]
        public List<CartItemResponse> CartItems =>
            string.IsNullOrEmpty(CartItemResponse) ? null : JsonSerializer.Deserialize<List<CartItemResponse>>(CartItemResponse);
    }
}
