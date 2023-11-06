using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Batch_32_Final_Project.Models
{
    public class Alldetailsofapplication
    {

            
        public int aid { get; set; }
        public int vid { get; set; }
        public int rid { get; set; }
        
        public string JobTitle { get; set; }

        public string JobDescription { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Qualifications { get; set; }
        public string Qualification { get; set; }
        public string Department { get; set; }

        public string Responsibilities { get; set; }
        public string Resume { get; set; }

        public string Photo { get; set; }

        public string Experiance { get; set; }
        public string Description { get; set; }

        
        public string AppliedDate { get; set; }

        public string Status { get; set; }
        [DataType(DataType.Date)]
        public string Interviewdate { get; set; }

    }
    }
