using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Collections;

namespace WebModule.Accounting
{
    public partial class ReceiptOrder : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_GROUPID;
            }
        }

        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        private class datasample
        {
            public string Code { get; set; }
            public string Date { get; set; }
            public string Sent { get; set; }
            public string sd { get; set; }
            public string dg { get; set; }
            public string Amount { get; set; }
            public string status { get; set; }            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList data = new ArrayList();

            data.Add(new datasample() { Code = "UNC192/PF187", Date = "19/05/2013", sd = "Ủy nhiệm thu 1",dg = "Ví dụ 1", Sent = "Công ty Dược Mỹ Vinh", Amount = "120,000,000", status  = "Đã duyệt"});
            data.Add(new datasample() { Code = "UNC193/PF187", Date = "28/07/2013", sd = "Ủy nhiệm thu 2",dg = "Ví dụ 2", Sent = "Công ty Hà My", Amount = "150,000,000", status = "Chưa duyệt" });
            data.Add(new datasample() { Code = "UNC194/PF187", Date = "20/08/2013", sd = "Ủy nhiệm thu 1",dg = "Ví dụ 3", Sent = "Công ty Dược Đông Phương", Amount = "180,000,000", status = "Chưa duyệt" });
            data.Add(new datasample() { Code = "UNC195/PF187", Date = "28/09/2013", sd = "Ủy nhiệm thu 1",dg = "Ví dụ 4", Sent = "Công ty Dược VICO", Amount = "150,00,000,000", status = "Chưa duyệt" });

            grdData.DataSource = data;
            grdData.DataBind();
        }

        protected void grdData_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void grdData_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "");
        }

        protected void grdData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "");
        }


    }
}