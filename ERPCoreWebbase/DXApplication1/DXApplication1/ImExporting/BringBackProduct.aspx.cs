using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.ImExporting
{
    public partial class BringBackProduct : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grid_bringbacklist.DataSource = new[] {
                    new{sequenceno = "01", bringbackid = "TH00001", 
                        orderid = "PBH0001", voucherid = "VC00001", reason = "", bringbackdate = "12/12/2012"},
                    new{sequenceno = "02", bringbackid = "TH00002", 
                        orderid = "PBH0002", voucherid = "VC00002", reason = "", bringbackdate = "11/12/2012"},
                    new{sequenceno = "03", bringbackid = "TH00003", 
                        orderid = "PBH0003", voucherid = "VC00003", reason = "", bringbackdate = "10/12/2012"},
                };
                grid_bringbacklist.DataBind();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        public string AccessObjectId
        {
            get { return Constant.ACCESSOBJECT_IMEXPORT_GROUPID; }
        }

        public string AccessObjectGroupId
        {
            get { return Constant.ACCESSOBJECT_IMEXPORT_GROUPID; }
        }
    }
}