using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.GUI.usercontrol
{
    public partial class uViewSalesInfo : System.Web.UI.UserControl
    {
        object[] product_orderlst = new[]{   
                                new{stt = 1, productid = "SP00001", productname = "Hàng hóa 1", unit = "Hộp",   number = 500, priceunit = "5.000", total = "2.500.000", lotid = "SL0001", duedate = "01/07/2015", note = "Hàng mới"},
                                new{stt = 2, productid = "SP00002", productname = "Hàng hóa 2", unit = "Hộp",   number = 50,  priceunit = "5.000", total = "2.500.000", lotid = "SL0002", duedate = "01/08/2014", note = "Note"},
                                new{stt = 3, productid = "SP00003", productname = "Hàng hóa 3", unit = "Thùng", number = 70,  priceunit = "5.000", total = "2.500.000", lotid = "SL0002", duedate = "01/09/2016", note = "Ví dụ"}
                };
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxGridView2_hanghoa.DataSource = product_orderlst;
            ASPxGridView2_hanghoa.DataBind();

            ASPxGridView3_dichvu.DataSource = new[]{    new{ price = "60.000", serviceid = "DV00001", servicename = "Dịch vụ 1", unit = "Lần", number = 5, total = "300.000", note = "Ví dụ 1",ck ="5%"},
                                                        new{ price = "5.000.000" , serviceid = "DV00002", servicename = "Dịch vụ 2", unit = "Lần", number = 5, total = "25.000.000", note = "Ví dụ 2",ck ="7%"},
                                                        new{ price = "2.000.000" , serviceid = "DV00003", servicename = "Dịch vụ 3", unit = "Lần", number = 5, total = "10.000.000", note = "Ví dụ 3",ck ="6%"}};
            ASPxGridView3_dichvu.KeyFieldName = "c1";
            ASPxGridView3_dichvu.DataBind();

            gridview_tdgh.DataSource = new[]{      
                new{stt = "01", plandate = "01/07/2013", productid = "SP00001", productname = "Hàng hóa 1",
                    unit = "Hộp", lotid = "SL0001", plannumber = 500, note = "Ví dụ 1", 
                    realdate = "01/07/2013", realnumber = "500"},
                new{stt = "02", plandate = "02/07/2013", productid = "SP00002", productname = "Hàng hóa 2",
                    unit = "Hộp", lotid = "SL0002", plannumber = 600, note = "Ví dụ 2", 
                    realdate = "01/07/2013", realnumber = "600"},
                new{stt = "03", plandate = "03/07/2013", productid = "SP00003", productname = "Hàng hóa 3",
                    unit = "Thùng", lotid = "SL0002", plannumber = 700, note = "Ví dụ 3",
                    realdate = "01/07/2013", realnumber = "700"
                }};

            gridview_tdgh.KeyFieldName = "c1";
            gridview_tdgh.DataBind();

            gridview_tdtt.DataSource = new[]{   
                new{c1 = 1, c2 = "01/07/2013",c3 = "10.000.000", c4 = "01/07/2013", c5 = "10.000.000",
                    c6 = "Ví dụ 1", c7 = "Ví dụ 1", c8 = "Ví dụ 1"},
                new{c1 = 2, c2 = "01/07/2013",c3 = "10.000.000", c4 = "02/07/2013",c5 = "15.000.000",
                    c6 = "Ví dụ 2", c7 = "Ví dụ 2", c8 = "Ví dụ 2"},
                new{c1 = 3, c2 = "01/07/2013", c3 = "10.000.000", c4 = "03/07/2013",c5 = "100.000.000",
                    c6 = "Ví dụ 3", c7 = "Ví dụ 3",c8 ="Ví dụ 3"}
                };
            gridview_tdtt.KeyFieldName = "c1";
            gridview_tdtt.DataBind();
        }
    }
}