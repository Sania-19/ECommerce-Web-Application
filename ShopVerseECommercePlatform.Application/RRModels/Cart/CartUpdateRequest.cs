using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.RRModels.Cart
{
    public class CartUpdateRequest
    {
        public Guid CartItemId { get; set; }

        public int Quantity { get; set; }
    }
}
