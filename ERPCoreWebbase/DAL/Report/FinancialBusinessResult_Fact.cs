using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Account;

namespace NAS.DAL.Report
{
    public class FinancialBusinessResult_Fact : XPCustomObject
    {
        int fFinancialBusinessResult_FactId;
        [Key(true)]
        public int FinancialBusinessResult_FactId
        {
            get { return fFinancialBusinessResult_FactId; }
            set { SetPropertyValue<int>("FinancialBusinessResult_FactId", ref fFinancialBusinessResult_FactId, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }


        string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }
     
        double fBalance;
        public double Balance
        {
            get { return fBalance; }
            set { SetPropertyValue<double>("Balance", ref fBalance, value); }
        }

        double fLastBalance;
        public double LastBalance
        {
            get { return fLastBalance; }
            set { SetPropertyValue<double>("LastBalance", ref fLastBalance, value); }
        }

        string fDebitAccount;
        public string DebitAccount
        {
            get { return fDebitAccount; }
            set { SetPropertyValue<string>("DebitAccount", ref fDebitAccount, value); }
        }

        string fCreditAccount;
        public string CreditAccount
        {
            get { return fCreditAccount; }
            set { SetPropertyValue<string>("CreditAccount", ref fCreditAccount, value); }
        }

        string fExpresstion;
        public string Expresstion
        {
            get { return fExpresstion; }
            set { SetPropertyValue<string>("Expresstion", ref fExpresstion, value); }
        }

        bool fDetail;
        public bool Detail
        {
            get { return fDetail; }
            set { SetPropertyValue<bool>("Detail", ref fDetail, value); }
        }        

        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }

        FinancialAccountDim fFinancialAccountDimId;
        [Association(@"FinancialBusinessResult_FactReferencesFinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return fFinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref fFinancialAccountDimId, value); }
        }

        public FinancialBusinessResult_Fact(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
