using System;
using DevExpress.Xpo;
using NAS.DAL.Staging.Accounting.Journal;
using NAS.DAL.Accounting.AccountChart;

namespace NAS.DAL.Accounting.Configure
{

    public class AllocationAccountTemplate : XPCustomObject
    {
        public AllocationAccountTemplate(Session session)
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
        // Fields...
        private Allocation _AllocationId;
        private Account _AccountId;
        private AccountActorType _AccountActorTypeId;
        private string _Name;
        private string _Description;
        private char _CardSite;
        private Guid _AllocationAccountTemplateId;

        [Key(true)]
        public Guid AllocationAccountTemplateId
        {
            get
            {
                return _AllocationAccountTemplateId;
            }
            set
            {
                SetPropertyValue("AllocationAccountTemplateId", ref _AllocationAccountTemplateId, value);
            }
        }


        public char CardSite
        {
            get
            {
                return _CardSite;
            }
            set
            {
                SetPropertyValue("CardSite", ref _CardSite, value);
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
                SetPropertyValue("Description", ref _Description, value);
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
                SetPropertyValue("Name", ref _Name, value);
            }
        }

        /*2013-11-27 Khoa.Truong DEL START*/
        //[Association("AllocationTypeTemplatesRefencesAccountActorType")]
        //public AccountActorType AccountActorTypeId
        //{
        //    get
        //    {
        //        return _AccountActorTypeId;
        //    }
        //    set
        //    {
        //        SetPropertyValue("AccountActorTypeId", ref _AccountActorTypeId, value);
        //    }
        //}
        /*2013-11-27 Khoa.Truong DEL END*/

        [Association("AllocationAccountTemplatesRefencesAccount",typeof(Account))]
        public Account AccountId
        {
            get
            {
                return _AccountId;
            }
            set
            {
                SetPropertyValue("AccountId", ref _AccountId, value);
            }
        }


        [Association("AllocationTemplatesRefencesAllocation")]
        public Allocation AllocationId
        {
            get
            {
                return _AllocationId;
            }
            set
            {
                SetPropertyValue("AllocationId", ref _AllocationId, value);
            }
        }
    }

}