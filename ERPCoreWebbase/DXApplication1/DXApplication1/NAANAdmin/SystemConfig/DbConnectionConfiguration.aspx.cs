using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.NAANAdmin.SystemConfig
{
    public partial class DbConnectionConfiguration : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        const string ACCESSOBJECT_SYSADMIN_SYSTEM_DBCONFIG_ID = "sysadmin-system-dbconfig";
        const string ACCESSOBJECT_SYSADMIN_SYSTEM_ID = "sysadmin-system";

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_SYSTEM_DBCONFIG_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_SYSTEM_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}