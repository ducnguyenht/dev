using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXApplication1.GUI.usercontrol
{
    public partial class product_composition_edit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grid_productgrp.DataSource = new[] { 
                new{
                    productgrp_id   = "MNHH0001",
                    productgrp_name = "Nhóm đặc trị 1",
                    description     = "",
                    note            = ""
                },
                new{
                    productgrp_id   = "MNHH0002",
                    productgrp_name = "Nhóm đặc trị 2",
                    description     = "",
                    note            = ""
                },
                new{
                    productgrp_id   = "MNHH0003",
                    productgrp_name = "Nhóm đặc trị 3",
                    description     = "",
                    note            = ""
                },
            };

            grid_productgrp.DataBind();

            grid_manufacturer.DataSource = new[] { 
                new { supplierid   = "MNCC00009",
                      suppliername = "Nhà cung cấp 9",
                      description  = ""
                },
                new { supplierid   = "MNCC00010",
                      suppliername = "Nhà cung cấp 10",
                      description  = ""
                },
            };
            grid_manufacturer.DataBind();

            var datasource = new[]
            {
                new { OrganizationId=1, ParentOrganizationId=0, code="MS001", name="Thùng lớn", description="Thùng lớn là đơn vị tính cao nhất", amount=""  },
                new { OrganizationId=2, ParentOrganizationId=1, code="LT002",name="Hợp lớn", description="1 Thùng lớn chứa 20 Hộp lớn", amount="20"  },
                new { OrganizationId=3, ParentOrganizationId=2, code="LT003",name="Vĩ", description="1 Hộp lớn chứa 30 Vĩ", amount="30"  },
                new { OrganizationId=4, ParentOrganizationId=3, code="LT004",name="Viên nén tròn", description="1 Vĩ chứa 12 Viên nén tròn", amount="12"  }
            };
            ASPxTreeList1.DataSource = datasource;
            ASPxTreeList1.DataBind();
        }
    }
}