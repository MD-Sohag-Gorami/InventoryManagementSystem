﻿using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class SignUpUserModel
    {
        [Required(ErrorMessage ="Please enter your first name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter a confirm  password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }


    }
}
