using ShopVerseECommercePlatform.Application.RRModels.ProductDetails;
using ShopVerseECommercePlatform.Domain;
using ShopVerseECommercePlatform.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using static ShopVerseECommercePlatform.Domain.Enum;

namespace ShopVerseECommercePlatform.Application.RRModels.Product
{
    public class ProductResponseWithDetails
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Units Units { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public Guid ProductDetailId { get; set; }
    }


    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Units Units { get; set; }
        public Guid CategoryId { get; set; }
    }


    public class ProductResponseWithJsonResult
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Units Units { get; set; }
        public Guid CategoryId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

        public string? ProductDetailsJson { get; set; }

        [NotMapped]
        public List<ProductDetailsResponse> ProductDetails =>
            string.IsNullOrEmpty(ProductDetailsJson) ? null : JsonSerializer.Deserialize<List<ProductDetailsResponse>>(ProductDetailsJson);
    }

 

}
