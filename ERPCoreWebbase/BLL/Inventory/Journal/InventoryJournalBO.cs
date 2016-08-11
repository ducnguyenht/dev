using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL.Inventory.Journal;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Inventory.Ledger;
using NAS.BO.Accounting.Journal;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Nomenclature.Item;
using Utility;
using NAS.DAL.Inventory.Lot;
using NAS.DAL.Accounting.AccountChart;

namespace NAS.BO.Inventory.Journal
{
    public class InventoryJournalBO
    {

        public static IEnumerable<Item> getItemUnitInInventory(Session session, Guid inventoryId)
        {
            List<Item> rs = null;
            try
            {
                XPQuery<Item> itemQuery = session.Query<Item>();
                XPQuery<ItemUnitRelationType> itemUnitRelationTypeQ = session.Query<ItemUnitRelationType>();
                ItemUnitRelationType unitRelationType = itemUnitRelationTypeQ.Where(r => r.Name == "UNIT").FirstOrDefault();
                XPQuery<InventoryJournal> inventoryJournalQuery = session.Query<InventoryJournal>();
                rs = (from ivj in inventoryJournalQuery
                      where ivj.InventoryId.InventoryId == inventoryId
                      && ivj.ItemUnitId.ItemUnitRelationTypeId == unitRelationType
                      group ivj by ivj.ItemUnitId.ItemId into it
                      select it.Key).ToList();
                return rs;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }

        public XPCollection<InventoryJournal> getTransactionInventoryJournals(Session session, Guid InventoryId, Guid ItemUnitId)
        {
            try
            {
                AccountingPeriod currentAP = AccountingPeriodBO.getCurrentAccountingPeriod(session);
                //if (currentAP == null)
                //    return null;

                XPCollection<InventoryTransaction> ITLst = new XPCollection<InventoryTransaction>(session);
                //ITLst.Criteria = CriteriaOperator.And(
                //        new BinaryOperator("AccountingPeriodId", currentAP, BinaryOperatorType.Equal),
                //        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                //    );

                ITLst.Criteria = new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater);

                if (ITLst == null || ITLst.Count == 0)
                    return null;

                CriteriaOperator criteriaIN = new InOperator("InventoryTransactionId", ITLst);
                XPCollection<InventoryJournal> IJLst = new XPCollection<InventoryJournal>(session);
                IJLst.Criteria = CriteriaOperator.And(
                    new InOperator("InventoryTransactionId", ITLst), 
                    new BinaryOperator("ItemUnitId!Key", ItemUnitId, BinaryOperatorType.Equal),
                    new BinaryOperator("InventoryId!Key", InventoryId, BinaryOperatorType.Equal)
                    );

                SortingCollection sortCollection = new SortingCollection();
                sortCollection.Add(new SortProperty("InventoryTransactionId.IssueDate", DevExpress.Xpo.DB.SortingDirection.Descending));
                sortCollection.Add(new SortProperty("CreateDate", DevExpress.Xpo.DB.SortingDirection.Descending));
                sortCollection.Add(new SortProperty("UpdateDate", DevExpress.Xpo.DB.SortingDirection.Descending));
                IJLst.Sorting = sortCollection;
                return IJLst;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public XPCollection<InventoryLedger> getTransactionInventoryLedgers(Session session, Guid InventoryId, Guid ItemUnitId)
        {
            try
            {
                AccountingPeriod currentAP = AccountingPeriodBO.getCurrentAccountingPeriod(session);
                //if (currentAP == null)
                //    return null;

                XPCollection<InventoryTransaction> ITLst = new XPCollection<InventoryTransaction>(session);
                //ITLst.Criteria = CriteriaOperator.And(
                //        new BinaryOperator("AccountingPeriodId", currentAP, BinaryOperatorType.Equal),
                //        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                //    );

                ITLst.Criteria = new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual);

                if (ITLst == null || ITLst.Count == 0)
                    return null;

                if (ITLst == null || ITLst.Count == 0)
                    return null;

                CriteriaOperator criteriaIN = new InOperator("InventoryTransactionId", ITLst);
                XPCollection<InventoryLedger> ILLst = new XPCollection<InventoryLedger>(session);
                ILLst.Criteria = CriteriaOperator.And(
                    new InOperator("InventoryTransactionId", ITLst),
                    new BinaryOperator("ItemUnitId!Key", ItemUnitId, BinaryOperatorType.Equal),
                    new BinaryOperator("InventoryId!Key", InventoryId, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual)
                    );

                SortingCollection sortCollection = new SortingCollection();
                sortCollection.Add(new SortProperty("IssueDate", DevExpress.Xpo.DB.SortingDirection.Descending));
                sortCollection.Add(new SortProperty("CreateDate", DevExpress.Xpo.DB.SortingDirection.Descending));
                sortCollection.Add(new SortProperty("UpdateDate", DevExpress.Xpo.DB.SortingDirection.Descending));
                ILLst.Sorting = sortCollection;
                return ILLst;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public XPCollection<COGS> getCOGS(Session session, Guid InventoryId, Guid ItemUnitId)
        {
            try
            {
                AccountingPeriod currentAP = AccountingPeriodBO.getCurrentAccountingPeriod(session);
                //if (currentAP == null)
                //    return null;

                XPCollection<InventoryTransaction> ITLst = new XPCollection<InventoryTransaction>(session);
                //ITLst.Criteria = CriteriaOperator.And(
                //        new BinaryOperator("AccountingPeriodId", currentAP, BinaryOperatorType.Equal),
                //        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                //    );
                ITLst.Criteria = new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater);

                if (ITLst == null || ITLst.Count == 0)
                    return null;

                CriteriaOperator criteriaIN = new InOperator("InventoryTransactionId", ITLst);
                XPCollection<COGS> ILLst = new XPCollection<COGS>(session);
                ILLst.Criteria = CriteriaOperator.And(
                    new InOperator("InventoryTransactionId", ITLst),
                    new BinaryOperator("ItemUnitId!Key", ItemUnitId, BinaryOperatorType.Equal),
                    new BinaryOperator("InventoryId!Key", InventoryId, BinaryOperatorType.Equal)
                    );

                SortingCollection sortCollection = new SortingCollection();
                sortCollection.Add(new SortProperty("InventoryTransactionId.IssueDate", DevExpress.Xpo.DB.SortingDirection.Descending));
                sortCollection.Add(new SortProperty("CreateDate", DevExpress.Xpo.DB.SortingDirection.Descending));
                sortCollection.Add(new SortProperty("UpdateDate", DevExpress.Xpo.DB.SortingDirection.Descending));
                ILLst.Sorting = sortCollection;
                return ILLst;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public XPCollection<InventoryJournal> GetDeliveryPlanningJournalForTransaction(Session session, Guid transactionId)
        {
            XPCollection<InventoryJournal> result = null;
            try
            {
                InventoryTransaction transaction = session.GetObjectByKey<InventoryTransaction>(transactionId);
                if (transaction == null)
                {
                    return result;
                }

                CriteriaOperator criteria_0 = new BinaryOperator("JournalType", Constant.PLANNING_JOURNAL, BinaryOperatorType.Equal);
                CriteriaOperator criteria_1 = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_2 = new BinaryOperator("Debit", 0, BinaryOperatorType.Equal);
                CriteriaOperator criteria_3 = new BinaryOperator("Credit", 0, BinaryOperatorType.NotEqual);
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1, criteria_2, criteria_3);
                result = new XPCollection<InventoryJournal>(session, transaction.InventoryJournals, criteria);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public Guid CreateInventoryPlanningJournal(Session session, Guid transactionId, Guid itemUnitId, double amount, Guid lotId, Guid inventoryId, string description)
        {
            Guid result = Guid.Empty;
            try
            {
                InventoryTransaction transaction = session.GetObjectByKey<InventoryTransaction>(transactionId);
                ItemUnit itemUnit = session.GetObjectByKey<ItemUnit>(itemUnitId);
                Lot lot = session.GetObjectByKey<Lot>(lotId);
                NAS.DAL.Nomenclature.Inventory.Inventory inventory = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(inventoryId);
                CriteriaOperator filter = new BinaryOperator("Code", DefaultAccountEnum.OWNER_INVENTORY, BinaryOperatorType.Equal);
                Account account = session.FindObject<Account>(filter);

                InventoryJournal creditJournal = new InventoryJournal(session);
                creditJournal.JournalType = Constant.PLANNING_JOURNAL;
                creditJournal.InventoryTransactionId = transaction;
                creditJournal.AccountId = account;
                creditJournal.ItemUnitId = itemUnit;
                creditJournal.LotId = lot;
                creditJournal.InventoryId = inventory;
                creditJournal.CreateDate = DateTime.Now;
                creditJournal.Credit = amount;
                creditJournal.Description = description;
                creditJournal.RowStatus = Constant.ROWSTATUS_ACTIVE;
                creditJournal.Save();

                InventoryJournal debitJournal = new InventoryJournal(session);
                debitJournal.JournalType = Constant.PLANNING_JOURNAL;
                debitJournal.InventoryTransactionId = transaction;
                debitJournal.AccountId = account;
                debitJournal.ItemUnitId = itemUnit;
                debitJournal.LotId = lot;
                debitJournal.InventoryId = inventory;
                debitJournal.CreateDate = DateTime.Now;
                debitJournal.Debit = amount;
                debitJournal.Description = description;
                debitJournal.RowStatus = Constant.ROWSTATUS_ACTIVE;
                debitJournal.Save();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public void UpdateInventoryPlanningJournal(Session session, Guid journalId, Guid itemUnitId, double amount, Guid lotId, Guid inventoryId, string description)
        {
            try
            {
                InventoryJournal journal = session.GetObjectByKey<InventoryJournal>(journalId);
                CriteriaOperator criteria_0 = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_1 = new BinaryOperator("Debit", amount, BinaryOperatorType.Equal);
                CriteriaOperator criteria_2 = new BinaryOperator("InventoryTransactionId", journal.InventoryTransactionId, BinaryOperatorType.Equal);
                CriteriaOperator criteria_3 = new BinaryOperator("ItemUnitId", journal.ItemUnitId, BinaryOperatorType.Equal);
                CriteriaOperator criteria_4 = new BinaryOperator("LotId", journal.LotId, BinaryOperatorType.Equal);
                CriteriaOperator criteria_5 = new BinaryOperator("InventoryId", journal.InventoryId, BinaryOperatorType.Equal);
                CriteriaOperator criteria_6 = new BinaryOperator("JournalType", journal.JournalType, BinaryOperatorType.Equal);
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1, criteria_2, criteria_3, criteria_4, criteria_5, criteria_6);

                InventoryJournal DebitJournal = session.FindObject<InventoryJournal>(criteria);

                journal.JournalType = Constant.PLANNING_JOURNAL;
                journal.InventoryId = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(inventoryId);
                journal.Credit = amount;
                journal.ItemUnitId = session.GetObjectByKey<ItemUnit>(itemUnitId);
                journal.LotId = session.GetObjectByKey<Lot>(lotId);
                journal.Debit = 0;
                journal.Description = description;
                journal.Save();

                DebitJournal.JournalType = Constant.PLANNING_JOURNAL;
                DebitJournal.InventoryId = session.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(inventoryId);
                DebitJournal.Credit = 0;
                DebitJournal.ItemUnitId = session.GetObjectByKey<ItemUnit>(itemUnitId);
                DebitJournal.LotId = session.GetObjectByKey<Lot>(lotId);
                DebitJournal.Debit = amount;
                DebitJournal.Description = description;
                DebitJournal.Save();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteInventoryPlanningJournal(Session session, Guid journalId)
        {
            try
            {
                InventoryJournal journal = session.GetObjectByKey<InventoryJournal>(journalId);
                CriteriaOperator criteria_0 = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_1 = new BinaryOperator("Debit", journal.Credit, BinaryOperatorType.Equal);
                CriteriaOperator criteria_2 = new BinaryOperator("InventoryTransactionId", journal.InventoryTransactionId, BinaryOperatorType.Equal);
                CriteriaOperator criteria_3 = new BinaryOperator("ItemUnitId", journal.ItemUnitId, BinaryOperatorType.Equal);
                CriteriaOperator criteria_4 = new BinaryOperator("LotId", journal.LotId, BinaryOperatorType.Equal);
                CriteriaOperator criteria_5 = new BinaryOperator("InventoryId", journal.InventoryId, BinaryOperatorType.Equal);
                CriteriaOperator criteria_6 = new BinaryOperator("JournalType", journal.JournalType, BinaryOperatorType.Equal);
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1, criteria_2, criteria_3, criteria_4, criteria_5, criteria_6);

                InventoryJournal DebitJournal = session.FindObject<InventoryJournal>(criteria);

                journal.RowStatus = Constant.ROWSTATUS_DELETED;
                journal.Save();
                if (DebitJournal != null)
                {
                    DebitJournal.RowStatus = Constant.ROWSTATUS_DELETED;
                    DebitJournal.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
