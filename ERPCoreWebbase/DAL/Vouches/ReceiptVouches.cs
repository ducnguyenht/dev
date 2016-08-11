using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using Utility;
using NAS.DAL.Accounting.Journal;
namespace NAS.DAL.Vouches
{

    [MapInheritance(MapInheritanceType.OwnTable)]
    public partial class ReceiptVouches : Vouches
    {
        public ReceiptVouches(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        // Fields...
        private string _Payer;

        public string Payer
        {
            get
            {
                return _Payer;
            }
            set
            {
                SetPropertyValue("Payer", ref _Payer, value);
            }
        }
        
        #region Logic

        public static ReceiptVouches InitNewRow(Session session)
        {
            try
            {
                ReceiptVouches receiptVouches = new ReceiptVouches(session)
                {
                    RowStatus = Constant.ROWSTATUS_TEMP
                };
                receiptVouches.Save();
                return receiptVouches;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }

        #endregion

        [Association(@"ReceiptVoucherTransactionReferencesReceiptVoucher", typeof(ReceiptVouchesTransaction))]
        public XPCollection<ReceiptVouchesTransaction> ReceiptVouchesTransactions { get { return GetCollection<ReceiptVouchesTransaction>("ReceiptVouchesTransactions"); } } 
    }

}
