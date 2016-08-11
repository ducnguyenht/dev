using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Sales.Price
{
    public class PricePolicyCondition : XPCustomObject
    {
        public PricePolicyCondition(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        //attribute
        private Guid _PricePolicyConditionId;
        private DateTime _CreateDate;
        private bool _IsIncluding;
        private DateTime _IssueDate;
        private DateTime _LastUpdateDate;
        private short _RowStatus;
        private PricePolicy _PricePolicyId;

        //Properties
        [Key(true)]
        public Guid PricePolicyConditionId
        {
            get
            {
                return _PricePolicyConditionId;
            }
            set
            {
                SetPropertyValue<Guid>("PricePolicyConditionId", ref _PricePolicyConditionId, value);
            }
        }

        public DateTime CreateDate
        {
            get
            {
                return _CreateDate;
            }
            set
            {
                SetPropertyValue<DateTime>("CreateDate", ref _CreateDate, value);
            }
        }

        public bool IsIncluding
        {
            get
            {
                return _IsIncluding;
            }
            set
            {
                SetPropertyValue<bool>("IsIncluding", ref _IsIncluding, value);
            }
        }

        public DateTime IssueDate
        {
            get
            {
                return _IssueDate;
            }
            set
            {
                SetPropertyValue<DateTime>("IssueDate", ref _IssueDate, value);
            }
        }

        public DateTime LastUpdateDate
        {
            get
            {
                return _LastUpdateDate;
            }
            set
            {
                SetPropertyValue<DateTime>("LastUpdateDate", ref _LastUpdateDate, value);
            }
        }

        public short RowStatus
        {
            get
            {
                return _RowStatus;
            }
            set
            {
                SetPropertyValue<short>("RowStatus", ref _RowStatus, value);
            }
        }

        [Association(@"PricePolicyConditionReferencesPricePolicy")]
        public PricePolicy PricePolicyId
        {
            get
            {
                return _PricePolicyId;
            }
            set
            {
                SetPropertyValue<PricePolicy>("PricePolicyId", ref _PricePolicyId, value);
            }
        }
    }
}
