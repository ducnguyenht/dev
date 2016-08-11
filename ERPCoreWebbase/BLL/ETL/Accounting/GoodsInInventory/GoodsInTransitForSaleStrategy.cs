using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using NAS.DAL.BI.Accounting.Account;
using DevExpress.Xpo;
using NAS.BO.ETL.Accounting.GoodsInInventory.TempData;
using NAS.DAL.BI.Accounting.GoodsInTransit;
using NAS.DAL;
using NAS.DAL.BI.Accounting;
using Utility;
using NAS.DAL.BI.Actor;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Inventory;

namespace NAS.BO.ETL.Accounting.GoodsInInventory
{
    public class GoodsInTransitForSaleStrategy : GoodsInInventoryBase
    {
        private const string ACCOUNT_CODE = "157";

        public GoodsInTransitForSaleStrategy(Guid transactionId)
            : base(transactionId, ACCOUNT_CODE)
        {
        }

        public override void ExtractTransaction(DevExpress.Xpo.Session session)
        {
            base.GetIsRelatedStrategy(session);
            if (IsRelatedStrategy)
            {
                this.fFinancialTransactionData = base.ExtractTransaction(session, TransactionId, ACCOUNT_CODE);
            }
        }

        public override void TransformTransaction(DevExpress.Xpo.Session session)
        {
            if (IsRelatedStrategy)
                this.fFinancialTransformData = base.TransformTransaction(session, this.fFinancialTransactionData, ACCOUNT_CODE);
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

            XPCollection<GoodsInTransitForSaleDetail> neededToBeFixList = new XPCollection<GoodsInTransitForSaleDetail>(session, criteria);
            GoodsInTransitForSaleSummary_Fact fact = null;
            if (neededToBeFixList != null && neededToBeFixList.Count > 0)
                foreach (GoodsInTransitForSaleDetail detail in neededToBeFixList)
                {
                    fact = detail.GoodsInTransitForSaleSummary_FactId;
                    detail.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    detail.Save();
                }
        }

        protected void LoadTransaction(Session session, ETL_GoodsInInventoryTransformData data)
        {
            try
            {
                foreach (ETL_GoodsInInventoryDetail detail in data.ETL_DetailList)
                {
                    CreateGoodsInTransitForSaleDetail(session, detail, data.MainAccountCode);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public void CreateGoodsInTransitForSaleDetail(
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
                GoodsInTransitForSaleSummary_Fact Fact = GetGoodsInTransitForSaleSummary_Fact(session, Detail.OwnerOrgId, Detail.IssueDate, MainAccountCode);
                GoodsInTransitForSaleDetail newDetail = new GoodsInTransitForSaleDetail(session);
                if (Fact == null)
                {
                    Fact = CreateGoodsInTransitForSaleSummary_Fact(session, Detail.OwnerOrgId, Detail.IssueDate, MainAccountCode, Detail.IsBalanceForward);
                    if (Fact == null) return;
                }
                else
                {
                    var date = new DateTime(Detail.IssueDate.Year, Detail.IssueDate.Month, 1);
                    GoodsInTransitForSaleSummary_Fact previousSummary = GetGoodsInTransitForSaleSummary_Fact(session,
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
                if (financialTransactionDim == null)
                {
                    financialTransactionDim = accountingBO.CreateFinancialTransactionDim(session, Detail.TransactionId);
                    if (financialTransactionDim == null)
                    {
                        return;
                    }
                }

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

                newDetail.CorrespondFinancialAccountDimId = correspondFinancialAccountDim;
                newDetail.Credit = Detail.Credit;
                newDetail.Debit = Detail.Debit;
                newDetail.Quantity = Detail.Quantity;
                newDetail.CurrencyDimId = currencyDim;
                newDetail.FinancialAccountDimId = financialAccountDim;
                newDetail.GoodsInTransitForSaleSummary_FactId = Fact;
                newDetail.FinancialTransactionDimId = financialTransactionDim;
                newDetail.InventoryCommandDimId = inventoryCommandDim;

                if (newDetail.FinancialAccountDimId == null)
                    newDetail.FinancialAccountDimId = defaultFinancialAcc;
                if (newDetail.CorrespondFinancialAccountDimId == null)
                    newDetail.CorrespondFinancialAccountDimId = defaultCorrespondindAcc;

                newDetail.RowStatus = Constant.ROWSTATUS_ACTIVE;
                newDetail.Save();

                if (Detail.IsBalanceForward)
                {
                    Fact.BeginCreditBalance = Fact.EndCreditBalance = Detail.Credit;
                    Fact.BeginDebitBalance = Fact.EndDebitBalance = Detail.Debit;
                    Fact.CreditSum = 0;
                    Fact.DebitSum = 0;
                }
                else
                {
                    Fact.CreditSum = Fact.GoodsInTransitForSaleDetails.Where(i => i.RowStatus == 1
                        && i.Credit > 0 && i.CorrespondFinancialAccountDimId == defaultCorrespondindAcc).Sum(d => d.Credit);

                    Fact.DebitSum = Fact.GoodsInTransitForSaleDetails.Where(i => i.RowStatus == 1
                        && i.Debit > 0 && i.CorrespondFinancialAccountDimId == defaultCorrespondindAcc).Sum(d => d.Debit);

                    Fact.EndCreditBalance = Fact.BeginCreditBalance + Fact.CreditSum - Fact.DebitSum;
                    Fact.EndDebitBalance = Fact.BeginDebitBalance + Fact.DebitSum - Fact.CreditSum;
                }             
                Fact.Save();                
            }
            catch (Exception)
            {
                return;
            }
        }

        private GoodsInTransitForSaleSummary_Fact GetGoodsInTransitForSaleSummary_Fact(
                                                                    Session session,
                                                                    Guid OwnerOrgId,
                                                                    DateTime IssueDate,
                                                                    string FinancialAccountCode)
        {
            GoodsInTransitForSaleSummary_Fact result = null;
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

                    GoodsInTransitForSaleSummary_Fact fact = session.FindObject<GoodsInTransitForSaleSummary_Fact>(criteria);

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

        private GoodsInTransitForSaleSummary_Fact CreateGoodsInTransitForSaleSummary_Fact(Session session,
                                                                    Guid OwnerOrgId,
                                                                    DateTime IssueDate,
                                                                    string FinancialAccountCode,
                                                                    bool IsBalanceForward)
        {
            GoodsInTransitForSaleSummary_Fact result = new GoodsInTransitForSaleSummary_Fact(session);
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
                GoodsInTransitForSaleSummary_Fact previousSummary = GetGoodsInTransitForSaleSummary_Fact(session,
                    OwnerOrgId, date.AddMonths(-1), FinancialAccountCode);

                if (previousSummary != null)
                {
                    result.BeginDebitBalance = previousSummary.EndDebitBalance;
                    result.BeginCreditBalance = previousSummary.EndCreditBalance;
                }
                result.Save();

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
