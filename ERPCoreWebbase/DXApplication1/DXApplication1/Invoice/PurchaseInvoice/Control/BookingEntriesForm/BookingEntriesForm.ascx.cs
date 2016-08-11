using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.GUI.Pattern;
using NAS.BO.Invoice;
using NAS.DAL.Invoice;
using NAS.BO.Accounting.Journal;
using NAS.ETLBO.System.Object;

namespace WebModule.Invoice.PurchaseInvoice.Control.BookingEntriesForm
{
    public partial class BookingEntriesForm : System.Web.UI.UserControl
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

                PurchaseInvoiceTransactionBO purchaseInvoiceTransactionBO = new PurchaseInvoiceTransactionBO();
                //Bind data to booking entries gridview
                var datasource = //session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(BillId).PurchaseInvoiceTransactions;
                    purchaseInvoiceTransactionBO.GetTransactions(session, BillId);
                gridviewBookingEntriesForm.SetDataSource(datasource);

                /*2014-01-15 ERP-1396 Khoa.Truong INS START*/
                //Bind data to voucher booking entries gridview
                datasource = purchaseInvoiceTransactionBO.GetVoucherTransactions(session, BillId);
                gridviewVoucherBookingEntriesForm.SetDataSource(datasource);

                //Bind data to inventory booking entries gridview
                datasource = purchaseInvoiceTransactionBO.GetInventoryTransactions(session, BillId);
                gridviewInventoryBookingEntriesForm.SetDataSource(datasource);
                /*2014-01-15 ERP-1396 Khoa.Truong INS END*/
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
                    /*2014-02-13 ERP-1417 Duc.Vo INS START*/
                    BusinessObjectBO BusinessObjectBO = new BusinessObjectBO();
                    int objectInventoryFinacialType = int.MinValue;
                    //int objectInventoryItemType = int.MinValue;
                    int objectInvoiceFinacialType = int.MinValue;
                    int objectVoucherItemType = int.MinValue;
                    /*2014-02-13 ERP-1417 Duc.Vo INS END*/
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
                            List<string> errorList = new List<string>();
                            bool canBookEntriesTemp;
                            bool canBookEntries = true;

                            canBookEntriesTemp = gridviewBookingEntriesForm.CanBookEntries(out temp);
                            canBookEntries = canBookEntries & canBookEntriesTemp;
                            errorList.AddRange(temp);

                            canBookEntriesTemp = gridviewVoucherBookingEntriesForm.CanBookEntries(out temp);
                            canBookEntries = canBookEntries & canBookEntriesTemp;
                            errorList.AddRange(temp);

                            canBookEntriesTemp = gridviewInventoryBookingEntriesForm.CanBookEntries(out temp);
                            canBookEntries = canBookEntries & canBookEntriesTemp;
                            errorList.AddRange(temp);

                            /*2014-02-13 ERP-1417 Duc.Vo INS START*/
                            if (bill is NAS.DAL.Invoice.PurchaseInvoice)
                            {
                                objectInventoryFinacialType = Utility.Constant.BusinessObjectType_InputInventoryCommandFinancialTransaction;
                                //objectInventoryItemType = Utility.Constant.BusinessObjectType_InputInventoryCommandItemTransaction;
                                objectInvoiceFinacialType = Utility.Constant.BusinessObjectType_PurcharseFinancialTransaction;
                                objectVoucherItemType = Utility.Constant.BusinessObjectType_PaymentVoucherTransaction;
                            }
                            else if (bill is NAS.DAL.Invoice.SalesInvoice)
                            {
                                objectInventoryFinacialType = Utility.Constant.BusinessObjectType_OutputInventoryCommandFinancialTransaction;
                                //objectInventoryItemType = Utility.Constant.BusinessObjectType_OutputInventoryCommandItemTransaction;
                                objectInvoiceFinacialType = Utility.Constant.BusinessObjectType_SalesFinancialTransaction;
                                objectVoucherItemType = Utility.Constant.BusinessObjectType_ReceiptVoucherTransaction;
                            }
                            /*2014-02-13 ERP-1417 Duc.Vo INS END*/

                            if (canBookEntries)
                            {
                                TransactionBOBase transactionBOBase = new TransactionBOBase();

                                bill.RowStatus = Utility.Constant.ROWSTATUS_BOOKED_ENTRY;
                                //Book entries
                                //var transactions = new XPCollection<Transaction>(uow,
                                //    gridviewBookingEntriesForm.GetDataSource());

                                var transactions1 = gridviewBookingEntriesForm.GetDataSource();
                                foreach (var transaction in transactions1)
                                {                                    
                                    transactionBOBase.BookEntry(uow, transaction.TransactionId);
                                }

                                var transactions2 = gridviewVoucherBookingEntriesForm.GetDataSource();
                                foreach (var transaction in transactions2)
                                {
                                    transactionBOBase.BookEntry(uow, transaction.TransactionId);
                                }

                                var transactions3 = gridviewInventoryBookingEntriesForm.GetDataSource();
                                foreach (var transaction in transactions3)
                                {
                                    transactionBOBase.BookEntry(uow, transaction.TransactionId);
                                }

                                /*2014-02-13 ERP-1417 Duc.Vo INS START*/
                                foreach (var transaction in transactions1)
                                {
                                    BusinessObjectBO.CreateBusinessObject(uow,
                                        objectInvoiceFinacialType,
                                        transaction.TransactionId,
                                        transaction.IssueDate);
                                }

                                foreach (var transaction in transactions2)
                                {
                                    BusinessObjectBO.CreateBusinessObject(uow,
                                        objectVoucherItemType,
                                        transaction.TransactionId,
                                        transaction.IssueDate);
                                }

                                foreach (var transaction in transactions3)
                                {
                                    BusinessObjectBO.CreateBusinessObject(uow,
                                        objectInventoryFinacialType,
                                        transaction.TransactionId,
                                        transaction.IssueDate);
                                }
                                /*2014-02-13 ERP-1417 Duc.Vo INS END*/
                            }
                            else
                            {
                                foreach (var message in errorList)
                                {
                                    messages += message + "\n";
                                }
                            }
                        }

                        uow.CommitChanges();

                        BindData();
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