using Localization.Content;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Localization.Models {
    public class Data {
        public int ID { get; set; }

        [Required(ErrorMessageResourceName = "RequiredValidationMessage", ErrorMessageResourceType = typeof(LocalizationText))]
        public string Name { get; set; }
    }
}