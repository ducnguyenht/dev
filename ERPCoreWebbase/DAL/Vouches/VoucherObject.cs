using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Vouches
{
    public class VoucherObject : XPCustomObject
    {
        public VoucherObject(Session session)
            : base(session)
        {
            
        }

        public override void AfterConstruction() { base.AfterConstruction(); }

        // Fields...
        private NAS.DAL.CMS.ObjectDocument.Object _ObjectId;
        private Vouches _VoucherId;
        private Guid _VoucherObjectId;
        [Key(true)]
        public Guid VoucherObjectId
        {
            get
            {
                return _VoucherObjectId;
            }
            set
            {
                SetPropertyValue("VoucherObjectId", ref _VoucherObjectId, value);
            }
        }


        [Association("VoucherObjectReferenceVouches")]
        public Vouches VoucherId
        {
            get
            {
                return _VoucherId;
            }
            set
            {
                SetPropertyValue("VoucherId", ref _VoucherId, value);
            }
        }


        [Association("VoucherObjectReferenceObject")]
        public NAS.DAL.CMS.ObjectDocument.Object ObjectId
        {
            get
            {
                return _ObjectId;
            }
            set
            {
                SetPropertyValue("ObjectId", ref _ObjectId, value);
            }
        }

    }
}
