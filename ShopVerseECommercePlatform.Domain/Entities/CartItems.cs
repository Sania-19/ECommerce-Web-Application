using ShopVerseECommercePlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopVerseECommercePlatform.Domain.Entities
{
    public class CartItem : BaseEntity
    {
        public Guid CartId { get; set; }

        public Guid ProductDetailId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal FinalPrice { get; set; }

        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; } = null!;

        [ForeignKey(nameof(ProductDetailId))]
        public ProductDetails ProductDetails { get; set; } = null!;
    }
}
