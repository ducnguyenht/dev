using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.BO.ETL.Accounting.Interface;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.Accounting.Journal;
using Utility;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL.BI.Accounting.Finance.PrepaidExpense;
using NAS.BO.ETL.Accounting.TempData;
using NAS.BO.ETL.Accounting.FinancialPrepaidExpense.TempData;
using NAS.DAL;
using NAS.DAL.Nomenclature.Organization;
using NAS.BO.Accounting.Journal;
using NAS.BO.Accounting;
using NAS.BO.ETL.Accounting.FinancialLiability;
using NAS.DAL.Accounting.Currency;
using NAS.DAL.BI.Accounting;
using NAS.DAL.BI.Actor;
using NAS.DAL.System.ShareDim;

namespace NAS.BO.ETL.Accounting.FinancialPrepaidExpense
{
    public abstract class FinancialPrepaidExpenseStrategy : IELTLogicJob
    {
        private ETLAccountingBO accountingBO = new ETLAccountingBO();

        protected ETL_FinancialPrepaidExpenseTransaction fFinancialTransactionData;
        public ETL_FinancialPrepaidExpenseTransaction FinancialTransactionData
        {
            get { return fFinancialTransactionData; }
        }

        protected ETL_FinancialPrepaidExpenseTransformData fFinancialTransformData;
        public ETL_FinancialPrepaidExpenseTransformData FinancialTransformData
        {
            get { return fFinancialTransformData; }
        }

         public FinancialPrepaidExpenseStrategy()
        {
            this.fIsRelatedStrategy = false;
            this.fAccountCode = string.Empty;
            this.fTransactionId = Guid.Empty;
            this.fFinancialTransactionData = new ETL_FinancialPrepaidExpenseTransaction();
            this.fFinancialTransformData = new ETL_FinancialPrepaidExpenseTransformData();
        }

        public FinancialPrepaidExpenseStrategy(Guid transactionId, string accountCode)
        {
            this.fIsRelatedStrategy = false;
            this.fAccountCode = accountCode;
            this.fTransactionId = transactionId;
            this.fFinancialTransactionData = new ETL_FinancialPrepaidExpenseTransaction();
            this.fFinancialTransformData = new ETL_FinancialPrepaidExpenseTransformData();
        }

        #region Implement IELTLogicJob

        protected string fAccountCode;
        public string AccountCode
        {
            get { return fAccountCode; }
        }

        protected Guid fTransactionId;
        public Guid TransactionId
        {
            get { return fTransactionId; }
        }

        protected bool fIsRelatedStrategy;
        public bool IsRelatedStrategy
        {
            get { return fIsRelatedStrategy; }
        }

        public abstract void ExtractTransaction(Session session);

        public abstract void TransformTransaction(Session session);

        public abstract void LoadTransaction(Session session);

        public void GetIsRelatedStrategy(Session session)
        {
            fIsRelatedStrategy = false;
            Transaction transaction = session.GetObjectByKey<Transaction>(this.TransactionId);
            if (transaction == null)
            {
                return;
            }

            CriteriaOperator criteria_RowStatus
                    = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
            CriteriaOperator criteria_Code = new BinaryOperator("Code", this.AccountCode, BinaryOperatorType.Equal);
            CriteriaOperator criteria = CriteriaOperator.And(criteria_Code, criteria_RowStatus);
            Account account = session.FindObject<Account>(criteria);

            foreach (GeneralJournal journal
                    in
                    transaction.GeneralJournals.Where(i =>
                        i.RowStatus == Constant.ROWSTATUS_BOOKED_ENTRY || i.RowStatus == Constant.ROWSTATUS_ACTIVE))
            {
                Account tmpAccount = session.GetObjectByKey<Account>(journal.AccountId.AccountId);
                bool flgIsLeafAccount = tmpAccount.Accounts == null || tmpAccount.Accounts.Count == 0 ? true : false;

                if (flgIsLeafAccount && accountingBO.IsRelateAccount(session, account.AccountId, tmpAccount.AccountId))
                {
                    fIsRelatedStrategy = true;
                    return;
                }
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

            XPCollection<FinancialPrepaidExpenseDetail> neededToBeFixList = new XPCollection<FinancialPrepaidExpenseDetail>(session, criteria);
            FinancialPrepaidExpenseSummary_Fact fact = null;

            if (neededToBeFixList != null && neededToBeFixList.Count > 0)
            {
                foreach (FinancialPrepaidExpenseDetail detail in neededToBeFixList)
                {
                    fact = detail.FinancialPrepaidExpenseSummary_FactId;
                    detail.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    detail.Save();
                    //if (defaultAccDim != null && detail.CorrespondFinancialAccountDimId != null
                    //    && detail.CorrespondFinancialAccountDimId.Code.Equals(defaultAccDim.Code))
                    //{
                    //    fact.CreditSum -= detail.Credit;
                    //    fact.DebitSum -= detail.Debit;
                    //}

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
        }       
        #endregion

        public ETL_FinancialPrepaidExpenseTransaction ExtractTransaction(Session session, Guid TransactionId, string AccountCode)
        {
            ETL_FinancialPrepaidExpenseTransaction resultTransaction = null;
            try
            {
                bool Acceptable = false;
                CriteriaOperator criteria_RowStatus
                    = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_Code = new BinaryOperator("Code", AccountCode, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_Code, criteria_RowStatus);
                Account account = session.FindObject<Account>(criteria);

                Organization defaultOrg = Organization.GetDefault(session, OrganizationEnum.NAAN_DEFAULT);
                Organization currentDeployOrg = Organization.GetDefault(session, OrganizationEnum.QUASAPHARCO);
                Account defaultAccount = Account.GetDefault(session, DefaultAccountEnum.NAAN_DEFAULT);
                Transaction transaction = session.GetObjectByKey<Transaction>(TransactionId);
                if (transaction == null)
                {
                    return resultTransaction;
                }

                resultTransaction = new ETL_FinancialPrepaidExpenseTransaction();
                if (currentDeployOrg != null)
                    resultTransaction.OwnerOrgId = currentDeployOrg.OrganizationId;
                else
                    resultTransaction.OwnerOrgId = defaultOrg.OrganizationId;

                resultTransaction.TransactionId = transaction.TransactionId;
                resultTransaction.Amount = transaction.Amount;
                resultTransaction.Code = transaction.Code;
                resultTransaction.CreateDate = transaction.CreateDate;
                resultTransaction.Description = transaction.Description;
                resultTransaction.IsBalanceForward = (transaction is BalanceForwardTransaction);
                resultTransaction.IssuedDate = transaction.IssueDate;
                resultTransaction.UpdateDate = transaction.UpdateDate;
                resultTransaction.GeneralJournalList = new List<ETL_GeneralJournal>();

                foreach (GeneralJournal journal
                    in
                    transaction.GeneralJournals.Where(i => i.RowStatus == Constant.ROWSTATUS_BOOKED_ENTRY || i.RowStatus == Constant.ROWSTATUS_ACTIVE))
                {
                    ETL_GeneralJournal tempJournal = new ETL_GeneralJournal();
                    if (journal.AccountId != null)
                        tempJournal.AccountId = journal.AccountId.AccountId;
                    else
                        tempJournal.AccountId = defaultAccount.AccountId;

                    tempJournal.CreateDate = journal.CreateDate;
                    tempJournal.Credit = journal.Credit;
                    if (journal.CurrencyId == null)
                    {
                        tempJournal.CurrencyId = CurrencyBO.DefaultCurrency(session).CurrencyId;
                    }
                    else
                    {
                        tempJournal.CurrencyId = journal.CurrencyId.CurrencyId;
                    }
                    tempJournal.Debit = journal.Debit;
                    tempJournal.Description = journal.Description;
                    tempJournal.GeneralJournalId = journal.GeneralJournalId;
                    tempJournal.JournalType = journal.JournalType;
                    resultTransaction.GeneralJournalList.Add(tempJournal);

                    Account tmpAccount = session.GetObjectByKey<Account>(tempJournal.AccountId);
                    bool flgIsLeafAccount = tmpAccount.Accounts == null || tmpAccount.Accounts.Count == 0 ? true : false;

                    if (flgIsLeafAccount && 
                        accountingBO.IsRelateAccount(session, account.AccountId, tempJournal.AccountId))
                    {
                        Acceptable = true;
                    }
                }
                if (!Acceptable) return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            resultTransaction.AccountCode = AccountCode;
            return resultTransaction;
        }

        protected ETL_FinancialPrepaidExpenseTransformData TransformTransaction
        (
            Session session,
            ETL_FinancialPrepaidExpenseTransaction transaction,
            string AccountCode)
        {
            Util util = new Util();
            ETL_FinancialPrepaidExpenseTransformData result = new ETL_FinancialPrepaidExpenseTransformData();
            Account account = util.GetXpoObjectByFieldName<Account, string>(session, "Code", AccountCode, BinaryOperatorType.Equal);
            if (account == null) return null;
            if (transaction == null) return null;
            ETL_Transaction etlTransaction = transaction;

            string subMainAccount = string.Empty;

            List<ETL_FinancialPrepaidExpenseDetail> detail = new List<ETL_FinancialPrepaidExpenseDetail>();
            try
            {
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                FinancialLiabilityBO liabilityBO = new FinancialLiabilityBO();
                List<ETL_GeneralJournal> JournalListJoined = JoinJournal(session, etlTransaction.GeneralJournalList);
                List<ETL_GeneralJournal> FinishJournalList = liabilityBO.ClearJournalList(session, JournalListJoined, account.AccountId);
                foreach (ETL_GeneralJournal journal in FinishJournalList)
                {
                    ETL_FinancialPrepaidExpenseDetail temp = new ETL_FinancialPrepaidExpenseDetail();
                    temp.AccountCode = string.Empty;
                    temp.CorrespondAccountCode = string.Empty;
                    if (accountingBO.IsRelateAccount(session, account.AccountId, journal.AccountId))
                    {
                        temp.AccountCode = subMainAccount = session.GetObjectByKey<Account>(journal.AccountId).Code;
                    }
                    else
                    {
                        temp.CorrespondAccountCode = session.GetObjectByKey<Account>(journal.AccountId).Code;
                    }
                    temp.CurrencyCode = session.GetObjectByKey<Currency>(journal.CurrencyId).Code;
                    temp.IsBalanceForward = etlTransaction.IsBalanceForward;
                    temp.IssueDate = etlTransaction.IssuedDate;
                    temp.OwnerOrgId = etlTransaction.OwnerOrgId;
                    temp.TransactionId = etlTransaction.TransactionId;
                    temp.Credit = (decimal)journal.Credit;
                    temp.Debit = (decimal)journal.Debit;
                    detail.Add(temp);
                }

                result.MainAccountCode = subMainAccount;
                result.ETL_DetailList = detail;
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }

        protected void LoadTransaction(Session session, ETL_FinancialPrepaidExpenseTransformData data)
        {
            try
            {
                foreach (ETL_FinancialPrepaidExpenseDetail detail in data.ETL_DetailList)
                {
                    CreateFinancialPrepaidExpenseDetail(session, detail, data.MainAccountCode);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public void CreateFinancialPrepaidExpenseDetail(
            Session session,
            ETL_FinancialPrepaidExpenseDetail Detail,
            string MainAccountCode)
        {
            try
            {
                Util util = new Util();
                CorrespondFinancialAccountDim defaultCorrespondindAcc = CorrespondFinancialAccountDim.GetDefault(session, CorrespondFinancialAccountDimEnum.NAAN_DEFAULT);
                FinancialAccountDim defaultFinancialAcc = FinancialAccountDim.GetDefault(session, FinancialAccountDimEnum.NAAN_DEFAULT);
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                FinancialPrepaidExpenseSummary_Fact Fact = GetFinancialPrepaidExpenseSummaryFact(session, Detail.OwnerOrgId, Detail.IssueDate, MainAccountCode);
                FinancialPrepaidExpenseDetail newDetail = new FinancialPrepaidExpenseDetail(session);
                if (Fact == null)
                {
                    Fact = CreateFinancialPrepaidExpenseSummaryFact(session, Detail.OwnerOrgId, Detail.IssueDate, MainAccountCode, Detail.IsBalanceForward);
                    if (Fact == null) return;
                }
                else
                {
                    var date = new DateTime(Detail.IssueDate.Year, Detail.IssueDate.Month, 1);
                    FinancialPrepaidExpenseSummary_Fact previousSummary = GetFinancialPrepaidExpenseSummaryFact(session,
                        Detail.OwnerOrgId, date.AddMonths(-1), MainAccountCode);

                    if (previousSummary != null)
                    {
                        Fact.BeginCreditBalance = previousSummary.EndCreditBalance;
                        Fact.BeginDebitBalance= previousSummary.EndDebitBalance;
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
                newDetail.FinancialPrepaidExpenseSummary_FactId = Fact;
                newDetail.FinancialTransactionDimId = financialTransactionDim;

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
                    Fact.CreditSum = Fact.FinancialPrepaidExpenseDetails.Where(i => i.RowStatus == 1
                        && i.Credit > 0 && i.CorrespondFinancialAccountDimId == defaultCorrespondindAcc).Sum(d => d.Credit);

                    Fact.DebitSum = Fact.FinancialPrepaidExpenseDetails.Where(i => i.RowStatus == 1
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

        public List<ETL_GeneralJournal> JoinJournal(Session session, List<ETL_GeneralJournal> journalList)
        {
            List<ETL_GeneralJournal> result = new List<ETL_GeneralJournal>();
            try
            {
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                foreach (ETL_GeneralJournal journal in journalList)
                {
                    ETL_GeneralJournal rsJournal = GetJournal(session, result, journal.AccountId);
                    if (rsJournal == null)
                    {
                        rsJournal = new ETL_GeneralJournal();
                        rsJournal.AccountId = accountingBO.GetHighestAccount(session, journal.AccountId).AccountId;
                        rsJournal.CreateDate = journal.CreateDate;
                        rsJournal.Credit = journal.Credit;
                        rsJournal.CurrencyId = journal.CurrencyId;
                        rsJournal.Debit = journal.Debit;
                        rsJournal.JournalType = journal.JournalType;
                        result.Add(rsJournal);
                    }
                    else
                    {
                        rsJournal.Credit += journal.Credit;
                        rsJournal.Debit += journal.Debit;
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private ETL_GeneralJournal GetJournal(Session session, List<ETL_GeneralJournal> journalList, Guid AccountId)
        {
            ETL_GeneralJournal result = new ETL_GeneralJournal();
            try
            {
                Account account = session.GetObjectByKey<Account>(AccountId);
                if (account == null) return null;
                return GetJournal(session, journalList, account.Code);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private ETL_GeneralJournal GetJournal(Session session, List<ETL_GeneralJournal> journalList, string AccountCode)
        {
            ETLAccountingBO accountingBO = new ETLAccountingBO();
            ETL_GeneralJournal result = new ETL_GeneralJournal();
            try
            {
                if (journalList == null) return null;
                Util util = new Util();
                Account account = util.GetXpoObjectByFieldName<Account, string>(session, "Code", AccountCode, BinaryOperatorType.Equal);
                if (account == null) return null;
                foreach (ETL_GeneralJournal journal in journalList)
                {
                    if (accountingBO.IsRelateAccount(session, journal.AccountId, account.AccountId))
                    {
                        return journal;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private FinancialPrepaidExpenseSummary_Fact GetFinancialPrepaidExpenseSummaryFact(
                                                                    Session session,
                                                                    Guid OwnerOrgId,
                                                                    DateTime IssueDate,
                                                                    string FinancialAccountCode)
        {
            FinancialPrepaidExpenseSummary_Fact result = null;
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
                    FinancialPrepaidExpenseSummary_Fact a = null;
                    CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                    CriteriaOperator criteria_OwnerOrg = new BinaryOperator("OwnerOrgDimId", ownerOrgDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_Month = new BinaryOperator("MonthDimId", monthDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_Year = new BinaryOperator("YearDimId", yearDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_AccountCode = new BinaryOperator("FinancialAccountDimId", financialAccountDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria = CriteriaOperator.And(criteria_RowStatus, criteria_OwnerOrg, criteria_Month, criteria_Year, criteria_AccountCode);

                    FinancialPrepaidExpenseSummary_Fact fact = session.FindObject<FinancialPrepaidExpenseSummary_Fact>(criteria);

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

        private FinancialPrepaidExpenseSummary_Fact CreateFinancialPrepaidExpenseSummaryFact(Session session,
                                                                    Guid OwnerOrgId,
                                                                    DateTime IssueDate,
                                                                    string FinancialAccountCode,
                                                                    bool IsBalanceForward)
        {
            FinancialPrepaidExpenseSummary_Fact result = new FinancialPrepaidExpenseSummary_Fact(session);
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
                FinancialPrepaidExpenseSummary_Fact previousSummary = GetFinancialPrepaidExpenseSummaryFact(session,
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
