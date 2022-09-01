using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class SignUpUserModel
    {
        [Required(ErrorMessage = "Please enter your email")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm a password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


    }
}
