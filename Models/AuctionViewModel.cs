using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace beltprep.Models
{
    public class AuctionViewModel : BaseEntity
    {
        public int AuctionId { get; set; }

        [Required(ErrorMessage="Product length must be greater than 3 characters")]
        [MinLength(3)]
        public string ProductName { get; set; }

        [Required(ErrorMessage="Description length must be greater thatn 10 characters")]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public Decimal StartAmount { get; set; }

        [Required(ErrorMessage="End date must be in the future")]
        [DataType(DataType.Date)]
        [FutureDate]
        public DateTime EndDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public bool IsEnded { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null && (DateTime)value > DateTime.Now;
        }
    }
}