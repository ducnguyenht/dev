using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.System.ArtifactCode.ArtifactCodeFormater
{
    public class CodeRuleDataTypeString : CodeRuleDataType
    {
        public override bool ValidateDataType(CodeRuleData data)
        {
            return true;
        }
    }
}
