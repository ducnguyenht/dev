using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.DAL.Invoice;
using NAS.DAL;
using DevExpress.Xpo;
using NAS.BO.Accounting.Journal;
using NAS.DAL.Accounting.Journal;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.Accounting.AccountChart;

namespace WebModule.Invoice.PurchaseInvoice.Control.BookingEntriesForm
{
    public partial class BookingEntriesForm_Backup : System.Web.UI.UserControl
    {
        string m_Code;
        DateTime m_IssuedDate;
        string m_SupplierName;
        double m_SumOfItemPrice;
        double m_SumOfTax;
        double m_SumOfPromotion;
        double m_Total;

        Session session;

        XPCollection<Transaction> m_ListTransaction;
       
        Bill m_Bill;

        SaleInvoiceTransactionBO saleInvoiceTransactionBO;
        PurchaseInvoiceTransactionBO purchaseInvoiceTransactionBO;

        void BindData()
        {
            if (m_Bill == null)
            {
                return;
            }

            lblCode.Text = m_Bill.Code;
            lblIssuedDate.Text = m_Bill.IssuedDate.ToString();
            lblSumOfItemPrice.Text = m_Bill.SumOfItemPrice.ToString("n0");
            lblSumOfPromotion.Text = m_Bill.SumOfPromotion.ToString("n0");
            lblSumOfTax.Text = m_Bill.SumOfTax.ToString("n0");
            lblSupplier.Text = m_Bill.SourceOrganizationId.Name;
            lblTotal.Text = m_Bill.Total.ToString("n0");

            GridViewBookingEntries1.SetDataSource(m_ListTransaction);            
            GridViewBookingEntries1.DataBind();
        }

        bool ValidateEntries()
        {
            foreach (Transaction transaction in m_ListTransaction)
            {
                double _Debit = 0;
                double _Credit = 0;

                foreach (GeneralJournal generalJournal in transaction.GeneralJournals.Where(g =>g.RowStatus >= Utility.Constant.ROWSTATUS_ACTIVE))
                {
                    if (generalJournal.AccountId.Name == DefaultAccountEnum.NAAN_DEFAULT.ToString())
                    {
                        cpBookingEntriesForm.JSProperties.Add("cpInvalidAccount", "Chưa chọn tài khoản hạch toán !");
                        return false;
                    }

                    _Debit += generalJournal.Debit;
                    _Credit += generalJournal.Credit;                    
                }

                if (_Debit != _Credit)
                {
                    cpBookingEntriesForm.JSProperties.Add("cpNotBalance", "Tổng nợ không bằng tổng có !");
                    return false;
                }
            }

            return true;
        }

        void BookingEntries()
        {
            foreach (Transaction transaction in m_ListTransaction)
            {
                transaction.RowStatus = Utility.Constant.ROWSTATUS_BOOKED_ENTRY;
                transaction.Save();
            }            
        }

        void Set_M_ListTransaction()
        {
            if (m_Bill != null)
            {
                saleInvoiceTransactionBO = new SaleInvoiceTransactionBO();
                purchaseInvoiceTransactionBO = new PurchaseInvoiceTransactionBO();

                //SalesInvoice _SalesInvoice = session.GetObjectByKey<SalesInvoice>(m_Bill.BillId);

                //if (_SalesInvoice == null)
                //{
                    NAS.DAL.Invoice.PurchaseInvoice _PurchaseInvoice = session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(m_Bill.BillId);
                    m_ListTransaction = purchaseInvoiceTransactionBO.GetTransactionsAndRelatedTransactions(session, m_Bill.BillId);
                //}
                //else
                //{
                //    m_ListTransaction = saleInvoiceTransactionBO.GetTransactionsAndRelatedTransactions(session, m_Bill.BillId);
                //}
            }

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                m_Bill = (Bill)Session["data"];

                //Set_M_ListTransaction();
                //BindData();
            }
            Set_M_ListTransaction();
            BindData();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void cpBookingEntriesForm_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            ASPxButton _button;

            string[] para = e.Parameter.Split('|');
            switch (para[0])
            {
                case "show":
                    m_Bill = session.GetObjectByKey<Bill>(Guid.Parse(para[1]));
                    Session["data"] = m_Bill;
                    

                    if (m_Bill != null)
                    {
                        Set_M_ListTransaction();
                        if (m_ListTransaction != null)
                        {
                            if (m_ListTransaction[0].RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                            {
                                _button = (ASPxButton)formBookingEntriesForm.FindControl("buttonBookingEntries");
                                _button.Enabled = false;
                            }
                        }
                    }

                    BindData();

                    break;
                case "booking":
                    if (!ValidateEntries())
                        return;
                    
                    BookingEntries();

                    _button = (ASPxButton)formBookingEntriesForm.FindControl("buttonBookingEntries");
                    _button.Enabled = false;

                    break;
                default:
                    break;
            }
        }
    }
}