using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.DAL;
using DevExpress.Xpo;

namespace NAS.BO.Vouches
{
    public class VoucherBO
    {
        public bool DeleteTempObject(Guid voucherId)
        {
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                NAS.DAL.Vouches.Vouches voucher = uow.GetObjectByKey<NAS.DAL.Vouches.Vouches>(voucherId);
                if (voucher == null)
                {
                    throw new Exception("The voucher does not exist");
                }
                if (!voucher.RowStatus.Equals(Utility.Constant.ROWSTATUS_TEMP))
                {
                    throw new Exception("The object is not a temp object");
                }
                voucher.Delete();
                uow.CommitChanges();
                return true;
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

        public bool DeleteLogical(Guid voucherId)
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                NAS.DAL.Vouches.Vouches voucher = session.GetObjectByKey<NAS.DAL.Vouches.Vouches>(voucherId);
                if (voucher == null)
                {
                    throw new Exception("The voucher does not exist");
                }
                if (voucher.RowStatus.Equals(Utility.Constant.ROWSTATUS_BOOKED_ENTRY))
                {
                    throw new Exception(
                        String.Format("Không thể xóa phiếu thu '{0}' vì đã được hạch toán", voucher.Code));
                }
                voucher.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                voucher.Save();
                return true;
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

        public bool CanBookingEntry(Guid voucherId)
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                NAS.DAL.Vouches.Vouches voucher = session.GetObjectByKey<NAS.DAL.Vouches.Vouches>(voucherId);
                if (voucher == null)
                {
                    throw new Exception("The voucher does not exist");
                }
                if (voucher.RowStatus.Equals(Utility.Constant.ROWSTATUS_TEMP))
                {
                    return false;
                }
                //2013-12-12 ERP-951 Khoa.Truong MOD START
                //double sumOfAllocationAmount = voucher.VoucherAllocations.Sum(r => r.Amount);
                double sumOfAllocationAmount = double.MinValue;
                if (voucher is NAS.DAL.Vouches.ReceiptVouches)
                {
                    sumOfAllocationAmount = 
                        ((NAS.DAL.Vouches.ReceiptVouches)voucher)
                            .ReceiptVouchesTransactions
                            .Where(r => r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                            .Sum(r => r.Amount);
                }
                else if (voucher is NAS.DAL.Vouches.PaymentVouches)
                {
                    sumOfAllocationAmount =
                        ((NAS.DAL.Vouches.PaymentVouches)voucher)
                            .PaymentVouchesTransactions
                            .Where(r => r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                            .Sum(r => r.Amount);
                }
                //2013-12-12 ERP-951 Khoa.Truong MOD END
                double voucherAmount = voucher.SumOfCredit + voucher.SumOfDebit;
                if (sumOfAllocationAmount != voucherAmount)
                    return false;
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }


    }
}
