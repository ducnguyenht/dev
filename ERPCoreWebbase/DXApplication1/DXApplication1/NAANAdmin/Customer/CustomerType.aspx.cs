using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.NAANAdmin.Customer
{
    public partial class CustomerType : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_CUSTOMERTYPE_ID;
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
            gvCustomerType.DataSource =
              new[] { 
                     new { key="123", Name = "Khách vãng lai", Description = "Khách vãng lai",
                             RowStatus="Hoạt động"
                    },
                    new { key="124", Name = "Sản xuất", Description = "Khách hàng sản xuất",
                             RowStatus="Hoạt động"
                    },
                    new { key="1233", Name = "Siêu thị", Description = "Khách siêu thị",
                             RowStatus="Hoạt động"
                    },
                    new { key="123", Name = "Khách sạn", Description = "Khách sạn",
                             RowStatus="Hoạt động"
                    },
                };
            gvCustomerType.KeyFieldName = "key";
            gvCustomerType.DataBind();
        }
    }
}