using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecuredSwashbuckleExample.Models
{
    public class User
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName {get; set;}

        [Required]
        [StringLength(100, ErrorMessage = "The {0} meust be at least {2} character long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "The pawssword and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
    }
}