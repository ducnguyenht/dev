using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Accounting.AccountChart;
using NAS.BO.CMS.ObjectDocument;
using NAS.DAL.Accounting.JournalAllocation;

namespace NAS.BO.Accounting.Journal
{
    public class ManualBookingTransactionBO
    {
        public Guid CreateTransaction(string code, DateTime issuedDate, double amount, string description)
        {
            UnitOfWork uow = null;
            Guid m_TransactionId = Guid.NewGuid();

            try
            {
                GeneralJournalBO generalJournalBO = new GeneralJournalBO();
                uow = XpoHelper.GetNewUnitOfWork();

                //Create new transaction
                ManualBookingTransaction transaction = new ManualBookingTransaction(uow)
                {
                    TransactionId = m_TransactionId,
                    Amount = amount,
                    Code = code,
                    CreateDate = DateTime.Now,
                    Description = description,
                    IssueDate = issuedDate,
                    RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                    UpdateDate = DateTime.Now
                };
                uow.FlushChanges();
                //Create double entry
                //Create debit jounal
                GeneralJournal debitGeneralJournal = generalJournalBO.CreateGeneralJournal
                    (
                        uow,
                        transaction.TransactionId,
                        Account.GetDefault(uow, DefaultAccountEnum.NAAN_DEFAULT).AccountId,
                        Side.DEBIT,
                        amount,
                        description,
                        JounalTypeFlag.ACTUAL
                    );
                //Create credit jounal
                GeneralJournal creditGeneralJournal = generalJournalBO.CreateGeneralJournal
                    (
                        uow,
                        transaction.TransactionId,
                        Account.GetDefault(uow, DefaultAccountEnum.NAAN_DEFAULT).AccountId,
                        Side.CREDIT,
                        amount,
                        description,
                        JounalTypeFlag.ACTUAL
                    );

                ObjectBO objectBO = new ObjectBO();
                //Create CMS object for transaction
                NAS.DAL.CMS.ObjectDocument.Object transactionCMSObject =
                    objectBO.CreateCMSObject(uow,
                        DAL.CMS.ObjectDocument.ObjectTypeEnum.MANUAL_BOOKING);

                TransactionObject transactionObject = new TransactionObject(uow)
                {
                    ObjectId = transactionCMSObject,
                    TransactionId = transaction
                };

                GeneralJournalObject debitGeneralJournalObject = null;
                GeneralJournalObject creditGeneralJournalObject = null;
                //Create CMS object for debit jounal
                NAS.DAL.CMS.ObjectDocument.Object debitJounalCMSObject =
                    objectBO.CreateCMSObject(uow,
                        DAL.CMS.ObjectDocument.ObjectTypeEnum.MANUAL_BOOKING);
                debitGeneralJournalObject = new GeneralJournalObject(uow)
                {
                    GeneralJournalId = debitGeneralJournal,
                    ObjectId = debitJounalCMSObject
                };

                //Create CMS object for debit jounal
                NAS.DAL.CMS.ObjectDocument.Object creditJounalCMSObject =
                    objectBO.CreateCMSObject(uow,
                        DAL.CMS.ObjectDocument.ObjectTypeEnum.MANUAL_BOOKING);
                creditGeneralJournalObject = new GeneralJournalObject(uow)
                {
                    GeneralJournalId = creditGeneralJournal,
                    ObjectId = creditJounalCMSObject
                };

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

            return m_TransactionId;
        }

        public void DeleteTransaction(Guid transactionId)
        {
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                Transaction transaction =
                    uow.GetObjectByKey<Transaction>(transactionId);
                if (transaction == null)
                {
                    throw new Exception("Could not found the transaction");
                }
                //Update rowstatus for the transaction
                transaction.RowStatus = Utility.Constant.ROWSTATUS_DELETED;

                //Update rowstatus for all journals of transacion
                foreach (var generalJounal in transaction.GeneralJournals)
                {
                    generalJounal.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    generalJounal.Save();
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

        public void UpdateTransaction(Guid transactionId, string code, DateTime issuedDate, double amount, string description)
        {
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                //Update transaction
                Transaction transaction =
                    uow.GetObjectByKey<Transaction>(transactionId);
                if (transaction == null)
                {
                    throw new Exception("Could not found the transaction");
                }
                transaction.Amount = amount;
                transaction.Code = code;
                transaction.Description = description;
                transaction.IssueDate = issuedDate;
                transaction.UpdateDate = DateTime.Now;

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
