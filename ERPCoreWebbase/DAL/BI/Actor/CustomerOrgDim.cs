using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting;
using NAS.DAL.BI.Accounting.SupplierLiability;
using NAS.DAL.BI.Accounting.CustomerLiability;

namespace NAS.DAL.BI.Actor
{
    public class CustomerOrgDim : XPCustomObject
    {
        public CustomerOrgDim(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
        #endregion

        #region Properties
        int fCustomerOrgDimId;
        [Key(true)]
        public int CustomerOrgDimId
        {
            get { return fCustomerOrgDimId; }
            set { SetPropertyValue<int>("CustomerOrgDimId", ref fCustomerOrgDimId, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        Guid fRefId;
        public Guid RefId
        {
            get { return fRefId; }
            set { SetPropertyValue<Guid>("RefId", ref fRefId, value); }
        }

        short fRowStatus;
        public short RowStatus
        {
            get { return fRowStatus; }
            set { SetPropertyValue<short>("RowStatus", ref fRowStatus, value); }
        }
        #endregion

        #region References
        [Association(@"FinancialRevenueByItem_FactReferencesCustomerOrgDim", typeof(FinancialRevenueByItem_Fact)), Aggregated]
        public XPCollection<FinancialRevenueByItem_Fact> FinancialRevenueByItem_Facts
        {
            get { return GetCollection<FinancialRevenueByItem_Fact>("FinancialRevenueByItem_Facts"); }
        }


        [Association(@"FinancialCustomerLiabilitySummary_FactReferencesCustomerOrgDim", typeof(FinancialCustomerLiabilitySummary_Fact))]
        public XPCollection<FinancialCustomerLiabilitySummary_Fact> FinancialCustomerLiabilitySummary_Facts { get { return GetCollection<FinancialCustomerLiabilitySummary_Fact>("FinancialCustomerLiabilitySummary_Facts"); } }
        #endregion
    }
}
