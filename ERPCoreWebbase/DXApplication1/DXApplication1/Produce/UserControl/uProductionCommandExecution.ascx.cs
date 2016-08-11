using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Produce.UserControl
{
    public partial class uProductionCommandExecution : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxGridView1.DataSource = new[] {  new{ ID = "CM001", IDExecution= "TT001", ProductID = "SP001", Date = "09/08/2013", RQ = "100",RStart = "8h30",REnd = "10h30", Sl = "100", Note ="Ví dụ 1", ExecutionValue = "100000"},
                                                new{ ID = "CM002", IDExecution= "TT002", ProductID = "SP002",Date = "09/08/2013", RQ = "200",RStart = "9h30",REnd = "11h30", Sl = "200", Note ="Ví dụ 2" , ExecutionValue = "100000"},
                                                new{ ID = "CM003", IDExecution= "TT003", ProductID = "SP003",Date = "09/08/2013", RQ = "300",RStart = "10h30",REnd = "12h30", Sl = "300", Note ="Ví dụ 3" , ExecutionValue = "100000"},
                                                new{ ID = "CM004",  IDExecution= "TT004", ProductID = "SP004",Date = "09/08/2013", RQ = "140",RStart = "12h30",REnd = "15h30", Sl = "140", Note ="Ví dụ 4" , ExecutionValue = "100000"}};
            ASPxGridView1.KeyFieldName = "ID";
            ASPxGridView1.DataBind();
        }
    }
}