using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using NAS.DAL;

namespace WebModule.Accounting
{
    public partial class BegingBalance : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
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
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //    var datasource = new[]
            //{
            //    new { OrganizationId=1, ParentOrganizationId=0,  SoTaiKhoan="111",TenTaiKhoan ="Tiền mặt", SoDuNo ="", SoDuCo = "10.000.000.000"},
            //    new { OrganizationId=2, ParentOrganizationId=1, SoTaiKhoan="1111",TenTaiKhoan ="Tiền Việt Nam", SoDuNo ="", SoDuCo = "5.000.000.000" },
            //    new { OrganizationId=3, ParentOrganizationId=1, SoTaiKhoan="1112",TenTaiKhoan ="Ngoại tệ",SoDuNo ="", SoDuCo = "3.000.000.000"},
            //    new { OrganizationId=4, ParentOrganizationId=0, SoTaiKhoan="112",TenTaiKhoan ="Tiền gửi ngân hàng", SoDuNo ="", SoDuCo = "5.000.000.000"  },
            //    new { OrganizationId=5, ParentOrganizationId=4, SoTaiKhoan="1121",TenTaiKhoan ="Tiền Việt Nam",SoDuNo ="", SoDuCo = "3.000.000.000"  },
            //    new { OrganizationId=6, ParentOrganizationId=4, SoTaiKhoan="1122",TenTaiKhoan ="Ngoại tệ", SoDuNo ="", SoDuCo = "2.000.000.000"},
            //    new { OrganizationId=7, ParentOrganizationId=1, SoTaiKhoan="1113",TenTaiKhoan ="Vàng, bạc, kim khí quý, đá quý", SoDuNo ="", SoDuCo = "2.000.000.000" }
            //};
            //    treedata.DataSource = datasource;
            //    treedata.DataBind();
        }

        //protected void treedata_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
        //{
        //    var datasource = new[]
        //    {
        //        new { OrganizationId=1, ParentOrganizationId=0,  SoTaiKhoan="111", SoDuNo ="", SoDuCo = ""},
        //        new { OrganizationId=2, ParentOrganizationId=1, SoTaiKhoan="1111", SoDuNo ="", SoDuCo = "" },
        //        new { OrganizationId=3, ParentOrganizationId=1, SoTaiKhoan="1112",SoDuNo ="", SoDuCo = ""},
        //        new { OrganizationId=4, ParentOrganizationId=0, SoTaiKhoan="112", SoDuNo ="", SoDuCo = ""  },
        //        new { OrganizationId=5, ParentOrganizationId=4, SoTaiKhoan="1121",SoDuNo ="", SoDuCo = ""  },
        //        new { OrganizationId=6, ParentOrganizationId=4, SoTaiKhoan="1122", SoDuNo ="", SoDuCo = ""},
        //        new { OrganizationId=7, ParentOrganizationId=3, SoTaiKhoan="1113", SoDuNo ="", SoDuCo = "" }
        //    };
        //    treedata.DataSource = datasource;
        //    treedata.DataBind();
        //}

    }
}