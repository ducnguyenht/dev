using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.System.ArtifactCode.ArtifactCodeFormater
{
    public class CodeRuleDataTypeNumber : CodeRuleDataType
    {
        public override bool ValidateDataType(CodeRuleData data)
        {
            int temp;
            return int.TryParse(data.Value.ToString(), out temp);
        }
    }
}
