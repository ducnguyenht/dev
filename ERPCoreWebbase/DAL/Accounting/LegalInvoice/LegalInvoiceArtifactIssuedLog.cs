using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Accounting.LegalInvoice
{
    public partial class LegalInvoiceArtifactIssuedLog : XPCustomObject
    {
        public LegalInvoiceArtifactIssuedLog(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fLegalInvoiceArtifactIsssuedLogId;
        [Key(true)]
        public Guid LegalInvoiceArtifactIssuedLogId
        {
            get { return fLegalInvoiceArtifactIsssuedLogId; }
            set { SetPropertyValue<Guid>("LegalInvoiceArtifactIssuedLogId", ref fLegalInvoiceArtifactIsssuedLogId, value); }
        }

        private string fIssuedType;
        public string IssuedType
        {
            get { return fIssuedType; }
            set { SetPropertyValue<string>("IssuedType", ref fIssuedType, value); }
        }

        private DateTime fIssuedDate;
        public DateTime IssuedDate
        {
            get { return fIssuedDate; }
            set { SetPropertyValue<DateTime>("IssuedDate", ref fIssuedDate, value); }
        }

        private ushort fIssuedNumber;
        public ushort IssuedNumber
        {
            get { return fIssuedNumber; }
            set { SetPropertyValue<ushort>("IssuedNumber", ref fIssuedNumber, value); }
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
        [Association("LegalInvoiceArtifactIssuedLogReferencesLegalInvoiceArtifact")]
        public LegalInvoiceArtifact LegalInvoiceArtifactId
        {
            get { return fLegalInvoiceArtifactId; }
            set { SetPropertyValue<LegalInvoiceArtifact>("LegalInvoiceArtifactId", ref fLegalInvoiceArtifactId, value); }
        }
        #endregion
    }
}
