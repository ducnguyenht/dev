using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.System.ArtifactCode;

namespace NAS.BO.System.ArtifactCode
{
    public class CodeRuleStringDataBO
    {
        public CodeRuleStringData Insert(Session session, Guid codeRuleDefinitionId, Guid codeRuleDataFormatId, string stringValue)
        {
            CodeRuleStringData codeRuleStringData = null;
            try
            {
                //Get CodeRuleDefinition
                CodeRuleDefinition codeRuleDefinition = session.GetObjectByKey<CodeRuleDefinition>(codeRuleDefinitionId);
                //Get CodeRuleDataFormat
                CodeRuleDataFormat codeRuleDataFormat = session.GetObjectByKey<CodeRuleDataFormat>(codeRuleDataFormatId);
                //Create new CodeRuleStringData
                codeRuleStringData = new CodeRuleStringData(session)
                {
                    CodeRuleDataFormatId = codeRuleDataFormat,
                    CodeRuleDataId = Guid.NewGuid(),
                    CodeRuleDefinitionId = codeRuleDefinition,
                    CreateDate = DateTime.Now,
                    RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                    StringValue = stringValue
                };
                codeRuleStringData.Save();
                return codeRuleStringData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public CodeRuleStringData Update(Session session, Guid codeRuleDefinitionId, Guid codeRuleDataFormatId, string stringValue)
        {
            CodeRuleStringData codeRuleStringData = null;
            try
            {
                CodeRuleDataBO codeRuleDataBO = new CodeRuleDataBO();
                //Check updating CodeRuleDataType already exist
                //Get CodeRuleDataFormat
                CodeRuleDataFormat codeRuleDataFormat =
                    session.GetObjectByKey<CodeRuleDataFormat>(codeRuleDataFormatId);

                //Get CodeRuleDefinition
                CodeRuleDefinition codeRuleDefinition =
                    session.GetObjectByKey<CodeRuleDefinition>(codeRuleDefinitionId);

                codeRuleStringData = (CodeRuleStringData)codeRuleDataBO.GetCodeRuleData(session,
                                            codeRuleDefinition.CodeRuleDefinitionId,
                                            codeRuleDataFormat.CodeRuleDataTypeId.CodeRuleDataTypeId);

                foreach (var item in codeRuleDefinition.CodeRuleData)
                {
                    item.RowStatus = Utility.Constant.ROWSTATUS_INACTIVE;
                    item.Save();
                }

                if (codeRuleStringData == null)
                {
                    codeRuleStringData = Insert(session, codeRuleDefinition.CodeRuleDefinitionId, codeRuleDataFormatId, stringValue);
                }
                else
                {
                    codeRuleStringData.StringValue = stringValue;
                    codeRuleStringData.CodeRuleDataFormatId = codeRuleDataFormat;
                    codeRuleStringData.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                    codeRuleStringData.Save();
                }

                return codeRuleStringData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
