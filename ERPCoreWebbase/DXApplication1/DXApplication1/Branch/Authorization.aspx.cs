using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxTreeList;
using Utility;

namespace WebModule.Branch
{
    public partial class Authorization : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_AUTHORIZATION_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_BRANCH_GROUPID;
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
                     new { key="1236", Code="KH0002", Name = "GMP", Description="Nhà thuốc", Type = "Nhà thuốc",
                             RowStatus="Hoạt động"
                    },
                    new { key="1237", Code="KH0003", Name = "Sản xuất", Description="Sản xuất", Type = "Sản xuất",
                             RowStatus="Hoạt động"
                    },
                    
                };
            gvApplication.KeyFieldName = "key";
            gvApplication.DataBind();


        }

        protected void tlSitemap_Load(object sender, EventArgs e)
        {
            try
            {
                ASPxTreeList detailView = (ASPxTreeList)sender;
                detailView.Load += new EventHandler(detailView_Load);

                var datasource = new[]
                {
                    new { OrganizationId=1, ParentOrganizationId=0, Id="MS0001",name="Trang Chủ", Description="Trang chủ",  Property="" },
                new { OrganizationId=2, ParentOrganizationId=1, Id="MS0002",name="Tổ chức", Description="Tổ chức", Property="123456879" },
                new { OrganizationId=3, ParentOrganizationId=2, Id="MS0003",name="Người dùng", Description="Người sử dụng trong tổ chức", Property="" },
                new { OrganizationId=4, ParentOrganizationId=3, Id="MS0004",name="Danh mục", Description="Danh mục", Property="" },
                new { OrganizationId=5, ParentOrganizationId=3, Id="MS0005",name="Mời", Description="Mời", Property="" },
                new { OrganizationId=6, ParentOrganizationId=5, Id="MS0006",name="Mời qua email", Description="Mời người sử dụng", Property="" },
                new { OrganizationId=7, ParentOrganizationId=5, Id="MS0007",name="Mời theo danh sách file", Description="Mời theo danh sách file", Property="" },
                new { OrganizationId=8, ParentOrganizationId=1, Id="MS0008",name="MailServer", Description="MailServer", Property="" },
                new { OrganizationId=9, ParentOrganizationId=0, Id="MS0009",name="Nghiệp vụ mua", Description="Nghiệp vụ mua", Property="" },
                new { OrganizationId=10, ParentOrganizationId=0, Id="MS00010",name="Nghiệp vụ bán", Description="Nghiệp vụ bán", Property="" },
                new { OrganizationId=11, ParentOrganizationId=0, Id="MS0011",name="Cấu hình Dữ liệu", Description="Cấu hình Dữ liệu", Property="" },
                new { OrganizationId=12, ParentOrganizationId=11, Id="MS0012",name="OperationDB", Description="OperationDB", Property="" },
                new { OrganizationId=13, ParentOrganizationId=11, Id="MS0023",name="Warehouse", Description="Warehouse", Property="" },
                new { OrganizationId=14, ParentOrganizationId=11, Id="MS0014",name="Analytic Services", Description="Analytic Services", Property="" },
                new { OrganizationId=15, ParentOrganizationId=11, Id="MS0015",name="FileServer", Description="FileServer", Property="" },
                new { OrganizationId=16, ParentOrganizationId=11, Id="MS0016",name="Search Services", Description="Search Services", Property="" },
                };
                detailView.DataSource = datasource;
            }
            catch (Exception) { }
        }
        void detailView_Load(object sender, EventArgs e)
        {
            ASPxTreeList detailView = (ASPxTreeList)sender;
            detailView.DataBind();
        }

        protected void tlSitemap_StartNodeEditing(object sender, TreeListNodeEditingEventArgs e)
        {
            ASPxTreeList detailView = (ASPxTreeList)sender;
            detailView.CancelEdit();
        }
    }
}