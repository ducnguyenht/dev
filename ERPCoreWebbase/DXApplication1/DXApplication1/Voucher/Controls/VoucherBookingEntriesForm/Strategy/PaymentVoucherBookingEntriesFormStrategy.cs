using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.Journal;
using NAS.BO.Accounting.Journal;
using NAS.BO.CMS.ObjectDocument;
using NAS.DAL.Accounting.JournalAllocation;

namespace WebModule.Voucher.Controls.VoucherBookingEntriesForm.Strategy
{
    public class PaymentVoucherBookingEntriesFormStrategy : VoucherBookingEntriesFormStrategy
    {
        public PaymentVoucherBookingEntriesFormStrategy() : base() { }

        public override Type GetConcreteVoucherTransactionType()
        {
            return typeof(NAS.DAL.Accounting.Journal.PaymentVouchesTransaction);
        }

        public override DevExpress.Data.Filtering.CriteriaOperator GetVoucherTransactionCriteria(Guid voucherId)
        {
            return CriteriaOperator.And(
                new BinaryOperator("PaymentVouchesId!Key", voucherId),
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual)
            );
        }

        public override NAS.DAL.Accounting.Journal.GeneralJournal CreateGeneralJournal(DevExpress.Xpo.Session session, Guid transactionId, Guid accountId, NAS.BO.Accounting.Journal.Side side, double amount, string description, NAS.BO.Accounting.Journal.JounalTypeFlag journalType)
        {
            GeneralJournal generalJournal =
                base.CreateGeneralJournal(session,
                    transactionId,
                    accountId,
                    side,
                    amount,
                    description,
                    journalType);
            //Create CMS object...

            ObjectBO objectBO = new ObjectBO();
            NAS.DAL.CMS.ObjectDocument.Object CMSObject =
                objectBO.CreateCMSObject(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.VOUCHER_PAYMENT);

            GeneralJournalObject generalJournalObject = new GeneralJournalObject(session)
            {
                GeneralJournalId = generalJournal,
                ObjectId = CMSObject
            };
            generalJournalObject.Save();

            return generalJournal;
        }

        protected override NAS.BO.Accounting.Journal.VoucherTransactionBOBase CreateVoucherTransactionBO()
        {
            return new PaymentVoucherTransactionBO();
        }
    }
}