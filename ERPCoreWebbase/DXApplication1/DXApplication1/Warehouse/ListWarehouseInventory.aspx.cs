using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.Warehouse
{
    public partial class ListWarehouseInventory : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_WAREHOUSEINVENTORY_ID;
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
            ASPxGridView1.DataSource =
             new[] { 
                     new { key="123", code = "MAT001", date = "20/07/2013", admin="Nguyễn Văn A", staff1="Nguyễn Văn B",staff2="Nguyễn Văn C"
                             
                    },
                    new { key="123", code = "MAT001", date = "25/07/2013", admin="Nguyễn Văn B", staff1="Nguyễn Văn A",staff2="Nguyễn Văn C"
                    },
                };
            ASPxGridView1.KeyFieldName = "key";
            ASPxGridView1.DataBind();
        }

        protected void ASPxGridView1_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            this.ASPxGridView1.CancelEdit();
        }

        protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            this.ASPxGridView1.CancelEdit();
        }
    }
}