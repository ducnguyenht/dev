using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Invoice.InvoiceDynamicField
{
    public class BillCustomType: XPCustomObject
    {
        public BillCustomType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Guid fBillCustomTypeId;
        [Key(true)]
        public Guid BillCustomTypeId
        {
            get { return fBillCustomTypeId; }
            set { SetPropertyValue<Guid>("BillCustomTypeId", ref fBillCustomTypeId, value); }
        }

        private NAS.DAL.CMS.ObjectDocument.ObjectType fObjectTypeId;
        [Association(@"BillCustomTypeReferencesNAS.DAL.CMS.ObjectDocument.ObjectType")]
        public NAS.DAL.CMS.ObjectDocument.ObjectType ObjectTypeId
        {
            get { return fObjectTypeId; }
            set { SetPropertyValue<NAS.DAL.CMS.ObjectDocument.ObjectType>("ObjectTypeId", ref fObjectTypeId, value); }
        }

        private Bill fBillId;
        [Association(@"BillCustomTypeReferencesBill")]
        public Bill BillId
        {
            get { return fBillId; }
            set { SetPropertyValue<Bill>("BillId", ref fBillId, value); }
        }
    }
}
