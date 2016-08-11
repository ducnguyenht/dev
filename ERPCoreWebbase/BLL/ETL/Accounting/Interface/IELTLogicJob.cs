using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.System.Log;

namespace NAS.BO.ETL.Accounting.Interface
{
    public interface IELTLogicJob
    {
        string AccountCode
        {
            get;
        }

        Guid TransactionId
        {
            get;
        }

        bool IsRelatedStrategy
        {
            get;
        }

        void GetIsRelatedStrategy(Session session);

        void FixInvokedBussinessObjects(Session session, XPCollection<BusinessObject> invokedBussinessObjects);

        void ExtractTransaction(Session session);

        void TransformTransaction(Session session);

        void LoadTransaction(Session session);
    }
}
