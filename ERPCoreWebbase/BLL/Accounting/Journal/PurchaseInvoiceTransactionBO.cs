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

namespace NAS.BO.Accounting.Journal
{
    public class PurchaseInvoiceTransactionBO
    {
        public XPCollection<Transaction> GetTransactionsAndRelatedTransactions(Session session, Guid billId)
        {
            try
            {
                XPCollection<Transaction> ret = null;
                XPCollection<Transaction> billTransactions = null;
                XPCollection<Transaction> voucherTransactions = null;
                XPCollection<Transaction> inventoryTransactions = null;

                PaymentVoucherTransactionBO paymentVoucherTransactionBO = new PaymentVoucherTransactionBO();

                //Get bill transactions
                billTransactions = GetTransactions(session, billId);
                //Get related voucher transactions
                voucherTransactions =
                    paymentVoucherTransactionBO.GetRelatedTransactionsWithBill(session, billId);
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
            PaymentVoucherTransactionBO paymentVoucherTransactionBO = new PaymentVoucherTransactionBO();
            return paymentVoucherTransactionBO.GetRelatedTransactionsWithBill(session, billId);
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
                NAS.DAL.Invoice.PurchaseInvoice bill = session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(billId);

                CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual),
                    CriteriaOperator.Or(
                        new ContainsOperator("GeneralJournals",
                            new BinaryOperator("JournalType", JounalTypeConstant.ACTUAL)
                        ),
                        new BinaryOperator(new AggregateOperand("GeneralJournals", Aggregate.Count), 0, BinaryOperatorType.Equal)
                    )
                );

                ret = new XPCollection<Transaction>(session, bill.PurchaseInvoiceTransactions, criteria);

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
                NAS.DAL.Invoice.PurchaseInvoice bill = session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(billId);

                CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual),
                    new ContainsOperator("GeneralJournals", new BinaryOperator("JournalType", JounalTypeConstant.PLANNING)));

                resultTransaction = new XPCollection<Transaction>(session, bill.PurchaseInvoiceTransactions, criteria);
            }
            catch (Exception)
            {
                throw;
            }
            return resultTransaction;
        }

        public Guid CreatePlanningTransaction(Session session, Guid billId, DateTime _PaymentDate, string _Name, double _Amount, string _Description, Guid _CurrencyId)
        {
            PurchaseInvoiceTransaction newTransaction = new PurchaseInvoiceTransaction(session);
            if (_Amount == 0) return Guid.Empty;
            try
            {
                AccountingBO accountingBO = new AccountingBO();
                NAS.DAL.Invoice.PurchaseInvoice purchaseInvoice = session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(billId);
                Currency currency = session.GetObjectByKey<Currency>(_CurrencyId);

                CriteriaOperator filter = new BinaryOperator("Code", DefaultAccountEnum.NAAN_DEFAULT.ToString(), BinaryOperatorType.Equal);
                Account account = session.FindObject<Account>(filter);

                if (purchaseInvoice == null || currency == null || account == null)
                {
                    return Guid.Empty;
                }

                newTransaction.IssueDate = _PaymentDate;
                newTransaction.Code = _Name;
                newTransaction.Amount = _Amount;
                newTransaction.Description = _Description;
                newTransaction.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                newTransaction.PurchaseInvoiceId = purchaseInvoice;
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
            PurchaseInvoiceTransaction transaction = session.GetObjectByKey<PurchaseInvoiceTransaction>(transactionId);
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
            PurchaseInvoiceTransaction transaction = session.GetObjectByKey<PurchaseInvoiceTransaction>(transactionId);
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
