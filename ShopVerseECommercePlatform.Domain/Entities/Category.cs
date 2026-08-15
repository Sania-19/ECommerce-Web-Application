using ShopVerseECommercePlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public ICollection<Product> Products { get; set; } = null!;
    }
}
