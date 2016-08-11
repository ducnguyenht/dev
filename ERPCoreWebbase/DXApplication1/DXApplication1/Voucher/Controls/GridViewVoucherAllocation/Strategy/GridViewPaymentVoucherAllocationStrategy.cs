using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Data.Filtering;

namespace WebModule.Voucher.Controls.GridViewVoucherAllocation.Strategy
{
    public class GridViewPaymentVoucherAllocationStrategy : GridViewVoucherAllocationStrategy
    {
        public GridViewPaymentVoucherAllocationStrategy() : base() { }

        public override NAS.BO.Accounting.Journal.VoucherTransactionBOBase CreateVoucherTransactionBO()
        {
            return new NAS.BO.Accounting.Journal.PaymentVoucherTransactionBO();
        }

        public override Type GetConcreteVoucherType()
        {
            return typeof(NAS.DAL.Accounting.Journal.PaymentVouchesTransaction);
        }

        public override DevExpress.Data.Filtering.CriteriaOperator GetVoucherTransactionCriteria(Guid voucherId)
        {
            return CriteriaOperator.And(
                new BinaryOperator("PaymentVouchesId!Key", voucherId),
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual)
            );
        }
    }
}