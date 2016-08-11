using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

namespace WebModule.Warehouse.UserControl
{
    public partial class uEntryDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grdData.DataSource =
              new[] { 
                       new { key="1236", verifyid = "MAT001", position = "Kho 1; Kệ 1", date="27-07-2013", recieptamount="100", realamount = "99", editamount = "99",
                             amountdiff= "0", note=""
                    },
                    new { key="12346", verifyid = "MAT002", position = "Kho 2; Kệ 1",date="10-07-2013", recieptamount="99", realamount = "100", editamount = "100",
                             amountdiff= "0", note="Thừa"
                    },
                };
            grdData.KeyFieldName = "key";
            grdData.DataBind();
        }
        protected void txtCode_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {

        }

        protected void txtName_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {

        }

        protected void cpLine_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }

        protected void grdData_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            ASPxTextBox txtAmount = grdData.FindRowCellTemplateControl(e.VisibleIndex,
                        grdData.Columns["editamount"] as GridViewDataColumn, "txtAmount") as ASPxTextBox;
            ASPxTextBox txtNote = grdData.FindRowCellTemplateControl(e.VisibleIndex,
                    grdData.Columns["note"] as GridViewDataColumn, "txtNote") as ASPxTextBox;
            ASPxLabel lblAmount = grdData.FindRowCellTemplateControl(e.VisibleIndex,
                    grdData.Columns["editamount"] as GridViewDataColumn, "lblAmount") as ASPxLabel;
            ASPxLabel lblNote = grdData.FindRowCellTemplateControl(e.VisibleIndex,
                    grdData.Columns["note"] as GridViewDataColumn, "lblNote") as ASPxLabel;
            if (e.VisibleIndex != 0)
            {
                txtAmount.Visible = false;
                txtNote.Visible = false;
                lblAmount.Visible = true;
                lblNote.Visible = true;
            }
            else {
                txtAmount.Visible = true;
                txtNote.Visible = true;
                lblAmount.Visible = false;
                lblNote.Visible = false;
            }
        }
    }
}