using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.GUI.Sales
{
    public partial class OrderTest : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        public string AccessObjectId
        {
            get { return Constant.ACCESSOBJECT_SALES_SERVICE_ID; }
        }

        public string AccessObjectGroupId
        {
            get { return Constant.ACCESSOBJECT_SALES_GROUPID; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}