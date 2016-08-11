using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Accounting.Journal;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.Inventory.Command;
using Utility;

namespace WebModule.Accounting.GeneralBookingEntries
{
    public partial class GeneralBookingEntriesForm : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_ACCOUNTINGENTRY_ID;
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
            }
            if (!IsPostBack)
            {
                ASPxListBox listBox = comboTransactionType.FindControl("listBox") as ASPxListBox;
                listBox.SelectAll();
            }
            BindDataToGridView();
        }

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

        private void BindDataToGridView()
        {
            ASPxListBox listBox = comboTransactionType.FindControl("listBox") as ASPxListBox;
            var selectedValues = listBox.SelectedValues;
            //XPCollection<Transaction> transactions = null;
            List<Transaction> transactionList = new List<Transaction>();
            foreach (var item in selectedValues)
            {
                XPCollection<Transaction> temp = null;
                CriteriaOperator criteria = null;
                if (item == null)
                    continue;
                int selectedValue = int.Parse(item.ToString());
                #region Get transactions
                switch (selectedValue)
                {
                    case 1: //ManualBookingTransaction
                        criteria = new BinaryOperator("RowStatus",
                                        Utility.Constant.ROWSTATUS_ACTIVE,
                                        BinaryOperatorType.GreaterOrEqual);
                        temp = new XPCollection<Transaction>(session,
                            new XPCollection<ManualBookingTransaction>(session, criteria));
                        break;
                    case 2: //PurchaseInvoiceTransaction
                        criteria = CriteriaOperator.And(
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE,
                                        BinaryOperatorType.GreaterOrEqual),
                            //2014-02-18 Khoa.Truong MOD START
                            //new ContainsOperator("GeneralJournals",
                            //    new BinaryOperator("JournalType", JounalTypeConstant.ACTUAL))
                            CriteriaOperator.Or(
                                new ContainsOperator("GeneralJournals",
                                    new BinaryOperator("JournalType", JounalTypeConstant.ACTUAL)
                                ),
                                new BinaryOperator(new AggregateOperand("GeneralJournals", Aggregate.Count), 0, BinaryOperatorType.Equal)
                            )
                            //2014-02-18 Khoa.Truong MOD END
                        );
                        temp = new XPCollection<Transaction>(session,
                            new XPCollection<PurchaseInvoiceTransaction>(session, criteria));
                        break;
                    case 3: //SaleInvoiceTransaction
                        criteria = CriteriaOperator.And(
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE,
                                        BinaryOperatorType.GreaterOrEqual),
                            //2014-02-18 Khoa.Truong MOD START
                            //new ContainsOperator("GeneralJournals",
                            //    new BinaryOperator("JournalType", JounalTypeConstant.ACTUAL))
                            CriteriaOperator.Or(
                                new ContainsOperator("GeneralJournals",
                                    new BinaryOperator("JournalType", JounalTypeConstant.ACTUAL)
                                ),
                                new BinaryOperator(new AggregateOperand("GeneralJournals", Aggregate.Count), 0, BinaryOperatorType.Equal)
                            )
                            //2014-02-18 Khoa.Truong MOD END
                        );
                        temp = new XPCollection<Transaction>(session,
                            new XPCollection<SaleInvoiceTransaction>(session, criteria));
                        break;
                    case 4: //ReceiptVouchesTransaction
                        criteria = new BinaryOperator("RowStatus",
                                        Utility.Constant.ROWSTATUS_ACTIVE,
                                        BinaryOperatorType.GreaterOrEqual);
                        temp = new XPCollection<Transaction>(session,
                            new XPCollection<ReceiptVouchesTransaction>(session, criteria));
                        break;
                    case 5: //PaymentVouchesTransaction
                        criteria = new BinaryOperator("RowStatus",
                                        Utility.Constant.ROWSTATUS_ACTIVE,
                                        BinaryOperatorType.GreaterOrEqual);
                        temp = new XPCollection<Transaction>(session,
                            new XPCollection<PaymentVouchesTransaction>(session, criteria));
                        break;
                    case 6: //Input InventoryCommandFinancialTransaction
                        criteria = CriteriaOperator.And(
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE,
                                        BinaryOperatorType.GreaterOrEqual),
                            new BinaryOperator("InventoryCommandId.CommandType", NAS.BO.Inventory.Command.INVENTORY_COMMAND_TYPE.IN)
                        );
                        temp = new XPCollection<Transaction>(session,
                            new XPCollection<InventoryCommandFinancialTransaction>(session, criteria));
                        break;
                    case 7: //Output InventoryCommandFinancialTransaction
                        criteria = CriteriaOperator.And(
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE,
                                        BinaryOperatorType.GreaterOrEqual),
                            new BinaryOperator("InventoryCommandId.CommandType", NAS.BO.Inventory.Command.INVENTORY_COMMAND_TYPE.OUT)
                        );
                        temp = new XPCollection<Transaction>(session,
                            new XPCollection<InventoryCommandFinancialTransaction>(session, criteria));
                        break;
                    case 8: //Moving InventoryCommandFinancialTransaction
                        criteria = CriteriaOperator.And(
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE,
                                        BinaryOperatorType.GreaterOrEqual),
                            new BinaryOperator("InventoryCommandId.CommandType", NAS.BO.Inventory.Command.INVENTORY_COMMAND_TYPE.MOVE)
                        );
                        temp = new XPCollection<Transaction>(session,
                            new XPCollection<InventoryCommandFinancialTransaction>(session, criteria));
                        break;
                    default:
                        break;
                }
                #endregion
                if (temp != null)
                {
                    transactionList.AddRange(temp);
                }
            }
            gridGeneralBookingEntries.SetDataSource(transactionList);
        }

        protected void cpnGeneralBookingEntries_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindDataToGridView();
        }
    }
}