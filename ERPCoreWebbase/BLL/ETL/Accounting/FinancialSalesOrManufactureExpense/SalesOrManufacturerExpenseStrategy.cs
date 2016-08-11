using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.BO.ETL.Accounting.FinancialSalesOrManufactureExpense.TempData;
using NAS.BO.ETL.Accounting.FinancialActualPrice.TempData;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;
using DevExpress.Data.Filtering;
using Utility;
using NAS.DAL.Accounting.AccountChart;
using NAS.BO.ETL.Accounting.Interface;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.BI.Accounting.Finance.SalesOrManufactureExpense;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL;
using NAS.BO.Accounting.Journal;
using NAS.BO.ETL.Accounting.TempData;
using NAS.BO.Accounting;
using NAS.BO.ETL.Accounting.FinancialLiability;
using NAS.DAL.Accounting.Currency;
using NAS.DAL.BI.Actor;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Accounting;
using Utility.ETL;
using System.Diagnostics;

namespace NAS.BO.ETL.Accounting.FinancialSalesOrManufactureExpense
{
    public abstract class SalesOrManufacturerExpenseStrategy : IELTLogicJob
    {
        public SalesOrManufacturerExpenseStrategy()
        {
            this.fIsRelatedStrategy = false;
            this.fAccountCode = string.Empty;
            this.fTransactionId = Guid.Empty;
            this.fFinancialTransactionData = new ETL_SalesOrManufacturerExpenseTransaction();
            this.fFinancialTransformData = new ETL_SalesOrManufacturerExpenseTransformData();
        }

        public SalesOrManufacturerExpenseStrategy(Guid transactionId, string accountCode)
        {
            this.fIsRelatedStrategy = false;
            this.fAccountCode = accountCode;
            this.fTransactionId = transactionId;
            this.fFinancialTransactionData = new ETL_SalesOrManufacturerExpenseTransaction();
            this.fFinancialTransformData = new ETL_SalesOrManufacturerExpenseTransformData();
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

            XPCollection<FinancialSalesOrManufactureExpenseDetail> neededToBeFixList
                = new XPCollection<FinancialSalesOrManufactureExpenseDetail>(session, criteria);
            List<SalesOrManufactureExpenseByGroup> relevantGroups = new List<SalesOrManufactureExpenseByGroup>();
            if (neededToBeFixList != null && neededToBeFixList.Count > 0)
                foreach (FinancialSalesOrManufactureExpenseDetail detail in neededToBeFixList)
                {
                    SalesOrManufactureExpenseByGroup group = null;
                    group = detail.SalesOrManufactureExpenseByGroupId;
                    detail.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    detail.Save();
                    relevantGroups.Add(group);
                }

            if (relevantGroups != null && relevantGroups.Count > 0)
                foreach (SalesOrManufactureExpenseByGroup group in relevantGroups)
                {
                    group.SumExpense = group.FinancialSalesOrManufactureExpenseDetails.Where(i => i.RowStatus >= 1 && i.Debit > 0).
                        Sum(r => r.Debit);
                    group.Save();
                }
        }

        #endregion

        private ETLAccountingBO accountingBO = new ETLAccountingBO();

        protected ETL_SalesOrManufacturerExpenseTransformData fFinancialTransformData;
        public ETL_SalesOrManufacturerExpenseTransformData FinancialTransformData
        {
            get { return fFinancialTransformData; }
        }

        protected ETL_SalesOrManufacturerExpenseTransaction fFinancialTransactionData;
        public ETL_SalesOrManufacturerExpenseTransaction FinancialTransactionData
        {
            get { return fFinancialTransactionData; }
        }

        protected ETL_SalesOrManufacturerExpenseTransaction ExtractTransaction(Session session, Guid TransactionId, string AccountCode)
        {
            ETL_SalesOrManufacturerExpenseTransaction resultTransaction = null;
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

                resultTransaction = new ETL_SalesOrManufacturerExpenseTransaction();
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

                    if (flgIsLeafAccount && tempJournal.Debit > 0 && tempJournal.Credit == 0 &&
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

        protected ETL_SalesOrManufacturerExpenseTransformData TransformTransactionSalesOrManufacturerExpenseDetail
        (
            Session session,
            ETL_SalesOrManufacturerExpenseTransaction transaction,
            string AccountCode)
        {
            Util util = new Util();
            ETL_SalesOrManufacturerExpenseTransformData result = new ETL_SalesOrManufacturerExpenseTransformData();
            Account account = util.GetXpoObjectByFieldName<Account, string>(session, "Code", AccountCode, BinaryOperatorType.Equal);
            if (account == null) return null;
            if (transaction == null) return null;
            ETL_Transaction etlTransaction = transaction;

            string subMainAccount = string.Empty;

            List<ETL_SalesOrManufacturerExpenseDetail> detail = new List<ETL_SalesOrManufacturerExpenseDetail>();
            try
            {
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                FinancialLiabilityBO liabilityBO = new FinancialLiabilityBO();
                List<ETL_GeneralJournal> JournalListJoined = JoinJournal(session, etlTransaction.GeneralJournalList, AccountCode);
                List<ETL_GeneralJournal> FinishJournalList = liabilityBO.ClearJournalList(session, JournalListJoined, account.AccountId);
                foreach (ETL_GeneralJournal journal in FinishJournalList)
                {
                    ETL_SalesOrManufacturerExpenseDetail temp = new ETL_SalesOrManufacturerExpenseDetail();
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

                result.ETL_DetailList = detail;
                result.MainAccountCode = subMainAccount;
                result.HighestAccountCode = AccountCode;
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }

        protected void LoadFinancialSalesOrManufacturerExpenseDetail(Session session, ETL_SalesOrManufacturerExpenseTransformData data)
        {
            try
            {
                foreach (ETL_SalesOrManufacturerExpenseDetail detail in data.ETL_DetailList)
                {
                    CreateFinancialSalesOrManufacturerExpenseDetail(session, detail, data.MainAccountCode, data.HighestAccountCode);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public void CreateFinancialSalesOrManufacturerExpenseDetail(
            Session session, 
            ETL_SalesOrManufacturerExpenseDetail Detail, 
            string MainAccountCode, string HighestAccountCode)
        {
            try
            {
                if (Detail == null ||
                    MainAccountCode.Equals(string.Empty) ||
                    Detail.OwnerOrgId.Equals(Guid.Empty) ||
                    Detail.IssueDate == null)
                    return;

                #region prepare Summary header data
                Util util = new Util();
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                FinancialSalesOrManufactureExpenseSummary_Fact summary = 
                    GetFinancialSalesOrManufactureExpenseSummary(
                        session, 
                        Detail.OwnerOrgId, 
                        Detail.IssueDate);

                FinancialSalesOrManufactureExpenseDetail newDetail = new FinancialSalesOrManufactureExpenseDetail(session);
                if (summary == null)
                {
                    summary = 
                        CreateFinancialSalesOrManufactureExpenseSummary(
                            session, 
                            Detail.OwnerOrgId, 
                            Detail.IssueDate,
                            Detail.IsBalanceForward);
                    if (summary == null) return;
                }
                #endregion

                #region prepare group data
                CorrespondFinancialAccountDim correspondFinancialAccountDim = null;
                FinancialAccountDim financialAccountDim = null;
                FinancialAccountDim GroupAccountDim = null;

                if (!HighestAccountCode.Equals(string.Empty))
                    GroupAccountDim = util.GetXpoObjectByFieldName<FinancialAccountDim, string>(
                        session, 
                        "Code",
                        HighestAccountCode, 
                        BinaryOperatorType.Equal);

                if (!MainAccountCode.Equals(string.Empty))
                    financialAccountDim = util.GetXpoObjectByFieldName<FinancialAccountDim, string>(
                        session, 
                        "Code", 
                        MainAccountCode, 
                        BinaryOperatorType.Equal);

                if (!Detail.CorrespondAccountCode.Equals(string.Empty))
                    correspondFinancialAccountDim = util.GetXpoObjectByFieldName<CorrespondFinancialAccountDim, string>(
                        session, 
                        "Code", 
                        Detail.CorrespondAccountCode, 
                        BinaryOperatorType.Equal);

                if (GroupAccountDim == null && !HighestAccountCode.Equals(string.Empty))
                {
                    GroupAccountDim = accountingBO.CreateFinancialAccountDim(session, HighestAccountCode);
                }


                if (financialAccountDim == null && !MainAccountCode.Equals(string.Empty))
                {
                    financialAccountDim = accountingBO.CreateFinancialAccountDim(session, MainAccountCode);
                }

                if (correspondFinancialAccountDim == null && !Detail.CorrespondAccountCode.Equals(string.Empty))
                {
                    correspondFinancialAccountDim = accountingBO.CreateCorrespondFinancialAccountDim(session, Detail.CorrespondAccountCode);
                }                

                SalesOrManufactureExpenseByGroup group = 
                    GetSalesOrManufactureExpenseByGroup(
                        session, 
                        summary.FinancialSalesOrManufactureExpenseSummary_FactId, 
                        HighestAccountCode);

                if (group == null)
                {
                    group = CreateSalesOrManufactureExpenseByGroup(
                        session,
                        summary.FinancialSalesOrManufactureExpenseSummary_FactId, 
                        HighestAccountCode);

                    if (group == null)
                    {
                        return;
                    }
                }

                #endregion                

                #region prepare Detail
                CurrencyDim currencyDim = util.GetXpoObjectByFieldName<CurrencyDim, string>(
                    session, 
                    "Code", 
                    Detail.CurrencyCode, 
                    BinaryOperatorType.Equal);

                if (currencyDim == null && !Detail.CurrencyCode.Equals(string.Empty))
                {
                    currencyDim = accountingBO.CreateCurrencyDim(session, Detail.CurrencyCode);
                }

                FinancialTransactionDim financialTransactionDim = util.GetXpoObjectByFieldName<FinancialTransactionDim, Guid>(
                    session, 
                    "RefId", 
                    Detail.TransactionId, 
                    BinaryOperatorType.Equal);

                if (financialTransactionDim == null)
                {
                    financialTransactionDim = accountingBO.CreateFinancialTransactionDim(session, Detail.TransactionId);
                    if (financialTransactionDim == null)
                    {
                        return;
                    }
                }
                newDetail.Credit = Detail.Credit;
                newDetail.Debit = Detail.Debit;
                newDetail.CurrencyDimId = currencyDim;
                newDetail.FinancialTransactionDimId = financialTransactionDim;
                newDetail.FinancialAccountDimId = financialAccountDim;
                newDetail.CorrespondFinancialAccountDimId = correspondFinancialAccountDim;
                newDetail.SalesOrManufactureExpenseByGroupId = group;
                CorrespondFinancialAccountDim defaultCorrespondindAcc = CorrespondFinancialAccountDim.GetDefault(session, CorrespondFinancialAccountDimEnum.NAAN_DEFAULT);
                FinancialAccountDim defaultFinancialAcc = FinancialAccountDim.GetDefault(session, FinancialAccountDimEnum.NAAN_DEFAULT);

                if (newDetail.FinancialAccountDimId == null)
                    newDetail.FinancialAccountDimId = defaultFinancialAcc;
                if (newDetail.CorrespondFinancialAccountDimId == null)
                    newDetail.CorrespondFinancialAccountDimId = defaultCorrespondindAcc;

                newDetail.RowStatus = Constant.ROWSTATUS_ACTIVE;   
                newDetail.Save();
                #endregion

                if (Detail.IsBalanceForward)
                {
                    group.SumExpense = Detail.Credit;
                }
                else
                {
                    group.SumExpense = group.FinancialSalesOrManufactureExpenseDetails.Where(i => i.RowStatus >= 1 && i.Debit > 0).
                        Sum(r => r.Debit);
                    group.Save();
                }

                group.Save();
                summary.Save();
            }
            catch (Exception)
            {
                return;
            }
        }

        private List<ETL_GeneralJournal> JoinJournal(Session session, List<ETL_GeneralJournal> journalList, string mainAccountCode)
        {
            List<ETL_GeneralJournal> result = new List<ETL_GeneralJournal>();

            CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
            CriteriaOperator criteria_Code = new BinaryOperator("Code", mainAccountCode, BinaryOperatorType.Equal);
            CriteriaOperator criteria = CriteriaOperator.And(criteria_Code, criteria_RowStatus);
            Account mainAccount = session.FindObject<Account>(criteria);

            if (mainAccount == null)
                return null;
            try
            {
                ETLAccountingBO accountingBO = new ETLAccountingBO();                
                foreach (ETL_GeneralJournal journal in journalList)
                {
                    if (accountingBO.IsRelateAccount(session, mainAccount.AccountId, journal.AccountId))
                    {
                        ETL_GeneralJournal masterJournal = new ETL_GeneralJournal();
                        masterJournal.AccountId = journal.AccountId;
                        masterJournal.CreateDate = journal.CreateDate;
                        masterJournal.Credit = journal.Credit;
                        masterJournal.Debit = journal.Debit;
                        masterJournal.CurrencyId = journal.CurrencyId;                        
                        masterJournal.JournalType = journal.JournalType;
                        result.Add(masterJournal);
                    }
                    else
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

        private FinancialSalesOrManufactureExpenseSummary_Fact GetFinancialSalesOrManufactureExpenseSummary(
            Session session,
            Guid OwnerOrgId,
            DateTime IssueDate
        )
        {
            FinancialSalesOrManufactureExpenseSummary_Fact result = null;
            try
            {
                Util util = new Util();
                OwnerOrgDim ownerOrgDim = util.GetXpoObjectByFieldName<OwnerOrgDim, Guid>(session, "RefId", OwnerOrgId, BinaryOperatorType.Equal);
                MonthDim monthDim = util.GetXpoObjectByFieldName<MonthDim, string>(session, "Name", IssueDate.Month.ToString(), BinaryOperatorType.Equal);
                YearDim yearDim = util.GetXpoObjectByFieldName<YearDim, string>(session, "Name", IssueDate.Year.ToString(), BinaryOperatorType.Equal);

                if (ownerOrgDim == null || monthDim == null || yearDim == null)
                {
                    return null;
                }
                else
                {
                    CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                    CriteriaOperator criteria_OwnerOrg = new BinaryOperator("OwnerOrgDimId", ownerOrgDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_Month = new BinaryOperator("MonthDimId", monthDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_Year = new BinaryOperator("YearDimId", yearDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria = CriteriaOperator.And(criteria_RowStatus, criteria_OwnerOrg, criteria_Month, criteria_Year);

                    FinancialSalesOrManufactureExpenseSummary_Fact fact = session.FindObject<FinancialSalesOrManufactureExpenseSummary_Fact>(criteria);
                    
                    if (fact == null) return null;
                    result = fact;
                }
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }

        private FinancialSalesOrManufactureExpenseSummary_Fact CreateFinancialSalesOrManufactureExpenseSummary(
            Session session,
            Guid OwnerOrgId,
            DateTime IssueDate,
            bool IsBalanceForward
            )
        {
            FinancialSalesOrManufactureExpenseSummary_Fact result = new FinancialSalesOrManufactureExpenseSummary_Fact(session);
            try
            {
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                DimBO dimBO = new DimBO();
                result.MonthDimId = dimBO.GetMonthDim(session, (short)IssueDate.Month);
                result.YearDimId = dimBO.GetYearDim(session, (short)IssueDate.Year);
                result.OwnerOrgDimId = dimBO.GetOwnerOrgDim(session, OwnerOrgId);
                result.RowStatus = Constant.ROWSTATUS_ACTIVE;
                if (result.MonthDimId == null || result.YearDimId == null || result.OwnerOrgDimId == null)
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

        private SalesOrManufactureExpenseByGroup GetSalesOrManufactureExpenseByGroup(Session session, int summaryFactId, string HighestAccountCode)
        {
            try
            {
                Util util = new Util();
                FinancialSalesOrManufactureExpenseSummary_Fact summary = session.GetObjectByKey<FinancialSalesOrManufactureExpenseSummary_Fact>(summaryFactId);

                if (summary == null)
                    return null;

                CriteriaOperator criteria_0 = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_1 = new BinaryOperator("FinancialSalesOrManufactureExpenseSummary_FactId", summary, BinaryOperatorType.Equal);
                CriteriaOperator criteria_2 = CriteriaOperator.Parse("not(IsNull(FinancialAccountDimId))");
                CriteriaOperator criteria_3 = new BinaryOperator("FinancialAccountDimId.Code", HighestAccountCode, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_0, criteria_1, criteria_2, criteria_3);    
                return session.FindObject<SalesOrManufactureExpenseByGroup>(criteria);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private SalesOrManufactureExpenseByGroup CreateSalesOrManufactureExpenseByGroup(Session session, int summaryFactId, string HighestAccountCode)
        {
            SalesOrManufactureExpenseByGroup result = new SalesOrManufactureExpenseByGroup(session);
            try
            {
                Util util = new Util();
                FinancialAccountDim GroupAccountDim = null;
                FinancialSalesOrManufactureExpenseSummary_Fact summary = session.GetObjectByKey<FinancialSalesOrManufactureExpenseSummary_Fact>(summaryFactId);

                if (summary == null)
                    return null;

                if (!HighestAccountCode.Equals(string.Empty))
                    GroupAccountDim = util.GetXpoObjectByFieldName<FinancialAccountDim, string>(
                        session,
                        "Code",
                        HighestAccountCode,
                        BinaryOperatorType.Equal);

                if (GroupAccountDim == null && !HighestAccountCode.Equals(string.Empty))
                {
                    GroupAccountDim = accountingBO.CreateFinancialAccountDim(session, HighestAccountCode);
                }

                if (GroupAccountDim == null)
                    return null;

                result.RowStatus = Constant.ROWSTATUS_ACTIVE;
                result.FinancialSalesOrManufactureExpenseSummary_FactId = summary;
                result.FinancialAccountDimId = GroupAccountDim;
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
