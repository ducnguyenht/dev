using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.System.ArtifactCode.ArtifactCodeFormater
{
    public class CodeRuleDataFormaterDateTime : CodeRuleDataFormater
    {
        public override string FormatData(CodeRuleData data)
        {
            if (ValidateData(data))
            {
                return String.Format(data.FormatString, (DateTime)data.Value);
            }
            else
            {
                throw new Exception("Format is invalid");
            }
        }
        protected override bool ValidateData(CodeRuleData data)
        {
            CodeRuleDataTypeDateTime dataType = new CodeRuleDataTypeDateTime();
            return dataType.ValidateDataType(data);
        }
    }
}
