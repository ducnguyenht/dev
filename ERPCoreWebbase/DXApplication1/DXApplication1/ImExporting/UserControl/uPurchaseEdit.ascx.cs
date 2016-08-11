using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DevExpress.Web.ASPxGridView;

namespace WebModule.ImExporting.UserControl
{
    public partial class uPurchaseEdit : System.Web.UI.UserControl
    {
        object[] grv_SelectPaymentPlanSource = new[]{ new {Name = "L/C", Description = "L/C", RowStatus = "Đang Hoạt Động" },
            new { Name = "TT", Description = "TT", RowStatus = "Đang Hoạt Động" }};

        object[] grv_PaymentProcessSource = new[] { 
            new{seq = "01", id = 0, description = "", duration = ""},
            new{seq = "02", id = 1, description = "", duration = ""},
            new{seq = "03", id = 2, description = "", duration = ""},
            new{seq = "04", id = 3, description = "", duration = ""},
            new{seq = "05", id = 4, description = "", duration = ""},
            new{seq = "06", id = 5, description = "", duration = ""},
            new{seq = "07", id = 6, description = "", duration = ""}
        };
        
        private class datasample {
            public String No {get;set;}
            public String Code {get;set;}
            public String Name {get;set;}
            public String Unit {get;set;}
            public String Quantity {get;set;}
            public String Price {get;set;}
            public String AmountB {get;set;}
            public String Modify {get;set;}
            public String Currency {get;set;}
            public String Exchange {get;set;}
            public String AmountA {get;set;}
            public String Lot {get;set;}
            public String Time {get;set;}
            public String Note { get; set; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {            
            
            ArrayList data = new ArrayList();
            data.Add(new datasample() { 
                No = "1",
                Code = "TANATRIL",
                Name = "Tanatril",
                Unit = "Viên",
                Quantity = "10",
                Price = "230,000",
                AmountB = "2,300,000",
                Modify = "30,0000", 
                Currency = "VND",
                Exchange = "1",
                AmountA = "2,300,000",
                Lot = "LOT20/PXNJ",
                Time = "30" ,
                Note = "NAAN Solution"
            });

            data.Add(new datasample()
            {
                No = "2",
                Code = "KLACID",
                Name = "Klacid",
                Unit = "Viên",
                Quantity = "100",
                Price = "1,230,000",
                AmountB = "120,300,000",
                Modify = "30,0000",
                Currency = "VND",
                Exchange = "1",
                AmountA = "12,300,000",
                Lot = "LOT20/PXNJ",
                Time = "30",
                Note = "NAAN Solution"
            });

            grdProduct.DataSource = data;
            grdProduct.DataBind();

            grv_SelectPaymentPlan.DataSource = grv_SelectPaymentPlanSource;
            grv_SelectPaymentPlan.DataBind();
        }

        protected void grv_SelectPaymentPlan_OnHtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Detail) {
                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridView subgrid = grid.FindDetailRowTemplateControl(e.VisibleIndex, "grv_PaymentProcess") as ASPxGridView;
                subgrid.DataSource = grv_PaymentProcessSource;
                subgrid.DataBind();
            }
        }
    }
}