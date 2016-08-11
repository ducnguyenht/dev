using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using NAS.DAL.BI.Accounting.Account;
using DevExpress.Xpo;
using NAS.BO.ETL.Accounting.GoodsInInventory.TempData;
using Utility;
using NAS.DAL.BI.Accounting.GoodsInInventory;
using NAS.DAL;
using NAS.DAL.BI.Accounting;
using NAS.DAL.BI.Actor;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Inventory;

namespace NAS.BO.ETL.Accounting.GoodsInInventory
{
    public class GoodsInInventoryStategy : GoodsInInventoryBase
    {
        public GoodsInInventoryStategy(Guid transactionId, string mainAccount)
            : base(transactionId, mainAccount)
        {
        }
        
        public override void ExtractTransaction(DevExpress.Xpo.Session session)
        {
        }

        public override void TransformTransaction(DevExpress.Xpo.Session session)
        {
        }

        public override void LoadTransaction(DevExpress.Xpo.Session session)
        {
            LoadTransaction(session, this.fFinancialTransformData);
        }

        public override void FixInvokedBussinessObjects(DevExpress.Xpo.Session session, DevExpress.Xpo.XPCollection<DAL.System.Log.BusinessObject> invokedBussinessObjects)
        {
            if (invokedBussinessObjects == null || invokedBussinessObjects.Count == 0)
                return;

            CriteriaOperator criteria_0 = CriteriaOperator.Parse("not(IsNull(FinancialTransactionDimId))");
            CriteriaOperator criteria_1 = new InOperator("FinancialTransactionDimId.RefId", invokedBussinessObjects.Select(i => i.RefId));
            CriteriaOperator criteria_2 = new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater);
            CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1, criteria_2);
            CorrespondFinancialAccountDim defaultCorrespondAccDim = CorrespondFinancialAccountDim.GetDefault(session, CorrespondFinancialAccountDimEnum.NAAN_DEFAULT);

            XPCollection<GoodsInInventoryDetail> neededToBeFixList = new XPCollection<GoodsInInventoryDetail>(session, criteria);
            GoodsInInventorySummary_Fact fact = null;
            if (neededToBeFixList != null && neededToBeFixList.Count > 0)
                foreach (GoodsInInventoryDetail detail in neededToBeFixList)
                {
                    fact = detail.GoodsInInventorySummary_FacftId;
                    detail.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    detail.Save();

                    //fact.CreditSum = fact.GoodsInInventoryDetails.Where(i => i.RowStatus == 1
                    //    && i.Credit > 0 && i.CorrespondFinancialAccountDimId == defaultCorrespondAccDim).Sum(d => d.Credit);

                    //fact.DebitSum = fact.GoodsInInventoryDetails.Where(i => i.RowStatus == 1
                    //    && i.Debit > 0 && i.CorrespondFinancialAccountDimId == defaultCorrespondAccDim).Sum(d => d.Debit);

                    //fact.EndCreditBalance
                    //        = fact.BeginCreditBalance +
                    //        fact.CreditSum -
                    //        fact.DebitSum;

                    //fact.EndDebitBalance
                    //    = fact.BeginDebitBalance +
                    //    fact.DebitSum -
                    //    fact.CreditSum;

                    //fact.Save();
                }
        }

        protected void LoadTransaction(Session session, ETL_GoodsInInventoryTransformData data)
        {
            try
            {
                foreach (ETL_GoodsInInventoryDetail detail in data.ETL_DetailList)
                {
                    CreateGoodsInInventoryDetail(session, detail, data.MainAccountCode);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public void CreateGoodsInInventoryDetail(
            Session session,
            ETL_GoodsInInventoryDetail Detail,
            string MainAccountCode)
        {
            try
            {
                Util util = new Util();
                CorrespondFinancialAccountDim defaultCorrespondindAcc = CorrespondFinancialAccountDim.GetDefault(session, CorrespondFinancialAccountDimEnum.NAAN_DEFAULT);
                FinancialAccountDim defaultFinancialAcc = FinancialAccountDim.GetDefault(session, FinancialAccountDimEnum.NAAN_DEFAULT);
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                GoodsInInventorySummary_Fact Fact = GetGoodsInInventorySummaryFact(session, Detail.OwnerOrgId, Detail.IssueDate, MainAccountCode);
                GoodsInInventoryDetail newDetail = new GoodsInInventoryDetail(session);
                if (Fact == null)
                {
                    Fact = CreateGoodsInInventorySummaryFact(session, Detail.OwnerOrgId, Detail.IssueDate, MainAccountCode, Detail.IsBalanceForward);
                    if (Fact == null) return;
                }
                else
                {
                    var date = new DateTime(Detail.IssueDate.Year, Detail.IssueDate.Month, 1);
                    GoodsInInventorySummary_Fact previousSummary = GetGoodsInInventorySummaryFact(session,
                        Detail.OwnerOrgId, date.AddMonths(-1), MainAccountCode);

                    if (previousSummary != null)
                    {
                        Fact.BeginCreditBalance = previousSummary.EndCreditBalance;
                        Fact.BeginDebitBalance = previousSummary.EndDebitBalance;
                    }
                }

                CorrespondFinancialAccountDim correspondFinancialAccountDim = null;
                FinancialAccountDim financialAccountDim = null;

                if (!Detail.CorrespondAccountCode.Equals(string.Empty))
                    correspondFinancialAccountDim = util.GetXpoObjectByFieldName<CorrespondFinancialAccountDim, string>(session, "Code", Detail.CorrespondAccountCode, BinaryOperatorType.Equal);
                if (!MainAccountCode.Equals(string.Empty))
                    financialAccountDim = util.GetXpoObjectByFieldName<FinancialAccountDim, string>(session, "Code", MainAccountCode, BinaryOperatorType.Equal);

                FinancialTransactionDim financialTransactionDim = util.GetXpoObjectByFieldName<FinancialTransactionDim, Guid>(session, "RefId", Detail.TransactionId, BinaryOperatorType.Equal);

                CurrencyDim currencyDim = util.GetXpoObjectByFieldName<CurrencyDim, string>(session, "Code", Detail.CurrencyCode, BinaryOperatorType.Equal);

                InventoryCommandDim inventoryCommandDim = null;
                if (!Detail.ArtifactId.Equals(Guid.Empty))
                {
                    inventoryCommandDim = util.GetXpoObjectByFieldName<InventoryCommandDim, Guid>(session, "RefId", Detail.ArtifactId, BinaryOperatorType.Equal);
                    if (inventoryCommandDim == null)
                    {
                        DimBO dimBO = new DimBO();
                        inventoryCommandDim = dimBO.GetInventoryCommandDim(session, Detail.ArtifactId);
                    }
                }
                else
                    inventoryCommandDim = InventoryCommandDim.GetDefault(session, InventoryCommandDimEnum.UNKNOWN);

                if (financialTransactionDim == null)
                {
                    financialTransactionDim = accountingBO.CreateFinancialTransactionDim(session, Detail.TransactionId);
                    if (financialTransactionDim == null)
                    {
                        return;
                    }
                }                

                if (financialAccountDim == null && !MainAccountCode.Equals(string.Empty))
                {
                    financialAccountDim = accountingBO.CreateFinancialAccountDim(session, MainAccountCode);
                }

                if (correspondFinancialAccountDim == null && !Detail.CorrespondAccountCode.Equals(string.Empty))
                {
                    correspondFinancialAccountDim = accountingBO.CreateCorrespondFinancialAccountDim(session, Detail.CorrespondAccountCode);
                }

                if (currencyDim == null && !Detail.CurrencyCode.Equals(string.Empty))
                {
                    currencyDim = accountingBO.CreateCurrencyDim(session, Detail.CurrencyCode);
                }

                if (Detail.IsBalanceForward)
                {
                    Fact.BeginCreditBalance = Fact.EndCreditBalance = Detail.Credit;
                    Fact.BeginDebitBalance = Fact.EndDebitBalance = Detail.Debit;
                    Fact.CreditSum = 0;
                    Fact.DebitSum = 0;
                }
                else
                {
                    Fact.CreditSum = Fact.GoodsInInventoryDetails.Where(i => i.RowStatus == 1
                        && i.Credit > 0 && i.CorrespondFinancialAccountDimId == defaultCorrespondindAcc).Sum(d => d.Credit);

                    Fact.DebitSum = Fact.GoodsInInventoryDetails.Where(i => i.RowStatus == 1
                        && i.Debit > 0 && i.CorrespondFinancialAccountDimId == defaultCorrespondindAcc).Sum(d => d.Debit);

                    Fact.EndCreditBalance = Fact.BeginCreditBalance + Fact.CreditSum - Fact.DebitSum;
                    Fact.EndDebitBalance = Fact.BeginDebitBalance + Fact.DebitSum - Fact.CreditSum;
                }   
                
                Fact.Save();
                newDetail.CorrespondFinancialAccountDimId = correspondFinancialAccountDim;
                newDetail.Credit = Detail.Credit;
                newDetail.Debit = Detail.Debit;
                newDetail.Quantity = Detail.Quantity;
                newDetail.CurrencyDimId = currencyDim;
                newDetail.FinancialAccountDimId = financialAccountDim;
                newDetail.GoodsInInventorySummary_FacftId= Fact;
                newDetail.FinancialTransactionDimId = financialTransactionDim;
                newDetail.InventoryCommandDimId = inventoryCommandDim;           

                if (newDetail.FinancialAccountDimId == null)
                    newDetail.FinancialAccountDimId = defaultFinancialAcc;
                if (newDetail.CorrespondFinancialAccountDimId == null)
                    newDetail.CorrespondFinancialAccountDimId = defaultCorrespondindAcc;

                newDetail.RowStatus = Constant.ROWSTATUS_ACTIVE;
                newDetail.Save();
            }
            catch (Exception)
            {
                return;
            }
        }

        private GoodsInInventorySummary_Fact GetGoodsInInventorySummaryFact(
                                                                    Session session,
                                                                    Guid OwnerOrgId,
                                                                    DateTime IssueDate,
                                                                    string FinancialAccountCode)
        {
            GoodsInInventorySummary_Fact result = null;
            try
            {
                Util util = new Util();
                OwnerOrgDim ownerOrgDim = util.GetXpoObjectByFieldName<OwnerOrgDim, Guid>(session, "RefId", OwnerOrgId, BinaryOperatorType.Equal);
                MonthDim monthDim = util.GetXpoObjectByFieldName<MonthDim, string>(session, "Name", IssueDate.Month.ToString(), BinaryOperatorType.Equal);
                YearDim yearDim = util.GetXpoObjectByFieldName<YearDim, string>(session, "Name", IssueDate.Year.ToString(), BinaryOperatorType.Equal);
                FinancialAccountDim financialAccountDim = util.GetXpoObjectByFieldName<FinancialAccountDim, string>(session, "Code", FinancialAccountCode, BinaryOperatorType.Equal);

                if (ownerOrgDim == null || monthDim == null || yearDim == null || financialAccountDim == null)
                {
                    return null;
                }
                else
                {
                    CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                    CriteriaOperator criteria_OwnerOrg = new BinaryOperator("OwnerOrgDimId", ownerOrgDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_Month = new BinaryOperator("MonthDimId", monthDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_Year = new BinaryOperator("YearDimId", yearDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_AccountCode = new BinaryOperator("FinancialAccountDimId", financialAccountDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria = CriteriaOperator.And(criteria_RowStatus, criteria_OwnerOrg, criteria_Month, criteria_Year, criteria_AccountCode);

                    GoodsInInventorySummary_Fact fact = session.FindObject<GoodsInInventorySummary_Fact>(criteria);

                    if (fact == null) return null;
                    {
                        result = fact;
                    }
                }
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }

        private GoodsInInventorySummary_Fact CreateGoodsInInventorySummaryFact(Session session,
                                                                    Guid OwnerOrgId,
                                                                    DateTime IssueDate,
                                                                    string FinancialAccountCode,
                                                                    bool IsBalanceForward)
        {
            GoodsInInventorySummary_Fact result = new GoodsInInventorySummary_Fact(session);
            try
            {
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                DimBO dimBO = new DimBO();
                result.BeginCreditBalance = 0;
                result.BeginDebitBalance = 0;
                result.CreditSum = 0;
                result.DebitSum = 0;
                result.EndCreditBalance = 0;
                result.EndDebitBalance = 0;
                result.FinancialAccountDimId = accountingBO.GetFinancialAccountDim(session, FinancialAccountCode);
                result.MonthDimId = dimBO.GetMonthDim(session, (short)IssueDate.Month);
                result.YearDimId = dimBO.GetYearDim(session, (short)IssueDate.Year);
                result.OwnerOrgDimId = dimBO.GetOwnerOrgDim(session, OwnerOrgId);
                result.RowStatus = Constant.ROWSTATUS_ACTIVE;
                if (result.FinancialAccountDimId == null ||
                    result.MonthDimId == null ||
                    result.YearDimId == null ||
                    result.OwnerOrgDimId == null)
                {
                    return null;
                }
                
                var date = new DateTime(IssueDate.Year, IssueDate.Month, 1);
                GoodsInInventorySummary_Fact previousSummary = GetGoodsInInventorySummaryFact(session,
                    OwnerOrgId, date.AddMonths(-1), FinancialAccountCode);

                if (previousSummary != null)
                {
                    result.BeginDebitBalance = previousSummary.EndDebitBalance;
                    result.BeginCreditBalance = previousSummary.EndCreditBalance;
                }
                result.Save();
                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
