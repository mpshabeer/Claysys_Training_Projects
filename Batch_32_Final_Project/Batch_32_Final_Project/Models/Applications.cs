using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Batch_32_Final_Project.Models
{
    public class Applications
    {
        public int id {  get; set; }

        [Display(Name = "Image")]

        public byte[] Imagefile { get; set; }
    }
}