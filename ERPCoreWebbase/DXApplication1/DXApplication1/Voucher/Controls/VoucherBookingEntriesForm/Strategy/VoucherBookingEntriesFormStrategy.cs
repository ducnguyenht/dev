using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Xpo;
using NAS.BO.Accounting.Journal;
using NAS.DAL.Accounting.Journal;
using DevExpress.Data.Filtering;

namespace WebModule.Voucher.Controls.VoucherBookingEntriesForm.Strategy
{
    public abstract class VoucherBookingEntriesFormStrategy
    {
        private VoucherTransactionBOBase _VoucherTransactionBOBase;

        public VoucherTransactionBOBase VoucherTransactionBO
        {
            get { return _VoucherTransactionBOBase; }
        }

        public VoucherBookingEntriesFormStrategy()
        {
            _VoucherTransactionBOBase = CreateVoucherTransactionBO();
        }

        protected abstract VoucherTransactionBOBase CreateVoucherTransactionBO();

        public abstract Type GetConcreteVoucherTransactionType();

        public abstract CriteriaOperator GetVoucherTransactionCriteria(Guid voucherId);

        public virtual GeneralJournal CreateGeneralJournal(
                Session session,
                Guid transactionId,
                Guid accountId,
                Side side,
                double amount,
                string description,
                JounalTypeFlag journalType)
        {
            GeneralJournalBO generalJournalBO = new GeneralJournalBO();
            return generalJournalBO.CreateGeneralJournal(
                                        session, 
                                        transactionId, 
                                        accountId, 
                                        side, 
                                        amount, 
                                        description, 
                                        journalType);
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