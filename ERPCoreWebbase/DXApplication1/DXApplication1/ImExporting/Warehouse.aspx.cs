using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.ImExporting
{
    public partial class Warehouse : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_WAREHOUSE_ID;
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
                     new { code = "MAT011", name = "Kho trung chuyển 1", warehousetype="Kho trung chuyển", rowstatus = "Kích hoạt",
                             description = "Kho trung chuyển 1"
                    },
                    new { code = "MAT012", name = "Kho hàng nhập 1", warehousetype="Kho hàng nhập", rowstatus = "Kích hoạt",
                             description = "Kho hàng nhập 1"
                    },
                    new { code = "MAT013", name = "Kho hàng xuất 1", warehousetype="Kho hàng xuất", rowstatus = "Kích hoạt",
                             description = "Kho hàng xuất 1"
                    },
                };
            grdData.DataBind();

            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
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
    }
}