using System;
using System.Web.UI.WebControls;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.ReportsV2;
using DevExpress.Web.ASPxEditors;
using DevExpress.Xpo;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.ExpressApp.ReportsV2.Web;
using WebDemo1.Module.Controllers;

namespace Namoly.Module.Web.Controllers.Utils
{
    public partial class CustomHandlePrintReport_ViewControllerWeb : CustomHandlePrintReport_ViewController
    {
        private IObjectSpace objectSpace = null;

        public override void CustomShowPreview(
            string reportContainerHandle,
            ReportParametersObjectBase parametersObject,
            DevExpress.Data.Filtering.CriteriaOperator criteria,
            bool canApplyCriteria,
            SortProperty[] sortProperty,
            bool canApplySortProperty,
            ShowViewParameters showViewParameters,
            bool isSinglePage)
        {
            IReportContainer reportContainer = ReportDataProvider.ReportsStorage.GetReportContainerByHandle(reportContainerHandle);
            Guard.ArgumentNotNull(reportContainer, "reportContainer");
            Type dataType = GetReportDataSourceObjectType(reportContainer.Report);
            objectSpace = Application.CreateObjectSpace(dataType);
            DetailView previewDetailView = new DetailView((IModelDetailView)Application.Model.Views[ReportsAspNetModuleV2.ReportViewDetailViewWebName], objectSpace, null, Application, false);
            ReportViewerContainer reportViewContainer = new ReportViewerContainer(parametersObject, criteria, canApplyCriteria, sortProperty, canApplySortProperty);
            DevExpress.ExpressApp.ReportsV2.Web.ReportViewerDetailItem reportViewer =
                new DevExpress.ExpressApp.ReportsV2.Web.ReportViewerDetailItem(
                    reportContainerHandle,
                    reportViewContainer,
                    "ReportViewer");
            reportViewer.QueryReport += this.HandleQueryReportEvent;
            reportViewer.CustomParameterEditors += HandleCustomParameterEditorsEvent;
            previewDetailView.AddItem(reportViewer);
            previewDetailView.Caption = reportContainer.Report.DisplayName;

            if (isSinglePage)
            {
                Namoly.Module.Common.Utility.SinglePageHelper.GenerateSinglePageReport(reportContainer.Report, true, 6);
            }

            PreviewReportDialogController windowController = Application.CreateController<PreviewReportDialogController>();
            windowController.AcceptAction.Active["ReportPreview"] = false;
            if (showViewParameters == null)
            {
                showViewParameters = new ShowViewParameters();
                showViewParameters.Controllers.Add(windowController);
                showViewParameters.CreatedView = previewDetailView;
                showViewParameters.TargetWindow = TargetWindow.NewWindow;
                Application.ShowViewStrategy.ShowView(showViewParameters, new ShowViewSource(Frame, null));
            }
            else
            {
                showViewParameters.Controllers.Add(windowController);
                showViewParameters.CreatedView = previewDetailView;
                showViewParameters.TargetWindow = TargetWindow.NewWindow;
            }
        }

        private void HandleQueryReportEvent(object sender, QueryReportEventArgs e)
        {
            IReportContainer reportContainer = ReportDataProvider.ReportsStorage.GetReportContainerByHandle(e.ReportHandle);
            Guard.ArgumentNotNull(reportContainer, "reportContainer");
            XtraReport report = reportContainer.Report;
            reportServiceController.SetupBeforePrint(report, e.ParametersObject, e.Criteria, e.CanApplyCriteria, e.SortProperty, e.CanApplySortProperty);
            e.Report = report;
        }
        private void HandleCustomParameterEditorsEvent(object sender, CustomizeParameterEditorsEventArgs e)
        {
            CreateCustomParameterEditors(e);
            OnCustomParameterEditors(e);
        }

        protected virtual void OnCustomParameterEditors(CustomizeParameterEditorsEventArgs args)
        {
            if (CustomParameterEditors != null)
            {
                CustomParameterEditors(this, args);
            }
        }

        private void CreateCustomParameterEditors(CustomizeParameterEditorsEventArgs e)
        {
            ITypeInfo typeInfo = XafTypesInfo.Instance.FindTypeInfo(e.Parameter.Type);
            if (typeInfo != null && typeInfo.IsDomainComponent)
            {
                ASPxPropertyEditor editor = (ASPxPropertyEditor)Application.EditorFactory.CreateDetailViewEditor(false, GetParameterViewItem(e.Parameter.Name, e.Parameter.Type), typeof(ParametersObject), Application, objectSpace);
                ASPxLookupPropertyEditor lookupPropertyEditor = editor as ASPxLookupPropertyEditor;
                if (lookupPropertyEditor != null)
                {
                    lookupPropertyEditor.WebLookupEditorHelper.EditorMode = LookupEditorMode.AllItems;
                    lookupPropertyEditor.WebLookupEditorHelper.SmallCollectionItemCount = int.MaxValue;
                }
                editor.ViewEditMode = ViewEditMode.Edit;
                editor.CreateControl();
                editor.CurrentObject = new object();
                editor.ReadValue();
                if (lookupPropertyEditor != null)
                {
                    e.Editor = lookupPropertyEditor.DropDownEdit.DropDown;
                    new PropertyEditorDisposeManager().Attach(lookupPropertyEditor, lookupPropertyEditor.DropDownEdit.DropDown);
                    e.Editor.ID = editor.Id + "_" + e.Editor.ID;
                    lookupPropertyEditor.SetValueToControl(e.Parameter.Value);
                    e.ShouldSetParameterValue = false;
                }
                else
                {
                    e.Editor = editor.Editor as ASPxEdit;
                    new PropertyEditorDisposeManager().Attach(editor, editor.Editor);
                }
            }
        }

        internal IModelMemberViewItem GetParameterViewItem(string parameterName, Type parameterType)
        {
            string uniqueParameterName = "Reports_" + parameterName.Replace('.', '_') + "_" + parameterType.Name;
            UpdatableParameter xafParameter = new UpdatableParameter(uniqueParameterName, parameterType);
            ParameterList parameterList = new ParameterList();
            parameterList.Add(xafParameter);
            ParametersObject.CreateBoundObject(parameterList);
            IModelDetailView detailViewModel = TempDetailViewHelper.CreateTempDetailViewModel(Application.Model, typeof(ParametersObject));
            return (IModelMemberViewItem)detailViewModel.Items[uniqueParameterName];
        }

        private class PropertyEditorDisposeManager
        {
            ASPxPropertyEditor propertyEditor;
            WebControl editor;
            private void editor_Disposed(object sender, EventArgs e)
            {
                DisposeObjects();
            }
            private void editor_Unload(object sender, EventArgs e)
            {
                DisposeObjects();
            }
            private void DisposeObjects()
            {
                if (editor != null)
                {
                    editor.Unload -= new EventHandler(editor_Unload);
                    editor.Disposed -= new EventHandler(editor_Disposed);
                    editor.Dispose();
                    editor = null;
                }
                if (propertyEditor != null)
                {
                    propertyEditor.Dispose();
                    propertyEditor = null;
                }
            }
            public void Attach(ASPxPropertyEditor propertyEditor, WebControl editor)
            {
                this.propertyEditor = propertyEditor;
                this.editor = editor;
                editor.Unload += new EventHandler(editor_Unload);
                editor.Disposed += new EventHandler(editor_Disposed);
            }
        }
        public event EventHandler<CustomizeParameterEditorsEventArgs> CustomParameterEditors;

        protected virtual Type GetReportDataSourceObjectType(XtraReport report)
        {
            Type result = null;
            ReportsModuleV2 reportsModule = ReportsModuleV2.FindReportsModule(Application.Modules);
            if (reportsModule != null && reportsModule.ReportsDataSourceHelper != null)
            {
                object dataSource = reportsModule.ReportsDataSourceHelper.GetMasterReportDataSource(report);
                DataSourceBase dataTypeContainer = dataSource as DataSourceBase;
                if (dataTypeContainer != null)
                {
                    result = dataTypeContainer.DataType;
                }
            }
            return result;
        }
    }
}
