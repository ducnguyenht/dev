using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxFormLayout;

namespace DXApplication1.GUI
{
    public partial class LiabiPartners : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gridview_master.DataSource = new[]{   
                new{customername = "Huỳnh Quang Minh", firstdebt1 = "1.000.000.000", firstdebt2 = "", 
                    issue = "50.000.000", payment = "600.000.000",
                    lastdebt1 = "350.000.000", lastdebt2 = "", customerid = "NVVVP0014"},
                new{customername = "Lê Thị Anh Thư", firstdebt1 = "500.000.000", firstdebt2 = "", 
                    issue = "10.000.000", payment = "200.000.000",
                    lastdebt1 = "310.000.000", lastdebt2 = "",customerid = "NVVVP0015"},
                new{customername = "Lê Quang Tín", firstdebt1 = "860.000.000", firstdebt2 = "", 
                    issue = "5.000.000", payment = "700.000.000",
                    lastdebt1 = "165.000.000", lastdebt2 = "",customerid = "NVVVP0016"}
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
                        new{date = "01/05/2013", evidenceid = "BHD62857", description = "Xuất bán hàng",
                            firstdebt = "500.000.000", issue = "40.000.000", payment = "", lastdebt = "50.000.000"},
                        new{date = "02/05/2013", evidenceid = "BHD62857", description = "Nhận thanh toán",
                            firstdebt = "", issue = "", payment = "410.000.000", lastdebt = ""},
                        new{date = "18/05/2013", evidenceid = "BHD62858", description = "Xuất bán hàng",
                            firstdebt = "500.000.000", issue = "10.000.000", payment = "", lastdebt = "300.000.000"},
                        new{date = "20/05/2013", evidenceid = "BHD62858", description = "Nhận thanh toán",
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
                        new{date = "02/06/2013", evidenceid = "BHD62859", description = "Xuất bán hàng", 
                            firstdebt = "300.000.000", issue = "5.000.000", payment = "", lastdebt = "105.000.000"},
                        new{date = "05/06/2013", evidenceid = "BHD62859", description = "Nhận thanh toán", 
                            firstdebt = "300.000.000", issue = "", payment = "100.000.000", lastdebt = "105.000.000"},
                        new{date = "26/06/2013", evidenceid = "BHD62860", description = "Xuất bán hàng",
                            firstdebt = "200.000.000", issue = "5.000.000", payment = "", lastdebt = "295.000.000"},
                        new{date = "28/06/2013", evidenceid = "BHD62860", description = "Nhận thanh toán",
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
                        new{date = "01/07/2013", evidenceid = "BHD62861", description = "Xuất bán hàng",
                            firstdebt = "430.000.000", issue = "4.000.000", payment = "", lastdebt = "100.000.000"},
                        new{date = "10/07/2013", evidenceid = "BHD62861", description = "Nhận thanh toán",
                            firstdebt = "430.000.000", issue = "", payment = "334.000.000", lastdebt = "100.000.000"},
                        new{date = "10/07/2013", evidenceid = "BHD62862", description = "Xuất bán hàng",
                            firstdebt = "430.000.000", issue = "1.000.000", payment = "", lastdebt = "65.000.000"},
                        new{date = "10/07/2013", evidenceid = "BHD62862", description = "Nhận thanh toán",
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

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        public string AccessObjectId
        {
            get { return Constant.ACCESSOBJECT_SALES_DEBTCUST_ID; }
        }

        public string AccessObjectGroupId
        {
            get { return Constant.ACCESSOBJECT_SALES_GROUPID; }
        }
    }
}