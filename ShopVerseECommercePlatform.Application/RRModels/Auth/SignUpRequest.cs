using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static ShopVerseECommercePlatform.Domain.Enum;

namespace ShopVerseECommercePlatform.Application.RRModels.Auth
{
    public class SignupRequest
    {
        [Required(ErrorMessage = "Email is Required")]
        //[RegularExpression("/^[a-zA-Z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,}$/", ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; } = null!;

        public string PhoneNo { get; set; } = string.Empty;


        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; } = string.Empty;


        [Required(ErrorMessage = "Confirm is Required")]
        [Compare(nameof(Password), ErrorMessage = "Password and confirm password doesnot match")]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
