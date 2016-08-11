using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Accounting.UserControl
{
    public partial class uDeclareBillingVoucher : System.Web.UI.UserControl
    {
        object[] grv_productsSource = new[]{   
                                new{stt = 1, productid = "SP00001", productname = "Hàng hóa 1", unit = "Hộp",   number = 500, priceunit = "5.000", total = "2.500.000", lotid = "SL0001", duedate = "01/07/2015", note = ""},
                                new{stt = 2, productid = "SP00002", productname = "Hàng hóa 2", unit = "Hộp",   number = 50,  priceunit = "5.000", total = "2.500.000", lotid = "SL0002", duedate = "01/08/2014", note = ""},
                                new{stt = 3, productid = "SP00003", productname = "Hàng hóa 3", unit = "Thùng", number = 70,  priceunit = "5.000", total = "2.500.000", lotid = "SL0002", duedate = "01/09/2016", note = ""}
                };

        object[] grv_servicesSource = new[]{    new{ price = "60.000", serviceid = "DV00001", servicename = "Dịch vụ 1", unit = "Lần", number = 5, total = "300.000", note = "Ví dụ 1",ck ="5%"},
                                                        new{ price = "5.000.000" , serviceid = "DV00002", servicename = "Dịch vụ 2", unit = "Lần", number = 5, total = "25.000.000", note = "Ví dụ 2",ck ="7%"},
                                                        new{ price = "2.000.000" , serviceid = "DV00003", servicename = "Dịch vụ 3", unit = "Lần", number = 5, total = "10.000.000", note = "Ví dụ 3",ck ="6%"}};

        protected void Page_Load(object sender, EventArgs e)
        {
            grv_products.DataSource = grv_productsSource;
            grv_products.DataBind();

            grv_services.DataSource = grv_servicesSource;
            grv_services.DataBind();
        }
    }
}