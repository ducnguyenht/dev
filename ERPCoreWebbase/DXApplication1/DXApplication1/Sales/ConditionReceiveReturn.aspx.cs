using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.GUI.Sales
{
    public partial class ConditionReceiveReturn : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grv_configConditionReceive.DataSource = new[]{
                new {id = "CS00001", name = "Điều kiện trả hàng 1", from = "20/01/2009", to = "20/01/2015", description = ""},
                new {id = "CS00002", name = "Điều kiện trả hàng 2", from = "20/01/2009", to = "20/01/2012", description = ""},
                new {id = "CS00003", name = "Điều kiện trả hàng 3", from = "20/01/2009", to = "20/01/2013", description = ""},
                new {id = "CS00004", name = "Điều kiện trả hàng 4", from = "20/01/2009", to = "20/01/2014", description = ""}
            };
            grv_configConditionReceive.DataBind();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        public string AccessObjectId
        {
            get { return Constant.ACCESSOBJECT_SALES_CONDITIONTORECEIVE_ID; }
        }

        public string AccessObjectGroupId
        {
            get { return Constant.ACCESSOBJECT_SALES_GROUPID; }
        }
    }
}