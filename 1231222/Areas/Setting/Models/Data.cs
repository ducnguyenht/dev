using MVC.App_Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Areas.Setting.Models
{
    public class Data
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceName = "RequiredValidationMessage", ErrorMessageResourceType = typeof(Translate))]
        public string Name { get; set; }
    }
}