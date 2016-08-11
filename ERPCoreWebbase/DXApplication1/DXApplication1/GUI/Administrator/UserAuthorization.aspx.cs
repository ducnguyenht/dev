using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using BLL.NASID;
using DevExpress.Xpo;
//using DAL.NASID;
using System.Collections;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using Utility;

namespace DXApplication1.GUI.Administrator
{
    public partial class UserAuthorization : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
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
                return Constant.ACCESSOBJECT_SYSTEM_DEPARTMENT_ID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        //private OrganizationBLO organizationBLO;
        //private Session session;

        protected void Page_Init(object sender, EventArgs e)
        {
            //session = XpoHelper.GetNewSession();
            //organizationBLO = new OrganizationBLO(session);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadOrganizationHierachyTreeList();
            LoadUserGridView();
            LoadRolesOfOrganizationGridView();
            LoadAccessObjectsOfRoleGridView();
        }

        private void LoadOrganizationHierachyTreeList()
        {
            var datasource = new[] {
                new { OrganizationId = 1, ParentOrganizationId = 0, Name = "Công ty CP TM Dược Sâm Ngọc Linh Quảng Nam" },
                new { OrganizationId = 2, ParentOrganizationId = 1, Name = "Đại lý 1" },
                new { OrganizationId = 3, ParentOrganizationId = 1, Name = "Đại lý 2" },
                new { OrganizationId = 4, ParentOrganizationId = 1, Name = "Đại lý 3" },
                new { OrganizationId = 5, ParentOrganizationId = 1, Name = "Đại lý 4" },
                new { OrganizationId = 6, ParentOrganizationId = 1, Name = "Đại lý 5" },
                new { OrganizationId = 7, ParentOrganizationId = 1, Name = "Đại lý 6" },
                new { OrganizationId = 8, ParentOrganizationId = 1, Name = "Đại lý 7" },
                new { OrganizationId = 9, ParentOrganizationId = 1, Name = "Đại lý 8" },
                new { OrganizationId = 10, ParentOrganizationId = 1, Name = "Đại lý 9" }
            };
            //Guid root = Guid.Parse("f3ca4e28-3bb4-47ec-8673-5bc5c8bd43ed");
            //trlOrganization.DataSource = organizationBLO.getOrganizationHierachy(root);
            trlOrganization.DataSource = datasource;
            trlOrganization.DataBind();
        }

        private void LoadUserGridView()
        {
            var datasource = new[]
            {
                new { OrganizationUserId=Guid.NewGuid(), Email = "user1@gmail.com", JobTitle = "Kế toán trưởng", FullName="Nguyễn Văn A", RowStatus="Chờ kích hoạt", isAdmin=false },
                new { OrganizationUserId=Guid.NewGuid(), Email = "admin@gmail.com", JobTitle = "Admin", FullName="Trần Văn B", RowStatus="Online", isAdmin=true }
            }.ToList();

            grdUser.DataSource = datasource;
            grdUser.DataBind();
        }

        private ArrayList LoadDataRoleOfUser()
        {
            var datasource = new ArrayList
            {
                new { UserRoleId=Guid.NewGuid(), Name="Kế toán" }
            };
            return datasource;
        }

        private void LoadRolesOfOrganizationGridView()
        {
            var datasource = new[] {
                new { OrganizationRoleId=Guid.NewGuid(), Name="Quản lý chứng từ" },
                new { OrganizationRoleId=Guid.NewGuid(), Name="Quản trị tổ chức" },
                new { OrganizationRoleId=Guid.NewGuid(), Name="Quản lý mua hàng" },
                new { OrganizationRoleId=Guid.NewGuid(), Name="Quản lý bán hàng" }
            };
            grdRolesOfOganization.DataSource = datasource;
            grdRolesOfOganization.DataBind();
        }

        private void LoadAccessObjectsOfRoleGridView()
        {
            var datasource = new[] {
                new { AccessObjectId=Guid.NewGuid(), Name="Quản lý Phiếu mua hàng" },
                new { AccessObjectId=Guid.NewGuid(), Name="Quản lý Phiếu bán hàng" },
                new { AccessObjectId=Guid.NewGuid(), Name="Quản lý Hóa đơn nhập" },
                new { AccessObjectId=Guid.NewGuid(), Name="Quản lý Hóa đơn xuất" }
            };
            grdAccessObjectsOfRole.DataSource = datasource;
            grdAccessObjectsOfRole.DataBind();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            //session.Dispose();
            //organizationBLO.Dispose();
        }

        protected void grdRolesOfUser_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSource = LoadDataRoleOfUser();
        }

        protected void lnkRoleDetails_Init(object sender, EventArgs e)
        {
            ASPxHyperLink hyperLink = sender as ASPxHyperLink;
            hyperLink.ClientSideEvents.Click =
                String.Format("function(s,e) {{ {0}.Show(); }}", pcRoleDetails.ClientInstanceName);
        }

        protected void grdRolesOfUser_Load(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.DataBind();
        }

        protected void lnkRoleDetails_Init1(object sender, EventArgs e)
        {
            ASPxHyperLink hyperLink = sender as ASPxHyperLink;
            hyperLink.ClientSideEvents.Click =
                String.Format("function(s,e) {{ {0}.Show(); }}", pcRoleDetails.ClientInstanceName);
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