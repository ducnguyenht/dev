using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Xpo;
using System;

namespace WebDemo1.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public abstract partial class CustomHandlePrintReport_ViewController : ViewController
    {
        protected ReportServiceController reportServiceController = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomHandlePrintReport_ViewController"/> class.
        /// </summary>
        public CustomHandlePrintReport_ViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

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
        //    if (reportServiceController == null)
        //    {
        //        reportServiceController = Frame.GetController<ReportServiceController>();
        //        if (reportServiceController != null)
        //        {
        //            reportServiceController.CustomShowPreview += reportServiceController_CustomShowPreview;
        //        }
        //    }
        //}
        protected override void OnViewControllersActivated()
        {
            base.OnViewControllersActivated();
            if (reportServiceController == null)
            {
                reportServiceController = Frame.GetController<ReportServiceController>();
                if (reportServiceController != null)
                {
                    reportServiceController.CustomShowPreview += reportServiceController_CustomShowPreview;
                }
            }
        }

        private void reportServiceController_CustomShowPreview(object sender, CustomShowPreviewEventArgs e)
        {
            if (CustomHandlePrintReport != null)
            {
                CustomHandlePrintReportEventArgs eventArgs = new CustomHandlePrintReportEventArgs();
                eventArgs.CanApplyCriteria = e.CanApplyCriteria;
                eventArgs.CanApplySortProperty = e.CanApplySortProperty;
                eventArgs.Criteria = e.Criteria;
                eventArgs.ParametersObject = e.ParametersObject;
                eventArgs.ReportContainerHandle = e.ReportContainerHandle;
                eventArgs.ShowViewParameters = e.ShowViewParameters;
                eventArgs.SortProperty = e.SortProperty;

                CustomHandlePrintReport(this, eventArgs);

                CustomShowPreview(
                    eventArgs.ReportContainerHandle,
                    eventArgs.ParametersObject,
                    eventArgs.Criteria,
                    eventArgs.CanApplyCriteria,
                    eventArgs.SortProperty,
                    eventArgs.CanApplySortProperty,
                    eventArgs.ShowViewParameters,
                    eventArgs.IsSinglePage);
                e.Handled = true;
            }
        }

        protected override void OnDeactivated()
        {
            if (reportServiceController != null)
            {
                reportServiceController.CustomShowPreview -= reportServiceController_CustomShowPreview;
                reportServiceController = null;
            }
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        public abstract void CustomShowPreview(
            string reportContainerHandle,
            ReportParametersObjectBase parametersObject,
            CriteriaOperator criteria,
            bool canApplyCriteria,
            SortProperty[] sortProperty,
            bool canApplySortProperty,
            ShowViewParameters showViewParameters,
            bool isSinglePage);

        public event EventHandler<CustomHandlePrintReportEventArgs> CustomHandlePrintReport;
    }

    public class CustomHandlePrintReportEventArgs : EventArgs
    {
        public bool CanApplyCriteria { get; set; }
        public bool CanApplySortProperty { get; set; }
        public CriteriaOperator Criteria { get; set; }
        public ReportParametersObjectBase ParametersObject { get; set; }
        public string ReportContainerHandle { get; set; }
        public ShowViewParameters ShowViewParameters { get; set; }
        public SortProperty[] SortProperty { get; set; }
        public bool IsSinglePage { get; set; }
    }
}