using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Inventory.Command;
using NAS.BO.Accounting.Journal;
using NAS.DAL.Invoice;
using NAS.DAL.Inventory.Journal;
using NAS.BO.Accounting;
using NAS.DAL.Inventory.Ledger;
using NAS.DAL.Accounting.Journal;

namespace NAS.BO.Inventory.Command
{
    public partial class InventoryCommandBO
    {
        /// <summary>
        /// Kiểm tra phiếu kho đã ghi sổ hay chưa
        /// </summary>
        /// <param name="_InventoryCommand"></param>
        /// <returns></returns>
        public bool IsBookedEntriesForInventoryCommand(Session _Session, Guid _InventoryCommand)
        {
            try
            {
                NAS.BO.Accounting.Journal.TransactionBOBase transactionBOBase = new TransactionBOBase();
                InventoryCommand command = _Session.GetObjectByKey<InventoryCommand>(_InventoryCommand);
                if (command == null)
                    throw new Exception("The InventoryCommand is not exist in system");

                command.Reload();

                if (command.InventoryCommandFinancialTransactions != null && command.InventoryCommandFinancialTransactions.Count > 0)
                    foreach (InventoryCommandFinancialTransaction t in command.InventoryCommandFinancialTransactions)
                    {
                        Transaction transaction = null;
                        if (!transactionBOBase.IsBookedTransaction(_Session, t.TransactionId, out transaction))
                            return false;
                    }

                if (command.RowStatus != Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                    return false;

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Ghi sổ tài chính phiếu kho
        /// </summary>
        /// <param name="_InventoryCommand"></param>
        public void BookFinancialEntriesOfInventoryCommand(Guid _InventoryCommand)
        {
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                NAS.BO.Accounting.Journal.TransactionBOBase transactionBOBase = new TransactionBOBase();
                InventoryCommand command = uow.GetObjectByKey<InventoryCommand>(_InventoryCommand);
                if (command == null)
                    throw new Exception("The InventoryCommand is not exist in system");

                InventoryCommandBO CheckBO = new InventoryCommandBO();

                if (CheckBO.IsBookedEntriesForInventoryCommand(uow, _InventoryCommand))
                    throw new Exception(string.Format("Không thể tiến hành vì Phiếu '{0}' đã hạch toán từ trước!", command.Code));

                int objectFinacialType = int.MinValue;
                int objectItemType = int.MinValue;
                Bill billArtifact = GetSourceArtifactFromInventoryCommand(uow, command.InventoryCommandId);

                if (command.CommandType == INVENTORY_COMMAND_TYPE.IN)
                {
                    objectFinacialType = Utility.Constant.BusinessObjectType_InputInventoryCommandFinancialTransaction;
                    objectItemType = Utility.Constant.BusinessObjectType_InputInventoryCommandItemTransaction;
                }
                else if (command.CommandType == INVENTORY_COMMAND_TYPE.OUT)
                {
                    objectFinacialType = Utility.Constant.BusinessObjectType_OutputInventoryCommandFinancialTransaction;
                    objectItemType = Utility.Constant.BusinessObjectType_OutputInventoryCommandItemTransaction;
                }

                if (command.InventoryCommandFinancialTransactions != null && command.InventoryCommandFinancialTransactions.Count > 0)
                    foreach (InventoryCommandFinancialTransaction t in command.InventoryCommandFinancialTransactions)
                    {
                        if (t.RowStatus != Utility.Constant.ROWSTATUS_BOOKED_ENTRY && t.RowStatus != Utility.Constant.ROWSTATUS_ACTIVE)
                            continue;

                        CanBookingEntryReturnValue rs = transactionBOBase.CanBookingEntry(t.TransactionId, true);
                        if (rs == CanBookingEntryReturnValue.DEBIT_CREDIT_ZERO)
                            throw new Exception(string.Format("Bút toán '{0}' không hợp lệ! Nợ = Có = 0", t.Code));
                        else if (rs == CanBookingEntryReturnValue.HAVE_NO_JOURNAL)
                            throw new Exception(string.Format("Bút toán '{0}' không chưa nhập định khoản", t.Code));
                        else if (rs == CanBookingEntryReturnValue.INVALID_TRANSACTION_STATUS)
                            throw new Exception(string.Format("Bút toán '{0}' có trạng thái không hợp lệ", t.Code));
                        else if (rs == CanBookingEntryReturnValue.MANY_SIDE)
                            throw new Exception(string.Format("Bút toán '{0}' không hợp lệ vì có nhiều tài khoản nợ và nhiều tài khoản có", t.Code));
                        else if (rs == CanBookingEntryReturnValue.NOT_BALANCED)
                            throw new Exception(string.Format("Bút toán '{0}' chưa cân bằng", t.Code));
                    }

                foreach (InventoryCommandFinancialTransaction t in command.InventoryCommandFinancialTransactions)
                {
                    if (t.RowStatus != Utility.Constant.ROWSTATUS_BOOKED_ENTRY && t.RowStatus != Utility.Constant.ROWSTATUS_ACTIVE)
                        continue;

                    if (!transactionBOBase.BookEntry(uow, t.TransactionId))
                        throw new Exception("Xử lý ghi sổ phát sinh lỗi");
                    t.AccountingPeriodId = AccountingPeriodBO.GetAccountingPeriod(uow, t.IssueDate);
                    uow.FlushChanges();
                }

                foreach (InventoryTransaction t in command.InventoryCommandItemTransactions)
                {
                    if (t.RowStatus != Utility.Constant.ROWSTATUS_BOOKED_ENTRY && t.RowStatus != Utility.Constant.ROWSTATUS_ACTIVE)
                        continue;

                    if (billArtifact != null)
                    {
                        CurrencyBO currencyBO = new CurrencyBO();
                        COGSBO CogsBO = new COGSBO();
                        COGSBussinessBO COGSInventoryCommandBO = new COGSBussinessBO();

                        foreach (InventoryJournal j in t.InventoryJournals)
                        {
                            #region setting COGS
                            if (command.CommandType == INVENTORY_COMMAND_TYPE.IN)
                            {
                                if (j.JournalType.Equals('A') && j.Debit > 0 && j.Credit == 0)
                                {
                                    BillItem billItem = billArtifact.BillItems.Where(
                                        i => i.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE &&
                                        i.ItemUnitId == j.ItemUnitId).FirstOrDefault();

                                    if (billItem == null)
                                        throw new Exception("The ItemUnit is not exist in Bill");

                                    COGSInventoryCommandBO.CreateCOGS(
                                        uow,
                                        0,
                                        j.Debit,
                                        DateTime.Now,
                                        billItem.Price,
                                        t.IssueDate,
                                        t.InventoryTransactionId,
                                        command.RelevantInventoryId.InventoryId,
                                        j.ItemUnitId.ItemUnitId,
                                        currencyBO.GetDefaultCurrency(uow).CurrencyId);
                                }
                            }
                            else if (command.CommandType == INVENTORY_COMMAND_TYPE.OUT)
                            {
                                if (!j.JournalType.Equals('A') && j.Debit == 0 && j.Credit > 0)
                                {
                                    COGS LastCogs =
                                        CogsBO.GetLastCOGS(
                                            uow,
                                            j.ItemUnitId.ItemUnitId,
                                            currencyBO.GetDefaultCurrency(uow).CurrencyId,
                                            command.RelevantInventoryId.InventoryId);

                                    COGSInventoryCommandBO.CreateCOGS(
                                        uow,
                                        j.Credit,
                                        0,
                                        DateTime.Now,
                                        LastCogs == null ? 0 : LastCogs.COGSPrice,
                                        t.IssueDate,
                                        t.InventoryTransactionId,
                                        command.RelevantInventoryId.InventoryId,
                                        j.ItemUnitId.ItemUnitId,
                                        currencyBO.GetDefaultCurrency(uow).CurrencyId);
                                }
                            }
                            #endregion
                            //j.RowStatus = Utility.Constant.ROWSTATUS_BOOKED_ENTRY;
                        }
                        uow.FlushChanges();
                    }
                    t.AccountingPeriodId = AccountingPeriodBO.GetAccountingPeriod(uow, t.IssueDate);
                    t.RowStatus = Utility.Constant.ROWSTATUS_BOOKED_ENTRY;
                }

                command.RowStatus = Utility.Constant.ROWSTATUS_BOOKED_ENTRY;

                foreach (InventoryTransaction t in command.InventoryCommandItemTransactions)
                {
                    if (t.RowStatus != Utility.Constant.ROWSTATUS_BOOKED_ENTRY && t.RowStatus != Utility.Constant.ROWSTATUS_ACTIVE)
                        continue;

                    BusinessObjectBO.CreateBusinessObject(uow,
                            objectItemType,
                            t.InventoryTransactionId,
                            t.IssueDate);
                }

                foreach (InventoryCommandFinancialTransaction tf in command.InventoryCommandFinancialTransactions)
                {
                    if (tf.RowStatus != Utility.Constant.ROWSTATUS_BOOKED_ENTRY && tf.RowStatus != Utility.Constant.ROWSTATUS_ACTIVE)
                        continue;

                    BusinessObjectBO.CreateBusinessObject(uow,
                            objectFinacialType,
                            tf.TransactionId,
                            tf.IssueDate);
                }
                uow.CommitChanges();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (uow != null) uow.Dispose();
            }
        }
    }
}
