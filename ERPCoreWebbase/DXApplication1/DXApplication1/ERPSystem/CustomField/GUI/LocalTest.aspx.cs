using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.ERPSystem.CustomField.GUI
{
    public partial class LocalTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void panel_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            gridview.CMSObjectId = Guid.Parse("91feabd5-5c0c-4c92-964a-ce6212c474cf");
            gridview.DataBind();
        }
    }
}