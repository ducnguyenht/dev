using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Accounting.Journal
{
    public class TransactionObject : XPCustomObject
    {
        public TransactionObject(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        // Fields...
        private Guid _TransactionObjectId;
        private NAS.DAL.CMS.ObjectDocument.Object _ObjectId;
        private Transaction _TransactionId;

        [Key(true)]
        public Guid TransactionObjectId
        {
            get
            {
                return _TransactionObjectId;
            }
            set
            {
                SetPropertyValue("TransactionObjectId", ref _TransactionObjectId, value);
            }
        }

        [Association("TransactionObjectReferenceTransaction")]
        public Transaction TransactionId
        {
            get
            {
                return _TransactionId;
            }
            set
            {
                SetPropertyValue("TransactionId", ref _TransactionId, value);
            }
        }


        [Association("TransactionObjectReferenceObject")]
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
