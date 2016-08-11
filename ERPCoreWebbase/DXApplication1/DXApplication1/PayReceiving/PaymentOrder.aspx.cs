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
    public partial class PaymentOrder : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
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
            public string Date { get; set; }
            public string Sent { get; set; }
            public string Receive { get; set; }
            public string Amount { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList data = new ArrayList();

            data.Add(new datasample() { Code = "UNC192/PF187", Date = "19/05/2013", Sent = "Công ty TNHH Sâm Ngọc Linh", Receive = "Công ty Dược Mỹ Vinh", Amount = "120,000,000" });
            data.Add(new datasample() { Code = "UNC193/PF187", Date = "28/07/2013", Sent = "Công ty TNHH Sâm Ngọc Linh", Receive = "Công ty Hà My", Amount = "150,000,000" });
            data.Add(new datasample() { Code = "UNC194/PF187", Date = "20/08/2013", Sent = "Công ty TNHH Sâm Ngọc Linh", Receive = "Công ty Dược Đông Phương", Amount = "180,000,000" });
            data.Add(new datasample() { Code = "UNC195/PF187", Date = "28/09/2013", Sent = "Công ty TNHH Sâm Ngọc Linh", Receive = "Công ty Dược VICO", Amount = "150,00,000,000" });

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