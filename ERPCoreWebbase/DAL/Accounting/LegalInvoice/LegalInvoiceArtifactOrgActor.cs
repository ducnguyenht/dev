using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Accounting.LegalInvoice
{
    public partial class LegalInvoiceArtifactOrgActor : XPCustomObject
    {
        public LegalInvoiceArtifactOrgActor(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fLegalInvoiceArtifactOrgActorId;
        [Key(true)]
        public Guid LegalInvoiceArtifactOrgActorId
        {
            get { return fLegalInvoiceArtifactOrgActorId; }
            set { SetPropertyValue<Guid>("LegalInvoiceArtifactOrgActorId", ref fLegalInvoiceArtifactOrgActorId, value); }
        }

        private string fCode;
        public string Code{
            get{ return fCode;}
            set{SetPropertyValue<string>("Code", ref fCode, value);}
        }

        private string fName;
        public string Name
        {
            get { return fName; }
            set{ SetPropertyValue<string>("Name", ref fName, value);}
        }

        private string fAddress;
        public string Address
        {
            get { return fAddress; }
            set { SetPropertyValue<string>("Address", ref fAddress, value); }
        }

        private string fTelephoneFax;
        public string TelephoneFax
        {
            get { return fTelephoneFax; }
            set { SetPropertyValue<string>("TelephoneFax", ref fTelephoneFax, value); }
        }

        private string fTaxCode;
        public string TaxCode
        {
            get { return fTaxCode; }
            set { SetPropertyValue<string>("TaxCode", ref fTaxCode, value); }
        }

        private string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        private string fAccountNumber;
        public string AccountNumber
        {
            get { return fAccountNumber; }
            set { SetPropertyValue<string>("AccountNumber", ref fAccountNumber, value); }
        }

        private char fOrgActorType;
        [Size(1)]
        public char OrgActorType
        {
            get { return fOrgActorType; }
            set { SetPropertyValue<char>("OrgActorType", ref fOrgActorType, value); }
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
        [Association("LegalInvoiceArtifactOrgActorReferencesLegalInvoiceArtifact")]
        public LegalInvoiceArtifact LegalInvoiceArtifactId
        {
            get { return fLegalInvoiceArtifactId; }
            set { SetPropertyValue<LegalInvoiceArtifact>("LegalInvoiceArtifactId", ref fLegalInvoiceArtifactId, value); }
        }
        #endregion
    }
}
