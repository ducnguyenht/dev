using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.BO.ETL.Accounting.Interface;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using Utility;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.BI.Accounting.Account;
using NAS.DAL.Nomenclature.Organization;
using NAS.BO.ETL.Accounting.TempData;
using NAS.BO.Accounting;
using NAS.DAL.Accounting.Currency;
using NAS.DAL;
using NAS.DAL.BI.Accounting;
using NAS.DAL.BI.Actor;
using NAS.DAL.System.ShareDim;
using NAS.BO.ETL.Accounting.FinancialItemInventory.TempData;
using NAS.DAL.BI.Accounting.ItemInventory;
using NAS.DAL.BI.Inventory;
using NAS.BO.Inventory.Ledger;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Inventory.Command;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Inventory.Ledger;
using NAS.BO.ETL.Inventory.TempData;
using NAS.BO.ETL.Accounting.FinancialLiability;
using NAS.DAL.BI.Item;

namespace NAS.BO.ETL.Accounting.FinancialItemInventory
{
    public abstract class FinancialItemInventoryBase : IELTLogicJob
    {
        public FinancialItemInventoryBase()
        {
            this.fIsRelatedStrategy = false;
            this.fAccountCode = string.Empty;
            this.fTransactionId = Guid.Empty;
            this.fExtractingData = new ETL_FinancialItemInventoryExtracting();
            this.fTransformData = new ETL_FinancialItemInventoryTransformData();
        }

        public FinancialItemInventoryBase(Guid transactionId, string accountCode)
        {
            this.fIsRelatedStrategy = false;
            this.fAccountCode = accountCode;
            this.fTransactionId = transactionId;
            this.fExtractingData = new ETL_FinancialItemInventoryExtracting();
            this.fTransformData = new ETL_FinancialItemInventoryTransformData();
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
            NAS.DAL.Accounting.Journal.Transaction transaction = session.GetObjectByKey<NAS.DAL.Accounting.Journal.Transaction>(this.TransactionId);
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
            #region init 
            if (invokedBussinessObjects == null || invokedBussinessObjects.Count == 0)
                return;

            CorrespondFinancialAccountDim defaultCorrespondindAcc = CorrespondFinancialAccountDim.GetDefault(session, CorrespondFinancialAccountDimEnum.NAAN_DEFAULT);
            // Financial criteria
            CriteriaOperator criteria_0 = CriteriaOperator.Parse("not(IsNull(FinancialTransactionDimId))");
            CriteriaOperator criteria_1 = new InOperator("FinancialTransactionDimId.RefId", invokedBussinessObjects.Select(i => i.RefId));
            CriteriaOperator criteria_2 = new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater);            
            CriteriaOperator financialCriteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1, criteria_2);
            // Inventory criteria
            CriteriaOperator criteria_3 = CriteriaOperator.Parse("not(IsNull(InventoryTransactionDimId))");
            CriteriaOperator criteria_4 = new InOperator("InventoryTransactionDimId.RefId", invokedBussinessObjects.Select(i => i.RefId));
            CriteriaOperator criteria_5 = new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater);
            CriteriaOperator inventoryCriteria = new GroupOperator(GroupOperatorType.And, criteria_3, criteria_4, criteria_5);
            #endregion

            #region Fix Financial Transaction
            List<ItemInventoryByArtifact> relevantArtifacts = new List<ItemInventoryByArtifact>();
            XPCollection<FinancialEntryDetail> financialDetailToBeFixList
                = new XPCollection<FinancialEntryDetail>(session, financialCriteria);
            if (financialDetailToBeFixList != null && financialDetailToBeFixList.Count > 0)
                foreach (FinancialEntryDetail detail in financialDetailToBeFixList)
                {
                    ItemInventoryByArtifact artifact = null;
                    artifact = detail.ItemInventoryByArtifactId;
                    detail.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    detail.Save();
                    relevantArtifacts.Add(artifact);
                }            
            #endregion

            #region Fix Inventory Transaction
            XPCollection<InventoryEntryDetail> neededInventoryToBeFixList
                = new XPCollection<InventoryEntryDetail>(session, inventoryCriteria);

            if (neededInventoryToBeFixList != null && neededInventoryToBeFixList.Count > 0)
                foreach (InventoryEntryDetail detail in neededInventoryToBeFixList)
                {
                    ItemInventoryByArtifact artifact = null;
                    artifact = detail.ItemInventoryByArtifactId;
                    detail.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    detail.Save();
                }
            #endregion

            #region Update ItemInventoryByArtifact/FinancialItemInventorySummary_Fact
            List<FinancialItemInventorySummary_Fact> relevantSummaries = new List<FinancialItemInventorySummary_Fact>();
            if (relevantArtifacts != null && relevantArtifacts.Count > 0)
                foreach (ItemInventoryByArtifact artifact in relevantArtifacts)
                {
                    foreach (InventoryEntryDetail detail in artifact.InventoryEntryDetails)
                    {
                        detail.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                        detail.Save();
                    }
                    //Update ItemInventoryByArtifact---Financial
                    IEnumerable<FinancialEntryDetail> financilaDetails = artifact.FinancialEntryDetails.Where(i => i.RowStatus >= 1);
                    IEnumerable<InventoryEntryDetail> inventoryDetails = artifact.InventoryEntryDetails.Where(i => i.RowStatus >= 1);

                    artifact.DebitSum = financilaDetails.Where(i =>i.Debit > 0).
                        Sum(r => r.Debit);
                    artifact.CreditSum = financilaDetails.Where(i =>i.Credit > 0).
                        Sum(r => r.Credit);                    
                    //Update ItemInventoryByArtifact---Inventory
                    artifact.DebitItemSum = inventoryDetails.Where(i =>i.Debit > 0).
                        Sum(r => r.Debit);
                    artifact.CreditItemSum = inventoryDetails.Where(i =>i.Credit > 0).
                        Sum(r => r.Credit);

                    artifact.Save();
                    relevantSummaries.Add(artifact.FinancialItemInventorySummary_FactId);
                }

            if (relevantSummaries != null && relevantSummaries.Count > 0)
                foreach (FinancialItemInventorySummary_Fact summary in relevantSummaries)
                {
                    /// Summary Financial Caculation
                    summary.CreditSum = summary.ItemInventoryByArtifacts.
                                Where(i => i.RowStatus >= 1 && i.CreditSum > 0 
                                    && i.CorrespondFinancialAccountDimId == defaultCorrespondindAcc).
                                Sum(r => r.CreditSum);

                    summary.DebitSum = summary.ItemInventoryByArtifacts.
                                Where(i => i.RowStatus >= 1 && i.DebitSum > 0
                                    && i.CorrespondFinancialAccountDimId == defaultCorrespondindAcc).
                                Sum(r => r.DebitSum);
                    summary.EndCreditBalance = summary.BeginCreditBalance + summary.CreditSum - summary.DebitSum;
                    summary.EndDebitBalance = summary.BeginDebitBalance + summary.DebitSum - summary.CreditSum;
                    summary.Save();

                    /// Summary Inventory Caculation
                    summary.CreditItemSum = summary.ItemInventoryByArtifacts.
                                Where(i => i.RowStatus >= 1 && i.CreditItemSum > 0
                                    && i.CorrespondFinancialAccountDimId == defaultCorrespondindAcc).
                                Sum(r => r.CreditItemSum);

                    summary.DebitItemSum = summary.ItemInventoryByArtifacts.
                                Where(i => i.RowStatus >= 1 && i.DebitItemSum > 0
                                    && i.CorrespondFinancialAccountDimId == defaultCorrespondindAcc).
                                Sum(r => r.DebitItemSum);

                    summary.EndBalanceItem = summary.BeginBalanceItem + summary.DebitItemSum - summary.CreditItemSum;
                    summary.Save();
                }

            if (relevantArtifacts != null && relevantArtifacts.Count > 0)
                foreach (ItemInventoryByArtifact artifact in relevantArtifacts.Where(i=>i.CorrespondFinancialAccountDimId == defaultCorrespondindAcc))
                {
                    FinancialItemInventorySummary_Fact summary = artifact.FinancialItemInventorySummary_FactId;
                    artifact.CurrentBalanceItem = summary.EndBalanceItem;
                    artifact.CurrentBalance = summary.EndBalanceItem * artifact.Price;
                    artifact.Save();
                    summary.Save();
                }
            #endregion
        }

        #endregion

        private ETLAccountingBO accountingBO = new ETLAccountingBO();

        protected ETL_FinancialItemInventoryExtracting fExtractingData;
        public ETL_FinancialItemInventoryExtracting ExtractingData
        {
            get { return fExtractingData; }
        }

        protected ETL_FinancialItemInventoryTransformData fTransformData;
        public ETL_FinancialItemInventoryTransformData TransformData
        {
            get { return fTransformData; }
        }

        protected ETL_FinancialItemInventoryExtracting ExtractTransaction(Session session, Guid TransactionId, string AccountCode)
        {
            COGSBO CogsBO = new COGSBO();
            CurrencyBO currencyBO = new CurrencyBO();
            ETL_FinancialItemInventoryExtracting resultExtracting = null;
            ETL_Transaction financialETL_Transaction = null;
            ETL_InventoryTransaction inventoryETL_Transaction = null;            
            try
            {
                #region Extracting Financial
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
                    return null;
                }

                financialETL_Transaction = new ETL_Transaction();
                if (currentDeployOrg != null)
                    financialETL_Transaction.OwnerOrgId = currentDeployOrg.OrganizationId;
                else
                    financialETL_Transaction.OwnerOrgId = defaultOrg.OrganizationId;

                financialETL_Transaction.TransactionId = transaction.TransactionId;
                financialETL_Transaction.Amount = transaction.Amount;
                financialETL_Transaction.Code = transaction.Code;
                financialETL_Transaction.CreateDate = transaction.CreateDate;
                financialETL_Transaction.Description = transaction.Description;
                financialETL_Transaction.IsBalanceForward = (transaction is BalanceForwardTransaction);
                financialETL_Transaction.IssuedDate = transaction.IssueDate;
                financialETL_Transaction.UpdateDate = transaction.UpdateDate;
                financialETL_Transaction.GeneralJournalList = new List<ETL_GeneralJournal>();

                ETL_GeneralJournal masterFinancialJournal = null;
                foreach (GeneralJournal journal
                    in
                    transaction.GeneralJournals.Where
                    (i => i.RowStatus == Constant.ROWSTATUS_BOOKED_ENTRY || i.RowStatus == Constant.ROWSTATUS_ACTIVE))
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
                    financialETL_Transaction.GeneralJournalList.Add(tempJournal);

                    Account tmpAccount = session.GetObjectByKey<Account>(tempJournal.AccountId);
                    bool flgIsLeafAccount = tmpAccount.Accounts == null || tmpAccount.Accounts.Count == 0 ? true : false;

                    if (flgIsLeafAccount &&
                        accountingBO.IsRelateAccount(session, account.AccountId, tempJournal.AccountId))
                    {
                        Acceptable = true;
                        masterFinancialJournal = new ETL_GeneralJournal();
                        masterFinancialJournal = tempJournal;
                    }
                }

                InventoryCommand command = null;
                Item item = null;
                Unit unit = null;
                NAS.DAL.Nomenclature.Inventory.Inventory inventory = null;
                NAS.DAL.Nomenclature.Inventory.Inventory correspondInventory = null;
                decimal quantity;
                decimal price;
                try
                {
                    command = 
                        (transaction.InventoryJournalFinancials[0].InventoryJournalId.InventoryTransactionId 
                        as InventoryCommandItemTransaction).InventoryCommandId;
                    item = transaction.InventoryJournalFinancials[0].InventoryJournalId.ItemUnitId.ItemId;
                    unit = transaction.InventoryJournalFinancials[0].InventoryJournalId.ItemUnitId.UnitId;
                    inventory = command.RelevantInventoryId != null? command.RelevantInventoryId : null;
                    correspondInventory = command.CorrespondInventoryId != null ? command.CorrespondInventoryId : null;
                    decimal debit  = (decimal)transaction.InventoryJournalFinancials[0].InventoryJournalId.Debit;
                    decimal credit = (decimal)transaction.InventoryJournalFinancials[0].InventoryJournalId.Credit;
                    quantity = debit + credit;

                    if (quantity != 0)
                        price = (decimal)transaction.Amount / quantity;
                    else
                        price = 0;

                    if (financialETL_Transaction.IsBalanceForward)
                    {
                        correspondInventory = null;
                        command = null;
                    }

                }
                catch (Exception)
                {
                    command = null;
                    item = null;
                    unit = null;
                    inventory = null;
                    correspondInventory = null;
                    quantity = 0;
                    price = 0;
                }

                #endregion

                if (!Acceptable)
                    return null;

                if (item == null || unit == null || inventory == null)
                    return null;

                #region Extracting Inventory
                InventoryTransaction invenTransaction = transaction.InventoryJournalFinancials[0].InventoryJournalId.InventoryTransactionId;
                if (invenTransaction != null)
                {
                    inventoryETL_Transaction = new ETL_InventoryTransaction();
                    if (invenTransaction.AccountingPeriodId != null)
                    {
                        inventoryETL_Transaction.AccountingPeriodId = invenTransaction.AccountingPeriodId.AccountingPeriodId;
                    }
                    inventoryETL_Transaction.Code = invenTransaction.Code;
                    inventoryETL_Transaction.CreateDate = invenTransaction.CreateDate;
                    inventoryETL_Transaction.Description = invenTransaction.Description;
                    inventoryETL_Transaction.InventoryTransactionId = invenTransaction.InventoryTransactionId;
                    inventoryETL_Transaction.IssueDate = invenTransaction.IssueDate;
                    inventoryETL_Transaction.InventoryJournalList = new List<ETL_InventoryJournal>();
                    Console.WriteLine();
                    foreach (InventoryJournal journal in invenTransaction.InventoryJournals)
                    {
                        if (journal.RowStatus >= 0)
                        {
                            ETL_InventoryJournal newJournal = new ETL_InventoryJournal();
                            if (journal.AccountId != null)
                            {
                                newJournal.AccountId = journal.AccountId.AccountId;
                            }
                            if (price > 0)
                            {
                                //newJournal.Credit = (double)masterFinancialJournal.Credit / (double)price;
                                //newJournal.Debit = (double)masterFinancialJournal.Debit / (double)price;
                                newJournal.Credit = journal.Credit;
                                newJournal.Debit = journal.Debit;
                            }

                            if (journal.InventoryId != null)
                            {
                                newJournal.InventoryId = journal.InventoryId.InventoryId;
                            }
                            newJournal.InventoryJournalId = journal.InventoryJournalId;
                            if (journal.InventoryTransactionId != null)
                            {
                                newJournal.InventoryTransactionId = journal.InventoryTransactionId.InventoryTransactionId;
                            }
                            if (journal.ItemUnitId != null)
                            {
                                newJournal.ItemUnitId = journal.ItemUnitId.ItemUnitId;
                            }
                            newJournal.JournalType = journal.JournalType;
                            if (journal.LotId != null)
                            {
                                newJournal.LotId = journal.LotId.LotId;
                            }
                            newJournal.Description = journal.Description;
                            newJournal.CreateDate = journal.CreateDate;

                            if (newJournal.JournalType == 'A' || journal.InventoryTransactionId is InventoryTransactionBalanceForward)
                            {
                                inventoryETL_Transaction.InventoryJournalList.Add(newJournal);
                            }
                        }
                    }
                }
                #endregion

                if (financialETL_Transaction != null && inventoryETL_Transaction != null)
                {
                    resultExtracting = new ETL_FinancialItemInventoryExtracting();
                    resultExtracting.FinancialTransaction = financialETL_Transaction;
                    resultExtracting.InventoryTransaction = inventoryETL_Transaction;
                    resultExtracting.AccountCode = AccountCode;
                    
                    resultExtracting.ItemId = item.ItemId;
                    resultExtracting.UnitId = unit.UnitId;
                    resultExtracting.Quantity = quantity;
                    resultExtracting.Price = price;
                    resultExtracting.InventoryId = inventory.InventoryId;
                    resultExtracting.ArtifactId = command != null?
                        command.InventoryCommandId : Guid.Empty;
                    resultExtracting.CorrespondInventoryId = correspondInventory != null ? 
                        correspondInventory.InventoryId : Guid.Empty;
                }
            }
            catch (Exception)
            {
                resultExtracting = null;
            }
            
            return resultExtracting;
        }

        protected ETL_FinancialItemInventoryTransformData TransformTransaction
        (
            Session session,
            ETL_FinancialItemInventoryExtracting transactionExtracting,
            string AccountCode)
        {
            ETL_FinancialItemInventoryTransformData rt = new ETL_FinancialItemInventoryTransformData();
            Util util = new Util();
            #region Transform Financial            
            List<ETL_FinancialItemInventoryDetail> financialDetail = new List<ETL_FinancialItemInventoryDetail>();
            List<ETL_InventoryLedger> inventoryLedgerDetail = new List<ETL_InventoryLedger>();
            Account account = util.GetXpoObjectByFieldName<Account, string>(session, "Code", AccountCode, BinaryOperatorType.Equal);
            if (account == null) return null;
            if (transactionExtracting.FinancialTransaction == null) 
                return null;
            ETL_Transaction etlFinancialTransaction = transactionExtracting.FinancialTransaction;
            ETL_InventoryTransaction etlInventoryTransaction = transactionExtracting.InventoryTransaction;

            string subMainAccount = string.Empty;
            try
            {
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                FinancialLiabilityBO liabilityBO = new FinancialLiabilityBO();

                List<ETL_GeneralJournal> JournalListJoined =
                    JoinJournal(session, etlFinancialTransaction.GeneralJournalList);
                List<ETL_GeneralJournal> FinishJournalList =
                    liabilityBO.ClearJournalList(session, JournalListJoined, account.AccountId);

                foreach (ETL_GeneralJournal journal in etlFinancialTransaction.GeneralJournalList)
                {
                    ETL_FinancialItemInventoryDetail temp = new ETL_FinancialItemInventoryDetail();
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
                    temp.IsBalanceForward = etlFinancialTransaction.IsBalanceForward;
                    temp.IssueDate = etlFinancialTransaction.IssuedDate;
                    temp.OwnerOrgId = etlFinancialTransaction.OwnerOrgId;
                    temp.TransactionId = etlFinancialTransaction.TransactionId;
                    temp.Credit = (decimal)journal.Credit;
                    temp.Debit = (decimal)journal.Debit;
                    financialDetail.Add(temp);
                }
            }
            catch (Exception)
            {
                financialDetail = null;
            }
            #endregion

            #region Transform Inventory
            try
            {
                foreach (ETL_InventoryJournal inventoryJournal in etlInventoryTransaction.InventoryJournalList)
                {
                    ETL_InventoryLedger temp = new ETL_InventoryLedger();
                    temp.CreateDate = inventoryJournal.CreateDate;
                    temp.AccountId = inventoryJournal.AccountId;
                    temp.Credit = inventoryJournal.Credit;
                    temp.Debit = inventoryJournal.Debit;
                    temp.Description = inventoryJournal.Description;
                    temp.InventoryId = inventoryJournal.InventoryId;
                    temp.InventoryTransactionId = inventoryJournal.InventoryTransactionId;
                    temp.IsOriginal = true;
                    temp.IssueDate = etlInventoryTransaction.IssueDate;
                    temp.ItemUnitId = inventoryJournal.ItemUnitId;
                    temp.LedgerType = inventoryJournal.JournalType;
                    temp.LotId = inventoryJournal.LotId;
                    inventoryLedgerDetail.Add(temp);
                }
            }
            catch (Exception)
            {
                inventoryLedgerDetail = null;
            }
            #endregion

            if (financialDetail != null && inventoryLedgerDetail != null)
            {
                rt.ETL_FinancialDetailList = financialDetail;
                rt.ETL_InventoryDetailList = inventoryLedgerDetail;
                rt.AccountCode = AccountCode;
                rt.ArtifactId = transactionExtracting.ArtifactId;
                rt.ItemId = transactionExtracting.ItemId;
                rt.UnitId = transactionExtracting.UnitId;
                rt.Price = transactionExtracting.Price;
                rt.Quantity = transactionExtracting.Quantity;
                rt.InventoryId = transactionExtracting.InventoryId;
                rt.CorrespondInventoryId = transactionExtracting.CorrespondInventoryId;
                financialDetail.Sort(
                delegate(ETL_FinancialItemInventoryDetail p1, ETL_FinancialItemInventoryDetail p2)
                {
                    return p1.CorrespondAccountCode.CompareTo(p2.CorrespondAccountCode);
                }
            );
            }
            return rt;
        }

        protected void LoadTransaction(Session session, ETL_FinancialItemInventoryTransformData data)
        {
            Guid tmpGuid = Guid.Empty;
            List<Guid> relevantItemInventoryArtifacts = new List<Guid>();

            CorrespondFinancialAccountDim defaultCorrespondindAcc = CorrespondFinancialAccountDim.GetDefault(session, CorrespondFinancialAccountDimEnum.NAAN_DEFAULT);
            foreach (ETL_FinancialItemInventoryDetail detail in data.ETL_FinancialDetailList)
            {
                CreateFinancialDetail(
                    session,
                    detail,
                    data.AccountCode,
                    data.InventoryId,
                    data.ItemId,
                    data.UnitId,
                    data.ArtifactId,
                    data.Price, out tmpGuid);
                relevantItemInventoryArtifacts.Add(tmpGuid);
            }

            foreach (Guid key in relevantItemInventoryArtifacts)
            {
                CreateInventoryDetail(
                    session,
                    data.ETL_InventoryDetailList,
                    data.InventoryId,
                    data.CorrespondInventoryId, key);
            }
            /// ItemInventoryByArtifact Caculation
            //ItemInventoryByArtifact masterArtifact = session.GetObjectByKey<ItemInventoryByArtifact>(masterItemIventoryByArtifact);
            //FinancialItemInventorySummary_Fact summary = masterArtifact.FinancialItemInventorySummary_FactId;
            //masterArtifact.CurrentBalanceItem = summary.EndBalanceItem;
            //masterArtifact.CurrentBalance = summary.EndBalanceItem * data.Price;

            //foreach (ItemInventoryByArtifact slaveArtifact in summary.
            //    ItemInventoryByArtifacts.Where(i => i != masterArtifact && i.Price > 0))
            //{
            //    slaveArtifact.CreditItemSum = slaveArtifact.CreditSum/slaveArtifact.Price;
            //    slaveArtifact.DebitItemSum = slaveArtifact.DebitSum/slaveArtifact.Price;
            //    slaveArtifact.CurrentBalanceItem = summary.EndBalanceItem;
            //    slaveArtifact.CurrentBalance = summary.EndBalanceItem * data.Price;
            //    slaveArtifact.Save();
            //}
            //masterArtifact.Save();
            //summary.Save();
        }

        public void CreateFinancialDetail(
            Session session,
            ETL_FinancialItemInventoryDetail Detail, 
            string AccountCode,
            Guid inventoryId,
            Guid itemId,
            Guid unitId,
            Guid inventoyCommandId,             
            decimal price, 
            out Guid ItemInventoryByArtifactId)
        {
            ItemInventoryByArtifactId = Guid.Empty;
            try
            {
                if (Detail == null ||
                    AccountCode.Equals(string.Empty) ||
                    Detail.OwnerOrgId.Equals(Guid.Empty) ||
                    Detail.IssueDate == null)
                    return;

                #region prepare Summary header data
                Util util = new Util();
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                FinancialItemInventorySummary_Fact summary =
                    GetFinancialItemInventorySummaryFact(
                        session,
                        Detail.OwnerOrgId,
                        inventoryId,
                        itemId,
                        unitId,
                        AccountCode, 
                        Detail.IssueDate
                        );

                if (summary == null)
                {
                    summary =
                        CreateFinancialItemInventorySummaryFact(
                            session,
                            Detail.OwnerOrgId,
                            Detail.IssueDate,
                            inventoryId,
                            itemId,
                            unitId,
                            AccountCode,
                            Detail.IsBalanceForward
                            );
                    if (summary == null) return;
                }

                var date = new DateTime(Detail.IssueDate.Year, Detail.IssueDate.Month, 1);
                FinancialItemInventorySummary_Fact previousSummary = GetFinancialItemInventorySummaryFact(session,
                        Detail.OwnerOrgId,
                        inventoryId,
                        itemId,
                        unitId,
                        AccountCode,
                        date.AddMonths(-1));

                if (previousSummary != null)
                {
                    summary.BeginCreditBalance = previousSummary.EndCreditBalance;
                    summary.BeginDebitBalance = previousSummary.EndDebitBalance;
                    summary.BeginBalanceItem = previousSummary.EndBalanceItem;
                }
                #endregion

                #region prepare ItemInventoryByArtifact data
                CorrespondFinancialAccountDim defaultCorrespondindAcc = CorrespondFinancialAccountDim.GetDefault(session, CorrespondFinancialAccountDimEnum.NAAN_DEFAULT);
                FinancialAccountDim defaultFinancialAcc = FinancialAccountDim.GetDefault(session, FinancialAccountDimEnum.NAAN_DEFAULT);
                FinancialAccountDim financialAccountDim = null;
                CorrespondFinancialAccountDim correspondAccountDim = null;                

                if (!AccountCode.Equals(string.Empty))
                    financialAccountDim = util.GetXpoObjectByFieldName<FinancialAccountDim, string>(
                        session,
                        "Code",
                        AccountCode,
                        BinaryOperatorType.Equal);

                if (!Detail.CorrespondAccountCode.Equals(string.Empty))
                    correspondAccountDim = util.GetXpoObjectByFieldName<CorrespondFinancialAccountDim, string>(
                        session,
                        "Code",
                        Detail.CorrespondAccountCode,
                        BinaryOperatorType.Equal);

                if (financialAccountDim == null && !AccountCode.Equals(string.Empty))
                {
                    financialAccountDim = accountingBO.CreateFinancialAccountDim(session, AccountCode);
                }
                
                if (financialAccountDim == null)
                    financialAccountDim = defaultFinancialAcc;  

                if (correspondAccountDim == null && !Detail.CorrespondAccountCode.Equals(string.Empty))
                {
                    correspondAccountDim = accountingBO.CreateCorrespondFinancialAccountDim(session, Detail.CorrespondAccountCode);
                }

                if (correspondAccountDim == null)
                    correspondAccountDim = defaultCorrespondindAcc;

                ItemInventoryByArtifact itemInventoryByArtifact =
                    GetItemInventoryByArtifact(
                        session, 
                        summary.FinancialItemInventorySummary_FactId, 
                        inventoyCommandId,
                        correspondAccountDim.Code);

                if (itemInventoryByArtifact == null)
                {
                    itemInventoryByArtifact = CreateItemInventoryByArtifact(
                        session,
                        summary.FinancialItemInventorySummary_FactId,
                        inventoyCommandId,
                        price,
                        correspondAccountDim.Code);

                    if (itemInventoryByArtifact == null)
                    {
                        return;
                    }
                }
                #endregion

                #region prepare Detail
                FinancialEntryDetail newDetail = new FinancialEntryDetail(session);
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
                newDetail.CorrespondFinancialAccountDimId = correspondAccountDim;
                newDetail.ItemInventoryByArtifactId = itemInventoryByArtifact;            
                newDetail.RowStatus = Constant.ROWSTATUS_ACTIVE;
                newDetail.Save();
                #endregion

                if (Detail.IsBalanceForward)
                {
                    itemInventoryByArtifact.CreditSum  = Detail.Credit;
                    itemInventoryByArtifact.DebitSum = Detail.Debit;
                    summary.BeginCreditBalance = summary.EndCreditBalance = Detail.Credit;
                    summary.BeginCreditBalance = summary.EndCreditBalance = Detail.Debit;
                    summary.CreditSum = 0;
                    summary.DebitSum = 0;
                }
                else
                {
                    itemInventoryByArtifact.DebitSum = itemInventoryByArtifact.FinancialEntryDetails.
                        Where(i => i.RowStatus >= 1 && i.Debit > 0).
                        Sum(r => r.Debit);

                    itemInventoryByArtifact.CreditSum = itemInventoryByArtifact.FinancialEntryDetails.
                        Where(i => i.RowStatus >= 1 && i.Credit > 0).
                        Sum(r => r.Credit);

                    /// Summary Financial Caculation   
                    summary.CreditSum = summary.ItemInventoryByArtifacts.
                            Where(i => i.RowStatus >= 1 && i.CreditSum > 0 
                                && i.CorrespondFinancialAccountDimId == defaultCorrespondindAcc).
                            Sum(r => r.CreditSum);

                    summary.DebitSum = summary.ItemInventoryByArtifacts.
                                Where(i => i.RowStatus >= 1 && i.DebitSum > 0
                                && i.CorrespondFinancialAccountDimId == defaultCorrespondindAcc).
                                Sum(r => r.DebitSum);
                    summary.EndCreditBalance = summary.BeginCreditBalance + summary.CreditSum - summary.DebitSum;
                    summary.EndDebitBalance = summary.BeginDebitBalance + summary.DebitSum - summary.CreditSum;
                }

                itemInventoryByArtifact.Save();                             
                summary.Save();

                ItemInventoryByArtifactId = itemInventoryByArtifact.ItemInventoryByArtifactId;
            }
            catch (Exception)
            {
                return;
            }
        }

        public void CreateInventoryDetail(
            Session session, 
            List<ETL_InventoryLedger> ledgerList, 
            Guid inventoryId, 
            Guid correspondInventoryId, 
            Guid itemInventoryByArtifactId)
        {
            try
            {
                CorrespondFinancialAccountDim defaultCorrespondindAcc = CorrespondFinancialAccountDim.GetDefault(session, CorrespondFinancialAccountDimEnum.NAAN_DEFAULT);
                DimBO dimBO = new DimBO();
                if (ledgerList == null) 
                    return;

                if (ledgerList.Count == 0) 
                    return;

                if (ledgerList.Any() == false) 
                    return;

                InventoryTransaction invTransaction = session.GetObjectByKey<InventoryTransaction>((ledgerList.FirstOrDefault()).InventoryTransactionId);
                if (invTransaction == null) 
                    return;

                ItemInventoryByArtifact itemInventoryByArtifact = session.GetObjectByKey<ItemInventoryByArtifact>(itemInventoryByArtifactId);
                if (itemInventoryByArtifact == null)
                    return;

                FinancialItemInventorySummary_Fact summary = itemInventoryByArtifact.FinancialItemInventorySummary_FactId;

                if (itemInventoryByArtifact.CorrespondFinancialAccountDimId == defaultCorrespondindAcc)
                {
                    foreach (ETL_InventoryLedger ledger in ledgerList)
                    {
                        InventoryEntryDetail detail = new InventoryEntryDetail(session);
                        detail.Credit = (decimal)ledger.Credit;
                        detail.Debit = (decimal)ledger.Debit;
                        detail.CorrespondInventoryDimId = dimBO.GetCorrespondInventoryDim(session, correspondInventoryId);
                        detail.InventoryDimId = dimBO.GetInventoryDim(session, inventoryId);
                        detail.InventoryTransactionDimId = dimBO.GetInventoryTransactionDim(session, invTransaction.InventoryTransactionId);
                        detail.ItemInventoryByArtifactId = itemInventoryByArtifact;
                        detail.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                        detail.Save();

                        if (invTransaction is InventoryTransactionBalanceForward)
                        {
                            itemInventoryByArtifact.CreditItemSum = (decimal)ledger.Credit;
                            itemInventoryByArtifact.DebitItemSum = (decimal)ledger.Debit;
                            summary.BeginBalanceItem = (decimal)ledger.Debit;
                            summary.CreditItemSum = 0;
                            summary.DebitItemSum = 0;
                        }
                        else
                        {
                            if (itemInventoryByArtifact.InventoryCommandDimId.CommandType.Equals('O'))
                                itemInventoryByArtifact.CreditItemSum = itemInventoryByArtifact.InventoryEntryDetails.
                                    Where(i => i.RowStatus >= 1 && i.Credit > 0).
                                    Sum(r => r.Credit);
                            else if (itemInventoryByArtifact.InventoryCommandDimId.CommandType.Equals('I'))
                                itemInventoryByArtifact.DebitItemSum = itemInventoryByArtifact.InventoryEntryDetails.
                                    Where(i => i.RowStatus >= 1 && i.Debit > 0).
                                    Sum(r => r.Debit);
                        }
                    }
                    itemInventoryByArtifact.Save();

                    /// Summary Inventory Caculation    
                    if (!(invTransaction is InventoryTransactionBalanceForward))
                    {
                        summary.CreditItemSum = summary.ItemInventoryByArtifacts.
                                    Where(i => i.RowStatus >= 1 && i.CreditItemSum > 0
                                        && i.CorrespondFinancialAccountDimId == defaultCorrespondindAcc).
                                    Sum(r => r.CreditItemSum);

                        summary.DebitItemSum = summary.ItemInventoryByArtifacts.
                                    Where(i => i.RowStatus >= 1 && i.DebitItemSum > 0
                                    && i.CorrespondFinancialAccountDimId == defaultCorrespondindAcc).
                                    Sum(r => r.DebitItemSum);
                    }
                    summary.EndBalanceItem = summary.BeginBalanceItem + summary.DebitItemSum - summary.CreditItemSum;
                    summary.Save();
                }
                else if (itemInventoryByArtifact.Price > 0)
                {
                    itemInventoryByArtifact.CreditItemSum = itemInventoryByArtifact.CreditSum / itemInventoryByArtifact.Price;
                    itemInventoryByArtifact.DebitItemSum = itemInventoryByArtifact.DebitSum / itemInventoryByArtifact.Price;
                }

                itemInventoryByArtifact.CurrentBalanceItem = summary.EndBalanceItem;
                itemInventoryByArtifact.CurrentBalance = summary.EndDebitBalance;
                itemInventoryByArtifact.Save();
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

        private FinancialItemInventorySummary_Fact GetFinancialItemInventorySummaryFact(
            Session session,
            Guid OwnerOrgId,
            Guid InventoryId,
            Guid ItemId,
            Guid UnitId,
            string FinancialAccountCode,
            DateTime IssueDate
        )
        {
            FinancialItemInventorySummary_Fact result = null;
            try
            {
                Util util = new Util();
                OwnerOrgDim ownerOrgDim = util.GetXpoObjectByFieldName<OwnerOrgDim, Guid>(session, "RefId", OwnerOrgId, BinaryOperatorType.Equal);
                MonthDim monthDim = util.GetXpoObjectByFieldName<MonthDim, string>(session, "Name", IssueDate.Month.ToString(), BinaryOperatorType.Equal);
                YearDim yearDim = util.GetXpoObjectByFieldName<YearDim, string>(session, "Name", IssueDate.Year.ToString(), BinaryOperatorType.Equal);
                ItemDim itemDim = util.GetXpoObjectByFieldName<ItemDim, Guid>(session, "RefId", ItemId, BinaryOperatorType.Equal);
                UnitDim unitDim = util.GetXpoObjectByFieldName<UnitDim, Guid>(session, "RefId", UnitId, BinaryOperatorType.Equal);
                InventoryDim inventoryDim = util.GetXpoObjectByFieldName<InventoryDim, Guid>(session, "RefId", InventoryId, BinaryOperatorType.Equal);
                FinancialAccountDim financialAccountDim = util.GetXpoObjectByFieldName<FinancialAccountDim, string>(session, "Code", FinancialAccountCode, BinaryOperatorType.Equal);

                if (ownerOrgDim == null || monthDim == null || yearDim == null || itemDim == null || unitDim == null || inventoryDim == null || financialAccountDim == null)
                    return null;
                else
                {
                    CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                    CriteriaOperator criteria_OwnerOrg = new BinaryOperator("OwnerOrgDimId", ownerOrgDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_Month = new BinaryOperator("MonthDimId", monthDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_Year = new BinaryOperator("YearDimId", yearDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_item = new BinaryOperator("ItemDimId", itemDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_unit = new BinaryOperator("UnitDimId", unitDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_inventory = new BinaryOperator("InventoryDimId", inventoryDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria_Account = new BinaryOperator("FinancialAccountDimId", financialAccountDim, BinaryOperatorType.Equal);
                    CriteriaOperator criteria = CriteriaOperator.And(
                        criteria_RowStatus, 
                        criteria_OwnerOrg, 
                        criteria_Month,
                        criteria_Year, 
                        criteria_item, 
                        criteria_unit, 
                        criteria_inventory, 
                        criteria_Account);

                    FinancialItemInventorySummary_Fact fact = session.FindObject<FinancialItemInventorySummary_Fact>(criteria);
                    
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

        private FinancialItemInventorySummary_Fact CreateFinancialItemInventorySummaryFact(
            Session session,
            Guid OwnerOrgId,
            DateTime IssueDate,
            Guid InventoryId,
            Guid ItemId,
            Guid UnitId,
            string FinancialAccountCode,
            bool IsBalanceForward
            )
        {
            FinancialItemInventorySummary_Fact result = new FinancialItemInventorySummary_Fact(session);
            try
            {
                ETLAccountingBO accountingBO = new ETLAccountingBO();
                DimBO dimBO = new DimBO();
                result.MonthDimId = dimBO.GetMonthDim(session, (short)IssueDate.Month);
                result.YearDimId = dimBO.GetYearDim(session, (short)IssueDate.Year);
                result.OwnerOrgDimId = dimBO.GetOwnerOrgDim(session, OwnerOrgId);
                result.InventoryDimId = dimBO.GetInventoryDim(session, InventoryId);
                result.ItemDimId = dimBO.GetItemDim(session, ItemId);
                result.UnitDimId = dimBO.GetUnitDim(session, UnitId);
                result.FinancialAccountDimId = accountingBO.GetFinancialAccountDim(session, FinancialAccountCode);
                result.RowStatus = Constant.ROWSTATUS_ACTIVE;
                if (result.MonthDimId == null || result.YearDimId == null || result.OwnerOrgDimId == null ||
                    result.InventoryDimId == null || result.ItemDimId == null || result.UnitDimId == null || result.FinancialAccountDimId == null)
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

        private ItemInventoryByArtifact GetItemInventoryByArtifact(
            Session session, 
            int summaryFactId, 
            Guid inventoryCommandId,
            string CorrespondAccountCode)
        {
            try
            {
                Util util = new Util();
                DimBO dimBO = new DimBO();
                FinancialItemInventorySummary_Fact summary = session.GetObjectByKey<FinancialItemInventorySummary_Fact>(summaryFactId);
                InventoryCommandDim commandDim = util.GetXpoObjectByFieldName<InventoryCommandDim, Guid>(session, "RefId", inventoryCommandId, BinaryOperatorType.Equal);
                if (summary == null || commandDim == null)
                    return null;

                CriteriaOperator criteria_0 = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_1 = new BinaryOperator("FinancialItemInventorySummary_FactId", summary, BinaryOperatorType.Equal);
                CriteriaOperator criteria_2 = new BinaryOperator("InventoryCommandDimId", commandDim, BinaryOperatorType.Equal);
                CriteriaOperator criteria_3 = new BinaryOperator("CorrespondFinancialAccountDimId.Code", CorrespondAccountCode, BinaryOperatorType.Equal);
                CriteriaOperator criteria = CriteriaOperator.And(criteria_0, criteria_1, criteria_2, criteria_3);
                return session.FindObject<ItemInventoryByArtifact>(criteria);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private ItemInventoryByArtifact CreateItemInventoryByArtifact(
            Session session, 
            int summaryFactId, 
            Guid inventoryCommandId,
            decimal price,
            string CorrespondAccountCode)
        {
            ItemInventoryByArtifact result = new ItemInventoryByArtifact(session);
            try
            {
                Util util = new Util();
                DimBO dimBO = new DimBO();
                CorrespondFinancialAccountDim correspondAccountDim = null;
                FinancialItemInventorySummary_Fact summary = session.GetObjectByKey<FinancialItemInventorySummary_Fact>(summaryFactId);

                if (summary == null)
                    return null;

                if (!CorrespondAccountCode.Equals(string.Empty))
                    correspondAccountDim = util.GetXpoObjectByFieldName<CorrespondFinancialAccountDim, string>(
                        session,
                        "Code",
                        CorrespondAccountCode,
                        BinaryOperatorType.Equal);

                if (correspondAccountDim == null && !CorrespondAccountCode.Equals(string.Empty))
                {
                    correspondAccountDim = accountingBO.CreateCorrespondFinancialAccountDim(session, CorrespondAccountCode);
                }

                if (correspondAccountDim == null)
                    return null;

                result.InventoryCommandDimId = dimBO.GetInventoryCommandDim(session, inventoryCommandId);
                //if (result.InventoryCommandDimId == null)
                //    return null;

                result.RowStatus = Constant.ROWSTATUS_ACTIVE;
                result.FinancialItemInventorySummary_FactId = summary;
                result.CorrespondFinancialAccountDimId = correspondAccountDim;
                result.Price = price;
                result.Save();
                return result;
            }
            catch (Exception)
            {
                return null;
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
    }
}
