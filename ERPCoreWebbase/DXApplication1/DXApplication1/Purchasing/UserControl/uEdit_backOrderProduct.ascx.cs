using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Purchasing.UserControl
{
    public partial class uEdit_backOrderProduct : System.Web.UI.UserControl
    {
        object[] product_orderlst = new[]{   
                                new{stt = 1, productid = "SP00001", productname = "Hàng hóa 1", unit = "Hộp",   number = 500, priceunit = "5.000", 
                                    saleid = "PB0001", lotid = "SL0001", duedate = "01/07/2015", note = "Hàng mới"},
                                new{stt = 2, productid = "SP00002", productname = "Hàng hóa 2", unit = "Hộp",   number = 50, priceunit = "5.000", 
                                    saleid = "PB0002", lotid = "SL0002", duedate = "01/08/2014", note = "Note"},
                                new{stt = 3, productid = "SP00003", productname = "Hàng hóa 3", unit = "Thùng", number = 70, priceunit = "5.000", 
                                    saleid = "PB0002", lotid = "SL0002", duedate = "01/09/2016", note = "Ví dụ"}
                };
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

            grdData.DataSource = new[] { 
                new{SupplierId = 1, Code = "BG001", Date = "19/07/2013", Supplier = "Nhà cung cấp Mỹ Châu", Department = "Phòng Marketing" },
                new{SupplierId = 2, Code = "BG002", Date = "20/07/2013", Supplier = "Nhà cung cấp Kiến Việt", Department = "Phòng Tài Chính" },
                new{SupplierId = 3, Code = "BG003", Date = "29/07/2013", Supplier = "Nhà cung cấp Mỹ Châu", Department = "Phòng kế hoạch vật tư" },
                new{SupplierId = 4, Code = "BG004", Date = "20/08/2013", Supplier = "Nhà cung cấp Kiến Việt", Department = "Phòng Giám Đốc" },
                new{SupplierId = 5, Code = "BG005", Date = "19/07/2013", Supplier = "Nhà cung cấp Mỹ Châu", Department = "Phòng Marketing" },
                new{SupplierId = 6, Code = "BG006", Date = "20/07/2013", Supplier = "Nhà cung cấp Kiến Việt", Department = "Phòng Tài Chính" },
                new{SupplierId = 7, Code = "BG007", Date = "29/07/2013", Supplier = "Nhà cung cấp Mỹ Châu", Department = "Phòng kế hoạch vật tư" },
                new{SupplierId = 8, Code = "BG008", Date = "20/08/2013", Supplier = "Nhà cung cấp Kiến Việt", Department = "Phòng Giám Đốc" },
                new{SupplierId = 9, Code = "BG009", Date = "20/08/2013", Supplier = "Nhà cung cấp Kiến Việt", Department = "Phòng Giám Đốc" },
                new{SupplierId = 10, Code = "BG0010", Date = "19/07/2013", Supplier = "Nhà cung cấp Mỹ Châu", Department = "Phòng Marketing" },
                new{SupplierId = 11, Code = "BG0011", Date = "20/07/2013", Supplier = "Nhà cung cấp Kiến Việt", Department = "Phòng Tài Chính" },
                new{SupplierId = 12, Code = "BG0012", Date = "29/07/2013", Supplier = "Nhà cung cấp Mỹ Châu", Department = "Phòng kế hoạch vật tư" },


            };
            grdData.DataBind();
            
      
        }
   
        protected void gridview_backProduct_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
        }

        protected void grdData_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Detail) {
                ASPxGridView grdData = sender as ASPxGridView;
                ASPxGridView grv_hanghoa = grdData.FindDetailRowTemplateControl(e.VisibleIndex, "grv_hanghoa") as ASPxGridView;
                grv_hanghoa.DataSource = new[] { 
                    new{productid = "SP0001", productname = "Hàng hóa 1", unit = "Hộp", number = "1.000", numberinventory="200", returnNumber = "200", 
                        priceunit = "100.000", total= "100.000.000", lotid = "SL0001", duedate = "20/10/2015", reason = "Hỏng",note = ""},
                    new{productid = "SP0002", productname = "Hàng hóa 2", unit = "Hộp", number = "1.000", numberinventory="300", returnNumber = "200",
                        priceunit = "100.000", total= "100.000.000", lotid = "SL0001", duedate = "20/10/2015", reason = "Hết hạn sử dụng",note = ""},
                    new{productid = "SP0003", productname = "Hàng hóa 3", unit = "Hộp", number = "1.000", numberinventory="300", returnNumber = "200",
                        priceunit = "100.000", total= "100.000.000", lotid = "SL0001", duedate = "20/10/2015", reason = "Hết nhu cầu",note = ""},
                    new{productid = "SP0004", productname = "Hàng hóa 4", unit = "Hộp", number = "1.000", numberinventory="300", returnNumber = "200",
                        priceunit = "100.000", total= "100.000.000", lotid = "SL0001", duedate = "20/10/2015", reason = "Hết nhu cầu", note = ""},
                    new{productid = "SP0005", productname = "Hàng hóa 1", unit = "Hộp", number = "1.000", numberinventory="250",returnNumber = "200", 
                        priceunit = "100.000", total= "100.000.000", lotid = "SL0001", duedate = "20/10/2015", reason = "Hỏng",note = ""},
                    new{productid = "SP0006", productname = "Hàng hóa 2", unit = "Hộp", number = "1.000", numberinventory="200", returnNumber = "200",
                        priceunit = "100.000", total= "100.000.000", lotid = "SL0001", duedate = "20/10/2015", reason = "Hết hạn sử dụng",note = ""},
                    new{productid = "SP0007", productname = "Hàng hóa 3", unit = "Hộp", number = "1.000", numberinventory="200", returnNumber = "200",
                        priceunit = "100.000", total= "100.000.000", lotid = "SL0001", duedate = "20/10/2015", reason = "Hết nhu cầu",note = ""},
                    new{productid = "SP0008", productname = "Hàng hóa 4", unit = "Hộp", number = "1.000", numberinventory="200", returnNumber = "200",
                        priceunit = "100.000", total= "100.000.000", lotid = "SL0001", duedate = "20/10/2015", reason = "Hết nhu cầu", note = ""},
                    new{productid = "SP0009", productname = "Hàng hóa 1", unit = "Hộp", number = "1.000", numberinventory="200", returnNumber = "200", 
                        priceunit = "100.000", total= "100.000.000", lotid = "SL0001", duedate = "20/10/2015", reason = "Hỏng",note = ""},
                    new{productid = "SP00010", productname = "Hàng hóa 2", unit = "Hộp", number = "1.000", numberinventory="200", returnNumber = "200",
                        priceunit = "100.000", total= "100.000.000", lotid = "SL0001", duedate = "20/10/2015", reason = "Hết hạn sử dụng",note = ""},
                    new{productid = "SP00011", productname = "Hàng hóa 3", unit = "Hộp", number = "1.000", numberinventory="200", returnNumber = "200",
                        priceunit = "100.000", total= "100.000.000", lotid = "SL0001", duedate = "20/10/2015", reason = "Hết nhu cầu",note = ""},
                    new{productid = "SP00012", productname = "Hàng hóa 4", unit = "Hộp", number = "1.000", numberinventory="200", returnNumber = "200",
                        priceunit = "100.000", total= "100.000.000", lotid = "SL0001", duedate = "20/10/2015", reason = "Hết nhu cầu", note = ""},
                };
                grv_hanghoa.DataBind();
            }
        }
    }
}