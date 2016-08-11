using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NAS.DAL.Accounting.Journal;
using DevExpress.Xpo;
using NAS.BO.Accounting.Journal;
using NAS.BO.CMS.ObjectDocument;
using NAS.DAL.Accounting.JournalAllocation;

namespace WebModule.Accounting.Journal.Transaction.Control.Strategy
{
    public abstract class GridViewBookingEntriesStrategy
    {
        private IEnumerable<NAS.DAL.Accounting.Journal.Transaction> _TransactionsDataSource;

        public void SetTransactionsDataSource(IEnumerable<NAS.DAL.Accounting.Journal.Transaction> transactions)
        {
            this._TransactionsDataSource = transactions;
        }

        public IEnumerable<NAS.DAL.Accounting.Journal.Transaction> TransactionsDataSource
        {
            get { return this._TransactionsDataSource; }
        }

        //public abstract IEnumerable<NAS.DAL.Accounting.Journal.Transaction> GetTransactions(Session session);

        //public abstract NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum GetObjectTypeEnum();

        public virtual GeneralJournal CreateGeneralJournal(
                Session session,
                Guid transactionId,
                Guid accountId,
                Side side,
                double amount,
                string description,
                JounalTypeFlag journalType)
        {
            GeneralJournal ret = null;
            ObjectBO objectBO = new ObjectBO();
            GeneralJournalBO generalJournalBO = new GeneralJournalBO();
            ret = generalJournalBO.CreateGeneralJournal(
                                        session,
                                        transactionId,
                                        accountId,
                                        side,
                                        amount,
                                        description,
                                        journalType);

            //Create CMS object...
            Guid objectTypeId = ret.TransactionId.TransactionObjects.First().ObjectId.ObjectTypeId.ObjectTypeId;

            NAS.DAL.CMS.ObjectDocument.Object CMSObject =
                objectBO.CreateCMSObject(session, objectTypeId);

            GeneralJournalObject generalJournalObject = new GeneralJournalObject(session)
            {
                GeneralJournalId = ret,
                ObjectId = CMSObject
            };
            generalJournalObject.Save();
            
            if (session is UnitOfWork)
            {
                session.FlushChanges();
            }
            //Copy readonly data from transaction to journal
            //Get transaction object
            NAS.DAL.CMS.ObjectDocument.Object transactionCMSObject
                = session.GetObjectByKey<NAS.DAL.Accounting.Journal.Transaction>(transactionId)
                    .TransactionObjects.First().ObjectId;
            //Get general journal object
            NAS.DAL.CMS.ObjectDocument.Object generalJournalCMSObject =
                ret.GeneralJournalObjects.First().ObjectId;

            objectBO.CopyReadOnlyCustomFieldData(
                transactionCMSObject.ObjectId,
                generalJournalCMSObject.ObjectId);

            return ret;
        }

        public virtual void DeleteGeneralJournal(Guid generalJournalId)
        {
            GeneralJournalBO generalJournalBO = new GeneralJournalBO();
            generalJournalBO.DeleteGeneralJounal(generalJournalId);
        }

        public virtual GeneralJournal UpdateGeneralJournal(
                Session session,
                Guid generalJounalId,
                Guid accountId,
                Side side,
                double amount,
                string description)
        {
            GeneralJournalBO generalJournalBO = new GeneralJournalBO();
            return generalJournalBO.UpdateGeneralJournal(
                session,
                generalJounalId,
                accountId,
                side,
                amount,
                description);
        }
    }
}