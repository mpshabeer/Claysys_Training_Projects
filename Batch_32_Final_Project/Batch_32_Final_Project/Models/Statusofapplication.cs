using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Batch_32_Final_Project.Models
{
    public class Statusofapplication
    {
        [Display(Name = "Status")]
        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        [DataType(DataType.Date)]
        public string Interviewdate { get; set; }
    }
}