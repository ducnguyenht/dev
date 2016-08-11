using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Vouches;

namespace NAS.DAL.Accounting.Journal
{
    [MapInheritance(MapInheritanceType.OwnTable)]
    public partial class ReceiptVouchesTransaction : Transaction
    {
        // Fields...
        ReceiptVouches fReceiptVouchesId;
        [Association(@"ReceiptVoucherTransactionReferencesReceiptVoucher")]
        public ReceiptVouches ReceiptVouchesId
        {
            get
            {
                return fReceiptVouchesId;
            }
            set
            {
                SetPropertyValue<ReceiptVouches>("ReceiptVouchesId", ref fReceiptVouchesId, value);
            }
        }

        [NonPersistent]
        public DateTime pIssueDate
        {
            get
            {
                return ReceiptVouchesId.IssuedDate;
            }
        }
    }
}
