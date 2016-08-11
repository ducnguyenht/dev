using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.Purchasing.UserControl
{
    public partial class uReturnProductsInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gridview_backProduct.DataSource = new[] { 
                new{productid = "SP00001", productname = "Tên hàng hóa A", lotid = "SL0001",unitid = "Vỉ", numberofback = "200", duedate = "01/07/2015",unitprice = "20.000", 
                    total = "4.000.000", reason = "Hỏng", status = ""},
                new{productid = "SP00002", productname = "Tên hàng hóa B", lotid = "SL0002", unitid = "hộp", numberofback = "200", duedate = "01/07/2014", unitprice = "20.000", 
                    total = "4.000.000", reason = "Hết hạn sử dụng", status = ""},
                new{productid = "SP00003", productname = "Tên hàng hóa C", lotid = "SL0003", unitid = "hộp", numberofback = "200", duedate = "01/07/2013", unitprice = "20.000", 
                    total = "4.000.000", reason = "Hết nhu cầu", status = ""}
            };
            gridview_backProduct.DataBind();
        }
    }
}