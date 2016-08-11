using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Invoice;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.Currency;
using NAS.DAL.Accounting.AccountChart;
using Utility;
using NAS.BO.Inventory.Command;
using NAS.DAL.Inventory.Command;

namespace NAS.BO.Accounting.Journal
{
    public class SaleInvoiceTransactionBO
    {
        public XPCollection<Transaction> GetTransactionsAndRelatedTransactions(Session session, Guid billId)
        {
            try
            {
                XPCollection<Transaction> ret = null;
                XPCollection<Transaction> billTransactions = null;
                XPCollection<Transaction> voucherTransactions = null;
                XPCollection<Transaction> inventoryTransactions = null;
                InventoryCommandBO inventoryCommandBO = new InventoryCommandBO();

                inventoryTransactions = inventoryCommandBO.GetInventoryFinancialTransactionOfSourceBill(session, billId);

                ReceiptVoucherTransactionBO receiptVoucherTransactionBO = new ReceiptVoucherTransactionBO();

                //Get bill transactions
                billTransactions = GetTransactions(session, billId);
                //Get related voucher transactions
                voucherTransactions = 
                    receiptVoucherTransactionBO.GetRelatedTransactionsWithBill(session, billId);
                //Get related inventory transactions...

                ret = billTransactions;

                if (voucherTransactions != null)
                    ret.AddRange(voucherTransactions);

                if (inventoryTransactions != null)
                    ret.AddRange(inventoryTransactions);

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public XPCollection<Transaction> GetVoucherTransactions(Session session, Guid billId)
        {
            ReceiptVoucherTransactionBO receiptVoucherTransactionBO = new ReceiptVoucherTransactionBO();
            return receiptVoucherTransactionBO.GetRelatedTransactionsWithBill(session, billId);
        }

        public XPCollection<Transaction> GetInventoryTransactions(Session session, Guid billId)
        {
            InventoryCommandBO inventoryCommandBO = new InventoryCommandBO();
            return inventoryCommandBO.GetInventoryFinancialTransactionOfSourceBill(session, billId);
        }

        public XPCollection<Transaction> GetTransactions(Session session, Guid billId)
        {
            try
            {
                XPCollection<Transaction> ret = null;
                //Get bill
                SalesInvoice bill = session.GetObjectByKey<SalesInvoice>(billId);

                CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual),
                    CriteriaOperator.Or(
                        new ContainsOperator("GeneralJournals",
                            new BinaryOperator("JournalType", JounalTypeConstant.ACTUAL)
                        ),
                        new BinaryOperator(new AggregateOperand("GeneralJournals", Aggregate.Count), 0, BinaryOperatorType.Equal)
                    )
                );

                ret = new XPCollection<Transaction>(session, bill.SaleInvoiceTransactions, criteria);

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public XPCollection<Transaction> GetPlanningTransactionsOfBill(Session session, Guid billId)
        {
            XPCollection<Transaction> resultTransaction = null;
            try
            {
                NAS.DAL.Invoice.SalesInvoice bill = session.GetObjectByKey<NAS.DAL.Invoice.SalesInvoice>(billId);

                CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual),
                    new ContainsOperator("GeneralJournals", new BinaryOperator("JournalType", JounalTypeConstant.PLANNING)));

                resultTransaction = new XPCollection<Transaction>(session, bill.SaleInvoiceTransactions, criteria);
            }
            catch (Exception)
            {
                throw;
            }
            return resultTransaction;
        }

        public Guid CreatePlanningTransaction(Session session, Guid billId, DateTime _PaymentDate, string _Name, double _Amount, string _Description, Guid _CurrencyId)
        {
            SaleInvoiceTransaction newTransaction = new SaleInvoiceTransaction(session);
            try
            {
                AccountingBO accountingBO = new AccountingBO();
                SalesInvoice salesInvoice = session.GetObjectByKey<SalesInvoice>(billId);
                Currency currency = session.GetObjectByKey<Currency>(_CurrencyId);

                CriteriaOperator filter = new BinaryOperator("Code", DefaultAccountEnum.NAAN_DEFAULT.ToString(), BinaryOperatorType.Equal);
                Account account = session.FindObject<Account>(filter);

                if (salesInvoice == null || currency == null || account == null)
                {
                    return Guid.Empty;
                }
                
                newTransaction.IssueDate = _PaymentDate;
                newTransaction.Code = _Name;
                newTransaction.Amount = _Amount;
                newTransaction.Description = _Description;
                newTransaction.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                newTransaction.SalesInvoiceId = salesInvoice;
                newTransaction.CreateDate = DateTime.Now;
                newTransaction.UpdateDate = DateTime.Now;
                newTransaction.Save();

                accountingBO.CreateGeneralJournal(session, newTransaction.TransactionId, account.AccountId, Constant.PLANNING_JOURNAL, _Name, _Amount, 0, Constant.ROWSTATUS_ACTIVE, _CurrencyId);
                accountingBO.CreateGeneralJournal(session, newTransaction.TransactionId, account.AccountId, Constant.PLANNING_JOURNAL, _Name, 0, _Amount, Constant.ROWSTATUS_ACTIVE, _CurrencyId);
            }
            catch (Exception)
            {
                throw;
            }
            return newTransaction.TransactionId;
        }

        public void UpdatePlanningTransaction(Session session, Guid transactionId, DateTime _PaymentDate, string _Name, double _Amount, string _Description, Guid _CurrencyId)
        {
            SaleInvoiceTransaction transaction = session.GetObjectByKey<SaleInvoiceTransaction>(transactionId);
            if (transaction == null || _Amount == 0) return;
            try
            {
                AccountingBO accountingBO = new AccountingBO();

                Currency currency = session.GetObjectByKey<Currency>(_CurrencyId);
                CriteriaOperator filter = new BinaryOperator("Code", DefaultAccountEnum.NAAN_DEFAULT.ToString(), BinaryOperatorType.Equal);
                Account account = session.FindObject<Account>(filter);

                if (currency == null || account == null)
                {
                    return;
                }

                transaction.IssueDate = _PaymentDate;
                transaction.Code = _Name;
                transaction.Amount = _Amount;
                transaction.Description = _Description;
                transaction.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                transaction.UpdateDate = DateTime.Now;
                transaction.Save();

                CriteriaOperator criteria_0 = new BinaryOperator("TransactionId", transaction, BinaryOperatorType.Equal);
                CriteriaOperator criteria_1 = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_2 = new BinaryOperator("JournalType", Constant.PLANNING_JOURNAL, BinaryOperatorType.Equal);
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1, criteria_2);

                XPCollection<GeneralJournal> journalList = new XPCollection<GeneralJournal>(session, criteria);

                foreach (GeneralJournal generalJournal in journalList)
                {
                    generalJournal.AccountId = account;
                    if (generalJournal.Credit == 0)
                    {
                        generalJournal.Debit = _Amount;
                    }
                    else
                    {
                        generalJournal.Credit = _Amount;
                    }
                    generalJournal.Description = _Name;
                    generalJournal.CurrencyId = currency;
                    generalJournal.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeletePlanningTransaction(Session session, Guid transactionId)
        {
            SaleInvoiceTransaction transaction = session.GetObjectByKey<SaleInvoiceTransaction>(transactionId);
            if (transaction == null) return;
            try
            {
                transaction.RowStatus = Constant.ROWSTATUS_DELETED;
                transaction.Save();

                CriteriaOperator criteria_0 = new BinaryOperator("TransactionId", transaction, BinaryOperatorType.Equal);
                CriteriaOperator criteria_1 = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator criteria_2 = new BinaryOperator("JournalType", Constant.PLANNING_JOURNAL, BinaryOperatorType.Equal);
                CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_2);

                XPCollection<GeneralJournal> journalList = new XPCollection<GeneralJournal>(session, criteria);

                foreach (GeneralJournal generalJournal in journalList)
                {
                    generalJournal.RowStatus = Constant.ROWSTATUS_DELETED;
                    generalJournal.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
