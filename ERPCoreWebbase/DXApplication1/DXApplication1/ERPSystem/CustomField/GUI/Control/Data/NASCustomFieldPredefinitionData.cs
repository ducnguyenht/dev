using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.ERPSystem.CustomField.GUI.Control.Data
{
    public class NASCustomFieldPredefinitionData
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid RefId { get; set; }
        public string PredefinitionType { get; set; }
    }
}