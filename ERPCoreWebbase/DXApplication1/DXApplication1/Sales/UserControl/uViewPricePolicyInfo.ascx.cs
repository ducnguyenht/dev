using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.GUI.Sales.userControl
{
    public partial class uViewPricePolicyInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxGridView_thamkhaogia.DataSource = ASPxGridView_thamkhaogia.DataSource = new[] { 
                new{
                        productid = "SP00001", 
                        product_name = "Dexmedetomidin", 
                        productgrpid = "Dược phẩm Châu Á",
                        manufacturergrpid = "Nhóm miền Nam",
                        manufacturerpid = "Cty dược Cần Thơ",
                        suppliergrppid = "Nhóm miền Trung",
                        supplierpid = "Nhà phân phối Minh Châu",
                        cost = "10.000", 
                        profit = "1000", 
                        tax = "10", 
                        income_price = "95.000",
                        ref_price = "100.000",
                        fixedprice = "110.000"
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
                        income_price = "90.000",
                        ref_price = "95.000" ,
                        fixedprice = "100.000"
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
                        income_price = "70.000",
                        ref_price = "80.000",
                        fixedprice = "90.000"
                    }
            };

            ASPxGridView_thamkhaogia.DataBind();
        }
    }
}