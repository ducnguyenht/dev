using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCLargeDB01.Models
{
    public class Premise
    {
        public int PremiseID { get; set; }

        [Required]
        [Display(Name = "Premises Code")]
        public string Code { get; set; }

        [Required(ErrorMessage="Warehouse No is required")]
        [Display(Name = "Warehouse No")]
        public string Whse_No { get; set; }

        [Required]
        [Display(Name = "Warehouse Type")]
        public string Whse_Type { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Address Line 1")]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Display(Name = "Post Code")]
        public string Post_Code { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int CountryID { get; set; }

        [Required]
        public string System_Code { get; set; }

        public virtual Country Country { get; set; }


    }
}