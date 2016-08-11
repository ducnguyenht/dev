using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.NAANAdmin.Customer
{
    public partial class Customer : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_CUSTOMER_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_AUTHORIZATION_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            gvCustomer.DataSource =
              new[] { 
                     new { key="123", Code="KH0001", Name = "Sâm Ngọc Linh", Taxcode="55432323", Type = "Dược phẩm",
                             Admin="Nguyễn văn A", RowStatus="Hoạt động"
                    },
                     new { key="1234", Code="KH0002", Name = "Hội Doanh Nghiệp Trẻ", Taxcode="55432323", Type = "CRM",
                             Admin="Nguyễn văn B", RowStatus="Hoạt động"
                    },
                    new { key="1234", Code="KH0003", Name = "Vĩnh Đan", Taxcode="55432323", Type = "Xuất nhập khẩu",
                             Admin="Nguyễn văn C", RowStatus="Hoạt động"
                    },
                    
                };
            gvCustomer.KeyFieldName = "key";
            gvCustomer.DataBind();
        }
    }
}