using ShopVerseECommercePlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static ShopVerseECommercePlatform.Domain.Enum;

namespace ShopVerseECommercePlatform.Domain.Entities
{
    public class AppFiles:BaseEntity
    {
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public bool IsVideo { get; set; } = false;
        public AppModule AppModule { get; set; } 
        public Guid EntityId { get; set; }
    }
}
