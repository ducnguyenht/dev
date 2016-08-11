using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Account;

namespace NAS.DAL.Report
{
    public class FinancialBalanceAccount_Fact : XPCustomObject
    {
        int fFinancialBalanceAccount_FactId;
        [Key(true)]
        public int FinancialBalanceAccount_FactId
        {
            get { return fFinancialBalanceAccount_FactId; }
            set { SetPropertyValue<int>("FinancialBalanceAccount_FactId", ref fFinancialBalanceAccount_FactId, value); }
        }

        int fOrderNumber;        
        public int OrderNumber
        {
            get { return fOrderNumber; }
            set { SetPropertyValue<int>("OrderNumber", ref fOrderNumber, value); }
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

        double fBeginBalance;
        public double BeginBalance
        {
            get { return fBeginBalance; }
            set { SetPropertyValue<double>("BeginBalance", ref fBeginBalance, value); }
        }


        double fEndBalance;
        public double EndBalance
        {
            get { return fEndBalance; }
            set { SetPropertyValue<double>("EndBalance", ref fEndBalance, value); }
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
        [Association(@"FinancialBalanceAccount_FactReferencesFinancialAccountDim")]
        public FinancialAccountDim FinancialAccountDimId
        {
            get { return fFinancialAccountDimId; }
            set { SetPropertyValue<FinancialAccountDim>("FinancialAccountDimId", ref fFinancialAccountDimId, value); }
        }
      
        public FinancialBalanceAccount_Fact(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }
}
