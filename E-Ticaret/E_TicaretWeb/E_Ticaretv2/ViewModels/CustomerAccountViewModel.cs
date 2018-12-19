using E_Ticaretv2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Ticaretv2.ViewModels
{
    public class CustomerAccountViewModel
    {

    }
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "E Mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Compare("Password", ErrorMessage = "Passwords are different")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "PHone Number")]
        [StringLength(11, ErrorMessage = "Phone number's length should be 11.")]
        public string PhoneNumber { get; set; }

    }

    public class ChangePasswordModel
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword",ErrorMessage ="New passwords are not equal")]
        public string NewPassword2 { get; set; }
    }

}