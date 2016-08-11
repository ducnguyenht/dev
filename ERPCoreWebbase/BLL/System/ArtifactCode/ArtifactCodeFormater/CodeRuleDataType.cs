using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.System.ArtifactCode.ArtifactCodeFormater
{
    public abstract class CodeRuleDataType
    {
        public abstract bool ValidateDataType(CodeRuleData data);
    }
}
