using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.BI.Accounting.Cash
{
    public class FinancialVoucherDim : XPCustomObject
    {
        int fFinancialVoucherDimId;
        [Key(true)]
        public int FinancialVoucherDimId
        {
            get { return fFinancialVoucherDimId; }
            set { SetPropertyValue<int>("FinancialVoucherDimId", ref fFinancialVoucherDimId, value); }
        }

        DateTime fBookingDate;
        public DateTime BookingDate
        {
            get { return fBookingDate; }
            set { SetPropertyValue<DateTime>("BookingDate", ref fBookingDate, value); }
        }

        DateTime fIssueDate;
        public DateTime IssueDate
        {
            get { return fIssueDate; }
            set { SetPropertyValue<DateTime>("IssueDate", ref fIssueDate, value); }
        }

        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        Guid fRefId;
        public Guid RefId
        {
            get { return fRefId; }
            set { SetPropertyValue<Guid>("RefId", ref fRefId, value); }
        }
        
        [Association("FinancialCash_FactReferencesFinancialVoucherDim", typeof(FinancialCash_Fact)), Aggregated]
        public XPCollection<FinancialCash_Fact> FinancialCash_Facts
        {
            get { return GetCollection<FinancialCash_Fact>("FinancialCash_Facts"); }
        }

        public FinancialVoucherDim(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
