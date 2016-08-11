using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.NAANAdmin.Authorization.UserControl
{
    public partial class uAuthorization : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadDepartmentTreeList();
            //organizationBLO = new OrganizationBLO(session);
            var datasource = new[]
            {
                new { OrganizationId=1, ParentOrganizationId=0, isAlow=true, id="PB001", Name="Phòng Nhân sự", type="", admin="Nguyễn Văn A", status="Hoạt động" },
                new { OrganizationId=2, ParentOrganizationId=0, isAlow=false, id="PB002", Name="Phòng Kế toán", type="", admin="Nguyễn Văn A", status="Hoạt động"},
                new { OrganizationId=3, ParentOrganizationId=2, isAlow=false, id="PB0021", Name="Bộ phận Kế toán tổng hợp", type="", admin="Nguyễn Văn A", status="Hoạt động" },
                new { OrganizationId=4, ParentOrganizationId=2, isAlow=false, id="PB0022", Name="Bộ phận Kế toán nội bộ", type="", admin="Nguyễn Văn A", status="Hoạt động" }
            };
            //Guid root = Guid.Parse("f3ca4e28-3bb4-47ec-8673-5bc5c8bd43ed");
            //trlOrganization.DataSource = organizationBLO.getOrganizationHierachy(root);
            trlOrganization.DataSource = datasource;
            trlOrganization.DataBind();

            ASPxGridView1.DataSource =
                new[] { 
                    new { isAlow=true, Fullname = "Võ Nhật Đức", Username = "vnhatduc@yahoo.com.vn",
                             Role = "Nhân sự", Status = "Kích hoạt",Grade = "Trưởng phòng nhân sự", AccessTime = DateTime.Now
                    },
                    new {isAlow=true, Fullname = "Trương Đình Bảo Khoa", Username = "khoatdb@gmail.com",
                             Role = "Nhân sự", Status = "Tạm ngưng",Grade = "Phó phòng nhân sự", AccessTime = DateTime.Now
                    },
                };
            ASPxGridView1.DataBind();
        }

        private void LoadDepartmentTreeList()
        {
            var datasource = new[]
            {
                new { OrganizationId=1, ParentOrganizationId=0, isAlow=true, id="PB001", name="Phòng Nhân sự", type="", admin="Nguyễn Văn A", status="Hoạt động" },
                new { OrganizationId=2, ParentOrganizationId=0, isAlow=false, id="PB002", name="Phòng Kế toán", type="", admin="Nguyễn Văn A", status="Hoạt động"},
                new { OrganizationId=3, ParentOrganizationId=2, isAlow=false, id="PB0021", name="Bộ phận Kế toán tổng hợp", type="", admin="Nguyễn Văn A", status="Hoạt động" },
                new { OrganizationId=4, ParentOrganizationId=2, isAlow=false, id="PB0022", name="Bộ phận Kế toán nội bộ", type="", admin="Nguyễn Văn A", status="Hoạt động" }
            };
            trlDepartment.DataSource = datasource;
            trlDepartment.DataBind();
        }
    }
}