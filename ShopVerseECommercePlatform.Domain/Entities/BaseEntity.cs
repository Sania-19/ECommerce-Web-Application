using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.Now;
    }
}
