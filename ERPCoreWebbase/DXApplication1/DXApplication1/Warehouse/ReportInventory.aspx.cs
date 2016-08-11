using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Collections;
using DevExpress.Utils;
using System.Data;

namespace WebModule.Warehouse
{
    public partial class ReportInventory : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_REPORTINVENTORY_ID;
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
            var datasource = new[]
            {
                new { OrganizationId=1, ParentOrganizationId=0, name="Kho 1", Description="Phòng Nhân sự", Email="dept.hr@quasapharco.com", Address="Phòng Nhân sự", PhoneNo="123456879" },
                new { OrganizationId=2, ParentOrganizationId=1, name="Dãy 1.1", Description="Phòng Kế toán", Email="dept.accounting@quasapharco.com", Address="Phòng Nhân sự", PhoneNo="123456879" },
                new { OrganizationId=3, ParentOrganizationId=2, name="Kệ 1.1.1", Description="Bộ phận Kế toán tổng hợp", Email="dept.accounting.general@quasapharco.com", Address="Bộ phận Kế toán tổng hợp", PhoneNo="123456879" },
                new { OrganizationId=4, ParentOrganizationId=3, name="Học1.1.1.1", Description="Bộ phận Kế toán nội bộ", Email="dept.accounting.internal@quasapharco.com", Address="Bộ phận Kế toán nội bộ", PhoneNo="123456879" }
            };
            ASPxTreeList1.DataSource = datasource;
            ASPxTreeList1.DataBind();

            InitPivotGrid();
            BindChart();
        }
        void BindChart()
        {
            //WebChartControl1.DataSource = ASPxPivotGrid12;
            //WebChartControl1.SeriesTemplate.DataFilters
        }
        void InitPivotGrid()
        {
            // Change this property to transpose the chart.
            ASPxPivotGrid12.OptionsChartDataSource.ProvideDataByColumns = false;

            ASPxPivotGrid12.BeginUpdate();
            ASPxPivotGrid12.DataSource = GetData("SalesPerson");
            SetFilter();
            ASPxPivotGrid12.EndUpdate();
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
            var xmlFileName = Server.MapPath("~/Warehouse/Datasource/nwindSalesPerson.xml");
            if (string.IsNullOrEmpty(xmlFileName))
                return null;
            using (DataSet dataSet = new DataSet())
            {
                dataSet.ReadXml(xmlFileName);
                return dataSet.Tables[tableName].DefaultView;
            }
        }
    }
}