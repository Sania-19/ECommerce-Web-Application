using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.RRModels.ProductDetails
{
    public class UpdateProductImageRequest
    {
        public IFormFileCollection Files { get; set; }
        public Guid ProductDetailId { get; set; }
    }
}
