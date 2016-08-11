using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebModule.Voucher.Controls.GridViewVoucherAllocation.Strategy
{
    public enum GridViewVoucherAllocationStrategyEnum
    {
        RECEIPT_VOUCHER,
        PAYMENT_VOUCHER
    }

    public static class GridViewVoucherAllocationStrategySimpleFactory
    {
        public static GridViewVoucherAllocationStrategy 
            CreateGridViewVoucherAllocationStrategy(GridViewVoucherAllocationStrategyEnum type)
        {
            GridViewVoucherAllocationStrategy ret = null;
            switch (type)
            {
                case GridViewVoucherAllocationStrategyEnum.RECEIPT_VOUCHER:
                    ret = new GridViewReceiptVoucherAllocationStrategy();
                    break;
                case GridViewVoucherAllocationStrategyEnum.PAYMENT_VOUCHER:
                    ret = new GridViewPaymentVoucherAllocationStrategy();
                    break;
                default:
                    break;
            }
            return ret;
        }
    }
}