using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.BO.ETL.Accounting.TempData;
using NAS.BO.ETL.Accounting.FinancialActualPrice.TempData;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Accounting.Journal;
using Utility;
using NAS.DAL;
using NAS.BO.Accounting.Journal;
using NAS.BO.Accounting;
using NAS.DAL.Accounting.Currency;
using NAS.DAL.BI.Accounting.FinancialActualPrice;
using NAS.DAL.BI.Actor;
using NAS.DAL.System.ShareDim;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.BI.Accounting;
using NAS.BO.ETL.Accounting.FinancialLiability;
using NAS.DAL.System.Log;
using NAS.BO.ETL.Accounting.Interface;

namespace NAS.BO.ETL.Accounting.FinancialActualPrice
{
    public abstract class FinancialActualPriceStrategy : IELTLogicJob
    {
        private ETLAccountingBO accountingBO = new ETLAccountingBO();

        public FinancialActualPriceStrategy()
        {
            this.fIsRelatedStrategy = false;
            this.fAccountCode = string.Empty;
            this.fTransactionId = Guid.Empty;
            this.fActualPriceTransaction = new ETL_ActualPriceTransaction();
            this.fDetailAfterTransformData = new ETL_FinanciaActualPriceTransformData();
        }

        public FinancialActualPriceStrategy(Guid transactionId, string accountCode)
        {
            this.fIsRelatedStrategy = false;
            this.fAccountCode = accountCode;
            this.fTransactionId = transactionId;
            this.fActualPriceTransaction = new ETL_ActualPriceTransaction();
            this.fDetailAfterTransformData = new ETL_FinanciaActualPriceTransformData();
        }

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

        protected ETL_ActualPriceTransaction fActualPriceTransaction;
        public ETL_ActualPriceTransaction ActualPriceTransaction
        {
            get { return fActualPriceTransaction; }
        }

        protected ETL_FinanciaActualPriceTransformData fDetailAfterTransformData;
        public ETL_FinanciaActualPriceTransformData DetailAfterTransformData
        {
            get { return fDetailAfterTransformData; }
        }

        protected bool fIsRelatedStrategy;
        public bool IsRelatedStrategy
        {
            get { return fIsRelatedStrategy; }
        }
        
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

        public void FixInvokedBussinessObjects(Session session, XPCollection<BusinessObject> invokedBussinessObjects)
        {
            if (invokedBussinessObjects == null || invokedBussinessObjects.Count == 0)
                return;

            CriteriaOperator criteria_0 = CriteriaOperator.Parse("not(IsNull(FinancialTransactionDimId))");
            CriteriaOperator criteria_1 = new InOperator("FinancialTransactionDimId.RefId", invokedBussinessObjects.Select(i => i.RefId));
            CriteriaOperator criteria_2 = new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater);
            CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1, criteria_2);
            CorrespondFinancialAccountDim defaultCorrespondAccDim = CorrespondFinancialAccountDim.GetDefault(session, CorrespondFinancialAccountDimEnum.NAAN_DEFAULT);

            XPCollection<FinancialActualPriceDetail> neededToBeFixList = new XPCollection<FinancialActualPriceDetail>(session, criteria);
            FinancialActualPriceSummary_Fact fact = null;
            if (neededToBeFixList != null && neededToBeFixList.Count > 0)
                foreach (FinancialActualPriceDetail detail in neededToBeFixList)
                {
                    fact = detail.FinancialActualPriceSummary_FactId;
                    detail.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    detail.Save();
                    fact.Save();
                }
        }

        public abstract void ExtractTransaction(Session session);

        protected ETL_ActualPriceTransaction ExtractTransaction(Session session, Guid TransactionId, string AccountCode)
        {
            ETL_ActualPriceTransaction resultTransaction = null;
            try
            {
                bool Acceptable = false;
                CriteriaOperator criteria_RowStatus 
                    = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_Code = new BinaryOperator("Code", AccountCode, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_Code, criteria_RowStatus);
                Account account = session.FindObject<Account>(criteria);
                /*2014/02/20 Duc.Vo INS START*/

                Organization defaultOrg = Organization.GetDefault(session, OrganizationEnum.NAAN_DEFAULT);
                Organization currentDeployOrg = Organization.GetDefault(session, OrganizationEnum.QUASAPHARCO);
                Account defaultAccount = Account.GetDefault(session, DefaultAccountEnum.NAAN_DEFAULT);
                /*2014/02/20 Duc.Vo INS END*/
                Transaction transaction = session.GetObjectByKey<Transaction>(TransactionId);
                if (transaction == null)
                {
                    return resultTransaction;
                }

                Util util = new Util();

                /*2014/02/20 Duc.Vo MOD START*/
                resultTransaction = new ETL_ActualPriceTransaction();
                if (currentDeployOrg != null)
                    resultTransaction.OwnerOrgId = currentDeployOrg.OrganizationId;
                else
                    resultTransaction.OwnerOrgId = defaultOrg.OrganizationId;

                if (transaction is SaleInvoiceTransaction)
                {
                    if ((transaction as SaleInvoiceTransaction).SalesInvoiceId.SourceOrganizationId != null)
                        resultTransaction.CustomerOrgId = (transaction as SaleInvoiceTransaction).SalesInvoiceId.SourceOrganizationId.OrganizationId;
                    else
                        resultTransaction.CustomerOrgId = defaultOrg.OrganizationId;
                }
                else if (transaction is PurchaseInvoiceTransaction)
                {
                    if ((transaction as PurchaseInvoiceTransaction).PurchaseInvoiceId.SourceOrganizationId != null)
                        resultTransaction.SupplierOrgId = 
                            (transaction as PurchaseInvoiceTransaction).PurchaseInvoiceId.SourceOrganizationId.OrganizationId;
                    else
                        resultTransaction.SupplierOrgId = defaultOrg.OrganizationId;
                }
                else if (transaction is PaymentVouchesTransaction)
                {
                    PaymentVoucherTransactionBO paymentVoucherTransactionBO = new PaymentVoucherTransactionBO();

                    Organization SuppOrg = paymentVoucherTransactionBO.GetAllocatedSupplier(session, transaction.TransactionId);
                    Organization CustOrg = paymentVoucherTransactionBO.GetAllocatedCustomer(session, transaction.TransactionId);

                    if (SuppOrg != null)
                        resultTransaction.SupplierOrgId = SuppOrg.OrganizationId;
                    else
                        resultTransaction.SupplierOrgId = defaultOrg.OrganizationId;

                    if (CustOrg != null)
                        resultTransaction.CustomerOrgId = CustOrg.OrganizationId;
                    else
                        resultTransaction.CustomerOrgId = defaultOrg.OrganizationId;
                }
                else if (transaction is ReceiptVouchesTransaction)
                {
                    ReceiptVoucherTransactionBO receiptVoucherTransactionBO = new ReceiptVoucherTransactionBO();
                    Organization SuppOrg = receiptVoucherTransactionBO.GetAllocatedSupplier(session, transaction.TransactionId);
                    Organization CustOrg = receiptVoucherTransactionBO.GetAllocatedCustomer(session, transaction.TransactionId);

                    if (SuppOrg != null)
                        resultTransaction.SupplierOrgId = SuppOrg.OrganizationId;
                    else
                        resultTransaction.SupplierOrgId = defaultOrg.OrganizationId;

                    if (CustOrg != null)
                        resultTransaction.CustomerOrgId = CustOrg.OrganizationId;
                    else
                        resultTransaction.CustomerOrgId = defaultOrg.OrganizationId;
                }
                else
                {
                    Organization SuppOrg = accountingBO.GetAllocatedSupplierByManualTransaction(session, transaction.TransactionId);
                    Organization CustOrg = accountingBO.GetAllocatedCustomerByManualTransaction(session, transaction.TransactionId);

                    if (SuppOrg != null)
                        resultTransaction.SupplierOrgId = SuppOrg.OrganizationId;
                    else
                        resultTransaction.SupplierOrgId = defaultOrg.OrganizationId;

                    if (CustOrg != null)
                        resultTransaction.CustomerOrgId = CustOrg.OrganizationId;
                    else
                        resultTransaction.CustomerOrgId = defaultOrg.OrganizationId;
                }

                if (resultTransaction.SupplierOrgId == Guid.Empty)
                    resultTransaction.SupplierOrgId = defaultOrg.OrganizationId;

                if (resultTransaction.CustomerOrgId == Guid.Empty)
                    resultTransaction.CustomerOrgId = defaultOrg.OrganizationId;

                /*2014/02/20 Duc.Vo MOD END*/
                resultTransaction.TransactionId = transaction.TransactionId;
                resultTransaction.Amount = transaction.Amount;
                resultTransaction.Code = transaction.Code;
                resultTransaction.CreateDate = transaction.CreateDate;
                resultTransaction.Description = transaction.Description;
                resultTransaction.IsBalanceForward = (transaction is BalanceForwardTransaction);
                resultTransaction.IssuedDate = transaction.IssueDate;
                resultTransaction.UpdateDate = transaction.UpdateDate;
                //resultTransaction.GeneralJournalList = new List<ETL_GeneralJournal>();
                List<ETL_GeneralJournal> tmpGeneralJournalList = new List<ETL_GeneralJournal>();

                double sumDebit_NotRelativeBussiness = 0;
                foreach (GeneralJournal journal 
                    in 
                    transaction.GeneralJournals.
                    Where(i => i.RowStatus == Constant.ROWSTATUS_BOOKED_ENTRY || i.RowStatus == Constant.ROWSTATUS_ACTIVE))
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

                    if (tempJournal.Debit > 0
                        && !accountingBO.IsRelateAccount(session, AccountCode, tempJournal.AccountId)
                        && !accountingBO.IsRelateAccount(session, "611", tempJournal.AccountId)
                        && !accountingBO.IsRelateAccount(session, "621", tempJournal.AccountId)
                        && !accountingBO.IsRelateAccount(session, "622", tempJournal.AccountId)
                        && !accountingBO.IsRelateAccount(session, "623", tempJournal.AccountId)
                        && !accountingBO.IsRelateAccount(session, "627", tempJournal.AccountId)
                        && !accountingBO.IsRelateAccount(session, "631", tempJournal.AccountId)
                        && !accountingBO.IsRelateAccount(session, "632", tempJournal.AccountId)
                        && !accountingBO.IsRelateAccount(session, "635", tempJournal.AccountId)
                        && !accountingBO.IsRelateAccount(session, "641", tempJournal.AccountId)
                        && !accountingBO.IsRelateAccount(session, "642", tempJournal.AccountId)
                        && !accountingBO.IsRelateAccount(session, "811", tempJournal.AccountId)
                        && !accountingBO.IsRelateAccount(session, "821", tempJournal.AccountId)
                        )
                    {
                        sumDebit_NotRelativeBussiness += tempJournal.Debit;
                        continue;
                    }
                    //resultTransaction.GeneralJournalList.Add(tempJournal);
                    tmpGeneralJournalList.Add(tempJournal);
                    Account tmpAccount = session.GetObjectByKey<Account>(tempJournal.AccountId);
                    bool flgIsLeafAccount = tmpAccount.Accounts == null || tmpAccount.Accounts.Count == 0 ? true : false;

                    if (flgIsLeafAccount && accountingBO.IsRelateAccount(session, account.AccountId, tempJournal.AccountId))
                    {
                        Acceptable = true;
                    }
                }
                if (!Acceptable) 
                    return null;

                ETL_GeneralJournal masterJournal = tmpGeneralJournalList.
                    Where(j => j.Debit == transaction.Amount || j.Credit == transaction.Amount).FirstOrDefault();

                if (masterJournal == null)
                    return null;

                if (masterJournal.Credit == sumDebit_NotRelativeBussiness && sumDebit_NotRelativeBussiness > 0)
                    return null;

                else if (masterJournal.Credit > 0)
                {
                    masterJournal.Credit -= sumDebit_NotRelativeBussiness;                    
                }

                resultTransaction.GeneralJournalList = tmpGeneralJournalList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            resultTransaction.AccountCode = AccountCode;
            return resultTransaction;
        }

        public abstract void TransformTransaction(Session session);

        protected ETL_FinanciaActualPriceTransformData TransformTransactionFinancialActualPriceDetail
        (
            Session session,
            ETL_ActualPriceTransaction transaction, 
            string AccountCode)
        {
            Util util = new Util();
            ETL_FinanciaActualPriceTransformData result = new ETL_FinanciaActualPriceTransformData();
            Account account = util.GetXpoObjectByFieldName<Account, string>(session, "Code", AccountCode, BinaryOperatorType.Equal);
            if (account == null) return null;
            if (transaction == null) return null;
            ETL_Transaction etlTransaction = transaction;

            string subMainAccount = string.Empty;

            List<ETL_FinanciaActualPriceDetail> detail = new List<ETL_FinanciaActualPriceDetail>();
            try
            {
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                FinancialLiabilityBO liabilityBO = new FinancialLiabilityBO();
                //List<ETL_GeneralJournal> JournalListJoined = JoinJournal(session, etlTransaction.GeneralJournalList, AccountCode);
                List<ETL_GeneralJournal> FinishJournalList = liabilityBO.ClearJournalList(session, etlTransaction.GeneralJournalList, account.AccountId);
                foreach (ETL_GeneralJournal journal in FinishJournalList)
                {
                    ETL_FinanciaActualPriceDetail temp = new ETL_FinanciaActualPriceDetail();
                    temp.AccountCode = "";
                    temp.CorrespondAccountCode = "";
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
                result.AccountCode = subMainAccount;
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }

        public abstract void LoadTransaction(Session session);

        protected void LoadFinancialActualPriceDetail(Session session, ETL_FinanciaActualPriceTransformData data)
        {
            try
            {
                foreach (ETL_FinanciaActualPriceDetail detail in data.ETL_DetailList)
                {
                    CreateFinancialActualPriceDetail(session, detail, data.AccountCode);
                }
            }
            catch (Exception)
            {
                return;
            }   
        }

        private void CreateFinancialActualPriceDetail(
            Session session,
            ETL_FinanciaActualPriceDetail Detail,
            string MainAccountCode)
        {
            try
            {
                Util util = new Util();
                CorrespondFinancialAccountDim defaultCorrespondindAcc = CorrespondFinancialAccountDim.GetDefault(session, CorrespondFinancialAccountDimEnum.NAAN_DEFAULT);
                FinancialAccountDim defaultFinancialAcc = FinancialAccountDim.GetDefault(session, FinancialAccountDimEnum.NAAN_DEFAULT);
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                FinancialActualPriceSummary_Fact Fact = GetFinancialActualPriceSummaryFact(session, Detail.OwnerOrgId, Detail.IssueDate, MainAccountCode);
                FinancialActualPriceDetail newDetail = new FinancialActualPriceDetail(session);
                if (Fact == null)
                {
                    Fact = CreateFinancialActualPriceSummaryFact(session, Detail.OwnerOrgId, Detail.IssueDate, MainAccountCode, Detail.IsBalanceForward);
                    if (Fact == null) return;
                }
                else {
                    var date = new DateTime(Detail.IssueDate.Year, Detail.IssueDate.Month, 1);
                    FinancialActualPriceSummary_Fact previousSummary = GetFinancialActualPriceSummaryFact(session,
                        Detail.OwnerOrgId, date.AddMonths(-1), MainAccountCode);

                    if (previousSummary != null)
                    {
                        Fact.BeginDebitBalance = previousSummary.EndDebitBalance;
                        Fact.BeginCreditBalance = previousSummary.EndCreditBalance;
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
                newDetail.FinancialTransactionDimId = financialTransactionDim;
                newDetail.FinancialActualPriceSummary_FactId = Fact;

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
                    Fact.CreditSum = Fact.FinancialActualPriceDetails.Where(i => i.RowStatus == 1
                        && i.Credit > 0 && i.CorrespondFinancialAccountDimId == defaultCorrespondindAcc).Sum(d => d.Credit);

                    Fact.DebitSum = Fact.FinancialActualPriceDetails.Where(i => i.RowStatus == 1
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

        private FinancialActualPriceSummary_Fact GetFinancialActualPriceSummaryFact(
                                                                    Session session,
                                                                    Guid OwnerOrgId,
                                                                    DateTime IssueDate,
                                                                    string FinancialAccountCode)
        {
            FinancialActualPriceSummary_Fact result = null;
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

                    FinancialActualPriceSummary_Fact fact = session.FindObject<FinancialActualPriceSummary_Fact>(criteria);

                    if (fact == null) 
                        return null;
                    result = fact;
                }
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }

        private FinancialActualPriceSummary_Fact CreateFinancialActualPriceSummaryFact(Session session,
                                                                    Guid OwnerOrgId,
                                                                    DateTime IssueDate,
                                                                    string FinancialAccountCode,
                                                                    bool IsBalanceForward)
        {
            FinancialActualPriceSummary_Fact result = new FinancialActualPriceSummary_Fact(session);
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
                result.CoefficientDiff = 0;
                result.FinancialAccountDimId = accountingBO.GetFinancialAccountDim(session, FinancialAccountCode);
                result.MonthDimId = dimBO.GetMonthDim(session, (short)IssueDate.Month);
                result.YearDimId = dimBO.GetYearDim(session, (short)IssueDate.Year);
                result.OwnerOrgDimId = dimBO.GetOwnerOrgDim(session, OwnerOrgId);
                result.RowStatus = Constant.ROWSTATUS_ACTIVE;
                if (result.FinancialAccountDimId == null || result.MonthDimId == null || result.YearDimId == null || result.OwnerOrgDimId == null)
                {
                    return null;
                }

                var date = new DateTime(IssueDate.Year, IssueDate.Month, 1);
                FinancialActualPriceSummary_Fact previousSummary = GetFinancialActualPriceSummaryFact(session,
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
