using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Nomenclature.Item;
using NAS.BO.Nomenclature.Items;
using System.ComponentModel;
using NAS.DAL.Inventory.Ledger;
using NAS.BO.Inventory.Ledger;

namespace WebModule.Invoice.Control.TradingItemExtraInformation
{
    public partial class TradingItemExtraInformation : System.Web.UI.UserControl
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

        [Browsable(false)]
        public Guid ItemId
        {
            get
            {
                if (ViewState["TradingItemExtraInformation_ItemId"] == null)
                    return Guid.Empty;
                return (Guid)ViewState["TradingItemExtraInformation_ItemId"];
            }
            set
            {
                ViewState["TradingItemExtraInformation_ItemId"] = value;
            }
        }

        [Browsable(false)]
        public Guid UnitId
        {
            get
            {
                if (ViewState["TradingItemExtraInformation_UnitId"] == null)
                    return Guid.Empty;
                return (Guid)ViewState["TradingItemExtraInformation_UnitId"];
            }
            set
            {
                ViewState["TradingItemExtraInformation_UnitId"] = value;
            }
        }

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            InitClientScript();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        private void InitClientScript()
        {
            panelTradingItemExtraInformation.ClientInstanceName =
                String.Format("{0}_panelTradingItemExtraInformation", _ClientInstanceName);

            panelTradingItemExtraInformation.ClientSideEvents.EndCallback =
                String.Format("function(s, e) {{ {0}.RePerformCallback(); }}", _ClientInstanceName);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DisplayInfo(ItemId, UnitId);
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            if (url.Contains("Invoice/SalesInvoice/GUI/MaterialSalesInvoiceListingForm.aspx") || url.Contains("Invoice/PurchaseInvoice/GUI/MaterialPurchaseInvoiceListingForm.aspx"))
            {
                formlayoutTradingItemExtraInformation.Items[0].Caption = "Thông tin chi tiết nguyên vật liệu";
            }
            else if (url.Contains("Invoice/SalesInvoice/GUI/ToolSalesInvoiceListingForm.aspx") || url.Contains("Invoice/PurchaseInvoice/GUI/ToolPurchaseInvoiceListingForm.aspx"))
            {
                formlayoutTradingItemExtraInformation.Items[0].Caption = "Thông tin chi tiết công cụ, dụng cụ";
            }
            else if (url.Contains("Invoice/SalesInvoice/GUI/FixedAssetsSalesInvoiceListingForm.aspx") || url.Contains("Invoice/PurchaseInvoice/GUI/FixedAssetsPurchaseInvoiceListingForm.aspx"))
            {
                formlayoutTradingItemExtraInformation.Items[0].Caption = "Thông tin chi tiết tài sản cố định";
            }
        }

        protected void panelTradingItemExtraInformation_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            try
            {
                string[] args = e.Parameter.Split('|');
                if (args == null || args.Length == 0)
                    throw new Exception("Invalid parameters");
                ItemId = Guid.Parse(args[0]);
                if (args.Length == 2)
                    UnitId = Guid.Parse(args[1]);
                else
                    UnitId = Guid.Empty;
                DisplayInfo(ItemId, UnitId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void DisplayInfo(Guid itemId, Guid unitId)
        {
            Item item = null;
            NAS.DAL.Nomenclature.Item.Unit unit = null;

            item = session.GetObjectByKey<Item>(itemId);
            if (item != null)
            {
                lblItemCode.Text = item.Code;
                lblItemName.Text = item.Name;
                lblManufacturer.Text = item.ManufacturerOrgId.Name;

                //Get ItemTax
                ItemBO itemBO = new ItemBO();
                ItemTax itemTax = itemBO.GetCurrentVATOfItem(session, itemId);
                if (itemTax == null)
                    lblTax.Text = "N/A";
                else
                    lblTax.Text = String.Format("{0}%", itemTax.TaxId.Percentage);
            }

            unit = session.GetObjectByKey<NAS.DAL.Nomenclature.Item.Unit>(unitId);
            if (unit != null)
            {
                lblUnit.Text = unit.Code;
                if (item == null)
                    return;
                //Get ItemUnit
                ItemUnit itemUnit = item.ItemUnits.Where(r => r.UnitId.UnitId == unit.UnitId).FirstOrDefault();
                InventoryLedgerBO inventoryLedgerBO = new InventoryLedgerBO();
                double balance = inventoryLedgerBO.GetItemUnitBalance(session, itemUnit.ItemUnitId);
                lblBalance.Text = String.Format("{0:N0}", balance);
                //lblConvertedBalance.Text = balance.
            }
        }
    }
}