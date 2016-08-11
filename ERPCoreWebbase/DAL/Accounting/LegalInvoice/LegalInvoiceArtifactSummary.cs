using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Accounting.LegalInvoice
{
    public partial class LegalInvoiceArtifactSummary : XPCustomObject
    {
        public LegalInvoiceArtifactSummary(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fLegalInvoiceArtifactSummaryId;
        [Key(true)]
        public Guid LegalInvoiceArtifactSummaryId
        {
            get { return fLegalInvoiceArtifactSummaryId; }
            set { SetPropertyValue<Guid>("LegalInvoiceArtifactSummayId", ref fLegalInvoiceArtifactSummaryId, value); }
        }

        private double fAmount;
        public double Amount
        {
            get { return fAmount; }
            set { SetPropertyValue<double>("Amount", ref fAmount, value); }
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
        private LegalInvoiceArtifactSummaryType fLegalInvoiceArtifactSummaryTypeId;
        [Association("LegalInvoiceArtifactSummaryReferencesLegalInvoiceArtifactSummaryType")]
        public LegalInvoiceArtifactSummaryType LegalInvoiceArtifactSummaryTypeId
        {
            get { return fLegalInvoiceArtifactSummaryTypeId; }
            set { SetPropertyValue<LegalInvoiceArtifactSummaryType>("LegalInvoiceArtifactSummaryTypeId", ref fLegalInvoiceArtifactSummaryTypeId, value); }
        }

        private LegalInvoiceArtifact fLegalInvoiceArtifactId;
        [Association("LegalInvoiceArtifactSummaryReferencesLegalInvoiceArtifact")]
        public LegalInvoiceArtifact LegalInvoiceArtifactId
        {
            get { return fLegalInvoiceArtifactId; }
            set { SetPropertyValue<LegalInvoiceArtifact>("LegalInvoiceArtifactId", ref fLegalInvoiceArtifactId, value); }
        }
        #endregion
    }
}
