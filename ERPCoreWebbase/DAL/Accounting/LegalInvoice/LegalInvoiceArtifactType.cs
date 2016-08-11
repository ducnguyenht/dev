using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;


namespace NAS.DAL.Accounting.LegalInvoice
{
    public partial class LegalInvoiceArtifactType : XPCustomObject
    {
        public LegalInvoiceArtifactType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fLegalInvoiceArtifactTypeId;
        [Key(true)]
        public Guid LegalInvoiceArtifactTypeId
        {
            get { return fLegalInvoiceArtifactTypeId; }
            set { SetPropertyValue<Guid>("LegalInvoiceArtifactTypeId", ref fLegalInvoiceArtifactTypeId, value); }
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

        private char fCategory;
        [Size(1)]
        public char Category
        {
            get { return fCategory; }
            set { SetPropertyValue<char>("Category", ref fCategory, value); }
        }

        private short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        #endregion

        #region References
        [Association(@"LegalInvoiceArtifactReferencesLegalInvoiceArtifactType", typeof(LegalInvoiceArtifact))]
        public XPCollection<LegalInvoiceArtifact> LegalInvoiceArtifacts { get { return GetCollection<LegalInvoiceArtifact>("LegalInvoiceArtifacts"); } }

        [Association(@"NAS.DAL.Invoice.SaleInvoiceArtifaceReferencesLegalInvoiceArtifactType", typeof(NAS.DAL.Invoice.SaleInvoiceArtiface))]
        public XPCollection<NAS.DAL.Invoice.SaleInvoiceArtiface> SaleInvoiceArtifaces { get { return GetCollection<NAS.DAL.Invoice.SaleInvoiceArtiface>("SaleInvoiceArtifaces"); } }
        #endregion
    }
}
