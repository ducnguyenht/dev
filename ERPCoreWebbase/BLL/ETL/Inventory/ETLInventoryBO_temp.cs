using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.BO.ETL.Inventory.TempData;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Inventory.Ledger;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Accounting.Currency;
using Utility;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL.Inventory.Lot;

namespace NAS.BO.ETL.Inventory
{
    public class ETLInventoryBO_temp
    {
        public static bool IsExistCOGS(Session session, ItemUnit itemUnit, CurrencyType currencyType)
        {
            bool result = false;
            try
            {
                CriteriaOperator criteria_0 = new BinaryOperator("ItemUnitId", itemUnit, BinaryOperatorType.Equal);
                CriteriaOperator criteria_1 = new BinaryOperator("CurrencyId.CurrencyTypeId.CurrencyTypeId", currencyType.CurrencyTypeId, BinaryOperatorType.Equal);
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1);
                COGS cogs = session.FindObject<COGS>(criteria);
                if (cogs == null)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }

        public ETL_InventoryTransaction ExtractInventoryTransaction(Session session, Guid InventoryTransactionId)
        {
            ETL_InventoryTransaction result = null;
            try
            {
                InventoryTransaction transaction = session.GetObjectByKey<InventoryTransaction>(InventoryTransactionId);
                if (transaction != null)
                {
                    result = new ETL_InventoryTransaction();
                    if (transaction.AccountingPeriodId != null)
                    {
                        result.AccountingPeriodId = transaction.AccountingPeriodId.AccountingPeriodId;
                    }
                    result.Code = transaction.Code;
                    result.CreateDate = transaction.CreateDate;
                    result.Description = transaction.Description;
                    result.InventoryTransactionId = transaction.InventoryTransactionId;
                    result.IssueDate = transaction.IssueDate;
                    result.InventoryJournalList = new List<ETL_InventoryJournal>();
                    result.COGSList = new List<ETL_COGS>();
                    foreach (InventoryJournal journal in transaction.InventoryJournals)
                    {
                        if (journal.RowStatus >= 0)
                        {
                            ETL_InventoryJournal newJournal = new ETL_InventoryJournal();
                            if (journal.AccountId != null)
                            {
                                newJournal.AccountId = journal.AccountId.AccountId;
                            }
                            newJournal.Credit = journal.Credit;
                            newJournal.Debit = journal.Debit;
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
                            result.InventoryJournalList.Add(newJournal);
                        }
                    }

                    foreach (COGS cogs in transaction.COGSs)
                    {
                        ETL_COGS newCOGS = new ETL_COGS();
                        newCOGS.Amount = cogs.Amount;
                        newCOGS.Assumption = cogs.Assumption;
                        newCOGS.Balance = cogs.Balance;
                        newCOGS.COGSPrice = cogs.COGSPrice;
                        newCOGS.COGSId = cogs.COGSId;
                        newCOGS.Credit = cogs.Credit;
                        newCOGS.Debit = cogs.Debit;
                        newCOGS.Description = cogs.Description;
                        newCOGS.CreateDate = cogs.CreateDate;
                        newCOGS.IssueDate = cogs.IssueDate;
                        newCOGS.UpdateDate = cogs.UpdateDate;
                        if (cogs.InventoryId != null)
                        {
                            newCOGS.InventoryId = cogs.InventoryId.InventoryId;
                        }
                        if (cogs.InventoryTransactionId != null)
                        {
                            newCOGS.InventoryTransactionId = cogs.InventoryTransactionId.InventoryTransactionId;
                        }
                        if (cogs.ItemUnitId != null)
                        {
                            newCOGS.ItemUnitId = cogs.ItemUnitId.ItemUnitId;
                        }
                        if (cogs.CurrencyId != null)
                        {
                            newCOGS.CurrencyId = cogs.CurrencyId.CurrencyId;
                        }
                        newCOGS.Price = cogs.Price;
                        newCOGS.Total = cogs.Total;
                        result.COGSList.Add(newCOGS);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        #region COGS
        public COGS GetPreviousCOGS(Session session, Guid COGSId)
        {
            COGS result = null;
            try
            {
                COGS currentCOGS = session.GetObjectByKey<COGS>(COGSId);
                if (currentCOGS == null) return null;
                XPQuery<COGS> COGSQuery = new XPQuery<COGS>(session);
                IQueryable<COGS> COGSCol = (from c in COGSQuery
                                            where c.ItemUnitId == currentCOGS.ItemUnitId
                                            && c.InventoryTransactionId.RowStatus >= 1
                                            && c.CurrencyId.CurrencyTypeId == currentCOGS.CurrencyId.CurrencyTypeId
                                            && c.IssueDate == currentCOGS.IssueDate
                                            && c.CreateDate < currentCOGS.CreateDate
                                            select c);
                if (COGSCol == null || COGSCol.Count() == 0)
                {
                    COGSCol = (from c in COGSQuery
                               where c.ItemUnitId == currentCOGS.ItemUnitId
                               && c.InventoryTransactionId.RowStatus >= 1
                               && c.CurrencyId.CurrencyTypeId == currentCOGS.CurrencyId.CurrencyTypeId
                               && c.IssueDate < currentCOGS.IssueDate
                               select c);
                    if (COGSCol == null || COGSCol.Count() == 0)
                    {
                        return null;
                    }
                    else
                    {
                        result = COGSCol.OrderByDescending(c => c.IssueDate).FirstOrDefault();
                    }
                }
                else
                {
                    result = COGSCol.OrderByDescending(c => c.CreateDate).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        public void RepairCOGSByTransaction(Session session, Guid _TransactionId)
        {
            try
            {
                InventoryTransaction transaction = session.GetObjectByKey<InventoryTransaction>(_TransactionId);
                if (transaction == null) return;
                foreach (COGS cogs in transaction.COGSs)
                {
                    RepairCOGS(session, cogs.COGSId);
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        public void RepairCOGS(Session session, Guid COGSId)
        {
            COGS currentCOGS = session.GetObjectByKey<COGS>(COGSId);
            if (currentCOGS == null) return;
            if (currentCOGS.InventoryTransactionId is InventoryTransactionBalanceForward)
            {
                currentCOGS.Amount = 0;
                currentCOGS.Total = currentCOGS.Balance * currentCOGS.Price;
                currentCOGS.COGSPrice = currentCOGS.Price;
                currentCOGS.Save();
                return;
            }
            COGS previousCOGS = GetPreviousCOGS(session, COGSId);
            if (previousCOGS == null)
            {
                
                if (currentCOGS.Debit == 0 && currentCOGS.Credit != 0)
                {
                    currentCOGS.Balance = currentCOGS.Debit - currentCOGS.Credit;
                    currentCOGS.Save();
                    return;
                }
                else if (currentCOGS.Debit != 0 && currentCOGS.Credit == 0)
                {
                    currentCOGS.Balance = currentCOGS.Debit - currentCOGS.Credit;
                    currentCOGS.Amount = currentCOGS.Price * currentCOGS.Debit;
                    currentCOGS.Total = currentCOGS.Balance * currentCOGS.Price;
                    currentCOGS.COGSPrice = currentCOGS.Price;
                    currentCOGS.Save();
                }
                else if (currentCOGS.Debit == 0 && currentCOGS.Credit == 0)
                {                    
                    currentCOGS.Amount = currentCOGS.Price * currentCOGS.Balance;
                    currentCOGS.Total = currentCOGS.Amount;
                    currentCOGS.COGSPrice = currentCOGS.Price;
                    currentCOGS.Save();
                }
            }
            else
            {
                currentCOGS.Balance = previousCOGS.Balance + currentCOGS.Debit - currentCOGS.Credit;
                if (currentCOGS.Debit == 0 && currentCOGS.Credit != 0)
                {
                    currentCOGS.Price = previousCOGS.COGSPrice;
                }
                currentCOGS.Amount = currentCOGS.Price * (currentCOGS.Debit - currentCOGS.Credit);
                currentCOGS.Total = previousCOGS.Total + currentCOGS.Amount;
                currentCOGS.COGSPrice = currentCOGS.Total / currentCOGS.Balance;
                currentCOGS.Save();
            }
        }
        public void CreateInitCOGS(Session session, ItemUnit itemUnit, CurrencyType currencyType)
        {
            if(IsExistCOGS(session, itemUnit, currencyType))
            {
                return;
            }
            COGS result = new COGS(session);
            try
            {
                CriteriaOperator criteria = new BinaryOperator("IsDefault",true,BinaryOperatorType.Equal);
                CriteriaOperator criteria_0 = new BinaryOperator("Code", "NAAN_DEFAULT", BinaryOperatorType.Equal);
                Currency currency = new XPCollection<Currency>(session, currencyType.Currencies, criteria).FirstOrDefault();
                if (currency != null)
                {
                    result.Amount = 0;
                    result.Assumption = 1;
                    result.Balance = 0;
                    result.COGSPrice = 0;
                    result.CreateDate = DateTime.Now;
                    result.Credit = 0;
                    result.CurrencyId = currency;
                    result.Debit = 0;
                    result.Description = "";
                    result.InventoryId = session.FindObject<NAS.DAL.Nomenclature.Inventory.Inventory>(criteria_0);
                    result.IsOriginal = true;
                    result.IssueDate = DateTime.Now;
                    result.ItemUnitId = itemUnit;
                    result.Price = 0;
                    result.Total = 0;
                    result.UpdateDate = DateTime.Now;
                    result.Save();
                }
            }
            catch (Exception)
            {
                return;// result;
            }
            return;// result;
        }
        public void PopulateCOGS(Session session)
        {
            CriteriaOperator criteria = new BinaryOperator("RowStatus",Utility.Constant.ROWSTATUS_ACTIVE,BinaryOperatorType.GreaterOrEqual);
            XPCollection<ItemUnit> ItemUnitCol = new XPCollection<ItemUnit>(session,criteria);
            XPCollection<CurrencyType> CurrencyTypeCol = new XPCollection<CurrencyType>(session, criteria);

            foreach (ItemUnit itemUnit in ItemUnitCol)
            {
                foreach (CurrencyType type in CurrencyTypeCol)
                {
                    CreateInitCOGS(session, itemUnit, type);
                }
            }
        }
        #endregion

        #region Ledger
        public InventoryLedger GetPreviousInventoryLedger(Session session, DateTime IssueDate, Guid InventoryId, Guid AccountId, Guid LotID, Guid ItemUnitId)
        {
            InventoryLedger result = null;
            try
            {
                NAS.DAL.Nomenclature.Inventory.Inventory inventory = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(InventoryId);
                Account account = session.GetObjectByKey<Account>(AccountId);
                Lot lot = session.GetObjectByKey<Lot>(LotID);
                ItemUnit itemUnit = session.GetObjectByKey<ItemUnit>(ItemUnitId);
                if (inventory == null || account == null || lot == null || itemUnit == null) return null;

                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_IssueDate = new BinaryOperator("IssueDate", IssueDate, BinaryOperatorType.LessOrEqual);
                CriteriaOperator criteria_Inventory = new BinaryOperator("InventoryId", inventory, BinaryOperatorType.Equal);
                CriteriaOperator criteria_Account = new BinaryOperator("AccountId", account, BinaryOperatorType.Equal);
                CriteriaOperator criteria_Lot = new BinaryOperator("LotId", lot, BinaryOperatorType.Equal);
                CriteriaOperator criteria_ItemUnit = new BinaryOperator("ItemUnitId", itemUnit, BinaryOperatorType.Equal);

                CriteriaOperator criteria = CriteriaOperator.And(criteria_IssueDate, criteria_RowStatus, criteria_Inventory, criteria_Account, criteria_Lot, criteria_ItemUnit);

                XPCollection<InventoryLedger> inventoryLedgerCol = new XPCollection<InventoryLedger>(session, criteria);
            }
            catch(Exception)
            {
                return result;
            }
            return result;

        }
        public List<ETL_InventoryLedger> TransformInventoryJournalToInventoryLedger(ETL_InventoryTransaction inventoryTransaction)
        {
            List<ETL_InventoryLedger> result = new List<ETL_InventoryLedger>();
            try
            {
                foreach(ETL_InventoryJournal journal in inventoryTransaction.InventoryJournalList){

                }
            }
            catch (Exception)
            {
            }
            return result;
        }

        public void LoadInventoryLedger(List<ETL_InventoryLedger> ledgerList)
        {
        }
        #endregion
    }
}
