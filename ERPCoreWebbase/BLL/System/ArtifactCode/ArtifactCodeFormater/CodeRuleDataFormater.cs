using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.System.ArtifactCode.ArtifactCodeFormater
{
    public abstract class CodeRuleDataFormater
    {
        public abstract string FormatData(CodeRuleData data);
        protected abstract bool ValidateData(CodeRuleData data);
    }
}
