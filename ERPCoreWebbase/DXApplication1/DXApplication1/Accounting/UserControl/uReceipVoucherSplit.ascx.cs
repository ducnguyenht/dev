using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Accounting.UserControl
{
    public partial class uReceipVoucherSplit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxGridView1.DataSource = new[] {  new{ID = "PT001",Date = "01/07/2013",Amount = "100.000.000"},
                                                new{ID = "PT002",Date = "03/07/2013",Amount = "150.000.000"},
                                                new{ID = "PT003",Date = "05/07/2013",Amount = "200.000.000"},
                                                new{ID = "PT004",Date = "08/07/2013",Amount = "160.000.000"}};
            ASPxGridView1.KeyFieldName = "ID";
            ASPxGridView1.DataBind();
            ASPxGridView2.DataSource = new[] {  new{STT = "1",ID = "PT001_A",NN = "Công ty TNHH Minh Phát",DC = "19 Lạc Long Quân F6 QTB",Amount = "10.000.000"},
                                                new{STT = "2",ID = "PT001_B",NN = "Cửa Hàng Song Hiệp",DC = "2B Bùi Thị Xuân F2 QTB",Amount = "50.000.000"},
                                                new{STT = "3",ID = "PT001_C",NN = "Cửa Hàng Thuận Việt",DC = "38 Bùi Thị Xuân F2 QTB",Amount = "40.000.000"}};
            ASPxGridView2.KeyFieldName = "STT";
            ASPxGridView2.DataBind();

            grdData.DataSource = new[] {    new{STT = "1",Code = "PT001_A", Customer="Công ty TNHH Minh Phát", Address="19 Lạc Long Quân F6 QTB", dg="...", status="...", Amount="10.000.000"},
                                            new{STT = "2",Code = "PT001_B", Customer="Cửa Hàng Song Hiệp", Address="2B Bùi Thị Xuân F2 QTB", dg="...", status="...", Amount="50.000.000"},
                                            new{STT = "3",Code = "PT001_C", Customer="Cửa Hàng Thuận Việt", Address="38 Bùi Thị Xuân F2 QTB", dg="...", status="...", Amount="40.000.000"}};
            grdData.KeyFieldName = "STT";
            grdData.DataBind();

        }
    }
}