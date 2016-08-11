using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.Warehouse
{
    public partial class WarehouseStorage : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_STORAGE_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            grid_dmorder.DataSource =
              new[] { 
                     new { key="123", code = "MAT001", name = "Tiêu chuẩn 1",  status = "Kích hoạt",
                             description = "Kho thuốc bán lẻ"
                    },
                    new { key="124", code = "MAT002", name = "Tiêu chuẩn 2", status = "Tạm ngưng",
                             description = "Kho thuốc theo toa"
                    },
                };
            grid_dmorder.KeyFieldName = "key";
            grid_dmorder.DataBind();
        }

        protected void grid_dmorder_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            grid_dmorder.CancelEdit();
        }

        protected void grid_dmorder_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grid_dmorder.CancelEdit();
        }
    }
}