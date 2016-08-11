//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Item;
namespace NAS.DAL.Invoice
{

    public partial class SaleInvoiceArtiface : XPCustomObject
    {
        #region Properties
        Guid fSaleInvoiceArtifaceId;
		[Key(true)]
        public Guid SaleInvoiceArtifaceId
        {
            get { return fSaleInvoiceArtifaceId; }
            set { SetPropertyValue<Guid>("SaleInvoiceArtifaceId", ref fSaleInvoiceArtifaceId, value); }
		}
        string fIssuedArtifaceCode;
        public string IssuedArtifaceCode
        {
            get { return fIssuedArtifaceCode; }
            set { SetPropertyValue<string>("IssuedArtifaceCode", ref fIssuedArtifaceCode, value); }
        }
        DateTime fIssuedDate;
        public DateTime IssuedDate
        {
            get { return fIssuedDate; }
            set { SetPropertyValue<DateTime>("IssuedDate", ref fIssuedDate, value); }
        }
        short fIssuedNumber;
        public short IssuedNumber
        {
            get { return fIssuedNumber; }
            set { SetPropertyValue<short>("IssuedNumber", ref fIssuedNumber, value); }
        }
        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        string fSeriesNumber;
        public string SeriesNumber
        {
            get { return fSeriesNumber; }
            set { SetPropertyValue<string>("SeriesNumber", ref fSeriesNumber, value); }
        }
        #endregion

        #region References
        Bill fBillId; 
        [Association(@"SaleInvoiceArtifaceReferencesBill")]
        public Bill BillId
        {
            get { return fBillId; }
            set { SetPropertyValue<Bill>("BillId", ref fBillId, value); }
        }

        private NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType fLegalInvoiceArtifactTypeId;
        [Association(@"NAS.DAL.Invoice.SaleInvoiceArtifaceReferencesLegalInvoiceArtifactType")]
        public NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType LegalInvoiceArtifactTypeId
        {
            get { return fLegalInvoiceArtifactTypeId; }
            set { SetPropertyValue<NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType>("LegalInvoiceArtifactTypeId", ref fLegalInvoiceArtifactTypeId, value); }
        }
        #endregion
    }

}
