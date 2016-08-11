using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Interface;

namespace NAS.DAL.Staging.Accounting.Journal
{
    public partial class AccountActor : XPCustomObject, IDALValidate
    {
        public AccountActor(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fAccountActorId;
        [Key(true)]
        public Guid AccountActorId
        {
            get { return fAccountActorId; }
            set { SetPropertyValue<Guid>("AccountActorId", ref fAccountActorId, value); }
        }
        private string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }
        private string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        private Guid fRefid;
        public Guid Refid
        {
            get { return fRefid; }
            set { SetPropertyValue<Guid>("Refid", ref fRefid, value); }
        }
        private byte fRowStatus;
        public byte RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<byte>("RowStatus", ref fRowStatus, value); }
        }
        #endregion

        #region References
        private AccountActorType fAccountActorTypeId;
        [Association("AccountActorReferencesAccountActorType")]
        public AccountActorType AccountActorTypeId
        {
            get { return fAccountActorTypeId; }
            set { SetPropertyValue("AccountActorTypeId", ref fAccountActorTypeId, value); }
        }
        #endregion

        #region validate database

        public bool ValidateParameter()
        {
            if (this.Name.Equals(string.Empty))
                return false;
            return true;
        }

        public bool ValidateUnique()
        {
            return true;
        }

        public bool IsExist()
        {
            return true;
            //throw new NotImplementedException();
        }

        protected override void OnSaving()
        {
            if (ValidateParameter())
            {
                if (ValidateUnique())
                    base.OnSaving();
            }
        }
        #endregion
    }
}
