using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.Invoice;

namespace NAS.DAL.Nomenclature.Item
{
    public partial class ItemTax : XPCustomObject
    {
        public ItemTax(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fItemTaxId;
        [Key(true)]
        public Guid ItemTaxId
        {
            get { return fItemTaxId; }
            set { SetPropertyValue<Guid>("ItemTaxId", ref fItemTaxId, value); }
        }

        private short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        private DateTime fValidFromDate;
        public DateTime ValidFromDate
        {
            get { return fValidFromDate; }
            set { SetPropertyValue<DateTime>("ValidFromDate", ref fValidFromDate, value); }
        }

        private DateTime fValidToDate;
        public DateTime ValidToDate
        {
            get { return fValidToDate; }
            set { SetPropertyValue<DateTime>("ValidToDate", ref fValidToDate, value); }
        }

        private Item fItemId;
        [Association("ItemTaxReferencesItem")]
        public Item ItemId
        {
            get { return fItemId; }
            set { SetPropertyValue<Item>("ItemId", ref fItemId, value); }
        }

        private NAS.DAL.Accounting.Tax.Tax fTaxId;
        [Association("ItemTaxReferencesNAS.DAL.Accounting.Tax.Tax")]
        public NAS.DAL.Accounting.Tax.Tax TaxId
        {
            get { return fTaxId; }
            set { SetPropertyValue<NAS.DAL.Accounting.Tax.Tax>("TaxId", ref fTaxId, value); }
        }

        [Association(@"BillItemTaxReferencesItemTax", typeof(BillItemTax))]
        public XPCollection<BillItemTax> BillItemTaxs { get { return GetCollection<BillItemTax>("BillItemTaxs"); } }

        private bool checkIsDupplicateTax()
        {
            Session session = XpoHelper.GetNewSession();

            if (TaxId != null && ItemId != null)
            {
                CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("TaxId!Key", TaxId.TaxId),
                    new BinaryOperator("ItemId!Key", ItemId.ItemId));
                ItemTax obj = session.FindObject<ItemTax>(criteria);
                if (obj != null)
                    return true;
            }

            return false;
        }

        protected override void OnSaving()
        {
            if (ItemTaxId == Guid.Empty && checkIsDupplicateTax())
                throw new Exception(String.Format("Thuế suất {0} đã tồn tại, vui lòng chọn thuế suất!", TaxId.Code));
            base.OnSaving();
        }
    }
}
