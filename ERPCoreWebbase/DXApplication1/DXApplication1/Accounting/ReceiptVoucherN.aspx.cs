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
    public partial class ReceiptVoucherN : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
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
            public string Customer { get; set; }
            public string Address { get; set; }
            public string Amount { get; set; }
            public string Status { get; set; }
            public string Desc { get; set; }
            public string Order { get; set; }
            public string AM { get; set; }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList data = new ArrayList();

            data.Add(new datasample() { AM = "Phiếu thu 1",Code = "PT12989", Date="18/07/2013", Customer = "Công ty TNHH Minh Phát", Address = "19 Lạc Long Quân F6 QTB", Amount = "10,000,000", Order = "HD919820/NX", Status="Hoàn tất", Desc="Thu tiền khách hàng" });
            data.Add(new datasample() { AM = "Phiếu thu 2",Code = "PT32489", Date = "20/07/2013", Customer = "Cửa Hàng Song Hiệp", Address = "2B Bùi Thị Xuân F2 QTB", Amount = "15,000,000", Order = "HD129810/NX", Status = "Hoàn tất", Desc = "Thu tiền khách hàng" });
            data.Add(new datasample() { AM = "Phiếu thu 1",Code = "PT52489", Date = "29/07/2013", Customer = "Cửa Hàng Thuận Việt", Address = "2B Bùi Thị Xuân F2 QTB", Amount = "18,000,000", Order = "HD129810/NX", Status = "Đang chờ", Desc = "Thu tiền khách hàng" });

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