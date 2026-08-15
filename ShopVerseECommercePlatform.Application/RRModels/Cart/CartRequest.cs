using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.RRModels.Cart
{
    public class CartRequest
    {
        public Guid ProductDetailId { get; set; }

        public int Quantity { get; set; }
    }


}
