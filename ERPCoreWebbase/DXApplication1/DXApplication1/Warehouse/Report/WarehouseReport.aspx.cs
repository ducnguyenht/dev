using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.XtraReports.Web;
using WebModule.Accounting.Report;
using DevExpress.Web.ASPxFormLayout;
using DevExpress.Web.ASPxPopupControl;


namespace WebModule.Warehouse.Report
{
    public partial class WarehouseReport : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_REPORTRP_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxComboBox1.DataSource = new[] { "Tháng 1", "Tháng 2", "Quý 1", "6 tháng đầu năm" };
            ASPxComboBox1.DataBind();
            gvData.DataSource = ReportMappingConstant.getWarehouseRPDS();
            gvData.DataBind();
            if (this.hf.Value != string.Empty)
            {
                ReportViewer1.Report = ReportMappingConstant.getReportInstance(this.hf.Value);
                ReportViewer1.DataBind();
            }

        }

        protected void gvData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            gvData.CancelEdit();
        }

        protected void ASPxCallback1_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
           
          
        }

        protected void popup_report_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
            //if (e.Parameter.ToString().Equals("S03a2-DN"))
            //{
            //    S03a3_DN r = new S03a3_DN();
            //    this.ReportViewer1.Report = r;
            //    this.ReportViewer1.DataBind();
            //    //Page.Session["2"] = null;
            //}
            //else if (e.Parameter == "2")
            //{

            //}
        }

        protected void ReportViewer1_RestoreReportDocumentFromCache(object sender, DevExpress.XtraReports.Web.RestoreReportDocumentFromCacheEventArgs e)
        {
            //Stream stream = Page.Session[e.Key] as Stream;
            //if (stream != null)
            //    e.RestoreDocumentFromStream(stream);
        }

        protected void ReportViewer1_CacheReportDocument(object sender, DevExpress.XtraReports.Web.CacheReportDocumentEventArgs e)
        {
            e.Key = "S03a3_DN";
            Page.Session[e.Key] = e.SaveDocumentToMemoryStream();
        }

        protected void ReportViewer1_Unload(object sender, EventArgs e)
        {
            ((ReportViewer)sender).Report = null;
        }
        protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
           // popup_report.HeaderText = e.Parameter.ToString();
        }
    }
}