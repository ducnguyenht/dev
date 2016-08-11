using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule.NAANAdmin.Deploy
{
    public partial class Deploy : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_DEPLOY_ID;
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
            gvApplication.DataSource =
              new[] { 
                     new { key="123", Code="KH0001", Name = "GDP", Description="Dược phẩm", Type = "Dược phẩm",
                             RowStatus="Hoạt động"
                    },
                     new { key="123", Code="KH0002", Name = "GMP", Description="Nhà thuốc", Type = "Nhà thuốc",
                             RowStatus="Hoạt động"
                    },
                    new { key="123", Code="KH0003", Name = "Sản xuất", Description="Sản xuất", Type = "Sản xuất",
                             RowStatus="Hoạt động"
                    },
                    
                };
            gvApplication.KeyFieldName = "key";
            gvApplication.DataBind();
        }
    }
}