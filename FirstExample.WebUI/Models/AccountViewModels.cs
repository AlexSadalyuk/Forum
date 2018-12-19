using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FirstExample.WebUI.Models
{
    public class AccountLogin
    {
        [Required(ErrorMessage = "Please, enter your username.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class AccountRegistration
    {
        [Required(ErrorMessage = "Please, enter your username.")]
        [StringLength(20, MinimumLength = 8,
            ErrorMessage = "Min length is 8. Max length is 20")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, enter your email.")]
        [EmailAddress(ErrorMessage = "Please, enter valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, enter password.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "(╯°□°）╯︵ ┻━┻ Minimal length is 8.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Incorrect validation password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password.")]
        public string ValidatePassword { get; set; }
    }
}