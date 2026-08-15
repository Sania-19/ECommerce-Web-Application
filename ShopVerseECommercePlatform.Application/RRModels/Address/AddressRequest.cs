using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.RRModels.Address
{
    public class AddressRequest
    {
        public string AddressLine { get; set; } = string.Empty;
        public string Landmark { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public int Pincode { get; set; }
        public string? ContactNo { get; set; }

    }
}
