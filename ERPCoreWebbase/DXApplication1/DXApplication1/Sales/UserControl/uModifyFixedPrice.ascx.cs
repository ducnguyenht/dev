using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

namespace WebModule.GUI.Sales.userControl
{
    public partial class uModifyFixedPrice : System.Web.UI.UserControl
    {
        object[]  grd_pricepolicySource = new[] { 
                new { sequenceno = "01", price_methodid = "PPT0001", price_methodname = "Phương pháp tính giá bán quý 1 2011", From = "01/01/2011", To = "31/03/2011", status = "Đang hoạt động"},
                new { sequenceno = "02", price_methodid = "PPT0002", price_methodname = "Phương pháp tính giá bán quý 2 2011", From = "01/04/2011", To = "30/06/2011", status = "Đang hoạt động"},
                new { sequenceno = "03", price_methodid = "PPT0003", price_methodname = "Phương pháp tính giá bán quý 3 2011", From = "01/07/2012", To = "30/09/2011", status = "Ngưng hoạt động"},
            };

        object[]  grv_referencePriceSource = new [] { 
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

        protected void Page_Load(object sender, EventArgs e)
        {
            grd_pricepolicy.DataSource = grd_pricepolicySource;
            grd_pricepolicy.DataBind();

            grv_FixedPrice.DataSource = grv_referencePriceSource;
            grv_FixedPrice.DataBind();
        }

        protected void grd_pricepolicy_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Detail)
            {
                ASPxGridView mastergrv = sender as ASPxGridView;
                ASPxGridView detailgrv = mastergrv.FindDetailRowTemplateControl(e.VisibleIndex, "grv_referencePrice") as ASPxGridView;
                detailgrv.DataSource = grv_referencePriceSource;
                detailgrv.DataBind();
            }

        }
    }
}