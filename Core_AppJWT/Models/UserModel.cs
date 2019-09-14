using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core_AppJWT.Models
{
   

    public class LoginUser
    {
        [Required(ErrorMessage ="User Name is Must")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Must")]
        public string Password { get; set; }
    }

    public class RegisterUser
    {
        [Required(ErrorMessage = "Email is Must")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Must")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", 
            ErrorMessage = "Passwords must be minimum 8 characters and can contain upper case, lower case, number (0-9) and special character")]
        public string  Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
