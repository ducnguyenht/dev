using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Inventory.Command;
using NAS.DAL.Inventory.Journal;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Nomenclature.Organization;

namespace NAS.BO.Inventory.Command.Report
{
    public class RPT_InputInventoryCommand : RPT_InventoryCommand
    {       

        public override void LoadInventoryCommandReport(Guid InventoryCommandId)
        {
            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            try
            {
                InventoryCommand command = uow.GetObjectByKey<InventoryCommand>(InventoryCommandId);
                if (command == null)
                    return;

                if (!command.CommandType.Equals('I'))
                    throw new Exception("The command is invalid");

                NAS.DAL.Invoice.Bill bill = ICBO.GetSourceArtifactFromInventoryCommand(uow, command.InventoryCommandId);
                XPCollection<InventoryCommandItemTransaction> Transactions
                    = new XPCollection<InventoryCommandItemTransaction>(uow,
                        CriteriaOperator.Or( 
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal),
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_BOOKED_ENTRY, BinaryOperatorType.Equal)));

                CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("Credit", 0, BinaryOperatorType.Greater),
                    new BinaryOperator("JournalType", 'A', BinaryOperatorType.Equal),
                    new NotOperator(new NullOperator("InventoryTransactionId")),
                    CriteriaOperator.Or(
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal),
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_BOOKED_ENTRY, BinaryOperatorType.Equal)));

                XPCollection<InventoryJournal> retJournal1 = new XPCollection<InventoryJournal>(uow,
                        Transactions.SelectMany(r => r.InventoryJournals), criteria);
                IEnumerable<InventoryJournal> retJournal2 = retJournal1.Where(r => r.InventoryTransactionId != null &&
                             (r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE || r.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY) &&
                             (r.InventoryTransactionId as InventoryCommandItemTransaction) != null &&
                             (r.InventoryTransactionId.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE || r.InventoryTransactionId.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY) &&
                             (r.InventoryTransactionId as InventoryCommandItemTransaction).InventoryCommandId != null &&
                             (r.InventoryTransactionId as InventoryCommandItemTransaction).InventoryCommandId.InventoryCommandId == InventoryCommandId &&
                             ((r.InventoryTransactionId as InventoryCommandItemTransaction).InventoryCommandId.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE
                          || (r.InventoryTransactionId as InventoryCommandItemTransaction).InventoryCommandId.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY));

                int idx = 1;
                double totalValue = 0;
                Person pStoreKeeper = ICBO.GetSelectedActorInventoryCommandCombobox(InventoryCommandId, DefaultInventoryCommandActorTypeEnum.STOREKEEPER);
                Person pShipper = ICBO.GetSelectedActorInventoryCommandCombobox(InventoryCommandId, DefaultInventoryCommandActorTypeEnum.SHIPPER);
                Person pCreator = ICBO.GetSelectedActorInventoryCommandCombobox(InventoryCommandId, DefaultInventoryCommandActorTypeEnum.CREATOR);

                RPT_InventoryCommand_Rows = 
                    retJournal2.Select(
                        i => new RPT_InputInventoryCommand_Row() 
                        {
                            ////////////////////Setting - Header//////////////////START
                            SeqNo = idx++.ToString(),
                            Code = command.Code,
                            CreateDate = command.IssueDate,
                            InventoryName = command.RelevantInventoryId.Name,
                            InventoryAddress = command.RelevantInventoryId.Address,
                            AmountByString = string.Empty,
                            PurchaseInvoiceCode = bill == null ? string.Empty : bill.Code,
                            PurchaseInvoiceDate = command.IssueDate,
                            ShipperName = pShipper == null ? string.Empty : pShipper.Name,
                            CreatorName = pCreator == null ? string.Empty : pCreator.Name,
                            StoreKeeperName = pStoreKeeper == null ? string.Empty : pStoreKeeper.Name,
                            ////////////////////Setting - Item List//////////////////START
                            ItemCode = i.ItemUnitId.ItemId.Code,
                            ItemName = i.ItemUnitId.ItemId.Name,
                            ItemUnit = i.ItemUnitId.UnitId.Name,
                            Quantity = i.Credit,
                            PlanQuantity = i.PlanCredit,
                            Price = bill != null && bill.BillItems != null && bill.BillItems.Count > 0 ? 
                                bill.BillItems.Where(r => r.ItemUnitId == i.ItemUnitId).FirstOrDefault().Price : 0,
                            TotalOfRow = bill != null && bill.BillItems != null && bill.BillItems.Count > 0 ? 
                                i.Credit * bill.BillItems.Where(r => r.ItemUnitId == i.ItemUnitId).FirstOrDefault().Price : 0
                            ,
                            TotalString = Utility.Accounting.NumberToString((totalValue = totalValue + (bill != null && bill.BillItems != null && bill.BillItems.Count > 0 ?
                                i.Credit * bill.BillItems.Where(r => r.ItemUnitId == i.ItemUnitId).FirstOrDefault().Price : 0)))
                            ////////////////////Setting - Item List//////////////////END
                        }).ToList<RPT_InventoryCommand_Row>();                
                
                ////////////////////Setting - Financial List//////////////////START
                IEnumerable<GeneralJournal> GeneralJournals = command.InventoryCommandFinancialTransactions.Where(t =>
                    (t.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE ||
                        t.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)).SelectMany(
                    j => j.GeneralJournals).Where(j => (j.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE ||
                        j.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                        && j.JournalType.Equals('A')
                        && j.AccountId != null
                        && j.TransactionId != null);

                if (GeneralJournals != null && GeneralJournals.Count() > 0)
                {
                    RPT_GenernalJournals_Objects = GeneralJournals.Select(
                            j => new RPT_GenernalJournal
                            {
                                GeneralJournalId = j.GeneralJournalId,
                                AccountId = j.AccountId.AccountId,
                                AccountName = j.AccountId.Name,
                                AccountCode = j.AccountId.Code,
                                Credit = j.Credit,
                                Debit = j.Debit,
                                Description = j.Description,
                                JournalType = j.JournalType,
                                IssueDate = j.TransactionId.IssueDate,
                                TransactionId = j.TransactionId.TransactionId
                            }
                        ).ToList<RPT_GenernalJournal>();

                    RPT_GenernalJournals_Objects = this.GetProcessedJournalForReport();
                }
                ////////////////////Setting - Financial List//////////////////END
            }
            catch (Exception)
            {
                throw;
            }
            finally {
                uow.Dispose();
            }
        }
    }
}
