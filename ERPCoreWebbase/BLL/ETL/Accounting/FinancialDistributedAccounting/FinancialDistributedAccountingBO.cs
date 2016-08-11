using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;
using NAS.BO.ETL.Accounting.TempData;
using Utility;

namespace NAS.BO.ETL.Accounting.FinancialDistributedAccounting
{
    public class FinancialDistributedAccountingBO
    {
        //ETLAccountingBO _ETLAccountingBO = new ETLAccountingBO();
        
        //public NAS.BO.ETL.Accounting.FinancialDistributedAccounting.TempData.Transaction Extract(Session session, Guid RefId)
        //{
        //    NAS.BO.ETL.Accounting.FinancialDistributedAccounting.TempData.Transaction result = null;            
        //    try
        //    {
        //        result = (NAS.BO.ETL.Accounting.FinancialDistributedAccounting.TempData.Transaction)_ETLAccountingBO.ExtractTransaction(session, RefId, "113");                
        //        Transaction transaction = session.GetObjectByKey<Transaction>(RefId);
        //        result.AccountingPeriodId = transaction.AccountingPeriodId.AccountingPeriodId;                
        //        if(transaction is SaleInvoiceTransaction)
        //        {
        //            result.ArtifactRefId = (transaction as SaleInvoiceTransaction).SalesInvoiceId.BillId;
        //            result.ArtifactDescription = (transaction as SaleInvoiceTransaction).SalesInvoiceId.Code;
        //        }
        //        if (transaction is PurchaseInvoiceTransaction)
        //        {
        //            result.ArtifactRefId = (transaction as PurchaseInvoiceTransaction).PurchaseInvoiceId.BillId;
        //            result.ArtifactDescription = (transaction as PurchaseInvoiceTransaction).PurchaseInvoiceId.Code;
        //        }
        //        if (transaction is PaymentVouchesTransaction)
        //        {
        //            result.ArtifactRefId = (transaction as PaymentVouchesTransaction).PaymentVouchesId.VouchesId;
        //            result.ArtifactDescription = (transaction as PaymentVouchesTransaction).PaymentVouchesId.Code;
        //        }
        //        if (transaction is ReceiptVouchesTransaction)
        //        {
        //            result.ArtifactRefId = (transaction as ReceiptVouchesTransaction).ReceiptVouchesId.VouchesId;
        //            result.ArtifactDescription = (transaction as ReceiptVouchesTransaction).ReceiptVouchesId.Code;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //    return result;
        //}

        //public List<NAS.BO.ETL.Accounting.FinancialDistributedAccounting.TempData.ETL_FinancialDistributedAccounting_Fact> Transfrom(Session session,NAS.BO.ETL.Accounting.FinancialDistributedAccounting.TempData.Transaction transaction)
        //{
        //    List<NAS.BO.ETL.Accounting.FinancialDistributedAccounting.TempData.ETL_FinancialDistributedAccounting_Fact> result = null;
        //    try
        //    {
        //        if (transaction == null)
        //        {
        //            return null;
        //        }
        //        result = new List<TempData.ETL_FinancialDistributedAccounting_Fact>();
        //        foreach (ETL_GeneralJournal journal in transaction.GeneralJournalList)
        //        {
        //            TempData.ETL_FinancialDistributedAccounting_Fact Fact_Temp = new TempData.ETL_FinancialDistributedAccounting_Fact();
        //            Fact_Temp.AccountPeriodId = transaction.AccountingPeriodId;
        //            Fact_Temp.Amount = (decimal)(journal.Credit + journal.Debit);
        //            Fact_Temp.CreditAccountId = journal.Credit > 0 ? journal.AccountId : Guid.Empty;
        //            Fact_Temp.DebitAccountId = journal.Debit > 0 ? journal.AccountId : Guid.Empty;
        //            //Asset unused
        //            Fact_Temp.FinancialAssetDimId = Guid.Empty;
        //            Fact_Temp.IssuedDate = transaction.IssuedDate;
        //            result.Add(Fact_Temp);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    return result;
        //}

        //public void Load(Session session)
        //{
        //}

        //public void CreatFinancialDistributedAccountingFact(Session session, TempData.ETL_FinancialDistributedAccounting_Fact Fact)
        //{
        //    try
        //    {
        //        FinancialDistributedAccounting_Fact newFact = new FinancialDistributedAccounting_Fact(session);
        //        //newFact.AccountingPeriodId
        //        //newFact.Amount
        //        newFact.Amount = Fact.Amount;
        //        //newFact.BusinessArtifactId
        //        //newFact.CreditAccountDimId
        //        //newFact.DebitAccountDimId
        //        //newFact.FinancialAssetDimId
                
        //        //newFact.IssuedDate
        //        newFact.IssuedDate = Fact.IssuedDate;
        //        //newFact.RowStatus
        //        newFact.RowStatus = Constant.ROWSTATUS_ACTIVE;
        //        newFact.Save();
        //    }
        //    catch(Exception)
        //    {
        //    }
        //}

        //public void CreatFinancialDistributedAccountingFact(Session session, List<TempData.ETL_FinancialDistributedAccounting_Fact> FactList)
        //{
        //    try
        //    {
        //        foreach (TempData.ETL_FinancialDistributedAccounting_Fact Fact in FactList)
        //        {
        //            CreatFinancialDistributedAccountingFact(session, Fact);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
    }
}
