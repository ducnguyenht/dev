using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.System.ArtifactCode;
using DevExpress.Xpo;

namespace NAS.BO.System.ArtifactCode
{
    public class CodeRuleNumberDataBO
    {
        public CodeRuleNumberData Insert(
            Session session,
            Guid codeRuleDefinitionId,
            Guid codeRuleDataFormatId,
            int beginNumber,
            int step,
            int endNumber,
            Guid ruleRepeaterTypeId)
        {
            CodeRuleNumberData codeRuleNumberData = null;
            try
            {
                //Get CodeRuleDefinition
                CodeRuleDefinition codeRuleDefinition = session.GetObjectByKey<CodeRuleDefinition>(codeRuleDefinitionId);
                //Get CodeRuleDataFormat
                CodeRuleDataFormat codeRuleDataFormat = session.GetObjectByKey<CodeRuleDataFormat>(codeRuleDataFormatId);
                //Get RuleRepeaterType
                RuleRepeaterType ruleRepeaterType = session.GetObjectByKey<RuleRepeaterType>(ruleRepeaterTypeId);
                //Create new CodeRuleStringData
                codeRuleNumberData = new CodeRuleNumberData(session)
                {
                    CodeRuleDataFormatId = codeRuleDataFormat,
                    CodeRuleDataId = Guid.NewGuid(),
                    CodeRuleDefinitionId = codeRuleDefinition,
                    CreateDate = DateTime.Now,
                    RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                    BeginNumberValue = beginNumber,
                    EndNumberValue = endNumber,
                    LastNumber = beginNumber - step,
                    RuleRepeaterTypeId = ruleRepeaterType,
                    Step = step
                };
                codeRuleNumberData.Save();
                return codeRuleNumberData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public CodeRuleNumberData Update(Session session,
            Guid codeRuleDefinitionId,
            Guid codeRuleDataFormatId,
            int beginNumber,
            int step,
            int endNumber,
            Guid ruleRepeaterTypeId)
        {
            CodeRuleNumberData codeRuleNumberData = null;
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

                codeRuleNumberData = (CodeRuleNumberData)codeRuleDataBO.GetCodeRuleData(session,
                                            codeRuleDefinition.CodeRuleDefinitionId,
                                            codeRuleDataFormat.CodeRuleDataTypeId.CodeRuleDataTypeId);

                foreach (var item in codeRuleDefinition.CodeRuleData)
                {
                    item.RowStatus = Utility.Constant.ROWSTATUS_INACTIVE;
                    item.Save();
                }

                if (codeRuleNumberData == null)
                {
                    codeRuleNumberData =
                        Insert(session,
                                codeRuleDefinition.CodeRuleDefinitionId,
                                codeRuleDataFormatId,
                                beginNumber,
                                step,
                                endNumber,
                                ruleRepeaterTypeId);
                }
                else
                {
                    int lastestNum = 0;
                    bool isUpdateLastestNum = false;

                    RuleRepeaterType ruleRepeaterType = session.GetObjectByKey<RuleRepeaterType>(ruleRepeaterTypeId);

                    codeRuleNumberData.BeginNumberValue = beginNumber;
                    codeRuleNumberData.Step = step;
                    codeRuleNumberData.EndNumberValue = endNumber;
                    codeRuleNumberData.RuleRepeaterTypeId = ruleRepeaterType;
                    codeRuleNumberData.CodeRuleDataFormatId = codeRuleDataFormat;
                    codeRuleNumberData.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                    //If lastest number is out of updating range
                    //then update lastest number
                    lastestNum = codeRuleNumberData.LastNumber;
                    if (step > 0)
                    {
                        if (((lastestNum + step) > endNumber) || (lastestNum + step) < beginNumber)
                        {
                            isUpdateLastestNum = true;
                        }
                    }
                    else if(step < 0)
                    {
                        if (((lastestNum + step) < endNumber) || (lastestNum + step) > beginNumber)
                        {
                            isUpdateLastestNum = true;
                        }
                    }
                    if (isUpdateLastestNum)
                    {
                        //update lastest number
                        codeRuleNumberData.LastNumber = beginNumber - step;
                    }
                    codeRuleNumberData.Save();
                }

                return codeRuleNumberData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
