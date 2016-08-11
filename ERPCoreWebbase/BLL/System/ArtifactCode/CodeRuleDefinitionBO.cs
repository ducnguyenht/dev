using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.System.ArtifactCode;
using NAS.DAL;

namespace NAS.BO.System.ArtifactCode
{
    public class CodeRuleDefinitionBO
    {

        public CodeRuleDefinition Insert(Session session, Guid artifactCodeRuleId, Guid codeRuleDataTypeId, Guid parentCodeRuleDefinitionId)
        {
            CodeRuleDefinition codeRuleDefinition = null;
            try
            {
                //Get ArtifactCodeRule
                ArtifactCodeRule artifactCodeRule =
                    session.GetObjectByKey<ArtifactCodeRule>(artifactCodeRuleId);
                //Get CodeRuleDataType
                CodeRuleDataType codeRuleDataType =
                    session.GetObjectByKey<CodeRuleDataType>(codeRuleDataTypeId);
                //Get parent CodeRuleDefinition
                CodeRuleDefinition parentCodeRuleDefinition = null;
                if (parentCodeRuleDefinitionId != Guid.Empty)
                {
                    parentCodeRuleDefinition = session.GetObjectByKey<CodeRuleDefinition>(parentCodeRuleDefinitionId);
                }
                //Validate 
                bool isInsertingValid =
                    isInsertingCodeRuleDefinitionValid(session, artifactCodeRuleId, parentCodeRuleDefinition);

                if (isInsertingValid)
                {
                    //Create new CodeRuleDefinition
                    codeRuleDefinition = new CodeRuleDefinition(session)
                    {
                        ArtifactCodeRuleId = artifactCodeRule,
                        CodeRuleDataTypeId = codeRuleDataType,
                        CodeRuleDefinitionId = Guid.NewGuid(),
                        ParentCodeRuleDefinitionId = parentCodeRuleDefinition,
                        RowStatus = Utility.Constant.ROWSTATUS_ACTIVE
                    };
                    codeRuleDefinition.Save();
                }

                return codeRuleDefinition;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public CodeRuleDefinition Update(Session session, Guid codeRuleDefinitionId, Guid codeRuleDataTypeId)
        {
            CodeRuleDefinition codeRuleDefinition = null;
            try
            {
                //Get current CodeRuleDefinition
                codeRuleDefinition = session.GetObjectByKey<CodeRuleDefinition>(codeRuleDefinitionId);
                //Get CodeRuleDataType
                CodeRuleDataType codeRuleDataType = session.GetObjectByKey<CodeRuleDataType>(codeRuleDataTypeId);
                codeRuleDefinition.CodeRuleDataTypeId = codeRuleDataType;
                codeRuleDefinition.Save();
                return codeRuleDefinition;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public void Delete(Session session, Guid codeRuleDefinitionId)
        {
            try
            {
                CodeRuleDefinition codeRuleDefinition = 
                    session.GetObjectByKey<CodeRuleDefinition>(codeRuleDefinitionId);
                if (codeRuleDefinition != null)
                {
                    codeRuleDefinition.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    codeRuleDefinition.Save();
                    if (codeRuleDefinition.CodeRuleData != null)
                    {
                        foreach (var data in codeRuleDefinition.CodeRuleData)
                        {
                            data.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                            data.Save();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool isInsertingCodeRuleDefinitionValid(Session session, Guid artifactCodeRuleId, CodeRuleDefinition parentCodeRuleDefinition)
        {
            try
            {
                //Get ArtifactCodeRule
                ArtifactCodeRule artifactCodeRule = session.GetObjectByKey<ArtifactCodeRule>(artifactCodeRuleId);

                if (parentCodeRuleDefinition == null)
                {
                    //Is CodeRuleDefinition root of the ArtifactCodeRule exist
                    int rootCount =
                        artifactCodeRule.CodeRuleDefinitions
                            .Where(r => r.RowStatus > 0 && r.ParentCodeRuleDefinitionId == null)
                            .Count();
                    if (rootCount != 0)
                    {
                        return false;
                    }
                }
                else
                {
                    //Check parentCodeRuleDefinition already has child
                    int countChild = parentCodeRuleDefinition.CodeRuleDefinitions.Count(r => r.RowStatus > 0);
                    if (countChild != 0)
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<CodeRuleDefinitionView> GetCodeRuleDefinitionView(Guid artifactCodeRuleId)
        {
            List<CodeRuleDefinitionView> ret = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                ArtifactCodeRule artifactCodeRule = session.GetObjectByKey<ArtifactCodeRule>(artifactCodeRuleId);

                XPQuery<CodeRuleData> codeRuleDataQuery = session.Query<CodeRuleData>();
                XPQuery<CodeRuleDefinition> codeRuleDefinitionQuery = session.Query<CodeRuleDefinition>();

                List<CodeRuleData> codeRuleDataList = (from da in codeRuleDataQuery
                                                       join de in codeRuleDefinitionQuery
                                                         on da.CodeRuleDefinitionId equals de
                                                       where de.ArtifactCodeRuleId == artifactCodeRule
                                                             && de.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE
                                                             && da.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE
                                                       select da).ToList();

                if (codeRuleDataList != null)
                {
                    ret = new List<CodeRuleDefinitionView>();
                    foreach (CodeRuleData item in codeRuleDataList)
                    {
                        CodeRuleDefinitionView codeRuleDefinitionView;
                        if (item is CodeRuleStringData)
                        {
                            CodeRuleStringData actualCodeRuleData = (CodeRuleStringData)item;
                            Guid parentCodeRuleDefinitionId = Guid.Empty;
                            if (actualCodeRuleData.CodeRuleDefinitionId.ParentCodeRuleDefinitionId != null)
                            {
                                parentCodeRuleDefinitionId =
                                    actualCodeRuleData.CodeRuleDefinitionId.ParentCodeRuleDefinitionId.CodeRuleDefinitionId;
                            }
                            codeRuleDefinitionView = new CodeRuleDefinitionView()
                            {
                                CodeRuleDefinitionId = actualCodeRuleData.CodeRuleDefinitionId.CodeRuleDefinitionId,
                                ParentCodeRuleDefinitionId = parentCodeRuleDefinitionId,
                                DataFormat = actualCodeRuleData.CodeRuleDataFormatId.Name,
                                DataType = actualCodeRuleData.CodeRuleDefinitionId.CodeRuleDataTypeId.Name,
                                DataValue = actualCodeRuleData.StringValue
                            };
                        }
                        else if (item is CodeRuleNumberData)
                        {
                            CodeRuleNumberData actualCodeRuleData = (CodeRuleNumberData)item;
                            Guid parentCodeRuleDefinitionId = Guid.Empty;
                            if (actualCodeRuleData.CodeRuleDefinitionId.ParentCodeRuleDefinitionId != null)
                            {
                                parentCodeRuleDefinitionId =
                                    actualCodeRuleData.CodeRuleDefinitionId.ParentCodeRuleDefinitionId.CodeRuleDefinitionId;
                            }
                            codeRuleDefinitionView = new CodeRuleDefinitionView()
                            {
                                CodeRuleDefinitionId = actualCodeRuleData.CodeRuleDefinitionId.CodeRuleDefinitionId,
                                ParentCodeRuleDefinitionId = parentCodeRuleDefinitionId,
                                DataFormat = actualCodeRuleData.CodeRuleDataFormatId.Name,
                                DataType = actualCodeRuleData.CodeRuleDefinitionId.CodeRuleDataTypeId.Name,
                                DataValue = actualCodeRuleData.BeginNumberValue.ToString()
                            };
                        }
                        else
                        {
                            CodeRuleData actualCodeRuleData = item;
                            Guid parentCodeRuleDefinitionId = Guid.Empty;
                            if (actualCodeRuleData.CodeRuleDefinitionId.ParentCodeRuleDefinitionId != null)
                            {
                                parentCodeRuleDefinitionId =
                                    actualCodeRuleData.CodeRuleDefinitionId.ParentCodeRuleDefinitionId.CodeRuleDefinitionId;
                            }
                            codeRuleDefinitionView = new CodeRuleDefinitionView()
                            {
                                CodeRuleDefinitionId = actualCodeRuleData.CodeRuleDefinitionId.CodeRuleDefinitionId,
                                ParentCodeRuleDefinitionId = parentCodeRuleDefinitionId,
                                DataFormat = actualCodeRuleData.CodeRuleDataFormatId.Name,
                                DataType = actualCodeRuleData.CodeRuleDefinitionId.CodeRuleDataTypeId.Name,
                                DataValue = String.Empty
                            };
                        }
                        ret.Add(codeRuleDefinitionView);
                    }
                }
                return ret;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (session != null)
                    session.Dispose();
            }
        }

        public class CodeRuleDefinitionView
        {
            public Guid CodeRuleDefinitionId { get; set; }
            public Guid ParentCodeRuleDefinitionId { get; set; }
            public string DataType { get; set; }
            public string DataValue { get; set; }
            public string DataFormat { get; set; }
        }

    }
}
