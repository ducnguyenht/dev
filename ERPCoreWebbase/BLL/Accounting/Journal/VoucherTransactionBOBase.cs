using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Vouches;
using NAS.ETLBO.System.Object;

namespace NAS.BO.Accounting.Journal
{
    public abstract class VoucherTransactionBOBase
    {
        private BusinessObjectBO BusinessObjectBO = new BusinessObjectBO();

        public abstract void CreateTransaction(Guid voucherId, string code, DateTime issuedDate, double amount, string description);

        public abstract void DeleteTransaction(Guid transactionId);

        public abstract void UpdateTransaction(Guid transactionId, string code, DateTime issuedDate, double amount, string description);

        public abstract bool CanBookEntries(Guid voucherId);

        public virtual bool IsVoucherLockedBookingEntry(Guid voucherId)
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //Get voucher
                NAS.DAL.Vouches.Vouches voucher = session.GetObjectByKey<NAS.DAL.Vouches.Vouches>(voucherId);
                if(voucher == null)
                {
                    throw new Exception("Could not found voucher");
                }
                if (voucher.RowStatus.Equals(Utility.Constant.ROWSTATUS_BOOKED_ENTRY))
                {
                    return true;
                }
                else if (voucher.RowStatus >= Utility.Constant.ROWSTATUS_TEMP)
                {
                    return false;
                }
                else
                {
                    throw new Exception("Invalid voucher state");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();

            }
        }

        public virtual bool BookEntries(Guid voucherId)
        {
            UnitOfWork uow = null;
            try
            {
                if (!CanBookEntries(voucherId))
                    return false;
                uow = XpoHelper.GetNewUnitOfWork();
                //Get voucher
                NAS.DAL.Vouches.Vouches voucher = uow.GetObjectByKey<NAS.DAL.Vouches.Vouches>(voucherId);
                if (voucher == null)
                {
                    throw new Exception("Could not found voucher");
                }
                if (voucher.RowStatus.Equals(Utility.Constant.ROWSTATUS_BOOKED_ENTRY))
                {
                    throw new Exception(String.Format("Chứng từ '{0}' đã được ghi sổ", voucher.Code));
                }
                else if (voucher.RowStatus > Utility.Constant.ROWSTATUS_TEMP)
                {
                    /*2014-02-13 ERP-1417 Duc.Vo INS START*/
                    int objectFinacialType = int.MinValue;
                    /*2014-02-13 ERP-1417 Duc.Vo INS END*/

                    voucher.RowStatus = Utility.Constant.ROWSTATUS_BOOKED_ENTRY;
                    voucher.Save();

                    IEnumerable<Transaction> tranasctions = null;
                    //Get transactions of the voucher
                    /*2014-02-13 ERP-1417 Duc.Vo INS START*/
                    if (voucher is ReceiptVouches)
                    {
                        tranasctions = ((ReceiptVouches)voucher).ReceiptVouchesTransactions
                            .Where(r => r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE || r.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY);
                        objectFinacialType = Utility.Constant.BusinessObjectType_ReceiptVoucherTransaction;
                    }
                    else if (voucher is PaymentVouches)
                    {
                        tranasctions = ((PaymentVouches)voucher).PaymentVouchesTransactions
                            .Where(r => r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE || r.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY);
                        objectFinacialType = Utility.Constant.BusinessObjectType_PaymentVoucherTransaction;
                    }
                    if (tranasctions == null)
                        throw new Exception("Operation is not valid");
                    /*2014-02-13 ERP-1417 Duc.Vo MOD END*/

                    /*2014-01-17 ERP-1417 Khoa.Truong INS START*/
                    //Get accounting period
                    AccountingPeriod accountingPeriod = 
                        AccountingPeriodBO.GetAccountingPeriod(uow, voucher.IssuedDate);
                    /*2014-01-17 ERP-1417 Khoa.Truong INS END*/

                    foreach (var tranasction in tranasctions)
                    {
                        tranasction.RowStatus = Utility.Constant.ROWSTATUS_BOOKED_ENTRY;
                        /*2014-01-17 ERP-1417 Khoa.Truong INS START*/
                        tranasction.AccountingPeriodId = accountingPeriod;
                        /*2014-01-17 ERP-1417 Khoa.Truong INS END*/
                        tranasction.Save();

                        IEnumerable<GeneralJournal> generalJournals = null;
                        generalJournals = tranasction.GeneralJournals
                            .Where(r => r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE || r.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY);
                        if (generalJournals == null)
                            throw new Exception("Operation is not valid");

                        foreach (var generalJournal in generalJournals)
                        {
                            generalJournal.RowStatus = Utility.Constant.ROWSTATUS_BOOKED_ENTRY;
                            generalJournal.Save();
                        }
                    }

                    /*2014-02-13 ERP-1417 Duc.Vo INS START*/
                    foreach (var tranasction in tranasctions)
                    {
                        BusinessObjectBO.CreateBusinessObject(uow,
                            objectFinacialType,
                            tranasction.TransactionId,
                            tranasction.IssueDate);
                    }
                    /*2014-02-13 ERP-1417 Duc.Vo INS END*/

                    uow.CommitChanges();

                    return true;
                }
                else
                {
                    throw new Exception("Invalid voucher state");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (uow != null) uow.Dispose();

            }
        }

        public abstract XPCollection<Transaction> GetRelatedTransactionsWithBill(Session session, Guid billId);

        public abstract XPCollection<GeneralJournal> GetActuallyCollectedOfBill(Session session, Guid billId);
        //public abstract XPCollection<GeneralJournal> GetRelatedGeneralJournalsWithBill(Session session, Guid billId);

    }
}
