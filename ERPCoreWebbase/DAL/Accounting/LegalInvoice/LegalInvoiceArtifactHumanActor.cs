using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Accounting.LegalInvoice
{
    public partial class LegalInvoiceArtifactHumanActor : XPCustomObject
    {
        public LegalInvoiceArtifactHumanActor(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fLegalInvoiceArtifactHumanActorId;
        [Key(true)]
        public Guid LegalInvoiceArtifactHumanActorId
        {
            get { return fLegalInvoiceArtifactHumanActorId; }
            set { SetPropertyValue<Guid>("LegalInvoiceArtifactHumanActorId", ref fLegalInvoiceArtifactHumanActorId, value); }
        }

        private string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
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
        private LegalInvoiceArtifact fLegalInvoiceArtifactId;
        [Association("LegalInvoiceArtifactHumanActorReferencesLegalInvoiceArtifact")]
        public LegalInvoiceArtifact LegalInvoiceArtifactId
        {
            get { return fLegalInvoiceArtifactId; }
            set { SetPropertyValue<LegalInvoiceArtifact>("LegalInvoiceArtifactId", ref fLegalInvoiceArtifactId, value); }
        }

        private LegalInvoiceArtifactHumanActorType fLegalInvoiceArtifactHumanActorTypeId;
        [Association("LegalInvoiceArtifactHumanActorReferencesLegalInvoiceArtifactHumanActorType")]
        public LegalInvoiceArtifactHumanActorType LegalInvoiceArtifactHumanActorTypeId
        {
            get { return fLegalInvoiceArtifactHumanActorTypeId; }
            set { SetPropertyValue<LegalInvoiceArtifactHumanActorType>("LegalInvoiceArtifactHumanTypeId", ref fLegalInvoiceArtifactHumanActorTypeId, value); }
        }
        #endregion
    }
}
