using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Project.Models
{
    public class ProductModel
    {
        [Key]
        public int  Productid { get; set; }
        [Required]
        [DisplayName("Poduct Name")]
        public string Productname { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Qty { get; set; }
    }
}