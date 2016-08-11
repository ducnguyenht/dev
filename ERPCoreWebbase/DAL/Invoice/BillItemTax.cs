using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Item;

namespace NAS.DAL.Invoice
{
    public class BillItemTax : XPCustomObject
    {
        public BillItemTax(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        Guid fBillItemTaxId;
        [Key(true)]
        public Guid BillItemTaxId
        {
            get { return fBillItemTaxId; }
            set { SetPropertyValue<Guid>("BillItemTaxId", ref fBillItemTaxId, value); }
        }

        double fTaxInNumber;
        public double TaxInNumber
        {
            get { return fTaxInNumber; }
            set { SetPropertyValue<double>("TaxInNumber", ref fTaxInNumber, value); }
        }
        double fTaxInPercentage;
        public double TaxInPercentage
        {
            get { return fTaxInPercentage; }
            set { SetPropertyValue<double>("TaxInPercentage", ref fTaxInPercentage, value); }
        }

        BillItem fBillItemId;
        [Association(@"BillItemTaxReferencesBillItem")]
        public BillItem BillItemId
        {
            get { return fBillItemId; }
            set { SetPropertyValue<BillItem>("BillItemId", ref fBillItemId, value); }
        }

        ItemTax fItemTaxId;
        [Association(@"BillItemTaxReferencesItemTax")]
        public ItemTax ItemTaxId
        {
            get { return fItemTaxId; }
            set { SetPropertyValue<ItemTax>("ItemTaxId", ref fItemTaxId, value); }
        }
    }
}
