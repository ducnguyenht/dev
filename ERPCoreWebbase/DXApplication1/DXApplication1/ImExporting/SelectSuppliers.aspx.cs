using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using Utility;

namespace WebModule.ImExporting
{
    public partial class SelectSuppliers : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
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

        private void BindGrid()
        {
            grdData.DataSource =
               new[] { 
                     new { key="123", code = "MAT001", name = "Mặt hàng 1", productunit = "Thùng",
                             amount = "50"
                    },
                    new { key="1234", code = "MAT002", name = "Mặt hàng 2", productunit="Hộp",
                             amount = "100"
                    },
                };
            grdData.KeyFieldName = "key";
            grdData.DataBind();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack || !Page.IsCallback)
            {
                BindGrid();
            }
            else
            {

            }
        }

        protected void grdData_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "new");

            Session["productMode"] = null;
        }

        protected void grdData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            BindGrid();

            Guid guid = new Guid(e.EditingKeyValue.ToString());


            //Session["productMode"] = vs;

            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "edit");
        }

        protected void grdData_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

            e.Cancel = true;
            BindGrid();
        }

        protected void cpHeader_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            switch (e.Parameter)
            {
                case "refresh":
                    BindGrid();
                    break;
                default:
                    break;
            }
        }

        protected void grdDataDetail_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "new");

            Session["productMode"] = null;
        }

        protected void grdDataDetail_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            BindGrid();

            Guid guid = new Guid(e.EditingKeyValue.ToString());


            //Session["productMode"] = vs;

            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "edit");
        }

        protected void grdDataDetail_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

            e.Cancel = true;
            BindGrid();
        }

        protected void grdDataDetail_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView detailView = (ASPxGridView)sender;
                detailView.Load += new EventHandler(detailView_Load);
                detailView.DataSource =
                   new[] { 
                     new { name="Nhà cung cấp 1", price = "150000", discount = "5%",
                             costs = "0", sum="1.500.000", dday="28-07-2013", dpayment="30-07-2013", 
                             timeeffect= "30-03-2013", description="Nhà cung cấp 1", Currency="VND", Exchange="1", AmountA="1.500.000"
                    },
                    new { name="Nhà cung cấp 2", price = "140.000", discount = "5%",
                             costs = "0", sum="1.300.000", dday="30-07-2013", dpayment="30-07-2013", 
                             timeeffect= "30-07-2013", description="Nhà cung cấp 2", Currency="VND", Exchange="1", AmountA="140.000"
                    },
                };
            }
            catch (Exception) { }
        }

        void detailView_Load(object sender, EventArgs e)
        {
            ASPxGridView detailView = (ASPxGridView)sender;
            detailView.DataBind();
        }

        protected void grdDataDetail_DataBinding(object sender, EventArgs e)
        {
            int a = 12;
            a = 33;
            a = 55;
        }
    }
}