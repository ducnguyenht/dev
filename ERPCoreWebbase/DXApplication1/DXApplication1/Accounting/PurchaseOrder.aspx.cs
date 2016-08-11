using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.Accounting
{
    public partial class Phieumua : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxGridView1.DataSource = new[] {  new{c1 = "1",c2 = "Ví dụ 1",sd = "Bán hàng 1",c3 = "CTY ABC",c4 = "01/07/2013",c5 = "Đã duyệt"},
                                                new{c1 = "2",c2 = "Ví dụ 4",sd = "Bán hàng 2",c3 = "CTY XYZ",c4 = "03/07/2013",c5 = "Đã duyệt"},
                                                new{c1 = "3",c2 = "Ví dụ 3",sd = "Bán hàng 1",c3 = "Cửa hàng A",c4 = "05/07/2013",c5 = "Chưa duyệt"},
                                                new{c1 = "4",c2 = "Ví dụ 6",sd = "Bán hàng 2",c3 = "Đại lí ABC",c4 = "06/07/2013",c5 = "Chưa duyệt"},
                                                new{c1 = "5",c2 = "Ví dụ 2",sd = "Bán hàng 1",c3 = "Nhà thuốc A",c4 = "07/07/2013",c5 = "Chưa duyệt"},
                                                new{c1 = "6",c2 = "Ví dụ 7",sd = "Bán hàng 1",c3 = "Nhà thuốc B",c4 = "08/07/2013",c5 = "Chưa duyệt"}};
            ASPxGridView1.KeyFieldName = "c1";
            ASPxGridView1.DataBind();
        }

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
    }
}