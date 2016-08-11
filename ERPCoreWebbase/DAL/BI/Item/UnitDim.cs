using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting;
using NAS.DAL.BI.Accounting.ItemInventory;

namespace NAS.DAL.BI.Item
{
    public class UnitDim:XPCustomObject
    {
        public UnitDim(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
        #endregion

        #region Properties
        int fUnitDimId;
        [Key(true)]
        public int UnitDimId
        {
            get { return fUnitDimId; }
            set { SetPropertyValue<int>("UnitDimId", ref fUnitDimId, value); }
        }

        Guid fRefId;
        public Guid RefId
        {
            get { return fRefId; }
            set
            {
                SetPropertyValue<Guid>("RefId", ref fRefId, value);
            }
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
            get {return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        string fName;
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
        #endregion

        #region References
        [Association(@"FinancialRevenueByItem_FactReferencesUnitDim", typeof(FinancialRevenueByItem_Fact)), Aggregated]
        public XPCollection<FinancialRevenueByItem_Fact> FinancialRevenueByItem_Facts
        {
            get { return GetCollection<FinancialRevenueByItem_Fact>("FinancialRevenueByItem_Facts"); }
        }

        [Association(@"FinancialItemInventorySummary_Fact-UnitDim", typeof(FinancialItemInventorySummary_Fact))]
        public XPCollection<FinancialItemInventorySummary_Fact> FinancialItemInventorySummary_Facts { get { return GetCollection<FinancialItemInventorySummary_Fact>("FinancialItemInventorySummary_Facts"); } }
        #endregion
    }
}
