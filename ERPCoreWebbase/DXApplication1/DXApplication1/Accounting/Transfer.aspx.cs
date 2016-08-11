using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Accounting
{
    public partial class Transfer : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_GROUPID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_GROUPID;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataGrid();
        }

        public void DataGrid()
        {
            var datasource = new[]
            {
                new {STT = 1,DienGiai = "Cộng chỉ tiêu tiền mặt để báo cáo tổng hợp về công ty mẹ", GhiChu= ""}
            };
            ASPxGridView1.DataSource = datasource;
            ASPxGridView1.DataBind();
        }
    }
}