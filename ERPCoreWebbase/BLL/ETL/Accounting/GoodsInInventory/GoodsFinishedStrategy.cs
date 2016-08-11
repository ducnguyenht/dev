using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAS.BO.ETL.Accounting.GoodsInInventory
{
    public class GoodsFinishedStrategy : GoodsInInventoryStategy
    {
        private const string ACCOUNT_CODE = "155";

        public GoodsFinishedStrategy(Guid transactionId)
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
    }
}
