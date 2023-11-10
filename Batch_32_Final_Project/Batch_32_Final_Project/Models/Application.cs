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

        [Display(Name = "Upload resume")]

        public string Resume { get; set; }

        [Display(Name = "Upload photo")]
        public string Photo { get; set; }
        [Display(Name = "Upload acadamic certificate")]
        public string Acadamiccertificate { get; set; }
        [Required(ErrorMessage = "Experience  required")]
        [Display(Name = "Experience")]
        public string Experiance { get; set; }

        [Display(Name = "Experience certificate if any")]
        public string Experiancecertificate { get; set; }

        [Display(Name = "Highest qualification")]
        public string Qualification { get; set; }
        public string AppliedDate { get; set; }
        public string Description { get; set; }

          [Display(Name = "Status")]

        public string Status { get; set; }

        [DataType(DataType.Date)]
        public string Interviewdate { get; set; }
    }
}