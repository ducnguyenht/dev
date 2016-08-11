using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.Warehouse
{
    public partial class ReportDevice : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_REPORTDEVICE_ID;
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
            gr_data.DataSource =
             new[] { 
                     new { key="123", code = "HDP001", name = "Công cụ 1", 
                             description = "Công cụ 1"
                    },
                    new { key="1234", code = "HDP003", name = "Công cụ 2", 
                             description = "Công cụ 2"
                    },
                };
            gr_data.KeyFieldName = "key";
            gr_data.DataBind();
        }

        protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            gr_data.CancelEdit();
            gr_data.JSProperties.Add("cpNew", "new");

        }

        protected void ASPxGridView1_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            gr_data.CancelEdit();
            gr_data.JSProperties.Add("cpEdit", "edit");
        }
    }
}