using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Purchasing.UserControl
{
    public partial class uViewIssueCooperativePrinciples : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grv_debtstatus.DataSource = new[] { new {
                principleid = "NT0001",
                fromdate = "01/01/2012",
                todate = "01/11/2012",
                limmitdebt = "100.000.000",
                currentdebt = "110.000.000",
                status     = "Vượt hạn ngạch"
                }
            };
            grv_debtstatus.DataBind();

            grv_debt.DataSource = new[] { 
                new{saleid = "PDH00002", principleid = "NT00001", saledate = "05/07/2013", numberoverdate = "10",
                    totalsale = "100.000.000", ratecharge = "4", charge = "4.000.000"},
                new{saleid = "PDH00003", principleid = "NT00002",saledate = "07/07/2013", numberoverdate = "30",
                    totalsale = "100.000.000", ratecharge = "4", charge = "4.000.000"}
            };

            grv_debt.DataBind();

            grv_SalePrinciple.DataSource = new[] { 
                new{  principleid = "NT0001", objectiveAmount = "100.000.000", realAmount = "90.000.000", 
                    fromDate = "01/01/2012", toDate = "01/05/2012", rateCharge = "4", charge = "4.000.000"},
                new{  principleid = "NT0001", objectiveAmount = "150.000.000", realAmount = "160.000.000", 
                    fromDate = "02/05/2012", toDate = "01/07/2012", rateCharge = "4", charge = "0"},
                new{  principleid = "NT0001", objectiveAmount = "120.000.000", realAmount = "160.000.000", 
                    fromDate = "02/07/2012", toDate = "01/11/2012", rateCharge = "4", charge = "0"},
            };
            grv_SalePrinciple.DataBind();
        }

        protected void grv_SalePrinciple_HtmlDataCellPrepared(object sender,
            DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.VisibleIndex == 0)
                e.Cell.BackColor = System.Drawing.Color.Yellow;
        }

        protected void grv_debtstatus_HtmlDataCellPrepared(object sender,
           DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.VisibleIndex == 0)
                e.Cell.BackColor = System.Drawing.Color.Yellow;
        }

    } 
}