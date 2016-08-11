using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Vouches;

namespace NAS.DAL.Accounting.Journal
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public partial class PaymentVouchesTransaction : Transaction
    {
        // Fields...
        PaymentVouches fPaymentVouchesId;
        [Association(@"PaymentVoucherTransactionReferencesPaymentVoucher")]
        public PaymentVouches PaymentVouchesId
        {
            get
            {
                return fPaymentVouchesId;
            }
            set
            {
                SetPropertyValue<PaymentVouches>("PaymentVouchesId", ref fPaymentVouchesId, value);
            }
        }

        [NonPersistent]
        public DateTime pIssueDate
        {
            get
            {
                return PaymentVouchesId.IssuedDate;
            }
        }
    }
}
