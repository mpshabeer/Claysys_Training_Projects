using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Batch_32_Final_Project.Models
{
    public class Application
    {
        public int aid { get; set; }

        [Display(Name = "Your Resume")]

        public string Resume { get; set; }

        [Display(Name = "Your Photo")]

        public string Photo { get; set; }

        [Display(Name = "Experiance")]
  
        public string Experiance { get; set; }

        [Display(Name = "Highest Qualification")]
   
        public string Qualification { get; set; }
        public string Description { get; set; }

          [Display(Name = "Status")]

        public string Status { get; set; }

        [DataType(DataType.Date)]
        public string Interviewdate { get; set; }
    }
}