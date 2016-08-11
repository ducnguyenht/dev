using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.System.ArtifactCode;
using NAS.DAL;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace NAS.BO.System.ArtifactCode
{

    public enum ArtifactTypeEnum {
        VOUCHER_RECEIPT,
        VOUCHER_PAYMENT,
        INVOICE_PURCHASE,
        INVOICE_SALE,
        INVENTORY_INPUT,
        INVENTORY_OUTPUT,
        INVENTORY_MOVE,
        INVENTORYTRANSACTION_MOVE,
        INVENTORYTRANSACTION_OUTPUT,
        INVENTORYTRANSACTION_INPUT,
        TRANSACTION
    }

    public class ArtifactCodeRuleBO
    {

        public string GetCodeDefinition(Session session, CodeRuleDefinition codeRuleDefinition, bool isUpdateLastestWhenIsNumber)
        {
            string codeDefinition = String.Empty;
            ArtifactCodeFormater.CodeRuleData data = null;
            ArtifactCodeFormater.CodeRuleDataFormater formater = null;
            int beginNum = 0;
            int step = 0;
            int endNum = 0;
            int lastestNum = 0;
            int currentNum = 0;

            //2.1. Get data type

            //2.2. Get format type
            CodeRuleData codeRuleData =
                codeRuleDefinition.CodeRuleData.FirstOrDefault(r => r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE);

            //Get format code
            string dataFormat = codeRuleData.CodeRuleDataFormatId.Code;

            //2.3. Determine the data type
            //2.3.1. When is STRING
            #region STRING
            if (codeRuleDefinition.CodeRuleDataTypeId.Code.Equals("STRING"))
            {
                CodeRuleStringData _codeRuleStringData = (CodeRuleStringData)codeRuleData;
                data = new ArtifactCodeFormater.CodeRuleData();
                switch (dataFormat)
                {
                    case "NORMAL":
                        formater = new ArtifactCodeFormater.CodeRuleDataFormaterString();
                        break;
                    case "LOWERCASE":
                        formater = new ArtifactCodeFormater.CodeRuleDataFormaterStringLowerCase();
                        break;
                    case "UPPERCASE":
                        formater = new ArtifactCodeFormater.CodeRuleDataFormaterStringUpperCase();
                        break;
                    case "ACSII":
                        formater = new ArtifactCodeFormater.CodeRuleDataFormaterStringACSI();
                        break;
                    case "UPPERCASE_ACSII":
                        formater = new ArtifactCodeFormater.CodeRuleDataFormaterStringACSI(
                                                new ArtifactCodeFormater.CodeRuleDataFormaterStringUpperCase()
                                            );
                        break;
                    case "LOWERCASE_ACSII":
                        formater = new ArtifactCodeFormater.CodeRuleDataFormaterStringACSI(
                                                new ArtifactCodeFormater.CodeRuleDataFormaterStringLowerCase()
                                            );
                        break;
                    default:
                        break;
                }
                
                data.Value = _codeRuleStringData.StringValue;
                data.FormatString = _codeRuleStringData.CodeRuleDataFormatId.FormatString;
            }
            #endregion
            //2.3.2. When is NUMBER
            #region NUMBER
            else if (codeRuleDefinition.CodeRuleDataTypeId.Code.Equals("NUMBER"))
            {
                CodeRuleNumberData _codeRuleNumberData = (CodeRuleNumberData)codeRuleData;
                //2.3.2.1. Get begin number
                beginNum = _codeRuleNumberData.BeginNumberValue;
                //2.3.2.2. Get step
                step = _codeRuleNumberData.Step;
                //2.3.2.3. Get end number
                endNum = _codeRuleNumberData.EndNumberValue;
                //2.3.2.4. Get lastest number
                lastestNum = _codeRuleNumberData.LastNumber;
                //check can get more code
                if (isUpdateLastestWhenIsNumber)
                {
                    if (step > 0)
                    {
                        if ((step + lastestNum) > endNum)
                        {
                            throw new Exception("Definition is invalid");
                        }
                    }
                    else if (step < 0)
                    {
                        if ((step + lastestNum) < endNum)
                        {
                            throw new Exception("Definition is invalid");
                        }
                    }
                    currentNum = lastestNum + step;
                }
                else
                {
                    currentNum = beginNum;
                }
                formater = new ArtifactCodeFormater.CodeRuleDataFormaterNumber();
                data = new ArtifactCodeFormater.CodeRuleData();
                data.Value = currentNum;
                data.FormatString = _codeRuleNumberData.CodeRuleDataFormatId.FormatString;

                if (isUpdateLastestWhenIsNumber)
                {
                    _codeRuleNumberData.LastNumber = currentNum;
                    _codeRuleNumberData.Save();
                }
                
            }
            #endregion
            //2.3.3. When is DATETIME
            #region DATETIME
            //Get system datetime
            else if (codeRuleDefinition.CodeRuleDataTypeId.Code.Equals("DATETIME"))
            {
                formater = new ArtifactCodeFormater.CodeRuleDataFormaterDateTime();
                data = new ArtifactCodeFormater.CodeRuleData();
                data.Value = DateTime.Now;
                data.FormatString = codeRuleData.CodeRuleDataFormatId.FormatString;
            }
            #endregion

            data.UseFormater(formater);
            codeDefinition = data.GetFormatedValue();

            int countChild = codeRuleDefinition.CodeRuleDefinitions.Where(r => r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE).Count();
            if (countChild == 0)
            {
                return codeDefinition;
            }
            else
            {
                CodeRuleDefinition childCodeRuleDefinition = 
                    codeRuleDefinition.CodeRuleDefinitions
                        .Where(r => r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                        .First();
                return codeDefinition += GetCodeDefinition(session, childCodeRuleDefinition, isUpdateLastestWhenIsNumber);
            }

        }

        public string GetArtifactCode(Guid artifactCodeRuleId, bool isUpdateLastestWhenIsNumber)
        {
            UnitOfWork uow = null;
            string artifactCode = String.Empty;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();

                CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("ArtifactCodeRuleId.ArtifactCodeRuleId", artifactCodeRuleId),
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE),
                    new UnaryOperator(UnaryOperatorType.IsNull, "ParentCodeRuleDefinitionId")
                );
                XPCollection<CodeRuleDefinition> codeRuleDefinitions =
                    new XPCollection<CodeRuleDefinition>(PersistentCriteriaEvaluationBehavior.InTransaction, uow, criteria, false);
                CodeRuleDefinition rootDefinition = codeRuleDefinitions.FirstOrDefault();
                if (rootDefinition == null)
                {
                    return String.Empty;
                }
                else
                {
                    artifactCode = GetCodeDefinition(uow, rootDefinition, isUpdateLastestWhenIsNumber);
                }
                uow.CommitChanges();
                return artifactCode;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (uow != null) uow.Dispose();
            }
        }

        public string GetArtifactCodeOfArtifactType(Guid artifactTypeId)
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //Get first active artifact code rule of the artifact type
                ArtifactCodeRule artifactCodeRule = 
                    session.GetObjectByKey<ArtifactType>(artifactTypeId).ArtifactCodeRules
                        .Where(r => r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                        .FirstOrDefault();
                if (artifactCodeRule == null)
                {
                    return String.Empty;
                }
                return GetArtifactCode(artifactCodeRule.ArtifactCodeRuleId, true);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }

        public string GetArtifactCodeOfArtifactType(ArtifactTypeEnum artifactTypeEnum)
        {
            Session session = null;
            string artifactTypeCode = null;
            try
            {
                session = XpoHelper.GetNewSession();
                artifactTypeCode = Enum.GetName(typeof(ArtifactTypeEnum), artifactTypeEnum);
                ArtifactType artifactType = 
                    Util.getXPCollection<ArtifactType>(session, "Code", artifactTypeCode).FirstOrDefault();
                return GetArtifactCodeOfArtifactType(artifactType.ArtifactTypeId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }

        public void Delete(Session session, Guid artifactCodeRuleId)
        {
            try
            {
                ArtifactCodeRule artifactCodeRule = 
                    session.GetObjectByKey<ArtifactCodeRule>(artifactCodeRuleId);

                artifactCodeRule.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                artifactCodeRule.Save();
                if (artifactCodeRule != null)
                {
                    if (artifactCodeRule.CodeRuleDefinitions != null)
                    {
                        CodeRuleDefinitionBO codeRuleDefinitionBO = new CodeRuleDefinitionBO();
                        foreach (var definition in artifactCodeRule.CodeRuleDefinitions)
                        {
                            codeRuleDefinitionBO.Delete(session, definition.CodeRuleDefinitionId);
                        }
                    }
                }
            }
            catch (Exception)
            {   
                throw;
            }
        }

    }
}
