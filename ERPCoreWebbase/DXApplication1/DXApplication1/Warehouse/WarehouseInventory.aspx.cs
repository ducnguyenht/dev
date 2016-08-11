using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

namespace WebModule
{
    public partial class WarehouseInventory : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_WAREHOUSEINVENTORY_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }
        protected void Page_Init(object sender, EventArgs e)
        {
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

            grdData.DataSource =
              new[] { 
                     new { key="123", code = "MAT001", name = "Mặt hàng 1", lot="123", reciept = "HD001",
                             recieptamount="100", realamount="90", difamount="10", unit="Thùng", description = "Mặt hàng 1",
                             entry="Đã bút toán"
                    },
                    new { key="1234", code = "MAT002", name = "Mặt hàng 2", lot="1234", reciept = "HD001",
                             recieptamount="150", realamount="130", difamount="20", unit="Hộp", description = "Mặt hàng 2",
                             entry="Chưa bút toán"
                    },
                };
            grdData.KeyFieldName = "key";
            grdData.DataBind();

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
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "edit");
        }
    }
}