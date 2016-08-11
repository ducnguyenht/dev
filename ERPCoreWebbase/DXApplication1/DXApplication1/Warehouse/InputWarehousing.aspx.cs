using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Utility;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

namespace DXApplication1.Purchasing
{
    public partial class WarehouseTesting : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_INPUTWAREHOUSE_ID;
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
            var datasource = new[]
            {
                new { OrganizationId=1, ParentOrganizationId=0, name="Kho 1", Description="Phòng Nhân sự", Email="dept.hr@quasapharco.com", Address="Phòng Nhân sự", PhoneNo="123456879" },
                new { OrganizationId=2, ParentOrganizationId=1, name="Dãy 1.1", Description="Phòng Kế toán", Email="dept.accounting@quasapharco.com", Address="Phòng Nhân sự", PhoneNo="123456879" },
                new { OrganizationId=3, ParentOrganizationId=2, name="Kệ 1.1.1", Description="Bộ phận Kế toán tổng hợp", Email="dept.accounting.general@quasapharco.com", Address="Bộ phận Kế toán tổng hợp", PhoneNo="123456879" },
                new { OrganizationId=4, ParentOrganizationId=3, name="Học1.1.1.1", Description="Bộ phận Kế toán nội bộ", Email="dept.accounting.internal@quasapharco.com", Address="Bộ phận Kế toán nội bộ", PhoneNo="123456879" }
            };
            ASPxTreeList1.DataSource = datasource;
            ASPxTreeList1.DataBind();
            grdData0.DataSource =
              new[] { 
                     new { key="123", code = "ML0001", name = "Nguyễn văn A", date = "25-07-2013",warehouse="Kho1",
                             status="Đã sắp xếp"
                    },
                    new { key="1234", code = "ML0002", name = "Nguyễn văn B", date = "25-07-2013",warehouse="Kho2",
                             status="Chưa sắp xếp"
                    },
                };
            grdData0.KeyFieldName = "key";
            grdData0.DataBind();

            //grdData.DataSource =
            //  new[] { 
            //         new { key="123", code = "MAT001", name = "Mặt hàng 1", lot="123", date="25-5-2014", reciept = "HD001",
            //                 recieptamount="100", unit="Thùng", description = "Mặt hàng 1"
            //        },
            //        new { key="1234", code = "MAT002", name = "Mặt hàng 2", lot="1234", date="25-7-2015",reciept = "HD001",
            //                 recieptamount="150", unit="Hộp", description = "Mặt hàng 2"
            //        },
            //    };
            //grdData.KeyFieldName = "key";
            //grdData.DataBind();

            grdDataAccept.DataSource =
              new[] { 
                     new { key="123", code = "MAT001", name = "Mặt hàng 1", lot="123", reciept = "HD001",date = "25/09/2014",
                             sortamount="90", realamount="90", difamount="10", unit="Thùng", description = "Mặt hàng 1"
                    },
                    new { key="1234", code = "MAT002", name = "Mặt hàng 2", lot="1234", reciept = "HD001",date = "25/09/2014",
                             sortamount="130", realamount="130", difamount="20", unit="Hộp", description = "Mặt hàng 2"
                    },
                };
            grdDataAccept.KeyFieldName = "key";
            grdDataAccept.DataBind();

            ASPxGridView1.DataSource =
              new[] { 
                     new { key="123", code = "MAT001", name = "Mặt hàng 1", lot="123", date="25-5-2014", reciept = "HD001",
                              recieptamount="100", unit="Thùng", description = "Mặt hàng 1", position = "Kho 1; Dãy B; Kệ 5"
                    },
                    new { key="1234", code = "MAT002", name = "Mặt hàng 2", lot="1234", date="25-7-2015",reciept = "HD001",
                              recieptamount="150", unit="Hộp", description = "Mặt hàng 2", position = "Kho 1; Dãy B; Kệ 6"
                    },
                };
            ASPxGridView1.KeyFieldName = "key";
            ASPxGridView1.DataBind();

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

        protected void grdData0_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            grdData0.CancelEdit();
            grdData0.JSProperties.Add("cpEdit", "edit");
        }

        protected void grdData0_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            grdData0.CancelEdit();
            grdData0.JSProperties.Add("cpEdit", "edit");
        }

        protected void grdData0_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdData0.CancelEdit();
        }

        protected void grdData_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            ASPxTextBox txt_realamount = grid.FindRowTemplateControl(e.VisibleIndex, "txt_realamount") as ASPxTextBox;
            txt_realamount.Focus();
        }

        protected void grdData_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            int idx = Int16.Parse(e.Parameters.ToString());

            //grdData.DataSource =
            //  new[] { 
            //         new { key="123", code = "MAT001", name = "Mặt hàng 1", lot="123", date="25-5-2014", reciept = "HD001",
            //                 recieptamount="100", realamount="90", difamount="10", unit="Thùng", description = "Mặt hàng 1"
            //        },
            //        new { key="1234", code = "MAT002", name = "Mặt hàng 2", lot="1234", date="25-7-2015",reciept = "HD001",
            //                 recieptamount="150", realamount="130", difamount="20", unit="Hộp", description = "Mặt hàng 2"
            //        },
            //    };
            //grdData.KeyFieldName = "key";
            //grdData.DataBind();
            
            ASPxTextBox txt_realamount = grid.FindRowCellTemplateControl(idx, grid.Columns["realamount"] as GridViewDataColumn,"txt_realamount") as ASPxTextBox;
            txt_realamount.Focus();
        }
    }
}