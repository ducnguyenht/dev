using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.Purchasing
{
    public partial class CooperativePrinciples : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grd_principles.DataSource = new[]{
                new{principleID = "NT00001", customerID = "KH0001", customerName = "Huỳnh Quang Minh", 
                    startDate = "12/12/2012", endDate = "12/12/2015", description = ""
                }, 
                new{principleID = "NT00002", customerID = "KH0002", customerName = "Lê Thị Anh Thư", 
                    startDate = "12/12/2012", endDate = "12/12/2015", description = ""
                }, 
                new{principleID = "NT00003", customerID = "KH0003", customerName = "Lê Xuân Mai", 
                    startDate = "12/12/2012", endDate = "12/12/2015", description = ""
                }, 
            };
            grd_principles.DataBind();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        public string AccessObjectId
        {
            get { return Constant.ACCESSOBJECT_PURCHASE_COOPERATIVEPRINCIPLES_ID; }
        }

        public string AccessObjectGroupId
        {
            get { return Constant.ACCESSOBJECT_PURCHASE_GROUPID; }
        }
    }
}