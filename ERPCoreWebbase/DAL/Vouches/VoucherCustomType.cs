using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.CMS.ObjectDocument;

namespace NAS.DAL.Vouches
{
    public class VoucherCustomType : XPCustomObject
    {
        public VoucherCustomType(Session session)
            : base(session)
        {
            
        }

        public override void AfterConstruction() { base.AfterConstruction(); }

        // Fields...
        private ObjectType _ObjectTypeId;
        private Vouches _VoucherId;
        private Guid _VoucherCustomTypeId;
        [Key(true)]
        public Guid VoucherCustomTypeId
        {
            get
            {
                return _VoucherCustomTypeId;
            }
            set
            {
                SetPropertyValue("VoucherCustomTypeId", ref _VoucherCustomTypeId, value);
            }
        }


        [Association("VoucherCustomTypeReferenceVouches")]
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


        [Association("VoucherCustomTypeReferenceObjectType")]
        public ObjectType ObjectTypeId
        {
            get
            {
                return _ObjectTypeId;
            }
            set
            {
                SetPropertyValue("ObjectTypeId", ref _ObjectTypeId, value);
            }
        }

    }
}
