using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Item;

namespace NAS.DAL.Accounting.LegalInvoice
{
    public partial class LegalInvoiceArtifactDetail : XPCustomObject
    {
        public LegalInvoiceArtifactDetail(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fLegalInvoiceArtifactDetailId;
        [Key(true)]
        public Guid LegalInvoiceArtifactDetailId
        {
            get { return fLegalInvoiceArtifactDetailId; }
            set { SetPropertyValue<Guid>("LegalInvoiceArtifactDetailId", ref fLegalInvoiceArtifactDetailId, value); }
        }

        private double fPrice;
        public double Price
        {
            get { return fPrice; }
            set { SetPropertyValue<double>("Price", ref fPrice, value); }
        }

        private double fTotal;
        public double Total
        {
            get { return fTotal; }
            set { SetPropertyValue<double>("Total", ref fTotal, value); }
        }

        private double fAmount;
        public double Amount
        {
            get { return fAmount; }
            set { SetPropertyValue<double>("Amount", ref fAmount, value); }
        }

        private short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        private double fItemPromotionInPercentage;
        public double ItemPromotionInPercentage
        {
            get { return fItemPromotionInPercentage; }
            set { SetPropertyValue<double>("ItemPromotionInPercentage", ref fItemPromotionInPercentage, value); }
        }

        private double fItemTaxInPercentage;
        public double ItemTaxInPercentage
        {
            get { return fItemTaxInPercentage; }
            set { SetPropertyValue<double>("ItemTaxInPercentage", ref fItemTaxInPercentage, value); }
        }

       
        #endregion

        #region Reference
        private LegalInvoiceArtifact fLegalInvoiceArtifactId;
        [Association("LegalInvoiceArtifactDetailReferencesLegalInvoiceArtifact")]
        public LegalInvoiceArtifact LegalInvoiceArtifactId
        {
            get { return fLegalInvoiceArtifactId; }
            set { SetPropertyValue<LegalInvoiceArtifact>("LegalInvoiceArtifactId", ref fLegalInvoiceArtifactId, value); }
        }

        private Item fItemId;
        [Association("LegalInvoiceArtifactDetailReferencesItem")]
        public Item ItemId
        {
            get { return fItemId; }
            set { SetPropertyValue<Item>("ItemId", ref fItemId, value); }
        }

        private Unit fUnitId;
        [Association("LegalInvoiceArtifactDetailReferencesUnit")]
        public Unit UnitId
        {
            get { return fUnitId; }
            set { SetPropertyValue<Unit>("UnitId", ref fUnitId, value); }
        }
        #endregion
    }
}
