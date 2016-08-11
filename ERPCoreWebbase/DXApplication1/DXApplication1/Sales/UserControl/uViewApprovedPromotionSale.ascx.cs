using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxRoundPanel;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxNavBar;
using DevExpress.Web.ASPxFormLayout;
using DevExpress.Web.ASPxTabControl;

namespace WebModule.GUI.usercontrol
{
    public partial class uViewApprovedPromotionSale : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            gridview_tangpham.DataSource =
                new[] { 
                new { stt = "01", MaQuaTang = "N/A", TenQuaTang = "Phiếu Giảm Giá", DonViTinh = "N/A", PhanLoai = "Tặng phẩm", GiaTri = "15.000",
                    lotid = "SL0001", duedate = "01/01/2015" , SoLuong = "2", 
                    ThanhTien = "0",MoTa = "NAAN Solution"               
                },
                new { stt = "02", MaQuaTang = "N/A", TenQuaTang = "Gốm Minh Long", DonViTinh = "N/A",PhanLoai = "Tặng phẩm",GiaTri = "15.000",
                    lotid = "SL0001", duedate = "01/01/2015" , SoLuong = "3", 
                    ThanhTien = "0", MoTa = "NAAN Solution"   
                },
                new { stt = "03", MaQuaTang = "SP00001", TenQuaTang = "Hàng hóa 1", DonViTinh = "Hộp", PhanLoai = "Hàng hóa", GiaTri = "15.000", 
                    lotid = "SL0001", duedate = "01/01/2015" , SoLuong = "1", 
                    ThanhTien = "0",MoTa = ""               
                },
                new { stt = "04", MaQuaTang = "SP00002", TenQuaTang = "Hàng hóa 2", DonViTinh = "Hộp", PhanLoai = "Hàng hóa", GiaTri = "15.000", 
                    lotid = "SL0001", duedate = "01/01/2015" , SoLuong = "1", 
                    ThanhTien = "0",MoTa = ""               
                },
                new { stt = "05", MaQuaTang = "SP00003", TenQuaTang = "Hàng hóa 3", DonViTinh = "Thùng", PhanLoai = "Hàng hóa", GiaTri = "15.000", 
                    lotid = "SL0001", duedate = "01/01/2015" , SoLuong = "1", 
                    ThanhTien = "0",MoTa = ""               
                },
            };
            gridview_tangpham.DataBind();

            /* Setting for tab 1 */
            ASPxFormLayout form_infoquyenloi = navbar_info.Groups[1].FindControl("form_infoquyenloi") as ASPxFormLayout;
            ASPxGridView gridview_hanghoatang = form_infoquyenloi.FindControl("gridview_hanghoatang") as ASPxGridView;
            gridview_hanghoatang.DataSource = new[] { 
                        new{productid = "SP00001", productname = "Hàng hóa 1", productunitid = "Hộp", lotid = "L00001",
                            duedate = "01/01/2015", condition_buy = "5", condition_give = "1"
                        },
                        new{productid = "SP00002", productname = "Hàng hóa 2", productunitid = "Hộp", lotid = "L00002",
                            duedate = "01/01/2015", condition_buy = "10", condition_give = "1"
                        },
                        new{productid = "SP00003", productname = "Hàng hóa 3", productunitid = "Thùng", lotid = "L00003",
                            duedate = "01/01/2015", condition_buy = "100", condition_give = "1"
                        },
                    };
            gridview_hanghoatang.DataBind();
            gridview_hanghoatang.Visible = true;
            ASPxLabel lbl_title_khuyenmai2 = form_infoquyenloi.FindControl("lbl_title_khuyenmai2") as ASPxLabel;
            lbl_title_khuyenmai2.Visible = true;

            ASPxGridView gridview_hanghoabonus = form_infoquyenloi.FindControl("gridview_hanghoabonus") as ASPxGridView;
            gridview_hanghoabonus.DataSource =
                  new[] { 
                            new { TenQuaTang = "Phiếu Giảm Giá", GiaTri = "15.000", SoLuong = "2", ThanhTien = "30.000",MoTa = "NAAN Solution"               
                            },
                            new {  TenQuaTang = "Gấu Bông",GiaTri = "15.000",SoLuong = "3", ThanhTien = "45.000", MoTa = "NAAN Solution"   
                            },
                        };
            gridview_hanghoabonus.DataBind();
            gridview_hanghoabonus.Visible = false;

            ASPxLabel lbl_title_khuyenmai3 = form_infoquyenloi.FindControl("lbl_title_khuyenmai3") as ASPxLabel;
            ASPxRoundPanel round_chietkhau = form_infoquyenloi.FindControl("round_chietkhau") as ASPxRoundPanel;
            round_chietkhau.Visible = false;
            lbl_title_khuyenmai3.Visible = false;
        }

   
       
    }
}