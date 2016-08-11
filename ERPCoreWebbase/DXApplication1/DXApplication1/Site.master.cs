using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule
{
    public partial class Site : System.Web.UI.MasterPage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            CurrentSession.Instance.Lang = Constant.LANG_DEFAULT;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!MasterHelper.HasNonEmptyControls(LeftSubmitContainer)
                && !MasterHelper.HasNonEmptyControls(CenterSubmitContainer)
                && !MasterHelper.HasNonEmptyControls(RightSubmitContainer))
            {
                MainContentSplitter.GetPaneByName("SubmitContainer").Collapsed = true;
                MainContentSplitter.GetPaneByName("ContentContainer").PaneStyle.BorderBottom.BorderWidth = 0;
            }
        }
    }
}