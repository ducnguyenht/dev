using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using Utility;

namespace WebModule.Purchasing
{
    public partial class Warehousing : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_OUTPUTCOMM_ID;
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
        private void BindGrid()
        {

            grdData.DataSource =
               new[] { 
                     new { key="123", code = "HDP001", date = "25-07-2013", suppliername="Khách hàng 1", rowstatus = "Kích hoạt",
                             description = "Hóa đơn bán hàng"
                    },
                    new { key="1234", code = "HDP002", date = "24-07-2013", suppliername="Khách hàng 2", rowstatus = "Kích hoạt",
                             description = "Hóa đơn bán hàng"
                    },
                };
            grdData.KeyFieldName = "key";
            grdData.DataBind();

            grdDataAccept.DataSource =
               new[] { 
                     new { key="123", code="MH001", name="Mặt hàng 1", unit = "Thùng", lot = "0123",
                             amount= "500.000", description="Mặt hàng 1", codedepence="HDP001"
                    },
                    new { key="234", code="MH002", name="Mặt hàng 2", unit = "Hộp", lot = "0124",
                             amount= "1.000.000", description="Mặt hàng 2", codedepence="HDP002"
                    },
                };
            grdDataAccept.KeyFieldName = "key";
            grdDataAccept.DataBind();

            ASPxGridView1.DataSource =
                   new[] { 
                     new { key="123", code="MH001", name="Mặt hàng 1", unit = "Thùng", lot = "0123",
                             amount= "500.000", description="Mặt hàng 1", codedepence="HDP001"
                    },
                    new { key="1234", code="MH002", name="Mặt hàng 2", unit = "Hộp", lot = "0124",
                             amount= "1.000.000", description="Mặt hàng 2", codedepence="HDP002"
                    },
                };
            ASPxGridView1.KeyFieldName = "key";
            ASPxGridView1.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack || !Page.IsCallback)
            {
                BindGrid();
            }
            else
            {

            }
        }

        protected void grdData_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "new");

            Session["productMode"] = null;
        }

        protected void grdData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            BindGrid();

            Guid guid = new Guid(e.EditingKeyValue.ToString());


            //Session["productMode"] = vs;

            grdData.CancelEdit();
            grdData.JSProperties.Add("cpEdit", "edit");
        }

        protected void grdData_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

            e.Cancel = true;
            BindGrid();
        }

        protected void cpHeader_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            switch (e.Parameter)
            {
                case "refresh":
                    BindGrid();
                    break;
                default:
                    break;
            }
        }

        protected void grdDataDetail_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView detailView = (ASPxGridView)sender;
                detailView.Load += new EventHandler(detailView_Load);

                detailView.DataSource =
                   new[] { 
                     new { key="123", code="MH001", name="Mặt hàng 1", unit = "Thùng", lot = "0123",
                             amount= "500.000", description="Mặt hàng 1", codedepence="HDP001"
                    },
                    new { key="1234", code="MH002", name="Mặt hàng 2", unit = "Hộp", lot = "0124",
                             amount= "1.000.000", description="Mặt hàng 2", codedepence="HDP002"
                    },
                };
                detailView.KeyFieldName = "key";
            }
            catch (Exception) { }
        }
        void detailView_Load(object sender, EventArgs e)
        {
            ASPxGridView detailView = (ASPxGridView)sender;
            detailView.DataBind();
        }
    }
}