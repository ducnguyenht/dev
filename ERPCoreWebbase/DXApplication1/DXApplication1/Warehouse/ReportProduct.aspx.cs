using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Web.ASPxTreeList;
using NAS.BO.Inventory.Jouranl;
using NAS.DAL.Nomenclature.Inventory;
using System.Data;
using DevExpress.Web.ASPxGridView;
using WebModule.Purchasing;
using NAS.BO.Inventory.Journal;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Inventory.Ledger;

namespace WebModule.Warehouse
{
    public partial class ReportProduct : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        //public XPCollection<InventoryJournal> InventoryJournals
        //{
        //    get {
        //        if (Session["ReportProduct_InventoryJournals"] == null)
        //            return null;
        //        return Session["ReportProduct_InventoryJournals"] as XPCollection<InventoryJournal>;
        //    }
            
        //    set {
        //        Session["ReportProduct_InventoryJournals"] = value;
        //    }
        //}

        public XPCollection<InventoryLedger> InventoryLedgers
        {
            get
            {
                if (Session["ReportProduct_InventoryLedgers"] == null)
                    return null;
                return Session["ReportProduct_InventoryLedgers"] as XPCollection<InventoryLedger>;
            }

            set
            {
                Session["ReportProduct_InventoryLedgers"] = value;
            }
        }

        public XPCollection<NAS.DAL.Nomenclature.Item.ItemUnit> ItemUnits
        {
            get
            {
                if (Session["ReportProduct_ItemUnits"] == null)
                    return null;
                return Session["ReportProduct_ItemUnits"] as XPCollection<NAS.DAL.Nomenclature.Item.ItemUnit>;
            }

            set
            {
                Session["ReportProduct_ItemUnits"] = value;
            }
        }

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_REPORTPRODUCT_ID;
            }
        }

        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_WAREHOUSE_GROUPID;
            }
        }

        Session session;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsInventory.Session = session;
            dsItemUnit.Session = session;
            treeInventory.DataBind();
            dsInventory.CriteriaParameters["OrganizationId"].DefaultValue = "7e46b424-e0f5-4583-97f7-32bd2ec97e7e";
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

            //grdBalanceOfItems.DataSource = InventoryJournals;
            //grdBalanceOfItems.DataBind();

            grdBalanceOfInventoryCart.DataSource = InventoryLedgers;
            grdBalanceOfInventoryCart.DataBind();
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
            if (inventory != null) {
                System.Diagnostics.Debug.WriteLine(String.Format("InventoryId: {0}, Name: {1}", inventory.InventoryId, inventory.Name));
                Session["InventorySelected"] = inventory.InventoryId.ToString();
                ItemUnits = InventoryTransactionBO.getItemUnitsInInventory(session, Guid.Parse(Session["InventorySelected"].ToString()));
                this.grdataproduct.DataSource = ItemUnits;
                this.grdataproduct.DataBind();
            }
        }

        protected void grdBalanceOfInventoryCart_OnHtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (e.DataColumn.Name == "IsOriginal")
            {
                if (bool.Parse(e.GetValue("IsOriginal").ToString()))
                    e.Cell.Text = "Cập nhật tương ứng";                
                else e.Cell.Text = "Cập nhật do phát sinh";
            }
        }   

        protected void loadBalanceInfo(Guid InventoryId, Guid ItemUnitId) {

                InventoryJournalBO bo = new InventoryJournalBO();
                //XPCollection<InventoryJournal> IJLst = bo.getTransactionInventoryJournals(session, InventoryId, ItemUnitId);
                XPCollection<InventoryLedger> ILLst = bo.getTransactionInventoryLedgers(session, InventoryId, ItemUnitId);
                //InventoryJournals = IJLst;
                InventoryLedgers = ILLst;
                //grdBalanceOfItems.DataSource = InventoryJournals;
                grdBalanceOfInventoryCart.DataSource = ILLst;
                //grdBalanceOfItems.DataBind();
                grdBalanceOfInventoryCart.DataBind();
        }

        //protected void pcBalanceItems_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        //{
        //    loadBalanceInfo(Guid.Parse(Session["InventorySelected"].ToString()), Guid.Parse(e.Parameter.ToString()));
        //}

        protected void cpReportProduct_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            loadBalanceInfo(Guid.Parse(Session["InventorySelected"].ToString()), Guid.Parse(e.Parameter.ToString()));
        }
    }
}