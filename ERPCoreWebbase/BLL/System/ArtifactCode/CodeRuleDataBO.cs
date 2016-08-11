using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.System.ArtifactCode;
using DevExpress.Xpo;

namespace NAS.BO.System.ArtifactCode
{
    public class CodeRuleDataBO
    {

        public CodeRuleData GetCodeRuleData(Session session, Guid codeRuleDefinitionId)
        {
            CodeRuleData ret = null;
            try
            {
                XPQuery<CodeRuleDefinition> codeRuleDefinitionQuery = session.Query<CodeRuleDefinition>();
               
                var codeRuleDefinition = codeRuleDefinitionQuery
                    .Where(r => r.CodeRuleDefinitionId == codeRuleDefinitionId && r.RowStatus > 0)
                    .FirstOrDefault();

                ret = codeRuleDefinition.CodeRuleData.FirstOrDefault(r => r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE);

                return ret;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public CodeRuleData GetCodeRuleData(Session session, Guid codeRuleDefinitionId, Guid expectedCodeRuleDataTypeId)
        {
            CodeRuleData ret = null;
            try
            {
                XPQuery<CodeRuleDefinition> codeRuleDefinitionQuery = session.Query<CodeRuleDefinition>();
                var codeRuleDefinition = codeRuleDefinitionQuery
                    .Where(r => r.CodeRuleDefinitionId == codeRuleDefinitionId && r.RowStatus > 0)
                    .FirstOrDefault();
                CodeRuleDataType expectedCodeRuleDataType =
                    session.GetObjectByKey<CodeRuleDataType>(expectedCodeRuleDataTypeId);
                CodeRuleData tempCodeRuleData = codeRuleDefinition.CodeRuleData.FirstOrDefault(r => r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE);
                if (tempCodeRuleData.CodeRuleDataFormatId.CodeRuleDataTypeId.Equals(expectedCodeRuleDataType))
                {
                    ret = tempCodeRuleData;
                }
                return ret;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public CodeRuleData Insert(Session session, Guid codeRuleDefinitionId, Guid codeRuleDataFormatId)
        {
            CodeRuleData codeRuleData = null;
            try
            {
                //Get CodeRuleDefinition
                CodeRuleDefinition codeRuleDefinition = session.GetObjectByKey<CodeRuleDefinition>(codeRuleDefinitionId);
                //Get CodeRuleDataFormat
                CodeRuleDataFormat codeRuleDataFormat = session.GetObjectByKey<CodeRuleDataFormat>(codeRuleDataFormatId);
                //Create new CodeRuleStringData
                codeRuleData = new CodeRuleData(session)
                {
                    CodeRuleDataFormatId = codeRuleDataFormat,
                    CodeRuleDataId = Guid.NewGuid(),
                    CodeRuleDefinitionId = codeRuleDefinition,
                    CreateDate = DateTime.Now,
                    RowStatus = Utility.Constant.ROWSTATUS_ACTIVE
                };
                codeRuleData.Save();
                return codeRuleData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public CodeRuleData Update(Session session, Guid codeRuleDefinitionId, Guid codeRuleDataFormatId)
        {
            CodeRuleData codeRuleData = null;
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

                codeRuleData = (CodeRuleData)codeRuleDataBO.GetCodeRuleData(session,
                                            codeRuleDefinition.CodeRuleDefinitionId,
                                            codeRuleDataFormat.CodeRuleDataTypeId.CodeRuleDataTypeId);

                foreach (var item in codeRuleDefinition.CodeRuleData)
                {
                    item.RowStatus = Utility.Constant.ROWSTATUS_INACTIVE;
                    item.Save();
                }

                if (codeRuleData == null)
                {
                    codeRuleData = Insert(session, codeRuleDefinition.CodeRuleDefinitionId, codeRuleDataFormatId);
                }
                else
                {
                    codeRuleData.CodeRuleDataFormatId = codeRuleDataFormat;
                    codeRuleData.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                    codeRuleData.Save();
                }

                return codeRuleData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
