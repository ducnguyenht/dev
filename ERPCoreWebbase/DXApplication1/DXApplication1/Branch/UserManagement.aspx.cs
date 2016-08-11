using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using BLL.NASID;
using DevExpress.Xpo;
//using DAL.NASID;
using Utility;

namespace WebModule.Branch
{
    public partial class UserManagement : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
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
                return Constant.ACCESSOBJECT_AUTHORIZATION_USERID;
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var datasource = new[] {
                new { OrganizationId = 1, ParentOrganizationId = 0, Name = "Phòng Nhân sự" },
                new { OrganizationId = 2, ParentOrganizationId = 0, Name = "Phòng Kế toán" },
                new { OrganizationId = 3, ParentOrganizationId = 2, Name = "Bộ phận Kế toán tổng hợp" },
                new { OrganizationId = 4, ParentOrganizationId = 2, Name = "Bộ phận Kế toán nội bộ" },
            };
            //Guid root = Guid.Parse("f3ca4e28-3bb4-47ec-8673-5bc5c8bd43ed");
            //trlOrganization.DataSource = organizationBLO.getOrganizationHierachy(root);
            trlOrganization.DataSource = datasource;
            trlOrganization.DataBind();

            ASPxGridView1.DataSource =
                new[] { 
                    new { Fullname = "Võ Nhật Đức", Username = "vnhatduc@yahoo.com.vn",
                             Role = "Nhân sự", Status = "Kích hoạt",Grade = "Trưởng phòng nhân sự", AccessTime = DateTime.Now
                    },
                    new { Fullname = "Trương Đình Bảo Khoa", Username = "khoatdb@gmail.com",
                             Role = "Nhân sự", Status = "Tạm ngưng",Grade = "Phó phòng nhân sự", AccessTime = DateTime.Now
                    },
                };
            ASPxGridView1.DataBind();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            //session.Dispose();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
        }
    }
}