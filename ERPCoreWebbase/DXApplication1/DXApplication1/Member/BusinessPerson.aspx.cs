using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.Member
{
    public partial class BusinessPerson : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_MEMBER_BUSINESSPERSON_ID;
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
            grdDoanhNhan.DataSource = new[] { new { madn = "11", tendn = "Hồ Văn Sông", trangthai = "đang hoạt động", mota = "kinh doanh dược phẩm" } };
            grdDoanhNhan.DataBind();

            grdNhomDN.DataSource = new[] { new { manhomdn = "23", tennhomdn = "y tế", trangthai = "đang hoạt động", mota = "sản xuất thuốc" } };
            grdNhomDN.DataBind();
        }
    }
}