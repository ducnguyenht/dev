using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.System.ArtifactCode.ArtifactCodeFormater
{
    public class CodeRuleDataFormaterString : CodeRuleDataFormater
    {
        public override string FormatData(CodeRuleData data)
        {
            if (ValidateData(data))
            {
                return String.Format(data.FormatString, data.Value);
            }
            else
            {
                throw new Exception("Format is invalid");
            }
        }
        protected override bool ValidateData(CodeRuleData data)
        {
            CodeRuleDataTypeString dataType = new CodeRuleDataTypeString();
            return dataType.ValidateDataType(data);
        }
    }
}
