using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Actor;
using DevExpress.Data.Filtering;
using Utility;
using NAS.DAL;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Accounting.Account;
using NAS.BO.ETL.Accounting.TempData;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL.Accounting.Currency;
using NAS.DAL.BI.Accounting;
using NAS.DAL.BI.Accounting.CustomerLiability;

namespace NAS.BO.ETL.Accounting.FinancialLiability
{
    public class FinancialCustomerLiabilityBO : FinancialLiabilityBO
    {

        //public bool IsExistInJournalList(Session session, List<ETL_GeneralJournal> journalList, string AccountCode)
        //{
        //    ETLAccountingBO accountingBO = new ETLAccountingBO();
        //    if (journalList == null) return false;
        //    if (journalList.Count == 0) return false;
        //    bool result = false;
        //    try
        //    {
        //        Util util = new Util();
        //        Account account = util.GetXpoObjectByFieldName<Account, string>(session, "Code", AccountCode, BinaryOperatorType.Equal);
        //        if (account == null) return false;
        //        foreach (ETL_GeneralJournal journal in journalList)
        //        {
        //            if (accountingBO.IsRelateAccount(session, journal.AccountId, account.AccountId))
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    return result;
        //}
        //public bool IsExistInJournalList(Session session, List<ETL_GeneralJournal> journalList, Guid AccountId)
        //{
        //    bool result = false;
        //    try
        //    {
        //        Account account = session.GetObjectByKey<Account>(AccountId);
        //        if (account == null) return false;
        //        return IsExistInJournalList(session, journalList, account.Code);
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    return result;
        //}

        //public ETL_GeneralJournal GetJournal(Session session, List<ETL_GeneralJournal> journalList, string AccountCode)
        //{
        //    ETLAccountingBO accountingBO = new ETLAccountingBO();
        //    ETL_GeneralJournal result = new ETL_GeneralJournal();
        //    try
        //    {
        //        if (journalList == null) return null;
        //        Util util = new Util();
        //        Account account = util.GetXpoObjectByFieldName<Account, string>(session, "Code", AccountCode, BinaryOperatorType.Equal);
        //        if (account == null) return null;
        //        foreach (ETL_GeneralJournal journal in journalList)
        //        {
        //            if (accountingBO.IsRelateAccount(session, journal.AccountId, account.AccountId))
        //            {
        //                return journal;
        //            }
        //        }
        //        return null;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //    return result;
        //}
        //public ETL_GeneralJournal GetJournal(Session session, List<ETL_GeneralJournal> journalList, Guid AccountId)
        //{
        //    ETL_GeneralJournal result = new ETL_GeneralJournal();
        //    try
        //    {
        //        Account account = session.GetObjectByKey<Account>(AccountId);
        //        if (account == null) return null;
        //        return GetJournal(session, journalList, account.Code);
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //    return result;
        //}

        //public List<ETL_GeneralJournal> JoinJournal(Session session, List<ETL_GeneralJournal> journalList)
        //{
        //    List<ETL_GeneralJournal> result = new List<ETL_GeneralJournal>();
        //    try
        //    {
        //        ETLAccountingBO accountingBO = new ETLAccountingBO();
        //        foreach (ETL_GeneralJournal journal in journalList)
        //        {
        //            ETL_GeneralJournal rsJournal = GetJournal(session, result, journal.AccountId);
        //            if (rsJournal == null)
        //            {
        //                rsJournal = new ETL_GeneralJournal();
        //                rsJournal.AccountId = accountingBO.GetHighestAccount(session, journal.AccountId).AccountId;
        //                rsJournal.CreateDate = journal.CreateDate;
        //                rsJournal.Credit = journal.Credit;
        //                rsJournal.CurrencyId = journal.CurrencyId;
        //                rsJournal.Debit = journal.Debit;
        //                rsJournal.JournalType = journal.JournalType;
        //                result.Add(rsJournal);
        //            }
        //            else
        //            {
        //                rsJournal.Credit += journal.Credit;
        //                rsJournal.Debit += journal.Debit;
        //            }
        //        }
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //    return result;
        //}
        //public List<ETL_GeneralJournal> ClearJournalList(Session session, List<ETL_GeneralJournal> journalList, Guid AccountId)
        //{
        //    List<ETL_GeneralJournal> result = new List<ETL_GeneralJournal>();
        //    try
        //    {
        //        char AccountSide = 'N';
        //        char MainAccountSide = 'C';
        //        short DebitCount = 0;
        //        short CreditCount = 0;
        //        ETLAccountingBO accountingBO = new ETLAccountingBO();
        //        foreach (ETL_GeneralJournal journal in journalList)
        //        {
        //            if (journal.Credit > 0)
        //            {
        //                if (accountingBO.IsRelateAccount(session, AccountId, journal.AccountId))
        //                {
        //                    AccountSide = 'C';
        //                }
        //                CreditCount++;
        //            }
        //            else
        //            {
        //                if (accountingBO.IsRelateAccount(session, AccountId, journal.AccountId))
        //                {
        //                    AccountSide = 'D';
        //                }
        //                DebitCount++;
        //            }
        //        }
        //        if (DebitCount == 1)
        //        {
        //            MainAccountSide = 'D';
        //        }
        //        if (MainAccountSide == AccountSide) return journalList;

        //        ETL_GeneralJournal mainJournal = new ETL_GeneralJournal();

        //        if (MainAccountSide == 'C')
        //        {
        //            mainJournal = journalList.Where(r => r.Credit > 0).FirstOrDefault();
        //            result.Add(mainJournal);
        //            foreach (ETL_GeneralJournal journal in journalList)
        //            {
        //                if (journal.Debit > 0)
        //                {
        //                    if (!accountingBO.IsRelateAccount(session, AccountId, journal.AccountId))
        //                    {
        //                        mainJournal.Credit -= journal.Debit;
        //                    }
        //                    else
        //                    {
        //                        result.Add(journal);
        //                    }
        //                }
        //            }
        //        }
        //        if (MainAccountSide == 'D')
        //        {
        //            mainJournal = journalList.Where(r => r.Debit > 0).FirstOrDefault();
        //            result.Add(mainJournal);
        //            foreach (ETL_GeneralJournal journal in journalList)
        //            {
        //                if (journal.Credit > 0)
        //                {
        //                    if (!accountingBO.IsRelateAccount(session, AccountId, journal.AccountId))
        //                    {
        //                        mainJournal.Debit -= journal.Credit;
        //                    }
        //                    else
        //                    {
        //                        result.Add(journal);
        //                    }
        //                }
        //            }
        //        }
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //    return result;
        //}

        public List<ETL_FinnancialCustomerLiabilityDetail> TransformTransactionToCustomerLiabilityDetail(Session session, ETL_Transaction transaction, string AccountCode)
        {
            Util util = new Util();
            Account account = util.GetXpoObjectByFieldName<Account, string>(session, "Code", AccountCode, BinaryOperatorType.Equal);
            if (account == null) return null;
            if (transaction == null) return null;
            ETL_Transaction etlTransaction = transaction;

            List<ETL_FinnancialCustomerLiabilityDetail> detail = new List<ETL_FinnancialCustomerLiabilityDetail>();

            try
            {
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                List<ETL_GeneralJournal> JournalListJoined = JoinJournal(session, etlTransaction.GeneralJournalList);
                List<ETL_GeneralJournal> FinishJournalList = ClearJournalList(session, JournalListJoined, account.AccountId);
                foreach (ETL_GeneralJournal journal in FinishJournalList)
                {
                    ETL_FinnancialCustomerLiabilityDetail temp = new ETL_FinnancialCustomerLiabilityDetail();
                    temp.AccountCode = "";
                    temp.CorrespondAccountCode = "";
                    if (accountingBO.IsRelateAccount(session, account.AccountId, journal.AccountId))
                    {
                        temp.AccountCode = session.GetObjectByKey<Account>(journal.AccountId).Code;
                    }
                    else
                    {
                        temp.CorrespondAccountCode = session.GetObjectByKey<Account>(journal.AccountId).Code;
                    }
                    temp.CurrencyCode = session.GetObjectByKey<Currency>(journal.CurrencyId).Code;
                    temp.IsBalanceForward = etlTransaction.IsBalanceForward;
                    temp.IssueDate = etlTransaction.IssuedDate;
                    temp.OwnerOrgId = etlTransaction.OwnerOrgId;
                    temp.CustomerOrgId = etlTransaction.CustomerOrgId;
                    temp.TransactionId = etlTransaction.TransactionId;
                    temp.Credit = (decimal)journal.Credit;
                    temp.Debit = (decimal)journal.Debit;
                    detail.Add(temp);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return detail;
        }

        public bool IsExistFinnancialCustomerLiabilitySummaryFact(Session session,
                                                                    Guid OwnerOrgId,
                                                                    Guid CustomerOrgId,
                                                                    DateTime IssueDate,
                                                                    string FinancialAccountCode)
        {
            bool result = true;
            try
            {
                Util util = new Util();
                OwnerOrgDim ownerOrgDim = util.GetXpoObjectByFieldName<OwnerOrgDim, Guid>(session, "RefId", OwnerOrgId, BinaryOperatorType.Equal);
                CustomerOrgDim CustomerOrgDim = util.GetXpoObjectByFieldName<CustomerOrgDim, Guid>(session, "RefId", CustomerOrgId, BinaryOperatorType.Equal);
                MonthDim monthDim = util.GetXpoObjectByFieldName<MonthDim, string>(session, "Name", IssueDate.Month.ToString(), BinaryOperatorType.Equal);
                YearDim yearDim = util.GetXpoObjectByFieldName<YearDim, string>(session, "Name", IssueDate.Year.ToString(), BinaryOperatorType.Equal);
                FinancialAccountDim financialAccountDim = util.GetXpoObjectByFieldName<FinancialAccountDim, string>(session, "Code", FinancialAccountCode, BinaryOperatorType.Equal);

                if (ownerOrgDim == null || CustomerOrgDim == null || monthDim == null || yearDim == null || financialAccountDim == null)
                {
                    return false;
                }
                else
                {
                    FinancialCustomerLiabilitySummary_Fact a = null;
                    CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                    CriteriaOperator criteria_OwnerOrg = new BinaryOperator("OwnerOrgDimId", ownerOrgDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_CustomerOrg = new BinaryOperator("CustomerOrgDimId", CustomerOrgDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_Month = new BinaryOperator("MonthDimID", monthDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_Year = new BinaryOperator("YearDimId", yearDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_AccountCode = new BinaryOperator("FinancialAccountDimId", financialAccountDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria = CriteriaOperator.And(criteria_RowStatus, criteria_OwnerOrg, criteria_CustomerOrg, criteria_Month, criteria_Year, criteria_AccountCode);

                    FinancialCustomerLiabilitySummary_Fact fact = session.FindObject<FinancialCustomerLiabilitySummary_Fact>(criteria);

                    if (fact == null) return false;
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        public FinancialCustomerLiabilitySummary_Fact GetFinnancialCustomerLiabilitySummaryFact(
                                                                    Session session,
                                                                    Guid OwnerOrgId,
                                                                    Guid CustomerOrgId,
                                                                    DateTime IssueDate,
                                                                    string FinancialAccountCode)
        {
            FinancialCustomerLiabilitySummary_Fact result = null;
            try
            {
                Util util = new Util();
                OwnerOrgDim ownerOrgDim = util.GetXpoObjectByFieldName<OwnerOrgDim, Guid>(session, "RefId", OwnerOrgId, BinaryOperatorType.Equal);
                CustomerOrgDim CustomerOrgDim = util.GetXpoObjectByFieldName<CustomerOrgDim, Guid>(session, "RefId", CustomerOrgId, BinaryOperatorType.Equal);
                MonthDim monthDim = util.GetXpoObjectByFieldName<MonthDim, string>(session, "Name", IssueDate.Month.ToString(), BinaryOperatorType.Equal);
                YearDim yearDim = util.GetXpoObjectByFieldName<YearDim, string>(session, "Name", IssueDate.Year.ToString(), BinaryOperatorType.Equal);
                FinancialAccountDim financialAccountDim = util.GetXpoObjectByFieldName<FinancialAccountDim, string>(session, "Code", FinancialAccountCode, BinaryOperatorType.Equal);

                if (ownerOrgDim == null || CustomerOrgDim == null || monthDim == null || yearDim == null || financialAccountDim == null)
                {
                    return null;
                }
                else
                {
                    FinancialCustomerLiabilitySummary_Fact a = null;
                    CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                    CriteriaOperator criteria_OwnerOrg = new BinaryOperator("OwnerOrgDimId", ownerOrgDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_CustomerOrg = new BinaryOperator("CustomerOrgDimId", CustomerOrgDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_Month = new BinaryOperator("MonthDimId", monthDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_Year = new BinaryOperator("YearDimId", yearDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_AccountCode = new BinaryOperator("FinancialAccountDimId", financialAccountDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria = CriteriaOperator.And(criteria_RowStatus, criteria_OwnerOrg, criteria_CustomerOrg, criteria_Month, criteria_Year, criteria_AccountCode);

                    FinancialCustomerLiabilitySummary_Fact fact = session.FindObject<FinancialCustomerLiabilitySummary_Fact>(criteria);

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

        public FinancialCustomerLiabilitySummary_Fact CreateFinnancialCustomerLiabilitySummaryFact(Session session,
                                                                    Guid OwnerOrgId,
                                                                    Guid CustomerOrgId,
                                                                    DateTime IssueDate,
                                                                    string FinancialAccountCode,
                                                                    bool IsBalanceForward)
        {
            FinancialCustomerLiabilitySummary_Fact result = new FinancialCustomerLiabilitySummary_Fact(session);
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
                result.CustomerOrgDimId = dimBO.GetCustomerOrgDim(session, CustomerOrgId);
                if (result.FinancialAccountDimId == null || result.MonthDimId == null || result.YearDimId == null || result.OwnerOrgDimId == null || result.CustomerOrgDimId == null)
                {
                    return null;
                }
                result.Save();
                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public XPCollection<FinancialCustomerLiabilityDetail> GetFinancialCustomerLiabilityDetail(
                                                                    Session session,
                                                                    Guid OwnerOrgId,
                                                                    Guid CustomerOrgId,
                                                                    DateTime IssueDate,
                                                                    string FinancialAccountCode)
        {
            XPCollection<FinancialCustomerLiabilityDetail> result = null;
            try
            {
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        public void CreateFinancialCustomerLiabilityDetail(
            Session session, 
            ETL_FinnancialCustomerLiabilityDetail Detail, 
            string MainAccountCode)
        {
            try
            {
                Util util = new Util();
                CorrespondFinancialAccountDim defaultCorrespondindAcc = CorrespondFinancialAccountDim.GetDefault(session, CorrespondFinancialAccountDimEnum.NAAN_DEFAULT);
                FinancialAccountDim defaultFinancialAcc = FinancialAccountDim.GetDefault(session, FinancialAccountDimEnum.NAAN_DEFAULT);
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                FinancialCustomerLiabilitySummary_Fact Fact = GetFinnancialCustomerLiabilitySummaryFact(session, Detail.OwnerOrgId, Detail.CustomerOrgId, Detail.IssueDate, MainAccountCode);
                FinancialCustomerLiabilityDetail newDetail = new FinancialCustomerLiabilityDetail(session);
                if (Fact == null)
                {
                    Fact = CreateFinnancialCustomerLiabilitySummaryFact(session, Detail.OwnerOrgId, Detail.CustomerOrgId, Detail.IssueDate, MainAccountCode, Detail.IsBalanceForward);
                    if (Fact == null) return;
                }

                var date = new DateTime(Detail.IssueDate.Year, Detail.IssueDate.Month, 1);
                FinancialCustomerLiabilitySummary_Fact previousSummary = GetFinnancialCustomerLiabilitySummaryFact(session,
                    Detail.OwnerOrgId, Detail.CustomerOrgId, date.AddMonths(-1), MainAccountCode);

                if (previousSummary != null)
                {
                    Fact.BeginCreditBalance = previousSummary.EndCreditBalance;
                    Fact.BeginDebitBalance = previousSummary.EndDebitBalance;
                }

                /*2014/02/22 Duc.Vo MOD START*/
                CorrespondFinancialAccountDim correspondFinancialAccountDim = null;
                FinancialAccountDim financialAccountDim = null;

                if (!Detail.CorrespondAccountCode.Equals(string.Empty))
                    correspondFinancialAccountDim = util.GetXpoObjectByFieldName<CorrespondFinancialAccountDim, string>(session, "Code", Detail.CorrespondAccountCode, BinaryOperatorType.Equal);
                if (!MainAccountCode.Equals(string.Empty))
                    financialAccountDim = util.GetXpoObjectByFieldName<FinancialAccountDim, string>(session, "Code", MainAccountCode, BinaryOperatorType.Equal);
                /*2014/02/22 Duc.Vo MOD END*/

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

                /*2014/02/22 Duc.Vo INS START*/
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
                newDetail.CurrencyDimId = currencyDim;
                newDetail.FinancialAccountDimId = financialAccountDim;
                newDetail.FinancialCustomerLiabilitySummary_FactId = Fact;
                newDetail.FinancialTransactionDimId = financialTransactionDim;

                /*2014-02-22 ERP-1417 Duc.Vo INS START*/


                if (newDetail.FinancialAccountDimId == null)
                    newDetail.FinancialAccountDimId = defaultFinancialAcc;
                if (newDetail.CorrespondFinancialAccountDimId == null)
                    newDetail.CorrespondFinancialAccountDimId = defaultCorrespondindAcc;
                /*2014-02-22 ERP-1417 Duc.Vo INS END*/

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
                    Fact.CreditSum = Fact.FinancialCustomerLiabilityDetails.Where(i => i.RowStatus == 1
                        && i.Credit > 0 && i.CorrespondFinancialAccountDimId == defaultCorrespondindAcc).Sum(d => d.Credit);

                    Fact.DebitSum = Fact.FinancialCustomerLiabilityDetails.Where(i => i.RowStatus == 1
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

        public void LoadFinancialCustomerLiabilityDetail(Session session, List<ETL_FinnancialCustomerLiabilityDetail> DetailList, string MainAccountCode)
        {
            try
            {
                foreach (ETL_FinnancialCustomerLiabilityDetail detail in DetailList)
                {
                    CreateFinancialCustomerLiabilityDetail(session, detail, MainAccountCode);
                    
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public void FixInvokedBussinessObjects(Session session, XPCollection<DAL.System.Log.BusinessObject> invokedBussinessObjects)
        {
            if (invokedBussinessObjects == null || invokedBussinessObjects.Count == 0)
                return;

            CriteriaOperator criteria_0 = CriteriaOperator.Parse("not(IsNull(FinancialTransactionDimId))");
            CriteriaOperator criteria_1 = new InOperator("FinancialTransactionDimId.RefId", invokedBussinessObjects.Select(i => i.RefId));
            CriteriaOperator criteria_2 = new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater);
            CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1, criteria_2);
            CorrespondFinancialAccountDim defaultAccDim = CorrespondFinancialAccountDim.GetDefault(session, CorrespondFinancialAccountDimEnum.NAAN_DEFAULT);

            XPCollection<FinancialCustomerLiabilityDetail> neededToBeFixList = new XPCollection<FinancialCustomerLiabilityDetail>(session, criteria);
            FinancialCustomerLiabilitySummary_Fact fact = null;

            if (neededToBeFixList != null && neededToBeFixList.Count > 0)
            {
                foreach (FinancialCustomerLiabilityDetail detail in neededToBeFixList)
                {
                    fact = detail.FinancialCustomerLiabilitySummary_FactId;
                    detail.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    detail.Save();
                    if (defaultAccDim != null && detail.CorrespondFinancialAccountDimId != null
                        && detail.CorrespondFinancialAccountDimId.Code.Equals(defaultAccDim.Code))
                    {
                        fact.CreditSum -= detail.Credit;
                        fact.DebitSum -= detail.Debit;
                    }

                    fact.EndCreditBalance
                            = fact.BeginCreditBalance +
                            fact.CreditSum -
                            fact.DebitSum;

                    fact.EndDebitBalance
                        = fact.BeginDebitBalance +
                        fact.DebitSum -
                        fact.CreditSum;

                    fact.Save();
                }
            }
        }
    }
}
