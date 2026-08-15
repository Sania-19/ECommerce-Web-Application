using System;
using System.Collections.Generic;
using System.Text;
using ShopVerseECommercePlatform.Domain;
using static ShopVerseECommercePlatform.Domain.Enum;

namespace ShopVerseECommercePlatform.Application.RRModels.Product
{
    public class UpdateProductRequest
    {
        public Guid Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Brand { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public Units Units { get; set; }
    }
}
