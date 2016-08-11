using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Vouches.Allocation
{
    public partial class VoucherAllocationBookingAccount : XPCustomObject
    {
        public VoucherAllocationBookingAccount(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fVoucherAllocationBookingAccountId;
        [Key(true)]
        public Guid VoucherAllocationBookingAccountId
        {
            get { return fVoucherAllocationBookingAccountId; }
            set { SetPropertyValue<Guid>("VoucherAllocationBookingAccountId", ref fVoucherAllocationBookingAccountId, value); }
        }

        private double fCredit;
        public double Credit
        {
            get { return fCredit; }
            set { SetPropertyValue<double>("Credit", ref fCredit, value); }
        }

        private double fDebit;
        public double Debit
        {
            get { return fDebit; }
            set { SetPropertyValue<double>("Debit", ref fDebit, value); }
        }

        //References
        private VoucherAllocation fVoucherAllocationId;
        [Association("VoucherAllocationBookingAccountReferencesVoucherAllocation")]
        public VoucherAllocation VoucherAllocationId
        {
            get { return fVoucherAllocationId; }
            set { SetPropertyValue<VoucherAllocation>("VoucherAllocationId", ref fVoucherAllocationId, value); }
        }

        private NAS.DAL.Accounting.AccountChart.Account fAccountId;
        [Association("VoucherAllocationBookingAccountReferencesNAS.DAL.Accounting.AccoutChart.Accout")]
        public NAS.DAL.Accounting.AccountChart.Account AccountId
        {
            get { return fAccountId; }
            set { SetPropertyValue<NAS.DAL.Accounting.AccountChart.Account>("AccountId", ref fAccountId, value); }
        }
        //End References
    }
}
