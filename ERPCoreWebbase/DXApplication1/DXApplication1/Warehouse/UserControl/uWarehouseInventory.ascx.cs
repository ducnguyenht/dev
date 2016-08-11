using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebModule
{
    public partial class uWarehouseInventory : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            object[] grdDataSource = new[] { 
                     new { key="123", code = "MAT001", name = "Mặt hàng 1", lot="123", reciept = "HD001",
                             recieptamount="100", realamount="90", difamount="10", unit="Thùng", description = "Mặt hàng 1",
                             entry="Đã điều chỉnh"
                    },
                    new { key="1234", code = "MAT002", name = "Mặt hàng 2", lot="1234", reciept = "HD001",
                             recieptamount="150", realamount="130", difamount="20", unit="Hộp", description = "Mặt hàng 2",
                             entry="Chưa điều chỉnh"
                    },
                };
            /*StreamReader reader = new StreamReader(Server.MapPath("/Purchasing/datasource/hierarchy.xml"));
            try
            {
                XmlDataSource1.Data = reader.ReadToEnd();
            }
            finally
            {
                reader.Close();
            }
            ASPxTreeList1.DataSourceID = "XmlDataSource1";*/
            var datasource = new[]
            {
                new { OrganizationId=1, ParentOrganizationId=0, name="Kho 1", Description="Phòng Nhân sự", Email="dept.hr@quasapharco.com", Address="Phòng Nhân sự", PhoneNo="123456879" },
                new { OrganizationId=2, ParentOrganizationId=1, name="Dãy 1.1", Description="Phòng Kế toán", Email="dept.accounting@quasapharco.com", Address="Phòng Nhân sự", PhoneNo="123456879" },
                new { OrganizationId=3, ParentOrganizationId=2, name="Kệ 1.1.1", Description="Bộ phận Kế toán tổng hợp", Email="dept.accounting.general@quasapharco.com", Address="Bộ phận Kế toán tổng hợp", PhoneNo="123456879" },
                new { OrganizationId=4, ParentOrganizationId=3, name="Học1.1.1.1", Description="Bộ phận Kế toán nội bộ", Email="dept.accounting.internal@quasapharco.com", Address="Bộ phận Kế toán nội bộ", PhoneNo="123456879" }
            };
            ASPxTreeList1.DataSource = datasource;
            ASPxTreeList1.DataBind();

            grdData.DataSource = grdDataSource;
              
            grdData.KeyFieldName = "key";
            grdData.DataBind();

            grvConfirmData.DataSource = grdDataSource;
            grvConfirmData.DataBind();

            grdDataAccept.DataSource =
              new[] { 
                     new { key="123", code = "MAT001", name = "Mặt hàng 1", lot="123", reciept = "HD001",
                             recieptamount="90", realamount="90", difamount="10", unit="Thùng", description = "Mặt hàng 1"
                    },
                    new { key="1234", code = "MAT002", name = "Mặt hàng 2", lot="1234", reciept = "HD001",
                             recieptamount="130", realamount="130", difamount="20", unit="Hộp", description = "Mặt hàng 2"
                    },
                };
            grdDataAccept.KeyFieldName = "key";
            grdDataAccept.DataBind();


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Gr_nk.DataSource = new[] { new { ID = "MH001", Name = "Mặt hàng A", SLo = "001", Unit = "Hộp", Amount = "10", CT = "CT001" } };
            //Gr_nk.KeyFieldName = "ID";
            //Gr_nk.DataBind();
            //Gr_xk.DataSource = new[] { new { ID = "MH001", Name = "Mặt hàng A", SLo = "001", Unit = "Hộp", Amount = "10", CT = "CT001" } };
            //Gr_xk.KeyFieldName = "ID";
            //Gr_xk.DataBind();

        }
        protected void cpHeader_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            switch (e.Parameter)
            {
                case "refresh":

                    break;
                default:
                    break;
            }
        }

        protected void grdData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            //grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "edit");
        }
        protected void cpLine_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }
    }
}