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
using Utility.ETL;

namespace NAS.BO.ETL.Inventory
{
    public class ETLInventoryBO
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
                    int i = 1;
                    Console.WriteLine();
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

                            if (newJournal.JournalType == 'A' || journal.InventoryTransactionId is InventoryTransactionBalanceForward)
                            {
                                result.InventoryJournalList.Add(newJournal);
                            }
                        }
                    }
                    Console.WriteLine();

                    Console.WriteLine();
                    i = 1;
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
                    Console.WriteLine();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

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
        public InventoryLedger GetPreviousInventoryLedger(Session session, InventoryLedger ledger)
        {
            InventoryLedger result = null;
            try
            {
                //CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                //CriteriaOperator criteria_IssueDate = new BinaryOperator("IssueDate", ledger.IssueDate, BinaryOperatorType.Less);
                //CriteriaOperator criteria_IssueDate1 = new BinaryOperator("IssueDate", ledger.IssueDate, BinaryOperatorType.Equal);
                //CriteriaOperator criteria_CreateDate = new BinaryOperator("CreateDate", ledger.CreateDate, BinaryOperatorType.LessOrEqual);
                //CriteriaOperator criteria_Inventory = new BinaryOperator("InventoryId", ledger.InventoryId, BinaryOperatorType.Equal);
                //CriteriaOperator criteria_ItemUnit = new BinaryOperator("ItemUnitId", ledger.ItemUnitId, BinaryOperatorType.Equal);
                //CriteriaOperator criteria_InventoryLedgerId = new BinaryOperator("InventoryLedgerId!Key", ledger.InventoryLedgerId, BinaryOperatorType.NotEqual);

                //CriteriaOperator criteria_DateTime = CriteriaOperator.Or(   criteria_IssueDate,
                //                                                            CriteriaOperator.And(criteria_IssueDate1, criteria_CreateDate));

                //CriteriaOperator criteria = CriteriaOperator.And(criteria_DateTime, criteria_Inventory, criteria_InventoryLedgerId, criteria_RowStatus, criteria_ItemUnit);

                XPQuery<InventoryLedger> InventoryLedgerQuery = new XPQuery<InventoryLedger>(session);
                IQueryable<InventoryLedger> InventoryLedgerCol = (from r in InventoryLedgerQuery
                                                                  where r.RowStatus >= Constant.ROWSTATUS_ACTIVE
                                                                  && r.IssueDate == ledger.IssueDate
                                                                  && r.CreateDate <=ledger.CreateDate
                                                                  && r.InventoryId == ledger.InventoryId
                                                                  && r.InventoryLedgerId != ledger.InventoryLedgerId
                                                                  && r.ItemUnitId == ledger.ItemUnitId
                                                                  select r);
                if (InventoryLedgerCol != null)
                {
                    if (InventoryLedgerCol.Count() > 0)
                    {
                        result = InventoryLedgerCol.OrderByDescending(r => r.CreateDate).FirstOrDefault();
                        return result;
                    }
                }

                InventoryLedgerCol = (from r in InventoryLedgerQuery
                                      where r.RowStatus >= Constant.ROWSTATUS_ACTIVE
                                      && r.IssueDate < ledger.IssueDate                                      
                                      && r.InventoryId == ledger.InventoryId
                                      && r.InventoryLedgerId != ledger.InventoryLedgerId
                                      && r.ItemUnitId == ledger.ItemUnitId
                                      select r);
                
                if (InventoryLedgerCol != null)
                {
                    if (InventoryLedgerCol.Count() > 0)
                    {
                        result = InventoryLedgerCol.OrderByDescending(r => r.IssueDate).OrderByDescending(r => r.CreateDate).FirstOrDefault();
                        return result;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }
        public void RepairCOGSByTransaction(Session session, Guid _TransactionId)
        {
            try
            {
                InventoryTransaction transaction = session.GetObjectByKey<InventoryTransaction>(_TransactionId);
                ETLUtils etlUtil = new ETLUtils();
                if (transaction == null)
                {
                    etlUtil.logs("d:/logs/Process_history.txt", "TransactionId: " + _TransactionId.ToString() + "was not found");
                    return;
                }
                int i = 1;
              
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

        public List<ETL_InventoryLedger> TransformInventoryJournalToInventoryLedger(ETL_InventoryTransaction inventoryTransaction)
        {
            List<ETL_InventoryLedger> result = new List<ETL_InventoryLedger>();
            try
            {
                foreach (ETL_InventoryJournal inventoryJournal in inventoryTransaction.InventoryJournalList)
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
                    temp.IssueDate = inventoryTransaction.IssueDate;
                    temp.ItemUnitId = inventoryJournal.ItemUnitId;
                    temp.LedgerType = inventoryJournal.JournalType;
                    temp.LotId = inventoryJournal.LotId;
                    result.Add(temp);
                }
            }
            catch (Exception)
            {
            }
            return result;
        }

        public void 
            
            LoadInventoryLedger(Session session, List<ETL_InventoryLedger> ledgerList)
        {
            try
            {
                if(ledgerList == null) return;
                if(ledgerList.Count == 0) return;
                if(ledgerList.Any() == false) return;
                InventoryTransaction invTransaction = session.GetObjectByKey<InventoryTransaction>((ledgerList.FirstOrDefault()).InventoryTransactionId);
                if (invTransaction == null) return;

                CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_Inventory = new BinaryOperator("InventoryTransactionId",invTransaction,BinaryOperatorType.Equal);

                CriteriaOperator criteria = CriteriaOperator.And(criteria_Inventory, criteria_RowStatus);

                XPCollection<InventoryLedger> OldInventoryLedger = new XPCollection<InventoryLedger>(session,criteria);

                foreach (InventoryLedger ledger in OldInventoryLedger)
                {
                    ledger.RowStatus = Constant.ROWSTATUS_DELETED;
                    ledger.Save();
                }

                foreach (ETL_InventoryLedger ledger in ledgerList)
                {

                    InventoryLedger newInventoryLedger = new InventoryLedger(session);
                    newInventoryLedger.AccountId = session.GetObjectByKey<Account>(ledger.AccountId);
                    newInventoryLedger.CreateDate = ledger.CreateDate;
                    newInventoryLedger.Credit = ledger.Credit;
                    newInventoryLedger.Debit = ledger.Debit;
                    newInventoryLedger.Balance = ledger.Debit - ledger.Credit;
                    newInventoryLedger.Description = ledger.Description;
                    newInventoryLedger.InventoryId = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(ledger.InventoryId);
                    newInventoryLedger.InventoryTransactionId = session.GetObjectByKey<InventoryTransaction>(ledger.InventoryTransactionId);
                    newInventoryLedger.IsOriginal = ledger.IsOriginal;
                    newInventoryLedger.IssueDate = ledger.IssueDate;
                    newInventoryLedger.ItemUnitId = session.GetObjectByKey<ItemUnit>(ledger.ItemUnitId);
                    newInventoryLedger.LotId = session.GetObjectByKey<Lot>(ledger.LotId);
                    newInventoryLedger.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    newInventoryLedger.UpdateDate = DateTime.Now;
                    InventoryLedger preInventoryLedger = GetPreviousInventoryLedger(session, newInventoryLedger);
                    if (preInventoryLedger != null)
                    {
                        newInventoryLedger.Balance += preInventoryLedger.Balance;
                    }
                    newInventoryLedger.Save();
                }
            }
            catch(Exception)            
            {
                return;
            }            
        }
    }
}
