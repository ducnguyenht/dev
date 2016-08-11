using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.ETL.Accounting.FinancialActualPrice
{
    public class FinancialActualPrice158Strategy : FinancialActualPriceStrategy
    {
        private const string ACCOUNT_CODE = "158";

        public FinancialActualPrice158Strategy(Guid transactionId)
            : base(transactionId, ACCOUNT_CODE)
        {
        }
        public override void ExtractTransaction(DevExpress.Xpo.Session session)
        {
            base.GetIsRelatedStrategy(session);
            if (IsRelatedStrategy)
            {
                this.fActualPriceTransaction = base.ExtractTransaction(session, TransactionId, ACCOUNT_CODE);
            }
        }

        public override void TransformTransaction(DevExpress.Xpo.Session session)
        {
            if (IsRelatedStrategy)
                this.fDetailAfterTransformData
                    = base.TransformTransactionFinancialActualPriceDetail(session, this.fActualPriceTransaction, ACCOUNT_CODE);
        }

        public override void LoadTransaction(DevExpress.Xpo.Session session)
        {
            base.LoadFinancialActualPriceDetail(session, this.fDetailAfterTransformData);
        }

    }
}
