using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Accounting.UserControl
{
    public partial class uBringBackProduct : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxGridView1.DataSource = new[] {  new{No = "1", Item = "Mặt hàng A", TKno = "000", TKco = "",Amount = "1.000.000",Note = "Ví dụ 1"},
                                                new{No = "2", Item = "Mặt hàng B", TKno = "000", TKco = "",Amount = "500.000",Note = "Ví dụ 1"},
                                                new{No = "3", Item = "Mặt hàng C", TKno = "000", TKco = "",Amount = "1.500.000",Note = "Ví dụ 1"}};
            ASPxGridView1.KeyFieldName = "No";
            ASPxGridView1.DataBind();
        }
    }
}