using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Batch_32_Final_Project.Models
{
    public class Userdetails
    {
        public int rid { get; set; }

        [Display(Name = "Firstname")]
        [Required(ErrorMessage = "Firstname is required")]
        public string Firstname { get; set; }

        [Display(Name = "Laststname")]
        [Required(ErrorMessage = "Laststname is required")]
        public string Lastname { get; set; }

        [Display(Name = "Date of birth")]
        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public string Dateofbirth { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [DataType(DataType.PhoneNumber)]

        [Display(Name = "Phone number")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Please enter 10 digit Mobile No.")]

        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]

        [Display(Name = "Email")]
        [Required(ErrorMessage = "E-mail is required")]
        public string Email { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "state is required")]
        public string State { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [DataType(DataType.PostalCode)]
        [Display(Name = "Pincode")]
        [Required(ErrorMessage = "pincode is required")]
        public string Pincode { get; set; }
    }
}