using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Utility;
using DevExpress.Web.ASPxGridView;

namespace ERPCore.Purchasing
{
    public partial class quotation : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
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
            public string Dateto { get; set; }
            public string Datesent { get; set; }            
            public string Cost { get; set; }
            public string Amount { get; set; }
            public string Modify { get; set; }
            public string Supplier { get; set; }
        }

        private class datadetailsample
        {
            public int SupplierId { get; set; }
            public string No { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Unit { get; set; }            
            public string Amount2 { get; set; }            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayList data = new ArrayList();
            data.Add(new datasample() { SupplierId = 1, No = "1", Code = "BG001", Dateto = "19/07/2013", Datesent = "29/07/2013", Cost = "1,000,000", Amount = "8,900,000", Modify = "500,000", Supplier = "Cty TNHH kiêm Nhà cung cấp dược liệu Mỹ Châu" });
            data.Add(new datasample() { SupplierId = 2, No = "2", Code = "BG002", Dateto = "20/07/2013", Datesent = "26/07/2013", Cost = "3,000,000", Amount = "18,900,000", Modify = "1200,000", Supplier = "Nhà cung cấp Kiến Việt" });
            data.Add(new datasample() { SupplierId = 3, No = "3", Code = "BG003", Dateto = "29/07/2013", Datesent = "19/08/2013", Cost = "1,000,000", Amount = "900,000", Modify = "300,000", Supplier = "Nhà cung cấp Mỹ Châu" });
            data.Add(new datasample() { SupplierId = 4, No = "4", Code = "BG004", Dateto = "20/08/2013", Datesent = "26/10/2013", Cost = "35,000,000", Amount = "128,900,000", Modify = "1200,000", Supplier = "Nhà cung cấp Kiến Việt" });

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

        protected void grdData_BeforePerformDataSelect(object sender, EventArgs e)
        {
            
        }

        protected void grdLine_BeforePerformDataSelect(object sender, EventArgs e)
        {
            
            ArrayList data = new ArrayList();
            data.Add(new datadetailsample { No = "1", Code = "TANATRIL", Name = "Tanatril", Amount2 = "10,000,000", Unit = "Hộp", SupplierId = 1 });
            data.Add(new datadetailsample { No = "2", Code = "BETASERC", Name = "Betaserc", Amount2 = "50,000,000", Unit = "Viên", SupplierId = 1 });
            data.Add(new datadetailsample { No = "3", Code = "KLACID", Name = "Klacid", Amount2 = "10,000,000", Unit = "Viên", SupplierId = 1 });

            ASPxGridView detailGridView = (ASPxGridView)sender;
            detailGridView.Load += new EventHandler(detailView_Load);
            detailGridView.DataSource = data;            
        }

        void detailView_Load(object sender, EventArgs e)
        {
            ASPxGridView detailView = (ASPxGridView)sender;
            detailView.DataBind();
        }

    }
}