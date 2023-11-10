using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Batch_32_Final_Project.Models
{
    public class Login
    {
       

        [DataType(DataType.EmailAddress)]

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter your email address")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
    }
}