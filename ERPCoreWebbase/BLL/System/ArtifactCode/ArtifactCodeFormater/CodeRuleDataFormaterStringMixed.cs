using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.System.ArtifactCode.ArtifactCodeFormater
{
    public abstract class CodeRuleDataFormaterStringMixed : CodeRuleDataFormaterString
    {

        private CodeRuleDataFormaterString mixedFormater;

        public CodeRuleDataFormaterStringMixed()
        {
        }

        public CodeRuleDataFormaterStringMixed(CodeRuleDataFormaterString mixedFormater)
        {
            this.mixedFormater = mixedFormater;
        }

        public override string FormatData(CodeRuleData data)
        {
            string ret = base.FormatData(data);
            if (mixedFormater != null)
            {
                data.Value = ret;
                ret = mixedFormater.FormatData(data);
            }
            return ret;
        }
        protected override bool ValidateData(CodeRuleData data)
        {
            bool isValid = base.ValidateData(data); ;
            if (mixedFormater != null)
            {
                if (isValid)
                {
                    mixedFormater.FormatData(data);
                }
            }
            return isValid;
        }
    }
}
