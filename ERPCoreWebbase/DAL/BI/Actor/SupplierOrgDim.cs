using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.SupplierLiability;

namespace NAS.DAL.BI.Actor
{
    public class SupplierOrgDim : XPCustomObject
    {

        public SupplierOrgDim(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        #region Field [5]
        // Fields...
        //1.                       
        private Guid _SupplierOrgDimId;
        [Key(true)]
        public Guid SupplierOrgDimId
        {
            get { return _SupplierOrgDimId; }
            set { SetPropertyValue("SupplierOrgDimId", ref _SupplierOrgDimId, value); }
        }
        //2.
        private Guid _RefId;
        public Guid RefId
        {
            get { return _RefId; }
            set { SetPropertyValue("RefId", ref _RefId, value); }
        }
        //3.
        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { SetPropertyValue("Description", ref _Description, value); }
        }
        //4.
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue("Name", ref _Name, value); }
        }
        //5.
        private short _RowStatus;
        public short RowStatus
        {
            get { return _RowStatus; }
            set { SetPropertyValue("RowStatus", ref _RowStatus, value); }
        }
        private string _Code;
        public string Code
        {
            get { return _Code; }
            set { SetPropertyValue("Code", ref _Code, value); }
        }
        #endregion

        #region References
        //(1)-(n): Get Colection
        [Association("FinancialSupplierLiabilitySummaryFactReferencesSupplierOrgDim")]
        public XPCollection<FinancialSupplierLiabilitySummary_Fact> FinancialSupplierLiabilitySummary_Facts
        {
            get { return GetCollection<FinancialSupplierLiabilitySummary_Fact>("FinancialSupplierLiabilitySummary_Facts"); }
        }
        #endregion

    }
}
