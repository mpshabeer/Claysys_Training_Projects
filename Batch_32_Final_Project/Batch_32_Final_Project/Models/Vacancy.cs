using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Batch_32_Final_Project.Models
{
    public class Vacancy
    {
        [Key]
        public int vid { get; set; }

        [Display(Name = "Job title")]
        [Required(ErrorMessage = "Job title is required")]
        public string JobTitle { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Job description")]
        [Required(ErrorMessage = "Job description is required")]
        public string JobDescription { get; set; }

        [Display(Name = "Department")]
        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Display(Name = "Vacancy Status")]
        [Required(ErrorMessage = "Vacancy status is required")]
        public string VacancyStatus { get; set; }

        
        [Display(Name = "Number of openings")]
        [Required(ErrorMessage = "Number of openings is required")]
        public string NumberOfOpenings { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Qualifications")]
        [Required(ErrorMessage = "Qualification is required")]
        public string Qualifications { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Responsibilities and duties")]
        [Required(ErrorMessage = "Responsibilities and duties is required")]
        public string ResponsibilitiesAndDuties { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Salary range")]
        [Required(ErrorMessage = "Salary range is required")]
        public string SalaryRange { get; set; }

        public string PostedDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Last date to apply")]
        [Required(ErrorMessage = "Last date to apply is required")]
        public string LastDateToApply { get; set; }

        [Display(Name = "Recruiter incharge")]
        [Required(ErrorMessage = "Recruiter incharge is required")]
        public string RecruiterInCharge { get; set; }

        [Display(Name = "Interview rounds")]
        [Required(ErrorMessage = "Interview rounds is required")]
        public string InterviewRounds { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Selection process")]
        [Required(ErrorMessage = "Selection process is required")]
        public string SelectionProcess { get; set; }

        public string Createdby { get; set; }
    }
}