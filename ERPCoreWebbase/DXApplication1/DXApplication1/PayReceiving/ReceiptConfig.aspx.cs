using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Collections;

namespace ERPCore.PayReceiving
{
    public partial class ReceiptConfig : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_PAYRECEIVE_GROUPID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_PAYRECEIVE_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }


        private class datasample
        {            
            public string Code { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string RowStatus { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList data = new ArrayList();
            data.Add(new datasample() { Code = "L001/PT", Name = "Thu tạm ứng", Description = "Thu tạm ứng", RowStatus = "Đang Hoạt Động" });
            data.Add(new datasample() { Code = "L002/PT", Name = "Thu tiền khách hàng", Description = "Thu tiền khách hàng", RowStatus = "Đang Hoạt Động" });
            data.Add(new datasample() { Code = "L003/PT", Name = "Thu tiền trả hàng", Description = "Thu tiền trả hàng", RowStatus = "Đang Hoạt Động" });            

            grdData.DataSource = data;
            grdData.DataBind();
        }

        protected void grdData_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "");
        }

        protected void grdData_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

        }

        protected void grdData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "");
        }
    }
}