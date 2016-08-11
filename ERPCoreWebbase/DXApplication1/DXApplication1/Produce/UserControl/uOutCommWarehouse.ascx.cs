using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Produce.UserControl
{
    public partial class uOutCommWarehouse : System.Web.UI.UserControl
    {
        private void BindGrid()
        {

            grdData.DataSource =
               new[] { 
                     new { key="123", code = "HDP001", date = "25-07-2013", manufacturer="Khách hàng 1", rowstatus = "Kích hoạt",
                             description = "Hóa đơn mua hàng"
                    },
                    new { key="1234", code = "HDP002", date = "24-07-2013", manufacturer="Khách hàng 2", rowstatus = "Kích hoạt",
                             description = "Hóa đơn mua hàng"
                    },
                };
            grdData.KeyFieldName = "key";
            grdData.DataBind();

            grdDataAccept.DataSource =
               new[] { 
                     new { key="123", code="MH001", name="Mặt hàng 1", unit = "Thùng", lot = "0123",
                             amount= "500.000", description="Mặt hàng 1", codedepence="HDP001"
                    },
                    new { key="234", code="MH002", name="Mặt hàng 2", unit = "Hộp", lot = "0124",
                             amount= "1.000.000", description="Mặt hàng 2", codedepence="HDP002"
                    },
                };
            grdDataAccept.KeyFieldName = "key";
            grdDataAccept.DataBind();

            ASPxGridView1.DataSource =
                   new[] { 
                     new { key="123", code="MH001", name="Mặt hàng 1", unit = "Thùng", lot = "0123",
                             amount= "500.000", description="Mặt hàng 1", codedepence="HDP001"
                    },
                    new { key="1234", code="MH002", name="Mặt hàng 2", unit = "Hộp", lot = "0124",
                             amount= "1.000.000", description="Mặt hàng 2", codedepence="HDP002"
                    },
                };
            ASPxGridView1.KeyFieldName = "key";
            ASPxGridView1.DataBind();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack || !Page.IsCallback)
            {
                BindGrid();
            }
            else
            {

            }
        }

        protected void grdData_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "new");

            Session["productMode"] = null;
        }

        protected void grdData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            BindGrid();

            Guid guid = new Guid(e.EditingKeyValue.ToString());


            //Session["productMode"] = vs;

            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "edit");
        }

        protected void grdData_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

            e.Cancel = true;
            BindGrid();
        }

        protected void cpHeader_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            switch (e.Parameter)
            {
                case "refresh":
                    BindGrid();
                    break;
                default:
                    break;
            }
        }

        protected void grdDataDetail_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView detailView = (ASPxGridView)sender;
                detailView.Load += new EventHandler(detailView_Load);

                detailView.DataSource =
                   new[] { 
                     new { key="123", code="MH001", name="Mặt hàng 1", unit = "Thùng", lot = "0123",
                             amount= "500.000", description="Mặt hàng 1", codedepence="HDP001"
                    },
                    new { key="1234", code="MH002", name="Mặt hàng 2", unit = "Hộp", lot = "0124",
                             amount= "1.000.000", description="Mặt hàng 2", codedepence="HDP002"
                    },
                };
                detailView.KeyFieldName = "key";
            }
            catch (Exception) { }
        }
        void detailView_Load(object sender, EventArgs e)
        {
            ASPxGridView detailView = (ASPxGridView)sender;
            detailView.DataBind();
        }

        protected void grid_hanghoachietsuat_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView detailView = (ASPxGridView)sender;
                detailView.Load += new EventHandler(detailView_Load);

                detailView.DataSource = new[] { 
                    new{sequenceno = "01", productid = "SP00001", productname = "Hàng hóa 1", 
                        productunitid = "Hộp", lot = "SL0001", SoLuong = "500", DonGia  = "5.000", discountcash = "50.000",
                        discountrate = "5", condition_buy = "5", condition_give = "1"
                    },
                    new{sequenceno = "02", productid = "SP00002", productname = "Hàng hóa 2", 
                        productunitid = "Hộp", lot = "SL0001", SoLuong = "500", DonGia  = "5.000", discountcash = "50.000",
                        discountrate = "6", condition_buy = "10", condition_give = "1"
                    },
                    new{sequenceno = "03", productid = "SP00003", productname = "Hàng hóa 3", 
                        productunitid = "Thùng", lot = "SL0002", SoLuong = "500", DonGia  = "5.000", discountcash = "50.000",
                        discountrate = "10", condition_buy = "100", condition_give = "1"
                    },
                };
                detailView.KeyFieldName = "sequenceno";
            }
            catch (Exception) { }
        }

        protected void gridview_tangpham_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView detailView = (ASPxGridView)sender;
                detailView.Load += new EventHandler(detailView_Load);

                detailView.DataSource =
                    new[] { 
                    new { stt = "01", MaQuaTang = "N/A", TenQuaTang = "Phiếu Giảm Giá", DonViTinh = "N/A", PhanLoai = "Tặng phẩm", GiaTri = "15.000", SoLuong = "2", ThanhTien = "0",MoTa = "NAAN Solution"               
                    },
                    new { stt = "02", MaQuaTang = "N/A", TenQuaTang = "Gấu Bông", DonViTinh = "N/A",PhanLoai = "Tặng phẩm",GiaTri = "15.000",SoLuong = "3", ThanhTien = "0", MoTa = "NAAN Solution"   
                    },
                    new { stt = "03", MaQuaTang = "SP00001", TenQuaTang = "Hàng hóa 1", DonViTinh = "Hộp", PhanLoai = "Hàng hóa", GiaTri = "15.000", SoLuong = "1", ThanhTien = "0",MoTa = ""               
                    },
                    new { stt = "04", MaQuaTang = "SP00002", TenQuaTang = "Hàng hóa 2", DonViTinh = "Hộp", PhanLoai = "Hàng hóa", GiaTri = "15.000", SoLuong = "1", ThanhTien = "0",MoTa = ""               
                    },
                    new { stt = "05", MaQuaTang = "SP00003", TenQuaTang = "Hàng hóa 3", DonViTinh = "Thùng", PhanLoai = "Hàng hóa", GiaTri = "15.000", SoLuong = "1", ThanhTien = "0",MoTa = ""               
                    },
                };
                detailView.KeyFieldName = "stt";
            }
            catch (Exception) { }
        }

        protected void txtCode_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {

        }

        protected void txtName_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {

        }

        protected void cpLine_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }
    }
}