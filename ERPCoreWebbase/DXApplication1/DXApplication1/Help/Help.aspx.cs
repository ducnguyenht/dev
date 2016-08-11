using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.Help
{
    public partial class Help : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_DEFAULT_GROUPID;
            }
        }

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_DEFAULT_ID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}