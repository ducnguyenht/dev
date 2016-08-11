using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.Produce
{
    public partial class WarehouseCategory : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_PRODUCE_GROUPID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_PRODUCE_GROUPID;
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
                    new { key="123", code = "MTR001", name = "Kho nguyên vật liệu", rowstatus = "Kích hoạt",
                             description = "kho nguyên vật liệu"
                    },
                    new { key="1234", code = "MTR002", name = "Kho bán thành phẩm", rowstatus = "Kích hoạt",
                             description = "Kho bán thành phẩm"
                    },
                     new { key="12345", code = "MTR003", name = "Kho thành phẩm", rowstatus = "Kích hoạt",
                             description = "Kho thành phẩm"
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