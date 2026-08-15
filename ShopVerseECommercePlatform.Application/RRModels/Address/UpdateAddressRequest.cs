using ShopVerseECommercePlatform.Application.RRModels.Address;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.RRModels.Address
{
    public class UpdateAddressRequest : AddressRequest
    {
        public Guid Id { get; set; }
    }
}
