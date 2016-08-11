using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.ImExporting
{
    public partial class WarehouseCategory : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_CATEGORY_ID;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            grdData.DataSource =
               new[] { 
                    new { key="1", code = "MTR011", name = "Kho trung chuyển", rowstatus = "Kích hoạt",
                             description = "Kho trung chuyển"
                    },
                    new { key="2", code = "MTR012", name = "Kho hàng nhập", rowstatus = "Kích hoạt",
                             description = "Kho hàng nhập"
                    },
                    new { key="3", code = "MTR013", name = "Kho hàng xuất", rowstatus = "Kích hoạt",
                             description = "Kho hàng xuất"
                    },
                };
            grdData.KeyFieldName = "key";
            grdData.DataBind();
        }
        protected void grdData_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "new");

            Session["supplierMode"] = null;
        }

        protected void grdData_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

        }

        protected void grdData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "edit");
        }

        protected void cpHeader_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }
    }
}