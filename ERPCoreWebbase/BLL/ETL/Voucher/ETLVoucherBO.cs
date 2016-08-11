using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.BO.ETL.Voucher.TempData;
using DevExpress.Xpo;
using NAS.DAL.Vouches;
using NAS.DAL.Accounting.Journal;
using NAS.BO.ETL.Accounting.TempData;
using NAS.BO.ETL.Accounting;

namespace NAS.BO.ETL.Voucher
{
    public class ETLVoucherBO
    {        
        public ETL_Voucher ExtractPaymentVoucher(Session session, Guid VoucherId,bool ExtractTransaction)
        {
            ETL_Voucher result = null;
            try
            {
                PaymentVouches voucher = session.GetObjectByKey<PaymentVouches>(VoucherId);
                if (voucher == null)
                {
                    return null;
                }
                result = new ETL_Voucher();
                result.Credit = (decimal)voucher.SumOfCredit;
                result.Debit = (decimal)voucher.SumOfDebit;
                result.IssueDate = voucher.IssuedDate;
                result.VoucherId = voucher.VouchesId;                
                if (ExtractTransaction)
                {
                    ETLAccountingBO etlAccountingBO = new ETLAccountingBO();
                    result.FinancialTransactionList = new List<Accounting.TempData.ETL_Transaction>();
                    foreach (PaymentVouchesTransaction transaction in voucher.PaymentVouchesTransactions)
                    {
                        ETL_Transaction temp = new ETL_Transaction();
                        temp = etlAccountingBO.ExtractTransaction(session, transaction.TransactionId);
                        result.FinancialTransactionList.Add(temp);
                    }
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
        public ETL_Voucher ExtractReceiptVoucher(Session session, Guid VoucherId, bool ExtractTransaction)
        {
            ETL_Voucher result = null;
            try
            {
                ReceiptVouches voucher = session.GetObjectByKey<ReceiptVouches>(VoucherId);
                if (voucher == null)
                {
                    return null;
                }
                result = new ETL_Voucher();
                result.Credit = (decimal)voucher.SumOfCredit;
                result.Debit = (decimal)voucher.SumOfDebit;
                result.IssueDate = voucher.IssuedDate;
                result.VoucherId = voucher.VouchesId;
                if (ExtractTransaction)
                {
                    ETLAccountingBO etlAccountingBO = new ETLAccountingBO();
                    result.FinancialTransactionList = new List<Accounting.TempData.ETL_Transaction>();
                    foreach (ReceiptVouchesTransaction transaction in voucher.ReceiptVouchesTransactions)
                    {
                        ETL_Transaction temp = new ETL_Transaction();
                        temp = etlAccountingBO.ExtractTransaction(session, transaction.TransactionId);
                        result.FinancialTransactionList.Add(temp);
                    }
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
    }
}
