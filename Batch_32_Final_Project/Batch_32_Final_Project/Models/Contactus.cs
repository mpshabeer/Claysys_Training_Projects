using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Batch_32_Final_Project.Models
{
    public class Contactus
    {
        [Key]
        public int cid { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "E-mail is required")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Please enter 10 digit Mobile No.")]
        [Required(ErrorMessage = "Phone Number is required")]
        public string Phone { get; set; }



        [Display(Name = "Command")]
        public string Command { get; set; }



    }
}