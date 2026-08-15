using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.RRModels.Users
{
    public class UpdateUserRequest
    {
        public string Email { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
    }
}
