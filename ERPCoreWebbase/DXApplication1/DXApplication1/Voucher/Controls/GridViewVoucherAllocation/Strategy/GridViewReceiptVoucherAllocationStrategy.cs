using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Data.Filtering;

namespace WebModule.Voucher.Controls.GridViewVoucherAllocation.Strategy
{
    public class GridViewReceiptVoucherAllocationStrategy : GridViewVoucherAllocationStrategy
    {
        public GridViewReceiptVoucherAllocationStrategy() : base() { }

        public override NAS.BO.Accounting.Journal.VoucherTransactionBOBase CreateVoucherTransactionBO()
        {
            return new NAS.BO.Accounting.Journal.ReceiptVoucherTransactionBO();
        }

        public override Type GetConcreteVoucherType()
        {
            return typeof(NAS.DAL.Accounting.Journal.ReceiptVouchesTransaction);
        }

        public override DevExpress.Data.Filtering.CriteriaOperator GetVoucherTransactionCriteria(Guid voucherId)
        {
            return CriteriaOperator.And(
                new BinaryOperator("ReceiptVouchesId!Key", voucherId),
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual)
            );
        }
    }
}