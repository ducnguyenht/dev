using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Accounting.AccountChart
{
    public partial class AccountType : XPCustomObject
    {
        AccountCategory fAccountCategoryId;
        [Association(@"AccountTypeReferencesAccountCategory")]
        public AccountCategory AccountCategoryId
        {
            get { return fAccountCategoryId; }
            set { SetPropertyValue<AccountCategory>("AccountCategoryId", ref fAccountCategoryId, value); }
        }

        Guid fAccountTypeId;
        [Key(true)]
        public Guid AccountTypeId
        {
            get { return fAccountTypeId; }
            set { SetPropertyValue<Guid>("AccountTypeId", ref fAccountTypeId, value); }
        }
        string fCode;
        [Size(36)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fDescription;
        [Size(1024)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        string fName;
        [Size(255)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        
        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        [Association(@"AccountReferencesAccountType", typeof(Account)), Aggregated]
        public XPCollection<Account> Accounts { get { return GetCollection<Account>("Accounts"); } }
    }
}
