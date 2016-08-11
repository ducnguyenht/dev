using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Accounting.LegalInvoice
{
    public partial class LegalInvoiceArtifactIdentifier : XPCustomObject
    {
        public LegalInvoiceArtifactIdentifier(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fLegalInvoiceArtifactIdentifierId;
        [Key(true)]
        public Guid LegalInvoiceArtifactIdentifierId
        {
            get { return fLegalInvoiceArtifactIdentifierId; }
            set { SetPropertyValue<Guid>("LegalInvoiceArtifactIdentifierId", ref fLegalInvoiceArtifactIdentifierId, value); }
        }

        private string fIdentifier;
        public string Identifier
        {
            get { return fIdentifier; }
            set { SetPropertyValue<string>("Identifier", ref fIdentifier, value); }
        }

        private short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        #endregion

        #region References
        private LegalInvoiceArtifact fLegalInvoiceArtifactId;
        [Association("LegalInvoiceArtifactIdentifierReferencesLegalInvoiceArtifact")]
        public LegalInvoiceArtifact LegalInvoiceArtifactId
        {
            get{ return fLegalInvoiceArtifactId;}
            set{ SetPropertyValue<LegalInvoiceArtifact>("LegalInvoiceArtifactId", ref fLegalInvoiceArtifactId, value); }
        }

        private LegalInvoiceArtifactIdentifierType fLegalInvoiceArtifactIdentifierTypeId;
        [Association("LegalInvoiceArtifactIdentifierReferencesLegalInvoiceArtifactIdentifierType")]
        public LegalInvoiceArtifactIdentifierType LegalInvoiceArtifactIdentifierTypeId
        {
            get { return fLegalInvoiceArtifactIdentifierTypeId; }
            set { SetPropertyValue<LegalInvoiceArtifactIdentifierType>("LegalInvoiceArtifactIdentifierType", ref fLegalInvoiceArtifactIdentifierTypeId, value); }
        }
        #endregion
    }
}
