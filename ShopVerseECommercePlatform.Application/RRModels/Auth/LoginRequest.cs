using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopVerseECommercePlatform.Application.RRModels.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; } = string.Empty;
    }
}
