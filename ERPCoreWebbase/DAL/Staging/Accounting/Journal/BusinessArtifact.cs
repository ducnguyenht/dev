using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Interface;

namespace NAS.DAL.Staging.Accounting.Journal
{
    public partial class BusinessArtifact : XPCustomObject, IDALValidate
    {
        public BusinessArtifact(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Properties
        private Guid fBusinessArtifactId;
        [Key(true)]
        public Guid BusinessArtifactId
        {
            get { return fBusinessArtifactId; }
            set { SetPropertyValue<Guid>("BusinessArtifact", ref fBusinessArtifactId, value); }
        }

        BusinessArtifact fParentBusinessArtifactId;
        [Association(@"BusinessArtifactParentBusinessArtifact")]
        public BusinessArtifact ParentBusinessArtifactId
        {
            get { return fParentBusinessArtifactId; }
            set { SetPropertyValue<BusinessArtifact>("ParentBusinessArtifactId", ref fParentBusinessArtifactId, value); }
        }

        private string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        private DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue<DateTime>("CreateDate", ref fCreateDate, value); }
        }

        private string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        private DateTime fIssueDate;
        public DateTime IssueDate
        {
            get { return fIssueDate; }
            set { SetPropertyValue<DateTime>("IssueDate", ref fIssueDate, value); }
        }

        private DateTime fLastUpdateDate;
        public DateTime LastUpdateDate
        {
            get { return fLastUpdateDate; }
            set { SetPropertyValue<DateTime>("LastUpdateDate", ref fLastUpdateDate, value); }
        }

        private string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<String>("Name", ref fName, value); }
        }

        private byte fRowStatus;
        public byte RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<byte>("RowStatus", ref fRowStatus, value); }
        }
        #endregion

        #region References
        private BusinessArtifactType fBusinessArtifactTypeId;
        [Association("BusinessArtifactlReferencesBusinessArtifactType")]
        public BusinessArtifactType BusinessArtifactTypeId
        {
            get { return fBusinessArtifactTypeId; }
            set { SetPropertyValue<BusinessArtifactType>("BusinessArtifactTypeId", ref fBusinessArtifactTypeId, value); }
        }

        [Association(@"AccountingTransactionReferencesBusinessArtifact", typeof(AccountingTransaction))]
        public XPCollection<AccountingTransaction> AccountingTransactions { get { return GetCollection<AccountingTransaction>("AccountingTransactions"); } }

        [Association("BusinessArtifactParentBusinessArtifact")]
        public XPCollection<BusinessArtifact> BusinessArtifacts
        {
            get
            {
                return GetCollection<BusinessArtifact>("BusinessArtifacts");
            }
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
