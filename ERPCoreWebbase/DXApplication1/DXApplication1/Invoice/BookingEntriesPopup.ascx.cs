using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.BO.Accounting.Journal;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Invoice;
using NAS.GUI.Pattern;
using NAS.DAL.Accounting.Journal;
using NAS.BO.Invoice;

namespace WebModule.Invoice
{
    public partial class BookingEntriesPopup : System.Web.UI.UserControl
    {
        public string ClientInstanceName { get; set; }

        public string _ClientInstanceName
        {
            get
            {
                if (ClientInstanceName == null || ClientInstanceName.Trim().Length == 0)
                    return ClientID;
                return ClientInstanceName;
            }
        }

        public string Closing { get; set; }

        private Session session;
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
            if (ViewStateControlId == null)
            {
                GenerateViewStateControlId();
                BillId = Guid.Empty;
            }
            BindData();
        }

        #region State Pattern
        private void GenerateViewStateControlId()
        {
            ViewStateControlId = Guid.NewGuid().ToString().Replace("-", "");
        }

        private string ViewStateControlId
        {
            get
            {
                return (string)ViewState["ViewStateControlId"];
            }
            set
            {
                ViewState["ViewStateControlId"] = value;
            }
        }

        private Context GUIContext
        {
            get { return (NAS.GUI.Pattern.Context)Session["GUIContext_" + ViewStateControlId]; }
            set { Session["GUIContext_" + ViewStateControlId] = value; }
        }
        #endregion

        public Guid BillId
        {
            get
            {
                return (Guid)Session["BillId_" + ViewStateControlId];
            }
            set
            {
                Session["BillId_" + ViewStateControlId] = value;
            }
        }

        private void BindData()
        {
            if (BillId != null && !BillId.Equals(Guid.Empty))
            {
                PurchaseInvoiceBO purchaseInvoiceBO = new PurchaseInvoiceBO();
                //Bind data to purchase invoice information
                Bill bill = purchaseInvoiceBO.GetBillById(session, BillId);
                lblCode.Text = bill.Code;
                lblIssuedDate.Text = bill.IssuedDate.ToString();
                lblSumOfPromotion.Text = 
                    bill.SumOfPromotion != 0 ? String.Format("{0:#,###}", bill.SumOfPromotion) : "0";
                lblSumOfTax.Text = bill.SumOfTax != 0 ? String.Format("{0:#,###}", bill.SumOfTax) : "0";
                lblSumOfTotalPrice.Text = bill.SumOfItemPrice != 0 ? String.Format("{0:#,###}", bill.SumOfItemPrice) : "0";
                lblTotal.Text = bill.Total != 0 ? String.Format("{0:#,###}", bill.Total) : "0";
                lblOrganization.Text = String.Format("{0} - {1}",
                    bill.SourceOrganizationId.Code,
                    bill.SourceOrganizationId.Name);
                if (bill.RowStatus.Equals(Utility.Constant.ROWSTATUS_BOOKED_ENTRY))
                {
                    lblBookingStatus.Text = "Đã ghi sổ";
                }
                else
                {
                    lblBookingStatus.Text = "Chưa ghi sổ";
                }

                //Bind data to booking entries gridview
                PurchaseInvoiceTransactionBO purchaseInvoiceTransactionBO = new PurchaseInvoiceTransactionBO();
                var datasource = //session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(BillId).PurchaseInvoiceTransactions;
                    purchaseInvoiceTransactionBO.GetTransactionsAndRelatedTransactions(session, BillId);
                gridviewBookingEntriesForm.SetDataSource(datasource);
            }
        }

        protected void panelBookingEntriesPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] args = e.Parameter.Split('|');
            string command = args[0];
            NAS.BO.Invoice.PurchaseInvoiceBO purchaseInvoiceBO
                            = new NAS.BO.Invoice.PurchaseInvoiceBO();
            switch (command)
            {
                case "Show":
                    popupBookingEntriesForm.ShowOnPageLoad = true;
                    BillId = Guid.Parse(args[1]);
                    BindData();
                    popupBookingEntriesForm.HeaderText = String.Format("Hạch toán phiếu mua hàng - {0}",
                        purchaseInvoiceBO.GetBillById(session, BillId).Code);
                    break;
                case "Book":
                    popupBookingEntriesForm.ShowOnPageLoad = true;
                    string messages = null;

                    UnitOfWork uow = null;
                    try
                    {
                        uow = XpoHelper.GetNewUnitOfWork();
                        Bill bill = purchaseInvoiceBO.GetBillById(uow, BillId);

                        popupBookingEntriesForm.HeaderText = String.Format("Hạch toán phiếu mua hàng - {0}", bill.Code);

                        if (bill.RowStatus.Equals(Utility.Constant.ROWSTATUS_BOOKED_ENTRY))
                        {
                            messages = String.Format("Phiếu mua hàng '{0}' đã được ghi sổ", bill.Code);
                        }
                        else
                        {
                            IEnumerable<string> temp;
                            bool canBookEntries = gridviewBookingEntriesForm.CanBookEntries(out temp);
                            if (canBookEntries)
                            {
                                bill.RowStatus = Utility.Constant.ROWSTATUS_BOOKED_ENTRY;
                                //Book entries
                                //var transactions = new XPCollection<Transaction>(uow,
                                //    gridviewBookingEntriesForm.GetDataSource());

                                var transactions = gridviewBookingEntriesForm.GetDataSource();

                                foreach (var transaction in transactions)
                                {
                                    TransactionBOBase transactionBOBase = new TransactionBOBase();
                                    transactionBOBase.BookEntry(uow, transaction.TransactionId);
                                }
                            }
                            else
                            {
                                foreach (var message in temp)
                                {
                                    messages += message + "\n";
                                }
                            }
                        }

                        uow.CommitChanges();

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        if (uow != null) uow.Dispose();

                        if (messages != null)
                        {
                            panelBookingEntriesPopup.JSProperties["cpError"] = messages;
                        }
                    }
                    break;
                case "Cancel":
                    popupBookingEntriesForm.ShowOnPageLoad = false;
                    BillId = Guid.Empty;
                    panelBookingEntriesPopup.JSProperties["cpEvent"] = "Closing";
                    break;
                default:
                    break;
            }
        }
    }
}