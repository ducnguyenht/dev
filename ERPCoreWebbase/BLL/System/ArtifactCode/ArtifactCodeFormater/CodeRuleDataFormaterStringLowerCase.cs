using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.System.ArtifactCode.ArtifactCodeFormater
{
    public class CodeRuleDataFormaterStringLowerCase : CodeRuleDataFormaterString
    {
        public override string FormatData(CodeRuleData data)
        {
            string formatString = base.FormatData(data);
            return formatString.ToLower();
        }
        protected override bool ValidateData(CodeRuleData data)
        {
            return base.ValidateData(data);
        }
    }
}
