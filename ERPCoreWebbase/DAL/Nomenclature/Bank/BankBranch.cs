using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace NAS.DAL.Nomenclature.Bank
{
    public partial class BankBranch : XPCustomObject
    {
        public BankBranch(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        
        #region Properties
        private Guid fBankBranchId;
        [Key(true)]
        public Guid BankBranchId
        {
            get { return fBankBranchId; }
            set { SetPropertyValue<Guid>("BankBranchId", ref fBankBranchId, value); }
        }

        private string fAddress;
        public string Address
        {
            get { return fAddress; }
            set { SetPropertyValue<string>("Address", ref fAddress, value); }
        }

        private string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
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

        private string fPhoneFax;
        public string PhoneFax
        {
            get { return fPhoneFax; }
            set { SetPropertyValue<string>("PhoneFax", ref fPhoneFax, value); }
        }
        #endregion

        #region References
        Bank fBankId;
        [Association(@"BankBranchReferencesBank")]
        public Bank BankId
        {
            get { return fBankId; }
            set { SetPropertyValue<Bank>("BankId", ref fBankId, value); }
        }
        #endregion
    }
}
