using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Utility;


namespace WebModule.ImExporting
{
    public partial class Debit : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
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
            public int ProductId { get; set; }
            public string Supplier { get; set; }
            public string DK { get; set; }
            public string PS { get; set; }
            public string TT { get; set; }
            public string CK { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList data = new ArrayList();
            data.Add(new datasample() { ProductId = 1, Supplier = "Nhà cung cấp Mỹ Châu", DK = "200,000,000", PS = "1,400,000,000", TT = "100,000", CK = "100,000" });
            data.Add(new datasample() { ProductId = 2, Supplier = "Nhà cung cấp Ích Nhân", DK = "400,000,000", PS = "400,000,000", TT = "100,000", CK = "200,000,000" });


            grdData.DataSource = data;
            grdData.DataBind();

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

        protected void cpHeader_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }
    }
}