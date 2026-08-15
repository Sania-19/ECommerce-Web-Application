using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopVerseECommercePlatform.Application.RRModels.Auth
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Old Password is Required")]
        public string OldPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "New Password is Required")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm Password is Required")]
        [Compare(nameof(NewPassword), ErrorMessage = "New Password and Confirm Password are not matching")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
