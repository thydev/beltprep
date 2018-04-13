using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace beltprep.Models
{
    public class UserViewModel : BaseEntity
    {
        
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username must be greater than 3 and less than 20")]
        [MinLength(3)]
        [MaxLength(20)]
        public string UserName { get; set; }   

        [Required(ErrorMessage = "First name is required")]
        [Display(Name="First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name="Last name")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Password must be at least 8 characters long")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        [Display(Name="Confirm")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }

        
    }

}