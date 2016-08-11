using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Sales.Price
{
    public class PricePolicy : XPCustomObject
    {
        public PricePolicy(Session session)
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
        private Guid _PricePolicyId;
        private string _Code;
        private DateTime _CreateDate;
        private string _Description;
        private DateTime _IssueDate;
        private DateTime _LastUpdateDate;
        private string _Name;
        private int _Priority;
        private short _RowStatus;
        private DateTime _ValidFrom;
        private DateTime _ValidTo;
        private PricePolicyType _PricePolicyTypeId;

        //Properties
        [Key(true)]
        public Guid PricePolicyId
        {
            get
            {
                return _PricePolicyId;
            }
            set
            {
                SetPropertyValue<Guid>("PricePolicyId", ref _PricePolicyId, value);
            }
        }

        [Size(36)]
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                SetPropertyValue<string>("Code", ref _Code, value);
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

        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                SetPropertyValue<string>("Description", ref _Description, value);
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

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                SetPropertyValue<String>("Name", ref _Name, value);
            }
        }

        public int Priority
        {
            get
            {
                return _Priority;
            }
            set
            {
                SetPropertyValue<int>("Priority", ref _Priority, value);
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

        public DateTime ValidFrom
        {
            get
            {
                return _ValidFrom;
            }
            set
            {
                SetPropertyValue<DateTime>("ValidFrom", ref _ValidFrom, value);
            }
        }

        public DateTime ValidTo
        {
            get
            {
                return _ValidTo;
            }
            set
            {
                SetPropertyValue<DateTime>("ValidTo", ref _ValidTo, value);
            }
        }

        [Association(@"PricePolicyReferencesPricePolicyType")]
        public PricePolicyType PricePolicyTypeId
        {
            get
            {
                return _PricePolicyTypeId;
            }
            set
            {
                SetPropertyValue<PricePolicyType>("PricePolicyTypeId", ref _PricePolicyTypeId, value);
            }
        }

        [Association(@"PricePolicyConditionReferencesPricePolicy", typeof(PricePolicyCondition))]
        public XPCollection<PricePolicyCondition> PricePolicyCondititions
        {
            get
            {
                return GetCollection<PricePolicyCondition>("PricePolicyCondititions");
            }
        }

        [Association(@"PriceCaculatorReferencesPricePolicy", typeof(PriceCaculator))]
        public XPCollection<PriceCaculator> PriceCaculators
        {
            get
            {
                return GetCollection<PriceCaculator>("PriceCaculators");
            }
        }
    }
}
