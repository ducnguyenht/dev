using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.ETL.Accounting.FinancialPrepaidExpense
{
    public class FinancialPrepaidExpense335Strategy : FinancialPrepaidExpenseStrategy
    {
        private const string ACCOUNT_CODE = "335";

        public FinancialPrepaidExpense335Strategy(Guid transactionId)
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
                this.fFinancialTransformData = base.TransformTransaction(session, this.fFinancialTransactionData, ACCOUNT_CODE);
        }

        public override void LoadTransaction(DevExpress.Xpo.Session session)
        {
            base.LoadTransaction(session, this.fFinancialTransformData);
        }
    }
}
