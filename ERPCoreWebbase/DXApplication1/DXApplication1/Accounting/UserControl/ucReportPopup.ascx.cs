using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebModule.Accounting.Report;
using System.IO;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web;

namespace WebModule.Accounting.UserControl
{
    public partial class ucReportPopup : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rpvHf["ReportCode"] = null;
            }
            
            if (rpvHf["ReportCode"] != null && rpvHf["ReportCode"].ToString() != String.Empty)
            {
                XtraReport report = ReportMappingConstant.getReportInstance(rpvHf["ReportCode"].ToString());
                //double scaleFactor = 1;
                //scaleFactor = getReportScale(); //Convert.ToSingle(hf.Value) / 100f;
                //report.PaperKind = System.Drawing.Printing.PaperKind.Custom;
                //report.PageWidth = Convert.ToInt32(report.PageWidth * scaleFactor);
                //report.PageHeight = Convert.ToInt32(report.PageHeight * scaleFactor);
                //report.CreateDocument();
                //report.PrintingSystem.Document.ScaleFactor = scaleFactor;
                
                rpvReportViewerPopup.Report = report;
                rpvReportViewerPopup.DataBind();
            }
        }

        private double getReportScale()
        {
            return 0.5f;
        }

        protected void rpvReportViewerPopup_CacheReportDocument(object sender, DevExpress.XtraReports.Web.CacheReportDocumentEventArgs e)
        {
            e.Key = Guid.NewGuid().ToString();
            Page.Session[e.Key] = e.SaveDocumentToMemoryStream();
        }

        protected void rpvReportViewerPopup_RestoreReportDocumentFromCache(object sender, DevExpress.XtraReports.Web.RestoreReportDocumentFromCacheEventArgs e)
        {
            Stream stream = Page.Session[e.Key] as Stream;
            if (stream != null)
                e.RestoreDocumentFromStream(stream);
        }

        protected void rpvReportViewerPopup_Unload(object sender, EventArgs e)
        {
            ((ReportViewer)sender).Report = null;
        }
    }
}