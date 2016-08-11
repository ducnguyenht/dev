using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Accounting
{
    public partial class GrossSplitSale : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_GROSSSPLITSALE_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
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

        protected void Page_Load(object sender, EventArgs e)
        {


            ASPxGridView2.DataSource = new[]{   new{c1 = 1,c2 = "MS001",c3 = "Ví dụ 1",c4 = "Ví dụ 1",c5 = 5,c6 = "01/07/2015",c7 = 500, grossamount=500,c8 = "5.000",c9 = "2.500.000",c10 ="5%" ,c11 = "Ví dụ"},
                                                        new{c1 = 2,c2 = "MS002",c3 = "Ví dụ 2",c4 = "Ví dụ 2",c5 = 5,c6 = "01/08/2014",c7 = 500, grossamount=400,c8 = "5.000",c9 = "2.500.000",c10 ="6%" ,c11 = "Ví dụ"},
                                                        new{c1 = 3,c2 = "MS003",c3 = "Ví dụ 3",c4 = "Ví dụ 3",c5 = 5,c6 = "01/09/2016",c7 = 500, grossamount=450,c8 = "5.000",c9 = "2.500.000",c10 ="8%" ,c11 = "Ví dụ"}};
            ASPxGridView2.KeyFieldName = "c1";
            ASPxGridView2.DataBind();

            ASPxGridView2_hanghoa.DataSource = new[]{   new{c1 = 1,c2 = "MS001",c3 = "Ví dụ 1",c4 = "Ví dụ 1",c5 = 5,c6 = "01/07/2015",c7 = 500, grossamount=500,c8 = "5.000",c9 = "2.500.000",c10 ="5%" ,c11 = "Ví dụ"},
                                                        new{c1 = 2,c2 = "MS002",c3 = "Ví dụ 2",c4 = "Ví dụ 2",c5 = 5,c6 = "01/08/2014",c7 = 500, grossamount=400,c8 = "5.000",c9 = "2.500.000",c10 ="6%" ,c11 = "Ví dụ"},
                                                        new{c1 = 3,c2 = "MS003",c3 = "Ví dụ 3",c4 = "Ví dụ 3",c5 = 5,c6 = "01/09/2016",c7 = 500, grossamount=450,c8 = "5.000",c9 = "2.500.000",c10 ="8%" ,c11 = "Ví dụ"}};
            ASPxGridView2_hanghoa.KeyFieldName = "c1";
            ASPxGridView2_hanghoa.DataBind();

            ASPxGridView3_dichvu.DataSource = new[]{    new{gia = "60.000"      ,c1 = 1,c2 = 1,c3 = "Ví dụ 1",c4 = "Ví dụ 1",c5 = 5,c6 = "300.000",c7 = "Ví dụ 1",ck ="5%"},
                                                        new{gia = "5.000.000"   ,c1 = 2,c2 = 2,c3 = "Ví dụ 2",c4 = "Ví dụ 2",c5 = 5,c6 = "25.000.000",c7 = "Ví dụ 2",ck ="7%"},
                                                        new{gia = "2.000.000"   ,c1 = 3,c2 = 3,c3 = "Ví dụ 3",c4 = "Ví dụ 3",c5 = 5,c6 = "10.000.000",c7 = "Ví dụ 3",ck ="6%"}};
            ASPxGridView3_dichvu.KeyFieldName = "c1";
            ASPxGridView3_dichvu.DataBind();

            ASPxGridView_khuyenmai.DataSource =
              new[] { 
                    new { TenQuaTang = "Phiếu Giảm Giá", GiaTri = 15000, MoTa = "NAAN Solution"               
                    },
                    new {  TenQuaTang = "Gấu Bông",GiaTri = 15000, MoTa = "NAAN Solution"   
                    },
                };
            ASPxGridView_khuyenmai.DataBind();

            grdData0.DataSource =
              new[] { 
                     new { key="123", code = "ML0001", supplier = "Khách hàng 1", date = "25-07-2013", order="1",
                             status="Dang dở", sum="1.000.000"
                    },
                    new { key="1234", code = "ML0002", supplier = "Khách hàng 2", date = "28-07-2013", order="2",
                             status="Gốc", sum="1.500.000"
                    },
                };
            grdData0.KeyFieldName = "key";
            grdData0.DataBind();

            grdData1.DataSource =
             new[] { 
                     new { key="123", code = "ML0001", supplier = "Khách hàng 1", date = "25-07-2013", order="1",
                             status="Dang dở", sum="1.000.000"
                    },
                    new { key="1234", code = "ML0002", supplier = "Khách hàng 2", date = "28-07-2013", order="2",
                             status="Gốc", sum="1.500.000"
                    },
                };
            grdData1.KeyFieldName = "key";
            grdData1.DataBind();


            grdData.DataSource =
              new[] { 
                     new { key="123", code = "MAT001", supplier = "Khách hàng 1", date="25-07-2013", reciept = "HD001",
                             recieptamount="100", realamount="90", difamount="10", unit="Thùng", description = "Mặt hàng 1"
                    },
                    new { key="1234", code = "MAT002", supplier = "Khách hàng 2", date="28-02-2013", reciept = "HD001",
                             recieptamount="150", realamount="130", difamount="20", unit="Hộp", description = "Mặt hàng 2"
                    },
                };
            grdData.KeyFieldName = "key";
            grdData.DataBind();



        }

       

        protected void ASPxGridView1_BeforePerformDataSelect(object sender, EventArgs e)
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

        protected void pc_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
        {

        }
    }
}