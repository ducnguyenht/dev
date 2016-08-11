using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Voucher.Controls.VoucherBookingEntriesForm.Strategy
{
    public enum VoucherBookingEntriesFormStrategySimpleEnum
    {
        RECEIPT_VOUCHER,
        PAYMENT_VOUCHER
    }

    public static class VoucherBookingEntriesFormStrategySimpleFactory
    {
        public static VoucherBookingEntriesFormStrategy
            CreateVoucherBookingEntriesFormStrategy(VoucherBookingEntriesFormStrategySimpleEnum type)
        {
            VoucherBookingEntriesFormStrategy ret = null;
            switch (type)
            {
                case VoucherBookingEntriesFormStrategySimpleEnum.RECEIPT_VOUCHER:
                    ret = new ReceiptVoucherBookingEntriesFormStrategy();
                    break;
                case VoucherBookingEntriesFormStrategySimpleEnum.PAYMENT_VOUCHER:
                    ret = new PaymentVoucherBookingEntriesFormStrategy();
                    break;
                default:
                    break;
            }
            return ret;
        }
    }

}