using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCLargeDB01.Models
{
    public class Country
    {
        public int CountryID { get; set; }

        [Required]
        [Display(Name = "Country Code")]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual List<Premise> Premises { get; set; }
    }
}