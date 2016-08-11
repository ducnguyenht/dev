using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxNavBar;
using DevExpress.Web.ASPxFormLayout;
using DevExpress.Web.ASPxRoundPanel;
using DevExpress.Web.ASPxEditors;

namespace WebModule.GUI.usercontrol
{
    public partial class uApprovePromotionOnSales : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gridview_applykm.DataSource = new[] { 
                new{id = "KM00001", name = "Khuyến mãi 01", TotalBonus = "100.000", from = "01/01/2013", 
                    to = "31/12/2012", description = ""
                },
                new{id = "KM00002", name = "Khuyến mãi 02", TotalBonus = "150.000", from = "01/01/2013", 
                    to = "31/12/2012", description = ""
                }
            };

            gridview_applykm.DataBind();

            gridview_tangpham.DataSource =
                new[] { 
                new { stt = "01", MaQuaTang = "N/A", TenQuaTang = "Phiếu Giảm Giá", DonViTinh = "N/A", PhanLoai = "Tặng phẩm", GiaTri = "15.000", 
                    lotid = "SL0001", duedate = "01/01/2015", SoLuong = "2", ThanhTien = "0",MoTa = "NAAN Solution"               
                },
                new { stt = "02", MaQuaTang = "N/A", TenQuaTang = "Gấu Bông", DonViTinh = "N/A",PhanLoai = "Tặng phẩm", GiaTri = "15.000",
                    lotid = "SL0001", duedate = "01/01/2015" , SoLuong = "3", ThanhTien = "0", MoTa = "NAAN Solution"   
                },
                new { stt = "03", MaQuaTang = "SP00001", TenQuaTang = "Hàng hóa 1", DonViTinh = "Hộp", PhanLoai = "Hàng hóa", GiaTri = "15.000", 
                    lotid = "SL0001", duedate = "01/01/2015" , SoLuong = "1", ThanhTien = "0",MoTa = ""               
                },
                new { stt = "04", MaQuaTang = "SP00002", TenQuaTang = "Hàng hóa 2", DonViTinh = "Hộp", PhanLoai = "Hàng hóa", GiaTri = "15.000", 
                    lotid = "SL0001", duedate = "01/01/2015" , SoLuong = "1", ThanhTien = "0",MoTa = ""               
                },
                new { stt = "05", MaQuaTang = "SP00003", TenQuaTang = "Hàng hóa 3", DonViTinh = "Thùng", PhanLoai = "Hàng hóa", GiaTri = "15.000", 
                    lotid = "SL0001", duedate = "01/01/2015" , SoLuong = "1", ThanhTien = "0",MoTa = ""               
                },
            };
            gridview_tangpham.DataBind();
        }

        protected void gridview_applykm_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Detail) { 
                ASPxGridView mastergrid = sender as ASPxGridView;
                ASPxPageControl tabs = mastergrid.FindDetailRowTemplateControl(e.VisibleIndex, "tab_stepkhuyenmai") as ASPxPageControl;
                /* Setting for tab 1 */
                ASPxNavBar navbar_info = tabs.TabPages[0].FindControl("navbar_info") as ASPxNavBar;
                ASPxFormLayout form_infoquyenloi = navbar_info.Groups[1].FindControl("form_infoquyenloi") as ASPxFormLayout;
                ASPxGridView gridview_hanghoatang = form_infoquyenloi.FindControl("gridview_hanghoatang") as ASPxGridView;
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

                /* Setting for tab 2 */

                ASPxNavBar navbar_nextinfo = tabs.TabPages[1].FindControl("navi_nextinfo") as ASPxNavBar;
                form_infoquyenloi = navbar_nextinfo.Groups[1].FindControl("form_infoquyenloi") as ASPxFormLayout;
                gridview_hanghoatang = form_infoquyenloi.FindControl("gridview_hanghoatang") as ASPxGridView;
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
                gridview_hanghoatang.Visible = false;
                lbl_title_khuyenmai2 = form_infoquyenloi.FindControl("lbl_title_khuyenmai2") as ASPxLabel;
                lbl_title_khuyenmai2.Visible = false;

                gridview_hanghoabonus = form_infoquyenloi.FindControl("gridview_hanghoabonus") as ASPxGridView;
                gridview_hanghoabonus.DataSource =
                      new[] { 
                            new { TenQuaTang = "Phiếu Giảm Giá", GiaTri = "15.000", SoLuong = "2", ThanhTien = "30.000",MoTa = "NAAN Solution"               
                            },
                            new {  TenQuaTang = "Gấu Bông",GiaTri = "15.000",SoLuong = "3", ThanhTien = "45.000", MoTa = "NAAN Solution"   
                            },
                        };
                gridview_hanghoabonus.DataBind();
                gridview_hanghoabonus.Visible = true;
                round_chietkhau = form_infoquyenloi.FindControl("round_chietkhau") as ASPxRoundPanel;
                round_chietkhau.Visible = true;
            }
        }
    }
}