using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Inventory.Ledger;
using WebModule.Warehouse.Report;
using NAS.DAL.Accounting.Journal;
using DevExpress.XtraPrintingLinks;
using DevExpress.Web.ASPxEditors;
using DevExpress.Data.Filtering;
using NAS.DAL.Inventory.Lot; 
using NAS.DAL.Invoice;

namespace WebModule.Warehouse.UserControl
{
    public partial class uOutCommWarehouse : System.Web.UI.UserControl
    {
        Session session;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            BillItemXDS.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (hOutputCommId.Contains("id") && hOutputCommPrint.Contains("print"))
            {
                List<C02VT> lst = new List<C02VT>();
                if (hOutputCommPrint.Get("print").ToString().Equals("inventory"))
                {
                    XPQuery<SalesInvoiceInventoryTransaction> salesInvoiceInventoryTransaction = new XPQuery<SalesInvoiceInventoryTransaction>(session);
                    XPQuery<InventoryTransaction> inventoryTransaction = new XPQuery<InventoryTransaction>(session);
                    XPQuery<InventoryLedger> inventoryLedger = new XPQuery<InventoryLedger>(session);
                    XPQuery<BillItem> billItem = new XPQuery<BillItem>(session);


                    Guid billId =Guid.Parse(hOutputCommId.Get("id").ToString());

                    //var list = from sales in salesInvoiceInventoryTransaction.AsEnumerable()
                    //            join inTrans in inventoryTransaction on sales.InventoryTransactionId equals inTrans.InventoryTransactionId                               
                    //            join inLed in inventoryLedger on inTrans.InventoryTransactionId equals inLed.InventoryTransactionId.InventoryTransactionId
                    //            join bi in billItem on new { BillId = (Guid)sales.SalesInvoiceId.BillId, ItemUnitId= (Guid)inLed.ItemUnitId.ItemUnitId }
                    //                                                    equals new { BillId = (Guid)bi.BillId.BillId, ItemUnitId = (Guid)bi.ItemUnitId.ItemUnitId }
                    //            where sales.InventoryTransactionId == billId                             
                    //            select new C02VT
                    //            {
                    //                code = inTrans.Code,
                    //                createDate = inTrans.CreateDate,
                    //                itemCode = inLed.ItemUnitId.ItemId.Code,
                    //                itemName = inLed.ItemUnitId.ItemId.Name,
                    //                itemUnit = inLed.ItemUnitId.UnitId.Name,
                    //                quantity = inLed.Credit,
                    //                SaleInvoiceCode = sales.SalesInvoiceId.Code,                                       
                    //                InventoryName = inLed.InventoryId.Name,
                    //                InventoryAddress = inLed.InventoryId.Address                                    
                    //            };

                    //var list = //from bi in billItem.AsEnumerable()
                    //           from sales in salesInvoiceInventoryTransaction.AsEnumerable()
                    //           join bi in billItem.AsEnumerable() on new { sales.SalesInvoiceId.BillId } equals new { bi.BillId.BillId }
                    //           //from inLed in inventoryLedger.AsEnumerable() 
                    //           where sales.InventoryTransactionId == billId
                    //           //&& bi.ItemUnitId.ItemUnitId == inLed.ItemUnitId.ItemUnitId
                    //           //&& sales.InventoryTransactionId == inLed.InventoryTransactionId.InventoryTransactionId                               
                    //           select new C02VT
                    //           {
                    //               //code = inLed.InventoryTransactionId.Code,
                    //               //createDate = inLed.InventoryTransactionId.CreateDate,
                    //               //itemCode = inLed.ItemUnitId.ItemId.Code,
                    //               //itemName = inLed.ItemUnitId.ItemId.Name,
                    //               //itemUnit = inLed.ItemUnitId.UnitId.Name,
                    //               //quantity = inLed.Credit,
                    //               SaleInvoiceCode = ""//sales.SalesInvoiceId.Code,
                    //               //InventoryName = inLed.InventoryId.Name,
                    //               //InventoryAddress = inLed.InventoryId.Address
                    //           };
                    
                    var list = from bi in billItem.AsEnumerable()                               
                               where bi.BillId != null                                                        
                               join sales in salesInvoiceInventoryTransaction.AsEnumerable()                                
                               on bi.BillId.BillId equals sales.SalesInvoiceId.BillId
                               join inLed in inventoryLedger.AsEnumerable() on sales.InventoryTransactionId equals inLed.InventoryTransactionId.InventoryTransactionId                                
                               where sales.InventoryTransactionId == billId
                               && bi.ItemUnitId.ItemUnitId == inLed.ItemUnitId.ItemUnitId
                               select new C02VT
                               {
                                   code = inLed.InventoryTransactionId.Code,
                                   createDate = inLed.InventoryTransactionId.CreateDate,
                                   itemCode = inLed.ItemUnitId.ItemId.Code,
                                   itemName = inLed.ItemUnitId.ItemId.Name,
                                   itemUnit = inLed.ItemUnitId.UnitId.Name,
                                   quantity = bi.Quantity,
                                   SaleInvoiceCode = sales.SalesInvoiceId.Code,
                                   InventoryName = inLed.InventoryId.Name,
                                   InventoryAddress = inLed.InventoryId.Address
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
                    XPQuery<SalesInvoiceInventoryTransaction> salesInvoiceInventoryTransaction = new XPQuery<SalesInvoiceInventoryTransaction>(session);
                    XPQuery<InventoryTransaction> inventoryTransaction = new XPQuery<InventoryTransaction>(session);
                    XPQuery<COGS> cogs = new XPQuery<COGS>(session);

                    XPQuery<COGS> sum = new XPQuery<COGS>(session);
                    var ntos = from s in sum.AsEnumerable()
                                where s.InventoryTransactionId.InventoryTransactionId == Guid.Parse(hOutputCommId.Get("id").ToString())
                                group s by s.InventoryTransactionId.InventoryTransactionId into g
                                select new
                                {
                                    total = g.Sum(x => x.COGSPrice * x.Credit)
                                };

                    var list = from sales in salesInvoiceInventoryTransaction.AsEnumerable()
                                join inTrans in inventoryTransaction.AsEnumerable() on sales.InventoryTransactionId equals inTrans.InventoryTransactionId
                                join co in cogs.AsEnumerable() on inTrans.InventoryTransactionId equals co.InventoryTransactionId.InventoryTransactionId
                                where sales.InventoryTransactionId == Guid.Parse(hOutputCommId.Get("id").ToString())
                                select new C02VT
                                {
                                    code = inTrans.Code,
                                    createDate = inTrans.CreateDate,
                                    itemCode = co.ItemUnitId.ItemId.Code,
                                    itemName = co.ItemUnitId.ItemId.Name,
                                    itemUnit = co.ItemUnitId.UnitId.Name,
                                    quantity = co.Credit,
                                    price = co.COGSPrice,
                                    amount = co.Credit * co.COGSPrice,
                                    amountByString = Utility.Accounting.NumberToString(ntos.ToList()[0].total),
                                    SaleInvoiceCode = sales.SalesInvoiceId.Code,
                                    InventoryName = co.InventoryId.Name,
                                    InventoryAddress = co.InventoryId.Address
                                };

                    try
                    {
                        lst = list.Cast<C02VT>().ToList();
                    }
                    catch
                    {
                    }
                }
                    
                _02_VT report = new _02_VT();
                    
                report.DataSource = lst;
                report.DataMember = "";

                if (!hOutputCommPrint.Get("print").ToString().Equals("inventory"))
                {
                    XPQuery<SalesInvoiceInventoryAccountingTransaction> purchaseInvoiceInventoryAccountingTransaction = new XPQuery<SalesInvoiceInventoryAccountingTransaction>(session);
                    XPQuery<NAS.DAL.Accounting.Journal.GeneralJournal> generalJournal = new XPQuery<NAS.DAL.Accounting.Journal.GeneralJournal>(session);

                    var listg = from p in purchaseInvoiceInventoryAccountingTransaction.AsEnumerable()
                                join g in generalJournal.AsEnumerable() on p.TransactionId equals g.TransactionId.TransactionId
                                where p.SalesInvoiceInventoryTransactionId.InventoryTransactionId == Guid.Parse(hOutputCommId.Get("id").ToString())
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

                rptOutputCommViewer.Report = report;
                rptOutputCommViewer.DataBind();
                    
                hOutputCommPrint.Remove("print");

                cpOutputCommReport.JSProperties.Add("cpShowForm", "form");
            }


            if (Session["BillId"] != null)
            {
                BillItemXDS.CriteriaParameters["BillId"].DefaultValue = Session["BillId"].ToString();
            }
            
        }
      
        protected void cpLine_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] para = e.Parameter.Split('|');
            switch (para[0])
            {
                case "view":
                    InventoryTransaction inventoryTransaction = session.GetObjectByKey<InventoryTransaction>(Guid.Parse(hOutputCommId.Get("id").ToString()));
                    if (inventoryTransaction != null)
                    {
                        txtOutputCommWarehouseCode.Text = inventoryTransaction.Code;
                        txtOutputCommWarehouseCreateDate.Value = inventoryTransaction.CreateDate;
                        txtOutputCommWarehouseDescription.Text = inventoryTransaction.Description;
                        
                        SalesInvoiceInventoryTransaction pp = session.GetObjectByKey<SalesInvoiceInventoryTransaction>(Guid.Parse(hOutputCommId.Get("id").ToString()));
                        if (pp != null)
                        {
                            BillItemXDS.CriteriaParameters["BillId"].DefaultValue = pp.SalesInvoiceId.BillId.ToString();
                            Session["BillId"] = pp.SalesInvoiceId.BillId.ToString();
                        }

                    }

                    cpLine.JSProperties.Add("cpShowFormPrint", "showprint");

                    break;

                default:
                    break;
            }
        }

        protected void cpOutputCommReport_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
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
            int editRowIndex = grdSalesInvoice.EditingRowVisibleIndex;
            if (editRowIndex >= 0)
            {
                editItemUnitId = Guid.Parse(grdSalesInvoice.GetRowValues(editRowIndex, "ItemUnitId!Key").ToString());
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

        protected void grdSalesInvoice_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {            
            BillItem billItem = session.GetObjectByKey<BillItem>(Guid.Parse(e.OldValues["BillItemId"].ToString()));
            if (billItem != null)
            {
                Lot lot = session.GetObjectByKey<Lot>(Guid.Parse(e.NewValues["LotId!Key"].ToString()));
                if (lot != null)
                {
                    billItem.LotId = lot;
                    billItem.Save();
                }
            }
        }


        protected string newCategoryName = string.Empty;
        protected void grdSalesInvoice_ParseValue(object sender, DevExpress.Web.Data.ASPxParseValueEventArgs e)
        {
            if (e.FieldName == "LotId!Key")
            {
                newCategoryName = e.Value.ToString();
                Guid categoryID = Guid.Empty;
                if (!Guid.TryParse(newCategoryName, out categoryID))
                {
                    e.Value = null;
                }
            }
        }

        protected void grdSalesInvoice_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "LotId!Key")
            {
                ASPxComboBox combo = e.Editor as ASPxComboBox;
                combo.ClientSideEvents.ValueChanged = "function(s,e){ " +                                                   
                                                           "var a = s.GetSelectedItem().GetColumnText('ExpireDate').substring(0,10).split('/');" +
                                                           "var d = new Date(a[2], a[1]-1, a[0]);" +                                                               
                                                           "grdSalesInvoice.GetEditor('LotId.ExpireDate').SetValue(d);" +                                                           
                                                       "}";
            }
        }

        protected void grdSalesInvoice_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {

        }

        protected void grdSalesInvoice_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

        }

    }
}