using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopVerseECommercePlatform.Domain.Entities
{
    public class Address : BaseEntity
    {
        [Required(ErrorMessage = "Address Line is Required!")]
        public string AddressLine { get; set; } = string.Empty;
        public string Landmark { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is Required!")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "State is Required!")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pincode is Required!")]
        public int Pincode { get; set; }
        public string? ContactNo { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User user { get; set; } = null!;
    }
}
