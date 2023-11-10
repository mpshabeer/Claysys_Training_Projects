using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Batch_32_Final_Project.Models
{
    public class Registration
    {
        [Key]
        public int rid {  get; set; }

        [Display(Name = "Firstname")]
        [Required(ErrorMessage ="Firstname is required")]
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

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string Confirmpassword { get; set; }


    }
}