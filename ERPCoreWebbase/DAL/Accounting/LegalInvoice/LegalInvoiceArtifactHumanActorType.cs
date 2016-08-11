using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Accounting.LegalInvoice
{
    public partial class LegalInvoiceArtifactHumanActorType : XPCustomObject
    {
        public LegalInvoiceArtifactHumanActorType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fLegalInvoiceArtifactHumanActorTypeId;
        [Key(true)]
        public Guid LegalInvoiceArtifactHumanActorTypeId
        {
            get { return fLegalInvoiceArtifactHumanActorTypeId; }
            set { SetPropertyValue<Guid>("LegalInvoiceArtifactHumanActorTypeid", ref fLegalInvoiceArtifactHumanActorTypeId, value); }
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

        #region Refernces
        [Association(@"LegalInvoiceArtifactHumanActorReferencesLegalInvoiceArtifactHumanActorType", typeof(LegalInvoiceArtifactHumanActor))]
        public XPCollection<LegalInvoiceArtifactHumanActor> LegalInvoiceArtifactHumanActors { get { return GetCollection<LegalInvoiceArtifactHumanActor>("LegalInvoiceArtifactHumanActors"); } }
        #endregion
    }
}
