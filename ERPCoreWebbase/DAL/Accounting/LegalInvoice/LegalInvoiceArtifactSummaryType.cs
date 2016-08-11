using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Accounting.LegalInvoice
{
    public partial class LegalInvoiceArtifactSummaryType : XPCustomObject
    {
        public LegalInvoiceArtifactSummaryType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fLegalInvoiceArtifactSummaryTypeId;
        [Key(true)]
        public Guid LegalInvoiceArtifactSummaryTypeId
        {
            get { return fLegalInvoiceArtifactSummaryTypeId; }
            set { SetPropertyValue<Guid>("LegalInvoiceArtifactSummaryTypeId", ref fLegalInvoiceArtifactSummaryTypeId, value); }
        }

        private string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
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

        #region References
        [Association(@"LegalInvoiceArtifactSummaryReferencesLegalInvoiceArtifactSummaryType", typeof(LegalInvoiceArtifactSummary))]
        public XPCollection<LegalInvoiceArtifactSummary> LegalInvoiceArtifactSummaries { get { return GetCollection<LegalInvoiceArtifactSummary>("LegalInvoiceArtifactSummaries"); } }

        #endregion
    }
}
