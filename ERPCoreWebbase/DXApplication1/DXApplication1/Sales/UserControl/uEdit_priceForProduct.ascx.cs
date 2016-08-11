using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.GUI.usercontrol
{
    public partial class uEdit_priceForProduct : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                lbl_productid.Text = "SP00002";
                lbl_productname.Text = "Diazepam";
                txt_cost.Text = "15.000";
                txt_profit.Text = "10";
                txt_tax.Text = "5";
            }
        }
    }
}