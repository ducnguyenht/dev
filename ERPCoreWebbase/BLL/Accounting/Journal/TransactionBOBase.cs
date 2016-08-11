using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.CMS.ObjectDocument;
using NAS.BO.CMS.ObjectDocument;

namespace NAS.BO.Accounting.Journal
{
    public enum CanBookingEntryReturnValue
    {
        /// <summary>
        /// Tranasction have not any general journals
        /// </summary>
        HAVE_NO_JOURNAL,
        /// <summary>
        /// Total of debit and create are zero
        /// </summary>
        DEBIT_CREDIT_ZERO,
        /// <summary>
        /// Transaction is not balanced
        /// </summary>
        NOT_BALANCED,
        /// <summary>
        /// Transaction is balanced
        /// </summary>
        BALANCED,
        /// <summary>
        /// Row status of the transaction is not valid
        /// </summary>
        INVALID_TRANSACTION_STATUS,
        /// <summary>
        /// 
        /// </summary>
        MANY_SIDE,
        NOT_EQUAL_WITH_TOTAL
    }

    public class TransactionBOBase
    {
        //public bool CanBookingEntry(Guid transactionId, bool isCheckWithTransactionAmount)
        //{
        //    Session session = null;
        //    try
        //    {
        //        session = XpoHelper.GetNewSession();
        //        //Get transaction
        //        Transaction transaction = session.GetObjectByKey<Transaction>(transactionId);
        //        if (transaction == null)
        //        {
        //            throw new Exception("Could not found transaction");
        //        }
        //        if (transaction.GeneralJournals == null || transaction.GeneralJournals.Count == 0)
        //        {
        //            return false;
        //        }
        //        //Is general journal balanced?
        //        double sumOfDebit = 0;
        //        double sumOfCredit = 0;
        //        sumOfDebit = transaction.GeneralJournals
        //            .Where(r => r.RowStatus >= Utility.Constant.ROWSTATUS_TEMP).Sum(r => r.Debit);
        //        sumOfCredit = transaction.GeneralJournals
        //            .Where(r => r.RowStatus >= Utility.Constant.ROWSTATUS_TEMP).Sum(r => r.Credit);

        //        if (sumOfDebit != sumOfCredit)
        //            return false;

        //        if (isCheckWithTransactionAmount)
        //        {
        //            double transactionAmount = transaction.Amount;
        //            if (transactionAmount != sumOfDebit || transactionAmount != sumOfDebit)
        //                return false;
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (session != null) session.Dispose();
        //    }
        //}

        public bool IsBookedTransaction(Session session, Guid transactionId, out Transaction transaction)
        {
            transaction = session.GetObjectByKey<Transaction>(transactionId);
            if (transaction == null)
                throw new Exception("Could not found transaction");
            if (!transaction.RowStatus.Equals(Utility.Constant.ROWSTATUS_BOOKED_ENTRY))
                return false;
            return true;
        }

        public CanBookingEntryReturnValue CanBookingEntry(Guid transactionId, bool isCheckWithTransactionAmount)
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //Get transaction
                Transaction transaction = session.GetObjectByKey<Transaction>(transactionId);
                if (transaction == null)
                {
                    throw new Exception("Could not found transaction");
                }

                if (transaction is BalanceForwardTransaction)
                    return CanBookingEntryReturnValue.BALANCED;

                int countVisibleJournal = transaction.GeneralJournals
                    .Count(r => r.RowStatus >= Utility.Constant.ROWSTATUS_ACTIVE);
                if (countVisibleJournal == 0)
                {
                    return CanBookingEntryReturnValue.HAVE_NO_JOURNAL;
                }

                if (transaction.RowStatus < Utility.Constant.ROWSTATUS_ACTIVE)
                    return CanBookingEntryReturnValue.INVALID_TRANSACTION_STATUS;

                int countDebit = transaction.GeneralJournals
                    .Count(r => r.RowStatus >= Utility.Constant.ROWSTATUS_ACTIVE
                            && r.Debit > 0
                            && r.AccountId.AccountTypeId.AccountCategoryId.Code != "OFFBALANCE");

                int countCredit = transaction.GeneralJournals
                    .Count(r => r.RowStatus >= Utility.Constant.ROWSTATUS_ACTIVE
                            && r.Credit > 0
                            && r.AccountId.AccountTypeId.AccountCategoryId.Code != "OFFBALANCE");

                if (countDebit > 1 && countCredit > 1)
                    return CanBookingEntryReturnValue.MANY_SIDE;

                //Is general journal balanced?
                double sumOfDebit = 0;
                double sumOfCredit = 0;
                sumOfDebit = transaction.GeneralJournals
                    .Where(r => r.RowStatus >= Utility.Constant.ROWSTATUS_ACTIVE
                        && r.AccountId.AccountTypeId.AccountCategoryId.Code != "OFFBALANCE")
                    .Sum(r => r.Debit);
                sumOfCredit = transaction.GeneralJournals
                    .Where(r => r.RowStatus >= Utility.Constant.ROWSTATUS_ACTIVE
                        && r.AccountId.AccountTypeId.AccountCategoryId.Code != "OFFBALANCE")
                    .Sum(r => r.Credit);

                if (sumOfDebit == 0 && sumOfCredit == 0)
                    return CanBookingEntryReturnValue.DEBIT_CREDIT_ZERO;

                if (sumOfDebit != sumOfCredit)
                    return CanBookingEntryReturnValue.NOT_BALANCED;

                if (isCheckWithTransactionAmount)
                {
                    double transactionAmount = transaction.Amount;
                    if (transactionAmount != sumOfDebit || transactionAmount != sumOfDebit)
                        return CanBookingEntryReturnValue.NOT_EQUAL_WITH_TOTAL;
                }
                return CanBookingEntryReturnValue.BALANCED;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }

        public bool BookEntry(Guid transactionId)
        {
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();

                bool ret = BookEntry(uow, transactionId);

                uow.CommitChanges();

                return ret;
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

        public bool BookEntry(Session session, Guid transactionId)
        {
            try
            {
                Transaction transaction = session.GetObjectByKey<Transaction>(transactionId);

                if (transaction == null)
                    throw new Exception("Could not found transaction");

                transaction.RowStatus = Utility.Constant.ROWSTATUS_BOOKED_ENTRY;
                /*2014-01-17 ERP-1418 Khoa.Truong INS START*/
                transaction.AccountingPeriodId = 
                    AccountingPeriodBO.GetAccountingPeriod(session, transaction.IssueDate);
                /*2014-01-17 ERP-1418 Khoa.Truong INS END*/
                transaction.Save();

                IEnumerable<GeneralJournal> generalJournals = null;
                generalJournals = transaction.GeneralJournals
                    .Where(r => r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE);
                if (generalJournals == null)
                    throw new Exception("Operation is not valid");

                foreach (var generalJournal in generalJournals)
                {
                    generalJournal.RowStatus = Utility.Constant.ROWSTATUS_BOOKED_ENTRY;
                    generalJournal.Save();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
