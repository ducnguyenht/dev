using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Collections;
using DevExpress.Web.ASPxTreeList;

namespace ERPCore.Accounting
{
    public partial class Account : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_GROUPID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        //private class datasample
        //{
        //    public int ProductId { get; set; }
        //    public string Code { get; set; }
        //    public string Name { get; set; }
        //    public string Note { get; set; }            
        //}


        protected void Page_Load(object sender, EventArgs e)
        {
            //ArrayList data = new ArrayList();
            //data.Add(new datasample() { ProductId = 1, Code = "DSTK01", Name = "Hệ Thống Tài Khoản Công Ty Mẹ", Note = ""});
            //data.Add(new datasample() { ProductId = 2, Code = "DSTK02", Name = "Hệ Thống Tài Khoản Đại Lý 1", Note = "" });
            
            //grdData.DataSource = data;
            //grdData.DataBind();
        }

       
         

        //protected void grdData_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        //{
        //    e.Cancel = true;
        //}

        //protected void grdData_StartRowEditing1(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        //{
        //    e.Cancel = true;
        //    grdData.CancelEdit();
        //    grdData.JSProperties.Add("cpEdit", "edit");        
        //}

        //protected void grdData_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        //{
        //    if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Detail)
        //    {
        //        ASPxTreeList tree1 = grdData.FindDetailRowTemplateControl(e.VisibleIndex, "ASPxTreeList_httk") as ASPxTreeList;
        //        var datasource = new[]
        //    {
        //        new { OrganizationId=1, ParentOrganizationId=0,LoaiTaiKhoan = "Tài Sản Lưu Động",  SoTaiKhoan="111", Cap = "1" ,TenTaiKhoan="Tiền Mặt", GhiChu="Là Tài Khoản Tiền Mặt" },
        //        new { OrganizationId=2, ParentOrganizationId=1,LoaiTaiKhoan = "Tài Sản Lưu Động", SoTaiKhoan="1111", Cap = "2" ,TenTaiKhoan="Tiền Việt Nam", GhiChu="Là Tài Khoản Tiền Việt Nam"  },
        //        new { OrganizationId=3, ParentOrganizationId=1,LoaiTaiKhoan = "Tài Sản Lưu Động", SoTaiKhoan="1112", Cap = "2" ,TenTaiKhoan="Ngoại Tệ", GhiChu="Là Ngoại Tệ" },
        //        new { OrganizationId=4, ParentOrganizationId=0,LoaiTaiKhoan = "Tài Sản Lưu Động", SoTaiKhoan="112", Cap = "1" ,TenTaiKhoan="Tiền Gửi Ngân Hàng", GhiChu="Là Tiền Gửi Ngân Hàng"  },
        //        new { OrganizationId=5, ParentOrganizationId=4,LoaiTaiKhoan = "Tài Sản Lưu Động", SoTaiKhoan="1121", Cap = "2" ,TenTaiKhoan="Tiền Việt Nam", GhiChu="Là Tài Khoản Tiền Việt Nam"  },
        //        new { OrganizationId=6, ParentOrganizationId=4,LoaiTaiKhoan = "Tài Sản Lưu Động", SoTaiKhoan="1122", Cap = "2" ,TenTaiKhoan="Ngoại Tệ", GhiChu="Là Ngoại Tệ" },
        //        new { OrganizationId=7, ParentOrganizationId=3,LoaiTaiKhoan = "Tài Sản Lưu Động", SoTaiKhoan="1112.1", Cap = "3" ,TenTaiKhoan="Vàng Bạc, Kim Khí Quí, Đá Quí", GhiChu="Là Vàng Bạc, Kim Khí Quí, Đá Quí" }
        //    };
        //        tree1.DataSource = datasource;
        //        tree1.DataBind();
        //    }
        //}
    }
}