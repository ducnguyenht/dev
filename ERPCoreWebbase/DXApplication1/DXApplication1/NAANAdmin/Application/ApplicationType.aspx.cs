using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.NAANAdmin.Application
{
    public partial class ApplicationType : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_APPLICATIONTYPE_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_SYSTEM_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            gvApplicationType.DataSource =
              new[] { 
                     new { key="123", Name = "Dược phẩm",Description="Dược phẩm",
                             RowStatus="Hoạt động"
                    },
                    new { key="124", Name = "Sản xuất", Description="Sản xuất",
                             RowStatus="Hoạt động"
                    },
                    new { key="1233", Name = "Siêu thị", Description="Siêu thị",
                             RowStatus="Hoạt động"
                    },
                    new { key="123", Name = "Khách sạn", Description="Khách sạn",
                             RowStatus="Hoạt động"
                    },
                };
            gvApplicationType.KeyFieldName = "key";
            gvApplicationType.DataBind();
        }
    }
}