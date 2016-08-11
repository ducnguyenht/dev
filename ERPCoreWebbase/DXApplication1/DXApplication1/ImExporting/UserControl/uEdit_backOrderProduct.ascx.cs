using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.ImExporting.UserControl
{
    public partial class uEdit_backOrderProduct : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gridview_backProduct.DataSource = new[] { 
                    new{sequencenno = "01", productid = "SP00001", productname = "Tên hàng hóa A",unitid = "Vỉ", numberofback = "200", unitprice = "20.000", 
                        total = "4.000.000", reason = "Quá hạn sử dụng", note = ""},
                    new{sequencenno = "02", productid = "SP00002", productname = "Tên hàng hóa B",unitid = "hộp", numberofback = "200", unitprice = "20.000", 
                        total = "4.000.000", reason = "Quá hạn sử dụng", note = ""},
                    new{sequencenno = "03", productid = "SP00003", productname = "Tên hàng hóa C",unitid = "hộp", numberofback = "200", unitprice = "20.000", 
                        total = "4.000.000", reason = "Quá hạn sử dụng", note = ""},
                };
                gridview_backProduct.DataBind();
            }
        }
    }
}