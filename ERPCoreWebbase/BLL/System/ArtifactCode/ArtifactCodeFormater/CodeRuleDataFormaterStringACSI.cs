using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.System.ArtifactCode.ArtifactCodeFormater
{
    public class CodeRuleDataFormaterStringACSI : CodeRuleDataFormaterStringMixed
    {

        public CodeRuleDataFormaterStringACSI(CodeRuleDataFormaterString mixedFormater)
            : base(mixedFormater)
        {
        }

        public CodeRuleDataFormaterStringACSI()
        {
        }

        public override string FormatData(CodeRuleData data)
        {
            return base.FormatData(data);
        }
        protected override bool ValidateData(CodeRuleData data)
        {
            try
            {
                bool isValid = base.ValidateData(data);
                if (isValid)
                {
                    string sOut = 
                        Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(data.Value.ToString()));
                    if (sOut.Equals(data.Value.ToString()))
                    {
                        isValid = true;
                    }
                    else
                    {
                        isValid = false;
                    }
                }
                return isValid;
            }
            catch (Exception)
            {
                return false;
            }
            
            
        }
    }
}
