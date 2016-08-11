using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using Utility;
using WebModule.Purchasing.UserControl;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Invoice;
using NAS.DAL;
using NAS.BO;
using NAS.BO.PurchaseInvoice;
using DevExpress.Web.ASPxCallbackPanel;
using NAS.DAL.Inventory.StockCart;
using NAS.DAL.Inventory.Operation;
using NAS.DAL.Inventory.Item;
using NAS.DAL.Nomenclature.Inventory;
using NAS.BO.Sale;
using NAS.DAL.Accounting.Journal;
using NAS.BO.Accounting.Journal;
using NAS.BO.System.ArtifactCode;
using NAS.GUI.Pattern;
using WebModule.Sales.State.OrderList;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Accounting.Tax;
using NAS.DAL.Accounting.AccountChart;
using NAS.DAL.Inventory.Lot;
using NAS.BO.Inventory.Command;

namespace ERPCore.Sales.UserControl
{

    public partial class uOrderListEdit : System.Web.UI.UserControl
    {
        Session session;

        static short ROW_DELETE = -1;
        static short ROW_NEW = 1;
        static short ROW_IMPORT = 2;
        static short ROW_BOOK = 3;
        static short ROW_NOT_DELETE = 5;

        static Boolean allowModify = true;
        
        static string Status;
        static int PositionItem;
        /////////////////////////////////////////////////////////////////////////////

        private Context GUIContext
        {
            get { return (NAS.GUI.Pattern.Context)Session["GUIContext_OrderListEdit"]; }
            set { Session["GUIContext_OrderListEdit"] = value; }
        }

         // UpdateGUI

        public bool OrderListLoading_UpdateGUI()
        {
            if (hPurchaseEditId.Contains("id"))
            {
                if (hPurchaseEditId.Get("id").ToString().Equals(""))
                {                 
                    Status = "new";
                    
                    grdPurchaseEditProduct.Columns["Thao tác"].Visible = true;
                    grdPurchaseEditService.Columns["Thao tác"].Visible = true;
                    grdPaymentSchedule.Columns["Thao tác"].Visible = true;                    
                    grdDeliveryBillItem.Columns["Thao tác"].Visible = true;

                    SaleInvoiceBO salesInvoiceBO = new SaleInvoiceBO();
                    Guid billId = salesInvoiceBO.insertEmptySaleInvoice(session);

                    //hPurchaseEditId.Set("id", billId);

                    Session["SaleBillId"] = null;
                    Session["SaleBillId"] = billId;

                    //hPurchaseEditId.Remove("id");

                    ReceiptVoucherTransactionBO receiptVoucherTransactionBO = new ReceiptVoucherTransactionBO();
                    XPCollection<GeneralJournal> collectGeneralJournal = receiptVoucherTransactionBO.GetActuallyCollectedOfBill(session, Guid.Parse(Session["SaleBillId"].ToString()));

                    grdPaymentScheduleActual.KeyFieldName = "GeneralJournalId";
                    grdPaymentScheduleActual.DataSource = collectGeneralJournal;
                    grdPaymentScheduleActual.DataBind();


                    InventoryCommandBO inventoryCommandBO = new InventoryCommandBO();
                    IEnumerable<InventoryJournal> collectInventoryJournal = inventoryCommandBO.GetActualInventoryJournalOfBill(session,
                        Guid.Parse(Session["SaleBillId"].ToString()),
                        NAS.DAL.CMS.ObjectDocument.DefaultObjectTypeCustomFieldEnum.INVENTORY_OUT_SALE_INVOICE);
                    grdDeliveryScheduleActual.KeyFieldName = "InventoryJournalId";
                    grdDeliveryScheduleActual.DataSource = collectInventoryJournal;
                    grdDeliveryScheduleActual.DataBind();

                    txtIssuedDate.Value = DateTime.Now;

                    txtProductTax.Value = 0;
                    txtProductTaxValue.Value = 0;
                    txtProductDiscountAmount.Value = 0;
                    txtProductDiscountSum.Value = 0;
                    txtProductTotal.Value = 0;

                    txtServiceTax.Value = 0;
                    txtServiceTaxValue.Value = 0;
                    txtServiceDiscountAmount.Value = 0;
                    txtServiceDiscountSum.Value = 0;
                    txtServiceTotal.Value = 0;

                    txtSumDiscount.Value = 0;
                    txtSumProduct.Value = 0;
                    txtSumService.Value = 0;
                    txtSumTax.Value = 0;

                    txtAmount.Value = 0;
                    txtPaid.Value = 0;
                    txtCredit.Value = 0;

                    grdPurchaseEditProduct.AddNewRow();
                    grdPurchaseEditService.AddNewRow();                    

                    cpLine.JSProperties.Add("cpEnable", "true");

                    artifactCodeRuleBO = new ArtifactCodeRuleBO();

                    txtCode.Focus();
                    txtCode.Text = artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.INVOICE_SALE);
                    
                }            
                else
                {
                    Status = "load";

                    bOutputInventoryCommand.EnableClientSideAPI = false;
                    bPayment.EnableClientSideAPI = false;

                    txtProductTax.Value = 0;
                    txtProductTaxValue.Value = 0;
                    txtProductDiscountAmount.Value = 0;
                    txtProductDiscountSum.Value = 0;
                    txtProductTotal.Value = 0;

                    txtServiceTax.Value = 0;
                    txtServiceTaxValue.Value = 0;
                    txtServiceDiscountAmount.Value = 0;
                    txtServiceDiscountSum.Value = 0;
                    txtServiceTotal.Value = 0;

                    txtAmount.Value = 0;
                    
                    Session["SaleBillId"] = null;
                    Session["SaleBillId"] = Guid.Parse(hPurchaseEditId.Get("id").ToString());
                    //hPurchaseEditId.Remove("id");

                    UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
                    SalesInvoice salesInvoice = uow.GetObjectByKey<SalesInvoice>(Guid.Parse(hPurchaseEditId.Get("id").ToString()));

                    if (salesInvoice == null)
                    {
                        cpLine.JSProperties.Add("cpRefresh", "resfresh");
                        return false;
                    }

                    txtCode.Text = salesInvoice.Code;
                    txtIssuedDate.Value = salesInvoice.IssuedDate;

                    if (salesInvoice.SourceOrganizationId != null)
                    {
                        cboSupplier.Value = salesInvoice.SourceOrganizationId.Code;
                    }
                    if (salesInvoice.TargetOrganizationId != null)
                    {
                        cboUser.Value = salesInvoice.TargetOrganizationId.Code;
                    }

                    // Product

                    CriteriaOperator filter = new BinaryOperator("PromotionTypeName", "PROMOTION_TYPE_PRODUCT", BinaryOperatorType.Equal);
                    PromotionType promotionType = session.FindObject<PromotionType>(filter);

                    foreach (NAS.DAL.Invoice.BillPromotion item in salesInvoice.BillPromotions)
                    {
                        if (item.PromotionTypeId.PromotionTypeId == promotionType.PromotionTypeId)
                        {
                            txtProductDiscountAmount.Value = item.PromotionInPercentage;
                            txtProductDiscountSum.Value = item.PromotionInNumber;
                        }
                    }

                    filter = CriteriaOperator.Parse("[BillId!Key] = ? And [ItemUnitId.ItemId.ItemCustomTypes][[ObjectTypeId.ObjectTypeId] = ?]",
                       Guid.Parse(Session["SaleBillId"].ToString()),
                       Guid.Parse("5817b239-e150-4c8e-a313-eaa8bd6944c4"));

                    XPCollection<BillItem> billItem = new XPCollection<BillItem>(session, filter);

                    if (billItem.Count > 0)
                    {
                        txtProductTax.Value = billItem[0].ItemUnitId.ItemId.VatPercentage;
                        txtProductTaxValue.Value = (billItem.Sum(p => p.Quantity * p.Price) - double.Parse(txtProductDiscountSum.Value.ToString())) *
                                                            (billItem[0].ItemUnitId.ItemId.VatPercentage / 100);
                    }


                    double total = 0;
                    if (billItem.Count > 0)
                    {
                        foreach (BillItem item in billItem)
                        {
                            total += double.Parse(item.TotalPrice.ToString());
                        }
                    }

                    txtProductTotal.Value = total - double.Parse(txtProductDiscountSum.Value.ToString()) + double.Parse(txtProductTaxValue.Value.ToString());

                    // Service

                    filter = new BinaryOperator("PromotionTypeName", "PROMOTION_TYPE_SERVICE", BinaryOperatorType.Equal);
                    promotionType = session.FindObject<PromotionType>(filter);

                    foreach (NAS.DAL.Invoice.BillPromotion item in salesInvoice.BillPromotions)
                    {
                        if (item.PromotionTypeId.PromotionTypeId == promotionType.PromotionTypeId)
                        {
                            txtServiceDiscountAmount.Value = item.PromotionInPercentage;
                            txtServiceDiscountSum.Value = item.PromotionInNumber;

                        }
                    }

                    filter = CriteriaOperator.Parse("[BillId!Key] = ? And [ItemUnitId.ItemId.ItemCustomTypes][[ObjectTypeId.ObjectTypeId] = ?]",
                    Guid.Parse(Session["SaleBillId"].ToString()),
                    Guid.Parse("bebab7e7-8294-4eb0-81df-b5e33acbfe76"));
                 
                    billItem = new XPCollection<BillItem>(session, filter);

                    if (billItem.Count > 0)
                    {
                        txtServiceTax.Value = billItem[0].ItemUnitId.ItemId.VatPercentage;
                        txtServiceTaxValue.Value = (billItem.Sum(p => p.Quantity * p.Price) - double.Parse(txtProductDiscountSum.Value.ToString())) *
                                                            (billItem[0].ItemUnitId.ItemId.VatPercentage / 100);
                    }

                    total = 0;
                    if (billItem.Count > 0)
                    {
                        foreach (BillItem item in billItem)
                        {
                            total += double.Parse(item.TotalPrice.ToString());
                        }
                    }

                    txtServiceTotal.Value = total - double.Parse(txtServiceDiscountSum.Value.ToString()) + double.Parse(txtServiceTaxValue.Value.ToString());

                    txtSumDiscount.Value = double.Parse(txtProductDiscountSum.Value.ToString()) + double.Parse(txtServiceDiscountSum.Value.ToString());
                    txtSumProduct.Value = double.Parse(txtProductTotal.Value.ToString());
                    txtSumService.Value = double.Parse(txtServiceTotal.Value.ToString()); ;
                    txtSumTax.Value = double.Parse(txtProductTaxValue.Value.ToString()) + double.Parse(txtServiceTaxValue.Value.ToString()); 

                    txtAmount.Value = salesInvoice.Total;

                    //string sql = "select SUM(gl.Debit) as paided " +
                    //            " from GeneralJournal gl, SaleInvoiceTransaction sit" +
                    //            " where gl.TransactionId = sit.TransactionId" +
                    //            " and gl.Debit > 0" +
                    //            " and gl.JournalType = 'A'" +
                    //            " and sit.SalesInvoiceId = '" + Session["SaleBillId"].ToString() + "'" +
                    //            " group by gl.Debit";


                    //double value = 0;

                    //Object obj = session.ExecuteScalar(sql);

                    //if (obj != null)
                    //{
                    //    txtPaid.Value = Double.TryParse(obj.ToString(), out value);
                    //}
                    //else
                    //{
                    //    txtPaid.Value = 0;
                    //}

                    //txtCredit.Value = double.Parse(salesInvoice.Total.ToString()) - double.Parse(txtPaid.Value.ToString()); ; ;

                    cpLine.JSProperties.Add("cpEnable", "true");
                   
                    grdPurchaseEditProduct.Columns["Thao tác"].Visible = false;
                    grdPurchaseEditService.Columns["Thao tác"].Visible = false;
                    //grdDeliverySchedule.Columns["Thao tác"].Visible = false;
                    grdPaymentSchedule.Columns["Thao tác"].Visible = false;
                    grdDeliveryBillItem.Columns["Thao tác"].Visible = false;

                    if (grdPurchaseEditService.VisibleRowCount <= 0)
                    {
                        grdPurchaseEditService.Enabled = false;
                        cpLine.JSProperties.Add("cpDisable", "2");
                    }
              
                    txtCode.ReadOnly = true;                    
                    txtIssuedDate.ReadOnly = true;
                    txtProductDiscountAmount.ReadOnly = true;
                    txtProductDiscountSum.ReadOnly = true;
                    txtProductTax.ReadOnly = true;
                    txtProductTaxValue.ReadOnly = true;
                    txtProductTotal.ReadOnly = true;
                    txtServiceDiscountAmount.ReadOnly = true;
                    txtServiceDiscountSum.ReadOnly = true;
                    txtServiceTax.ReadOnly = true;
                    txtServiceTaxValue.ReadOnly = true;
                    txtServiceTotal.ReadOnly = true;

                    cboSupplier.ClientEnabled = false;
                    cboUser.ClientEnabled = false;
                   
                    txtCode.Focus();

                    filter = new BinaryOperator("SalesInvoiceId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);
                    SalesInvoiceInventoryTransaction salesInvoiceInventoryTransaction = session.FindObject<SalesInvoiceInventoryTransaction>(filter);

                    if (salesInvoiceInventoryTransaction != null)
                    {
                        Session["InventoryTransactionId"] = salesInvoiceInventoryTransaction.InventoryTransactionId;
                    }

                    //XPQuery<SalesInvoiceInventoryTransaction> saleInvoiceTransaction = new XPQuery<SalesInvoiceInventoryTransaction>(session);
                    //XPQuery<InventoryJournal> inventoryJournal = new XPQuery<InventoryJournal>(session);

                    //var list = from i in inventoryJournal.AsEnumerable()
                    //           join s in saleInvoiceTransaction on i.InventoryTransactionId.InventoryTransactionId equals s.InventoryTransactionId
                    //           where s.SalesInvoiceId.BillId == Guid.Parse(Session["SaleBillId"].ToString())
                    //           && i.JournalType == 'A' && i.Debit > 0
                    //           select new
                    //           {
                    //               CreateDate = i.CreateDate,
                    //               Code = i.InventoryTransactionId.Code,
                    //               Name = i.ItemUnitId.ItemId.Name,
                    //               UnitId = i.ItemUnitId.UnitId.Name,
                    //               Debit = i.Debit,
                    //               LotId = i.LotId.Code,
                    //               InventoryId = i.InventoryId.Name,
                    //               Description = i.InventoryTransactionId.Description
                    //           };

                    if (Session["SaleBillId"] as string != "")
                    {
                        ReceiptVoucherTransactionBO receiptVoucherTransactionBO = new ReceiptVoucherTransactionBO();
                        XPCollection<GeneralJournal> collectGeneralJournal = receiptVoucherTransactionBO.GetActuallyCollectedOfBill(session, Guid.Parse(Session["SaleBillId"].ToString()));
                        grdPaymentScheduleActual.KeyFieldName = "GeneralJournalId";
                        grdPaymentScheduleActual.DataSource = collectGeneralJournal;
                        grdPaymentScheduleActual.DataBind();


                        if (collectGeneralJournal.Count > 0)
                        {
                            txtPaid.Value = collectGeneralJournal.Sum(p => p.Debit);
                            txtCredit.Value = double.Parse(txtAmount.Value.ToString()) - double.Parse(txtPaid.Value.ToString());
                        }
                        else
                        {
                            txtPaid.Value = 0;
                            txtCredit.Value = txtAmount.Value;
                        }

                        InventoryCommandBO inventoryCommandBO = new InventoryCommandBO();
                        IEnumerable<InventoryJournal> collectInventoryJournal = inventoryCommandBO.GetActualInventoryJournalOfBill(session, 
                            Guid.Parse(Session["SaleBillId"].ToString()), 
                            NAS.DAL.CMS.ObjectDocument.DefaultObjectTypeCustomFieldEnum.INVENTORY_OUT_SALE_INVOICE);
                        grdDeliveryScheduleActual.KeyFieldName = "InventoryJournalId";
                        grdDeliveryScheduleActual.DataSource = collectInventoryJournal;
                        grdDeliveryScheduleActual.DataBind();
                    }
                }
            }
            return true;
        }

        public bool OrderListCreating_UpdateGUI()
        {
            if (!validate())
            {
                cpLine.JSProperties["cpRecoverFocus"] = true;
                return false;
            }

            SaleInvoiceBO salesInvoiceBO = new SaleInvoiceBO();
            SalesInvoice salesInvoice = salesInvoiceBO.SaleInvoiceInit(session,
                                        Guid.Parse(Session["SaleBillId"].ToString()),
                                        "SO",
                                        txtCode.Text,
                                        DateTime.Parse(txtIssuedDate.Value.ToString()),
                                        DateTime.Parse(txtIssuedDate.Value.ToString()),
                                        txtProductTotal.Value == null ? 0 : double.Parse(txtProductTotal.Value.ToString()),
                                        txtProductDiscountSum.Value == null ? 0 : double.Parse(txtProductDiscountSum.Value.ToString()),
                                        txtProductTaxValue.Value == null ? 0 : double.Parse(txtProductTaxValue.Value.ToString()),
                                        txtAmount.Value == null ? 0 : double.Parse(txtAmount.Value.ToString()),
                                        1,
                                        cboSupplier.Value == null ? "" : cboSupplier.Value.ToString(),
                                        cboUser.Value == null ? "" : cboUser.Value.ToString(),
                                        Guid.Empty);

            // Product - Promotion

            BillPromotion billPromotionProduct = new BillPromotion(session);
            Guid billPromotionId = Guid.NewGuid();


            CriteriaOperator filter = new BinaryOperator("PromotionTypeName", "PROMOTION_TYPE_PRODUCT", BinaryOperatorType.Equal);
            PromotionType promotionType = session.FindObject<PromotionType>(filter);

            foreach (NAS.DAL.Invoice.BillPromotion item in salesInvoice.BillPromotions)
            {
                if (item.PromotionTypeId.PromotionTypeId == promotionType.PromotionTypeId)
                {
                    billPromotionId = item.BillPromotionId;
                }
            }

            billPromotionProduct = salesInvoiceBO.BillPromotionInit(session,
                                        billPromotionId,
                                        txtProductDiscountAmount.Value == null ? 0 : double.Parse(txtProductDiscountAmount.Value.ToString()),
                                        txtProductDiscountSum.Value == null ? 0 : double.Parse(txtProductDiscountSum.Value.ToString()),
                                        promotionType.PromotionTypeId,
                                        salesInvoice.BillId);

            // Product - Tax

            Tax taxProduct = null;
            Tax taxService = null;

            BillItemTax billItemTax = new BillItemTax(session);

            foreach (NAS.DAL.Invoice.BillItem item in salesInvoice.BillItems)
            {
                ItemTax itemTax = null;

                if (taxProduct == null)
                {
                    filter = GroupOperator.And(new BinaryOperator("ObjectTypeId", Guid.Parse("5817B239-E150-4C8E-A313-EAA8BD6944C4"), BinaryOperatorType.Equal),
                                                        new BinaryOperator("ItemId", item.ItemUnitId.ItemId, BinaryOperatorType.Equal));                    

                    ItemCustomType itemCustomType = session.FindObject<ItemCustomType>(filter);

                    if (itemCustomType != null)
                    {
                        filter = GroupOperator.And(new BinaryOperator("TaxId.TaxTypeId.Code", "GTGT", BinaryOperatorType.Equal),
                                                             new BinaryOperator("ItemId.ItemId", item.ItemUnitId.ItemId, BinaryOperatorType.Equal));
                        itemTax = session.FindObject<ItemTax>(filter);
                        if (itemTax != null)
                        {
                            taxProduct = itemTax.TaxId;
                        }
                    }
                }

                if (taxService == null)
                {
                    filter = GroupOperator.And(new BinaryOperator("ObjectTypeId", Guid.Parse("BEBAB7E7-8294-4EB0-81DF-B5E33ACBFE76"), BinaryOperatorType.Equal),
                                                   new BinaryOperator("ItemId", item.ItemUnitId.ItemId, BinaryOperatorType.Equal));

                    ItemCustomType itemCustomType = session.FindObject<ItemCustomType>(filter);

                    if (itemCustomType != null)
                    {
                        filter = GroupOperator.And(new BinaryOperator("TaxId.TaxTypeId.Code", "GTGT", BinaryOperatorType.Equal),
                                                             new BinaryOperator("ItemId.ItemId", item.ItemUnitId.ItemId, BinaryOperatorType.Equal));
                        itemTax = session.FindObject<ItemTax>(filter);
                        if (itemTax != null)
                        {
                            taxService = itemTax.TaxId;
                        }
                    }
                }

                filter = new BinaryOperator("BillItemId", item.BillItemId, BinaryOperatorType.Equal);
                billItemTax = session.FindObject<BillItemTax>(filter);
                Guid billItemTaxId = Guid.NewGuid();

                if (billItemTax != null)
                {
                    billItemTaxId = billItemTax.BillItemTaxId;
                }
              

                billItemTax = salesInvoiceBO.BillItemTaxInit(
                    session,
                    item,
                    billItemTaxId,
                    itemTax,
                    item.ItemUnitId.ItemId.VatPercentage,
                    ((item.Quantity * item.Price) - (item.Quantity * item.Price * item.PromotionInPercentage / 100)) * item.ItemUnitId.ItemId.VatPercentage / 100
                    );

                billItemTax.Save();
                
               
            }

            filter = new BinaryOperator("BillId", salesInvoice.BillId, BinaryOperatorType.Equal);
            BillTax billTaxProduct = session.FindObject<BillTax>(filter);
            Guid billTaxId = Guid.NewGuid();

            if (billTaxProduct != null)
            {
                billTaxId = billTaxProduct.BillTaxId;
            }              
            
            billTaxProduct = salesInvoiceBO.BillTaxInit(session,
                                        salesInvoice,
                                        billTaxId,
                                        taxProduct,
                                        txtProductTax.Value == null ? 0 : double.Parse(txtProductTax.Value.ToString()),
                                        txtProductTaxValue.Value == null ? 0 : double.Parse(txtProductTaxValue.Value.ToString())
                                        );


            // Service - Promotion

            BillPromotion billPromotionService = new BillPromotion(session);
            billPromotionId = Guid.NewGuid();

            filter = new BinaryOperator("PromotionTypeName", "PROMOTION_TYPE_SERVICE", BinaryOperatorType.Equal);
            promotionType = session.FindObject<PromotionType>(filter);

            foreach (NAS.DAL.Invoice.BillPromotion item in salesInvoice.BillPromotions)
            {
                if (item.PromotionTypeId.PromotionTypeId == promotionType.PromotionTypeId)
                {
                    billPromotionId = item.BillPromotionId;
                }
            }

            billPromotionService = salesInvoiceBO.BillPromotionInit(session,
                                        billPromotionId,
                                        txtServiceDiscountAmount.Value == null ? 0 : double.Parse(txtServiceDiscountAmount.Value.ToString()),
                                        txtServiceDiscountSum.Value == null ? 0 : double.Parse(txtServiceDiscountSum.Value.ToString()),
                                        promotionType.PromotionTypeId,
                                        salesInvoice.BillId);

            // Service - Tax
            
            filter = new BinaryOperator("BillId", salesInvoice.BillId, BinaryOperatorType.Equal);
            BillTax billTaxService = session.FindObject<BillTax>(filter);
            billTaxId = Guid.NewGuid();

            if (billTaxService != null)
            {
                billTaxId = billTaxProduct.BillTaxId;
            }
 
            billTaxService = salesInvoiceBO.BillTaxInit(session,
                                        salesInvoice,
                                        billTaxId,
                                        taxService,
                                        txtServiceTax.Value == null ? 0 : double.Parse(txtServiceTax.Value.ToString()),
                                        txtServiceTaxValue.Value == null ? 0 : double.Parse(txtServiceTaxValue.Value.ToString())
                                        );
 

            salesInvoiceBO.updateSaleInvoice(session,
                                                    salesInvoice,                                                    
                                                    billPromotionProduct,
                                                    billTaxProduct,
                                                    billPromotionService,
                                                    billTaxService
                                                    );

            salesInvoiceBO.BillTransactionInit(session,
                                                Guid.Parse(Session["SaleBillId"].ToString()),
                                                double.Parse(txtAmount.Value.ToString()),
                                                double.Parse(txtSumDiscount.Value.ToString()),
                                                double.Parse(txtSumTax.Value.ToString()),
                                                double.Parse(txtAmount.Value.ToString()) + double.Parse(txtSumDiscount.Value.ToString()) + double.Parse(txtSumTax.Value.ToString())
                                                );

            grdPurchaseEditProduct.CancelEdit();
            grdPurchaseEditService.CancelEdit();

            cpLine.JSProperties.Add("cpEnable", Session["SaleBillId"].ToString());
                    
            Status = "load";

            return true;
        }

        public bool OrderListEditing_UpdateGUI()
        {
            Status = "edit";

            grdPurchaseEditProduct.Columns["Thao tác"].Visible = true;
            grdPurchaseEditService.Columns["Thao tác"].Visible = true;
            grdPaymentSchedule.Columns["Thao tác"].Visible = true;            
            //grdDeliverySchedule.Columns["Thao tác"].Visible = true;
            grdDeliveryBillItem.Columns["Thao tác"].Visible = true;

            txtAmount.ReadOnly = false;
            txtCode.ReadOnly = false;            
            txtIssuedDate.ReadOnly = false;
            txtProductDiscountAmount.ReadOnly = false;
            txtProductDiscountSum.ReadOnly = false;
            txtProductTax.ReadOnly = false;
            txtProductTaxValue.ReadOnly = false;
            txtProductTotal.ReadOnly = false;
            txtServiceDiscountAmount.ReadOnly = false;
            txtServiceDiscountSum.ReadOnly = false;
            txtServiceTax.ReadOnly = false;
            txtServiceTaxValue.ReadOnly = false;
            txtServiceTotal.ReadOnly = false;

            cboSupplier.ClientEnabled = true;
            cboUser.ClientEnabled = true;

            bOutputInventoryCommand.Enabled = true;
            bPayment.Enabled = true;

            if (!cpLine.JSProperties.ContainsKey("cpEnable"))
            {
                cpLine.JSProperties.Add("cpEnable", "true");
            }

            return true;
        }

        public bool OrderListExiting_UpdateGUI()
        {
            Status = "exit";
            return true;
        }

        // không sử dụng
        public bool OrderListInventoryExporting_UpdateGUI()
        {
            //2013-11-08 ERP-933 Khoa.Truong MOD INS
            using (UnitOfWork inventoryUow = XpoHelper.GetNewUnitOfWork())
            {
                SaleInvoiceBO saleInvoice = new SaleInvoiceBO();

                CriteriaOperator filter = CriteriaOperator.Parse("[BillId!Key] = ? And [ItemUnitId.ItemId.ItemCustomTypes][[ObjectTypeId.ObjectTypeId] = ?]", Session["SaleBillId"].ToString(), "5817b239-e150-4c8e-a313-eaa8bd6944c4");
                
                XPCollection<BillItem> billItem = new XPCollection<BillItem>(inventoryUow, filter);
                SalesInvoice bill = inventoryUow.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));
                string inventoryOutputCode = artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.INVENTORY_OUTPUT);                

                saleInvoice.CreateSalesInvoiceInventoryTransaction(                    
                    inventoryUow,
                    bill.BillId,                    
                    AccountingPeriodBO.getCurrentAccountingPeriod(session).AccountingPeriodId,
                    bill.IssuedDate,
                    Guid.Parse("fa31071d-6010-4788-83b9-9f0ce0c90c5f"),
                    billItem,
                    bill.Code + "_" + inventoryOutputCode,
                    "");

                bill.RowStatus = 2;
                bill.Save();
                inventoryUow.CommitChanges();
            }
            //2013-11-08 ERP-933 Khoa.Truong MOD INS
            grdPurchaseEditProduct.Columns["Thao tác"].Visible = false;
            grdPurchaseEditService.Columns["Thao tác"].Visible = false;

            txtAmount.ReadOnly = true;
            txtCode.ReadOnly = true;            
            txtIssuedDate.ReadOnly = true;
            txtProductDiscountAmount.ReadOnly = true;
            txtProductDiscountSum.ReadOnly = true;
            txtProductTax.ReadOnly = true;
            txtProductTaxValue.ReadOnly = true;
            txtProductTotal.ReadOnly = true;
            txtServiceDiscountAmount.ReadOnly = true;
            txtServiceDiscountSum.ReadOnly = true;
            txtServiceTax.ReadOnly = true;
            txtServiceTaxValue.ReadOnly = true;
            txtServiceTotal.ReadOnly = true;

            cboSupplier.ReadOnly = true;
            cboUser.ReadOnly = true;

            cpLine.JSProperties.Add("cpEnable", "true");
                    

            return true;
        }
        //

        public bool OrderListBooking_UpdateGUI()
        {
            return true;
        }

        public bool OrderListPrinting_UpdateGUI()
        {
            return true;
        }

        /////////////////////////////////////////////////////////////////////////////

        protected Boolean validate()
        {
            CriteriaOperator filter = new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] {
                                            new BinaryOperator("BillId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal),
                                            new BinaryOperator("RowStatus", -10, BinaryOperatorType.Greater)});

            SalesInvoice billExists = session.FindObject<SalesInvoice>(filter);

            if (billExists == null)
            {
                return false;
            }
            else
            {
                if (billExists.BillItems.Count <= 0)
                {
                    return false;
                }
            }         

            return true;
        }

        protected void sumProductItem()
        {
            double total = 0;
            double totalck = 0;

            total = Convert.ToDouble(grdPurchaseEditProduct.GetTotalSummaryValue(grdPurchaseEditProduct.TotalSummary["TotalPrice"]));
            totalck = Convert.ToDouble(grdPurchaseEditProduct.GetTotalSummaryValue(grdPurchaseEditProduct.TotalSummary["TotalPriceCK"]));

            txtProductTaxValue.Value = (total - totalck) * double.Parse(txtProductTax.Value == null ? "0" : txtProductTax.Value.ToString()) / 100;

            txtProductDiscountSum.Value = totalck;
            txtProductTotal.Value = total + double.Parse(txtProductTaxValue.Value == null ? "0" : txtProductTaxValue.Value.ToString()) - totalck;

            summaryBillChange();

            
        }

        protected void sumServiceItem()
        {
            double total = 0;
            double totalck = 0;

            total = Convert.ToDouble(grdPurchaseEditService.GetTotalSummaryValue(grdPurchaseEditService.TotalSummary["TotalPrice"]));
            totalck = Convert.ToDouble(grdPurchaseEditService.GetTotalSummaryValue(grdPurchaseEditService.TotalSummary["TotalPriceCK"]));

            txtServiceTaxValue.Value = (total - totalck) * double.Parse(txtServiceTax.Value == null ? "0" : txtServiceTax.Value.ToString()) / 100;

            txtServiceDiscountSum.Value = totalck;
            txtServiceTotal.Value = total + double.Parse(txtServiceTaxValue.Value == null ? "0" : txtServiceTaxValue.Value.ToString()) - totalck;

            summaryBillChange();
        }
       
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();

            if (!Page.IsPostBack)
            {
                if (Session["SaleBillId"] == null)
                {
                    Session["SaleBillId"] = Guid.NewGuid();
                }
            }
            
            ProductXDS.Session = session;
            ServiceXDS.Session = session;

            UnitItemServiceXDS.Session = session;
            PersonXDS.Session = session;
            BuyerXDS.Session = session;
            
            InventoryJournalXDS.Session = session;
            InventoryJournalActualXDS.Session = session;

            BillItemXDS.Session = session;
            
            SalesInvoiceInventoryTransactionXDS.Session = session;
            SalesInvoiceInventoryTransactionActualXDS.Session = session;
            SaleInvoiceTransactionXDS.Session = session;
            SaleInvoiceTransactionActualXDS.Session = session;

            GeneralJournalXDS.Session = session;

            BillActorXDS.Session = session;
        }
        
        private void SetCriteriaForOrganization()
        {
            //Get CUSTOMER trading type
            TradingCategory customerTradingCategory =
                NAS.DAL.Util.getXPCollection<TradingCategory>(session, "Code", "CUSTOMER").FirstOrDefault();
            //Get SUPPLIER trading type
            TradingCategory supplierTradingCategory =
                NAS.DAL.Util.getXPCollection<TradingCategory>(session, "Code", "SUPPLIER").FirstOrDefault();

            CriteriaOperator criteria = CriteriaOperator.And(
                CriteriaOperator.Or(
                    new ContainsOperator("OrganizationCategories",
                        CriteriaOperator.And(
                            new BinaryOperator("TradingCategoryId.TradingCategoryId", customerTradingCategory.TradingCategoryId),
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
                        )
                    ),
                    new ContainsOperator("OrganizationCategories",
                        CriteriaOperator.And(
                            new BinaryOperator("TradingCategoryId.TradingCategoryId", supplierTradingCategory.TradingCategoryId),
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
                        )
                    )
                ),
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
            );
            BuyerXDS.Criteria = criteria.ToString();
            cboSupplier.DataBindItems();
        }

        private ArtifactCodeRuleBO artifactCodeRuleBO;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (hPurchaseEditId.Contains("id"))
            //{
            //    if (!hPurchaseEditId.Get("id").ToString().Equals(""))
            //    {
            //        Session["SaleBillId"] = Guid.Parse(hPurchaseEditId.Get("id").ToString());
            //    }
            //}

            //if (Session["SaleBillId"] as string != "")
            //{
            //    XPCollection<SaleInvoiceTransaction> t = new XPCollection<SaleInvoiceTransaction>(session);
            //    t.Filter = new ContainsOperator("GeneralJournals", new BinaryOperator("JournalType", "A"));
                
            //    ReceiptVoucherTransactionBO receiptVoucherTransactionBO = new ReceiptVoucherTransactionBO();
            //    XPCollection<GeneralJournal> collectGeneralJournal = receiptVoucherTransactionBO.GetActuallyCollectedOfBill(session, Guid.Parse(Session["SaleBillId"].ToString()));

            //    grdPaymentScheduleActual.KeyFieldName = "GeneralJournalId";
            //    grdPaymentScheduleActual.DataSource = collectGeneralJournal;
            //    grdPaymentScheduleActual.DataBind();
            //}

          

            SetCriteriaForOrganization();

            //if (Session["SaleBillId"] != null)
            //{
            //    if (session.GetObjectByKey<Bill>(Guid.Parse(Session["SaleBillId"].ToString())) != null)
            //    {
            //        uEdittingOutputInventoryCommand1.SettingInit(Guid.Parse(Session["SaleBillId"].ToString()), bOutputInventoryCommand);
            //    }
            //}
        }

        protected void cpLine_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();

            String[] p = e.Parameter.Split('|');

            switch (p[0])
            {
                case "new":                             
                    GUIContext = new Context(new OrderListLoading(this, true));
                    GUIContext.Request("UpdateGUI");                    
                    break;

                case "load":
                    Status = "load";

                    GUIContext = new Context(new OrderListLoading(this, true));
                    GUIContext.Request("UpdateGUI");

                    break;

                case "edit":
                    GUIContext.State = new OrderListEditing(this, true);
                    GUIContext.Request("UpdateGUI");     
                    
                    break;

                case "save":
                    GUIContext.State = new OrderListCreating(this, true);
                    GUIContext.Request("UpdateGUI");

                    break;

                case "inventoryexport":
                    GUIContext.State = new OrderListInventoryExporting(this, true);
                    GUIContext.Request("UpdateGUI");

                    break;

                // Product 
                case "sumProductItem":
                    sumProductItem();

                    if (p[1] == "withAddNewRow")
                    {
                        grdPurchaseEditProduct.AddNewRow();
                    }
                    break;

                // % VAT
                case "txtProductTax_ValueChanged":

                    txtProductTaxValue.Value = double.Parse(txtProductTotal.Value.ToString()) * double.Parse(txtProductTax.Value.ToString()) / 100;
                    txtProductTotal.Value = double.Parse(txtProductTotal.Value.ToString()) + double.Parse(txtProductTaxValue.Value.ToString());
                    
                    summaryBillChange();

                    break;

                // % Chiet khau
                case "txtProductDiscountAmount_ValueChanged":
                    double total = Convert.ToDouble(grdPurchaseEditProduct.GetTotalSummaryValue(grdPurchaseEditProduct.TotalSummary["TotalPrice"]));
                    
                    txtProductDiscountSum.Value = total * double.Parse(txtProductDiscountAmount.Value.ToString()) / 100;
                    
                    txtProductTaxValue.Value = (total - double.Parse(txtProductDiscountSum.Value.ToString())) * double.Parse(txtProductTax.Value.ToString()) / 100;
                    txtProductTotal.Value = (total - double.Parse(txtProductDiscountSum.Value.ToString())) + double.Parse(txtProductTaxValue.Value.ToString());
                    
                    summaryBillChange();

                    //session.ExecuteNonQuery("UPDATE [dbo].[BillItem]" +
                    //                        " SET " +
                    //                            "[PromotionInPercentage] = " + txtProductDiscountAmount.Value.ToString() +
                    //                            ",[PromotionInNumber] = TotalPrice/100 *" + txtProductDiscountAmount.Value.ToString() +
                    //                        " WHERE " +
                    //                        " [BillId] = '" + Session["SaleBillId"].ToString() + "'");
                                        

                    break;

                // Chiet khau 
                case "txtProductDiscountSum_ValueChanged":
                    
                    total = Convert.ToDouble(grdPurchaseEditProduct.GetTotalSummaryValue(grdPurchaseEditProduct.TotalSummary["TotalPrice"]));

                    txtProductTaxValue.Value = (total - double.Parse(txtProductDiscountSum.Value.ToString())) * double.Parse(txtProductTax.Value.ToString()) / 100;
                    txtProductTotal.Value = (total - double.Parse(txtProductDiscountSum.Value.ToString())) + double.Parse(txtProductTaxValue.Value.ToString());
                    
                    summaryBillChange();

                    break;                

                case "sumServiceItem":  // Service
                    sumServiceItem();

                    if (p[1] == "withAddNewRow")
                    {
                        grdPurchaseEditService.AddNewRow();
                    }             
       
                    break;

                case "txtServiceDiscountAmount_ValueChanged":
                    total = Convert.ToDouble(grdPurchaseEditService.GetTotalSummaryValue(grdPurchaseEditService.TotalSummary["TotalPrice"]));

                    txtServiceDiscountSum.Value = total * double.Parse(txtServiceDiscountAmount.Value.ToString()) / 100;

                    txtServiceTaxValue.Value = (total - double.Parse(txtServiceDiscountSum.Value.ToString())) * double.Parse(txtServiceTax.Value.ToString()) / 100;
                    txtServiceTotal.Value = (total - double.Parse(txtServiceDiscountSum.Value.ToString())) + double.Parse(txtServiceTaxValue.Value.ToString());
                    
                    summaryBillChange();

                    break;

                case "txtServiceTax_ValueChanged":
                    total = Convert.ToDouble(grdPurchaseEditService.GetTotalSummaryValue(grdPurchaseEditService.TotalSummary["TotalPrice"]));

                    txtServiceDiscountSum.Value = total * double.Parse(txtServiceDiscountAmount.Value.ToString()) / 100;

                    txtServiceTaxValue.Value = (total - double.Parse(txtServiceDiscountSum.Value.ToString())) * double.Parse(txtServiceTax.Value.ToString()) / 100;
                    txtServiceTotal.Value = (total - double.Parse(txtServiceDiscountSum.Value.ToString())) + double.Parse(txtServiceTaxValue.Value.ToString());

                    summaryBillChange();

                    break;

                case "txtServiceDiscountSum_ValueChanged":
                    total = Convert.ToDouble(grdPurchaseEditService.GetTotalSummaryValue(grdPurchaseEditService.TotalSummary["TotalPrice"]));

                    txtServiceTaxValue.Value = (total - double.Parse(txtServiceDiscountSum.Value.ToString())) * double.Parse(txtServiceTax.Value.ToString()) / 100;
                    txtServiceTotal.Value = (total - double.Parse(txtServiceDiscountSum.Value.ToString())) + double.Parse(txtServiceTaxValue.Value.ToString());

                    summaryBillChange();

                    break;

                case "txtCode_Checkexists":
                    CriteriaOperator filter = new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] {
                            new BinaryOperator("Code", txtCode.Text, BinaryOperatorType.Equal),
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)});

                    SalesInvoice listBill = session.FindObject<SalesInvoice>(filter);

                    if (listBill != null)
                    {
                        if (listBill.RowStatus != ROW_DELETE)
                        {
                            cpLine.JSProperties.Add("cpCode", "invalid");
                            txtCode.Text = null;
                            txtCode.Focus();
                        }                        
                    }
                    else
                    {
                        cpLine.JSProperties.Add("cpCode", "valid");
                    }

                    break;

                case "txtIssuedDate_Checkperiod":
                    AccountingPeriod accountingPeriod = session.GetObjectByKey<AccountingPeriod>(AccountingPeriodBO.getCurrentAccountingPeriod(session).AccountingPeriodId);
                    if (accountingPeriod.FromDateTime > DateTime.Parse(txtIssuedDate.Value.ToString()))
                    {
                        cpLine.JSProperties.Add("cpAaccountPeriod", "invalid");
                        txtIssuedDate.Text = null;
                        txtIssuedDate.Focus();
                    }
                    else
                    {
                        cpLine.JSProperties.Add("cpAaccountPeriod", "valid");
                    }

                    break;

                case "viewReport":
                    cpLine.JSProperties.Add("cpReport", Guid.Parse(hPurchaseEditId.Get("id").ToString()));
                    break;
                
                case "itemproperty":

                    if (p[1].Equals("null"))
                    {
                        return;
                    }

                    PositionItem = 0;

                    Session["InventoryTransactionId"] = Guid.Parse(p[1]);
   
                    PositionItem = Int32.Parse(p[2]);                 

                    break;

                //case "itempropertyactual":
                //    if (p[1].Equals("null"))
                //    {
                //        return;
                //    }

                //    Session["InventoryTransactionId"] = Guid.Parse(p[1]);

                //    break;

                default:

                    //salesInvoice = uow.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));
                    //if (salesInvoice.RowStatus == 2)
                    //{
                    //    grdPurchaseEditProduct.Columns["Thao tác"].Visible = false;
                    //    grdPurchaseEditService.Columns["Thao tác"].Visible = false;

                    //    if (grdPurchaseEditService.VisibleRowCount <= 0)
                    //    {
                    //        grdPurchaseEditService.Enabled = false;
                    //        cpLine.JSProperties.Add("cpDisable", "2");
                    //    }

                    //    txtAmount.ReadOnly = true;
                    //    txtCode.ReadOnly = true;
                    //    txtDescription.ReadOnly = true;
                    //    txtIssuedDate.ReadOnly = true;
                    //    txtProductDiscountAmount.ReadOnly = true;
                    //    txtProductDiscountSum.ReadOnly = true;
                    //    txtProductTax.ReadOnly = true;
                    //    txtProductTaxValue.ReadOnly = true;
                    //    txtProductTotal.ReadOnly = true;
                    //    txtServiceDiscountAmount.ReadOnly = true;
                    //    txtServiceDiscountSum.ReadOnly = true;
                    //    txtServiceTax.ReadOnly = true;
                    //    txtServiceTaxValue.ReadOnly = true;
                    //    txtServiceTotal.ReadOnly = true;

                    //    cboSupplier.ClientEnabled = false;
                    //    cboUser.ClientEnabled = false;
                    //}
                    
                    
                    break;
                    
            }

            //if (Session["SaleBillId"] != null)
            //{
            //    if (session.GetObjectByKey<Bill>(Guid.Parse(Session["SaleBillId"].ToString())) != null)
            //    {
            //        uEdittingOutputInventoryCommand1.SettingInit(Guid.Parse(Session["SaleBillId"].ToString()), bOutputInventoryCommand);
            //    }
            //}
        }

        protected void summaryBillChange()
        {
            txtAmount.Value = txtCredit.Value = (txtProductTotal.Value == null ? 0 : double.Parse(txtProductTotal.Value.ToString())) + (txtServiceTotal.Value == null ? 0 : double.Parse(txtServiceTotal.Value.ToString()));

            txtSumDiscount.Value = double.Parse(txtProductDiscountSum.Value.ToString()) + double.Parse(txtServiceDiscountSum.Value.ToString());
            txtSumProduct.Value = double.Parse(txtProductTotal.Value.ToString());
            txtSumService.Value = double.Parse(txtServiceTotal.Value.ToString());
            txtSumTax.Value = double.Parse(txtProductTaxValue.Value.ToString()) + double.Parse(txtServiceTaxValue.Value.ToString());    
        }

        protected void cpCommand_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {            
            ASPxCallbackPanel cpp = (ASPxCallbackPanel)formPurchaseEdit.FindControl("cpCommand");
            ASPxButton button = (ASPxButton)cpp.FindControl("buttonImportInventory");

            SalesInvoice bill = session.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));
            if (bill != null)
            {
                if (bill.RowStatus != 1 || bill.BillItems.Count <= 0)
                {
                    //button.Visible = false;
                }
                else
                {
                    //button.Visible = true;
                }
            }

            if (bill != null)
            {
                if (bill.BillItems.Count <= 0)
                {
                    button = (ASPxButton)cpp.FindControl("buttonBooking");
                    button.ClientEnabled = false;
                }
                else
                {
                    button = (ASPxButton)cpp.FindControl("buttonBooking");
                    button.ClientEnabled = true;
                }
            }

            switch (Status)
            {
                case "new":
                    button = (ASPxButton)cpp.FindControl("buttonSaveDevice");
                    button.Enabled = true;

                    button = (ASPxButton)cpp.FindControl("buttonModify");
                    button.Enabled = false;

                    button = (ASPxButton)cpp.FindControl("buttonPrint");
                    button.Enabled = false;

                    button = (ASPxButton)cpp.FindControl("buttonImportInventory");
                    button.Enabled = false;

                    button = (ASPxButton)cpp.FindControl("buttonBooking");
                    button.Enabled = false;

                    break;

                case "load":

                    button = (ASPxButton)cpp.FindControl("buttonSaveDevice");
                    button.Enabled = false;

                    button = (ASPxButton)cpp.FindControl("buttonModify");
                    if (bill.RowStatus > 1)
                    {
                        button.Enabled = false;
                    }
                    else
                    {
                        button.Enabled = true;
                    }

                    button = (ASPxButton)cpp.FindControl("buttonImportInventory");

                    if (bill.RowStatus == 2 || bill.RowStatus == 4)
                    {
                        button.Enabled = false;
                    }
                    else
                    {
                        button.Enabled = true;
                    }

                    button = (ASPxButton)cpp.FindControl("buttonPrint");
                    button.Enabled = true;

                    bOutputInventoryCommand.Enabled = false;
                    bPayment.Enabled = false;

                    break;

                case "edit":

                    button = (ASPxButton)cpp.FindControl("buttonSaveDevice");
                    button.Enabled = true;

                    button = (ASPxButton)cpp.FindControl("buttonPrint");
                    button.Enabled = false;

                    button = (ASPxButton)cpp.FindControl("buttonModify");
                    button.Enabled = false;

                    button = (ASPxButton)cpp.FindControl("buttonImportInventory");
                    button.Enabled = false;

                    button = (ASPxButton)cpp.FindControl("buttonBooking");
                    button.Enabled = false;

                    bOutputInventoryCommand.Enabled = true;
                    bPayment.Enabled = true;

                    break;
            }

            button = (ASPxButton)cpp.FindControl("buttonLock");
            button.Enabled = false;

        }

        protected void cboUser_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ASPxEditors.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            XPCollection<Organization> collection = new XPCollection<Organization>(session);

            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            collection.Criteria = new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] {
                    new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                    new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)});
            collection.Sorting.Add(new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));

            cboUser.DataSource = collection;
            cboUser.DataBindItems();
        }

        protected void cboUser_ItemRequestedByValue(object source, DevExpress.Web.ASPxEditors.ListEditItemRequestedByValueEventArgs e)
        {
            try
            {
                Guid g = Guid.Parse(e.Value.ToString());                
            }
            catch
            {
            }
        }
         
        ///////////////////////////////// Product

        protected void grdPurchaseEditProduct_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView gridview = sender as ASPxGridView;

            if (e.Column.FieldName == "ItemUnitId!Key")
            {
                ASPxComboBox combo = e.Editor as ASPxComboBox;
                combo.ClientSideEvents.ValueChanged = "function(s,e){ " +
                                                           "if (s.GetSelectedIndex() < 0) {return;}" +                
                                                           "grdPurchaseEditProduct.GetEditor('ItemUnitId.ItemId.Name').SetValue(s.GetSelectedItem().GetColumnText('ItemId.Name'));" +
                                                           "grdPurchaseEditProduct.GetEditor('ItemUnitId.UnitId.Name').SetValue(s.GetSelectedItem().GetColumnText('UnitId.Name'));" +
                                                           "grdPurchaseEditProduct.GetEditor('ItemUnitId.ItemId.ManufacturerOrgId.Name').SetValue(s.GetSelectedItem().GetColumnText('ItemId.ManufacturerOrgId.Name'));" +
                                                           "grdPurchaseEditProduct.GetEditor('ItemUnitId.ItemId.VatPercentage').SetValue(s.GetSelectedItem().GetColumnText('ItemId.VatPercentage'));" +
                                                           "txtProductTax.SetText(s.GetSelectedItem().GetColumnText('ItemId.VatPercentage'));" +
                                                       "}";


                if (combo != null)
                {
                    combo.Focus();
                }
            }
        }

        protected void comboItemUnit_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            ItemUnit obj = session.GetObjectByKey<ItemUnit>(e.Value);

            if (obj != null)
            {
                comboItemUnit.DataSource = new ItemUnit[] { obj };
                comboItemUnit.DataBindItems();
            }
        }

        protected void comboItemUnit_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            XPCollection<ItemUnit> collection = new XPCollection<ItemUnit>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            //CriteriaOperator criteria = CriteriaOperator.And(
            //        new ContainsOperator("ItemId.ItemCustomTypes", new BinaryOperator("ObjectTypeId.Name",
            //            "PRODUCT")),
            //        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater),
            //        new ContainsOperator("ItemId.itemUnitTypeConfigs", new BinaryOperator("RowStatus", 1, BinaryOperatorType.Equal)),
            //        CriteriaOperator.Or(
            //            new BinaryOperator("ItemId.Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
            //            new BinaryOperator("ItemId.Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)));

            CriteriaOperator criteria = CriteriaOperator.And(
                    new ContainsOperator("ItemId.ItemCustomTypes", new BinaryOperator("ObjectTypeId.Name",
                        "PRODUCT")),
                    new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater),
                    new ContainsOperator("ItemId.itemUnitTypeConfigs", new BinaryOperator("RowStatus", 1, BinaryOperatorType.Equal)),
                    new BinaryOperator("ItemId.Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like));

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("ItemId.Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();
        }

        protected void grdPurchaseEditProduct_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {        
        }

        protected void grdPurchaseEditProduct_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            SalesInvoice bill = session.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));
        
            if (bill != null)
            {
                if (bill.RowStatus > ROW_NEW)
                {
                    e.Cancel = true;
                    grdPurchaseEditProduct.Columns["Thao tác"].Visible = false;
                    grdPurchaseEditService.Columns["Thao tác"].Visible = false;
                    return;
                }
            }
            else
            {
                bill = new SalesInvoice(session);
                bill.Code = txtCode.Text;
                bill.BillId = Guid.Parse(Session["BillId"].ToString());
                bill.RowStatus = 0;
                bill.Save();
            }

            ASPxSpinEdit c = (ASPxSpinEdit)grdPurchaseEditProduct.FindEditRowCellTemplateControl((GridViewDataColumn)grdPurchaseEditProduct.Columns["Price"], "colProductPrice");
            e.NewValues["Price"] = c.Value.ToString();

            c = (ASPxSpinEdit)grdPurchaseEditProduct.FindEditRowCellTemplateControl((GridViewDataColumn)grdPurchaseEditProduct.Columns["TotalPrice"], "colProductSum");
            e.NewValues["TotalPrice"] = c.Value.ToString();

            c = (ASPxSpinEdit)grdPurchaseEditProduct.FindEditRowCellTemplateControl((GridViewDataColumn)grdPurchaseEditProduct.Columns["Quantity"], "colProductAmount");
            e.NewValues["Quantity"] = c.Value.ToString();

            c = (ASPxSpinEdit)grdPurchaseEditProduct.FindEditRowCellTemplateControl((GridViewDataColumn)grdPurchaseEditProduct.Columns["PromotionInPercentage"], "colProductDiscountPercent");
            e.NewValues["PromotionInPercentage"] = c.Value.ToString();

            e.NewValues["BillId!Key"] = Session["SaleBillId"].ToString();

            e.NewValues["PromotionInNumber"] = double.Parse(e.NewValues["TotalPrice"].ToString()) * double.Parse(e.NewValues["PromotionInPercentage"].ToString()) / 100;

            e.NewValues["RowStatus"] = "1";                  

            ((ASPxGridView)sender).JSProperties.Add("cpSumProductItem", "withAddNewRow");           
        }

        protected void grdPurchaseEditProduct_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            SalesInvoice bill = session.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));
            if (bill != null)
            {
                if (bill.RowStatus == ROW_NOT_DELETE)
                {
                    e.Cancel = true;
                    grdPurchaseEditProduct.Columns["Thao tác"].Visible = false;
                    grdPurchaseEditService.Columns["Thao tác"].Visible = false;
                    return;
                }
            }

            ASPxSpinEdit c = (ASPxSpinEdit)grdPurchaseEditProduct.FindEditRowCellTemplateControl((GridViewDataColumn)grdPurchaseEditProduct.Columns["Price"], "colProductPrice");
            e.NewValues["Price"] = c.Value.ToString();

            c = (ASPxSpinEdit)grdPurchaseEditProduct.FindEditRowCellTemplateControl((GridViewDataColumn)grdPurchaseEditProduct.Columns["TotalPrice"], "colProductSum");
            e.NewValues["TotalPrice"] = c.Value.ToString();

            c = (ASPxSpinEdit)grdPurchaseEditProduct.FindEditRowCellTemplateControl((GridViewDataColumn)grdPurchaseEditProduct.Columns["Quantity"], "colProductAmount");
            e.NewValues["Quantity"] = c.Value.ToString();

            c = (ASPxSpinEdit)grdPurchaseEditProduct.FindEditRowCellTemplateControl((GridViewDataColumn)grdPurchaseEditProduct.Columns["PromotionInPercentage"], "colProductDiscountPercent");
            e.NewValues["PromotionInPercentage"] = c.Value.ToString();

            e.NewValues["PromotionInNumber"] = double.Parse(e.NewValues["TotalPrice"].ToString()) * double.Parse(e.NewValues["PromotionInPercentage"].ToString()) / 100;    

            grdPurchaseEditProduct.Focus();

            ((ASPxGridView)sender).JSProperties.Add("cpSumProductItem", "SumProductItem");
            
        }

        protected void grdPurchaseEditProduct_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            SalesInvoice bill = session.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));
            if (bill != null)
            {
                if (bill.RowStatus == ROW_NOT_DELETE)
                {
                    e.Cancel = true;
                    grdPurchaseEditProduct.Columns["Thao tác"].Visible = false;
                    grdPurchaseEditService.Columns["Thao tác"].Visible = false;
                    return;
                }
            }

            BillItem billItem = session.GetObjectByKey<BillItem>(e.Values["BillItemId"]);
            session.Delete(billItem);

            ((ASPxGridView)sender).JSProperties.Add("cpSumProductItem", "SumProductItem");
        }

        protected void grdPurchaseEditProduct_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            GridViewDataColumn col = ((ASPxGridView)sender).Columns["ItemUnitId!Key"] as GridViewDataColumn;
            if (e.NewValues["ItemUnitId!Key"] == null)
            {
                //e.Errors[grdPurchaseEditService.Columns["ItemUnitId!Key"]] = "Chưa nhập mã hàng hóa !";
                e.Errors.Add(col, "The dummy error");
                e.RowError = "Chưa chọn hàng hóa";
                return;
            }

            col = ((ASPxGridView)sender).Columns["Quantity"] as GridViewDataColumn;
            ASPxSpinEdit spin = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col, "colProductAmount") as ASPxSpinEdit;
            
            if (spin.Text == "")
            {
                e.Errors.Add(col, "The dummy error");
                e.RowError = "Số lượng > 0";

                return;
            }
            else
            {
                if (double.Parse(spin.Text) <= 0)
                {
                    e.Errors.Add(col, "The dummy error");
                    e.RowError = "Số lượng > 0";

                    return;
                }
            }

            col = ((ASPxGridView)sender).Columns["Price"] as GridViewDataColumn;
            spin = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col, "colProductPrice") as ASPxSpinEdit;


            if (spin.Text == "")
            {
                e.Errors.Add(col, "The dummy error");
                e.RowError = "Đơn giá > 0";

                return;
            }
            else
            {
                if (double.Parse(spin.Text) <= 0)
                {
                    e.Errors.Add(col, "The dummy error");
                    e.RowError = "Đơn giá > 0";

                    return;
                }
            }

            col = ((ASPxGridView)sender).Columns["TotalPrice"] as GridViewDataColumn;
            spin = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col, "colProductSum") as ASPxSpinEdit;


            if (spin.Text == "")
            {
                e.Errors.Add(col, "The dummy error");
                e.RowError = "Thành tiền > 0";

                return;
            }
            else
            {
                if (double.Parse(spin.Text) <= 0)
                {
                    e.Errors.Add(col, "The dummy error");
                    e.RowError = "Thành tiền > 0";

                    return;
                }
            }
        }

        protected void colProductAmount_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                spin.Text = DataBinder.Eval(container.DataItem, "Quantity").ToString();
            }

            spin.ClientSideEvents.ValueChanged = "function (s,e) {" +                                                            
                                                            "colProductSum.SetValue(s.GetValue()*colProductPrice.GetValue());" +                                                            
                                                  "}";
        }

        protected void colProductPrice_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;
            
            if (!container.Grid.IsNewRowEditing)
            {
                spin.Text = DataBinder.Eval(container.DataItem, "Price").ToString();
            }

            spin.ClientSideEvents.ValueChanged = "function (s,e) { " +                                                            
                                                            "colProductSum.SetValue(s.GetValue()*colProductAmount.GetValue());" +                                                            
                                                  "}";
        }

        protected void colProductSum_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                spin.Text = DataBinder.Eval(container.DataItem, "TotalPrice").ToString();
            }
        }

        protected void colProductDiscountPercent_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                spin.Text = DataBinder.Eval(container.DataItem, "PromotionInPercentage").ToString();
            }

            spin.ClientSideEvents.ValueChanged = "function (s,e) {" +
                                                        "grdPurchaseEditProduct.GetEditor('TotalPriceCK').SetValue(colProductSum.GetValue()*s.GetValue()/100);" +                                                                             
                                                  "}";
        }

        protected void grdPurchaseEditProduct_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "TotalPriceCK")
            {
                decimal ck = Convert.ToDecimal(e.GetListSourceFieldValue("PromotionInPercentage"));
                decimal total = Convert.ToDecimal(e.GetListSourceFieldValue("TotalPrice"));
                e.Value = total * ck / 100;
            }
        }

        protected void grdPurchaseEditProduct_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (Status == "new" || Status == "edit")
            {
                e.Visible = true;
            }
            else
            {
                e.Visible = false;
            }

                //SalesInvoice salesInvoice = session.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));

                //if (salesInvoice != null)
                //{
                //    if (salesInvoice.RowStatus > ROW_NEW)
                //    {
                //        e.Visible = false;
                //    }
                //}
                //else
                //{
                //    e.Visible = true;
                //}
                
        }

        /////////////////////////////////// Service       

        protected void grdPurchaseEditService_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "ItemUnitId!Key")
            {
                ASPxComboBox combo = e.Editor as ASPxComboBox;
                combo.ClientSideEvents.ValueChanged = "function(s,e){ " +
                                                           "grdPurchaseEditService.GetEditor('ItemUnitId.ItemId.Name').SetValue(s.GetSelectedItem().GetColumnText('ItemId.Name'));" +
                                                           "grdPurchaseEditService.GetEditor('ItemUnitId.UnitId.Name').SetValue(s.GetSelectedItem().GetColumnText('UnitId.Name'));" +
                                                           "grdPurchaseEditService.GetEditor('ItemUnitId.ItemId.ManufacturerOrgId.Name').SetValue(s.GetSelectedItem().GetColumnText('ItemId.ManufacturerOrgId.Name'));" +
                                                           "txtServiceTax.SetText(s.GetSelectedItem().GetColumnText('ItemId.VatPercentage'));" +
                                                       "}";

                if (combo != null)
                {
                    combo.Focus();
                }
            }

        }

        protected void grdPurchaseEditService_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            SalesInvoice bill = session.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));
            if (bill != null)
            {
                if (bill.RowStatus == ROW_NOT_DELETE)
                {
                    e.Cancel = true;
                    grdPurchaseEditProduct.Columns["Thao tác"].Visible = false;
                    grdPurchaseEditService.Columns["Thao tác"].Visible = false;
                    return;
                }
            }

            ASPxSpinEdit c = (ASPxSpinEdit)grdPurchaseEditService.FindEditRowCellTemplateControl((GridViewDataColumn)grdPurchaseEditService.Columns["Price"], "colServicePrice");
            e.NewValues["Price"] = c.Value.ToString();

            c = (ASPxSpinEdit)grdPurchaseEditService.FindEditRowCellTemplateControl((GridViewDataColumn)grdPurchaseEditService.Columns["TotalPrice"], "colServiceSum");
            e.NewValues["TotalPrice"] = c.Value.ToString();

            c = (ASPxSpinEdit)grdPurchaseEditService.FindEditRowCellTemplateControl((GridViewDataColumn)grdPurchaseEditService.Columns["Quantity"], "colServiceAmount");
            e.NewValues["Quantity"] = c.Value.ToString();

            c = (ASPxSpinEdit)grdPurchaseEditService.FindEditRowCellTemplateControl((GridViewDataColumn)grdPurchaseEditService.Columns["PromotionInPercentage"], "colServiceDiscountPercent");
            e.NewValues["PromotionInPercentage"] = c.Value.ToString();

            e.NewValues["BillId!Key"] = Session["SaleBillId"].ToString();

            e.NewValues["PromotionInNumber"] = double.Parse(e.NewValues["TotalPrice"].ToString()) * double.Parse(e.NewValues["PromotionInPercentage"].ToString()) / 100;

            e.NewValues["RowStatus"] = "1";

            ((ASPxGridView)sender).JSProperties.Add("cpSumServiceItem", "withAddNewRow");          
        }

        protected void grdPurchaseEditService_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            SalesInvoice bill = session.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));
            if (bill != null)
            {
                if (bill.RowStatus == ROW_NOT_DELETE)
                {
                    e.Cancel = true;
                    grdPurchaseEditProduct.Columns["Thao tác"].Visible = false;
                    grdPurchaseEditService.Columns["Thao tác"].Visible = false;
                    return;
                }
            }

            ASPxSpinEdit c = (ASPxSpinEdit)grdPurchaseEditService.FindEditRowCellTemplateControl((GridViewDataColumn)grdPurchaseEditService.Columns["Price"], "colServicePrice");
            e.NewValues["Price"] = c.Value.ToString();

            c = (ASPxSpinEdit)grdPurchaseEditService.FindEditRowCellTemplateControl((GridViewDataColumn)grdPurchaseEditService.Columns["TotalPrice"], "colServiceSum");
            e.NewValues["TotalPrice"] = c.Value.ToString();

            c = (ASPxSpinEdit)grdPurchaseEditService.FindEditRowCellTemplateControl((GridViewDataColumn)grdPurchaseEditService.Columns["Quantity"], "colServiceAmount");
            e.NewValues["Quantity"] = c.Value.ToString();

            c = (ASPxSpinEdit)grdPurchaseEditService.FindEditRowCellTemplateControl((GridViewDataColumn)grdPurchaseEditService.Columns["PromotionInPercentage"], "colServiceDiscountPercent");
            e.NewValues["PromotionInPercentage"] = c.Value.ToString();

            e.NewValues["PromotionInNumber"] = double.Parse(e.NewValues["TotalPrice"].ToString()) * double.Parse(e.NewValues["PromotionInPercentage"].ToString()) / 100;

            ((ASPxGridView)sender).JSProperties.Add("cpSumServiceItem", "SumServiceItem");           
        }

        protected void grdPurchaseEditService_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            SalesInvoice bill = session.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));
            if (bill != null)
            {
                if (bill.RowStatus == ROW_NOT_DELETE)
                {
                    e.Cancel = true;
                    grdPurchaseEditProduct.Columns["Thao tác"].Visible = false;
                    grdPurchaseEditService.Columns["Thao tác"].Visible = false;
                    return;
                }
            }

            BillItem billItem = session.GetObjectByKey<BillItem>(e.Values["BillItemId"]);
            session.Delete(billItem);

            ((ASPxGridView)sender).JSProperties.Add("cpSumServiceItem", "SumServiceItem");          
        }

        protected void grdPurchaseEditService_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            GridViewDataColumn col = ((ASPxGridView)sender).Columns["ItemUnitId!Key"] as GridViewDataColumn;
            if (e.NewValues["ItemUnitId!Key"] == null)
            {                
                e.Errors.Add(col, "The dummy error");
                e.RowError = "Chưa chọn dịch vụ";
                return;
            }

            col = ((ASPxGridView)sender).Columns["Quantity"] as GridViewDataColumn;
            ASPxSpinEdit spin = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col, "colServiceAmount") as ASPxSpinEdit;

            if (spin.Text == "")
            {
                e.Errors.Add(col, "The dummy error");
                e.RowError = "Số lượng > 0";

                return;
            }
            else
            {
                if (double.Parse(spin.Text) <= 0)
                {
                    e.Errors.Add(col, "The dummy error");
                    e.RowError = "Số lượng > 0";

                    return;
                }
            }

            col = ((ASPxGridView)sender).Columns["Price"] as GridViewDataColumn;
            spin = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col, "colServicePrice") as ASPxSpinEdit;

            if (spin.Text == "")
            {
                e.Errors.Add(col, "The dummy error");
                e.RowError = "Đơn giá > 0";

                return;
            }
            else
            {
                if (double.Parse(spin.Text) <= 0)
                {
                    e.Errors.Add(col, "The dummy error");
                    e.RowError = "Đơn giá > 0";

                    return;
                }
            }

            col = ((ASPxGridView)sender).Columns["TotalPrice"] as GridViewDataColumn;
            spin = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col, "colServiceSum") as ASPxSpinEdit;


            if (spin.Text == "")
            {
                e.Errors.Add(col, "The dummy error");
                e.RowError = "Thành tiền > 0";

                return;
            }
            else
            {
                if (double.Parse(spin.Text) <= 0)
                {
                    e.Errors.Add(col, "The dummy error");
                    e.RowError = "Thành tiền > 0";

                    return;
                }
            }
        }

        protected void colServiceDiscountPercent_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                spin.Text = DataBinder.Eval(container.DataItem, "PromotionInPercentage").ToString();
            }

            spin.ClientSideEvents.ValueChanged = "function (s,e) {" +
                                                        "grdPurchaseEditService.GetEditor('TotalPriceCK').SetValue(colServiceSum.GetValue()*s.GetValue()/100);" +
                                                        "grdPurchaseEditService.GetEditor('PromotionInNumber').SetValue(colServiceSum.GetValue()*s.GetValue()/100);" +
                                                  "}";
        }

        protected void colServiceAmount_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                spin.Text = DataBinder.Eval(container.DataItem, "Quantity").ToString();
            }

            spin.ClientSideEvents.ValueChanged = "function (s,e) {" +
                                                        "colServiceSum.SetValue(s.GetValue()*colServicePrice.GetValue());" +
                                                  "}";
        }

        protected void colServicePrice_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                spin.Text = DataBinder.Eval(container.DataItem, "Price").ToString();
            }

            spin.ClientSideEvents.ValueChanged = "function (s,e) {" +
                                                        "colServiceSum.SetValue(s.GetValue()*colServiceAmount.GetValue());" +
                                                  "}";
        }

        protected void colServiceSum_Init(object sender, EventArgs e)
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            GridViewDataItemTemplateContainer container = spin.NamingContainer as GridViewDataItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
            {
                spin.Text = DataBinder.Eval(container.DataItem, "TotalPrice").ToString();
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void grdPurchaseEditService_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "TotalPriceCK")
            {
                decimal ck = Convert.ToDecimal(e.GetListSourceFieldValue("PromotionInPercentage"));
                decimal total = Convert.ToDecimal(e.GetListSourceFieldValue("TotalPrice"));
                e.Value = total * ck / 100;
            }
        }
    
        protected void grdPurchaseEditService_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (Session["SaleBillId"] == null)
            {
                return;
            }

            SalesInvoice salesInvoice = session.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));

            if (salesInvoice != null)
            {
                if (salesInvoice.RowStatus == ROW_NOT_DELETE)
                {
                    if (e.VisibleIndex == -1 && e.ButtonType == DevExpress.Web.ASPxGridView.ColumnCommandButtonType.New)
                    {
                        e.Visible = false;
                    }
                }
            }
        }

        protected void grdPurchaseEditProduct_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            SalesInvoice bill = session.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));
            if (bill != null)
            {
                if (bill.RowStatus == ROW_NOT_DELETE)
                {
                    e.Cancel = true;
                    grdPurchaseEditProduct.CancelEdit();
                    grdPurchaseEditProduct.Columns["Thao tác"].Visible = false;
                    grdPurchaseEditService.Columns["Thao tác"].Visible = false;
                    return;
                }
            }
        }

        protected void grdPurchaseEditProduct_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            //SalesInvoice bill = session.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));
            //if (bill != null)
            //{
            //    if (bill.RowStatus == ROW_NOT_DELETE)
            //    {
            //        grdPurchaseEditProduct.CancelEdit();
            //        grdPurchaseEditProduct.Columns["Thao tác"].Visible = false;
            //        grdPurchaseEditService.Columns["Thao tác"].Visible = false;
            //        return;
            //    }
            //}
        }

        protected void grdPurchaseEditProduct_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] p = e.Parameters.Split('|');
            switch (p[0])
            {
                case "balance":
                    ASPxSpinEdit tb = grdPurchaseEditProduct.FindEditRowCellTemplateControl((GridViewDataColumn)grdPurchaseEditProduct.Columns["Balance"], "colBalance") as ASPxSpinEdit;

                    CriteriaOperator filter = new BinaryOperator("ItemUnitId", p[1], BinaryOperatorType.Equal);
                    ItemUnit itemUnit = session.FindObject<ItemUnit>(filter);

                    if (itemUnit != null)
                    {

                        string sql = "" +
                            " select round(Balance,2) as Balance " + 
                            " from InventoryLedger" +
                            " where InventoryLedgerId = (select InventoryLedgerId" +
                                                        " from InventoryLedger" +
                                                        " where ItemUnitid = '" + itemUnit.ItemUnitId + "'" +
                                                        " and issuedate = (select max(issuedate)" +
                                                                         " from InventoryLedger" +
                                                                         " where ItemUnitid = '" + itemUnit.ItemUnitId + "'))";

                        var seletectedData = session.ExecuteScalar(sql);

                        if (seletectedData != null)
                        {
                            tb.Text = seletectedData.ToString();
                        }
                        else
                        {
                            tb.Text = "0";
                        }
                    }
                    

                    break;
                default:
                    break;
            }
        }

        protected void grdPurchaseEditService_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            SalesInvoice bill = session.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));
            if (bill != null)
            {
                if (bill.RowStatus == ROW_NOT_DELETE)
                {
                    e.Cancel = true;
                    grdPurchaseEditService.CancelEdit();
                    grdPurchaseEditProduct.Columns["Thao tác"].Visible = false;
                    grdPurchaseEditService.Columns["Thao tác"].Visible = false;
                    return;
                }
            }
        }

        protected void grdPurchaseEditService_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            SalesInvoice bill = session.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));
            if (bill != null)
            {
                if (bill.RowStatus == ROW_NOT_DELETE)
                {
                    grdPurchaseEditService.CancelEdit();
                    grdPurchaseEditProduct.Columns["Thao tác"].Visible = false;
                    grdPurchaseEditService.Columns["Thao tác"].Visible = false;
                    return;
                }
            }
        }

        protected void grdDeliverySchedule_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (Status == "edit" || Status == "new")
            {
                e.Visible = true;
            }
            else
            {
                e.Visible = false;
            }            
        }

        protected void grdPaymentSchedule_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (Status == "edit" || Status == "new")
            {
                e.Visible = true;
            }
            else
            {
                e.Visible = false;
            }
            
        }

        protected void grdPurchaseEditService_CommandButtonInitialize1(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (Status == "edit" || Status == "new")
            {
                e.Visible = true;
            }
            else
            {
                e.Visible = false;
            }
        }

        protected void grdPaymentSchedule_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            SalesInvoice salesInvoice = session.GetObjectByKey<SalesInvoice>(Guid.Parse(Session["SaleBillId"].ToString()));
            if (salesInvoice != null)
            {                
                e.NewValues["TransactionId"] = Guid.NewGuid();
                e.NewValues["SalesInvoiceId!Key"] = salesInvoice.BillId.ToString();
                //e.NewValues["CreateDate"] = DateTime.Now.ToString();                
                e.NewValues["RowStatus"] = "1";

            }            
        }

        protected void grdPaymentSchedule_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            Transaction transaction = session.GetObjectByKey<Transaction>(Guid.Parse(e.NewValues["TransactionId"].ToString()));
            if (transaction != null)
            {

                CriteriaOperator filter = new BinaryOperator("Code", DefaultAccountEnum.NAAN_DEFAULT.ToString(), BinaryOperatorType.Equal);
                Account account = session.FindObject<Account>(filter);


                GeneralJournal generalJournal = new GeneralJournal(session);
                
                generalJournal.TransactionId = transaction;
                generalJournal.Credit = double.Parse(e.NewValues["Amount"].ToString());
                //generalJournal.CurrencyId = 
                generalJournal.Description = e.NewValues["Code"].ToString();
                generalJournal.JournalType = Constant.PLANNING_JOURNAL;
                generalJournal.RowStatus = Constant.ROWSTATUS_DEFAULT;
                generalJournal.AccountId = account;
                generalJournal.Save();

                generalJournal = new GeneralJournal(session);
                generalJournal.TransactionId = transaction;
                generalJournal.Debit = double.Parse(e.NewValues["Amount"].ToString());
                //generalJournal.CurrencyId = 
                generalJournal.Description = e.NewValues["Code"].ToString();
                generalJournal.JournalType = Constant.PLANNING_JOURNAL;
                generalJournal.RowStatus = Constant.ROWSTATUS_DEFAULT;
                generalJournal.AccountId = account;
                generalJournal.Save();

            }
        }

        protected void grdPaymentSchedule_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Values["RowStatus"] = "-1";
            e.Cancel = true;
        }

        protected void grdPaymentSchedule_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {            
            e.NewValues["JournalType"] = "P";
        }

        protected void grdDeliverySchedule_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {            
            //Debit          

            e.NewValues["InventoryJournalId"] = Guid.NewGuid();

            CriteriaOperator filter = new BinaryOperator("Code", DefaultAccountEnum.OWNER_INVENTORY, BinaryOperatorType.Equal);
            Account account = session.FindObject<Account>(filter);
            if (account != null)
            {
                e.NewValues["AccountId!Key"] = account.AccountId.ToString();
            }

            e.NewValues["InventoryTransactionId!Key"] = Session["InventoryTransactionId"].ToString();
            e.NewValues["JournalType"] = "P";
            e.NewValues["CreateDate"] = DateTime.Now.ToString();
            
        }

        protected void grdDeliverySchedule_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            // Credit

            InventoryJournal inventoryJournal = new InventoryJournal(session);
            inventoryJournal.InventoryJournalId = Guid.NewGuid();

            inventoryJournal.Credit = double.Parse(e.NewValues["Debit"].ToString());

            CriteriaOperator filter = new BinaryOperator("Code", DefaultAccountEnum.CUSTOMER_INVENTORY.ToString(), BinaryOperatorType.Equal);
            Account account = session.FindObject<Account>(filter);
            if (account != null)
            {
                inventoryJournal.AccountId = account;
            }

            inventoryJournal.InventoryTransactionId = session.GetObjectByKey<InventoryTransaction>(Guid.Parse(Session["InventoryTransactionId"].ToString()));

            ItemUnit itemUnit = session.GetObjectByKey<ItemUnit>(Guid.Parse(e.NewValues["ItemUnitId!Key"].ToString()));
            if (itemUnit != null)
            {
                inventoryJournal.ItemUnitId = itemUnit;
            }

            inventoryJournal.JournalType = 'P';
            inventoryJournal.CreateDate = DateTime.Now;

            inventoryJournal.Save();
        }

        protected void grdDeliverySchedule_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.NewValues["JournalType"] = "P";
        }

        protected void grdDeliverySchedule_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            
            
        }

        protected void grdDeliverySchedule_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
          
        }

        protected void grdDeliverySchedule_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "ItemUnitId!Key")
            {
                ASPxComboBox combo = e.Editor as ASPxComboBox;
                combo.ClientSideEvents.ValueChanged = "function(s,e){ " +
                                                           "grdDeliverySchedule.GetEditor('ItemUnitId.UnitId.Name').SetValue(s.GetSelectedItem().GetColumnText('ItemUnitId.UnitId.Name'));" +                                                           
                                                       "}";

            }
        }

        protected void grdDeliveryBillItem_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Values["RowStatus"] = "-1";
        }

        protected void grdDeliveryBillItem_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["InventoryTransactionId"] = Guid.NewGuid();
            e.NewValues["RowStatus"] = "1";
            e.NewValues["CreateDate"] = DateTime.Now.ToString();
            e.NewValues["SalesInvoiceId!Key"] = Session["SaleBillId"].ToString();
        }

        protected void grdDeliveryBillItem_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.NewValues["RowStatus"] = "1";
        }

        protected void grdDeliveryBillItem_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            
        }

        protected void colBillItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;

            CriteriaOperator filter = new BinaryOperator("ItemUnitId", e.Value, BinaryOperatorType.Equal);
            BillItem obj = session.FindObject<BillItem>(filter);

            if (obj != null)
            {
                comboItemUnit.DataSource = new BillItem[] { obj };
                comboItemUnit.DataBindItems();
            }
        }

        protected void colBillItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            XPCollection<BillItem> collection = new XPCollection<BillItem>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = //CriteriaOperator.And(
                    new BinaryOperator("BillId", Guid.Parse(Session["SaleBillId"].ToString()), BinaryOperatorType.Equal);//,
                    //new BinaryOperator("ItemUnitId.ItemId.Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)                    
            //);
                        
            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("ItemUnitId.ItemId.Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();
            
        }

        protected void colPersonOnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            if (e.Value == null)
            {
                return;
            }

            Person obj = session.GetObjectByKey<Person>(Guid.Parse(e.Value.ToString()));

            if (obj != null)
            {
                comboItemUnit.DataSource = new Person[] { obj };
                comboItemUnit.DataBindItems();
            }
        }

        protected void colPersonOnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {

            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            XPCollection<Person> collection = new XPCollection<Person>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = //new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual);
                CriteriaOperator.And(
                    CriteriaOperator.Or(
                        new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                        new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
                        ),
                    new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual));                   
            
                                                  

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();

        }

        protected void colLotIdOnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            if (e.Value == null)
            {
                return;
            }

            Lot obj = session.GetObjectByKey<Lot>(Guid.Parse(e.Value.ToString()));

            if (obj != null)
            {
                comboItemUnit.DataSource = new Lot[] { obj };
                comboItemUnit.DataBindItems();
            }
        }

        protected void colLotIdOnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {

            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            XPCollection<Lot> collection = new XPCollection<Lot>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = CriteriaOperator.And(                  
                   new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),                       
                   new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual));    

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();

        }

        protected void colInventoryOnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            if (e.Value == null)
            {
                return;
            }

            Inventory obj = session.GetObjectByKey<Inventory>(Guid.Parse(e.Value.ToString()));

            if (obj != null)
            {
                comboItemUnit.DataSource = new Inventory[] { obj };
                comboItemUnit.DataBindItems();
            }
        }

        protected void colInventoryOnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            XPCollection<Inventory> collection = new XPCollection<Inventory>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = CriteriaOperator.And(
                 CriteriaOperator.Or(
                     new BinaryOperator("Code", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like),
                     new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Like)
                     ),
                 new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual));    

            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();

        }

        protected void grdBillActor_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {

        }

        protected void grdBillActor_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            //if (e.Column.FieldName == "ActorType")
            //{
            //    switch (e.Value.ToString())
            //    {
            //        case "1":
            //            e.DisplayText = "Người lập phiếu";
            //            break;
            //        case "2":
            //            e.DisplayText = "Kế toán trưởng";
            //            break;
            //        case "3":
            //            e.DisplayText = "Giám đốc";
            //            break;
            //    }
            //}
        }

        protected void formBillActor_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {

        }

        protected override void OnPreRender(EventArgs e)
        {
          
            base.OnPreRender(e);
        }

        protected void grdDeliveryBillItem_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (Status == "edit" || Status == "new")
            {
                e.Visible = true;
            }
            else
            {
                e.Visible = false;
            }
        }

        protected void colBillActorTypeItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            if (e.Value == null)
            {
                return;
            }

            BillActorType obj = session.GetObjectByKey<BillActorType>(Guid.Parse(e.Value.ToString()));

            if (obj != null)
            {
                comboItemUnit.DataSource = new BillActorType[] { obj };
                comboItemUnit.DataBindItems();
            }
        }

        protected void colBillActorTypeItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboItemUnit = source as ASPxComboBox;
            XPCollection<BillActorType> collection = new XPCollection<BillActorType>(session);
            collection.SkipReturnedObjects = e.BeginIndex;
            collection.TopReturnedObjects = e.EndIndex - e.BeginIndex + 1;

            CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("Name", String.Format("%{0}%", e.Filter), BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", 1, BinaryOperatorType.GreaterOrEqual));


            collection.Criteria = criteria;
            collection.Sorting.Add(new SortProperty("Name", DevExpress.Xpo.DB.SortingDirection.Ascending));

            comboItemUnit.DataSource = collection;
            comboItemUnit.DataBindItems();

        }

        protected void grdPaymentScheduleActual_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Equals("Refresh"))
            {
               
                grdPaymentScheduleActual.JSProperties.Add("cpRefresh", grdPaymentScheduleActual.GetTotalSummaryValue(grdPaymentScheduleActual.TotalSummary["Debit"]).ToString());
            }
        }

        protected void grdDeliverySchedule_BeforePerformDataSelect(object sender, EventArgs e)
        {
            Session["InventoryTransactionId"] = (sender as DevExpress.Web.ASPxGridView.ASPxGridView).GetMasterRowKeyValue();
        }

        protected void grdDeliveryScheduleActual_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            InventoryCommandBO inventoryCommandBO = new InventoryCommandBO();
            IEnumerable<InventoryJournal> collectInventoryJournal = inventoryCommandBO.GetActualInventoryJournalOfBill(session,
                Guid.Parse(Session["SaleBillId"].ToString()),
                NAS.DAL.CMS.ObjectDocument.DefaultObjectTypeCustomFieldEnum.INVENTORY_OUT_SALE_INVOICE);
            grdDeliveryScheduleActual.KeyFieldName = "InventoryJournalId";
            grdDeliveryScheduleActual.DataSource = collectInventoryJournal;
            grdDeliveryScheduleActual.DataBind();
        }

        protected void bOutputInventoryCommand_Load(object sender, EventArgs e)
        {
            //if (Session["SaleBillId"] != null)
            //{
            //    if (session.GetObjectByKey<Bill>(Guid.Parse(Session["SaleBillId"].ToString())) != null)
            //    {
            //        uEdittingOutputInventoryCommand1.SettingInit(Guid.Parse(Session["SaleBillId"].ToString()), bOutputInventoryCommand, "SharedClientEvent");
            //    }
            //}
        }
    }
}
