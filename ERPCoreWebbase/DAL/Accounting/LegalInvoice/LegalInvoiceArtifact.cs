using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.LegalInvoice;

namespace NAS.DAL.Accounting.LegalInvoice
{
    public partial class LegalInvoiceArtifact: XPCustomObject
    {
        public LegalInvoiceArtifact(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fLegalInvoiceArtifactId;
        [Key(true)]
        public Guid LegalInvoiceArtifactId
        {
            get { return fLegalInvoiceArtifactId; }
            set { SetPropertyValue<Guid>("LegalInvoiceArtifactId", ref fLegalInvoiceArtifactId, value); }
        }

        private string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        private string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        private DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue<DateTime>("CreateDate", ref fCreateDate, value); }
        }

        private DateTime fIssuedDate;
        public DateTime IssuedDate
        {
            get { return fIssuedDate; }
            set { SetPropertyValue<DateTime>("IssuedDate", ref fIssuedDate, value); }
        }

        private DateTime fLastUpdateDate;
        public DateTime LastUpdateDate
        {
            get { return fLastUpdateDate; }
            set{ SetPropertyValue<DateTime>("LastUpdateDate", ref fLastUpdateDate, value); }
        }

        private short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        #endregion

        #region References
        [Association(@"LegalInvoiceArtifactSummaryReferencesLegalInvoiceArtifact", typeof(LegalInvoiceArtifactSummary))]
        public XPCollection<LegalInvoiceArtifactSummary> LegalInvoiceArtifactSummaries { get { return GetCollection<LegalInvoiceArtifactSummary>("LegalInvoiceArtifactSummaries"); } }

        [Association(@"LegalInvoiceArtifactOrgActorReferencesLegalInvoiceArtifact", typeof(LegalInvoiceArtifactOrgActor))]
        public XPCollection<LegalInvoiceArtifactOrgActor> LegalInvoiceArtifactOrgActors { get { return GetCollection<LegalInvoiceArtifactOrgActor>("LegalInvoiceArtifactOrgActors"); } }

        [Association(@"LegalInvoiceArtifactDetailReferencesLegalInvoiceArtifact", typeof(LegalInvoiceArtifactDetail))]
        public XPCollection<LegalInvoiceArtifactDetail> LegalInvoiceArtifactDetails { get { return GetCollection<LegalInvoiceArtifactDetail>("LegalInvoiceArtifactDetails"); } }

        [Association(@"LegalInvoiceArtifactHumanActorReferencesLegalInvoiceArtifact", typeof(LegalInvoiceArtifactHumanActor))]
        public XPCollection<LegalInvoiceArtifactHumanActor> LegalInvoiceArtifactHumanActors { get { return GetCollection<LegalInvoiceArtifactHumanActor>("LegalInvoiceArtifactHumanActors"); } }

        [Association(@"LegalInvoiceArtifactIdentifierReferencesLegalInvoiceArtifact", typeof(LegalInvoiceArtifactIdentifier))]
        public XPCollection<LegalInvoiceArtifactIdentifier> LegalInvoiceArtifactIdentifiers { get { return GetCollection<LegalInvoiceArtifactIdentifier>("LegalInvoiceArtifactIdentifiers"); } }

        [Association(@"LegalInvoiceArtifactIssuedLogReferencesLegalInvoiceArtifact", typeof(LegalInvoiceArtifactIssuedLog))]
        public XPCollection<LegalInvoiceArtifactIssuedLog> LegalInvoiceArtifactIssuedLogs { get { return GetCollection<LegalInvoiceArtifactIssuedLog>("LegalInvoiceArtifactIssuedLogs"); } }

        private LegalInvoiceArtifactType fLegalInvoiceArtifactTypeId;
        [Association("LegalInvoiceArtifactReferencesLegalInvoiceArtifactType")]
        public LegalInvoiceArtifactType LegalInvoiceArtifactTypeId
        {
            get { return fLegalInvoiceArtifactTypeId; }
            set { SetPropertyValue<LegalInvoiceArtifactType>("LegalInvoiceArtifactTypeId", ref fLegalInvoiceArtifactTypeId, value); }
        }
        #endregion
    }
}
