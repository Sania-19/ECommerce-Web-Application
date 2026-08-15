using ShopVerseECommercePlatform.Application.RRModels.Address;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.RRModels.Address
{
    public class AddressResponse : AddressRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
