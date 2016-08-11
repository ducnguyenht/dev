using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Invoice.InvoiceDynamicField
{
    public class BillObject : XPCustomObject
    {
        public BillObject(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fBillObjectId;
        [Key(true)]
        public Guid BillObjectId
        {
            get { return fBillObjectId; }
            set { SetPropertyValue<Guid>("BillObjectId", ref fBillObjectId, value); }
        }

        private NAS.DAL.CMS.ObjectDocument.Object fObjectId;
        [Association(@"BillObjectReferencesNAS.DAL.CMS.ObjectDocument.Object")]
        public NAS.DAL.CMS.ObjectDocument.Object ObjectId
        {
            get { return fObjectId; }
            set { SetPropertyValue<NAS.DAL.CMS.ObjectDocument.Object>("ObjectId", ref fObjectId, value); }
        }

        private Bill fBillId;
        [Association(@"BillObjectReferencesBill")]
        public Bill BillId
        {
            get { return fBillId; }
            set { SetPropertyValue<Bill>("BillId", ref fBillId, value); }
        }
    }
}
