using System;
using System.Collections.Generic;
using System.Text;

namespace ShopVerseECommercePlatform.Application.RRModels.Category
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FilePath { get; set; }= string.Empty;
        public string FileName { get; set; }= string.Empty;
        public string Description { get; set; }= string.Empty;
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}
