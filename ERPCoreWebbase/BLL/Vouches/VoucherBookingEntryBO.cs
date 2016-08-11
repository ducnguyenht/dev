using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Data.Filtering;
using NAS.DAL.Vouches.Allocation;
using NAS.DAL.Accounting.Configure;

namespace NAS.BO.Vouches
{
    public class VoucherBookingEntryBO
    {
        public void ValidateVoucherForBookingEntry(Guid VouchesId)
        {
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                try
                {
                    NAS.DAL.Vouches.Vouches voucher = uow.GetObjectByKey<NAS.DAL.Vouches.Vouches>(VouchesId);

                    if (voucher == null)
                        throw new Exception("The Vouches is not exist in db");

                    string AllocationTypeCode = string.Empty;

                    if (voucher is NAS.DAL.Vouches.PaymentVouches)
                        AllocationTypeCode = "VOUCHER_PAYMENT";
                    if (voucher is NAS.DAL.Vouches.ReceiptVouches)
                        AllocationTypeCode = "VOUCHER_RECEIPT";

                    XPCollection<VoucherAllocation> voucherAllocationLst = new XPCollection<VoucherAllocation>(uow,
                        new BinaryOperator("VouchesId", voucher, BinaryOperatorType.Equal));

                    if (voucherAllocationLst == null || voucherAllocationLst.Count == 0)
                        throw new Exception(string.Format("Thông tin hạch toán của phiếu {0} '{1}' chưa có dòng bút toán nào nên không thể hạch toán",
                            AllocationTypeCode.Equals("VOUCHER_PAYMENT") ? "Chi" : "Thu", voucher.Code ));

                    foreach (VoucherAllocation va in voucherAllocationLst)
                    {
                        double sumOfDebit = 0;
                        double sumOfCredit = 0;

                        foreach (VoucherAllocationBookingAccount vaba in va.VoucherAllocationBookingAccounts)
                        {
                            sumOfDebit += vaba.Debit;
                            sumOfCredit += vaba.Credit;
                        }

                        if (sumOfDebit != sumOfCredit)
                            throw new Exception(string.Format("Thông tin hạch toán của phiếu {0} '{1}' chưa cân đối ở mã bút toán '{2}'",
                            AllocationTypeCode.Equals("VOUCHER_PAYMENT") ? "Chi" : "Thu", voucher.Code, va.Code));

                        if (va.Amount != sumOfDebit || va.Amount != sumOfCredit)
                            throw new Exception(string.Format("Thông tin hạch toán của phiếu {0} '{1}' chưa phân bổ đủ tại mã bút toán '{2}'",
                            AllocationTypeCode.Equals("VOUCHER_PAYMENT") ? "Chi" : "Thu", voucher.Code, va.Code));
                    }
                }
                catch
                {
                    //uow.ExplicitRollbackTransaction();
                    throw;
                }
            }
        }

        public bool GenerateTemplateVoucherForBookingEntry(Guid VouchesId)
        { 
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                try
                {
                    NAS.DAL.Vouches.Vouches voucher = uow.GetObjectByKey<NAS.DAL.Vouches.Vouches>(VouchesId);

                    if (voucher == null)
                        throw new Exception("The Vouches is not exist in db");

                    string AllocationTypeCode = string.Empty;

                    if (voucher is NAS.DAL.Vouches.PaymentVouches)
                        AllocationTypeCode = "VOUCHER_PAYMENT";
                    if (voucher is NAS.DAL.Vouches.ReceiptVouches)
                        AllocationTypeCode = "VOUCHER_RECEIPT";

                    XPCollection<VoucherAllocation> voucherAllocationLst = new XPCollection<VoucherAllocation>(uow, 
                        new BinaryOperator("VouchesId", voucher, BinaryOperatorType.Equal));

                    XPCollection<Allocation> allocationsTemplate = new XPCollection<Allocation>(uow,
                        new BinaryOperator("AllocationTypeId.Code", AllocationTypeCode, BinaryOperatorType.Equal));

                    if (voucherAllocationLst != null && voucherAllocationLst.Count > 0)
                    {
                        foreach (VoucherAllocation va in voucherAllocationLst)
                        {
                            IEnumerable<Allocation> allocations = allocationsTemplate.Where( t => t == va.AllocationId);
                            if (allocations != null && allocations.Count<Allocation>() <= 0)
                                continue;

                            if (va.VoucherAllocationBookingAccounts != null && va.VoucherAllocationBookingAccounts.Count > 0)
                                continue;

                            foreach(AllocationAccountTemplate aat in va.AllocationId.AllocationTemplates)
                            {
                                VoucherAllocationBookingAccount vaba = new VoucherAllocationBookingAccount(uow);
                                vaba.AccountId = aat.AccountId;
                                vaba.VoucherAllocationId = va;
                            }
                        }
                    }
                    uow.CommitChanges();
                    return true;
                }
                catch
                { 
                    //uow.ExplicitRollbackTransaction();
                    throw;
                }
            }
        }
    }
}
