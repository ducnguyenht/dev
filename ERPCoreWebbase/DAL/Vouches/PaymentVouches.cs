using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using Utility;
using NAS.DAL.Accounting.Journal;
namespace NAS.DAL.Vouches
{

    [MapInheritance(MapInheritanceType.OwnTable)]
    public partial class PaymentVouches : Vouches
    {
        public PaymentVouches(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        // Fields...
        private string _Payee;

        public string Payee
        {
            get
            {
                return _Payee;
            }
            set
            {
                SetPropertyValue("Payee", ref _Payee, value);
            }
        }

        public static PaymentVouches InitNewRow(Session session)
        {
            try
            {
                PaymentVouches paymentVouches = new PaymentVouches(session)
                {
                    RowStatus = Constant.ROWSTATUS_TEMP
                };
                paymentVouches.Save();
                return paymentVouches;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }

        [Association(@"PaymentVoucherTransactionReferencesPaymentVoucher", typeof(PaymentVouchesTransaction))]
        public XPCollection<PaymentVouchesTransaction> PaymentVouchesTransactions { get { return GetCollection<PaymentVouchesTransaction>("PaymentVouchesTransactions"); } } 
    }

}
