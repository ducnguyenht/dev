using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.BO.ETL.Accounting.Interface;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.AccountChart;
using Utility;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.BI.Accounting.Finance.PrepaidExpense;
using NAS.DAL.Nomenclature.Organization;
using NAS.BO.ETL.Accounting.TempData;
using NAS.BO.Accounting;
using NAS.DAL;
using NAS.BO.ETL.Accounting.FinancialLiability;
using NAS.DAL.Accounting.Currency;
using NAS.BO.ETL.Accounting.GoodsInInventory.TempData;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Inventory.Command;
using NAS.DAL.BI.Inventory;

namespace NAS.BO.ETL.Accounting.GoodsInInventory
{
    public abstract class GoodsInInventoryBase : IELTLogicJob
    {
        private ETLAccountingBO accountingBO = new ETLAccountingBO();

        protected ETL_GoodsInInventoryTransaction fFinancialTransactionData;

        public ETL_GoodsInInventoryTransaction FinancialTransactionData
        {
            get { return fFinancialTransactionData; }
        }

        protected ETL_GoodsInInventoryTransformData fFinancialTransformData;

        public ETL_GoodsInInventoryTransformData FinancialTransformData
        {
            get { return fFinancialTransformData; }
        }

         public GoodsInInventoryBase()
        {
            this.fIsRelatedStrategy = false;
            this.fAccountCode = string.Empty;
            this.fTransactionId = Guid.Empty;
            this.fFinancialTransactionData = new ETL_GoodsInInventoryTransaction();
            this.fFinancialTransformData = new ETL_GoodsInInventoryTransformData();
        }

         public GoodsInInventoryBase(Guid transactionId, string accountCode)
        {
            this.fIsRelatedStrategy = false;
            this.fAccountCode = accountCode;
            this.fTransactionId = transactionId;
            this.fFinancialTransactionData = new ETL_GoodsInInventoryTransaction();
            this.fFinancialTransformData = new ETL_GoodsInInventoryTransformData();
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

        public abstract void FixInvokedBussinessObjects(Session session, XPCollection<DAL.System.Log.BusinessObject> invokedBussinessObjects);
              
        #endregion

        public ETL_GoodsInInventoryTransaction ExtractTransaction(Session session, Guid TransactionId, string AccountCode)
        {
            ETL_GoodsInInventoryTransaction resultTransaction = null;
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

                resultTransaction = new ETL_GoodsInInventoryTransaction();
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

                double numOfItem = 0;
                InventoryCommand command = null;
                if (transaction != null)
                {
                    InventoryJournal inventoryJournal = null;
                    try
                    {
                        inventoryJournal = transaction.InventoryJournalFinancials.FirstOrDefault().InventoryJournalId;
                        numOfItem = inventoryJournal.Debit > 0 ? inventoryJournal.Debit : inventoryJournal.Credit;
                        command = (inventoryJournal.InventoryTransactionId as InventoryCommandItemTransaction).InventoryCommandId;
                    }
                    catch (Exception)
                    {
                        numOfItem = 0;
                        command = null;
                    }
                }

                resultTransaction.Quantity = numOfItem;
                resultTransaction.ArtifactId = command == null ? Guid.Empty : command.InventoryCommandId;

                if (numOfItem != 0)
                    resultTransaction.Price = (decimal)resultTransaction.Amount / (decimal)numOfItem;
                else
                    resultTransaction.Price = 0;

                foreach (GeneralJournal journal
                    in
                    transaction.GeneralJournals.Where(i => i.RowStatus == Constant.ROWSTATUS_BOOKED_ENTRY || i.RowStatus == Constant.ROWSTATUS_ACTIVE))
                {
                    if ((journal.Debit + journal.Credit) == 0)
                        continue;

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

        protected ETL_GoodsInInventoryTransformData TransformTransaction
        (
            Session session,
            ETL_GoodsInInventoryTransaction transaction,
            string AccountCode)
        {
            Util util = new Util();
            ETL_GoodsInInventoryTransformData result = new ETL_GoodsInInventoryTransformData();
            Account account = util.GetXpoObjectByFieldName<Account, string>(session, "Code", AccountCode, BinaryOperatorType.Equal);
            if (account == null) return null;
            if (transaction == null) return null;
            ETL_GoodsInInventoryTransaction etlTransaction = transaction;

            string subMainAccount = string.Empty;

            List<ETL_GoodsInInventoryDetail> detail = new List<ETL_GoodsInInventoryDetail>();
            try
            {
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                FinancialLiabilityBO liabilityBO = new FinancialLiabilityBO();

                List<ETL_GeneralJournal> JournalListJoined = 
                    JoinJournal(session, etlTransaction.GeneralJournalList);
                List<ETL_GeneralJournal> FinishJournalList = 
                    liabilityBO.ClearJournalList(session, JournalListJoined, account.AccountId);

                foreach (ETL_GeneralJournal journal in etlTransaction.GeneralJournalList)
                {
                    ETL_GoodsInInventoryDetail temp = new ETL_GoodsInInventoryDetail();
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
                    temp.ArtifactId = transaction.ArtifactId;

                    if (etlTransaction.Price > 0)
                        temp.Quantity = 
                            temp.Debit == 0
                            ? (double)temp.Credit / (double)etlTransaction.Price :
                              (double)temp.Debit / (double)etlTransaction.Price;
                    else
                        temp.Quantity = 0;
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

        protected void LoadTransaction(Session session, ETL_GoodsInInventoryTransformData transportData)
        { 
            
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
    }
}
