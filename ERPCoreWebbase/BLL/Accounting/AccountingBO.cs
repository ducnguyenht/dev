using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using NAS.BO.Accounting.Journal;
using NAS.BO.Inventory.Jouranl;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL.Accounting.Currency;
using NAS.DAL.Accounting.Entry;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Invoice;
using NAS.DAL.Vouches;
using Utility;

namespace NAS.BO.Accounting
{

    /// <summary>
    /// 
    /// </summary>
    public enum AccountingErrorCodeEnum
    {
        /// <summary>
        /// Not ok
        /// </summary>
        /// 
        NOT_OK,
        /// <summary>
        /// Ok
        /// </summary>
        OK,
        /// <summary>
        /// Exist an error transaction
        /// </summary>
        EXIST_ERROR_TRANSACTION,
        /// <summary>
        /// Total of all transaction is not equal with artifact total
        /// </summary>
        NOT_EQUAL_TOTAL
    }

    public class AccountingBO
    {
        public double GetAccountBalanceInit(Session session, Guid AccountId)
        {
            return 0;
        }

        public static bool IsExistIn<T>(Session session, Guid ID)
                                        where T : XPCustomObject
        {
            var a = typeof(T);
            T item = session.GetObjectByKey<T>(ID);
            if (item == null) return false;
            return true;
        }
        //Check Balance of Account: True -> Imported Balance / False -> Don't Import Balance
        public static bool IsBalanceInitted(Session session, Guid AccountId)
        {
            XPQuery<GeneralJournalBalanceForward> GJBFQuery = session.Query<GeneralJournalBalanceForward>();
            GeneralJournalBalanceForward GJBF = (from c in GJBFQuery
                                                 where c.AccountId.AccountId == AccountId && c.RowStatus >= Utility.Constant.ROWSTATUS_ACTIVE
                                                 orderby c.TransactionId.IssueDate descending
                                                 select c).FirstOrDefault();
            if (GJBF != null) return true;
            return false;
        }
        //Check Artifact is Approved or Not
        public static bool IsApprovedArtifact<T>(Session session, Guid originalArtifactId)
        {
            return Journal.TransactionBO.isApprovedCosting<T>(session, originalArtifactId);
        }
        //Check Transaction is Approved or Not
        public static bool IsApproved(Session session, Guid TransactionId)
        {
            XPQuery<GeneralLedger> LedgerQuery = session.Query<GeneralLedger>();
            GeneralLedger ledger = (from c in LedgerQuery
                                    where c.TransactionId.TransactionId == TransactionId
                                    select c).FirstOrDefault();
            if (ledger != null) return true; //Đã hạch toán
            return false; //Chưa hạch toán
        }
        //Approve an Artiface
        public bool ApproveArtifact<T>(Session session, Guid originalArtifactId)
        {
            try
            {
                if (IsApprovedArtifact<T>(session, originalArtifactId) == false) return false;
                Journal.TransactionBO.ProcessApproveCosting<T>(session, originalArtifactId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public class TranDes
        {
            public string Description
            {
                get;
                set;
            }
            public double Balance
            {
                get;
                set;
            }
            public TranDes(string des, double bal)
            {
                Description = des;
                Balance = bal;
            }
        }
        //Transaction--------------------------------
        public bool UpdateBalanceForwardTransaction(Session session, Guid AccountId, double Balance)
        {
            try
            {
                XPQuery<GeneralJournalBalanceForward> JournalQuery = session.Query<GeneralJournalBalanceForward>();
                GeneralJournalBalanceForward Journal = (from c in JournalQuery
                                                        where c.AccountId.AccountId == AccountId
                                                        && c.TransactionId.AccountingPeriodId == AccountingPeriodBO.getCurrentAccountingPeriod(session)
                                                        select c).FirstOrDefault();
                double deltaBalance = Balance - Journal.Balance;
                Journal.Balance += deltaBalance;
                Journal.RowStatus = Constant.ROWSTATUS_ACTIVE;
                Journal.Save();
                BalanceForwardTransaction transaction = session.GetObjectByKey<BalanceForwardTransaction>(Journal.TransactionId.TransactionId);
                transaction.Balance += deltaBalance;
                transaction.UpdateDate = DateTime.Now;
                Journal.RowStatus = Constant.ROWSTATUS_ACTIVE;
                transaction.Save();
                XPQuery<GeneralLedger> GeneralLedgerQuery = session.Query<GeneralLedger>();
                var LedgerList = (from c in GeneralLedgerQuery
                                  where c.TransactionId == transaction
                                  select c);

                foreach (GeneralLedger ledger in LedgerList)
                {
                    GeneralLedger sledger = session.GetObjectByKey<GeneralLedger>(ledger.GeneralLedgerId);
                    sledger.Balance += deltaBalance;
                    sledger.UpdateDate = DateTime.Now;
                    double debit = sledger.AccountId.BalanceType == Utility.Constant.BALANCE_TYPE_CREDIT ? 0 : deltaBalance;
                    double credit = sledger.AccountId.BalanceType == Utility.Constant.BALANCE_TYPE_CREDIT ? deltaBalance : 0;
                    sledger.Debit += debit;
                    sledger.Credit += credit;
                    sledger.Save();
                    //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---START
                    //DownRepairLedger(session, sledger.GeneralLedgerId, debit, credit);
                    DownRepairLedger(session, sledger.GeneralLedgerId, debit, credit, sledger.CurrencyId.Code);
                    //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---END
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public bool ApproveTransaction(Session session, Guid TransactionId)
        {
            try
            {
                Transaction transaction = session.GetObjectByKey<Transaction>(TransactionId);
                if (transaction == null) return false;
                if (IsAvailableApprove(session, TransactionId) > 0) return false;
                double count = 0;
                foreach (GeneralJournal journal in transaction.GeneralJournals)
                {
                    CreateGeneralLedger(session, TransactionId, journal.GeneralJournalId, count);
                    count += 10;
                    //CreateGeneralLedger(session, TransactionId, journal.GeneralJournalId);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="session">Devexpress.XPO.Session</param>
        /// <param name="TransactionId"></param>
        /// <returns>
        /// -1: OK. Balance forward. 
        /// 0: OK. You can approve it. 
        /// (order: Can't approve) 
        /// 1: Credit != Debit. 
        /// 2: Approved. 
        /// 3: Credit = Debit = 0. 
        /// 4: Credit &lt; 0. 
        /// 5: Debit  &lt; 0. 
        /// 6: Don't have any journal 
        /// 7: Transaction not exist 
        /// 8: Credit and Debit &lt; 0 
        /// 9: Exist more than one Credit Account && more than one Debit Account
        /// </returns>
        public short IsAvailableApprove(Session session, Guid TransactionId)
        {
            BalanceForwardTransaction transaction = null;
            try
            {
                transaction = session.GetObjectByKey<BalanceForwardTransaction>(TransactionId);
                if (transaction != null) return -1; //Balance forward
            }
            catch (Exception)
            {
            }
            Transaction nTransaction = session.GetObjectByKey<Transaction>(TransactionId);
            if (nTransaction == null) return 7;//Transaction not exist            
            if (IsApproved(session, TransactionId) == true) return 2; //Was Approved
            XPQuery<GeneralJournal> journal = session.Query<GeneralJournal>();
            var templist = (from c in journal
                            where c.TransactionId.TransactionId == TransactionId
                            && c.RowStatus >= Constant.ROWSTATUS_ACTIVE
                            select c);
            if (templist.Count() == 0) return 6; //Don't have any journal
            if (TransactionTotalCredit(session, TransactionId) == 0 && TransactionTotalDebit(session, TransactionId) == 0) return 3; //Credit = Debit = 0
            if (TransactionTotalCredit(session, TransactionId) != TransactionTotalDebit(session, TransactionId)) return 1; //Credit != Debit
            if (TransactionTotalCredit(session, TransactionId) < 0 && TransactionTotalDebit(session, TransactionId) < 0) return 8; //Credit and Debit < 0
            if (TransactionTotalCredit(session, TransactionId) < 0) return 4;//Credit < 0
            if (TransactionTotalDebit(session, TransactionId) < 0) return 5;//Debit < 0            
            int debitCount = 0;
            int creditCount = 0;
            var debitList = (from c in journal
                             where c.AccountId.AccountTypeId.AccountCategoryId.Code != "OFFBALANCE"
                             && c.TransactionId.TransactionId == TransactionId
                             && c.Debit != 0
                             select c);
            debitCount = debitList.Count();
            var creditList = (from c in journal
                              where c.AccountId.AccountTypeId.AccountCategoryId.Code != "OFFBALANCE"
                              && c.TransactionId.TransactionId == TransactionId
                             && c.Credit != 0
                              select c);
            creditCount = creditList.Count();
            if (creditCount > 1 && debitCount > 1) return 9;// n Debit -> n Credit

            return 0;//OK. You can approve it
        }
        public AccountingErrorCodeEnum IsAvailableApproveArtifact<T>(Session session, T artifact)
        {
            AccountingErrorCodeEnum result = AccountingErrorCodeEnum.OK;
            double totalValue = 0;
            double totalAccountingValue = 0;
            try
            {
                InventoryTransactionBO inventoryTransactionBO = new InventoryTransactionBO();
                if (artifact is SalesInvoiceInventoryTransaction)
                {
                    SalesInvoiceInventoryTransaction transaction = artifact as SalesInvoiceInventoryTransaction;
                    totalValue = inventoryTransactionBO.getSaleInvoiceInventoryTransactionTotal(session, transaction.InventoryTransactionId);
                    XPQuery<SalesInvoiceInventoryAccountingTransaction> salesInvoiceInventoryAccountingTransaction = session.Query<SalesInvoiceInventoryAccountingTransaction>();
                    var accountingTransactionList = (from c in salesInvoiceInventoryAccountingTransaction
                                                     where c.SalesInvoiceInventoryTransactionId == transaction
                                                     select c);
                    foreach (SalesInvoiceInventoryAccountingTransaction accTran in accountingTransactionList)
                    {
                        if (IsAvailableApprove(session, accTran.TransactionId) != 0) return AccountingErrorCodeEnum.EXIST_ERROR_TRANSACTION;
                        totalAccountingValue += TransactionTotalValue(session, accTran.TransactionId);
                    }
                    if (totalAccountingValue != totalValue) return AccountingErrorCodeEnum.NOT_EQUAL_TOTAL;
                }
                else if (artifact is PurchaseInvoiceInventoryTransaction)
                {
                    PurchaseInvoiceInventoryTransaction transaction = artifact as PurchaseInvoiceInventoryTransaction;
                    totalValue = inventoryTransactionBO.getSaleInvoiceInventoryTransactionTotal(session, transaction.InventoryTransactionId);
                    XPQuery<PurchaseInvoiceInventoryAccountingTransaction> PurchaseInvoiceInventoryAccountingTransaction = session.Query<PurchaseInvoiceInventoryAccountingTransaction>();
                    var accountingTransactionList = (from c in PurchaseInvoiceInventoryAccountingTransaction
                                                     where c.PurchaseInvoiceInventoryTransactionId == transaction
                                                     select c);
                    foreach (PurchaseInvoiceInventoryAccountingTransaction accTran in accountingTransactionList)
                    {
                        if (IsAvailableApprove(session, accTran.TransactionId) != 0) return AccountingErrorCodeEnum.EXIST_ERROR_TRANSACTION;
                        totalAccountingValue += TransactionTotalValue(session, accTran.TransactionId);
                    }
                    if (totalAccountingValue != totalValue) return AccountingErrorCodeEnum.NOT_EQUAL_TOTAL;
                }
                else if (artifact is ReceiptVouches)
                {
                    ReceiptVouches vouches = artifact as ReceiptVouches;
                    totalValue = vouches.SumOfCredit + vouches.SumOfDebit;
                    XPQuery<ReceiptVouchesTransaction> transactionquery = session.Query<ReceiptVouchesTransaction>();
                    var accountingTransactionList = (from c in transactionquery
                                                     where c.ReceiptVouchesId == vouches
                                                     select c);
                    foreach (ReceiptVouchesTransaction transaction in accountingTransactionList)
                    {
                        if (IsAvailableApprove(session, transaction.TransactionId) != 0) return AccountingErrorCodeEnum.EXIST_ERROR_TRANSACTION;
                        totalAccountingValue += TransactionTotalValue(session, transaction.TransactionId);
                    }
                    if (totalAccountingValue != totalValue) return AccountingErrorCodeEnum.NOT_EQUAL_TOTAL;
                }
                else if (artifact is PaymentVouches)
                {
                    PaymentVouches vouches = artifact as PaymentVouches;
                    totalValue = vouches.SumOfCredit + vouches.SumOfDebit;
                    XPQuery<PaymentVouchesTransaction> transactionquery = session.Query<PaymentVouchesTransaction>();
                    var accountingTransactionList = (from c in transactionquery
                                                     where c.PaymentVouchesId == vouches
                                                     select c);
                    foreach (PaymentVouchesTransaction transaction in accountingTransactionList)
                    {
                        if (IsAvailableApprove(session, transaction.TransactionId) != 0) return AccountingErrorCodeEnum.EXIST_ERROR_TRANSACTION;
                        totalAccountingValue += TransactionTotalValue(session, transaction.TransactionId);
                    }

                    if (totalAccountingValue != totalValue) return AccountingErrorCodeEnum.NOT_EQUAL_TOTAL;
                }
            }
            catch (Exception)
            {
                return AccountingErrorCodeEnum.NOT_OK;
            }
            return result;
        }
        public double TransactionTotalCredit(Session session, Guid TransactionId)
        {
            BalanceForwardTransaction transaction = null;
            try
            {
                transaction = session.GetObjectByKey<BalanceForwardTransaction>(TransactionId);
                if (transaction != null)
                    return transaction.Balance;
            }
            catch (Exception)
            {
            }
            Transaction nTransaction = session.GetObjectByKey<Transaction>(TransactionId);
            if (nTransaction == null) return -0.1234567899876543;
            double TotalCredit = 0;

            foreach (GeneralJournal journal in nTransaction.GeneralJournals)
            {
                if (journal.AccountId.AccountTypeId.AccountCategoryId.Code != "OFFBALANCE")
                {
                //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---START
                    //TotalCredit += journal.Credit;
                    TotalCredit += journal.Credit * getCoefficientCompareWithDefaultByCurrencyCode(session, journal.CurrencyId.Code);
                //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---END
                }
            }

            return TotalCredit;
        }
        public double TransactionTotalDebit(Session session, Guid TransactionId)
        {
            BalanceForwardTransaction transaction = null;
            try
            {
                transaction = session.GetObjectByKey<BalanceForwardTransaction>(TransactionId);
                if (transaction != null) return transaction.Balance;
            }
            catch (Exception)
            {
            }
            Transaction nTransaction = session.GetObjectByKey<Transaction>(TransactionId);
            if (nTransaction == null) return -0.1234567899876543;
            double TotalDebit = 0;

            foreach (GeneralJournal journal in nTransaction.GeneralJournals)
            {
                if (journal.AccountId.AccountTypeId.AccountCategoryId.Code != "OFFBALANCE")
                {
                    //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---START
                    //TotalDebit += journal.Debit;
                    TotalDebit += journal.Debit * getCoefficientCompareWithDefaultByCurrencyCode(session, journal.CurrencyId.Code);
                    //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---END
                }
            }

            return TotalDebit;
        }
        public double TransactionTotalValue(Session session, Guid TransactionId)
        {
            BalanceForwardTransaction transaction = null;
            try
            {
                transaction = session.GetObjectByKey<BalanceForwardTransaction>(TransactionId);
                if (transaction != null) return transaction.Balance;
            }
            catch (Exception)
            {
            }
            Transaction nTransaction = session.GetObjectByKey<Transaction>(TransactionId);
            if (nTransaction == null) return -0.1234567899876543;
            double TotalDebit = 0;
            double TotalCredit = 0;

            foreach (GeneralJournal journal in nTransaction.GeneralJournals)
            {
                if (journal.AccountId.AccountTypeId.AccountCategoryId.Code != "OFFBALANCE")
                {
                    //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---START
                    //TotalCredit += journal.Credit;
                    //TotalDebit += journal.Debit;
                    TotalCredit += journal.Credit * getCoefficientCompareWithDefaultByCurrencyCode(session, journal.CurrencyId.Code);
                    TotalDebit += journal.Debit * getCoefficientCompareWithDefaultByCurrencyCode(session, journal.CurrencyId.Code);
                    //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---END
                }
            }

            if (TotalDebit == TotalCredit) return TotalCredit;
            return -0.1234567899876543;
        }
        //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
        //public Transaction FindNextTransaction(Session session, DateTime _Time)
        //{
        //    try
        //    {
        //        XPQuery<Transaction> TransactionQuery = session.Query<Transaction>();
        //        Transaction nextTransaction = (from c in TransactionQuery
        //                                       where c.IssueDate >= _Time
        //                                       && c.AccountingPeriodId == AccountingPeriodBO.getCurrentAccountingPeriod(session)
        //                                       && c.RowStatus > 0
        //                                       orderby c.IssueDate ascending
        //                                       select c).FirstOrDefault();
        //        return nextTransaction;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        
        //public Transaction FindPreviousTransaction(Session session, DateTime _Time)
        //{
        //    try
        //    {
        //        XPQuery<Transaction> TransactionQuery = session.Query<Transaction>();
        //        Transaction transaction = (from c in TransactionQuery
        //                                   where c.IssueDate < _Time
        //                                   && c.AccountingPeriodId == AccountingPeriodBO.getCurrentAccountingPeriod(session)
        //                                   && c.RowStatus > 0
        //                                   orderby c.IssueDate descending
        //                                   select c).FirstOrDefault();
        //        return transaction;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END
        public Guid FindNewestTransaction(Session session)
        {
            XPQuery<Transaction> TransactionQuery = session.Query<Transaction>();
            Transaction NewestTransaction = TransactionQuery.OrderByDescending(r => r.IssueDate).FirstOrDefault();
            return NewestTransaction.TransactionId;
        }
        public Guid CreateBookingEntryTransaction(Session session,
                                                    Guid BookingEntryId)
        {
            BookingEntryTransaction transaction = new BookingEntryTransaction(session);
            BookingEntry BKE = session.GetObjectByKey<BookingEntry>(BookingEntryId);

            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
            //Transaction nextTransaction = FindNextTransaction(session, BKE.IssueDate);
            //Transaction previousTransaction = FindPreviousTransaction(session, BKE.IssueDate);
            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END
            transaction.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
            transaction.CreateDate = DateTime.Now;
            transaction.UpdateDate = DateTime.Now;
            transaction.Description = BKE.Description;
            transaction.Code = BKE.Code;
            transaction.BookingEntryId = BKE;
            transaction.IssueDate = BKE.IssueDate;
            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
            //transaction.PreviousTransactionId = (previousTransaction == null) ? Guid.Empty : previousTransaction.TransactionId;
            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END
            transaction.AccountingPeriodId = AccountingPeriodBO.getCurrentAccountingPeriod(session);

            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
            //if (nextTransaction != null)
            //{
            //    nextTransaction.PreviousTransactionId = transaction.TransactionId;
            //    nextTransaction.UpdateDate = DateTime.Now;
            //}
            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END

            transaction.Save();
            return transaction.TransactionId;
        }

        public Guid CreateTransaction(Session session,
                                        String _Code,
                                        string _Description,
                                        DateTime _IssueDate,
                                        short _Rowstatus)
        {
            return CreateTransaction(session, AccountingPeriodBO.getCurrentAccountingPeriod(session).AccountingPeriodId, _Code, _Description, _IssueDate, _Rowstatus);
        }

        public Guid CreateTransaction(Session session,
                                        Guid _AccountPeriodId,
                                        String _Code,
                                        string _Description,
                                        DateTime _IssueDate,
                                        short _Rowstatus)
        {
            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
            //Transaction nextTransaction = FindNextTransaction(session, _IssueDate);
            //Transaction previousTransaction = FindPreviousTransaction(session, _IssueDate);
            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END
            Transaction transaction = new Transaction(session);
            transaction.AccountingPeriodId = session.GetObjectByKey<AccountingPeriod>(_AccountPeriodId);
            transaction.Code = _Code;
            transaction.CreateDate = DateTime.Now;
            transaction.CreateDate = DateTime.Now;
            transaction.Description = _Description;
            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
            //transaction.PreviousTransactionId = (previousTransaction == null) ? Guid.Empty : previousTransaction.TransactionId;
            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END
            transaction.IssueDate = _IssueDate;
            transaction.RowStatus = _Rowstatus;

            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
            //if (nextTransaction != null)
            //{
            //    nextTransaction.PreviousTransactionId = transaction.TransactionId;
            //    nextTransaction.UpdateDate = DateTime.Now;
            //}
            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END
            transaction.Save();

            return transaction.TransactionId;
        }

        public Guid CreateTransactionForArtifact<T>(Session session,
                                        T Artifact,
                                        string _Code,
                                        string _Description)
                                        where T : XPCustomObject
        {
            try
            {
                if (Artifact is SalesInvoice)
                {
                    SalesInvoice salesinvoice = Artifact as SalesInvoice;
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
                    //Transaction nextTransaction = FindNextTransaction(session, salesinvoice.IssuedDate);
                    //Transaction previousTransaction = FindPreviousTransaction(session, salesinvoice.IssuedDate);
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END

                    SaleInvoiceTransaction transaction = new SaleInvoiceTransaction(session);

                    transaction.AccountingPeriodId = AccountingPeriodBO.getCurrentAccountingPeriod(session);
                    transaction.Code = _Code;
                    transaction.CreateDate = DateTime.Now;
                    transaction.CreateDate = DateTime.Now;
                    transaction.Description = _Description;
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
                    //transaction.PreviousTransactionId = (previousTransaction == null) ? Guid.Empty : previousTransaction.TransactionId;
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END
                    transaction.IssueDate = salesinvoice.IssuedDate;
                    transaction.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                    transaction.SalesInvoiceId = Artifact as NAS.DAL.Invoice.SalesInvoice;

                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
                    //if (nextTransaction != null)
                    //{
                    //    nextTransaction.PreviousTransactionId = transaction.TransactionId;
                    //    nextTransaction.UpdateDate = DateTime.Now;
                    //}
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END

                    transaction.Save();
                    return transaction.TransactionId;

                }
                if (Artifact is NAS.DAL.Invoice.PurchaseInvoice)
                {
                    NAS.DAL.Invoice.PurchaseInvoice purchaseInvoice = Artifact as NAS.DAL.Invoice.PurchaseInvoice;
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
                    //Transaction nextTransaction = FindNextTransaction(session, purchaseInvoice.IssuedDate);
                    //Transaction previousTransaction = FindPreviousTransaction(session, purchaseInvoice.IssuedDate);
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END

                    PurchaseInvoiceTransaction transaction = new PurchaseInvoiceTransaction(session);

                    transaction.AccountingPeriodId = AccountingPeriodBO.getCurrentAccountingPeriod(session);
                    transaction.Code = _Code;
                    transaction.CreateDate = DateTime.Now;
                    transaction.CreateDate = DateTime.Now;
                    transaction.Description = _Description;
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
                    //transaction.PreviousTransactionId = (previousTransaction == null) ? Guid.Empty : previousTransaction.TransactionId;
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END
                    transaction.IssueDate = purchaseInvoice.IssuedDate;
                    transaction.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                    transaction.PurchaseInvoiceId = Artifact as NAS.DAL.Invoice.PurchaseInvoice;

                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
                    //if (nextTransaction != null)
                    //{
                    //    nextTransaction.PreviousTransactionId = transaction.TransactionId;
                    //    nextTransaction.UpdateDate = DateTime.Now;
                    //}
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END

                    transaction.Save();

                    return transaction.TransactionId;
                }
                if (Artifact is NAS.DAL.Vouches.PaymentVouches)
                {
                    NAS.DAL.Vouches.PaymentVouches paymentVouches = Artifact as NAS.DAL.Vouches.PaymentVouches;

                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
                    //Transaction nextTransaction = FindNextTransaction(session, paymentVouches.IssuedDate);
                    //Transaction previousTransaction = FindPreviousTransaction(session, paymentVouches.IssuedDate);
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END

                    PaymentVouchesTransaction transaction = new PaymentVouchesTransaction(session);

                    transaction.AccountingPeriodId = AccountingPeriodBO.getCurrentAccountingPeriod(session);
                    transaction.Code = _Code;
                    transaction.CreateDate = DateTime.Now;
                    transaction.CreateDate = DateTime.Now;
                    transaction.Description = _Description;
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
                    //transaction.PreviousTransactionId = (previousTransaction == null) ? Guid.Empty : previousTransaction.TransactionId;
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END
                    transaction.IssueDate = paymentVouches.IssuedDate;
                    transaction.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                    transaction.PaymentVouchesId = Artifact as NAS.DAL.Vouches.PaymentVouches;

                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
                    //if (nextTransaction != null)
                    //{
                    //    nextTransaction.PreviousTransactionId = transaction.TransactionId;
                    //    nextTransaction.UpdateDate = DateTime.Now;
                    //}
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END

                    transaction.Save();

                    return transaction.TransactionId;
                }
                if (Artifact is NAS.DAL.Vouches.ReceiptVouches)
                {
                    NAS.DAL.Vouches.ReceiptVouches receiptVouches = Artifact as NAS.DAL.Vouches.ReceiptVouches;
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
                    //Transaction nextTransaction = FindNextTransaction(session, receiptVouches.IssuedDate);
                    //Transaction previousTransaction = FindPreviousTransaction(session, receiptVouches.IssuedDate);
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END

                    ReceiptVouchesTransaction transaction = new ReceiptVouchesTransaction(session);

                    transaction.AccountingPeriodId = AccountingPeriodBO.getCurrentAccountingPeriod(session);
                    transaction.Code = _Code;
                    transaction.CreateDate = DateTime.Now;
                    transaction.CreateDate = DateTime.Now;
                    transaction.Description = _Description;
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
                    //transaction.PreviousTransactionId = (previousTransaction == null) ? Guid.Empty : previousTransaction.TransactionId;
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END
                    transaction.IssueDate = receiptVouches.IssuedDate;
                    transaction.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                    transaction.ReceiptVouchesId = Artifact as NAS.DAL.Vouches.ReceiptVouches;

                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
                    //if (nextTransaction != null)
                    //{
                    //    nextTransaction.PreviousTransactionId = transaction.TransactionId;
                    //    nextTransaction.UpdateDate = DateTime.Now;
                    //}
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END

                    transaction.Save();

                    return transaction.TransactionId;
                }
                if (Artifact is NAS.DAL.Inventory.Journal.PurchaseInvoiceInventoryTransaction)
                {
                    NAS.DAL.Inventory.Journal.PurchaseInvoiceInventoryTransaction purchaseInvoiceInventoryTransaction = Artifact as NAS.DAL.Inventory.Journal.PurchaseInvoiceInventoryTransaction;
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
                    //Transaction nextTransaction = FindNextTransaction(session, purchaseInvoiceInventoryTransaction.IssueDate);
                    //Transaction previousTransaction = FindPreviousTransaction(session, purchaseInvoiceInventoryTransaction.IssueDate);
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END

                    PurchaseInvoiceInventoryAccountingTransaction transaction = new PurchaseInvoiceInventoryAccountingTransaction(session);

                    transaction.AccountingPeriodId = AccountingPeriodBO.getCurrentAccountingPeriod(session);
                    transaction.Code = _Code;
                    transaction.CreateDate = DateTime.Now;
                    transaction.CreateDate = DateTime.Now;
                    transaction.Description = _Description;
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
                    //transaction.PreviousTransactionId = (previousTransaction == null) ? Guid.Empty : previousTransaction.TransactionId;
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END
                    transaction.IssueDate = purchaseInvoiceInventoryTransaction.IssueDate;
                    transaction.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                    transaction.PurchaseInvoiceInventoryTransactionId = Artifact as NAS.DAL.Inventory.Journal.PurchaseInvoiceInventoryTransaction;

                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
                    //if (nextTransaction != null)
                    //{
                    //    nextTransaction.PreviousTransactionId = transaction.TransactionId;
                    //    nextTransaction.UpdateDate = DateTime.Now;
                    //}
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END

                    transaction.Save();

                    return transaction.TransactionId;
                }
                if (Artifact is NAS.DAL.Inventory.Journal.SalesInvoiceInventoryTransaction)
                {
                    NAS.DAL.Inventory.Journal.SalesInvoiceInventoryTransaction purchaseInvoiceInventoryTransaction = Artifact as NAS.DAL.Inventory.Journal.SalesInvoiceInventoryTransaction;
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
                    //Transaction nextTransaction = FindNextTransaction(session, purchaseInvoiceInventoryTransaction.IssueDate);
                    //Transaction previousTransaction = FindPreviousTransaction(session, purchaseInvoiceInventoryTransaction.IssueDate);
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END

                    SalesInvoiceInventoryAccountingTransaction transaction = new SalesInvoiceInventoryAccountingTransaction(session);

                    transaction.AccountingPeriodId = AccountingPeriodBO.getCurrentAccountingPeriod(session);
                    transaction.Code = _Code;
                    transaction.CreateDate = DateTime.Now;
                    transaction.CreateDate = DateTime.Now;
                    transaction.Description = _Description;
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
                    //transaction.PreviousTransactionId = (previousTransaction == null) ? Guid.Empty : previousTransaction.TransactionId;
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END
                    transaction.IssueDate = purchaseInvoiceInventoryTransaction.IssueDate;
                    transaction.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                    transaction.SalesInvoiceInventoryTransactionId = Artifact as NAS.DAL.Inventory.Journal.SalesInvoiceInventoryTransaction;

                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
                    //if (nextTransaction != null)
                    //{
                    //    nextTransaction.PreviousTransactionId = transaction.TransactionId;
                    //    nextTransaction.UpdateDate = DateTime.Now;
                    //}
                    //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END

                    transaction.Save();

                    return transaction.TransactionId;
                }
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
            return Guid.Empty;
        }

        public Guid CreateBalanceForwardTransaction(Session session,
                                        String _Code,
                                        string _Description,
                                        DateTime _IssueDate,
                                        double _Balance)
        {
            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
            //Transaction nextTransaction = FindNextTransaction(session, _IssueDate);
            //Transaction previousTransaction = FindPreviousTransaction(session, _IssueDate);
            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END
            BalanceForwardTransaction transaction = new BalanceForwardTransaction(session);
            transaction.AccountingPeriodId = AccountingPeriodBO.getCurrentAccountingPeriod(session);
            transaction.Code = _Code;
            transaction.CreateDate = DateTime.Now;
            transaction.UpdateDate = DateTime.Now;
            transaction.Description = _Description;
            transaction.IssueDate = _IssueDate;
            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
            //transaction.PreviousTransactionId = (previousTransaction == null) ? Guid.Empty : previousTransaction.TransactionId;
            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END
            transaction.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
            transaction.Balance = _Balance;

            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
            //if (nextTransaction != null)
            //{
            //    nextTransaction.PreviousTransactionId = transaction.TransactionId;
            //    nextTransaction.UpdateDate = DateTime.Now;
            //}
            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END

            transaction.Save();

            return transaction.TransactionId;
        }

        public Guid CreateBalanceForwardTransaction(Session session,
                                        string _Description,
                                        DateTime _IssueDate,
                                        double _Balance)
        {
            return CreateBalanceForwardTransaction(session, "BI_" + AccountingPeriodBO.getCurrentAccountingPeriod(session).Code, _Description, _IssueDate, _Balance);
        }

        public TranDes TransactionFullDes(Session session, Guid _TransactionId)
        {
            Transaction transaction = session.GetObjectByKey<Transaction>(_TransactionId);
            double debit = 0;
            double credit = 0;
            TranDes Tranresult = new TranDes("", 0);
            //transaction.GeneralJournals
            //CriteriaOperator criteria2 = new BinaryOperator("TransactionId", transaction, BinaryOperatorType.Equal);
            //XPCollection<GeneralJournal> gjournal = new XPCollection<GeneralJournal>(session, criteria2);
            if (transaction == null)
            {
                Tranresult.Description = "Giao dịch không tồn tại";
                return Tranresult;
            }
            foreach (GeneralLedger gl in transaction.GeneralLedgers)
            {
                if (gl.IsOriginal)
                {
                    debit += gl.Debit;
                    credit += gl.Credit;
                }
            }

            if (debit != credit && transaction.Description != "Nhập đầu kì" && !(transaction is BalanceForwardTransaction))
            {
                Tranresult.Description = "Số dư nợ và có khác nhau";
                Tranresult.Balance = 0;
            }
            else
            {
                Tranresult.Balance = debit > credit ? debit : credit;
            }
            if (transaction is BalanceForwardTransaction)
            {
                BalanceForwardTransaction bltransaction = session.GetObjectByKey<BalanceForwardTransaction>(_TransactionId);
                Tranresult.Balance = bltransaction.Balance;
            }

            return Tranresult;
        }

        //public Guid CreatBalanceForwardTransaction(Session session,
        //                                Guid _AccountPeriodId,
        //                                String _Code,
        //                                string _Description,
        //                                DateTime _IssueDate,
        //                                short _Rowstatus,
        //                                double _Balance)
        //{
        //    Transaction nextTransaction = FindNextTransaction(session, _IssueDate);
        //    Transaction previousTransaction = FindPreviousTransaction(session, _IssueDate);

        //    BalanceForwardTransaction transaction = new BalanceForwardTransaction(session);
        //    transaction.AccountingPeriodId = session.GetObjectByKey<AccountingPeriod>(_AccountPeriodId);
        //    transaction.Code = _Code;
        //    transaction.CreateDate = DateTime.Now;
        //    transaction.Description = _Description;
        //    transaction.IssueDate = _IssueDate;
        //    transaction.RowStatus = _Rowstatus;
        //    transaction.Balance = _Balance;

        //    transaction.Save();

        //    return transaction.TransactionId;
        //}
        public List<object> GetFullDesTransactionList(Session session)
        {
            List<object> resultList = new List<object>();
            XPQuery<Transaction> transaction = session.Query<Transaction>();
            XPQuery<GeneralLedger> ledger = session.Query<GeneralLedger>();
            //var listObj = from acc in account
            //              select new { CreatDate = CurrentLedger(session, acc.AccountId).CreateDate, Code = acc.Code, Debit = (CurrentLedger(session, acc.AccountId).GeneralJournalId == null) ? 0 : CurrentLedger(session, acc.AccountId).GeneralJournalId.Debit, Credit = (CurrentLedger(session, acc.AccountId).GeneralJournalId == null) ? 0 : CurrentLedger(session, acc.AccountId).GeneralJournalId.Credit, Balance = CurrentLedger(session, acc.AccountId).Balance };
            //var ls = TransactionFullDes(session, transaction.Where(r=>r.RowStatus >0).FirstOrDefault().TransactionId);
            var listObj = from tran in transaction
                          where tran.AccountingPeriodId == AccountingPeriodBO.getCurrentAccountingPeriod(session)
                          && tran.RowStatus > 0
                          select new
                          {
                              ID = tran.TransactionId,
                              CreatDate = tran.IssueDate,
                              Code = tran.Code,
                              Description = tran.Description,
                              Total = TransactionFullDes(session, tran.TransactionId).Balance,
                              Note = TransactionFullDes(session, tran.TransactionId).Description
                          };

            //where (lger.AccountId.Code != "NAAN_DEFAULT")
            //select new
            //{
            //    CreatDate = lger.CreateDate,
            //    Description = (lger.GeneralJournalId == null) ? "" : ((lger.GeneralJournalId.TransactionId == null) ? "" : lger.GeneralJournalId.TransactionId.Description),
            //    Code = lger.AccountId.Code,
            //    Balance = lger.Balance,
            //    Credit = lger.GeneralJournalId.Credit,
            //    Debit = lger.GeneralJournalId.Debit,
            //    JournalCode = (lger.GeneralJournalId == null) ? "" : ((lger.GeneralJournalId.TransactionId == null) ? "" : lger.GeneralJournalId.TransactionId.Code)
            //};
            try
            {
                foreach (var o in listObj)
                {
                    if (IsApproved(session, o.ID) == true)
                    {
                        resultList.Add(o);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message.ToString());
            }

            return resultList;
        }

        public bool IsExistTransactionCode(Session session, String Code)
        {
            bool result = false;
            XPQuery<Transaction> transactionQuery = new XPQuery<Transaction>(session);
            var templist = (from c in transactionQuery
                            where c.Code == Code
                            && c.RowStatus >= Constant.ROWSTATUS_ACTIVE
                            select c);
            if (templist.Count() > 0) result = true;
            return result;
        }

        //GeneralJournal-----------------------------------
        public Guid CreateGeneralJournal(Session session,
                                            Guid _TransactionId,
                                            Guid _AccountID,
                                            char _JournalType,
                                            string _Description,
                                            double _Debit,
                                            double _Credit,
                                            short _RowStatus,
                                            Guid _CurrencyId)
        {

            GeneralJournal generalJournal = new GeneralJournal(session);

            generalJournal.TransactionId = session.GetObjectByKey<Transaction>(_TransactionId);
            generalJournal.AccountId = session.GetObjectByKey<Account>(_AccountID);
            generalJournal.CurrencyId = session.GetObjectByKey<Currency>(_CurrencyId);
            generalJournal.JournalType = _JournalType;
            generalJournal.Description = _Description;
            generalJournal.CreateDate = DateTime.Now;
            generalJournal.Debit = _Debit;
            generalJournal.Credit = _Credit;
            generalJournal.RowStatus = _RowStatus;

            generalJournal.Save();

            return generalJournal.GeneralJournalId;
        }
        public Guid CreateGeneralJournal(Session session,
                                            Guid _TransactionId,
                                            Guid _AccountID,
                                            string _Description,
                                            double _Debit,
                                            double _Credit,
                                            short _RowStatus)
        {

            GeneralJournal generalJournal = new GeneralJournal(session);

            generalJournal.TransactionId = session.GetObjectByKey<Transaction>(_TransactionId);
            generalJournal.AccountId = session.GetObjectByKey<Account>(_AccountID);
            generalJournal.Description = _Description;
            generalJournal.Debit = _Debit;
            generalJournal.Credit = _Credit;
            generalJournal.RowStatus = _RowStatus;

            generalJournal.Save();

            return generalJournal.GeneralJournalId;
        }

        public Guid CreateGeneralJournalBalanceForward(Session session,
                                            Guid _TransactionId,
                                            Guid _AccountID,
                                            string _Description,
                                            double _Balance,
                                            short _RowStatus)
        {
            Account account = session.GetObjectByKey<Account>(_AccountID);
            GeneralJournalBalanceForward generalJournal = new GeneralJournalBalanceForward(session);
            BalanceForwardTransaction transaction = session.GetObjectByKey<BalanceForwardTransaction>(_TransactionId);
            transaction.Code = transaction.Code + "_" + account.Code;
            generalJournal.TransactionId = transaction;
            generalJournal.AccountId = account;
            generalJournal.Description = _Description;
            generalJournal.Debit = _Balance * ((account.BalanceType == Utility.Constant.BALANCE_TYPE_CREDIT) ? 0 : 1);
            generalJournal.Credit = _Balance * ((account.BalanceType == Utility.Constant.BALANCE_TYPE_CREDIT) ? 1 : 0);
            generalJournal.Balance = _Balance;
            generalJournal.RowStatus = _RowStatus;
            //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---START
            NAS.DAL.Accounting.Currency.Currency defaultCurrency = getDefaulCurrency(session);
            generalJournal.CurrencyId = defaultCurrency;
            //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---END
            transaction.Save();
            generalJournal.Save();
            //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---START
            //CreateGeneralLedger(session, account.AccountId, _TransactionId, "Nhập đầu kì", generalJournal.Debit, generalJournal.Credit, true, 0);
            CreateGeneralLedger(session, account.AccountId, _TransactionId, "Nhập đầu kì", generalJournal.Debit, generalJournal.Credit, generalJournal.CurrencyId.Code, true, 0);
            //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---END
            return generalJournal.GeneralJournalId;
        }

        //GeneralLedger------------------------------------

        public void CreateGeneralLedger(Session session,
                                        Guid _AccountId,
                                        Guid _TransactionId,
                                        string _Description,
                                        double _Debit,
                                        double _Credit,
        //--------11/12/2013 Duc.Vo ---- ADD - Issue E-1185---START
                                        string _CurrencyCode,
        //--------11/12/2013 Duc.Vo ---- ADD - Issue E-1185---END
                                        bool _IsOriginal,
                                        double milsec)
        {
            //Get Parameter for Ledger
            Account account = session.GetObjectByKey<Account>(_AccountId);
            Transaction transaction = session.GetObjectByKey<Transaction>(_TransactionId);

            //Get Newest Ledger of Account
            //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---START
            XPQuery<GeneralLedger> GeneralLedgerQuery = session.Query<GeneralLedger>();
            Guid PreviousGeneralLedgerId = GetPreviousGeneralLedger(session, _AccountId, transaction.IssueDate);
            GeneralLedger PreviousGeneralLedger = null;
            try
            {
                PreviousGeneralLedger = session.GetObjectByKey<GeneralLedger>(PreviousGeneralLedgerId);
            }
            catch (Exception)
            {
            }
            //if (GeneralLedgerQuery != null) PreviousGeneralLedger = (from c in GeneralLedgerQuery
            //                                                         where c.AccountId.AccountId == _AccountId
            //                                                         && c.TransactionId.AccountingPeriodId == AccountingPeriodBO.getCurrentAccountingPeriod(session)
            //                                                         && c.TransactionId.IssueDate < transaction.IssueDate
            //                                                         orderby c.TransactionId.IssueDate descending
            //                                                                ,c.CreateDate ascending    
            //                                                                ,c.UpdateDate descending
            //                                                         select c).FirstOrDefault();
            //Check Balance Type of Account

            //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---START
            //-----------coefficient -> Sign---------------------------
            //double coefficient = 1;
            //if (account.BalanceType == Utility.Constant.BALANCE_TYPE_CREDIT)
            //{
            //    coefficient = -1;
            //}
            ////Calculate New Balance for Account
            //double _Balance = 0;
            //if (PreviousGeneralLedger == null)
            //{
            //    _Balance = (_Debit - _Credit) * coefficient;
            //}
            //else
            //{
            //    _Balance = PreviousGeneralLedger.Balance + (_Debit - _Credit) * coefficient;
            //}
            double sign = 1;
            if (account.BalanceType == Utility.Constant.BALANCE_TYPE_CREDIT)
            {
                sign = -1;
            }
            //Calculate New Balance for Account
            double _Balance = 0;
            double _coefficient = 0;

            _coefficient = getCoefficientCompareWithDefaultByCurrencyCode(session, _CurrencyCode);


            if (PreviousGeneralLedger == null)
            {
                _Balance = (_Debit - _Credit) * sign * _coefficient;
            }
            else
            {
                _Balance = PreviousGeneralLedger.Balance + (_Debit - _Credit) * sign * _coefficient;
            }
            // End Get Parameter for Ledger
            //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---END

            GeneralLedger generalLedger = new GeneralLedger(session);
            generalLedger.AccountId = account;
            generalLedger.TransactionId = transaction;
            generalLedger.CreateDate = DateTime.Now;
            generalLedger.UpdateDate = DateTime.Now.AddMilliseconds(milsec);
            generalLedger.IssuedDate = transaction.IssueDate;
            generalLedger.Description = _Description;
            generalLedger.Debit = _Debit;
            generalLedger.Credit = _Credit;
            generalLedger.Balance = _Balance;
            generalLedger.IsOriginal = _IsOriginal;
            generalLedger.Save();
            //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---START
            //DownRepairLedger(session, generalLedger.GeneralLedgerId, _Debit, _Credit);
            DownRepairLedger(session, generalLedger.GeneralLedgerId, _Debit, _Credit, _CurrencyCode);
            //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---END

            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---START
            //if (account.ParentAccountId != null)
            //{
            //    CreateGeneralLedger(session, account.ParentAccountId.AccountId, _TransactionId, _Description, _Debit, _Credit, false, 0);
            //}
            //--------11/12/2013 Duc.Vo ---- DEL - Issue E-1185---END
        }
        //public void CreateGeneralLedger(Session session,
        //                                Guid _AccountId,
        //                                Guid _TransactionId,
        //                                string _Description,
        //                                double _Debit,
        //                                double _Credit,
        //                                bool _IsOriginal)
        //{
        //    //Get Parameter for Ledger
        //    Account         account   = session.GetObjectByKey<Account>(_AccountId);
        //    Transaction  transaction  = session.GetObjectByKey<Transaction>(_TransactionId);

        //    //Get Newest Ledger of Account

        //    XPQuery<GeneralLedger> GeneralLedgerQuery = session.Query<GeneralLedger>();
        //    Guid PreviousGeneralLedgerId = GetPreviousGeneralLedger(session, _AccountId, transaction.IssueDate);
        //    GeneralLedger PreviousGeneralLedger = null;
        //    try
        //    {
        //        PreviousGeneralLedger = session.GetObjectByKey<GeneralLedger>(PreviousGeneralLedgerId);
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    //if (GeneralLedgerQuery != null) PreviousGeneralLedger = (from c in GeneralLedgerQuery
        //    //                                                         where c.AccountId.AccountId == _AccountId
        //    //                                                         && c.TransactionId.AccountingPeriodId == AccountingPeriodBO.getCurrentAccountingPeriod(session)
        //    //                                                         && c.TransactionId.IssueDate < transaction.IssueDate
        //    //                                                         orderby c.TransactionId.IssueDate descending
        //    //                                                                ,c.CreateDate ascending    
        //    //                                                                ,c.UpdateDate descending
        //    //                                                         select c).FirstOrDefault();
        //        //Check Balance Type of Account
        //    double coefficient = 1;
        //    if(account.BalanceType == Utility.Constant.BALANCE_TYPE_CREDIT)
        //    {
        //        coefficient = -1;
        //    }
        //        //Calculate New Balance for Account
        //    double _Balance = 0;
        //    if (PreviousGeneralLedger == null)
        //    {
        //        _Balance = (_Debit - _Credit) * coefficient;
        //    }
        //    else{
        //        _Balance = PreviousGeneralLedger.Balance + (_Debit - _Credit) * coefficient;
        //    }
        //    // End Get Parameter for Ledger

        //    GeneralLedger generalLedger = new GeneralLedger(session);

        //    generalLedger.AccountId = account;
        //    generalLedger.TransactionId = transaction;            
        //    generalLedger.CreateDate = DateTime.Now;
        //    generalLedger.UpdateDate = DateTime.Now;
        //    generalLedger.Description = _Description;
        //    generalLedger.Debit = _Debit;
        //    generalLedger.Credit = _Credit;
        //    generalLedger.Balance = _Balance;
        //    generalLedger.IsOriginal = _IsOriginal;
        //    generalLedger.Save();
        //    DownRepairLedger(session, generalLedger.GeneralLedgerId,_Debit,_Credit);            
        //    //Recursive Ledger
        //    if (account.ParentAccountId != null)
        //    {                
        //        CreateGeneralLedger(session, account.ParentAccountId.AccountId,_TransactionId,_Description,_Debit,_Credit,false);
        //    }
        //}
        public void CreateGeneralLedger(Session session,
                                Guid TransactionId,
                                Guid GeneralJournalId, double milisecond)
        {
            GeneralJournal journal = session.GetObjectByKey<GeneralJournal>(GeneralJournalId);
            Transaction transaction = session.GetObjectByKey<Transaction>(TransactionId);
            if ((journal == null) || (transaction == null)) return;
            //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---START
            //CreateGeneralLedger(session, journal.AccountId.AccountId, TransactionId, journal.Description, journal.Debit, journal.Credit, true, milisecond);
            CreateGeneralLedger(session, journal.AccountId.AccountId, TransactionId, journal.Description, journal.Debit, journal.Credit, journal.CurrencyId.Code, true, milisecond);
            //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---END
            
        }

        //public void CreateGeneralLedger(Session session,
        //                                Guid TransactionId,
        //                                Guid GeneralJournalId)
        //{
        //    GeneralJournal journal = session.GetObjectByKey<GeneralJournal>(GeneralJournalId);
        //    Transaction transaction = session.GetObjectByKey<Transaction>(TransactionId);
        //    if ((journal == null) || (transaction == null)) return;

        //    CreateGeneralLedger(session, journal.AccountId.AccountId, TransactionId, journal.Description, journal.Debit, journal.Credit, true);
        //}

        public Guid GetPreviousGeneralLedger(Session session, Guid AccountId, DateTime IssueDate)
        {
            try
            {
                Account account = session.GetObjectByKey<Account>(AccountId);
                XPQuery<GeneralLedger> LedgerQuery = session.Query<GeneralLedger>();
                GeneralLedger resultLedger = (from c in LedgerQuery
                                              where c.TransactionId.AccountingPeriodId == AccountingPeriodBO.getCurrentAccountingPeriod(session)
                                              && c.IssuedDate <= IssueDate
                                              && c.AccountId == account
                                              orderby c.IssuedDate descending
                                              , c.UpdateDate descending
                                              select c).FirstOrDefault();
                return resultLedger.GeneralLedgerId;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        public List<object> GetNextGeneralLedgerList(Session session, Guid AccountId, DateTime IssueDate)
        {
            try
            {
                Account account = session.GetObjectByKey<Account>(AccountId);
                XPQuery<GeneralLedger> LedgerQuery = session.Query<GeneralLedger>();
                var tempList = (from c in LedgerQuery
                                where c.TransactionId.AccountingPeriodId == AccountingPeriodBO.getCurrentAccountingPeriod(session)
                                && c.TransactionId.IssueDate >= IssueDate
                                && c.AccountId == account
                                select c);

                List<object> resultList = new List<object>();
                try
                {
                    foreach (var o in tempList)
                    {
                        resultList.Add(o);
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message.ToString());
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        public GeneralLedger NewestLedger(Session session, Guid _AccountId)
        {
            try
            {
                Account account = session.GetObjectByKey<Account>(_AccountId);
                XPQuery<GeneralLedger> LedgerQuery = session.Query<GeneralLedger>();
                GeneralLedger newestLedger = (from c in LedgerQuery
                                              where c.AccountId.AccountId == _AccountId
                                              orderby c.TransactionId.IssueDate descending
                                                     , c.UpdateDate descending
                                              select c).FirstOrDefault();
                return newestLedger;
            }
            catch (Exception)
            {
            }
            return null;
        }

        public void DownRepairLedger(Session 
            session, Guid _GeneralLedgerId, 
            double Debit, 
            double Credit,
        //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---START
            string CurrencyCode)
        //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---START
        {
            GeneralLedger CurrentLedger = session.GetObjectByKey<GeneralLedger>(_GeneralLedgerId);
            Account account = CurrentLedger.AccountId;
            //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---START
            double coefficient = getCoefficientCompareWithDefaultByCurrencyCode(session, CurrencyCode);
            double sign = 1;
            //--------11/12/2013 Duc.Vo ---- MOD - Issue E-1185---END
            sign = (account.BalanceType == Utility.Constant.BALANCE_TYPE_CREDIT) ? -1 : 1;
            double deltaBal = (Debit - Credit) * sign * coefficient;

            XPQuery<GeneralLedger> GeneralLedgerQuery = session.Query<GeneralLedger>();

            var ledgerlist = (from c in GeneralLedgerQuery
                              where c.AccountId == CurrentLedger.AccountId
                              && c.TransactionId.AccountingPeriodId == AccountingPeriodBO.getCurrentAccountingPeriod(session)
                              && c.IssuedDate > CurrentLedger.IssuedDate
                              && c.GeneralLedgerId != CurrentLedger.GeneralLedgerId
                              orderby c.IssuedDate ascending
                              , c.UpdateDate ascending
                              select c);
            double milsec = 0;
            if (CurrentLedger.TransactionId is BalanceForwardTransaction)
            {
                ledgerlist = (from c in GeneralLedgerQuery
                              where c.AccountId == CurrentLedger.AccountId
                              && c.TransactionId.AccountingPeriodId == AccountingPeriodBO.getCurrentAccountingPeriod(session)
                              && c.IssuedDate == CurrentLedger.IssuedDate
                              && c.CreateDate > CurrentLedger.CreateDate
                              && c.GeneralLedgerId != CurrentLedger.GeneralLedgerId
                              orderby c.IssuedDate ascending
                              , c.UpdateDate ascending
                              select c);
                foreach (GeneralLedger ledger in ledgerlist)
                {
                    GeneralLedger updateledger = session.GetObjectByKey<GeneralLedger>(ledger.GeneralLedgerId);
                    updateledger.Balance += deltaBal;
                    updateledger.UpdateDate = DateTime.Now.AddMilliseconds(milsec);
                    updateledger.Save();
                    milsec += 10;
                }
            }
            ledgerlist = (from c in GeneralLedgerQuery
                          where c.AccountId == CurrentLedger.AccountId
                          && c.TransactionId.AccountingPeriodId == AccountingPeriodBO.getCurrentAccountingPeriod(session)
                          && c.IssuedDate > CurrentLedger.IssuedDate
                          && c.GeneralLedgerId != CurrentLedger.GeneralLedgerId
                          orderby c.IssuedDate ascending
                          , c.UpdateDate ascending
                          select c);
            foreach (GeneralLedger ledger in ledgerlist)
            {
                GeneralLedger updateledger = session.GetObjectByKey<GeneralLedger>(ledger.GeneralLedgerId);
                updateledger.Balance += deltaBal;
                updateledger.UpdateDate = DateTime.Now.AddMilliseconds(milsec);
                updateledger.Save();
                milsec += 10;
            }

            ////Find next GeneralLedger
            //GeneralLedger nextLeger = null;
            //if(GeneralLedgerQuery!=null) 
            //    nextLeger =(from c in GeneralLedgerQuery
            //                where c.AccountId == CurrentLedger.AccountId
            //                && c.TransactionId.IssueDate >= CurrentLedger.TransactionId.IssueDate
            //                && c.GeneralLedgerId != CurrentLedger.GeneralLedgerId
            //                orderby c.UpdateDate descending
            //                select c).FirstOrDefault();
            //if(nextLeger == null) return;
            //nextLeger.Balance += deltaBal;
            //nextLeger.UpdateDate = DateTime.Now;
            //nextLeger.Save();
            //DownRepairLedger(session, nextLeger.GeneralLedgerId, Debit, Credit);
        }

        public List<object> getFullLedgerList(Session session)
        {
            XPQuery<GeneralLedger> GeneralLedgerQuery = session.Query<GeneralLedger>();
            var tempList = (from c in GeneralLedgerQuery
                            where c.IsOriginal == true
                            && c.TransactionId.AccountingPeriodId == AccountingPeriodBO.getCurrentAccountingPeriod(session)
                            orderby c.IssuedDate ascending
                            , c.UpdateDate ascending
                            select new
                            {
                                CreatDate = c.TransactionId.IssueDate,
                                AccountCode = c.AccountId.Code,
                                Credit = c.Credit,
                                Description = c.Description,
                                Debit = c.Debit,
                                TransactionCode = c.TransactionId.Code,
                                Balance = c.Balance
                            });
            List<object> resultList = new List<object>();
            try
            {
                foreach (var o in tempList)
                {
                    resultList.Add(o);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message.ToString());
            }

            return resultList;

        }

        //Balance Sheet--------------------------------------
        public static bool IsParentAccount(Session session, Guid AccountId)
        {
            XPQuery<Account> accountQuery = session.Query<Account>();
            var childlist = (from c in accountQuery
                             where c.ParentAccountId.AccountId == AccountId
                             && c.RowStatus >= Constant.ROWSTATUS_ACTIVE
                             select c);
            if (childlist.Count() != 0)
            {
                return true;
            }
            return false;
        }

        public static List<object> getInittableAccountList(Session session)
        {
            XPQuery<Account> accountQuery = session.Query<Account>();
            List<object> resultlist = new List<object>();
            foreach (Account account in accountQuery)
            {
                if (IsParentAccount(session, account.AccountId) == false && account.RowStatus >= Constant.ROWSTATUS_ACTIVE && account.BalanceType != -2
                    && account.AccountTypeId.AccountCategoryId.Code != "REVENUE"
                    && account.AccountTypeId.AccountCategoryId.Code != "EXPENSE"
                    && account.AccountTypeId.AccountCategoryId.Code != "NETINCOME")
                {
                    resultlist.Add(account);
                }
            }
            return resultlist;
        }

        public static List<object> getNotParentAccountList(Session session)
        {
            XPQuery<Account> accountQuery = session.Query<Account>();
            List<object> resultlist = new List<object>();
            foreach (Account account in accountQuery.OrderBy(c => c.Code))
            {
                if (IsParentAccount(session, account.AccountId) == false && account.RowStatus >= Constant.ROWSTATUS_ACTIVE)
                {
                    resultlist.Add(account);
                }
            }
            return resultlist;
        }

        public XPCollection<Account> getLeafAccounts(Session session)
        {
            CriteriaOperator rowStatusCriteria =
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE);
            CriteriaOperator parentAccountsCriteria =
                CriteriaOperator.And(
                    rowStatusCriteria,
                    new UnaryOperator(UnaryOperatorType.Not,
                        new UnaryOperator(UnaryOperatorType.IsNull, "ParentAccountId")));
            XPCollection<Account> hasParentAccounts = new XPCollection<Account>(session, parentAccountsCriteria);
            List<Guid> parentAccountsIds = hasParentAccounts.Select(r => r.ParentAccountId.AccountId).ToList();
            CriteriaOperator leafAccountCriteria = CriteriaOperator.Or(
                    new BinaryOperator("AccountId", Account.GetDefault(session, DefaultAccountEnum.NAAN_DEFAULT)),
                    CriteriaOperator.And(
                        rowStatusCriteria, 
                        new NotOperator(new InOperator("AccountId", parentAccountsIds)))
                );
            XPCollection<Account> leafAccounts = new XPCollection<Account>(session, leafAccountCriteria);
            leafAccounts.Sorting = new SortingCollection(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));
            return leafAccounts;
        }

        public static XPCollection<Account> getNotParentAccountCollection(Session session)
        {
            CriteriaOperator filter = new BinaryOperator("Code", "-aaa", BinaryOperatorType.Equal);
            XPCollection<Account> collection = new XPCollection<NAS.DAL.Accounting.AccountChart.Account>(session, filter);

            XPQuery<Account> accountQuery = session.Query<Account>();
            foreach (Account account in accountQuery.OrderBy(c => c.Code))
            {
                if (IsParentAccount(session, account.AccountId) == false && account.RowStatus >= Constant.ROWSTATUS_ACTIVE)
                {
                    collection.Add(account);
                }
            }

            return collection;

        }

        public List<object> getLedgerListByAccountCategory(Session session, string AccountCategoryName)
        {
            XPQuery<AccountCategory> AccountCategoryQuery = session.Query<AccountCategory>();
            AccountCategory accCate = (from c in AccountCategoryQuery
                                       where c.Code == AccountCategoryName && c.RowStatus >= Constant.ROWSTATUS_ACTIVE
                                       select c).FirstOrDefault();
            return getLedgerListByAccountCategory(session, accCate.AccountCategoryId);
        }

        public List<object> getLedgerListByAccountCategory(Session session, Guid AccountCategoryId)
        {
            General gn = new General();
            List<object> resultlist = new List<object>();
            XPQuery<AccountType> AccountTypeQuery = session.Query<AccountType>();
            XPQuery<Account> AccountQuery = session.Query<Account>();
            var tempList = (from c in AccountQuery
                            where c.AccountTypeId.AccountCategoryId.AccountCategoryId == AccountCategoryId
                                // && c.Level == 1
                            && c.RowStatus >= Constant.ROWSTATUS_ACTIVE

                            select new
                            {
                                AccountId = c.AccountId,
                                ParentAccountId = c.ParentAccountId.AccountId,
                                Code = c.Code,
                                Name = c.Name,
                                Balance = General.AccountBalance(session, c)
                            });
            try
            {
                foreach (var o in tempList)
                {
                    resultlist.Add(o);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return resultlist;
        }

        //public List<object> GetLedgerList(Session session)
        //{
        //    List<object> resultList= new List<object>();
        //    XPQuery<Account> account = session.Query<Account>();
        //    XPQuery<GeneralLedger> ledger = session.Query<GeneralLedger>();
        //    //var listObj = from acc in account
        //    //              select new { CreatDate = CurrentLedger(session, acc.AccountId).CreateDate, Code = acc.Code, Debit = (CurrentLedger(session, acc.AccountId).GeneralJournalId == null) ? 0 : CurrentLedger(session, acc.AccountId).GeneralJournalId.Debit, Credit = (CurrentLedger(session, acc.AccountId).GeneralJournalId == null) ? 0 : CurrentLedger(session, acc.AccountId).GeneralJournalId.Credit, Balance = CurrentLedger(session, acc.AccountId).Balance };
        //    var listObj = from lger in ledger
        //                  where (lger.AccountId.Code != "NAAN_DEFAULT")
        //                  select new
        //                  {
        //                      CreatDate = lger.CreateDate,
        //                      Description = (lger.GeneralJournalId == null) ? "" : ((lger.GeneralJournalId.TransactionId == null) ? "" : lger.GeneralJournalId.TransactionId.Description),
        //                      Code = lger.AccountId.Code,
        //                      Balance = lger.Balance,
        //                      Credit = lger.GeneralJournalId.Credit,
        //                      Debit = lger.GeneralJournalId.Debit,
        //                      JournalCode = (lger.GeneralJournalId == null) ? "" : ((lger.GeneralJournalId.TransactionId == null) ? "" : lger.GeneralJournalId.TransactionId.Code)
        //                  };
        //    try
        //    {
        //        foreach (var o in listObj)
        //        {
        //            resultList.Add(o);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex.Message.ToString());
        //    }

        //    return resultList;
        //}

        public short InitBalanceForward(Session session, AccountingPeriod accountingPeriod)
        {
            List<Object> inittableaccountlist = getInittableAccountList(session);
            foreach (Account account_item in inittableaccountlist)
            {
                Account account = session.GetObjectByKey<Account>(account_item.AccountId);
                XPQuery<GeneralJournalBalanceForward> journalQuery = session.Query<GeneralJournalBalanceForward>();
                var listFromJournal = (from a in journalQuery
                                       where a.TransactionId.AccountingPeriodId == accountingPeriod
                                                    && a.AccountId == account
                                       select a);
                if (listFromJournal.Count() == 0)
                {
                    Guid transaction = CreateBalanceForwardTransaction(session, "Nhập số dư đầu kì", accountingPeriod.FromDateTime, 0);
                    CreateGeneralJournalBalanceForward(session, transaction, account.AccountId, "Nhập số dư đầu kì", 0, 1);
                    //BalanceForwardTransaction transaction = new BalanceForwardTransaction(session);
                    //transaction.AccountingPeriodId = accountingPeriod;
                    //transaction.Code = "BI_" + accountingPeriod.Code + "_" + account.Code;
                    //transaction.CreateDate = DateTime.Now;
                    //transaction.UpdateDate = DateTime.Now;
                    //transaction.Description = "Nhập số dư đầu kì";
                    //transaction.IssueDate = accountingPeriod.FromDateTime;
                    //transaction.PreviousTransactionId = Guid.Empty;
                    //transaction.Balance = 0;
                    //transaction.RowStatus = -1;
                    //transaction.Save();
                    //GeneralJournalBalanceForward generalJournal = new GeneralJournalBalanceForward(session);
                    //generalJournal.TransactionId = transaction;
                    //generalJournal.AccountId = account;
                    //generalJournal.Description = "Nhập số dư đầu kì";
                    //generalJournal.Debit = 0;
                    //generalJournal.Credit = 0;
                    //generalJournal.Balance = 0;
                    //generalJournal.RowStatus = -1;
                    //generalJournal.Save();
                    //GeneralLedger generalLedger = new GeneralLedger(session);
                    //generalLedger.AccountId = account;
                    //generalLedger.TransactionId = transaction;
                    //generalLedger.CreateDate = DateTime.Now;
                    //generalLedger.UpdateDate = DateTime.Now;
                    //generalLedger.IssuedDate = transaction.IssueDate;
                    //generalLedger.Description = "Nhập số dư đầu kì";
                    //generalLedger.Debit = 0;
                    //generalLedger.Credit = 0;
                    //generalLedger.Balance = 0;
                    //generalLedger.IsOriginal = true;
                    //generalLedger.Save();
                }
            }
            return 0;
        }

        public List<AccountingEntity> getInitBalanceForward(Session session, AccountingPeriod accountingPeriod)
        {
            List<AccountingEntity> resultlist = new List<AccountingEntity>();
            XPQuery<GeneralJournalBalanceForward> journalQuery = session.Query<GeneralJournalBalanceForward>();
            var listFromJournal = (from a in journalQuery
                                   where a.TransactionId.AccountingPeriodId == accountingPeriod
                                   select new AccountingEntity
                                   {
                                       AccountID = a.AccountId.AccountId,
                                       Code = a.AccountId.Code,
                                       Name = a.AccountId.Name,
                                       Balance = a.Balance
                                   });
            try
            {
                foreach (var o in listFromJournal)
                {
                    resultlist.Add(o);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return resultlist;
        }

        public class AccountingEntity
        {
            public Guid AccountID { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public double Balance { get; set; }

        }

        public double getCoefficientCompareWithDefaultByCurrencyCode(Session session, string _CurrencyCode)
        {
            //Vuong 31/12/2013
            return 1;
            XPQuery<NAS.DAL.Accounting.Currency.CurrencyType> CurrencyTypeQuery = new XPQuery<NAS.DAL.Accounting.Currency.CurrencyType>(session);
            NAS.DAL.Accounting.Currency.CurrencyType defaultCurrencyType = (from ct in CurrencyTypeQuery
                                                                        where ct.IsMaster == true
                                                                        && ct.RowStatus >= Constant.ROWSTATUS_ACTIVE
                                                                        select ct).FirstOrDefault();

            if (defaultCurrencyType == null)
                throw new Exception("Hệ thống chưa cấu hình loại tiền tệ mặc định");
            
            XPQuery<NAS.DAL.Accounting.Currency.Currency> CurrencyQuery = new XPQuery<NAS.DAL.Accounting.Currency.Currency>(session);
            NAS.DAL.Accounting.Currency.Currency defaultCurrency = (from c in CurrencyQuery
                                       where c.IsDefault == true
                                       && c.RowStatus >= Constant.ROWSTATUS_ACTIVE
                                       && c.CurrencyTypeId == defaultCurrencyType
                                       select c).FirstOrDefault();

            if (defaultCurrency == null)
                throw new Exception("Hệ thống chưa cấu hình tiền tệ mặc định");

            if (_CurrencyCode.Equals(defaultCurrency))
                return  1;
            else
            {
                NAS.DAL.Accounting.Currency.Currency _Currency = (from c in CurrencyQuery
                                                                        where c.Code == _CurrencyCode
                                                                        && c.RowStatus >= Constant.ROWSTATUS_ACTIVE
                                                                        && c.CurrencyTypeId == defaultCurrencyType
                                                                        select c).FirstOrDefault();

                if (_Currency == null)
                    throw new Exception(string.Format("Hệ thống chưa cấu hình cho tiền tệ '{0}'", _CurrencyCode));

                return _Currency.Coefficient;
            }
        }

        public NAS.DAL.Accounting.Currency.Currency getDefaulCurrency(Session session)
        {
            XPQuery<NAS.DAL.Accounting.Currency.CurrencyType> CurrencyTypeQuery = new XPQuery<NAS.DAL.Accounting.Currency.CurrencyType>(session);
            NAS.DAL.Accounting.Currency.CurrencyType defaultCurrencyType = (from ct in CurrencyTypeQuery
                                                                            where ct.IsMaster == true
                                                                            && ct.RowStatus >= Constant.ROWSTATUS_ACTIVE
                                                                            select ct).FirstOrDefault();

            if (defaultCurrencyType == null)
                throw new Exception("Hệ thống chưa cấu hình loại tiền tệ mặc định");

            XPQuery<NAS.DAL.Accounting.Currency.Currency> CurrencyQuery = new XPQuery<NAS.DAL.Accounting.Currency.Currency>(session);
            NAS.DAL.Accounting.Currency.Currency defaultCurrency = (from c in CurrencyQuery
                                                                    where c.IsDefault == true
                                                                    && c.RowStatus >= Constant.ROWSTATUS_ACTIVE
                                                                    && c.CurrencyTypeId == defaultCurrencyType
                                                                    select c).FirstOrDefault();

            if (defaultCurrency == null)
                throw new Exception("Hệ thống chưa cấu hình tiền tệ mặc định");

            return defaultCurrency;
        }                        
    }
}
