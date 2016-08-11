using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.Finance.DiaryJournal;

namespace NAS.DAL.BI.Accounting
{
    public class FinancialAssetDim:XPCustomObject
    {
        public FinancialAssetDim(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic
        #endregion

        #region Properties
        int fFinancialAssetDimId;
        [Key(true)]
        public int FinancialAssetDimId
        {
            get { return fFinancialAssetDimId; }
            set { SetPropertyValue<int>("FinancialAssetDimId", ref fFinancialAssetDimId, value); }
        }

        int fParentFinancialAssetDimId;
        public int ParentFinancialAssetDimId
        {
            get { return fParentFinancialAssetDimId; }
            set { SetPropertyValue<int>("ParentFinancialAssetDimId", ref fParentFinancialAssetDimId, value); }
        }

        char fNodeType;
        public char NodeType
        {
            get { return fNodeType; }
            set { SetPropertyValue<char>("NodeType", ref fNodeType, value); }
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
        [Association("FinancialDoubleEntry_FactReferencesFinancialAssetDim")]
        public XPCollection<FinancialDoubleEntry_Fact> Relations
        {
            get
            {
                return GetCollection<FinancialDoubleEntry_Fact>("Relations");
            }
        }

        [Association(@"FinancialRevenueByItem_FactReferencesFinancialAssetDim", typeof(FinancialRevenueByItem_Fact)), Aggregated]
        public XPCollection<FinancialRevenueByItem_Fact> FinancialRevenueByItem_Facts
        {
            get { return GetCollection<FinancialRevenueByItem_Fact>("FinancialRevenueByItem_Facts"); }
        }
        #endregion
    }
}
