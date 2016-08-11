using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting;

namespace NAS.DAL.BI.BillDim
{
    public class InvoiceDim:XPCustomObject
    {
        public InvoiceDim(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
        #endregion

        #region Properties
        int fInvoiceDimId;
        [Key(true)]
        public int InvoiceDimId
        {
            get { return fInvoiceDimId; }
            set { SetPropertyValue<int>("InvoiceDimId", ref fInvoiceDimId, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fDescription;
        public string Description
        {
            get {return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        Guid fRefId;
        public Guid RefId
        {
            get { return fRefId; }
            set { SetPropertyValue<Guid>("RefId", ref fRefId, value); }
        }

        DateTime fIssueDate;
        public DateTime IssueDate
        {
            get { return fIssueDate; }
            set { SetPropertyValue<DateTime>("IssueDate", ref fIssueDate, value); }
        }

        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        #endregion

        #region References
        [Association(@"FinancialRevenueByItem_FactReferencesInvoiceDim", typeof(FinancialRevenueByItem_Fact)), Aggregated]
        public XPCollection<FinancialRevenueByItem_Fact> FinancialRevenueByItem_Facts
        {
            get { return GetCollection<FinancialRevenueByItem_Fact>("FinancialRevenueByItem_Facts"); }
        }
        #endregion
    }
}
