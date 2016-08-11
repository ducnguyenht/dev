using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.System.ArtifactCode.ArtifactCodeFormater
{
    public class CodeRuleDataTypeDateTime : CodeRuleDataType
    {
        public override bool ValidateDataType(CodeRuleData data)
        {
            DateTime temp;
            return DateTime.TryParse(data.Value.ToString(), out temp);
        }
    }
}
