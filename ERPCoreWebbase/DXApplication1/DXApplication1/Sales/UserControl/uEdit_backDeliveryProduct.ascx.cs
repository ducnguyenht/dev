using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;

namespace WebModule.GUI.usercontrol
{
    public partial class uEdit_backDeliveryProduct : System.Web.UI.UserControl
    {
        static int flg_master = 0;

        object[] grv_serviceIssueSource = new[]{    new{ price = "60.000", serviceid = "DV00001", servicename = "Dịch vụ 1", unit = "Lần", number = 5, total = "300.000", note = "Ví dụ 1",ck ="5%"},
                                                        new{ price = "5.000.000" , serviceid = "DV00002", servicename = "Dịch vụ 2", unit = "Lần", number = 5, total = "25.000.000", note = "Ví dụ 2",ck ="7%"},
                                                        new{ price = "2.000.000" , serviceid = "DV00003", servicename = "Dịch vụ 3", unit = "Lần", number = 5, total = "10.000.000", note = "Ví dụ 3",ck ="6%"}};
        
        object[] grid_dmorderSource = new[] { new {stt = "01", id = "PDH00001", idkh = "KH00001", tenkh = "Nguyen Van A", ngaymua = "01/07/2013"},
                                            new {stt = "02", id = "PDH00002", idkh = "KH00002", tenkh = "Nguyen Van B", ngaymua = "05/07/2013"},
                                            new {stt = "03", id = "PDH00003", idkh = "KH00003", tenkh = "Nguyen Van C", ngaymua = "07/07/2013"}};

        object[] grd_addproductSource = new[]{   
                                new{productid = "SP00001", productname = "Hàng hóa 1", unit = "Hộp",   number = 500, priceunit = "5.000", total = "2.500.000", lotid = "SL0001", duedate = "01/07/2015", note = ""},
                                new{productid = "SP00002", productname = "Hàng hóa 2", unit = "Hộp",   number = 50,  priceunit = "5.000", total = "2.500.000", lotid = "SL0002", duedate = "01/08/2014", note = ""},
                                new{productid = "SP00003", productname = "Hàng hóa 3", unit = "Thùng", number = 70,  priceunit = "5.000", total = "2.500.000", lotid = "SL0002", duedate = "01/09/2016", note = ""}
                };
        
        object[] product_orderlst = new[]{   
                                new{productid = "SP00001", productname = "Hàng hóa 1", unit = "Hộp",   number = 500, priceunit = "5.000", 
                                    total = "...", duedate = "01/07/2015", note = ""},
                                new{productid = "SP00002", productname = "Hàng hóa 2", unit = "Hộp",   number = 50, priceunit = "5.000", 
                                    total = "...", duedate = "01/08/2014", note = ""},
                                new{productid = "SP00003", productname = "Hàng hóa 3", unit = "Thùng", number = 70, priceunit = "5.000", 
                                     total = "...", duedate = "01/09/2016", note = ""}
                };

        object[] gridview_backProductSource = new[] { 
                new{productid = "SP00001", productname = "Tên hàng hóa A", lotid = "SL0001",unitid = "Vỉ", numberofback = "200", duedate = "01/07/2015",unitprice = "20.000", 
                    total = "4.000.000", reason = "Hỏng", status = ""},
                new{productid = "SP00002", productname = "Tên hàng hóa B", lotid = "SL0002", unitid = "hộp", numberofback = "200", duedate = "01/07/2014", unitprice = "20.000", 
                    total = "4.000.000", reason = "Hết hạn sử dụng", status = ""},
                new{productid = "SP00003", productname = "Tên hàng hóa C", lotid = "SL0003", unitid = "hộp", numberofback = "200", duedate = "01/07/2013", unitprice = "20.000", 
                    total = "4.000.000", reason = "Hết nhu cầu", status = ""}
            };

        object[] grv_debtSource = new[] { 
                        new{date = "01/05/2013", customerid = "KH0001", customername = "Cty cổ phần dược Sài Gòn", 
                            firstdebt = "500.000.000", issue = "40.000.000", payment ="", lastdebt = "50.000.000"}
                    };

        object[] grv_givecashSource = new[] { 
                        new{date = "01/05/2013", customerid = "KH0001", customername = "Cty cổ phần dược Sài Gòn", 
                            cash = "", description = ""}
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
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                flg_master = 0;
            }

            grd_returnhanghoa.DataSource = product_orderlst;
            grd_returnhanghoa.DataBind();

            //grv_debt.DataSource = grv_debtSource;
            //grv_debt.DataBind();

            //grv_givecash.DataSource = grv_givecashSource;
            //grv_givecash.DataBind();


            //grd_givehanghoa.DataSource = product_orderlst;
            //grd_givehanghoa.DataBind();


            /* Lits */
            grid_dmorder.DataSource = grid_dmorderSource;
            grid_dmorder.DataBind();

            grd_addproduct.HtmlRowPrepared += new ASPxGridViewTableRowEventHandler(grd_addproduct_HtmlDataCellPrepared);
            grd_addproduct.DataSource = grd_addproductSource;
            grd_addproduct.DataBind();


            gridview_tangpham.DataSource = gridview_tangphamSource;
            gridview_tangpham.DataBind();

            grv_serviceIssue.DataSource = grv_serviceIssueSource;
            grv_serviceIssue.DataBind();
        }

        protected void update_panel_callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //string para = e.Parameter[0].ToString();
            //if (para == "0")
            //{
            //    panel_grp1.Visible = true;
            //    panel_grp2.Visible = false;
            //    panel_grp3.Visible = false;
            //}

            //if (para == "1")
            //{
            //    panel_grp1.Visible = false;
            //    panel_grp2.Visible = true;
            //    panel_grp3.Visible = false;
            //}

            //if (para == "2")
            //{
            //    panel_grp1.Visible = false;
            //    panel_grp2.Visible = false;
            //    panel_grp3.Visible = true;
            //}

        }

        protected void grid_dmorder_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {

            if (e.RowType == GridViewRowType.Detail)
            {
                ASPxGridView master =  sender as ASPxGridView;
                ASPxGridView grd_selectproduct = master.FindDetailRowTemplateControl(e.VisibleIndex, "grd_selectproduct") as ASPxGridView;

                grd_selectproduct.HtmlRowPrepared += new ASPxGridViewTableRowEventHandler(grd_addproduct_HtmlDataCellPrepared);
                
                grd_selectproduct.DataSource = new[]{   
                                new{stt = 1, productid = "SP00001", productname = "Hàng hóa 1", unit = "Hộp",   number = 500, priceunit = "5.000", total = "2.500.000", lotid = "SL0001", duedate = "01/07/2015", note = "Hàng mới"},
                                new{stt = 2, productid = "SP00002", productname = "Hàng hóa 2", unit = "Hộp",   number = 50,  priceunit = "5.000", total = "2.500.000", lotid = "SL0002", duedate = "01/08/2014", note = "Note"},
                                new{stt = 3, productid = "SP00003", productname = "Hàng hóa 3", unit = "Thùng", number = 70,  priceunit = "5.000", total = "2.500.000", lotid = "SL0002", duedate = "01/09/2016", note = "Ví dụ"}
                };

                grd_selectproduct.DataBind();
            }
        }

        protected void update_paneltab2_callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split(',');
            if (para[0] == "0")
            {
                paneltab2_grp1.Visible = true;
                paneltab2_grp2.Visible = false;
            }

            if (para[0] == "1")
            {
                paneltab2_grp1.Visible = false;
                paneltab2_grp2.Visible = true;
            }

            if (para[1] == "c")
            {
                flg_master = 1;
                grid_dmorder.DataSource = grid_dmorderSource;
                grid_dmorder.DataBind();

                grd_addproduct.DataSource = grd_addproductSource;
                grd_addproduct.DataBind();
            }

        }

        protected void grid_dmorder_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.VisibleIndex == 0 && flg_master == 1)
            {
                e.Cell.BackColor = System.Drawing.Color.Yellow;
            }
        }

        void grd_addproduct_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.VisibleIndex == 1 && flg_master == 1 && e.RowType != GridViewRowType.Detail)
            {   
                e.Row.BackColor = System.Drawing.Color.Yellow;
                //foreach (TableCell cell in e.Row.Cells)
                //{
                //    cell.BackColor = System.Drawing.Color.Yellow;
                //}
            }

        }
    }
}