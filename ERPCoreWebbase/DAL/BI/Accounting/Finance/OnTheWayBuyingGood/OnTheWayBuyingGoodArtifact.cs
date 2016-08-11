using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.BI.Accounting.Finance.OnTheWayBuyingGood
{
    public partial class OnTheWayBuyingGoodArtifact : XPCustomObject
    {
        public OnTheWayBuyingGoodArtifact(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region properties
        private Guid fOnTheWayBuyingGoodArtifactId;
        [Key(true)]
        public Guid OnTheWayBuyingGoodArtifactId
        {
            get { return fOnTheWayBuyingGoodArtifactId; }
            set { SetPropertyValue<Guid>("OnTheWayBuyingGoodArtifactId", ref fOnTheWayBuyingGoodArtifactId, value); }
        }

        private string fInvoiceCode;
        public string InvoiceCode
        {
            get { return fInvoiceCode; }
            set { SetPropertyValue<string>("InvoiceCode", ref fInvoiceCode, value); }
        }

        private DateTime fInvoiceIssuedDate;
        public DateTime InvoiceIssuedDate
        {
            get { return fInvoiceIssuedDate; }
            set { SetPropertyValue<DateTime>("InvoiceIssuedDate", ref fInvoiceIssuedDate, value); }
        }

        private string fLegalInvoiceCode;
        public string LegalInvoiceCode
        {
            get{ return fLegalInvoiceCode;}
            set { SetPropertyValue<string>("LegalInvoiceCode", ref fLegalInvoiceCode, value); }
        }

        private DateTime fLegalInvoiceIssuedDate;
        public DateTime LegalInvoiceIssuedDate
        {
            get { return fLegalInvoiceIssuedDate; }
            set { SetPropertyValue<DateTime>("LegalInvoiceIssuedDate", ref fLegalInvoiceIssuedDate, value); }
        }

        private string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        private short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        #endregion

        #region references
        [Association(@"FinancialOnTheWayBuyingGoodDetailReferencesOnTheWayBuyingGoodArtifact", typeof(FinancialOnTheWayBuyingGoodDetail))]
        public XPCollection<FinancialOnTheWayBuyingGoodDetail> FinancialOnTheWayBuyingGoodDetails { get { return GetCollection<FinancialOnTheWayBuyingGoodDetail>("FinancialOnTheWayBuyingGoodDetails"); } }

        private FinancialOnTheWayBuyingGoodSummary fFinancialOnTheWayBuyingGoodSummaryId;
        [Association("OnTheWayBuyingGoodArtifactReferencesFianncialOnTheWayBuyingGoodSummary")]
        public FinancialOnTheWayBuyingGoodSummary FinancialOnTheWayBuyingGoodSummaryId
        {
            get { return fFinancialOnTheWayBuyingGoodSummaryId; }
            set { SetPropertyValue<FinancialOnTheWayBuyingGoodSummary>("FinancialOnTheWayBuyingGoodSummaryId", ref fFinancialOnTheWayBuyingGoodSummaryId, value); }
        }
        #endregion
    }
}
