using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Accounting.UserControl
{
    public partial class uPopupGeneralAccount : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ASPxGridView_ht.JSProperties.ContainsKey("cpst").ToString().ToLower() == "false")
            {
                ASPxGridView_ht.DataSource = new[]{   new{No = "1",Des = "Tổng tiền thu chưa thuế",   TKno = "131",    TKco = "", Total = "100.000.000"  },
                                                new{No = "2",Des = "Tổng tiền thuế",            TKno = "33311",  TKco = "", Total = "10.000.000"   },
                                                new{No = "3",Des = "Tổng tiền thu có thuế",     TKno = "",    TKco = "511", Total = "110.000.000"  }};
                ASPxGridView_ht.KeyFieldName = "No";
                ASPxGridView_ht.DataBind();
                //BTctg.Enabled = false;
            }
        }

        protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            if (e.Parameter.ToString().Equals("new"))
            {
                ASPxGridView_ht.DataSource = new[]{ new{No = "1",Des = " ",TKno = " ",TKco = "", Total = ""  },
                                                new{No = "2",Des = " ",TKno = " ",TKco = "", Total = ""  },
                                                new{No = "3",Des = " ",TKno = " ",TKco = "", Total = ""  }};
                ASPxGridView_ht.KeyFieldName = "No";
                ASPxGridView_ht.DataBind();

               // BTctg.Enabled = true;
                if (ASPxGridView_ht.JSProperties.ContainsKey("cpst").ToString().ToLower() == "false")
                {
                    ASPxGridView_ht.JSProperties["cpst"] = "1";
                }
            }
            else if (e.Parameter.ToString().Equals("ht"))
            {
                ASPxGridView_ht.JSProperties.Remove("cpst");
               // BTctg.Enabled = false;
            }
        }
    }
}
