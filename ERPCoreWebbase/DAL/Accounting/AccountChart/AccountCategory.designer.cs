using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Accounting.AccountChart
{
    public partial class AccountCategory : XPCustomObject
    {
        Guid fAccountCategoryId;
        [Key(true)]
        public Guid AccountCategoryId
        {
            get { return fAccountCategoryId; }
            set { SetPropertyValue<Guid>("AccountCategoryId", ref fAccountCategoryId, value); }
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

        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        [Association(@"AccountTypeReferencesAccountCategory", typeof(AccountType)), Aggregated]
        public XPCollection<AccountType> AccountTypes 
        { 
            get { return GetCollection<AccountType>("AccountTypes"); } 
        }
    }
}
