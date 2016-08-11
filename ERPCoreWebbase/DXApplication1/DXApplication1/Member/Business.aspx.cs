using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.Member
{
    public partial class Business : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_MEMBER_BUSINESS_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_MEMBER_GROUPID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            grdDoanhNghiep.DataSource = new[] { new { madn = "12", tendn = "công ty dược", trangthai = "đang hoạt động", mota = "sản xuất" } };
            grdDoanhNghiep.DataBind();

            grdNhomDoanhNghiep.DataSource = new[] { new { manhomdn = "24", tennhomdn = "y tế", trangthai = "đang hoạt động", mota = "sản xuất" } };
            grdNhomDoanhNghiep.DataBind();
        }
    }
}