using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Accounting.UserControl
{
    public partial class uInventoryVoucher_ex : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxGridView_ive.DataSource = new[] {  new{No = "1", Item = "Mặt hàng 1", TKco = " ", TKno = "632",   Value = "1.500.000",Note = "..."},
                                                    new{No = "2", Item = "Mặt hàng 2", TKco = " ", TKno = "632",   Value = "550.000",Note = "..."},
                                                    new{No = "3", Item = "Mặt hàng 3", TKco = " ", TKno = "632",   Value = "150.000",Note = "..."}
                                                    };
            ASPxGridView_ive.KeyFieldName = "No";
            ASPxGridView_ive.DataBind();
        }

    }
}