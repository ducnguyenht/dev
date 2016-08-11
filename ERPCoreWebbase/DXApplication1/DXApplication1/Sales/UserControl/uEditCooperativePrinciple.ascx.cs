using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.GUI.usercontrol
{
    public partial class uEditCooperativePrinciple : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grv_SalePrinciple.DataSource = new[] { 
                new {objectiveAmount = "100.000.000", fromDate = "20/01/2012", 
                    toDate = "20/05/2012", rateCharge = "5"},
                new {objectiveAmount = "150.000.000", fromDate = "21/05/2012", 
                    toDate = "21/07/2012", rateCharge = "5"},
                new {objectiveAmount = "200.000.000", fromDate = "20/08/2012", 
                    toDate = "21/12/2012", rateCharge = "5"},
            };
            grv_SalePrinciple.DataBind();
        }
    }
}