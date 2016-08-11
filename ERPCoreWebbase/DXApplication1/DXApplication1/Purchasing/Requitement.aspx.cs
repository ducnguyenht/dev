using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Utility;

namespace ERPCore.Purchasing
{
    public partial class requitement : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_PURCHASE_PRODUCT_ID;
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
            public string No { get; set; }
            public string Code { get; set; }
            public string Date { get; set; }
            public string Department { get; set; }
            public string Purpose { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList data = new ArrayList();
            data.Add(new datasample() { SupplierId = 1, No = "1", Code = "PYC01", Date = "19/07/2013", Department = "Phòng kế hoạch vật tư", Purpose = "Sản Xuất" });
            data.Add(new datasample() { SupplierId = 2, No = "2", Code = "PYC02", Date = "20/07/2013", Department = "Phòng kế toán", Purpose = "Sử dụng" });
            data.Add(new datasample() { SupplierId = 3, No = "3", Code = "PYC03", Date = "24/07/2013", Department = "Phòng hành chánh nhân sự", Purpose = "Sử dụng" });
            data.Add(new datasample() { SupplierId = 4, No = "4", Code = "PYC04", Date = "26/07/2013", Department = "Phòng marketing", Purpose = "Sử dụng" });


            grdData.DataSource = data;
            grdData.DataBind();
        }

        protected void grdData_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "new");

        }

        protected void grdData_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Cancel = true;
        }

        protected void grdData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            e.Cancel = true;
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "edit");
        }
    }
}