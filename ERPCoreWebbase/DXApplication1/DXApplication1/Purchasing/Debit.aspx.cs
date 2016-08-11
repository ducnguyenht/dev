using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Utility;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxFormLayout;

namespace WebModule.Purchasing
{
    public partial class Debit : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_PURCHASE_PRODUCT_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_PURCHASE_GROUPID;
            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        //private class datasample
        //{
        //    public int ProductId { get; set; }
        //    public string Supplier { get; set; }
        //    public string DK { get; set; }
        //    public string PS { get; set; }
        //    public string TT { get; set; }
        //    public string CK { get; set; }
        //}

        //private class datasample0
        //{
        //    public string Date { get; set; }
        //    public string Supplier { get; set; }
        //    public string DK { get; set; }
        //    public string PS { get; set; }
        //    public string TT { get; set; }
        //    public string CK { get; set; }
        //}


        //protected void Page_Load(object sender, EventArgs e)
        //{
          

        //}

        //protected void grdData_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        //{

        //}

        //protected void grdData_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        //{

        //}

        //protected void grdData_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        //{

        //}

       

        //protected void grdData_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    switch (e.Parameters)
        //    {
        //        case "view":
        //            ArrayList data = new ArrayList();
        //            data.Add(new datasample() { ProductId = 1, Supplier = "Nhà cung cấp Mỹ Châu", DK = "200,000,000", PS = "1,400,000,000", TT = "100,000", CK = "100,000" });
        //            data.Add(new datasample() { ProductId = 2, Supplier = "Nhà cung cấp Ích Nhân", DK = "400,000,000", PS = "400,000,000", TT = "100,000", CK = "200,000,000" });

        //            grdData.DataSource = data;
        //            grdData.DataBind();

        //            break;
        //        default:
        //            break;
        //    }
        //}

       
        //protected void grdData_BeforePerformDataSelect(object sender, EventArgs e)
        //{
            
            
        //}

        //void detailView_Load(object sender, EventArgs e)
        //{
        //    ASPxGridView detailView = (ASPxGridView)sender;
        //    detailView.DataBind();
        //}

        //protected void grdData0_BeforePerformDataSelect(object sender, EventArgs e)
        //{
            
        //    ArrayList data = new ArrayList();
        //    data.Add(new datasample0() { Supplier = "Nhà cung cấp Mỹ Châu", Date = "01/08/2013", DK = "20,000,000", PS = "140,000,000", TT = "100,000", CK = "100,000,000" });
        //    data.Add(new datasample0() { Supplier = "Nhà cung cấp Ích Nhân", Date = "01/08/2013", DK = "90,000,000", PS = "40,000,000", TT = "1,000,000", CK = "2200,000,000" });

        //    ASPxGridView detailView = (ASPxGridView)sender;
        //    detailView.Load += new EventHandler(detailView_Load);

        //    detailView.DataSource = data;            
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            gridview_master.DataSource = new[]{   
                new{customername = "Nhà cung cấp dược Quang Minh", firstdebt1 = "", firstdebt2 = "1.000.000.000", 
                    issue = "50.000.000", payment = "600.000.000",
                    lastdebt1 = "", lastdebt2 = "450.000.000", customerid = "NCCD0014"},
                new{customername = "Nhà cung cấp dược Anh Thư", firstdebt1 = "", firstdebt2 = "500.000.000", 
                    issue = "10.000.000", payment = "200.000.000",
                    lastdebt1 = "", lastdebt2 = "310.000.000",customerid = "NCCD0015"},
                new{customername = "Nhà cung cấp dược Quang Tín", firstdebt1 = "", firstdebt2 = "860.000.000", 
                    issue = "5.000.000", payment = "700.000.000",
                    lastdebt1 = "", lastdebt2 = "165.000.000",customerid = "NCCD0016"}
            };
            gridview_master.DataBind();
        }

        protected void gridview_master_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Detail)
            {

                ASPxGridView gridview_detail = gridview_master.FindDetailRowTemplateControl(e.VisibleIndex, "gridview_detail") as ASPxGridView;
                ASPxFormLayout form_top = gridview_master.FindDetailRowTemplateControl(e.VisibleIndex, "form_top") as ASPxFormLayout;
                ASPxFormLayout form_bottom = gridview_master.FindDetailRowTemplateControl(e.VisibleIndex, "form_bottom") as ASPxFormLayout;

                if (e.VisibleIndex == 0)
                {
                    gridview_detail.DataSource = new[] { 
                        new{date = "01/05/2013", evidenceid = "MHD62857", description = "Mua hàng",
                            firstdebt = "500.000.000", issue = "40.000.000", payment = "", lastdebt = "50.000.000"},
                        new{date = "02/05/2013", evidenceid = "MHD62857", description = "Thanh toán",
                            firstdebt = "", issue = "", payment = "410.000.000", lastdebt = ""},
                        new{date = "18/05/2013", evidenceid = "MHD62858", description = "Mua hàng",
                            firstdebt = "500.000.000", issue = "10.000.000", payment = "", lastdebt = "300.000.000"},
                        new{date = "20/05/2013", evidenceid = "MHD62858", description = "Thanh toán",
                            firstdebt = "", issue = "", payment = "190.000.000", lastdebt = ""},
                    };

                    form_top.DataSource = new { form_top_E1 = "1.000.000.000" };
                    form_bottom.DataSource = new { form_bottom_E1 = "50.000.000", form_bottom_E2 = "600.000.000", form_bottom_E3 = "350.000.000" };
                    form_top.DataBind();
                    form_bottom.DataBind();
                }

                if (e.VisibleIndex == 1)
                {
                    gridview_detail.DataSource = new[] { 
                        new{date = "02/06/2013", evidenceid = "MHD62859", description = "Mua hàng", 
                            firstdebt = "300.000.000", issue = "5.000.000", payment = "", lastdebt = "105.000.000"},
                        new{date = "05/06/2013", evidenceid = "MHD62859", description = "Thanh toán", 
                            firstdebt = "300.000.000", issue = "", payment = "100.000.000", lastdebt = "105.000.000"},
                        new{date = "26/06/2013", evidenceid = "MHD62860", description = "Mua hàng",
                            firstdebt = "200.000.000", issue = "5.000.000", payment = "", lastdebt = "295.000.000"},
                        new{date = "28/06/2013", evidenceid = "MHD62860", description = "Thanh toán",
                            firstdebt = "200.000.000", issue = "", payment = "100.000.000", lastdebt = "295.000.000"},
                    };

                    form_top.DataSource = new { form_top_E1 = "500.000.000" };
                    form_bottom.DataSource = new { form_bottom_E1 = "10.000.000", form_bottom_E2 = "200.000.000", form_bottom_E3 = "310.000.000" };
                    form_top.DataBind();
                    form_bottom.DataBind();
                }

                if (e.VisibleIndex == 2)
                {
                    gridview_detail.DataSource = new[] { 
                        new{date = "01/07/2013", evidenceid = "MHD62861", description = "Mua hàng",
                            firstdebt = "430.000.000", issue = "4.000.000", payment = "", lastdebt = "100.000.000"},
                        new{date = "10/07/2013", evidenceid = "MHD62861", description = "Thanh toán",
                            firstdebt = "430.000.000", issue = "", payment = "334.000.000", lastdebt = "100.000.000"},
                        new{date = "10/07/2013", evidenceid = "MHD62862", description = "Mua hàng",
                            firstdebt = "430.000.000", issue = "1.000.000", payment = "", lastdebt = "65.000.000"},
                        new{date = "10/07/2013", evidenceid = "MHD62862", description = "Thanh toán",
                            firstdebt = "430.000.000", issue = "", payment = "366.000.000", lastdebt = "65.000.000"},
                    };

                    form_top.DataSource = new { form_top_E1 = "860.000.000" };
                    form_bottom.DataSource = new { form_bottom_E1 = "5.000.000", form_bottom_E2 = "700.000.000", form_bottom_E3 = "165.000.000" };
                    form_top.DataBind();
                    form_bottom.DataBind();
                }

                gridview_detail.DataBind();
            }

        }
        
    }
}