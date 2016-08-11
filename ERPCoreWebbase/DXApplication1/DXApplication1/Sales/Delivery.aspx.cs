using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;

namespace DXApplication1.GUI
{
    public partial class Delivery : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        void bind_tiendogiaohang()
        {
            grdBuyingProductCategory.DataSource = new[] { 
                        new{ MaDonHang = "SP001", MaKhachHang = "KH001", TenKhachHang = "Example 1", NgayMua = "01/07/2013", TongThanhTien = "10.000.000"},
                         new{ MaDonHang = "SP002", MaKhachHang = "KH002", TenKhachHang = "Example 2", NgayMua = "02/07/2013", TongThanhTien = "15.000.000"},
                           new{ MaDonHang = "SP003", MaKhachHang = "KH003", TenKhachHang = "Example 3", NgayMua = "03/07/2013", TongThanhTien = "100.000.000"}
                    };
            grdBuyingProductCategory.DataBind();

           
        }

        void bind_tiendothanhtoan()
        {
            grdBuyingProductCategory.DataSource = new[] { 
                        new{ MaDonHang = "SP006", MaKhachHang = "KH004", TenKhachHang = "Example 1", NgayMua = "01/08/2013", TongThanhTien = "12.000.000"},
                         new{ MaDonHang = "SP007", MaKhachHang = "KH005", TenKhachHang = "Example 2", NgayMua = "02/08/2013", TongThanhTien = "18.000.000"},
                           new{ MaDonHang = "SP008", MaKhachHang = "KH006", TenKhachHang = "Example 3", NgayMua = "03/08/2013", TongThanhTien = "120.000.000"}
                    };
            grdBuyingProductCategory.DataBind();
        }

   
        protected void Page_Load(object sender, EventArgs e)
        {
           

            string strIndex = pagetiendo.ActiveTabIndex.ToString();
            if (strIndex == "0")
            {
                bind_tiendogiaohang();
            }
            else
            {
                bind_tiendothanhtoan();
            }
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

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        public string AccessObjectId
        {
            get { return Constant.ACCESSOBJECT_SALES_ODERSPROCESS_ID; }
        }

        public string AccessObjectGroupId
        {
            get { return Constant.ACCESSOBJECT_SALES_GROUPID; }
        }

        protected void grdBuyingProductCategory_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string str = e.Parameters;
            if (str == "0")
            {
                bind_tiendogiaohang();
            }
            else
            {
                bind_tiendothanhtoan();
            }
        }   
    }
}