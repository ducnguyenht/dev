using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.ETL.Accounting.FinancialSalesOrManufactureExpense
{
    public class SalesOrManufacturerExpense642Strategy: SalesOrManufacturerExpenseStrategy
    {
        private const string ACCOUNT_CODE = "642";

        public SalesOrManufacturerExpense642Strategy(Guid transactionId)
            : base(transactionId, ACCOUNT_CODE)
        {
        }

        public override void ExtractTransaction(DevExpress.Xpo.Session session)
        {
            base.GetIsRelatedStrategy(session);
            if (IsRelatedStrategy)
            {
                this.fFinancialTransactionData = base.ExtractTransaction(session, TransactionId, ACCOUNT_CODE);
            }
        }

        public override void TransformTransaction(DevExpress.Xpo.Session session)
        {
            if (IsRelatedStrategy)
                this.fFinancialTransformData = base.TransformTransactionSalesOrManufacturerExpenseDetail(session, this.fFinancialTransactionData, ACCOUNT_CODE);
        }

        public override void LoadTransaction(DevExpress.Xpo.Session session)
        {
            base.LoadFinancialSalesOrManufacturerExpenseDetail(session, this.fFinancialTransformData);
        }
    }
}
