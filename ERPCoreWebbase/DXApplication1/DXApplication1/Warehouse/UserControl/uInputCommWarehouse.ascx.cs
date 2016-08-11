using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using WebModule.Accounting.Report;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Inventory.Ledger;
using WebModule.Warehouse.Report;
using NAS.DAL.Invoice;
using DevExpress.XtraPrintingLinks;
using NAS.DAL.Accounting.Journal;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.Inventory.Lot;
using NAS.DAL.Nomenclature.Item;


namespace WebModule.Warehouse.UserControl
{
    public partial class uInputCommWarehouse : System.Web.UI.UserControl
    {
        Session session;
        
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            BillItemXDS.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (hInputCommPrint.Contains("print") && hInputCommId.Contains("id"))
                {
                    List<C01VT> lst = new List<C01VT>();

                    if (hInputCommPrint.Get("print").ToString().Equals("inventory"))
                    {

                        XPQuery<PurchaseInvoiceInventoryTransaction> purchaseInvoiceInventoryTransaction = new XPQuery<PurchaseInvoiceInventoryTransaction>(session);
                        XPQuery<InventoryTransaction> queryInventoryTransaction = new XPQuery<InventoryTransaction>(session);
                        XPQuery<BillItem> billItem = new XPQuery<BillItem>(session);
                        XPQuery<InventoryLedger> inventoryLedger = new XPQuery<InventoryLedger>(session);

                        //var list = from pp in purchaseInvoiceInventoryTransaction.AsEnumerable()
                        //           join qq in queryInventoryTransaction.AsEnumerable() on pp.InventoryTransactionId equals qq.InventoryTransactionId
                        //           join b in billItem.AsEnumerable() on pp.PurchaseInvoiceId.BillId equals b.BillId.BillId
                        //           //join i in inventoryLedger.AsEnumerable() on pp.InventoryTransactionId equals i.InventoryTransactionId.InventoryTransactionId
                        //           where pp.InventoryTransactionId == Guid.Parse(hInputCommId.Get("id").ToString())
                        //           select new C01VT
                        //           {
                        //               Code = qq.Code,
                        //               CreateDate = qq.CreateDate,
                        //               ItemCode = b.ItemUnitId.ItemId.Code,
                        //               ItemName = b.ItemUnitId.ItemId.Name,
                        //               ItemUnit = b.ItemUnitId.UnitId.Name,
                        //               Quantity = b.Quantity,
                        //               PurchaseInvoiceCode = pp.PurchaseInvoiceId.Code,
                        //               Price = 0,
                        //               Amount = 0,
                        //               AmountByString = "",//Utility.Accounting.NumberToString(ntos != null ? ntos.ToList()[0].total : 0),
                        //               InventoryName = "Kho mặc định",
                        //               //InventoryAddress = i.InventoryId.Address,
                        //               PurchaseInvoiceDate = pp.PurchaseInvoiceId.IssuedDate,
                        //               ReceiverName = pp.PurchaseInvoiceId.TargetOrganizationId.Name
                        //           };

                        Guid billId = Guid.Parse(hInputCommId.Get("id").ToString());

                        var list = from bi in billItem.AsEnumerable()
                                   where bi.BillId != null
                                   join pp in purchaseInvoiceInventoryTransaction.AsEnumerable() on bi.BillId.BillId equals pp.PurchaseInvoiceId.BillId
                                   join qq in queryInventoryTransaction.AsEnumerable() on pp.InventoryTransactionId equals qq.InventoryTransactionId
                                   where pp.InventoryTransactionId == billId
                                   select new C01VT
                                   {
                                       Code = qq.Code,
                                       CreateDate = qq.CreateDate,
                                       ItemCode = bi.ItemUnitId.ItemId.Code,
                                       ItemName = bi.ItemUnitId.ItemId.Name,
                                       ItemUnit = bi.ItemUnitId.UnitId.Name,
                                       Quantity = bi.Quantity,
                                       PurchaseInvoiceCode = pp.PurchaseInvoiceId.Code,
                                       Price = 0,
                                       Amount = 0,
                                       AmountByString = "",//Utility.Accounting.NumberToString(ntos != null ? ntos.ToList()[0].total : 0),
                                       InventoryName = "Kho mặc định",
                                       ////InventoryAddress = i.InventoryId.Address,
                                       PurchaseInvoiceDate = pp.PurchaseInvoiceId.IssuedDate,
                                       ReceiverName = pp.PurchaseInvoiceId.TargetOrganizationId.Name
                                   };
                        try
                        {
                            lst = list.ToList();
                        }
                        catch
                        {
                        }

                    }

                    else
                    {
                        XPQuery<PurchaseInvoiceInventoryTransaction> purchaseInvoiceInventoryTransaction = new XPQuery<PurchaseInvoiceInventoryTransaction>(session);
                        XPQuery<InventoryTransaction> queryInventoryTransaction = new XPQuery<InventoryTransaction>(session);
                        XPQuery<BillItem> billItem = new XPQuery<BillItem>(session);

                        XPQuery<BillItem> sum = new XPQuery<BillItem>(session);
                        var ntos = from s in sum.AsEnumerable()
                                   join pp in purchaseInvoiceInventoryTransaction.AsEnumerable() on s.BillId.BillId equals pp.PurchaseInvoiceId.BillId
                                   where pp.InventoryTransactionId == Guid.Parse(hInputCommId.Get("id").ToString())
                                   group s by s.BillId.BillId into g
                                   select new
                                   {
                                       total = g.Sum(x => x.Quantity * x.Price * (x.PromotionInPercentage > 0 ? 1 - x.PromotionInPercentage / 100 : 1))
                                   };

                        double total = 0;
                        try
                        {
                            total = ntos.ToList()[0].total;
                        }
                        catch
                        {
                        }

                        //var list = from pp in purchaseInvoiceInventoryTransaction.AsEnumerable()
                        //           join qq in queryInventoryTransaction.AsEnumerable() on pp.InventoryTransactionId equals qq.InventoryTransactionId
                        //           join b in billItem.AsEnumerable() on pp.PurchaseInvoiceId.BillId equals b.BillId.BillId
                        //           //join i in inventoryLedger.AsEnumerable() on pp.InventoryTransactionId equals i.InventoryTransactionId.InventoryTransactionId
                        //           where pp.InventoryTransactionId == Guid.Parse(hInputCommId.Get("id").ToString())
                        //           select new C01VT
                        //           {
                        //               Code = qq.Code,
                        //               CreateDate = qq.CreateDate,
                        //               ItemCode = b.ItemUnitId.ItemId.Code,
                        //               ItemName = b.ItemUnitId.ItemId.Name,
                        //               ItemUnit = b.ItemUnitId.UnitId.Name,
                        //               Quantity = b.Quantity,
                        //               PurchaseInvoiceCode = pp.PurchaseInvoiceId.Code,
                        //               Price = b.Price * (b.PromotionInPercentage > 0 ? 1 - b.PromotionInPercentage / 100 : 1),
                        //               Amount = b.Quantity * b.Price * (b.PromotionInPercentage > 0 ? 1 - b.PromotionInPercentage / 100 : 1),
                        //               AmountByString = Utility.Accounting.NumberToString(total),
                        //               InventoryName = "Kho mặc định",
                        //               //InventoryAddress = i.InventoryId.Address,
                        //               PurchaseInvoiceDate = pp.PurchaseInvoiceId.IssuedDate,
                        //               ReceiverName = pp.PurchaseInvoiceId.TargetOrganizationId.Name
                        //           };

                        //try
                        //{
                        //    lst = list.Cast<C01VT>().ToList().Distinct().ToList();
                        //}
                        //catch
                        //{
                        //}

                        Guid billId = Guid.Parse(hInputCommId.Get("id").ToString());

                        var list = from bi in billItem.AsEnumerable()
                                   where bi.BillId != null
                                   join pp in purchaseInvoiceInventoryTransaction.AsEnumerable() on bi.BillId.BillId equals pp.PurchaseInvoiceId.BillId
                                   join qq in queryInventoryTransaction.AsEnumerable() on pp.InventoryTransactionId equals qq.InventoryTransactionId
                                   where pp.InventoryTransactionId == billId
                                   select new C01VT
                                   {
                                       Code = qq.Code,
                                       CreateDate = qq.CreateDate,
                                       ItemCode = bi.ItemUnitId.ItemId.Code,
                                       ItemName = bi.ItemUnitId.ItemId.Name,
                                       ItemUnit = bi.ItemUnitId.UnitId.Name,
                                       Quantity = bi.Quantity,
                                       PurchaseInvoiceCode = pp.PurchaseInvoiceId.Code,
                                       Price = bi.Price * (bi.PromotionInPercentage > 0 ? 1 - bi.PromotionInPercentage / 100 : 1),
                                       Amount = bi.Quantity * bi.Price * (bi.PromotionInPercentage > 0 ? 1 - bi.PromotionInPercentage / 100 : 1),
                                       AmountByString = Utility.Accounting.NumberToString(total),
                                       InventoryName = "Kho mặc định",
                                       ////InventoryAddress = i.InventoryId.Address,
                                       PurchaseInvoiceDate = pp.PurchaseInvoiceId.IssuedDate,
                                       ReceiverName = pp.PurchaseInvoiceId.TargetOrganizationId.Name
                                   };
                        try
                        {
                            lst = list.ToList();
                        }
                        catch
                        {
                        }
                    }


                    _01_VT report = new _01_VT();

                    report.DataSource = lst;
                    report.DataMember = "";

                    if (!hInputCommPrint.Get("print").ToString().Equals("inventory"))
                    {
                        XPQuery<PurchaseInvoiceInventoryAccountingTransaction> purchaseInvoiceInventoryAccountingTransaction = new XPQuery<PurchaseInvoiceInventoryAccountingTransaction>(session);
                        XPQuery<NAS.DAL.Accounting.Journal.GeneralJournal> generalJournal = new XPQuery<NAS.DAL.Accounting.Journal.GeneralJournal>(session);

                        var listg = from p in purchaseInvoiceInventoryAccountingTransaction.AsEnumerable()
                                    join g in generalJournal.AsEnumerable() on p.TransactionId equals g.TransactionId.TransactionId
                                    where p.PurchaseInvoiceInventoryTransactionId.InventoryTransactionId == Guid.Parse(hInputCommId.Get("id").ToString())
                                    orderby g.Credit, g.Debit
                                    select new
                                    {
                                        Dc = g.Debit > g.Credit ? "Nợ :" : "Có :",
                                        Account = g.AccountId.Code,
                                        Amount = Math.Max(g.Debit, g.Credit)
                                    };

                        grdBooking.DataSource = listg.ToList();
                        grdBooking.DataBind();

                        report.pccData.PrintableComponent = new PrintableComponentLinkBase() { Component = gvDataExporter };

                    }


                    rptInpuCommViewer.Report = report;
                    rptInpuCommViewer.DataBind();

                    hInputCommPrint.Remove("print");

                    cpInpuCommReport.JSProperties.Add("cpShowForm", "form");
                }

                if (Session["BillId"] != null)
                {
                    BillItemXDS.CriteriaParameters["BillId"].DefaultValue = Session["BillId"].ToString();
                }
            }            
        }
    

        protected void cpInpuCommLine_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] p = e.Parameter.Split('|');

            switch (p[0])
            {                
                case "view":
                    if (!hInputCommId.Contains("id"))
                    {
                        return;
                    }                        

                    InventoryTransaction inventoryTransaction = session.GetObjectByKey<InventoryTransaction>(Guid.Parse(hInputCommId.Get("id").ToString()));

                    if (inventoryTransaction != null)
                    {                        
                        
                        txtInputCommWarehouseCode.Text = inventoryTransaction.Code;
                        txtInputCommWarehouseCreateDate.Value = inventoryTransaction.CreateDate;
                        txtInputCommWarehouseDescription.Text = inventoryTransaction.Description;

                        //CriteriaOperator filter = new BinaryOperator(se
                        PurchaseInvoiceInventoryTransaction pp = session.GetObjectByKey<PurchaseInvoiceInventoryTransaction>(Guid.Parse(hInputCommId.Get("id").ToString()));
                        if (pp != null)
                        {
                            BillItemXDS.CriteriaParameters["BillId"].DefaultValue = pp.PurchaseInvoiceId.BillId.ToString();
                            Session["BillId"] = pp.PurchaseInvoiceId.BillId.ToString();   
                        }
                    }

                    break;        
                default:
                    break;

            }
        }

        protected void cpInpuCommReport_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] p = e.Parameter.Split('|');

            switch (p[0])
            {
                //case "viewReport":           
                //    cpInpuCommReport.JSProperties.Add("cpShowReport", "report");
                //    break;

                default:
                    break;

            }
        }

        protected void grdInputCommLine_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (e.NewValues["LotId!Key"] == null)
            {
                Lot lot = new Lot(session);
                lot.LotId = Guid.NewGuid();
                lot.Code = newCategoryName;
                lot.ExpireDate = DateTime.Parse(e.NewValues["LotId.ExpireDate"].ToString());

                int editRowIndex = grdInputCommLine.EditingRowVisibleIndex;
                if (editRowIndex >= 0)
                {
                    //lot.ItemUnitId = session.GetObjectByKey<ItemUnit>(Guid.Parse(grdInputCommLine.GetRowValues(editRowIndex, "ItemUnitId!Key").ToString()));
                }

                lot.Save();

                e.NewValues["LotId!Key"] = lot.LotId;
            }
            else
            {
                Lot lot = session.GetObjectByKey<Lot>(Guid.Parse(e.OldValues["LotId!Key"].ToString()));
                if (lot != null)
                {
                    //lot.ItemUnitId = null;
                    lot.Save();
                }

                lot = session.GetObjectByKey<Lot>(Guid.Parse(e.NewValues["LotId!Key"].ToString()));
                if (lot != null)
                {
                    int editRowIndex = grdInputCommLine.EditingRowVisibleIndex;
                    if (editRowIndex >= 0)
                    {
                        //lot.ItemUnitId = session.GetObjectByKey<ItemUnit>(Guid.Parse(grdInputCommLine.GetRowValues(editRowIndex, "ItemUnitId!Key").ToString()));
                    }

                    lot.Save();
                }
            }
        }

        protected void grdInputCommLine_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            if (e.NewValues["LotId!Key"] == null)
            {
                e.RowError = "Chưa nhập số lô !";
                return;
            }            
        }
        
        protected void grdInputCommLine_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
        }

        protected void grdInputCommLine_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "LotId!Key")
            {
                ASPxComboBox combo = (ASPxComboBox)e.Editor;

                combo.ClientSideEvents.ValueChanged = "function(s,e){ " +
                                                           "var a = s.GetSelectedItem().GetColumnText('ExpireDate').substring(0,10).split('/');" +
                                                           "var d = new Date(a[2], a[1]-1, a[0]);" +
                                                           "grdInputCommLine.GetEditor('LotId.ExpireDate').SetValue(d);" +
                                                       "}";

                //combo.ItemsRequestedByFilterCondition += new ListEditItemsRequestedByFilterConditionEventHandler(cboLot_ItemsRequestedByFilterCondition);
                //combo.ItemRequestedByValue += new ListEditItemRequestedByValueEventHandler(cboLot_ItemRequestedByValue);
                
            }
        }

        protected void cboLot_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            Lot obj = session.GetObjectByKey<Lot>(e.Value);

            if (obj != null)
            {
                comboItemUnit.DataSource = new Lot[] { obj };
                comboItemUnit.DataBindItems();
            }
        }

        protected Guid editItemUnitId = Guid.Empty;
        protected void cboLot_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            int editRowIndex = grdInputCommLine.EditingRowVisibleIndex;
            if (editRowIndex >= 0)
            {
                editItemUnitId = Guid.Parse(grdInputCommLine.GetRowValues(editRowIndex, "ItemUnitId!Key").ToString());
            }

            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            XPCollection<Lot> collection = new XPCollection<Lot>(session);

            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = new GroupOperator(GroupOperatorType.Or, new CriteriaOperator[] {                                                                
                                    new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] { 
                                                                new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                                                                new BinaryOperator("RowStatus", -1, BinaryOperatorType.Greater)
                                                    }),
                                    new BinaryOperator("ItemUnitId.ItemUnitId", editItemUnitId, BinaryOperatorType.Equal)
                                                     
            });

            
            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();
        }

        protected void grdInputCommLine_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {

        }

        protected void grdInputCommLine_BeforePerformDataSelect(object sender, EventArgs e)
        {
            
        }

        protected void grdInputCommLine_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
        {
            
        }

        protected string newCategoryName = string.Empty;        

        protected void grdInputCommLine_ParseValue(object sender, DevExpress.Web.Data.ASPxParseValueEventArgs e)
        {
            if (e.FieldName == "LotId!Key")
            {
                if (e.Value != null)
                {
                    newCategoryName = e.Value.ToString();
                    Guid categoryID = Guid.Empty;
                    if (!Guid.TryParse(newCategoryName, out categoryID))
                    {
                        e.Value = null;
                    }
                }
            }            
        }
    }
}