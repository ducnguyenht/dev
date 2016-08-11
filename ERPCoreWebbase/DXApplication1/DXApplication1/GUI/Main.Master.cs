using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.GUI
{
    public partial class Main : System.Web.UI.MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
            //Temporary assignment is vi
            CurrentSession.Instance.Lang = Constant.LANG_DEFAULT;
            Session["Lang"] = CurrentSession.Instance.Lang;
        }
    }
}