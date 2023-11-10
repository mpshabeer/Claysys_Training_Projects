using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Batch_32_Final_Project.Models
{
    public class Apply
    {
        [Key]
        public int aid { get; set; }
        public int vid { get; set; }
        public int rid { get; set; }

        [Display(Name = "Upload resume")]

        public string Resume { get; set; }

        [Display(Name = "Upload photo")]
       
        public string Photo { get; set; }

        [Display(Name = "Experience ")]
        [Required(ErrorMessage = "Experience  required")]
        public string Experiance { get; set; }

        [Display(Name = "Experience certificate if any")]
        public string Experiancecertificate { get; set; }

        [Display(Name = "Highest qualification certificate")]
        [Required(ErrorMessage = "Acadmic qualification certificate  required")]
        public string Acadamiccertificate { get; set; }

        [Display(Name = "Highest Qualification")]
        [Required(ErrorMessage = "Qualification required")]
        public string Qualification { get; set; }
        public string Description { get; set; }
        public string AppliedDate { get; set; }

        [ForeignKey("vid")] 
        public Vacancy Vacancy { get; set; }

        [ForeignKey("rid")]
        public Registration Registration { get; set; }

    }
}