using ShopVerseECommercePlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopVerseECommercePlatform.Domain.Entities
{
    public class ProductDetails : BaseEntity
    {
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public int QuantitySold { get; set; } = 0;

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
    }
}
