using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using WebDemo1.Module.BusinessObjects.DBWebDemo;

namespace WebDemo1.Module.Report
{//b2
    [DomainComponent]
    public class ReportPhieuNhapKho_Param : ReportParametersObjectBase
    {
        public ReportPhieuNhapKho_Param(IObjectSpaceCreator provider)
            : base(provider)
        {
        }

        protected override IObjectSpace CreateObjectSpace()
        {
            return objectSpaceCreator.CreateObjectSpace(typeof(Employee));
        }

        public Employee Employee { get; set; }

        public override CriteriaOperator GetCriteria()
        {
            CriteriaOperator criteria = new BinaryOperator("MyPropertyName", "MyValue");
            return criteria;
        }

        public override SortProperty[] GetSorting()
        {
            SortProperty[] sorting = { new SortProperty("MyPropertyName", SortingDirection.Descending) };
            return sorting;
        }
    }
}