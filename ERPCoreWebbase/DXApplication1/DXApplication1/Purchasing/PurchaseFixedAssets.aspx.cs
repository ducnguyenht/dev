using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Utility;

namespace WebModule.Purchasing
{
    public partial class PurchaseFixedAssets : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_PURCHASE_PURCHASE_FIXEDASSESTS_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_PURCHASE_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        private class datasample
        {
            public int SupplierId { get; set; }
            public string Code { get; set; }
            public string Date { get; set; }
            public string Supplier { get; set; }
            public string Department { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList data = new ArrayList();
            data.Add(new datasample() { SupplierId = 1, Code = "BG001", Date = "19/07/2013", Supplier = "Nhà cung cấp Mỹ Châu", Department = "Phòng Marketing" });
            data.Add(new datasample() { SupplierId = 2, Code = "BG002", Date = "20/07/2013", Supplier = "Nhà cung cấp Kiến Việt", Department = "Phòng Tài Chính" });
            data.Add(new datasample() { SupplierId = 3, Code = "BG003", Date = "29/07/2013", Supplier = "Nhà cung cấp Mỹ Châu", Department = "Phòng kế hoạch vật tư" });
            data.Add(new datasample() { SupplierId = 4, Code = "BG004", Date = "20/08/2013", Supplier = "Nhà cung cấp Kiến Việt", Department = "Phòng Giám Đốc" });

            grdData.DataSource = data;
            grdData.DataBind();


        }

        protected void grdData_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "new");
        }

        protected void grdData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            e.Cancel = true;
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "edit");
        }

        protected void grdData_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}