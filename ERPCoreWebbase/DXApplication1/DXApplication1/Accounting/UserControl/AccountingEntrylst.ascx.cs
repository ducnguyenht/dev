using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Accounting.UserControl
{
    public partial class AccountingEntrylst : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var datasource = new[]
            {
                new {STT = 1,DienGiai = "Bút toán phát sinh ngày 20/11/2012", GhiChu= "Example", SoTien = "100.000.000"}
            };
            
            ASPxGridView1.DataSource = datasource;
            ASPxGridView1.DataBind();
        }
    }
}