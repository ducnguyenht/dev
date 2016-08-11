using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

namespace WebModule.Warehouse.UserControl
{
    public partial class uReportPivot : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                reportType["ReportType"] = null;
            }
            if (reportType["ReportType"] != null && reportType["ReportType"].ToString() != String.Empty)
            {
                InitPivotGrid();
                BindChart();
            }
        }
        void BindChart()
        {
            //WebChartControl1.DataSource = ASPxPivotGrid12;
            //WebChartControl1.SeriesTemplate.DataFilters
        }
        void InitPivotGrid()
        {
            string strReportType = "reportproduct";
            if (reportType["ReportType"].Equals("ReportMaterial"))
            {
                strReportType = "reportmaterial";
            }
            else if (reportType["ReportType"].Equals("ReportDevice"))
            {
                strReportType = "reportdevice";
            }
            else if (reportType["ReportType"].Equals("ReportFinish"))
            {
                strReportType = "reportfinishedproduct";
            }
            // Change this property to transpose the chart.
            ASPxPivotGrid1.OptionsChartDataSource.ProvideDataByColumns = false;

            ASPxPivotGrid1.BeginUpdate();
            ASPxPivotGrid1.DataSource = GetData(strReportType);
            SetFilter();
            ASPxPivotGrid1.EndUpdate();
            SetSelection();
        }
        void SetFilter()
        {

        }
        void SetSelection()
        {

        }
        IList GetData(string tableName)
        {
            var xmlFileName = Server.MapPath("~/Warehouse/Datasource/" + tableName  + ".xml");
            if (string.IsNullOrEmpty(xmlFileName))
                return null;
            using (DataSet dataSet = new DataSet())
            {
                dataSet.ReadXml(xmlFileName);
                return dataSet.Tables[tableName].DefaultView;
            }
        }

        protected void ASPxPivotGrid1_DataBound(object sender, EventArgs e)
        {

        }
    }
}