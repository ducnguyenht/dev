using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NAS.BO.Accounting.Journal;
using DevExpress.Data.Filtering;

namespace WebModule.Voucher.Controls.GridViewVoucherAllocation.Strategy
{
    public abstract class GridViewVoucherAllocationStrategy
    {
        private VoucherTransactionBOBase VoucherTransactionBO
        {
            get;
            set;
        }

        public GridViewVoucherAllocationStrategy()
        {
            VoucherTransactionBO = CreateVoucherTransactionBO();
        }

        public abstract VoucherTransactionBOBase CreateVoucherTransactionBO();

        public abstract CriteriaOperator GetVoucherTransactionCriteria(Guid voucherId);

        public abstract Type GetConcreteVoucherType();

        public virtual void CreateVoucherTransaction(Guid voucherId, string code, DateTime issuedDate, double amount, string description)
        {
            VoucherTransactionBO.CreateTransaction(voucherId, code, issuedDate, amount, description);
        }

        public virtual void DeleteVoucherTransaction(Guid transactionId)
        {
            VoucherTransactionBO.DeleteTransaction(transactionId);
        }

        public virtual void UpdateVoucherTransaction(Guid transactionId, string code, DateTime issuedDate, double amount, string description)
        {
            VoucherTransactionBO.UpdateTransaction(transactionId, code, issuedDate, amount, description);
        }
    }
}