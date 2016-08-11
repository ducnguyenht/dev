using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Invoice;

namespace NAS.DAL.Accounting.Tax
{
    public partial class Tax : XPCustomObject
    {
        private NAS.DAL.Invoice.TaxType fTaxTypeId;
        [Association("TaxReferencesNAS.DAL.Invoice.TaxType")]
        public NAS.DAL.Invoice.TaxType TaxTypeId
        {
            get { return fTaxTypeId; }
            set { SetPropertyValue<NAS.DAL.Invoice.TaxType>("TaxTypeId", ref fTaxTypeId, value); }
        }

        private Guid fTaxId;
        [Key(true)]
        public Guid TaxId
        {
            get { return fTaxId; }
            set { SetPropertyValue<Guid>("TaxId", ref fTaxId, value); }
        }

        private short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        private double fPercentage;
        public double Percentage
        {
            get { return fPercentage; }
            set { SetPropertyValue<double>("Percentage", ref fPercentage, value); }
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

        private bool fIsInternal;
        public bool IsInternal
        {
            get { return fIsInternal; }
            set { SetPropertyValue<bool>("IsInternal", ref fIsInternal, value); }
        }

        private string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        public Tax(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private double fAmount;
        public double Amount
        {
            get { return fPercentage; }
            set { SetPropertyValue<double>("Amount", ref fAmount, value); }
        }

        [Association(@"ItemTaxReferencesNAS.DAL.Accounting.Tax.Tax", typeof(ItemTax))]
        public XPCollection<ItemTax> ItemTaxes { get { return GetCollection<ItemTax>("ItemTaxes"); } }

        [Association(@"BillTaxReferencesTax", typeof(BillTax))]
        public XPCollection<BillTax> BillTaxs { get { return GetCollection<BillTax>("BillTaxs"); } }
    }
}
