using System;
using System.Collections.Generic;
using System.Text;
using ShopVerseECommercePlatform.Domain;
using Microsoft.AspNetCore.Http;
using static ShopVerseECommercePlatform.Domain.Enum;

namespace ShopVerseECommercePlatform.Application.RRModels.Product
{
    public class ProductRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Units Units { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public IFormFile File{ get; set; }
    }
}
