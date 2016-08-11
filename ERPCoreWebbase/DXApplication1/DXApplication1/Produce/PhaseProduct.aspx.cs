using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Collections;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Produce
{
    public partial class PhaseProduct : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_PRODUCE_GROUPID;
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

        private class datasample
        {
            public string Code0 { get; set; }
            public string Name0 { get; set; }
            public string Unit { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }                
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList data = new ArrayList();
            data.Add(new datasample() { Code0 = "TANATRIL", Name0="Tanatril", Unit="Viên", Code = "CĐ001", Name="Dập Viên" });
            //data.Add(new datasample() { Code0 = "TANATRIL", Name0 = "Tanatril", Unit = "Viên", Code = "CĐ002", Name = "Đóng Gói" });
            //data.Add(new datasample() { Code0 = "TANATRIL", Name0 = "Tanatril", Unit = "Viên", Code = "CĐ003", Name = "Đóng Thùng" });

            data.Add(new datasample() { Code0 = "KLACID", Name0 = "Klacid", Unit = "Viên", Code = "CĐ001", Name = "Dập Viên" });
            //data.Add(new datasample() { Code0 = "KLACID", Name0 = "Klacid", Unit = "Viên", Code = "CĐ002", Name = "Đóng Gói" });
            //data.Add(new datasample() { Code0 = "KLACID", Name0 = "Klacid", Unit = "Viên", Code = "CĐ003", Name = "Đóng Thùng" });

            grdData.DataSource = data;
            grdData.DataBind();
        }

        protected void grdData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "");
        }

     
        protected void grdData0_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ArrayList data = new ArrayList();
            data.Add(new datasample() { Code0 = "TANATRIL", Name0 = "Tanatril", Unit = "Viên", Code = "CĐ001", Name = "Dập Viên" });
            data.Add(new datasample() { Code0 = "TANATRIL", Name0 = "Tanatril", Unit = "Viên", Code = "CĐ002", Name = "Đóng Gói" });
            data.Add(new datasample() { Code0 = "TANATRIL", Name0 = "Tanatril", Unit = "Viên", Code = "CĐ003", Name = "Đóng Thùng" });

            //data.Add(new datasample() { Code0 = "KLACID", Name0 = "Klacid", Unit = "Viên", Code = "CĐ001", Name = "Dập Viên" });
            //data.Add(new datasample() { Code0 = "KLACID", Name0 = "Klacid", Unit = "Viên", Code = "CĐ002", Name = "Đóng Gói" });
            //data.Add(new datasample() { Code0 = "KLACID", Name0 = "Klacid", Unit = "Viên", Code = "CĐ003", Name = "Đóng Thùng" });

            ASPxGridView detailView = (ASPxGridView)sender;
            detailView.Load += new EventHandler(detailView_Load);

            detailView.DataSource = data;
        }

        void detailView_Load(object sender, EventArgs e)
        {
            ASPxGridView detailView = (ASPxGridView)sender;
            detailView.DataBind();
        }

        protected void grdData_InitNewRow1(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "");
        }

        protected void grdData0_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

        }

        protected void grdData0_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {

        }
    }
}