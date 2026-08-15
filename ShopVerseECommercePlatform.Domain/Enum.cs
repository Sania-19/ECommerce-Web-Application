using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Domain
{
    public class Enum
    {
        public enum UserStatus
        {
            Active = 1,
            Inactive = 2,
            Blocked = 3
        }

        public enum UserRole
        {
            SuperAdmin = 1,
            Admin = 2,
            Customer = 3,
        }

        public enum Units
        {
            Piece = 1,
            Gram = 2,
            KG = 3,
            Litre = 4,
            Dozen = 5,
            Meter = 6,
            Pair = 7,
        }

        public enum AppModule
        {
            User = 1,
            Product = 2,
        }
    }
}
