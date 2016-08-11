using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Collections;

namespace WebModule.ImExporting
{
    public partial class Paytype : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        object[] grv_configProcessSource = new[] { 
            new{name = "Ký hợp đồng", description = ""},
            new{name = "Chuyển tiền", description = ""},
            new{name = "Chuyển hàng", description = ""},
            new{name = "Chứng từ về", description = ""},
            new{name = "Hàng về", description = ""},
            new{name = "Kê khai và làm thủ tục hải quan", description = ""},
            new{name = "Lấy hàng", description = ""},
            
        };
        
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_IMEXPORT_GROUPID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_IMEXPORT_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        private class datasample
        {                        
            public string Name { get; set; }
            public string Description { get; set; }
            public string RowStatus { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList datasupplier = new ArrayList();
            datasupplier.Add(new datasample() { Name = "L/C", Description = "L/C", RowStatus = "Đang Hoạt Động" });
            datasupplier.Add(new datasample() { Name = "DA", Description = "DA", RowStatus = "Đang Hoạt Động" });
            datasupplier.Add(new datasample() { Name = "DP", Description = "DP", RowStatus = "Đang Hoạt Động" });
            datasupplier.Add(new datasample() { Name = "TT", Description = "TT", RowStatus = "Đang Hoạt Động" });
            datasupplier.Add(new datasample() { Name = "Khác", Description = "Khác", RowStatus = "Đang Hoạt Động" });
            
            grdData.DataSource = datasupplier;
            grdData.DataBind();

            grv_configProcess.DataSource = grv_configProcessSource;
            grv_configProcess.DataBind();
        }

        protected void grdData_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

        }

        protected void grdData_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

        }

        protected void grdData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {

        }
    }
}