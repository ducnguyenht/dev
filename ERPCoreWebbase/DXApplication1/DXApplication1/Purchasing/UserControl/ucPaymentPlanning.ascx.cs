using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.BO.Accounting.Journal;

namespace WebModule.Purchasing.UserControl
{
    public partial class ucPaymentPlanning : System.Web.UI.UserControl
    {
        Session session;

        public Guid BillId { get; set; }

        public Guid CurrencyId { get; set; }

        //Guid billId = Guid.Empty;
        //Guid currencyId = Guid.Parse("23e455db-7409-419c-80e9-58830aa104db");
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void DataBind()
        {
            if (BillId != null)
            {
                PurchaseInvoiceTransactionBO purchaseInvoiceTransactionBO = new PurchaseInvoiceTransactionBO();
                GridPlanning.DataSource = purchaseInvoiceTransactionBO.GetPlanningTransactionsOfBill(session, BillId);
                GridPlanning.DataBind();
                PaymentVoucherTransactionBO paymentVoucherTransactionBO = new PaymentVoucherTransactionBO();
                GridActual.DataSource = paymentVoucherTransactionBO.GetActuallyCollectedOfBill(session, BillId);
                GridActual.KeyFieldName = "GeneralJournalId";
                GridActual.DataBind();
            }
        }

        protected void GridPlanning_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.Cancel = true;
            GridPlanning.CancelEdit();
            PurchaseInvoiceTransactionBO purchaseInvoiceTransactionBO = new PurchaseInvoiceTransactionBO();
            Guid transactionId = purchaseInvoiceTransactionBO.CreatePlanningTransaction(session, BillId, (DateTime)e.NewValues["IssueDate"], (string)e.NewValues["Code"], (double)e.NewValues["Amount"], (string)e.NewValues["Description"], CurrencyId);
            GridPlanning.DataSource = purchaseInvoiceTransactionBO.GetPlanningTransactionsOfBill(session, BillId);
            GridPlanning.DataBind();
        }

        protected void GridPlanning_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.Cancel = true;
            GridPlanning.CancelEdit();            
            PurchaseInvoiceTransactionBO purchaseInvoiceTransactionBO = new PurchaseInvoiceTransactionBO();
            purchaseInvoiceTransactionBO.UpdatePlanningTransaction(session, Guid.Parse(e.Keys["TransactionId"].ToString()), (DateTime)e.NewValues["IssueDate"], (string)e.NewValues["Code"], (double)e.NewValues["Amount"], (string)e.NewValues["Description"], CurrencyId);
            GridPlanning.DataSource = purchaseInvoiceTransactionBO.GetPlanningTransactionsOfBill(session, BillId);
            GridPlanning.DataBind();
        }

        protected void GridPlanning_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Cancel = true;
            PurchaseInvoiceTransactionBO purchaseInvoiceTransactionBO = new PurchaseInvoiceTransactionBO();
            purchaseInvoiceTransactionBO.DeletePlanningTransaction(session, Guid.Parse(e.Keys["TransactionId"].ToString()));
            GridPlanning.DataSource = purchaseInvoiceTransactionBO.GetPlanningTransactionsOfBill(session, BillId);
            GridPlanning.DataBind();
        }
    }
}