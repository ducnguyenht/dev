using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.BO.ETL.Bill.TempData;
using DevExpress.Xpo;
using NAS.DAL.Invoice;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL;
using NAS.DAL.Accounting.Journal;
using NAS.BO.ETL.Accounting;
using NAS.BO.ETL.Accounting.TempData;
using NAS.DAL.Inventory.Journal;
using NAS.BO.ETL.Inventory.TempData;
using NAS.BO.ETL.Inventory;

namespace NAS.BO.ETL.Bill
{
    public class ETLBillBO
    {
        public ETL_Bill ExtractSalesInvoice(Session session, Guid BillId,bool ExtractFinancialTransaction, bool ExtractInventoryTransaction)        
        {
            ETL_Bill result = null;
            try
            {
                result = ExtractSalesInvoice(session, BillId);
                if (result == null) return null;
                SalesInvoice invoice = session.GetObjectByKey<SalesInvoice>(BillId);                
                if (ExtractFinancialTransaction)
                {                    
                    ETLAccountingBO _ETLAccountingPeriodBO = new ETLAccountingBO();
                    result.FinancialTranSactionList = new List<ETL_Transaction>();
                    foreach (SaleInvoiceTransaction transaction in invoice.SaleInvoiceTransactions)
                    {
                        ETL_Transaction temp = _ETLAccountingPeriodBO.ExtractTransaction(session, transaction.TransactionId);
                        result.FinancialTranSactionList.Add(temp);
                    }
                }
                if (ExtractInventoryTransaction)
                {
                    ETLInventoryBO _ETLInventoryBO = new ETLInventoryBO();
                    result.InventoryTranSactionList = new List<Inventory.TempData.ETL_InventoryTransaction>();
                    foreach (SalesInvoiceInventoryTransaction transaction in invoice.SalesInvoiceInventoryTransactions)
                    {
                        ETL_InventoryTransaction temp = _ETLInventoryBO.ExtractInventoryTransaction(session, transaction.InventoryTransactionId);
                        result.InventoryTranSactionList.Add(temp);
                    }
                }
            }
            catch(Exception)
            {
                return null;
            }
            return result;
        }
        public ETL_Bill ExtractSalesInvoice(Session session, Guid BillId)
        {
            ETL_Bill result = null;
            try
            {
                Util util = new Util();
                SalesInvoice invoice = session.GetObjectByKey<SalesInvoice>(BillId);
                if (invoice == null) return null;
                result = new ETL_Bill();
                result.BillId = BillId;
                if (invoice.SourceOrganizationId == null)
                {
                    result.CustomerOrgId = Guid.Empty;
                }
                else
                {
                    result.CustomerOrgId = invoice.SourceOrganizationId.OrganizationId;
                }
                result.OwnerOrgId = util.GetXpoObjectByFieldName<Organization, string>(session, "Code", "QUASAPHARCO", DevExpress.Data.Filtering.BinaryOperatorType.Equal).OrganizationId;
                result.IssueDate = invoice.IssuedDate;
                result.BillItemList = new List<ETL_BillItem>();
                foreach (BillItem billItem in invoice.BillItems)
                {
                    ETL_BillItem temp = new ETL_BillItem();
                    temp.Asset = "VND";
                    temp.Amount = (billItem.Quantity * billItem.Price);
                    temp.item = billItem.ItemId;
                    temp.unit = billItem.UnitId;
                    result.BillItemList.Add(temp);
                }                
            }
            catch (Exception)
            {
            }
            return result;
        }

        public ETL_Bill ExtractPurchaseInvoice(Session session, Guid BillId, bool ExtractFinancialTransaction, bool ExtractInventoryTransaction)
        {
            ETL_Bill result = null;
            try
            {
                result = ExtractPurchaseInvoice(session, BillId);
                if (result == null) return null;
                NAS.DAL.Invoice.PurchaseInvoice invoice = session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(BillId);
                if (ExtractFinancialTransaction)
                {
                    ETLAccountingBO _ETLAccountingPeriodBO = new ETLAccountingBO();
                    result.FinancialTranSactionList = new List<ETL_Transaction>();
                    foreach (PurchaseInvoiceTransaction transaction in invoice.PurchaseInvoiceTransactions)
                    {
                        ETL_Transaction temp = _ETLAccountingPeriodBO.ExtractTransaction(session, transaction.TransactionId);
                        result.FinancialTranSactionList.Add(temp);
                    }
                }
                if (ExtractInventoryTransaction)
                {
                    ETLInventoryBO _ETLInventoryBO = new ETLInventoryBO();
                    result.InventoryTranSactionList = new List<Inventory.TempData.ETL_InventoryTransaction>();
                    foreach (PurchaseInvoiceInventoryTransaction transaction in invoice.PurchaseInvoiceInventoryTransactions)
                    {
                        ETL_InventoryTransaction temp = _ETLInventoryBO.ExtractInventoryTransaction(session, transaction.InventoryTransactionId);
                        result.InventoryTranSactionList.Add(temp);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }
        public ETL_Bill ExtractPurchaseInvoice(Session session, Guid BillId)
        {
            ETL_Bill result = null;
            try
            {
                Util util = new Util();
                NAS.DAL.Invoice.PurchaseInvoice invoice = session.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(BillId);
                if (invoice == null) return null;
                result = new ETL_Bill();
                result.BillId = BillId;
                if (invoice.SourceOrganizationId == null)
                {
                    result.SupplierOrgId = Guid.Empty;
                }
                else
                {
                    result.SupplierOrgId = invoice.SourceOrganizationId.OrganizationId;
                }
                result.OwnerOrgId = util.GetXpoObjectByFieldName<Organization, string>(session, "Code", "QUASAPHARCO", DevExpress.Data.Filtering.BinaryOperatorType.Equal).OrganizationId;
                result.IssueDate = invoice.IssuedDate;
                result.BillItemList = new List<ETL_BillItem>();
                foreach (BillItem billItem in invoice.BillItems)
                {
                    ETL_BillItem temp = new ETL_BillItem();
                    temp.Asset = "VND";
                    temp.Amount = (billItem.Quantity * billItem.Price);
                    temp.item = billItem.ItemId;
                    temp.unit = billItem.UnitId;
                    result.BillItemList.Add(temp);
                }
            }
            catch (Exception)
            {
            }
            return result;
        }        
    }
}
