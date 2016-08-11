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
    public partial class OutputWarehousing : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_OUTPUTWAREHOUSE_ID;
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
                     new { key="123", code = "HDP001", name = "Mặt hàng 1", unit="thùng", lot = "123",date = "25/07/2014",
                             reciept = "HD0001", amount= "100"
                    },
                    new { key="1234", code = "HDP002", name = "Mặt hàng 2", unit="Hộp", lot = "1234",date = "25/07/2014",
                             reciept = "HD0001", amount="150"
                    },
                };
            grdData.KeyFieldName = "key";
            grdData.DataBind();

            grdData0.DataSource =
              new[] { 
                     new { key="123", code = "ML0001", name = "Nguyễn văn A", date = "25-07-2013", warehouse="Kho 1",
                             status="Đã xuất kho"
                    },
                    new { key="1234", code = "ML0002", name = "Nguyễn văn B", date = "25-07-2013", warehouse="Kho 2",
                             status="Chưa chưa kho"
                    },
                };
            grdData0.KeyFieldName = "key";
            grdData0.DataBind();

              ASPxGridView1.DataSource =
               new[] { 
                       new { key="123", code="MH001", name="Mặt hàng 1", unit = "Thùng", lot = "0123",date = "25/07/2014",
                             amount= "100", amountreal="90", amountdiff="10", codedepence="HD0001", description="Mặt hàng 1"
                    },
                    new { key="1234", code="MH002", name="Mặt hàng 2", unit = "Hộp", lot = "0123",date = "25/07/2014",
                             amount= "150", amountreal="150", amountdiff="0", codedepence="HD0001", description="Mặt hàng 2"
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
                     new { key="123", location="Kho k1,Khu K1.1, Học H1", amountinw="150", unit = "Thùng", lot = "0123",date = "25/07/2014",
                             realoutamount= "100", amountremain="50", codedepence="HDP001"
                    },
                    new { key="1234", location="Kho k1,Khu K1.1, Học H2", amountinw="100", unit = "Thùng", lot = "0123",date = "25/07/2014",
                             realoutamount= "0", amountremain="100", codedepence="HDP001"
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

        protected void grdData0_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            grdData0.CancelEdit();
            grdData0.JSProperties.Add("cpEdit", "edit");
        }

        protected void grdData0_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            grdData0.CancelEdit();
            grdData0.JSProperties.Add("cpEdit", "edit");
        }
    }
}