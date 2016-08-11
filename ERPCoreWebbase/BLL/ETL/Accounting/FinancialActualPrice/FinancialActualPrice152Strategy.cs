using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.BO.ETL.Accounting.FinancialActualPrice.TempData;
using DevExpress.Xpo;
using NAS.DAL.BI.Accounting.FinancialActualPrice;
using DevExpress.Data.Filtering;

namespace NAS.BO.ETL.Accounting.FinancialActualPrice
{
    public class FinancialActualPrice152Strategy : FinancialActualPriceStrategy
    {
        private const string ACCOUNT_CODE = "152";

        public FinancialActualPrice152Strategy(Guid transactionId)
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
                this.fDetailAfterTransformData = base.TransformTransactionFinancialActualPriceDetail(session, this.fActualPriceTransaction, ACCOUNT_CODE);
        }

        public override void LoadTransaction(DevExpress.Xpo.Session session)
        {
            base.LoadFinancialActualPriceDetail(session, this.fDetailAfterTransformData);
        }
    }
}
