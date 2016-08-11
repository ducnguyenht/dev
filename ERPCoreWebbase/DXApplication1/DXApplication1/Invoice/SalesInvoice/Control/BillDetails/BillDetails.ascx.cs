using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.DAL.Invoice;
using NAS.GUI.Pattern;
using WebModule.Invoice.SalesInvoice.Control.BillDetails.State;
using DevExpress.Xpo;
using NAS.DAL;
using WebModule.Invoice.Control.BillItemEditForm.Strategy;
using NAS.BO.Accounting;
using System.ComponentModel;
using WebModule.Invoice.Control.BillItemEditForm;

namespace WebModule.Invoice.SalesInvoice.Control.BillDetails
{
    public partial class BillDetails : System.Web.UI.UserControl
    {
        public bool IsInCallback
        {
            get
            {
                return panelBillDetails.IsCallback;
            }
        }

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

        [Category("Client-Side")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [AutoFormatDisable]
        [Themeable(false)]
        [MergableProperty(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public BillItemEditFormClientSideEvents BillItemClientSideEvents
        {
            get
            {
                return billItemEditForm.ClientSideEvents;
            }
        }

        public string BillItemsUpdated { set { billItemEditForm.ClientSideEvents.GridViewDataChanged = value; } }

        public BillTypeEnum BillType { get; set; }

        public Guid BillId { get; set; }

        public void InitState()
        {
            if (ViewStateControlId == null)
            {
                GenerateViewStateControlId();
                GUIContext = new Context();
            }
            GUIContext.State = new BillItemsState(this);
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
                GUIContext = new Context();
                BillType = BillTypeEnum.PRODUCT;
            }
            if (BillId != null)
            {
                LoadDataOnBillItemsState();
            }

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            if (url.Contains("Invoice/SalesInvoice/GUI/FixedAssetsSalesInvoiceListingForm.aspx"))
            {
                pageBillDetails.TabPages[0].Text = "Tài sản cố định";
            }
            else if (url.Contains("Invoice/SalesInvoice/GUI/ToolSalesInvoiceListingForm.aspx"))
            {
                pageBillDetails.TabPages[0].Text = "Công cụ, dụng cụ";
            }
            else if (url.Contains("Invoice/SalesInvoice/GUI/MaterialSalesInvoiceListingForm.aspx"))
            {
                pageBillDetails.TabPages[0].Text = "Nguyên vật liệu";
            }
            //if (BillId != null)
            //{
            //    LoadDataOnBillItemsState();

            //    deliverySchedule.BillId = BillId;
            //    paymentPlanning.BillId = BillId;
            //    paymentPlanning.CurrencyId = CurrencyBO.DefaultCurrency(session).CurrencyId;
            //}
        }

        private void LoadDataOnBillItemsState()
        {
            if (BillId != null)
            {
                billItemEditForm.BillId = BillId;
                billItemEditForm.SetBillItemEditFormStrategy(new SalesInvoiceBillItemEditFormStrategy());
                switch (BillType)
                {
                    case BillTypeEnum.PRODUCT:
                        billItemEditForm.SetTradingItemsComboBoxDataLoadingStrategy(
                            new ProductItemsComboBoxDataLoadingStrategy());
                        break;
                    case BillTypeEnum.SERVICE:
                        billItemEditForm.SetTradingItemsComboBoxDataLoadingStrategy(
                            new ServiceItemsComboBoxDataLoadingStrategy());
                        break;
                    case BillTypeEnum.MATERIAL:
                        billItemEditForm.SetTradingItemsComboBoxDataLoadingStrategy(
                            new MaterialItemsComboBoxDataLoadingStrategy());
                        break;
                    case BillTypeEnum.TOOL:
                        billItemEditForm.SetTradingItemsComboBoxDataLoadingStrategy(
                            new ToolItemsComboBoxDataLoadingStrategy());
                        break;
                    case BillTypeEnum.REAL_ESTATE:
                        billItemEditForm.SetTradingItemsComboBoxDataLoadingStrategy(
                            new FixedAssetsItemsComboBoxDataLoadingStrategy());
                        break;
                    default:
                        break;
                }
                billItemEditForm.DataBind();
            }
        }

        private void LoadDataOnDeliveryScheduleState()
        {
            if (BillId != null)
            {
                deliverySchedule.BillId = BillId;
                deliverySchedule.DataBind();
            }
        }

        private void LoadDataOnPaymentPlanningState()
        {
            if (BillId != null)
            {
                paymentPlanning.BillId = BillId;
                paymentPlanning.CurrencyId = CurrencyBO.DefaultCurrency(session).CurrencyId;
                paymentPlanning.DataBind();
            }
        }

        public void LoadDataBaseOnCurrentState()
        {
            if (!panelBillDetails.IsCallback)
            {
                if (GUIContext == null || GUIContext.State == null) return;
                //If is BillItems State...
                else if (GUIContext.State is BillItemsState)
                    LoadDataOnBillItemsState();
                //If is DeliverySchedule State...
                else if (GUIContext.State is DeliveryScheduleState)
                    LoadDataOnDeliveryScheduleState();
                //If is PaymentPlanning State...
                else if (GUIContext.State is PaymentPlanningState)
                    LoadDataOnPaymentPlanningState();
            }
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

        #region UpdateGUI
        public bool BillItemsState_UpdateGUI()
        {
            pageBillDetails.ActiveTabIndex = 0;
            return true;
        }
        public bool DeliveryScheduleState_UpdateGUI()
        {
            pageBillDetails.ActiveTabIndex = 1;
            return true;
        }
        public bool PaymentPlanningState_UpdateGUI()
        {
            pageBillDetails.ActiveTabIndex = 2;
            return true;
        }
        #endregion

        #region CRUD
        public bool BillItemsState_CRUD()
        {
            LoadDataOnBillItemsState();
            return true;
        }
        public bool DeliveryScheduleState_CRUD()
        {
            LoadDataOnDeliveryScheduleState();
            return true;
        }
        public bool PaymentPlanningState_CRUD()
        {
            LoadDataOnPaymentPlanningState();
            return true;
        }
        #endregion

        #region PreTransitionCRUD
        public bool BillItemsState_PreTransitionCRUD(string transition)
        {
            return true;
        }
        public bool DeliveryScheduleState_PreTransitionCRUD(string transition)
        {
            return true;
        }
        public bool PaymentPlanningState_PreTransitionCRUD(string transition)
        {
            return true;
        }
        #endregion
        #endregion

        protected void panelBillDetails_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string command = e.Parameter;
            GUIContext.Request(command, this);
        }
    }
}