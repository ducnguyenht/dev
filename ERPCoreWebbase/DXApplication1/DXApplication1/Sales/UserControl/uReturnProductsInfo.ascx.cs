using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.GUI.Sales.userControl
{
    public partial class uReturnProductsInfo : System.Web.UI.UserControl
    {
        object[] product_orderlst = new[]{   
                                new{productid = "SP00001", productname = "Hàng hóa 1", unit = "Hộp",   number = 500, priceunit = "5.000", 
                                    total = "...", duedate = "01/07/2015", note = ""},
                                new{productid = "SP00002", productname = "Hàng hóa 2", unit = "Hộp",   number = 50, priceunit = "5.000", 
                                    total = "...", duedate = "01/08/2014", note = ""},
                                new{productid = "SP00003", productname = "Hàng hóa 3", unit = "Thùng", number = 70, priceunit = "5.000", 
                                     total = "...", duedate = "01/09/2016", note = ""}
                };

        object[] gridview_tangphamSource =
                new[] { 
                new { MaQuaTang = "N/A", TenQuaTang = "Phiếu Giảm Giá", DonViTinh = "N/A", PhanLoai = "Tặng phẩm", GiaTri = "15.000", SoLuong = "2", ThanhTien = "30.000",MoTa = "NAAN Solution"               
                },
                new { MaQuaTang = "N/A", TenQuaTang = "Gấu Bông", DonViTinh = "N/A",PhanLoai = "Tặng phẩm",GiaTri = "15.000",SoLuong = "3", ThanhTien = "45.000", MoTa = "NAAN Solution"   
                },
                new { MaQuaTang = "SP00001", TenQuaTang = "Hàng hóa 1", DonViTinh = "Hộp", PhanLoai = "Hàng hóa", GiaTri = "15.000", SoLuong = "1", ThanhTien = "15.000",MoTa = ""               
                },
                new { MaQuaTang = "SP00002", TenQuaTang = "Hàng hóa 2", DonViTinh = "Hộp", PhanLoai = "Hàng hóa", GiaTri = "15.000", SoLuong = "1", ThanhTien = "15.000",MoTa = ""               
                },
                new { MaQuaTang = "SP00003", TenQuaTang = "Hàng hóa 3", DonViTinh = "Thùng", PhanLoai = "Hàng hóa", GiaTri = "15.000", SoLuong = "1", ThanhTien = "15.000",MoTa = ""               
                },
            };

        object[] grv_serviceIssueSource = new[]{    new{ price = "60.000", serviceid = "DV00001", servicename = "Dịch vụ 1", unit = "Lần", number = 5, total = "300.000", note = "Ví dụ 1",ck ="5%"},
                                                        new{ price = "5.000.000" , serviceid = "DV00002", servicename = "Dịch vụ 2", unit = "Lần", number = 5, total = "25.000.000", note = "Ví dụ 2",ck ="7%"},
                                                        new{ price = "2.000.000" , serviceid = "DV00003", servicename = "Dịch vụ 3", unit = "Lần", number = 5, total = "10.000.000", note = "Ví dụ 3",ck ="6%"}};

        object[] grv_debtSource = new[] { 
                        new{date = "01/05/2013", customerid = "KH0001", customername = "Cty cổ phần dược Sài Gòn", 
                            firstdebt = "500.000.000", issue = "40.000.000", payment ="", lastdebt = "50.000.000"}
                    };

        object[] grv_givecashSource = new[] { 
                        new{date = "01/05/2013", customerid = "KH0001", customername = "Cty cổ phần dược Sài Gòn", 
                            cash = "", description = ""}
                    };
        
        protected void Page_Load(object sender, EventArgs e)
        {
            grd_returnhanghoa.DataSource = product_orderlst;
            grd_returnhanghoa.DataBind();

            gridview_tangpham.DataSource = gridview_tangphamSource;
            gridview_tangpham.DataBind();

            grv_serviceIssue.DataSource = grv_serviceIssueSource;
            grv_serviceIssue.DataBind();

            grv_debt.DataSource = grv_debtSource;
            grv_debt.DataBind();

            grv_givecash.DataSource = grv_givecashSource;
            grv_givecash.DataBind();

            grd_givehanghoa.DataSource = product_orderlst;
            grd_givehanghoa.DataBind();
        }
    }
}