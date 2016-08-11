using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.ImExporting.UserControl
{
    public partial class uEditPayType : System.Web.UI.UserControl
    {
        object[] grv_PaymentProcessSource = new[] { 
            new{seq = "01", id = 0, description = "", note = ""},
            new{seq = "02", id = 1, description = "", note = ""},
            new{seq = "03", id = 2, description = "", note = ""},
            new{seq = "04", id = 3, description = "", note = ""},
            new{seq = "05", id = 4, description = "", note = ""},
            new{seq = "06", id = 5, description = "", note = ""},
            new{seq = "07", id = 6, description = "", note = ""}
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            grv_PaymentProcess.DataSource = grv_PaymentProcessSource;
            grv_PaymentProcess.DataBind();
        }
    }
}