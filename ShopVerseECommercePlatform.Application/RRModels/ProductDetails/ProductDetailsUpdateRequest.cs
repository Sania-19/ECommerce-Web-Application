using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.RRModels.ProductDetails
{
    public class ProductDetailsUpdateRequest
    {
        //public Guid Id { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public IFormFile File { get; set; }
    }

}
