using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace ShopVerseECommercePlatform.Application.RRModels.Category
{
    public class CategoryRequest
    {
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "FilePath is Required")]
        public IFormFile File { get; set; }
    }
}
