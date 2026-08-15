using ShopVerseECommercePlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.Abstraction.IJWTProvider
{
    public interface IJWTProvider
    {
        public string GenerateToken(User user);
    }
}
