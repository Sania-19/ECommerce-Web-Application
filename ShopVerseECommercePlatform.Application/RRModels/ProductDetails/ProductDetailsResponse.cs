
using ShopVerseECommercePlatform.Application.RRModels.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json;
using static ShopVerseECommercePlatform.Domain.Enum;

namespace ShopVerseECommercePlatform.Application.RRModels.ProductDetails
{
    public class ProductDetailsResponse
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
    public class ProductFullDetailsWithFilesResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Units Units { get; set; }
        public Guid CategoryId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }


        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string FilePath { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public int Quantity { get; set; }

        public string? AppFilesJson { get; set; }
        public string? ProductDetailsJson { get; set; }

        [NotMapped]
        public List<FileResponse> AppFiles =>
            string.IsNullOrEmpty(AppFilesJson) ? null : JsonSerializer.Deserialize<List<FileResponse>>(AppFilesJson);
        [NotMapped]
        public List<ProductDetailsResponse> ProductDetails =>
            string.IsNullOrEmpty(ProductDetailsJson) ? null : JsonSerializer.Deserialize<List<ProductDetailsResponse>>(ProductDetailsJson);
    }
}

