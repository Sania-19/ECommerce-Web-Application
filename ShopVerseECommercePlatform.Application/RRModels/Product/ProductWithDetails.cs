using ShopVerseECommercePlatform.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.RRModels.Product
{
    public class ProductWithDetails
    {
        public Guid ProductId { get; set; }
        public ProductRequest ProductRequest { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid ProductDetailId { get; set; }
    }
}
