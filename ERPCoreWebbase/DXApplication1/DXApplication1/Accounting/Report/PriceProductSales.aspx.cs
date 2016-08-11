using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using NAS.DAL.Inventory.Ledger;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Inventory;
using DevExpress.Web.ASPxGridView;
using NAS.DAL;
using NAS.BO.Inventory.Jouranl;
using DevExpress.Web.ASPxTreeList;
using NAS.BO.Inventory.Journal;
using NAS.DAL.Inventory.Journal;

namespace WebModule.Accounting.Report
{
    public partial class PriceProductSales : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_REPORTPRODUCTCOGS_ID;
            }
        }

        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_GROUPID;
            }
        }

        public XPCollection<COGS> COGSData
        {
            get
            {
                if (Session["PriceProductSales_COGSData"] == null)
                    return null;
                return Session["PriceProductSales_COGSData"] as XPCollection<COGS>;
            }

            set
            {
                Session["PriceProductSales_COGSData"] = value;
            }
        }

        public XPCollection<NAS.DAL.Nomenclature.Item.ItemUnit> ItemUnits
        {
            get
            {
                if (Session["Report_ItemUnits"] == null)
                    return null;
                return Session["Report_ItemUnits"] as XPCollection<NAS.DAL.Nomenclature.Item.ItemUnit>;
            }

            set
            {
                Session["Report_ItemUnits"] = value;
            }
        }

        Session session;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsInventory.Session = session;
            dsItemUnit.Session = session;
            treeInventory.DataBind();
            if (!IsPostBack)
            {
                Session["InventorySelected"] = "FA31071D-6010-4788-83B9-9F0CE0C90C5F"; //Khoa mặc định
                string defaultKey = Guid.Parse(Session["InventorySelected"].ToString()).ToString().Replace("-", string.Empty);
                TreeListNode node = treeInventory.FindNodeByKeyValue(defaultKey);
                if (node != null)
                    node.Focus();
                ItemUnits = InventoryTransactionBO.getItemUnitsInInventory(session, Guid.Parse(Session["InventorySelected"].ToString()));
            }
            System.Diagnostics.Debug.WriteLine(String.Format("Pageload InventoryId: {0}", Session["InventorySelected"].ToString()));
            this.grdataproduct.DataSource = ItemUnits;
            this.grdataproduct.DataBind();

            grdBalanceOfItems.DataSource = COGSData;
            grdBalanceOfItems.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void treeInventory_SelectionChanged(object sender, EventArgs e)
        {
            ASPxTreeList treeList = sender as ASPxTreeList;
            Session["InventorySelected"] = treeList.GetSelectedNodes()[0].Key;

        }

        protected object GetMasterRowKeyValue(ASPxTreeList treeList)
        {
            GridViewBaseRowTemplateContainer container = null;
            Control control = treeList;
            while (control.Parent != null)
            {
                container = control.Parent as GridViewBaseRowTemplateContainer;
                if (container != null) break;
                control = control.Parent;
            }
            return container.KeyValue;
        }

        protected void trlItemUnit_OnInit(object sender, EventArgs e)
        {
            ASPxTreeList treeList = sender as ASPxTreeList;
            object keyValue = GetMasterRowKeyValue(treeList);
            dsItemUnit.CriteriaParameters["ItemId"].DefaultValue = keyValue.ToString();
            treeList.DataBind();
        }

        protected void grdataproduct_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            Inventory inventory = (Inventory)treeInventory.FocusedNode.DataItem;
            if (inventory != null)
            {
                Session["InventorySelected"] = inventory.InventoryId.ToString();
                ItemUnits = InventoryTransactionBO.getItemUnitsInInventory(session, Guid.Parse(Session["InventorySelected"].ToString()));
                this.grdataproduct.DataSource = ItemUnits;
                this.grdataproduct.DataBind();
            }
        }

        protected void loadBalanceInfo(Guid InventoryId, Guid ItemUnitId)
        {
            COGSData = null;
            InventoryJournalBO bo = new InventoryJournalBO();
            XPCollection<COGS> InventoryJournals = bo.getCOGS(
                session,
                InventoryId,
                ItemUnitId);

            COGSData = bo.getCOGS(session, Guid.Parse(Session["InventorySelected"].ToString()), ItemUnitId);
            grdBalanceOfItems.DataSource = COGSData;
            grdBalanceOfItems.DataBind();
        }

        protected void grdBalanceOfItems_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            loadBalanceInfo(Guid.Parse(Session["InventorySelected"].ToString()), 
                             Guid.Parse(e.Parameters.ToString())
                           );
        }

        protected void grdBalanceOfItems_OnHtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (e.DataColumn.Name == "IsOriginal")
            {
                if (bool.Parse(e.GetValue("IsOriginal").ToString()))
                    e.Cell.Text = "Cập nhật tương ứng";
                else e.Cell.Text = "Cập nhật do phát sinh";
            }
        }

        protected void grdataproduct_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (e.RowType == GridViewRowType.Data){
            }
        }   
    }
}