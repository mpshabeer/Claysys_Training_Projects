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

        [Display(Name = "Job Title")]
        [Required(ErrorMessage = "Job Title is required")]
        public string JobTitle { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Job Description")]
        [Required(ErrorMessage = "Job Description is required")]
        public string JobDescription { get; set; }

        [Display(Name = "Department")]
        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Display(Name = "Vacancy Status")]
        [Required(ErrorMessage = "Vacancy Status is required")]
        public string VacancyStatus { get; set; }

        
        [Display(Name = "Number Of Openings")]
        [Required(ErrorMessage = "Number Of Openings is required")]
        public string NumberOfOpenings { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Qualifications")]
        [Required(ErrorMessage = "Qualification is required")]
        public string Qualifications { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Responsibilities And Duties")]
        [Required(ErrorMessage = "Responsibilities And Duties is required")]
        public string ResponsibilitiesAndDuties { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Salary Range")]
        [Required(ErrorMessage = "Salary Range is required")]
        public string SalaryRange { get; set; }

        public string PostedDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Last Date To Apply")]
        [Required(ErrorMessage = "Last Date To Apply is required")]
        public string LastDateToApply { get; set; }

        [Display(Name = "Recruiter InCharge")]
        [Required(ErrorMessage = "Recruiter InCharge is required")]
        public string RecruiterInCharge { get; set; }

        [Display(Name = "Interview Rounds")]
        [Required(ErrorMessage = "Interview Rounds is required")]
        public string InterviewRounds { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Selection Process")]
        [Required(ErrorMessage = "Selection Process is required")]
        public string SelectionProcess { get; set; }

        public string Createdby { get; set; }
    }
}