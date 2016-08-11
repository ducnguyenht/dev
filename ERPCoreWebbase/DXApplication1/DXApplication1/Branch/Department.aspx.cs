using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DevExpress.Web.ASPxGridView;
using Utility;

namespace WebModule.Branch
{
    public partial class Department : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_BRANCH_GROUPID;
            }
        }

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_AUTHORIZATION_ORGANIZATIONID;
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
                new { OrganizationId="BR001", ParentOrganizationId=String.Empty, Name="Chi nhánh 1", Description="Chi nhánh 1", TaxCode="2400110001", Email="branch1@quasapharco.com", Address="Chi nhánh 1", PhoneNo="123456879" },
                new { OrganizationId="BR002", ParentOrganizationId=String.Empty, Name="Chi nhánh 2", Description="Chi nhánh 2", TaxCode="2400110002", Email="branch2@quasapharco.com", Address="Chi nhánh 2", PhoneNo="123456879" },
                new { OrganizationId="BR003", ParentOrganizationId=String.Empty, Name="Chi nhánh 3", Description="Chi nhánh 3", TaxCode="2400110003", Email="branch3@quasapharco.com", Address="Chi nhánh 3", PhoneNo="123456879" },
                new { OrganizationId="BR004", ParentOrganizationId=String.Empty, Name="Chi nhánh 4", Description="Chi nhánh 4", TaxCode="2400110004", Email="branch4@quasapharco.com", Address="Chi nhánh 4", PhoneNo="123456879" },
                new { OrganizationId="BR005", ParentOrganizationId=String.Empty, Name="Chi nhánh 5", Description="Chi nhánh 5", TaxCode="2400110005", Email="branch5@quasapharco.com", Address="Chi nhánh 5", PhoneNo="123456879" },
                new { OrganizationId="BR006", ParentOrganizationId=String.Empty, Name="Chi nhánh 6", Description="Chi nhánh 6", TaxCode="2400110006", Email="branch6@quasapharco.com", Address="Chi nhánh 6", PhoneNo="123456879" },
                new { OrganizationId="BR007", ParentOrganizationId=String.Empty, Name="Chi nhánh 7", Description="Chi nhánh 7", TaxCode="2400110007", Email="branch7@quasapharco.com", Address="Chi nhánh 7", PhoneNo="123456879" },
                new { OrganizationId="BR008", ParentOrganizationId=String.Empty, Name="Chi nhánh 8", Description="Chi nhánh 8", TaxCode="2400110008", Email="branch8@quasapharco.com", Address="Chi nhánh 8", PhoneNo="123456879" },
                new { OrganizationId="BR009", ParentOrganizationId=String.Empty, Name="Chi nhánh 9", Description="Chi nhánh 9", TaxCode="2400110009", Email="branch9@quasapharco.com", Address="Chi nhánh 9", PhoneNo="123456879" }
            };
            trlDepartment.DataSource = datasource;
            trlDepartment.DataBind();
        }

        private void LoadRolesOfOrganizationGridView()
        {
            var datasource = new[] {
                new { OrganizationRoleId=Guid.NewGuid(), Name="Vai trò Cầu kế toán" },
                new { OrganizationRoleId=Guid.NewGuid(), Name="Vai trò Hạch toán" },
                new { OrganizationRoleId=Guid.NewGuid(), Name="Vai trò Kết chuyển" }
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