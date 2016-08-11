using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.BaseImpl;
using WebDemo1.Module.BusinessObjects.DBWebDemo;
using WebDemo1.Module.Report;

namespace WebDemo1.Module.Controllers
{//b4
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class EmployeeViewController : ViewController
    {
        public EmployeeViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(Employee);
        }

        protected CustomHandlePrintReport_ViewController customHandlePrintReport;
        protected ShowViewParameters printActionSVP;

        /// <summary>
        /// Called when [activated].
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }

        //protected override void OnViewControlsCreated()
        //{
        //    base.OnViewControlsCreated();
        //    // Access and customize the target View control.
        //    //b4
        //    customHandlePrintReport = Frame.GetController<CustomHandlePrintReport_ViewController>();
        //    if (customHandlePrintReport != null)
        //    {//bi lap
        //        customHandlePrintReport.CustomHandlePrintReport += customHandlePrintReport_CustomHandlePrintReport;
        //    }
        //}
        protected override void OnViewControllersActivated()
        {
            customHandlePrintReport = Frame.GetController<CustomHandlePrintReport_ViewController>();
            if (customHandlePrintReport != null)
            {//bi lap
                customHandlePrintReport.CustomHandlePrintReport += customHandlePrintReport_CustomHandlePrintReport;
            }
            base.OnViewControllersActivated();
        }

        protected override void OnDeactivated()
        {
            if (customHandlePrintReport != null)
            {
                customHandlePrintReport.CustomHandlePrintReport -= customHandlePrintReport_CustomHandlePrintReport;
                customHandlePrintReport = null;
            }
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void customHandlePrintReport_CustomHandlePrintReport(object sender, CustomHandlePrintReportEventArgs e)
        {
            //b5
            Employee employee = View.CurrentObject as Employee;
            ReportPhieuNhapKho_Param param = new ReportPhieuNhapKho_Param(ReportDataProvider.ReportObjectSpaceProvider);
            param.Employee = employee;
            e.ParametersObject = param;
            e.ShowViewParameters = printActionSVP;
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {//b6
            ReportPhieuNhapKho report = new ReportPhieuNhapKho();
            Employee receipt = View.CurrentObject as Employee;

            if (receipt != null)
            {
                IReportDataV2 reportData = null;

                reportData =
                    View.ObjectSpace.FindObject<XtraReportData>
                    (
                        CriteriaOperator.And
                        (
                            new BinaryOperator("PredefinedReportType", typeof(ReportPhieuNhapKho), BinaryOperatorType.Equal),
                            new BinaryOperator("DisplayName", "Phieu nhap kho 2", BinaryOperatorType.Equal)
                        )
                    );

                string handle = ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);
                ReportServiceController reportServiceController = Frame.GetController<ReportServiceController>();
                printActionSVP = e.ShowViewParameters;

                reportServiceController.ShowPreview(handle, CriteriaOperator.Parse(""));
            }
        }
    }
}

// List<Employee> employees = new List<Employee>();
// employees.Add(View.CurrentObject as Employee);
//ReportPhieuNhapKho report = new ReportPhieuNhapKho();
//report.DataSource = View.CurrentObject as Employee;
// ReportPrintTool tool = new ReportPrintTool(report);
// tool.ShowPreview();
//Employee receiptOrder=View.CurrentObject as Employee;
//ReportPhieuNhapKho report = new ReportPhieuNhapKho();
//List<Employee> receiptOrder = new List<Employee>();
//receiptOrder.Add(View.CurrentObject as Employee);
//this.ObjectSpace//ef
//Session session = ((XPObjectSpace)ObjectSpace).Session;
// XPQuery<Employee> customers = session.Query<Employee>();
//var t = session.Query<Employee>().OrderByDescending(o => o.Birthday);
//XPCollection<Employee> em = new XPCollection<Employee>(session){
//var tt = session.Query<Employee>().Where(o => o.FirstName == "Khoi");
//var ttt = tt.First().Tasks;
//}