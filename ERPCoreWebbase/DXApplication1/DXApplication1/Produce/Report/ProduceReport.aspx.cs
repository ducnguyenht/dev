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

namespace WebModule.Produce.Report
{
    public partial class ProduceReport : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_PRODUCE_REPORT;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_PRODUCE_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            gvData.DataSource = ReportMappingConstant.getProduceReportDS();
            gvData.DataBind();
            if (this.hf.Value != string.Empty)
            {
                ReportViewer1.Report = ReportMappingConstant.getReportInstance(this.hf.Value);
                ReportViewer1.DataBind();
            }
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