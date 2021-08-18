using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models.Custom
{
    public class CustomLogIn
    {
        [Required(ErrorMessage = "Please Enter UserName")]
        [StringLength(30, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}