using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXApplication1.GUI.usercontrol
{
    public partial class test_promotional_program : System.Web.UI.UserControl
    {
        object[] product_orderlst = new[]{   
                                new{productid = "SP00001", productname = "Hàng hóa 1", unit = "Hộp",   number = 500, priceunit = "5.000", total = "2.500.000", lotid = "SL0001", duedate = "01/07/2015", note = "Hàng mới"},
                                new{productid = "SP00002", productname = "Hàng hóa 2", unit = "Hộp",   number = 50,  priceunit = "5.000", total = "2.500.000", lotid = "SL0002", duedate = "01/08/2014", note = "Note"},
                                new{productid = "SP00003", productname = "Hàng hóa 3", unit = "Thùng", number = 70,  priceunit = "5.000", total = "2.500.000", lotid = "SL0002", duedate = "01/09/2016", note = "Ví dụ"}
                };
        
        protected void Page_Load(object sender, EventArgs e)
        {
            gridview_cauhinhhhtest.DataSource = new[] { 
                    new{productid = "SP00001", 
                        product_name = "Dexmedetomidin", 
                        productgrpid = "Dược phẩm Châu Á",
                        manufacturergrpid = "Nhóm miền Nam",
                        manufacturerpid = "Cty dược Cần Thơ",
                        suppliergrppid = "Nhóm miền Trung",
                        supplierpid = "Nhà phân phối Minh Châu",
                        cost = "10.000", 
                        profit = "1000", 
                        tax = "10", 
                        totalsale = "100.000"
                    },
                    new{productid = "SP00002", 
                        product_name = "Diazepam",
                        productgrpid = "Dược phẩm Châu Á",
                        manufacturergrpid = "Nhóm miền Nam",
                        manufacturerpid = "Cty dược Cần Thơ",
                        suppliergrppid = "Nhóm miền Nam",
                        supplierpid = "Nhà phân phối Minh Châu",
                        cost = "15.000", 
                        profit = "1000", 
                        tax = "10", 
                        totalsale = "100.000" 
                    },
                    new{productid = "SP00003", 
                        product_name = "Lidocain (hydroclorid)", 
                        productgrpid = "Dược phẩm Châu Âu",
                        manufacturergrpid = "Nhóm miền Nam",
                        manufacturerpid = "Cty dược Hồ Chí Minh",
                        suppliergrppid = "Nhóm miền Trung",
                        supplierpid = "Nhà phân phối Minh Châu",
                        cost = "5000", 
                        profit = "500", 
                        tax = "10", 
                        totalsale = "50.000"
                    }
                };
            gridview_cauhinhhhtest.DataBind();

            gridview_hanghoa.DataSource = product_orderlst;
            gridview_hanghoa.KeyFieldName = "productid";
            gridview_hanghoa.DataBind();
        }
    }
}