using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule.NAANAdmin.Customer.Usercontrol
{
    public partial class uCustomerService : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvApplication.DataSource =
             new[] { 
                     new { key="123", Code="KH0001", Name = "GDP", Type = "Dược phẩm",
                             Description="Dược phẩm", RowStatus="Hoạt động"
                    },
                     new { key="1234", Code="KH0002", Name = "GMP", Type = "Nhà thuốc",
                             Description="Nhà thuốc", RowStatus="Hoạt động"
                    },
                    new { key="12734", Code="KH0003", Name = "Sản xuất", Type = "Sản xuất",
                             Description="Sản xuất", RowStatus="Hoạt động"
                    },
                    
                };
            gvApplication.KeyFieldName = "key";
            gvApplication.DataBind();

            gvApplication1.DataSource =
             new[] { 
                     new { key="123", Code="KH0001", Name = "GDP", Type = "Dược phẩm",
                             Description="Dược phẩm", RowStatus="Hoạt động"
                    },
                     new { key="1234", Code="KH0002", Name = "GMP", Type = "Nhà thuốc",
                             Description="Nhà thuốc", RowStatus="Hoạt động"
                    },
                                        
                };
            gvApplication1.KeyFieldName = "key";
            gvApplication1.DataBind();
        }
    }
}