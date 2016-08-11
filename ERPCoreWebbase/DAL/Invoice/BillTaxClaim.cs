using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Invoice
{
    public class BillTaxClaim : XPCustomObject
    {
        Guid fBillTaxClaimId;
        [Key(true)]
        public Guid BillTaxClaimId
        {
            get { return fBillTaxClaimId; }
            set { SetPropertyValue<Guid>("BillTaxClaimId", ref fBillTaxClaimId, value); }
        }

        double fAmount;
        public double Amount
        {
            get { return fAmount; }
            set { SetPropertyValue<double>("Code", ref fAmount, value); }
        }

        Bill fBillId;
        [Association(@"BillTaxClaimReferenceBill")]
        public Bill BillId
        {
            get { return fBillId; }
            set { SetPropertyValue<Bill>("BillId", ref fBillId, value); }
        }

        string fClaimItem;
        public string ClaimItem
        {
            get { return fClaimItem; }
            set { SetPropertyValue<string>("ClaimItem", ref fClaimItem, value); }
        }

        string fComment;
        public string Comment
        {
            get { return fComment; }
            set { SetPropertyValue<string>("Comment", ref fComment, value); }
        }

        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue<DateTime>("CreateDate", ref fCreateDate, value); }
        }

        DateTime fLastUpdateDate;
        public DateTime LastUpdateDate
        {
            get { return fLastUpdateDate; }
            set { SetPropertyValue<DateTime>("LastUpdateDate", ref fLastUpdateDate, value); }
        }

        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        double fTaxInNumber;
        public double TaxInNumber
        {
            get { return fTaxInNumber; }
            set { SetPropertyValue<double>("TaxInNumber", ref fTaxInNumber, value); }
        }

        double fTaxInPercentage;
        public double TaxInPercentage
        {
            get { return fTaxInPercentage; }
            set { SetPropertyValue<double>("TaxInPercentage", ref fTaxInPercentage, value); }
        }

        double fTotalAmount;
        public double TotalAmount
        {
            get { return fTotalAmount; }
            set { SetPropertyValue<double>("TotalAmount", ref fTotalAmount, value); }
        }

        public BillTaxClaim(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
