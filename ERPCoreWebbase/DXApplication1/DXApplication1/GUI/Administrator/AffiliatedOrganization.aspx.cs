using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Collections;
using Utility;

namespace DXApplication1.GUI.Administrator
{
    public partial class AffiliatedOrganization : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_SYSTEM_GROUPID;
            }
        }

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_SYSTEM_ORGANIZATION_ID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }
    
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadDepartmentTreeList();
            LoadRolesOfOrganizationGridView();
            LoadAccessObjectsOfRoleGridView();
        }

        private void LoadDepartmentTreeList()
        {
            var datasource = new[]
            {
                new { OrganizationId=1, ParentOrganizationId=0, Name="Đại lý 1", Description="Đại lý 1", Email="daily1@quasapharco.com", Address="Đại lý 1", PhoneNo="123456879" },
                new { OrganizationId=2, ParentOrganizationId=0, Name="Đại lý 2", Description="Đại lý 2", Email="daily2@quasapharco.com", Address="Đại lý 2", PhoneNo="123456879" },
                new { OrganizationId=3, ParentOrganizationId=0, Name="Đại lý 3", Description="Đại lý 3", Email="daily3@quasapharco.com", Address="Đại lý 3", PhoneNo="123456879" },
                new { OrganizationId=4, ParentOrganizationId=0, Name="Đại lý 4", Description="Đại lý 4", Email="daily4@quasapharco.com", Address="Đại lý 4", PhoneNo="123456879" },
                new { OrganizationId=5, ParentOrganizationId=0, Name="Đại lý 5", Description="Đại lý 5", Email="daily5@quasapharco.com", Address="Đại lý 5", PhoneNo="123456879" },
                new { OrganizationId=6, ParentOrganizationId=0, Name="Đại lý 6", Description="Đại lý 6", Email="daily6@quasapharco.com", Address="Đại lý 6", PhoneNo="123456879" },
                new { OrganizationId=7, ParentOrganizationId=0, Name="Đại lý 7", Description="Đại lý 7", Email="daily7@quasapharco.com", Address="Đại lý 7", PhoneNo="123456879" },
                new { OrganizationId=8, ParentOrganizationId=0, Name="Đại lý 8", Description="Đại lý 8", Email="daily8@quasapharco.com", Address="Đại lý 8", PhoneNo="123456879" },
                new { OrganizationId=9, ParentOrganizationId=0, Name="Đại lý 9", Description="Đại lý 9", Email="daily9@quasapharco.com", Address="Đại lý 9", PhoneNo="123456879" }
            };
            trlDepartment.DataSource = datasource;
            trlDepartment.DataBind();
        }

        private void LoadRolesOfOrganizationGridView()
        {
            var datasource = new[] {
                new { OrganizationRoleId=Guid.NewGuid(), Name="Vai trò Đại lý 1" },
            };
            grdRolesOfOganization.DataSource = datasource;
            grdRolesOfOganization.DataBind();
        }

        private void LoadAccessObjectsOfRoleGridView()
        {
            var datasource = new[] {
                new { AccessObjectId=Guid.NewGuid(), Name="Chức năng Cầu kế toán" },
                new { AccessObjectId=Guid.NewGuid(), Name="Chức năng Sổ kế toán" }
            };
            grdAccessObjectsOfRole.DataSource = datasource;
            grdAccessObjectsOfRole.DataBind();
        }

        private ArrayList LoadDataPermissions()
        {
            var datasource = new ArrayList
            {
                new { PermissionId=Guid.NewGuid(), Name="Đọc", Authorization=true },
                new { PermissionId=Guid.NewGuid(), Name="Tạo", Authorization=true },
                new { PermissionId=Guid.NewGuid(), Name="Cập nhật", Authorization=true },
                new { PermissionId=Guid.NewGuid(), Name="Xóa", Authorization=false }
            };
            return datasource;
        }

        protected void grdPermissions_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.Load += new EventHandler(grid_Load);
            grid.DataSource = LoadDataPermissions();
        }

        void grid_Load(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.DataBind();
        }
    }
}