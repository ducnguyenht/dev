using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.System.ArtifactCode.ArtifactCodeFormater
{
    public class CodeRuleData
    {
        
        private CodeRuleDataFormater Formater { get; set; }

        public CodeRuleData()
        {
        }

        public CodeRuleData(CodeRuleDataFormater formater) 
        {
            this.Formater = formater;
        }

        public object Value { get; set; }
        public string FormatString { get; set; }

        private string FormatData(CodeRuleData data)
        {
            return Formater.FormatData(data);
        }

        public string GetFormatedValue()
        {
            return FormatData(this);
        }

        public void UseFormater(CodeRuleDataFormater formater) 
        {
            this.Formater = formater;
        }

    }
}
