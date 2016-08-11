using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERPCore.Accounting.UserControl
{
    public partial class AccountEdit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var datasource = new[]
            {
                new { OrganizationId=1, ParentOrganizationId=0,LoaiTaiKhoan = "Tài Sản Lưu Động",  SoTaiKhoan="111", Cap = "1" ,TenTaiKhoan="Tiền Mặt", GhiChu="Là Tài Khoản Tiền Mặt" },
                new { OrganizationId=2, ParentOrganizationId=1,LoaiTaiKhoan = "Tài Sản Lưu Động", SoTaiKhoan="1111", Cap = "2" ,TenTaiKhoan="Tiền Việt Nam", GhiChu="Là Tài Khoản Tiền Việt Nam"  },
                new { OrganizationId=3, ParentOrganizationId=1,LoaiTaiKhoan = "Tài Sản Lưu Động", SoTaiKhoan="1112", Cap = "2" ,TenTaiKhoan="Ngoại Tệ", GhiChu="Là Ngoại Tệ" },
                new { OrganizationId=4, ParentOrganizationId=0,LoaiTaiKhoan = "Tài Sản Lưu Động", SoTaiKhoan="112", Cap = "1" ,TenTaiKhoan="Tiền Gửi Ngân Hàng", GhiChu="Là Tiền Gửi Ngân Hàng"  },
                new { OrganizationId=5, ParentOrganizationId=4,LoaiTaiKhoan = "Tài Sản Lưu Động", SoTaiKhoan="1121", Cap = "2" ,TenTaiKhoan="Tiền Việt Nam", GhiChu="Là Tài Khoản Tiền Việt Nam"  },
                new { OrganizationId=6, ParentOrganizationId=4,LoaiTaiKhoan = "Tài Sản Lưu Động", SoTaiKhoan="1122", Cap = "2" ,TenTaiKhoan="Ngoại Tệ", GhiChu="Là Ngoại Tệ" },
                   new { OrganizationId=7, ParentOrganizationId=3,LoaiTaiKhoan = "Tài Sản Lưu Động", SoTaiKhoan="1112.1", Cap = "3" ,TenTaiKhoan="Vàng Bạc, Kim Khí Quí, Đá Quí", GhiChu="Là Vàng Bạc, Kim Khí Quí, Đá Quí" }
            };
            ASPxTreeList1.DataSource = datasource;
            ASPxTreeList1.DataBind();
        }

        protected void grdData_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

        }

        protected void grdData_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

        }

        protected void grdData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {

        }

        protected void cpLine_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }

        

     
    }
}