using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}
