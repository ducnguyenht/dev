using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Purchasing.UserControl
{
    public partial class uMonitorProcessOfSalesTotalOnDetail : System.Web.UI.UserControl
    {
        object[] grv_SalePrincipleSource = new[] { 
                new{  seq = "01", objectiveAmount = "100.000.000", realAmount = "90.000.000", 
                    fromDate = "01/01/2012", toDate = "01/05/2012", rateCharge = "4", charge = "4.000.000", recharge = "4.000.000"},
                new{  seq = "02", objectiveAmount = "150.000.000", realAmount = "160.000.000", 
                    fromDate = "02/05/2012", toDate = "01/07/2012", rateCharge = "4", charge = "0", recharge = ""},
                new{  seq = "03", objectiveAmount = "120.000.000", realAmount = "160.000.000", 
                    fromDate = "02/07/2012", toDate = "01/11/2012", rateCharge = "4", charge = "0", recharge = ""},
            };

        protected void Page_Load(object sender, EventArgs e)
        {
            grv_SalePrinciple.DataSource = grv_SalePrincipleSource;
            grv_SalePrinciple.DataBind();
        }

        protected void grv_SalePrinciple_HtmlDataCellPrepared(object sender,
            DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.VisibleIndex == 0)
                e.Cell.BackColor = System.Drawing.Color.Yellow;
        }
    }
}