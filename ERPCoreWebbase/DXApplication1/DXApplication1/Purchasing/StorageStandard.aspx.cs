using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace DXApplication1.Purchasing
{
    public partial class StorageStandard : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
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


        protected void Page_Load(object sender, EventArgs e)
        {
            grdData.DataSource =
                new[] { 
                    new { name = "Tiêu chuẩn 1", rowstatus = "Kích hoạt",
                             description = "Tiêu chuẩn 1"
                    },
                    new { name = "Tiêu chuẩn 2", rowstatus = "Tạm ngưng",
                             description = "Tiêu chuẩn 2"
                    },
                };
            grdData.DataBind();
        }
        protected void cpHeader_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            switch (e.Parameter)
            {
                case "refresh":
                    //BindGrid();

                    break;
                default:
                    break;
            }
        }
        protected void grdData_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "new");

            Session["supplierMode"] = null;
        }

        protected void grdData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "edit");
        }

        protected void grdData_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            
        }
    }
}