using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using NAS.DAL.BI.Accounting.FinancialActualPrice;
using DevExpress.Xpo;

namespace NAS.BO.ETL.Accounting.FinancialActualPrice
{
    public class FinancialActualPrice155Strategy : FinancialActualPriceStrategy
    {
        private const string ACCOUNT_CODE = "155";

        public FinancialActualPrice155Strategy(Guid transactionId)
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

        //public override void FixInvokedBussinessObjects(DevExpress.Xpo.Session session, DevExpress.Xpo.XPCollection<DAL.System.Log.BusinessObject> invokedBussinessObjects)
        //{
        //    if (invokedBussinessObjects == null || invokedBussinessObjects.Count == 0)
        //        return;

        //    CriteriaOperator criteria_0 = CriteriaOperator.Parse("not(IsNull(FinancialTransactionDimId))");
        //    CriteriaOperator criteria_1 = new InOperator("FinancialTransactionDimId.RefId", invokedBussinessObjects.Select(i => i.RefId));
        //    CriteriaOperator criteria_2 = new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater);
        //    CriteriaOperator criteria = new GroupOperator(GroupOperatorType.And, criteria_0, criteria_1, criteria_2);

        //    XPCollection<FinancialActualPriceDetail> neededToBeFixList = new XPCollection<FinancialActualPriceDetail>(session, criteria);
        //    if (neededToBeFixList != null && neededToBeFixList.Count > 0)
        //        foreach (FinancialActualPriceDetail detail in neededToBeFixList)
        //        {
        //            detail.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
        //            detail.Save();
        //        }
        //}
    }
}
