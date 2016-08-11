using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.GUI.Sales
{
    public partial class ReturnProducts : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grid_return.DataSource = new[] { new {stt = "01", id = "PTH00001", idkh = "KH00001", tenkh = "Nguyen Van A", ngaytra = "01/07/2013"},
                                            new {stt = "02", id = "PTH00002", idkh = "KH00002", tenkh = "Nguyen Van B", ngaytra = "05/07/2013"},
                                            new {stt = "03", id = "PTH00003", idkh = "KH00003", tenkh = "Nguyen Van C", ngaytra = "07/07/2013"}
                                           };
            grid_return.DataBind();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        public string AccessObjectId
        {
            get { return Constant.ACCESSOBJECT_SALES_BRINGBACKPRODUCT_ID; }
        }

        public string AccessObjectGroupId
        {
            get { return Constant.ACCESSOBJECT_SALES_GROUPID; }
        }
    }
}