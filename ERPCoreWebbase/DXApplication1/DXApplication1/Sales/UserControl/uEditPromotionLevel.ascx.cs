using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.GUI.Sales.userControl
{
    public partial class uEditPromotionLevel : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grid_hanghoakhuyenmai.DataSource = new[] { 
                new{sequenceno = "01", productid = "SP00001", productname = "Hàng hóa 1", productunitid = "Hộp",
                    discountrate = "5", condition_buy = "5", condition_give = "1"
                },
                new{sequenceno = "02", productid = "SP00002", productname = "Hàng hóa 2", productunitid = "Hộp",
                    discountrate = "6", condition_buy = "10", condition_give = "1"
                },
                new{sequenceno = "03", productid = "SP00003", productname = "Hàng hóa 3", productunitid = "Thùng",
                    discountrate = "10", condition_buy = "100", condition_give = "1"
                },
            };

            grid_hanghoakhuyenmai.DataBind();

            gridview_hanghoatang.DataSource = new[] { 
                new{sequenceno = "01", productid = "SP00001", productname = "Hàng hóa 1", productunitid = "Hộp", lotid = "L00001",
                    condition_buy = "5", condition_give = "1"
                },
                new{sequenceno = "02", productid = "SP00002", productname = "Hàng hóa 2", productunitid = "Hộp", lotid = "L00002",
                    condition_buy = "10", condition_give = "1"
                },
                new{sequenceno = "03", productid = "SP00003", productname = "Hàng hóa 3", productunitid = "Thùng", lotid = "L00003",
                    condition_buy = "100", condition_give = "1"
                },
            };

            gridview_hanghoatang.DataBind();

            grv_bonusProduct.DataSource =
              new[] { 
                    new { TenQuaTang = "Phiếu Giảm Giá", GiaTri = "15.000", SoLuong = "2", ThanhTien = "30.000",MoTa = "NAAN Solution"               
                    },
                    new {  TenQuaTang = "Gấu Bông",GiaTri = "15.000",SoLuong = "3", ThanhTien = "45.000", MoTa = "NAAN Solution"   
                    },
                };
            grv_bonusProduct.DataBind();
        }
    }
}