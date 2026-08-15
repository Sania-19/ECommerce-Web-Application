using ShopVerseECommercePlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static ShopVerseECommercePlatform.Domain.Enum;

namespace ShopVerseECommercePlatform.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Units Units { get; set; }
        public bool IsActive { get; set; } = true;
        public bool DeactivatedByCategoryDelete { get; set; }
        public Guid CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;
        public ICollection<ProductDetails> ProductDetails { get; set; } = null!;

    }
}
